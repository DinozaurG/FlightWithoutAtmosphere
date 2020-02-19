using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightWithoutAtmosphere
{
    public partial class Form1 : Form
    {
        decimal t = 0, x0 = 0, y0, v0, cosA, sinA;

        

        const decimal dt = 0.01M, g = 9.81M;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (t == 0)
            {
                chart1.Series[0].Points.Clear(); //А оно ТУТ надо вообще?
                y0 = inputHeight.Value;
                v0 = inputSpeed.Value;
                double alpha = (double)inputAngle.Value * Math.PI / 180;
                cosA = (decimal)Math.Cos(alpha);
                sinA = (decimal)Math.Sin(alpha);
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = (double)(v0 * v0 * (decimal)Math.Sin(2*alpha) / g);
                chart1.ChartAreas[0].AxisY.Maximum = (double)(v0 * v0 * sinA * sinA / (2*g));
                chart1.Series[0].Points.AddXY(x0, y0);
                timer.Start();
            }
            else
            {
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t += dt;
            decimal x = x0 + v0 * cosA * t;
            decimal y = y0 + v0 * sinA * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);
            timeShow.Text = ("Seconds: " + t);
            if (y <= 0)
            {
                t = 0;
                timer.Stop();
            }
        }

        private void breakButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            t = 0;
            timeShow.Text = ("Seconds: " + t);
            chart1.Series[0].Points.Clear();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}
