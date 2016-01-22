using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Security;
using gx;
using cm;
using System.Linq;

namespace GBU_Server_Monitor
{
    public partial class MainForm : Form
    {
        private System.Threading.Timer timer;
        private AutoResetEvent timerEvent;

        private FileSystemWatcher[] watcher = new FileSystemWatcher[20];
        private bool monitorStatus = false;

        public struct PLATE_FOUND
        {
            public int id;
            public int cam;
            public DateTime dateTime;
            public string plateStr;
            public Image snapshot;
        };

        private List<PLATE_FOUND> _plateList = new List<PLATE_FOUND>();
        private int _plateListIdx = 0;

        private Database dbManager = new Database();

        public MainForm()
        {
            InitializeComponent();
        }

        ~MainForm()
        {
            Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView_result.View = View.Details;
            listView_result.FullRowSelect = true;
            listView_result.GridLines = true;

            listView_result.Columns.Add("카메라", 50, HorizontalAlignment.Left);
            listView_result.Columns.Add("시간", 100, HorizontalAlignment.Left);
            listView_result.Columns.Add("차량번호", 100, HorizontalAlignment.Left);
        }

        private void Btn_Disconnect_Click(object sender, EventArgs e)
        {
            
        }

        private void Stop()
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite); // stop timer
                timer.Dispose();
                timer = null;
            }

            for (int i = 0; i < 20; i++)
            {
                if (watcher[i] != null)
                {
                    watcher[i].EnableRaisingEvents = false;
                    watcher[i].Dispose();
                    watcher[i] = null;
                }
            }

            //_plateListIdx = 0;
            //_plateList.Clear();
        }

        private void MediaTimerCallBack(Object obj)
        {
            // ??
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        private void OnChangedANPRPath(object source, FileSystemEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.FullPath + " changed");
            TextBox[] textBoxes = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20 };
            PictureBox[] pictureBoxes = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16, pictureBox17, pictureBox18, pictureBox19, pictureBox20 };

            string lastLine = null;

            //try
            //{
                lastLine = File.ReadLines(e.FullPath).Last();

                string[] logresults = lastLine.Split(',');
                this.BeginInvoke(new Action(() =>
                    {
                        pictureBoxes[Convert.ToInt32(logresults[0], 10)].ImageLocation = logresults[3];
                        textBoxes[Convert.ToInt32(logresults[0], 10)].Text = logresults[1];

                        DateTime time = new DateTime();
                        string[] itemStr = { logresults[0], logresults[2], logresults[1] };
                        ListViewItem item = new ListViewItem(itemStr);
                        listView_result.Items.Add(item);
                    }
                ));

            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine("OnChangedANPRPath() error : " + ex.ToString());
            //}

        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void button_PlayStop_Click(object sender, EventArgs e)
        {
            if (monitorStatus == true)
            {
                Stop();

                button_PlayStop.Text = "연결";

                monitorStatus = false;
            }
            else
            {
                timerEvent = new AutoResetEvent(true);
                timer = new System.Threading.Timer(MediaTimerCallBack, null, 100, 200);


                for (int i = 0; i < 20; i++)
                {
                    watcher[i] = new FileSystemWatcher();
                    string path = @"D:\anprtest\ch" + i;
                    if (Directory.Exists(path))
                    {
                        watcher[i].Path = path;
                        watcher[i].NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                        watcher[i].Filter = "anprresult.txt";
                        watcher[i].Changed += new FileSystemEventHandler(OnChangedANPRPath);
                        watcher[i].EnableRaisingEvents = true;
                    }
                }

                button_PlayStop.Text = "끊기";

                monitorStatus = true;
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Owner = this;
            searchWindow.Init();
            searchWindow.Show();
        }

        private void button_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GBU ANPR Monitor " + Application.ProductVersion + "\n" + "For Gaenari Gas Station"
                + "\n\n" + "(C) 2015 GBU Datalinks Co. Ltd.");
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Stop();
            Application.Exit();
        }

        private void button_SwitchMonitor_Click(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length > 1)
            {
                Screen myScreen = Screen.FromControl(this);
                Screen otherScreen = Screen.AllScreens.FirstOrDefault(s => !s.Equals(myScreen)) ?? myScreen;

                this.StartPosition = FormStartPosition.Manual;

                Point p = new Point();

                p.X = otherScreen.WorkingArea.Left;
                p.Y = otherScreen.WorkingArea.Top;

                this.Location = p;
            }

        }

    }
}
