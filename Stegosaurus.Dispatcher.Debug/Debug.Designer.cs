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
        dbg_log = new System.Windows.Forms.RichTextBox();
        txt_id = new System.Windows.Forms.TextBox();
        btn_break_discovery = new System.Windows.Forms.Button();
        btn_start_discovery = new System.Windows.Forms.Button();
        btn_connect_win = new System.Windows.Forms.Button();
        btn_connect_lin = new System.Windows.Forms.Button();
        btn_start = new System.Windows.Forms.Button();
        txt_container_id = new System.Windows.Forms.TextBox();
        SuspendLayout();
        // 
        // dbg_log
        // 
        dbg_log.Location = new System.Drawing.Point(451, 12);
        dbg_log.Name = "dbg_log";
        dbg_log.Size = new System.Drawing.Size(337, 426);
        dbg_log.TabIndex = 1;
        dbg_log.Text = "";
        // 
        // txt_id
        // 
        txt_id.Location = new System.Drawing.Point(12, 12);
        txt_id.Name = "txt_id";
        txt_id.Size = new System.Drawing.Size(123, 23);
        txt_id.TabIndex = 2;
        txt_id.Text = "ShardId";
        // 
        // btn_break_discovery
        // 
        btn_break_discovery.Location = new System.Drawing.Point(12, 41);
        btn_break_discovery.Name = "btn_break_discovery";
        btn_break_discovery.Size = new System.Drawing.Size(123, 23);
        btn_break_discovery.TabIndex = 3;
        btn_break_discovery.Text = "Break discovery";
        btn_break_discovery.UseVisualStyleBackColor = true;
        btn_break_discovery.Click += btn_break_discovery_Click_1;
        // 
        // btn_start_discovery
        // 
        btn_start_discovery.Location = new System.Drawing.Point(12, 70);
        btn_start_discovery.Name = "btn_start_discovery";
        btn_start_discovery.Size = new System.Drawing.Size(123, 23);
        btn_start_discovery.TabIndex = 4;
        btn_start_discovery.Text = "Start discovery";
        btn_start_discovery.UseVisualStyleBackColor = true;
        // 
        // btn_connect_win
        // 
        btn_connect_win.Location = new System.Drawing.Point(12, 99);
        btn_connect_win.Name = "btn_connect_win";
        btn_connect_win.Size = new System.Drawing.Size(123, 23);
        btn_connect_win.TabIndex = 5;
        btn_connect_win.Text = "Connect win";
        btn_connect_win.UseVisualStyleBackColor = true;
        btn_connect_win.Click += btn_connect_win_Click;
        // 
        // btn_connect_lin
        // 
        btn_connect_lin.Location = new System.Drawing.Point(12, 128);
        btn_connect_lin.Name = "btn_connect_lin";
        btn_connect_lin.Size = new System.Drawing.Size(123, 23);
        btn_connect_lin.TabIndex = 6;
        btn_connect_lin.Text = "Connect lin";
        btn_connect_lin.UseVisualStyleBackColor = true;
        btn_connect_lin.Click += btn_connect_lin_Click;
        // 
        // btn_start
        // 
        btn_start.Location = new System.Drawing.Point(12, 157);
        btn_start.Name = "btn_start";
        btn_start.Size = new System.Drawing.Size(123, 23);
        btn_start.TabIndex = 7;
        btn_start.Text = "Start container";
        btn_start.UseVisualStyleBackColor = true;
        // 
        // txt_container_id
        // 
        txt_container_id.Location = new System.Drawing.Point(141, 12);
        txt_container_id.Name = "txt_container_id";
        txt_container_id.Size = new System.Drawing.Size(100, 23);
        txt_container_id.TabIndex = 8;
        txt_container_id.Text = "ContainerId";
        // 
        // Debug
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(txt_container_id);
        Controls.Add(btn_start);
        Controls.Add(btn_connect_lin);
        Controls.Add(btn_connect_win);
        Controls.Add(btn_start_discovery);
        Controls.Add(btn_break_discovery);
        Controls.Add(txt_id);
        Controls.Add(dbg_log);
        Text = "Debug";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button btn_start;
    private System.Windows.Forms.TextBox txt_container_id;

    private System.Windows.Forms.Button btn_start_discovery;
    private System.Windows.Forms.Button btn_connect_win;
    private System.Windows.Forms.Button btn_connect_lin;

    private System.Windows.Forms.Button btn_break_discovery;

    private System.Windows.Forms.TextBox txt_id;

    private System.Windows.Forms.RichTextBox dbg_log;

    #endregion
}