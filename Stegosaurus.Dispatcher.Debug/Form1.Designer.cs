namespace Stegosaurus.Dispatcher.Debug;

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
        btn_break_discovery = new System.Windows.Forms.Button();
        statusStrip1 = new System.Windows.Forms.StatusStrip();
        dbg_log = new System.Windows.Forms.RichTextBox();
        button1 = new System.Windows.Forms.Button();
        txt_id = new System.Windows.Forms.TextBox();
        SuspendLayout();
        // 
        // btn_connect_exchange_windows
        // 
        btn_connect_exchange_windows.Location = new System.Drawing.Point(12, 41);
        btn_connect_exchange_windows.Name = "btn_connect_exchange_windows";
        btn_connect_exchange_windows.Size = new System.Drawing.Size(126, 25);
        btn_connect_exchange_windows.TabIndex = 0;
        btn_connect_exchange_windows.Text = "Connect widnows";
        btn_connect_exchange_windows.UseVisualStyleBackColor = true;
        btn_connect_exchange_windows.Click += btn_connect_exchange_windows_Click;
        // 
        // btn_connect_exchange_linux
        // 
        btn_connect_exchange_linux.Location = new System.Drawing.Point(12, 130);
        btn_connect_exchange_linux.Name = "btn_connect_exchange_linux";
        btn_connect_exchange_linux.Size = new System.Drawing.Size(126, 23);
        btn_connect_exchange_linux.TabIndex = 1;
        btn_connect_exchange_linux.Text = "Connect linux";
        btn_connect_exchange_linux.UseVisualStyleBackColor = true;
        btn_connect_exchange_linux.Click += btn_connect_exchange_linux_Click;
        // 
        // btn_break_discovery
        // 
        btn_break_discovery.Location = new System.Drawing.Point(12, 72);
        btn_break_discovery.Name = "btn_break_discovery";
        btn_break_discovery.Size = new System.Drawing.Size(126, 23);
        btn_break_discovery.TabIndex = 2;
        btn_break_discovery.Text = "Kill discovery";
        btn_break_discovery.UseVisualStyleBackColor = true;
        btn_break_discovery.Click += btn_break_discovery_Click;
        // 
        // statusStrip1
        // 
        statusStrip1.Location = new System.Drawing.Point(0, 428);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new System.Drawing.Size(800, 22);
        statusStrip1.TabIndex = 3;
        statusStrip1.Text = "statusStrip1";
        // 
        // dbg_log
        // 
        dbg_log.Location = new System.Drawing.Point(355, 12);
        dbg_log.Name = "dbg_log";
        dbg_log.Size = new System.Drawing.Size(433, 406);
        dbg_log.TabIndex = 4;
        dbg_log.Text = "";
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(12, 101);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(126, 23);
        button1.TabIndex = 5;
        button1.Text = "Start discovery";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // txt_id
        // 
        txt_id.Location = new System.Drawing.Point(12, 12);
        txt_id.Name = "txt_id";
        txt_id.Size = new System.Drawing.Size(126, 23);
        txt_id.TabIndex = 6;
        txt_id.Text = "ID shard";
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(txt_id);
        Controls.Add(button1);
        Controls.Add(dbg_log);
        Controls.Add(statusStrip1);
        Controls.Add(btn_break_discovery);
        Controls.Add(btn_connect_exchange_linux);
        Controls.Add(btn_connect_exchange_windows);
        Text = "Form1";
        Load += Form1_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.TextBox txt_id;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.RichTextBox dbg_log;

    private System.Windows.Forms.StatusStrip statusStrip1;

    private System.Windows.Forms.Button btn_break_discovery;

    private System.Windows.Forms.Button btn_connect_exchange_windows;

    private System.Windows.Forms.Button btn_connect_exchange_linux;

    #endregion
}