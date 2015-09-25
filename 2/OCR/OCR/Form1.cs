using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OCR
{
    public partial class Form1 : Form
    {

        bool ve=false , vee=true,me=true;
        bool fff = false;
        bool sin = true ,te=true;
        bool jim = true, pe = true,be=true;
        char[] st = new char[10];
        int miny=1500, maxy=0,miny2=1500,maxy2=0 ,maxx=0 ,maxx2=0;
        string str = "";
        int w1, w2;
        int a1, b1;
        int wer = -1;
        int[] werwer ={ 1,0, 1, 3, 2, 3, 1, 3, 0, 1, 0, 1, 0, 1, 3, 0, 3, 0, 1, 1, 2, 0, 1, 1, 2, 1, 2, 0, 0, 1, 0, 0, 0,0 };
        int[,] temp = { { 90, 90, 90, 90 }, { 90, 90, 90, 90 }, { -53, 0, 0, 38 }, { -53, 0, 0, 38 }, { -53, 0, 0, 38 }, { -53, 0, 0, 38 }, { 0, -31, 82, -6 }, { 0, -31, 82, -6 }, { 0, -31, 82, -6 }, { 0, -31, 82, -6 }, { 37, 38, -61, -46 }, { 37, 38, -61, -46 }, { 81, -63, -34, -15 }, { 81, -63, -34, -15 }, { 81, -63, -34, -15 }, { -27, -8, -65, 70 }, { -27, -8, -65, 70 }, { -27, -19, -58, 67 }, { -27, -19, -58, 67 }, { -46, 29, -21, 0 }, { -46, 29, -21, 0 }, { -47, 68, -68, 18 }, { -47, 68, -68, 18 }, { 30, 44, -9, 21 }, { 63, 43, -35, 63 }, { 80, -14, 0, 39 }, { 80, -14, 0, 39 }, { 82, 90, -20, 77 }, { 56, 35, -70, 90 }, { 83, -40, 35, -86 }, { 18, -3, -88, -28 }, { 41, -53, 33, -53 }, { -26, 52, 3, 90 }, { 0, 0, 0, 0 } };
        char[] ch = { 'آ','ا', 'ب', 'پ', 'ت', 'ث', 'ج', 'چ', 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'ژ', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', 'ف', 'ق', 'ک', 'گ', 'ل', 'م', 'ن', 'و', 'ه', 'ی','-' };
        bool flag = true;
        int a, b;
        int nfs = 0;
        int harekat = 0;
        double[] zav = new double[4];
        int[] sw = new int[1000];
        int medix = 0, mediy = 0, medix2 = 0, mediy2 = 0, sum = 0, medix1 = 0, mediy1 = 0, medix3 = 0, mediy3 = 0;
        Graphics g;
        int[] mx = { 1, 1, 1, 0, -1, -1, -1, 0 };
        int[] my = { -1, 0, 1, 1, 1, 0, -1, -1 };
        int[] mz = { 1, 2, 3, 4, 5, 6, 7, 8 };

        bool dimiplimiu = true;
        int[,] ff = new int[2, 1000];
        bool dp = true;
        int xx, yy;
        public Form1()
        {
            InitializeComponent();
        }

       
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        
           //اگر کلید موس زده شده است :
            if (flag == false)
            {
                fff = true;
                if (harekat > 20)
                {
                    if (e.X <= xx + 10 && e.X >= xx - 10 && e.Y <= yy + 10 && e.Y >= yy - 10)
                    {
                        //آیا شکل خودش را قطع می کند ؟
                        ve = true; 
                      
                    }
                }
                harekat++;
                //نقاط مینیمم و ماکسیمم شکل اصلی را بدست می آورد  (برای تشخیص هم شکل ها از روی نقطه (
                if (dp == true)
                {
                    if (e.Y < miny)
                        miny = e.Y;
                    else
                        maxy = e.Y;
                    if (e.X > maxx)
                        maxx = e.X;
                }
                else
                {
                    if (e.Y < miny2)
                        miny2 = e.Y;
                    else
                        maxy2 = e.Y;
                    if (e.X > maxx2)
                        maxx2 = e.X;
                }
                g = pictureBox1.CreateGraphics();
                g.FillEllipse(new SolidBrush(Color.White), e.X, e.Y, 5, 5);
                //  g.DrawLine(new Pen(Color.Red), a, b, e.X, e.Y);
                tashkhis(e.X, e.Y);
                a = e.X;
                b = e.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = true;
            dimiplimiu = true;
            //اگر قسمت اصلی شکل بود :
            if (dp == true)
            {
                //تا چند ثانیه منتظر نقطه و سرکش می ماند !
                timer1.Enabled=true;
                nfs = 0;
                g = pictureBox1.CreateGraphics();
                //بدست آوردن وسط مسافت طی شده !
                //  medix2 = e.X;
                //   mediy2 = e.Y;

                for (int d = 0; d < sum / 4; d++)
                {
                    medix += ff[0, d];
                    mediy += ff[1, d];
                }
                for (int d = sum / 4; d < sum / 2; d++)
                {
                    medix1 += ff[0, d];
                    mediy1 += ff[1, d];
                }
                for (int d = sum / 2; d < 3 * sum / 4; d++)
                {
                    medix2 += ff[0, d];
                    mediy2 += ff[1, d];
                }
                for (int d = 3 * sum / 4; d < sum; d++)
                {
                    medix3 += ff[0, d];
                    mediy3 += ff[1, d];
                }

                /*g.DrawLine(new Pen(Color.Black), xx, yy, xx + medix, yy + mediy);
                g.DrawLine(new Pen(Color.Black), xx + medix, yy + mediy, xx + medix + medix1, yy + mediy + mediy1);
                g.DrawLine(new Pen(Color.Black), xx + medix + medix1, yy + mediy + mediy1, xx + medix + medix1 + medix2 , yy + mediy + mediy1 + mediy2);
                g.DrawLine(new Pen(Color.Black), xx + medix + medix1 + medix2, yy + mediy + mediy1 + mediy2, xx + medix + medix1 + medix2 + medix3, yy + mediy + mediy1 + mediy2 + mediy3); */

                zav[0] = Convert.ToDouble(Math.Atan((double)mediy / (double)medix) * (180.0 / 3.14));
                zav[1] = Convert.ToDouble(Math.Atan((double)mediy1 / (double)medix1) * (180.0 / 3.14));
                zav[2] = Convert.ToDouble(Math.Atan((double)mediy2 / (double)medix2) * (180.0 / 3.14));
                zav[3] = Convert.ToDouble(Math.Atan((double)mediy3 / (double)medix3) * (180.0 / 3.14));
               // MessageBox.Show(zav[0].ToString()+ " " +zav[1].ToString() + "  " + zav[2].ToString() + "  " + zav[3].ToString());
                bool[] dev = new bool[4];
         
                for (int m = 0; m < 4; m++)
                    dev[m] = false;
                //قسمت تشخیص حروف 
                for (int k = 0; k < 34; k++)
                {
                    for (int gg = 0; gg < 4; gg++)
                    {
                        if (zav[gg] + 30 > 90 || zav[gg] - 30 < -90)
                        {
                            if (zav[gg] + 30 > 90)
                            {
                                if (temp[k, gg] < -60)
                                {
                                    if (temp[k, gg] + 180 >= zav[gg] - 30 && temp[k, gg] + 180 <= zav[gg] + 30)
                                    {
                                        dev[gg] = true;
                                    }
                                    else
                                    {
                                        dev[gg] = false;
                                    }
                                }
                                else
                                {
                                    if (temp[k, gg] >= zav[gg] - 30 && temp[k, gg] <= zav[gg] + 30)
                                    {
                                        dev[gg] = true;
                                    }
                                    else
                                        dev[gg] = false;
                                }
                            }
                            if (zav[gg] - 30 < -90)
                            {
                                if (temp[k, gg] < 0)
                                {
                                    if (temp[k, gg] >= zav[gg] - 30 && temp[k, gg] <= zav[gg] + 30)
                                    {
                                        dev[gg] = true;
                                    }
                                    else
                                        dev[gg] = false;
                                }
                                else
                                {
                                    if (temp[k, gg] - 180 >= zav[gg] - 30 && temp[k, gg] - 180 <= zav[gg] + 30)
                                    {
                                        dev[gg] = true;
                                    }
                                    else
                                        dev[gg] = false;
                                }
                            }
                        }
                        else
                        {
                            if (temp[k, gg] >= zav[gg] - 30 && temp[k, gg] <= zav[gg] + 30)
                            {
                                dev[gg] = true;
                            }
                            else
                                dev[gg] = false;
                        }


                    }

                    bool devel = true;
               
                    for (int r = 0; r < 4; r++)
                    {
                        if (dev[r] == false)
                            devel = false;
                    }
                    if (devel == true)
                    {
                            str += ch[k];
                         //   MessageBox.Show(str);
                           
                    }
                    for (int t = 0; t < 4; t++)
                        dev[t] = false;
                    dp = false;
                }
              

            }
     

            // MessageBox.Show(zav[0].ToString() +"  " + zav[1].ToString()+"   "+ zav[2].ToString()+"    "+zav[3].ToString() );
        }
        public void tashkhis(int tx, int ty)
        {

            w1 = tx - a1;
            w2 = ty - b1;
            ff[0, sum] = w1;
            ff[1, sum] = w2;
            a1 = tx;
            b1 = ty;
            sum++;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            wer++;//تعداد قسمت های هر شکل را حساب می کند ---< 
            if (dimiplimiu == true)
            {
                dimiplimiu = false;//برای ذخیره ی اولین مختصات ----<
                xx = e.X;
                yy = e.Y;
                //  medix = e.X;
                //  mediy = e.Y;
            }
            flag = false;
            a = e.X;
            b = e.Y;
            a1 = e.X;
            b1 = e.Y;
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nfs++;
            //قسمت رسیدگی به استثنا ها و در آخر چاپ حرف  
            if(nfs>6)
            {
                if (fff == true)
                {
                    st = str.ToCharArray();
                    for (int k = 0; k < str.Length; k++)
                    {
                        if ((st[k] == 'خ' || st[k] == 'ج') && jim == true && wer == 1)
                        {
                            jim = false;
                            if (miny > miny2)
                                textBox1.Text += "خ";
                            else
                                textBox1.Text += "ج";

                        }
                        else if ((st[k] == 'ل' || st[k] == 'م') && wer == 0 && me == true)
                        {
                            me = false;
                            if (ve == true)
                                textBox1.Text += "م";
                            else
                                textBox1.Text += "ل";
                        }
                        else if ((st[k] == 'و' || st[k] == 'ح') && wer == 0 && vee == true)
                        {
                            vee = false;
                            if (ve == true)
                                textBox1.Text += "و";
                            else
                                textBox1.Text += "ح";

                        }
                        else if ((st[k] == 'ث' || st[k] == 'پ') && pe == true && wer == 3)
                        {
                            pe = false;
                            if (miny > miny2)
                                textBox1.Text += "ث";
                            if (miny < miny2)
                                textBox1.Text += "پ";

                        }
                        else if ((st[k] == 'ک' || st[k] == 'ب') && be == true && wer == 1)
                        {
                            be = false;
                            if (maxy > miny2)
                                textBox1.Text += "ک";
                            if (maxy < miny2)
                                textBox1.Text += "ب";
                        }
                        else if ((st[k] == 'س' || st[k] == 'ص') && wer == 0 && sin == true)
                        {
                            sin = false;
                            if (ve == true)
                                textBox1.Text += "ص";
                            else
                                textBox1.Text += "س";
                        }
                        else if ((st[k] == 'گ' || st[k] == 'ت') && wer == 2 && te == true)
                        {
                            te = false;
                            if (maxx < maxx2)
                                textBox1.Text += "گ";
                            else
                                textBox1.Text += "ت";
                        }
                        else
                        {
                            for (int m = 0; m < 34; m++)
                            {
                                if (ch[m] == st[k] && m != 2 && m != 3 && m != 5 && m != 25 && m != 6 && m != 9 && m != 8 && m != 30 && m != 28 && m != 27 && m != 15 && m != 17 && m != 4 && m != 26)
                                {


                                    if (werwer[m] == wer)
                                    {

                                      //اینجا چاپ می شود 
                                        textBox1.Text += ch[m].ToString();



                                    }
                                }
                            }
                        }

                    }
                    //آماده کردن متغیر ها برای حرف بعدی 
                    jim = true;
                    vee = true;
                    pe = true;
                    sin = true;
                    ve = false;
                    harekat = 0;
                    me = true;
                    be = true;
                    maxy = 0; maxy2 = 0; miny = 1500; miny2 = 1500;
                    str = "";
                    wer = -1;
                    flag = true;
                    medix = 0; mediy = 0; medix2 = 0; mediy2 = 0; sum = 0; medix1 = 0; mediy1 = 0; medix3 = 0; mediy3 = 0;
                    dimiplimiu = true;
                    dp = true;
                    timer1.Enabled = false;
                }
                else
                {
                    textBox1.Text += " ";
                    timer1.Enabled = false;
                }
            fff = false;


            for (int o = 0; o < sum; o++)
            {
                ff[0,o] = 0;
                ff[1, o] = 0;
            }
            sum = 0;

            }
       
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



    }
}