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

namespace UtK2_Text_Editor
{
    internal class FileDecoder
    {
        private Dictionary<string, String> correspondances;
        private Dictionary<string, String> inv_correspondances;
        private FileStream stream;
        public FileDecoder()
        {
            GetCorrespondances();
            GetInvCorrespondances();
        }

        public void Encode(string StrToEncode)
        {
            BinaryWriter  Writer = new BinaryWriter(new FileStream("results.bin", FileMode.Truncate));
            List<byte> arr = new List<byte>();

            //foreach (var c in StrToEncode)
            //{
            //    //Debug.WriteLine(c);
            //    if (c == '\r')
            //    {
            //        var tmp = Convert.FromHexString("F3FF");
            //        foreach (var t in tmp) { arr.Add(t); }
            //    }
            //    if (inv_correspondances.ContainsKey(Char.ToString(c))){
            //        var cor = inv_correspondances[Char.ToString(c)];
            //        var tmp = Convert.FromHexString(cor);
            //        foreach (var t in tmp) { arr.Add(t); }
            //    }
            //    else
            //    {
            //        var tmp = Convert.FromHexString(c.ToString());
            //        foreach (var t in tmp) { arr.Add(t); }
            //    }
            //    //arr.Add(Convert.FromHexString("DC00"));
            //}

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
                    Debug.WriteLine($"str: {StrToEncode[i + 1]} {Char.ToString(StrToEncode[i + 2])} {StrToEncode[i + 3].ToString()} {StrToEncode[i + 6].ToString()} {StrToEncode[i + 7].ToString()}");
                    var tmp1 = Convert.FromHexString(Char.ToString(StrToEncode[i + 6]) + Char.ToString(StrToEncode[i + 7]) + Char.ToString(StrToEncode[i + 1]) + Char.ToString(StrToEncode[i + 2]));
                    foreach (var t in tmp1) { 
                        arr.Add(t); 
                        //Debug.Write(t);
                    }
                    Debug.Write(" {");
                    was_flag = true;
                    i += 8;
                }
                else { 
                    //if (StrToEncode[i] == ' ')
                    //{
                    //    if (StrToEncode[i + 1] == '{' && StrToEncode[i - 1] == '}')
                    //    {
                    //        Debug.WriteLine($"str: {StrToEncode[i + 1] == '{'} {Char.ToString(StrToEncode[i + 2])} {StrToEncode[i + 3].ToString()} {StrToEncode[i + 7].ToString()} {StrToEncode[i + 8].ToString()}");
                    //        var tmp1 = Convert.FromHexString(Char.ToString(StrToEncode[i + 7]) + Char.ToString(StrToEncode[i + 8]) + Char.ToString(StrToEncode[i + 2]) + Char.ToString(StrToEncode[i + 3]));
                    //        foreach (var t in tmp1) { arr.Add(t); }
                    //        was_flag = true;
                    //        i += 9;
                    //    }
                    //}
                    if (inv_correspondances.ContainsKey(Char.ToString(StrToEncode[i])))
                    {
                        var cor = inv_correspondances[Char.ToString(StrToEncode[i])];
                        var tmp = Convert.FromHexString(cor);
                        foreach (var t in tmp) { arr.Add(t); }
                        was_flag = false;
                        Debug.WriteLine(" corr");
                    }
                }
            }


            //arr = Convert.FromHexString("DC00");

            //Debug.Write(string.Format("{0:X2}", arr[0]));

            Writer.Write(arr.ToArray());
            //Debug.Write(string.Format("{0:X2}", "0xDC"));

            Writer.Flush();
            Writer.Close();

            Debug.WriteLine("done encoding");
        }

        public string Decode(string path) {
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
                    //Debug.WriteLine(f_res + s_res);
                    if (correspondances.ContainsKey(s_res + f_res)) {
                        results += correspondances[s_res + f_res];
                    }
                    else
                    {
                        results += $"{{{s_res}}} {{{f_res}}}";
                    }
                    //if (correspondances.ContainsKey(s_res)) Debug.Write(correspondances[s_res]);

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
            string fileName = "C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\correspondences.json";
            using FileStream openStream = File.OpenRead(fileName);
            correspondances = JsonSerializer.Deserialize<Dictionary<string, string>>(openStream);
        }

        public void GetInvCorrespondances()
        {
            // https://stackoverflow.com/a/1212115
            string fileName = "C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\inv_correspondance.json";
            using FileStream openStream = File.OpenRead(fileName);
            inv_correspondances = JsonSerializer.Deserialize<Dictionary<string, string>>(openStream);
        }
    }
}
