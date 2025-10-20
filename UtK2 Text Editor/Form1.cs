using System.Net.Mime;

namespace UtK2_Text_Editor
{
    public partial class Form1 : Form
    {
        //FileDecoder dc = new FileDecoder("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\017E");
        FileDecoder dc = new FileDecoder();
        FileLoading fileLoading = new FileLoading();
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            //FileDecoder dc = new FileDecoder("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\017E");
        }

        protected override void OnLoad(EventArgs e)
        {
            //string tmp = dc.Decode("C:\\Users\\elarb\\Desktop\\decomp\\utk2\\UtK2 Text Editor\\UtK2 Text Editor\\content\\016F");
            //displayContent.Text = tmp;
            //modifyText.Text = tmp;

            //base.OnLoad(e);
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
            string file = System.IO.Path.Combine(currentDir, "results.bin");
            dc.Encode(modifyText.Text);
            displayContent.Text = dc.Decode(Path.GetFullPath(file));
            //displayContent.Text = modifyText.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var path = fileLoading.LoadFile();

            var decoded = dc.Decode(path);

            displayContent.Text = decoded;
            modifyText.Text = decoded;
        }

    }
}
