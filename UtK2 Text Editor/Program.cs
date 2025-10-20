using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UtK2_Text_Editor
{
    internal class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.

        //Debug.WriteLine("Getting all files");
        //String[] files = Directory.GetFiles("C:\\Users\\elarb\\Desktop\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\");
        //foreach (String f in files)
        //{
        //    Debug.WriteLine(f);
        //}

        //Debug.WriteLine(LoadFile("C:\\Users\\elarb\\Desktop\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\017E"));
        //C: \Users\elarb\Desktop\decomp\utk2\UtK2 Text Editor\UtK2 Text Editor\bin\Debug\net9.0 - windows
            //FileDecoder dc = new FileDecoder("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\017E");
            //FileDecoder dc = new FileDecoder("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\bin\\Debug\\net9.0-windows\\results.bin");
            //string tmp = dc.Decode();
            //dc.Encode(tmp);
            //Debug.Write(dc.Decode());

            // Start GUI Components
            // TODO: Build GUI Section
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
        private static String LoadFile(String filename)
        {
            FileStream stream;
            stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader readBinary = new BinaryReader(stream);

            byte inB1, inB2;
            string outB1, outB2;

            while (readBinary.BaseStream.Position < readBinary.BaseStream.Length-1) { 
                inB1 = readBinary.ReadByte(); inB2 = readBinary.ReadByte();
                outB1 = Convert.ToString(String.Format("{0:X}", inB1));
                outB2 = Convert.ToString(String.Format("{0:X}", inB2));
                Debug.WriteLine($"{{ {outB2} {outB1} }}");
            }

            return "ok";
        }

    }
}