using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UtK2_Text_Editor
{
    internal class FileLoading
    {
        public FileLoading()
        {
        }

        public static String LoadRom()
        {
            var filePath = string.Empty;
            var fileContent = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) // this is the "error" that should be checked for loading purposes
                {
                    filePath = openFileDialog.FileName;

                    return filePath;
                }
            }

            return "";
        }
    }
}
