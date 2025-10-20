namespace UtK2_Text_Editor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            displayContent = new TextBox();
            modifyText = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // displayContent
            // 
            displayContent.AcceptsReturn = true;
            displayContent.AcceptsTab = true;
            displayContent.Location = new Point(413, 37);
            displayContent.Multiline = true;
            displayContent.Name = "displayContent";
            displayContent.ReadOnly = true;
            displayContent.Size = new Size(363, 401);
            displayContent.TabIndex = 0;
            displayContent.WordWrap = false;
            displayContent.TextChanged += textBox1_TextChanged_1;
            // 
            // modifyText
            // 
            modifyText.AcceptsReturn = true;
            modifyText.AcceptsTab = true;
            modifyText.Location = new Point(12, 37);
            modifyText.Multiline = true;
            modifyText.Name = "modifyText";
            modifyText.Size = new Size(341, 401);
            modifyText.TabIndex = 1;
            modifyText.WordWrap = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 2;
            label1.Text = "Modifiable field";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(413, 9);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 3;
            label2.Text = "Conversion results";
            // 
            // button1
            // 
            button1.Location = new Point(278, 8);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "convert";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(165, 8);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Load File";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(modifyText);
            Controls.Add(displayContent);
            Name = "Form1";
            Text = "UtK2 Text Editor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox displayContent;
        private TextBox modifyText;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
    }
}
