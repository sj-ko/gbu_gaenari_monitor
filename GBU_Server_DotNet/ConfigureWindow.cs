using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBU_Server_Monitor
{
    public partial class ConfigureWindow : Form
    {
        public ConfigureWindow()
        {
            InitializeComponent();
        }

        public void Init()
        {
            MainForm form = (MainForm)this.Owner;
            Configure_textbox_savepath.Text = form.camera.savePath;
            Configure_textbox_serverPath.Text = form.camera.serverPath;
            Configure_textbox_configPath.Text = form.camera.configPath;
            Configure_combobox_nChannel.SelectedIndex = form.camera.nChannel - 1;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)this.Owner;
            form.camera.savePath = Configure_textbox_savepath.Text;
            form.camera.serverPath = Configure_textbox_serverPath.Text;
            form.camera.configPath = Configure_textbox_configPath.Text;
            form.camera.nChannel = Configure_combobox_nChannel.SelectedIndex + 1;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
