using System.ComponentModel;

namespace Stegosaurus.Dispatcher.Debug;

partial class Debug
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        listBox1 = new System.Windows.Forms.ListBox();
        richTextBox1 = new System.Windows.Forms.RichTextBox();
        textBox1 = new System.Windows.Forms.TextBox();
        SuspendLayout();
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 15;
        listBox1.Location = new System.Drawing.Point(511, 111);
        listBox1.Name = "listBox1";
        listBox1.Size = new System.Drawing.Size(120, 94);
        listBox1.TabIndex = 0;
        listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
        // 
        // richTextBox1
        // 
        richTextBox1.Location = new System.Drawing.Point(488, 288);
        richTextBox1.Name = "richTextBox1";
        richTextBox1.Size = new System.Drawing.Size(100, 96);
        richTextBox1.TabIndex = 1;
        richTextBox1.Text = "";
        richTextBox1.TextChanged += richTextBox1_TextChanged;
        // 
        // textBox1
        // 
        textBox1.Location = new System.Drawing.Point(279, 203);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(178, 23);
        textBox1.TabIndex = 2;
        // 
        // Debug
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(textBox1);
        Controls.Add(richTextBox1);
        Controls.Add(listBox1);
        Text = "Debug";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.TextBox textBox1;

    private System.Windows.Forms.RichTextBox richTextBox1;

    private System.Windows.Forms.ListBox listBox1;

    #endregion
}