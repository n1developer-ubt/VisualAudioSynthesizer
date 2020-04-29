using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSAudioVisualization;

namespace VisualAudioSynthesizer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            AudioVisualization audioViz = new AudioVisualization();
            //audioViz.Size = new Size(500, 200);
            audioViz.Dock = DockStyle.Fill;
            audioViz.MouseMove += AudioVizOnMouseMove;
            audioViz.MouseDown += AudioVizOnMouseDown;
            audioViz.BorderStyle = BorderStyle.FixedSingle;
            audioViz.BackColor = Color.White;
            audioViz.ColorBase = Color.Red;
            audioViz.Mode = Mode.WasapiLoopbackCapture;
            audioViz.DeviceIndex = 0;
            audioViz.Location = new Point(0,0);
            this.panel1.Controls.Add(audioViz);
            audioViz.Start();
        }

        private void AudioVizOnMouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("mouse down");
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        public Point MouseDownLocation { get; set; }

        private void AudioVizOnMouseMove(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("mouse move");
            if (sender is Control c)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (c.Location.X == 0 && e.X < 0)
                        return;
                    if (c.Location.Y == 0 && e.Y < 0)
                        return;
                    if (c.Location.X < 0)
                    {
                        c.Location = new Point(0, c.Location.Y);
                        return;
                    }
                    if (c.Location.Y < 0)
                    {
                        c.Location = new Point(c.Location.X, 0);
                        return;
                    }
                    c.Left = e.X + c.Left - MouseDownLocation.X;
                    c.Top = e.Y + c.Top - MouseDownLocation.Y;
                }
            }
        }

        private void panel1_Move(object sender, EventArgs e)
        {
            return;
            if (sender is Control c)
            {
                if (c.Location.X < 0)
                    c.Location = new Point(0, c.Location.Y);
                if (c.Location.Y < 0)
                    c.Location = new Point(c.Location.X, 0);
                if (c.Location.X+c.Width > Width)
                    c.Location = new Point(Width - c.Width, c.Location.Y);
                if (c.Location.Y+c.Height > Height)
                    c.Location = new Point(c.Location.X,  Height- c.Height);
            }
        }
    }
}
