namespace pimexin_code_fixer
{
    partial class Settings
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_htmlConfig = new System.Windows.Forms.TextBox();
            this.txt_cssConfig = new System.Windows.Forms.TextBox();
            this.btn_htmlConfig = new System.Windows.Forms.Button();
            this.btn_cssConfig = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "CSS configuration File:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "HTML Configuration File:";
            // 
            // txt_htmlConfig
            // 
            this.txt_htmlConfig.Enabled = false;
            this.txt_htmlConfig.Location = new System.Drawing.Point(157, 11);
            this.txt_htmlConfig.Name = "txt_htmlConfig";
            this.txt_htmlConfig.Size = new System.Drawing.Size(248, 20);
            this.txt_htmlConfig.TabIndex = 16;
      
            // 
            // txt_cssConfig
            // 
            this.txt_cssConfig.Enabled = false;
            this.txt_cssConfig.Location = new System.Drawing.Point(157, 45);
            this.txt_cssConfig.Name = "txt_cssConfig";
            this.txt_cssConfig.Size = new System.Drawing.Size(248, 20);
            this.txt_cssConfig.TabIndex = 16;
            // 
            // btn_htmlConfig
            // 
            this.btn_htmlConfig.Location = new System.Drawing.Point(411, 9);
            this.btn_htmlConfig.Name = "btn_htmlConfig";
            this.btn_htmlConfig.Size = new System.Drawing.Size(75, 23);
            this.btn_htmlConfig.TabIndex = 17;
            this.btn_htmlConfig.Text = "Browse";
            this.btn_htmlConfig.UseVisualStyleBackColor = true;
            this.btn_htmlConfig.Click += new System.EventHandler(this.btn_htmlConfig_Click);
            // 
            // btn_cssConfig
            // 
            this.btn_cssConfig.Location = new System.Drawing.Point(411, 41);
            this.btn_cssConfig.Name = "btn_cssConfig";
            this.btn_cssConfig.Size = new System.Drawing.Size(75, 23);
            this.btn_cssConfig.TabIndex = 17;
            this.btn_cssConfig.Text = "Browse";
            this.btn_cssConfig.UseVisualStyleBackColor = true;
            this.btn_cssConfig.Click += new System.EventHandler(this.btn_cssConfig_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(301, 71);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 18;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(382, 71);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(104, 23);
            this.btn_save.TabIndex = 19;
            this.btn_save.Text = "Save and Close";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 108);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_cssConfig);
            this.Controls.Add(this.btn_htmlConfig);
            this.Controls.Add(this.txt_cssConfig);
            this.Controls.Add(this.txt_htmlConfig);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_htmlConfig;
        private System.Windows.Forms.TextBox txt_cssConfig;
        private System.Windows.Forms.Button btn_htmlConfig;
        private System.Windows.Forms.Button btn_cssConfig;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_save;
    }
}