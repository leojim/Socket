namespace cline
{
    partial class Man
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
            this.messagelist = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.cilneMesage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messagelist
            // 
            this.messagelist.Location = new System.Drawing.Point(12, 12);
            this.messagelist.Name = "messagelist";
            this.messagelist.Size = new System.Drawing.Size(563, 179);
            this.messagelist.TabIndex = 0;
            this.messagelist.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(596, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 244);
            this.listBox1.TabIndex = 1;
            // 
            // cilneMesage
            // 
            this.cilneMesage.Location = new System.Drawing.Point(12, 206);
            this.cilneMesage.Name = "cilneMesage";
            this.cilneMesage.Size = new System.Drawing.Size(563, 21);
            this.cilneMesage.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(562, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Man
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cilneMesage);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.messagelist);
            this.Name = "Man";
            this.Text = "Man";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Man_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox messagelist;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox cilneMesage;
        private System.Windows.Forms.Button button1;
    }
}