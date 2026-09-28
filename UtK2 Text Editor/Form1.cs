using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Reflection.Emit;

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
            //var item = listBox1.SelectedIndex;

            //currentElem = entries[item];

            //var offset = currentElem.offset;
            //var size = currentElem.size;

            //int offset1 = (int)offset;
            //int total = (int)(offset + size);

            //var decoded = dc.Decode(ROMfile[offset1..total]);

            //displayContent.Text = decoded;
            //modifyText.Text = decoded;
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
            //string tmp = modifyText.Text;
            //Console.WriteLine(currentDir);
            string file = System.IO.Path.Combine(currentDir, "rom.nds");
            ////Encode(string StrToEncode, byte[] ROM, int startIndex)
            //dc.Encode(modifyText.Text, ROMfile, currentElem.offset, currentElem.size);

            ROMfile = DSRomLoader.Loader.LoadRom("rom.nds");

            var offset = currentElem.offset;
            var size = currentElem.size;

            int offset1 = (int)offset;
            int total = (int)(offset + size);
            var tmp1 = dc.Decode(ROMfile[offset1..total]);
            int h = 50;
            foreach(KeyValuePair<String, List<string>> item in tmp1)
            {
                TextBox tmp_txt = new TextBox();
                tmp_txt.Text = item.Key.ToString();
                tmp_txt.Location = new Point(300, h);
                this.Controls.Add(tmp_txt);

                TextBox tmp_txt2 = new TextBox();
                tmp_txt2.Text = string.Join("", item.Value);
                tmp_txt2.Location = new Point(500, h);
                tmp_txt2.Width = 500;
                tmp_txt2.Height = 100;
                //tmp_txt2.WordWrap = true;
                tmp_txt2.AutoSize = true;
                tmp_txt2.Multiline = true;
                tmp_txt2.ScrollBars = ScrollBars.Vertical;
                this.Controls.Add(tmp_txt2);

                h += 100;
            }

            //TextBox txt = new TextBox();
            //txt.Text = "helloo";
            //txt.Location = new Point(300, 50);
            //this.Controls.Add(txt);
            //displayContent.Text
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

            //var decoded = dc.Decode(ROMfile[offset1..total]);

            //displayContent.Text = decoded;
            //modifyText.Text = decoded;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var item = listBox1.SelectedIndex;

            //currentElem = entries[item];

            //var offset = currentElem.offset;
            //var size = currentElem.size;

            //int offset1 = (int)offset;
            //int total = (int)(offset + size);

            //var decoded = dc.Decode(ROMfile[offset1..total]);

            //CSVDumper.writeToFile(currentElem.name, decoded);
        }
    }
}
