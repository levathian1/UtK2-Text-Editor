using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DSRomLoader;

namespace UtK2_Text_Editor
{
    internal class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            string filelocation = FileLoading.LoadRom();

            byte[] ROM = DSRomLoader.Loader.LoadRom(filelocation);
            Application.Run(new Form1(ROM));
        }

    }
}