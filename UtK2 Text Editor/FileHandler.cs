using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using System.Reflection;

namespace UtK2_Text_Editor
{
    internal class FileHandler
    {
        private Dictionary<string, String> correspondances;
        private Dictionary<string, String> inv_correspondances;
        private FileStream stream;
        public FileHandler()
        {
            GetCorrespondances();
            GetInvCorrespondances();
        }

        public void Encode(string StrToEncode, byte[] ROM, uint startIndex, uint size)
        {
            BinaryWriter Writer = new BinaryWriter(new FileStream("rom.nds", FileMode.Create));
            List<byte> arr = new List<byte>();

            bool was_flag = false;

            for (int i = 0; i < StrToEncode.Length; i++)
            {
                if (StrToEncode[i] == '\r')
                {
                    var tmp = Convert.FromHexString("F3FF");
                    foreach (var t in tmp) { arr.Add(t); }
                }
                if (StrToEncode[i] == '{')
                {
                    var tmp1 = Convert.FromHexString(Char.ToString(StrToEncode[i + 6]) + Char.ToString(StrToEncode[i + 7]) + Char.ToString(StrToEncode[i + 1]) + Char.ToString(StrToEncode[i + 2]));
                    foreach (var t in tmp1)
                    {
                        arr.Add(t);
                        //Debug.Write(t);
                    }
                    was_flag = true;
                    i += 8;
                }
                else
                {
                    if (inv_correspondances.ContainsKey(Char.ToString(StrToEncode[i])))
                    {
                        var cor = inv_correspondances[Char.ToString(StrToEncode[i])];
                        var tmp = Convert.FromHexString(cor);
                        foreach (var t in tmp) { arr.Add(t); }
                        was_flag = false;
                    }
                }
            }

            // TODO: smarter way of doing the resizing, this works, but a more optimal way probably exists
            if (size < arr.Count) {
                arr.RemoveRange((int)size, (int)(arr.Count - size));
            }

            if (size > arr.Count) {
                for (int i = 0; i < (size - arr.Count); i++)
                {
                    // should probably just go straight from a null byte array instead 
                    var tmp = Convert.FromHexString("0000");
                    foreach (var t in tmp) { arr.Add(t); }
                }
            }

            foreach (var b in arr)
            {
                ROM[startIndex] = b;
                startIndex += 1;
            }

            Writer.Write(ROM);

            Writer.Flush();
            Writer.Close();

            Debug.WriteLine("done encoding");
        }

        public void Encode(string StrToEncode)
        {
            BinaryWriter  Writer = new BinaryWriter(new FileStream("results.bin", FileMode.Create));
            List<byte> arr = new List<byte>();

            bool was_flag = false;

            for (int i = 0; i < StrToEncode.Length; i++)
            {
                if (StrToEncode[i] == '\r')
                {
                    var tmp = Convert.FromHexString("F3FF");
                    foreach (var t in tmp) { arr.Add(t); }
                }
                if (StrToEncode[i] == '{')
                {
                    var tmp1 = Convert.FromHexString(Char.ToString(StrToEncode[i + 6]) + Char.ToString(StrToEncode[i + 7]) + Char.ToString(StrToEncode[i + 1]) + Char.ToString(StrToEncode[i + 2]));
                    foreach (var t in tmp1) { 
                        arr.Add(t); 
                        //Debug.Write(t);
                    }
                    was_flag = true;
                    i += 8;
                }
                else { 
                    if (inv_correspondances.ContainsKey(Char.ToString(StrToEncode[i])))
                    {
                        var cor = inv_correspondances[Char.ToString(StrToEncode[i])];
                        var tmp = Convert.FromHexString(cor);
                        foreach (var t in tmp) { arr.Add(t); }
                        was_flag = false;
                    }
                }
            }


            Writer.Write(arr.ToArray());

            Writer.Flush();
            Writer.Close();

            Debug.WriteLine("done encoding");
        }

        public string Decode(byte[] ArrayROM)
        {
            int f_hexIN, s_hexIN;
            string results = "";
            String f_res, s_res;
            try
            {
                for (int i = 0; i < ArrayROM.Length; i++) {
                    f_hexIN = ArrayROM[i];
                    i++;
                    s_hexIN = ArrayROM[i];
                    f_res = string.Format("{0:X2}", f_hexIN);
                    s_res = string.Format("{0:X2}", s_hexIN);
                    if (correspondances.ContainsKey(s_res + f_res))
                    {
                        results += correspondances[s_res + f_res];
                    }
                    else
                    {
                        results += $"{{{s_res}}} {{{f_res}}}";
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                Debug.WriteLine("Done decoding file");
            }
            return results;
        }

        public string Decode(string path) {
            // TODO: rewrite to handle saving straight inside the ROM
            stream = new FileStream(path, FileMode.Open);
            int f_hexIN = stream.ReadByte();
            int s_hexIN = stream.ReadByte();
            string results = "";
            String f_res, s_res;
            try
            {
                while (f_hexIN != -1 && s_hexIN != -1)
                {
                    f_res = string.Format("{0:X2}", f_hexIN);
                    s_res = string.Format("{0:X2}", s_hexIN);
                    if (correspondances.ContainsKey(s_res + f_res)) {
                        results += correspondances[s_res + f_res];
                    }
                    else
                    {
                        results += $"{{{s_res}}} {{{f_res}}}";
                    }

                    f_hexIN = stream.ReadByte();
                    s_hexIN = stream.ReadByte();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                Debug.WriteLine("Done decoding file");
            }
            stream.Close();
            return results;
        }

        public void GetCorrespondances()
        {
            // https://stackoverflow.com/a/1212115
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\correspondences.json");
            using FileStream openStream = File.OpenRead(fileName);
            correspondances = JsonSerializer.Deserialize<Dictionary<string, string>>(openStream);
        }

        public void GetInvCorrespondances()
        {
            // https://stackoverflow.com/a/1212115
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\inv_correspondance.json");
            using FileStream openStream = File.OpenRead(fileName);
            inv_correspondances = JsonSerializer.Deserialize<Dictionary<string, string>>(openStream);
        }

        public List<DSRomLoader.FATentry> setList(string path) {
            byte[] ROM = DSRomLoader.Loader.LoadRom(path);

            List<string> fileNames =  DSRomLoader.Loader.GetFNT(ROM);
            List<DSRomLoader.FATentry> entries = DSRomLoader.Loader.GetFAT(ROM);

            return entries;
        }

        public List<DSRomLoader.FATentry> setList(byte[] ROM)
        {
            List<string> fileNames = DSRomLoader.Loader.GetFNT(ROM);
            List<DSRomLoader.FATentry> entries = DSRomLoader.Loader.GetFAT(ROM);

            return entries;
        }
    }
}
