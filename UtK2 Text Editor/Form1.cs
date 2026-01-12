using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;

namespace UtK2_Text_Editor
{
    public partial class Form1 : Form
    {
        //FileDecoder dc = new FileDecoder("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\017E");
        FileHandler dc = new FileHandler();
        FileLoading fileLoading = new FileLoading();
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        List<DSRomLoader.FATentry> entries;
        DSRomLoader.FATentry currentElem;
        byte[] ROMfile;
        public Form1(byte[] ROM)
        {
            InitializeComponent();
            ROMfile = ROM;
            //FileDecoder dc = new FileDecoder("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\017E");
        }

        protected override void OnLoad(EventArgs e)
        {
            //string tmp = dc.Decode("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\016F");
            //displayContent.Text = tmp;
            //modifyText.Text = tmp;

            //base.OnLoad(e);
            listBox1.BeginUpdate();
            entries = dc.setList(ROMfile);

            foreach (var item in entries)
            {
                listBox1.Items.Add(item.name.ToString());
                //Debug.WriteLine(item.name.ToString());
            }

            listBox1.EndUpdate();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            var item = listBox1.SelectedIndex;

            currentElem = entries[item];

            var offset = currentElem.offset;
            var size = currentElem.size;

            int offset1 = (int)offset;
            int total = (int)(offset + size);

            var decoded = dc.Decode(ROMfile[offset1..total]);

            displayContent.Text = decoded;
            modifyText.Text = decoded;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //displayContent.Text = dc.Decode("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\016F");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //displayContent.Text = "Your text to put in textbox";
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //string tmp = modifyText.Text;
            //dc.Encode(tmp);
            ////displayContent.Text = dc.Decode("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\bin\\Debug\\net9.0-windows\\results.bin");
            //displayContent.Text = modifyText.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tmp = modifyText.Text;
            Console.WriteLine(currentDir);
            string file = System.IO.Path.Combine(currentDir, "rom.nds");
            //Encode(string StrToEncode, byte[] ROM, int startIndex)
            dc.Encode(modifyText.Text, ROMfile, currentElem.offset, currentElem.size);

            ROMfile = DSRomLoader.Loader.LoadRom("rom.nds");

            var offset = currentElem.offset;
            var size = currentElem.size;

            int offset1 = (int)offset;
            int total = (int)(offset + size);
            displayContent.Text = dc.Decode(ROMfile[offset1..total]);
            //displayContent.Text = modifyText.Text;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listBox1.SelectedIndex;

            currentElem = entries[item];

            var offset = currentElem.offset;
            var size = currentElem.size;

            int offset1 = (int)offset;
            int total = (int)(offset + size);

            var decoded = dc.Decode(ROMfile[offset1..total]);

            displayContent.Text = decoded;
            modifyText.Text = decoded;
        }
    }
}
