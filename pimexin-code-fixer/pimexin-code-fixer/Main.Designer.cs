namespace pimexin_code_fixer
{
    partial class Main
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
            this.btn_browse = new System.Windows.Forms.Button();
            this.txt_from_path = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_to_path = new System.Windows.Forms.TextBox();
            this.btn_browse2 = new System.Windows.Forms.Button();
            this.btn_settings = new System.Windows.Forms.Button();
            this.btn_convert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(394, 9);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(75, 23);
            this.btn_browse.TabIndex = 6;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // txt_from_path
            // 
            this.txt_from_path.Location = new System.Drawing.Point(58, 12);
            this.txt_from_path.Name = "txt_from_path";
            this.txt_from_path.Size = new System.Drawing.Size(330, 20);
            this.txt_from_path.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "From:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "To:";
            // 
            // txt_to_path
            // 
            this.txt_to_path.Location = new System.Drawing.Point(58, 46);
            this.txt_to_path.Name = "txt_to_path";
            this.txt_to_path.Size = new System.Drawing.Size(330, 20);
            this.txt_to_path.TabIndex = 10;
            // 
            // btn_browse2
            // 
            this.btn_browse2.Location = new System.Drawing.Point(394, 43);
            this.btn_browse2.Name = "btn_browse2";
            this.btn_browse2.Size = new System.Drawing.Size(75, 23);
            this.btn_browse2.TabIndex = 11;
            this.btn_browse2.Text = "Browse";
            this.btn_browse2.UseVisualStyleBackColor = true;
            this.btn_browse2.Click += new System.EventHandler(this.btn_browse2_Click);
            // 
            // btn_settings
            // 
            this.btn_settings.Location = new System.Drawing.Point(12, 95);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(75, 54);
            this.btn_settings.TabIndex = 12;
            this.btn_settings.Text = "Settings";
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // btn_convert
            // 
            this.btn_convert.Location = new System.Drawing.Point(394, 95);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(75, 54);
            this.btn_convert.TabIndex = 13;
            this.btn_convert.Text = "Convert";
            this.btn_convert.UseVisualStyleBackColor = true;
            this.btn_convert.Click += new System.EventHandler(this.btn_convert_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 158);
            this.Controls.Add(this.btn_convert);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.btn_browse2);
            this.Controls.Add(this.txt_to_path);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_from_path);
            this.Controls.Add(this.btn_browse);
            this.Name = "Main";
            this.Text = "Pimexin Code Fixer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox txt_from_path;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_to_path;
        private System.Windows.Forms.Button btn_browse2;
        private System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.Button btn_convert;
    }
}

