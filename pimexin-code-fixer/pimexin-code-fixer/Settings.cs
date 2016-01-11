using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace pimexin_code_fixer
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btn_htmlConfig_Click(object sender, EventArgs e)
        {
            /*
             * Open a dialog for selecting file.
             * Set selected path to txt_from_path TextView 
             * as text.
             */
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text file |*.txt";
            openFileDialog1.Title = "Select a config file for html.";
            
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txt_htmlConfig.Text = openFileDialog1.FileName;
            }
        }

        private void btn_cssConfig_Click(object sender, EventArgs e)
        {
            /*
             * Open a dialog for selecting file.
             * Set selected path to txt_from_path TextView 
             * as text.
             */

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text file |*.txt";
            openFileDialog1.Title = "Select a config file for css.";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txt_cssConfig.Text = openFileDialog1.FileName;
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txt_cssConfig.Text = Ayarlar.Default.CssConfigPath;
            txt_htmlConfig.Text = Ayarlar.Default.HmlConfigPath;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Ayarlar.Default.HmlConfigPath = txt_htmlConfig.Text;
            Ayarlar.Default.CssConfigPath = txt_cssConfig.Text;
            Ayarlar.Default.Save();
            this.Dispose();
        }

       

       
    }
}
