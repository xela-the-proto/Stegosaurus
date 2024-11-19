namespace Stegosaurus.Dispatcher;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        btn_connect_exchange_windows = new System.Windows.Forms.Button();
        btn_connect_exchange_linux = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // btn_connect_exchange_windows
        // 
        btn_connect_exchange_windows.Location = new System.Drawing.Point(116, 184);
        btn_connect_exchange_windows.Name = "btn_connect_exchange_windows";
        btn_connect_exchange_windows.Size = new System.Drawing.Size(126, 25);
        btn_connect_exchange_windows.TabIndex = 0;
        btn_connect_exchange_windows.Text = "Connect widnows";
        btn_connect_exchange_windows.UseVisualStyleBackColor = true;
        btn_connect_exchange_windows.Click += btn_connect_exchange_windows_Click;
        // 
        // btn_connect_exchange_linux
        // 
        btn_connect_exchange_linux.Location = new System.Drawing.Point(603, 186);
        btn_connect_exchange_linux.Name = "btn_connect_exchange_linux";
        btn_connect_exchange_linux.Size = new System.Drawing.Size(133, 23);
        btn_connect_exchange_linux.TabIndex = 1;
        btn_connect_exchange_linux.Text = "Connect linux";
        btn_connect_exchange_linux.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(btn_connect_exchange_linux);
        Controls.Add(btn_connect_exchange_windows);
        Text = "Form1";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button btn_connect_exchange_windows;

    private System.Windows.Forms.Button btn_connect_exchange_linux;

    #endregion
}