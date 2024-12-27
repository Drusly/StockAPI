namespace Karluna.Win
{
    partial class MainUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label1 = new Label();
            richTextBox2 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            statusStrip1 = new StatusStrip();
            textBox1 = new TextBox();
            tabPage2 = new TabPage();
            button1 = new Button();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(1, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(854, 465);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.Control;
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(richTextBox2);
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Controls.Add(textBox3);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(statusStrip1);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(846, 437);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Publisher";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 294);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 6;
            label1.Text = "Message";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(6, 312);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(831, 67);
            richTextBox2.TabIndex = 5;
            richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(7, 93);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(830, 193);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(7, 64);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Topic";
            textBox3.Size = new Size(187, 23);
            textBox3.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(7, 35);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Client ID";
            textBox2.Size = new Size(187, 23);
            textBox2.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(245, 69, 69);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(3, 412);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(840, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(7, 6);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "mqtt://localhost:14444";
            textBox1.Size = new Size(187, 23);
            textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(846, 437);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Subscriber";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(762, 385);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 7;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(66, 17);
            toolStripStatusLabel1.Text = "Disconnect";
            // 
            // MainUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(854, 469);
            Controls.Add(tabControl1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainUI";
            ShowIcon = false;
            Text = "Client";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private StatusStrip statusStrip1;
        private TextBox textBox1;
        private TabPage tabPage2;
        private Label label1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button button1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}