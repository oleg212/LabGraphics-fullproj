﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace filters
{

    
    public partial class Form1 : Form
    {
        int[,] ker()
        {
            int size = trackBar1.Value;
            int[,] ker = new int[size, size];
            ker[0, 0] = Convert.ToInt16(!checkBox22.Checked) * 255;
            ker[1, 0] = Convert.ToInt16(!checkBox21.Checked) * 255;
            ker[0, 1] = Convert.ToInt16(!checkBox24.Checked) * 255;
            ker[1, 1] = Convert.ToInt16(!checkBox23.Checked) * 255;
            if (size > 2)
            {
                ker[0, 2] = Convert.ToInt16(!checkBox30.Checked) * 255;
                ker[1, 2] = Convert.ToInt16(!checkBox29.Checked) * 255;
                ker[2, 2] = Convert.ToInt16(!checkBox26.Checked) * 255;
                ker[2, 1] = Convert.ToInt16(!checkBox20.Checked) * 255;
                ker[2, 0] = Convert.ToInt16(!checkBox18.Checked) * 255;
            }
            if (size > 3)
            {
                ker[0, 3] = Convert.ToInt16(!checkBox32.Checked) * 255;
                ker[1, 3] = Convert.ToInt16(!checkBox31.Checked) * 255;
                ker[2, 3] = Convert.ToInt16(!checkBox28.Checked) * 255;
                ker[3, 3] = Convert.ToInt16(!checkBox27.Checked) * 255;
                ker[3, 2] = Convert.ToInt16(!checkBox25.Checked) * 255;
                ker[3, 1] = Convert.ToInt16(!checkBox19.Checked) * 255;
                ker[3, 0] = Convert.ToInt16(!checkBox17.Checked) * 255;
            }
            if (size > 4)
            {
                ker[0, 4] = Convert.ToInt16(!checkBox14.Checked) * 255;
                ker[1, 4] = Convert.ToInt16(!checkBox13.Checked) * 255;
                ker[2, 4] = Convert.ToInt16(!checkBox10.Checked) * 255;
                ker[3, 4] = Convert.ToInt16(!checkBox9.Checked) * 255;
                ker[4, 4] = Convert.ToInt16(!checkBox54.Checked) * 255;
                ker[4, 3] = Convert.ToInt16(!checkBox48.Checked) * 255;
                ker[4, 2] = Convert.ToInt16(!checkBox46.Checked) * 255;
                ker[4, 0] = Convert.ToInt16(!checkBox40.Checked) * 255;
                ker[4, 0] = Convert.ToInt16(!checkBox38.Checked) * 255;
            }
            return ker;
        }
        Bitmap image;

        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All files (*.*) | *.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);

                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All files (*.*) | *.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                pictureBox1.Image.Save(filename);
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImg = ((filters)e.Argument).process(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImg;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new InversionFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new SepyFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void нормальныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new GaussFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new WaveFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new ReflectorFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void растяжениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new LinStretchFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new BinaryFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new ErosionFilter(ker(), trackBar1.Value);
            backgroundWorker1.RunWorkerAsync(filter);


        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new DilationFilter(ker(), trackBar1.Value);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        { 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Visible = false;
            
            checkBox9.Visible = false;
            checkBox10.Visible = false;
            
            
            checkBox13.Visible = false;
            checkBox14.Visible = false;
            
            checkBox17.Visible = false;
            checkBox18.Visible = false;
            checkBox19.Visible = false;
            checkBox20.Visible = false;
            checkBox21.Visible = false;
            checkBox22.Visible = false;
            checkBox23.Visible = false;
            checkBox24.Visible = false;
            checkBox25.Visible = false;
            checkBox26.Visible = false;
            checkBox27.Visible = false;
            checkBox28.Visible = false;
            checkBox29.Visible = false;
            checkBox30.Visible = false;
            checkBox31.Visible = false;
            checkBox32.Visible = false;
            
            checkBox38.Visible = false;
           
            checkBox40.Visible = false;
            
            checkBox46.Visible = false;
            
            checkBox48.Visible = false;
            
            checkBox54.Visible = false;
            
            button2.Visible = false;
            button3.Visible = false;




        }

        private void setMorphologyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackBar1.Visible = true;
            checkBox9.Visible = true;
            checkBox10.Visible = true;
            checkBox13.Visible = true;
            checkBox14.Visible = true;
            checkBox17.Visible = true;
            checkBox18.Visible = true;
            checkBox19.Visible = true;
            checkBox20.Visible = true;
            checkBox21.Visible = true;
            checkBox22.Visible = true;
            checkBox23.Visible = true;
            checkBox24.Visible = true;
            checkBox25.Visible = true;
            checkBox26.Visible = true;
            checkBox27.Visible = true;
            checkBox28.Visible = true;
            checkBox29.Visible = true;
            checkBox30.Visible = true;
            checkBox31.Visible = true;
            checkBox32.Visible = true;
            checkBox38.Visible = true;
            checkBox40.Visible = true;
            checkBox46.Visible = true;
            checkBox48.Visible = true;
            checkBox54.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
            
        }

        private void топологияToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void dilationfunc(object sender, EventArgs e)
        {
            filters filter = new DilationFilter(ker(), 0);
        }
        private async void someMethod()
        {
            await Task.Delay(8000);
        }
        private async void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            filters filter = new ErosionFilter(ker(), trackBar1.Value);
            backgroundWorker1.RunWorkerAsync(filter);
            
            BackgroundWorker worker2 = new BackgroundWorker();
            await Task.Delay(8000);
            filter = new DilationFilter(ker(), trackBar1.Value);
            someMethod();
            worker2.RunWorkerAsync(filter);

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
 
        }

        private async void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filters filter = new DilationFilter(ker(), trackBar1.Value);
            backgroundWorker1.RunWorkerAsync(filter);
            
            BackgroundWorker worker2 = new BackgroundWorker();
            await Task.Delay(8000);
            filter = new ErosionFilter(ker(), trackBar1.Value);
            worker2.RunWorkerAsync(filter);
        }

        private async void whiteHatToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            filters filter = new ErosionFilter(ker(), trackBar1.Value);
            backgroundWorker1.RunWorkerAsync(filter);

            BackgroundWorker worker2 = new BackgroundWorker();
            await Task.Delay(8000);
            filter = new DilationFilter(ker(), trackBar1.Value);
            someMethod();
            worker2.RunWorkerAsync(filter);
            await Task.Delay(8000);
        }
    }

    public abstract class filters
    {

        protected abstract Color MakeNewColor(Bitmap Img, int x, int y);
        public int clamp(int val, int min, int max)
        {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }

        public Bitmap process(Bitmap sourceImg, BackgroundWorker worker)
        {
            Bitmap resultImg = new Bitmap(sourceImg.Width, sourceImg.Height);

            for (int i = 0; i < sourceImg.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImg.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImg.Height; j++)
                {
                    resultImg.SetPixel(i, j, MakeNewColor(sourceImg, i, j));
                }
            }
            return resultImg;
        }
    }

    public class InversionFilter : filters
    {
        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            Color sourceColor = Img.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }
    }
    public class GlassFilter : filters
    {
        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            Random rnd = new Random();
            double r1 = rnd.NextDouble()*1;
            double r2 = rnd.NextDouble()*1;
            int x1 = (int)(x + (r1 - 0.5) * 10);
            int y1 = (int)(y+(r2-0.5)*10);

            x1 = x+rnd.Next(-5,5);
            y1=x+rnd.Next(-5,5);

            Color resultColor = Img.GetPixel(clamp(x1,0,Img.Width-1),clamp(y1, 0, Img.Height - 1));
            return resultColor;
        }
    }

    public class SepyFilter : filters
    {
        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            Color sourceColor = Img.GetPixel(x, y);
            int k = 16;
            char Intensity = (char)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);
            Color resultColor = Color.FromArgb(clamp(Intensity + 2 * k, 0, 255), clamp(Intensity + (int)(0.5 * k), 0, 255), clamp(Intensity - 1 * k, 0, 255));
            return resultColor;
        }
    }

    public class MedianFilter : filters
    {
        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            int[] arrR = new int[9];
            int[] arrG = new int[9];
            int[] arrB = new int[9];
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    arrR[(i + 1) * 3 + j + 1] = Img.GetPixel(clamp(x + i, 0, Img.Width - 1), clamp(y + j, 0, Img.Height - 1)).R;
                    arrG[(i + 1) * 3 + j + 1] = Img.GetPixel(clamp(x + i, 0, Img.Width - 1), clamp(y + j, 0, Img.Height - 1)).G;
                    arrB[(i + 1) * 3 + j + 1] = Img.GetPixel(clamp(x + i, 0, Img.Width - 1), clamp(y + j, 0, Img.Height - 1)).B;
                }
            int len = arrR.Length;
            int t;
            for (int i = 1; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (arrR[j] > arrR[j + 1])
                    {
                        t = arrR[j];
                        arrR[j] = arrR[j + 1];
                        arrR[j + 1] = t;
                    }
                    if (arrG[j] > arrG[j + 1])
                    {
                        t = arrR[j];
                        arrG[j] = arrG[j + 1];
                        arrG[j + 1] = t;
                    }
                    if (arrB[j] > arrB[j + 1])
                    {
                        t = arrB[j];
                        arrB[j] = arrB[j + 1];
                        arrB[j + 1] = t;
                    }
                }
            }

            return Color.FromArgb(arrR[len / 2], arrG[len / 2], arrB[len / 2]);
        }
    }

    public class MatrixFilter : filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() { }
        public MatrixFilter(float [,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            float R = 0;
            float G = 0;
            float B = 0;
            for (int i = -radiusY; i <= radiusY; i++)
                for (int j = -radiusX; j <= radiusX; j++)
                {
                    int idX = clamp(x + i, 0, Img.Width - 1);
                    int idY = clamp(y + j, 0, Img.Height - 1);
                    Color neighborColor = Img.GetPixel(idX, idY);
                    R += neighborColor.R * kernel[i + radiusX, j + radiusY];
                    G += neighborColor.G * kernel[i + radiusX, j + radiusY];
                    B += neighborColor.B * kernel[i + radiusX, j + radiusY];
                }
            return Color.FromArgb(clamp((int)R, 0, 255), clamp((int)G, 0, 255), clamp((int)B, 0, 255));
        }
    }

    public class BlurFilter: MatrixFilter
    {
        public BlurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
        }
    }

    public class GaussFilter : MatrixFilter
    {
        public void CreateGaussKernel(int rad, float sigma)
        {
            int size = 2 * rad + 1;
            kernel = new float[size, size];
            float norm = 0.0F;
            for (int i = -rad; i <= rad; i++)
                for (int j = -rad; j <=rad; j++)
                {
                    kernel[i + rad, j + rad] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));
                    norm += kernel[i + rad, j + rad];
                }
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    kernel[i, j] /= norm;
        }

        public GaussFilter()
        {
            CreateGaussKernel(3, 2);
        }
    }

    public class SharpnessFilter : MatrixFilter
    {
        public SharpnessFilter()
        {
            kernel = new float[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    kernel[i, j] = -1;
            kernel[1, 1] = 9;
        }
    }

    public class WaveFilter: filters
    {
        protected override Color MakeNewColor(Bitmap Img, int x, int y)
        {
            int k = x + (int)(20 * Math.Sin(2 * Math.PI * y / 60));
            int l = y;
            return Img.GetPixel(clamp(k, 0, Img.Width - 1), clamp(l, 0, Img.Height - 1));
        }
    }

}
