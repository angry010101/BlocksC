using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace WindowsFormsApplication2
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        const int xbase = 0, ybase = 0, shift = 100, rectwidth=100,rectheight=50;
        int x1 = 10, y1 = 10, lineverticallength = 10, offsetx=500;
        int yold1;
        Graphics gPanel;
        Pen p = new Pen(Color.Blue, 3);
        String s, s1;
        int currentfor = 0,countfor = 0,currentwhile = 0, countwhile=0,countif=0,currentif=0;
        bool inif = false;
        int iftruenow = 0;

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        Rectangle rect1, rect;
        Font font1;
        StringFormat stringFormat;

        Point[] points;


        private void button1_Click(object sender, EventArgs e)
        {
            initialization();
            drawbegin();
            blockanalyze();
            drawend();
            destruction();
            /*      Graphics gPanel = panel1.CreateGraphics();
                  Pen p = new Pen(Color.Red, 3);
                  gPanel.DrawLine(p, new Point(0, 0), new Point(50, 50));*/
        }

        private void destruction()
        {
            gPanel.Dispose();
            currentfor = 0;
            countfor = 0;
            countif = 0;
            currentif = 0;
        }
        private void drawVertical(int x1,int y1,int length,bool direction=true,bool inif=false) 
        {
            gPanel.DrawLine(p, new Point(x1+rectwidth/2,y1), new Point(x1 + rectwidth / 2, y1+length));
            if (!inif)
            {
                if (direction)
                {
                    drawVerticalarrow(x1, y1, length);
                }
                else
                {
                    drawVerticalarrow(x1, y1, length, false);
                }
            }
        }
        public void drawVerticalarrow(int x1,int y1,int length,bool direction=true)
        {
            const int widtharrow = 10;
            if (length > 20)
            {
                if (direction)
                {
                    gPanel.DrawLine(p, new Point(x1 + rectwidth / 2, y1 + length / 2), new Point(x1 + rectwidth / 2 - widtharrow, y1 + length / 2 - widtharrow));
                    gPanel.DrawLine(p, new Point(x1 + rectwidth / 2, y1 + length / 2), new Point(x1 + rectwidth / 2 + widtharrow, y1 + length / 2 - widtharrow));
                }
                else
                {
                    gPanel.DrawLine(p, new Point(x1 + rectwidth / 2, y1 + length / 2), new Point(x1 + rectwidth / 2 - widtharrow, y1 + length / 2 + widtharrow));
                    gPanel.DrawLine(p, new Point(x1 + rectwidth / 2, y1 + length / 2), new Point(x1 + rectwidth / 2 + widtharrow, y1 + length / 2 + widtharrow));
                }
            }
        }
        public void drawHorizontalif(int x1,int y1, int length)
        {
            gPanel.DrawLine(p, new Point(x1, y1 + rectheight / 2), new Point(x1 - length, y1 + rectheight / 2));
            gPanel.DrawLine(p, new Point(x1 + rectwidth, y1 + rectheight / 2), new Point(x1 + length + rectwidth, y1 + rectheight / 2));
        }
        public void drawHorizontal(int x1, int y1, int length)
        {
            gPanel.DrawLine(p, new Point(x1, y1 + rectheight / 2), new Point(x1 - length, y1 + rectheight / 2));
        }
        private void handlein()
        {
            s1 = s.Substring(s.IndexOf(">>") + 3);
            s1 = s1.Substring(0, s1.IndexOf(';'));
            points = new Point[]{
                new Point(x1,y1),new Point(x1-15,y1+rectheight),
                new Point(x1-15,y1+rectheight),new Point(x1+rectwidth,y1+rectheight),
                new Point(x1+rectwidth,y1+rectheight),new Point(x1+rectwidth+15,y1)
            };
            rect1.Y = y1;
            gPanel.DrawPolygon(p, points);
            gPanel.DrawString(s1, font1, Brushes.Blue, new Rectangle(x1, y1, rectwidth, rectheight), stringFormat);
            moveY(rectheight);
            drawVertical(x1, y1, lineverticallength);
            moveY(lineverticallength);
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            panel1.Refresh();
            initialization(panel1.AutoScrollPosition.X,panel1.AutoScrollPosition.Y);
            drawbegin();
            blockanalyze();
            drawend();
            destruction();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void handleout()
        {
            s1 = s.Substring(s.IndexOf("<<") + 3);
            s1 = s1.Substring(0, s1.IndexOf(';'));
            points = new Point[]{
                new Point(x1,y1),new Point(x1-15,y1+rectheight),
                new Point(x1-15,y1+rectheight),new Point(x1+rectwidth,y1+rectheight),
                new Point(x1+rectwidth,y1+rectheight),new Point(x1+rectwidth+15,y1)
            };
            rect1.Y = y1;
            gPanel.DrawPolygon(p,points);
            gPanel.DrawString(s1, font1, Brushes.Blue, new Rectangle(x1, y1, rectwidth, rectheight), stringFormat);
            moveY(rectheight);
            drawVertical(x1, y1, lineverticallength);
            moveY(lineverticallength);
        }
        private void moveY(int y)
        {
            this.y1 += y;
        }
        private int handleif(int index)
        {
            int retind = 0;

        /*    switch (iftruenow)
            {
                case 1:
                    {
                        x1 -= calculateoffset(index)-1 * rectwidth;
                        break;
                    }
                case -1:
                    {
                        x1 += calculateoffset(index)-1 * rectwidth;
                        break;
                    }
            }
            iftruenow = 1;*/
            s1 = s.Substring(s.IndexOf("(") + 1);
            s1 = s1.Substring(0, s1.IndexOf(')'));
           // x1 -= Math.Abs(currentif - countif) * rectwidth * 3;
            gPanel.DrawPolygon(p, new Point[] {
                new Point(x1 + rectwidth/2, y1),
                new Point(x1 + rectwidth, y1 + rectheight/2),
                new Point(x1 + rectwidth, y1 + rectheight/2),
                new Point(x1 + rectwidth/2, y1 + rectheight),
                new Point(x1 + rectwidth/2, y1 + rectheight),
                new Point(x1, y1 + rectheight/2) });
            gPanel.DrawString(s1, font1, Brushes.Blue, new Rectangle(x1, y1, rectwidth, rectheight), stringFormat);
            drawHorizontalif(x1,y1,rectwidth+rectwidth*calculateoffset(index));

            gPanel.DrawLine(p, new Point(x1 - rectwidth - rectwidth * calculateoffset(index), y1 + rectheight/2), new Point(x1 - rectwidth - rectwidth * calculateoffset(index), y1 + rectheight + rectheight / 2));
            gPanel.DrawLine(p, new Point(x1 + 2*rectwidth + rectwidth * calculateoffset(index), y1 + rectheight/2), new Point(x1 + 2*rectwidth + rectwidth * calculateoffset(index), y1 + rectheight + rectheight / 2));
            moveY(rectheight+ rectheight/2);
            x1 -= rectwidth * calculateoffset(index);
            retind = starthandleiftrue(index);
            gPanel.DrawLine(p, new Point(x1-rectwidth-1 - rectwidth * calculateoffset(index), y1), new Point(2+ x1 + rectwidth*2 + rectwidth * calculateoffset(index), y1)); ////////
            drawVertical(x1, y1, lineverticallength,false,true);
            moveY(lineverticallength);
            iftruenow = 0;
            return retind;
        }
        public int starthandleiftrue(int index)
        {
            int yold = y1,ifremembertrueend;
         //   yold1 = y1;
            x1 -= rectwidth + rectwidth / 2;
            int retint = blockanalyze(index);
            x1 += rectwidth + rectwidth / 2;
            if (textBox1.Lines[retint + 1].IndexOf("else") == -1)
            {
                int dec = y1 - yold;
                drawVertical(x1 + rectwidth + rectwidth / 2 + 2*rectwidth * calculateoffset(index), yold, y1 - yold,false,true);
                moveY(y1 - yold);
                moveY(-dec);
            }
            ifremembertrueend = y1;
            x1 += rectwidth * calculateoffset(index);
            if (textBox1.Lines[retint + 1].IndexOf("else") != -1)
            {
                iftruenow = -1;
                x1 += rectwidth * calculateoffset(index);
                retint = starthandleiffalse(retint + 2,yold);
                x1 -= rectwidth * calculateoffset(index);
                if (ifremembertrueend > y1)
                {
                    drawVertical(x1 + rectwidth + rectwidth / 2 + rectwidth * calculateoffset(index), y1, ifremembertrueend - y1, false,true);
                    moveY(ifremembertrueend - y1);
                }
                else
                {
                    if (ifremembertrueend < y1)
                    {
                        drawVertical(x1 - rectwidth - rectwidth / 2 - rectwidth * calculateoffset(index), ifremembertrueend, y1 - ifremembertrueend, false,true);
                        // moveY(-rectheight-lineverticallength);
                        //  moveY(y1 - ifremembertrueend);
                    }
                }
            }
            iftruenow = 0;
            return retint;
        }
        public int starthandleiffalse(int index,int yold1)
        {
            y1 = yold1;
            x1 += rectwidth + rectwidth / 2;
            int retint = blockanalyze(index);
            x1 -= rectwidth + rectwidth / 2;
            return retint;
        }
        public int blockanalyze(int line=0)
        {
            int countbrackets = 0;
            for (int i = line; i < textBox1.Lines.Length; i++)
            {
                s = textBox1.Lines[i];
                if (s.IndexOf("//") != -1)
                {
                    s = s.Substring(0, s.IndexOf("//"));
                }
                if (s.IndexOf("{") != -1)
                {
                    countbrackets++;
                }
                if (s.IndexOf("}") != -1)
                {
                    countbrackets--;
                }
                if (countbrackets == 0)
                {
                    return i;
                }
            /*    if (ifenable)
                {
                    if (s.IndexOf("}") != -1)
                    {
                        ifenable = false;
                        x1 += 150;
                    }
                    if (i + 1 < textBox1.Lines.Length)
                    {
                        if (textBox1.Lines[i + 1].IndexOf("else") == -1)
                        {
                            //  gPanel.DrawLine(p, new Point(x1 + 200, ify1), new Point(x1 + 200, y1 + 10));
                            //   gPanel.DrawLine(p, new Point(x1 - 100, y1), new Point(x1 + 202, y1));
                        }
                    }
                }
                if (elseenable)
                {
                    if (s.IndexOf("}") != -1)
                    {
                        elseenable = false;
                        x1 -= 150;
                        gPanel.DrawLine(p, new Point(x1 - 100, y1), new Point(x1 + 202, y1));
                        y1 += 10;
                        gPanel.DrawLine(p, new Point(x1 + 50, y1 - 10), new Point(x1 + 50, y1));
                    }

                }*/
                if (s.IndexOf("<<") != -1)
                {
                    handleout();
                }
                if (s.IndexOf(">>") != -1)
                {
                    handlein();
                }
                if (s.IndexOf("if") != -1)
                {
                    if (countif==0)
                    {
                        currentif = calculateif(i);
                        countif = calculateif(i);
                    }
                    i = handleif(i+1);
                    currentif--;
                }
                if (s.IndexOf("else") != -1)
                {
                }
                if (s.IndexOf("for") != -1 || s.IndexOf("while") != -1) // for and while,dowhile are the same
                {
                    if (currentfor == 0)
                    {
                        currentfor = calculate("for", i);
                        countfor = calculate("for", i);
                        currentfor += calculate("while", i);
                        countfor += calculate("while", i);
                    }
                    i = handlefor(i);
                    currentfor--;
                }
            }
            return 0;
        }
        public int handlefor(int index)
        {
            int yold = y1, xold =x1;
            s1 = s.Substring(s.IndexOf("("));
            s1 = s1.Substring(0, s1.IndexOf(")")+1);
            if (s.IndexOf("for") != -1)
            {
                gPanel.DrawPolygon(p, new Point[] {
                new Point(x1, y1),
                new Point(x1 + rectwidth, y1),
                new Point(x1 + rectwidth + rectwidth/3, y1+rectheight/2),
                new Point(x1 + rectwidth, y1+rectheight),
                new Point(x1, y1 + rectheight),
                new Point(x1 - rectwidth/3, y1 + rectheight/2),
                new Point(x1, y1)
            });
            }
            if (s.IndexOf("while") != -1)
            {
                gPanel.DrawPolygon(p, new Point[] {
                new Point(x1 + rectwidth/2, y1),
                new Point(x1 + rectwidth + rectwidth/2 - 5, y1+rectheight/2),
                new Point(x1 + rectwidth/2, y1+rectheight),
                new Point(x1 - rectwidth/2 + 5, y1 + rectheight/2)
            });
            }
            gPanel.DrawString(s1, font1, Brushes.Blue, new Rectangle(x1, y1, rectwidth, rectheight), stringFormat);
            moveY(rectheight);
            drawVertical(x1, y1, lineverticallength);
            moveY(lineverticallength);
            index = blockanalyze(index + 1);
            drawVertical(x1, y1, lineverticallength);
            moveY(lineverticallength);
            drawVertical(x1 - rectwidth - rectwidth/2 - Math.Abs(currentfor-countfor)*rectwidth, yold+rectheight/2, y1 - yold - rectheight/2,false);
            drawHorizontal(x1+rectwidth/2+2, y1-lineverticallength*2-lineverticallength/2, rectwidth+rectwidth/2+3 + Math.Abs(currentfor - countfor) * rectwidth);
            drawHorizontal(xold-rectwidth/3, yold, rectwidth-rectwidth/3 + Math.Abs(currentfor - countfor) * rectwidth);
            drawHorizontal(xold + rectwidth  + rectwidth + Math.Abs(currentfor - countfor) * rectwidth, yold, rectwidth - rectwidth / 3 + Math.Abs(currentfor - countfor) * rectwidth);
            drawVertical(x1 + rectwidth + rectwidth/2-1 + Math.Abs(currentfor - countfor) * rectwidth, yold + rectheight / 2, y1 - yold);
            drawHorizontal(x1 + rectwidth*2 + Math.Abs(currentfor - countfor) * rectwidth, y1 , rectwidth + rectwidth / 2 + 1 + Math.Abs(currentfor - countfor) * rectwidth);
            moveY(rectheight / 2);
            drawVertical(x1, y1,rectheight / 2);
            moveY(rectheight / 2);
            return index;
        }

        public int calculateif(int i)
        {
            int count = 1, countbrackets = 0,temp=0;
            do
            {
                s1 = textBox1.Lines[i];
                if (s1.IndexOf("if") != -1)
                {
                    count += calculateif(i+1);
                    return count;
                }
                if (textBox1.Lines[i + 1].IndexOf("{") != -1)
                {
                    countbrackets++;
                }
                if (textBox1.Lines[i + 1].IndexOf("}") != -1)
                {
                    countbrackets--;
                }
                i++;
            } while (countbrackets != 0);
            return count;
        }
        public int calculateoffset(int i)
        {
            int count = 0, countbrackets = 0;
            do
            {
                s1 = textBox1.Lines[i];
                if (s1.IndexOf("for") != -1 || s1.IndexOf("while") != -1 || s1.IndexOf("if") != -1)
                {
                    count++;
                }
                if (textBox1.Lines[i].IndexOf("{") != -1)
                {
                    countbrackets++;
                }
                if (textBox1.Lines[i].IndexOf("}") != -1)
                {
                    countbrackets--;
                }
                i++;
            } while (countbrackets != 0);
            return count;
        }
        public int calculate(String s,int i)
        {
            String s1;
            int count = 0,countbrackets=0;
            do
            {
                s1 = textBox1.Lines[i];
                if (s1.IndexOf(s) != -1)
                {
                    count++;
                }
                if (textBox1.Lines[i+1].IndexOf("{") != -1)
                {
                    countbrackets++;
                }
                if (textBox1.Lines[i + 1].IndexOf("}") != -1)
                {
                    countbrackets--;
                }
                i++;
            } while (countbrackets != 0);
            return count;
        }
        public void initialization(int x2 = 200,int y2=10)
        {
            gPanel = panel1.CreateGraphics();
            gPanel.Clip = new Region(new RectangleF(0, 0, 9999, 9999));
            x1 = x2;
            x1 += 2*rectwidth+calculateoffset(0)*rectwidth;
            y1 = y2;
            x1 += offsetx;
            currentfor = 0;
            gPanel.Clear(Color.White);
            font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            rect = new Rectangle(x1, y1, rectwidth, rectheight);
            rect1 = new Rectangle(x1, y1, rectwidth, rectheight);
            stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }
        public void drawbegin()
        {
            gPanel.DrawString("початок", font1, Brushes.Blue, rect, stringFormat);
            gPanel.DrawEllipse(p, rect);
            moveY(rectheight);
            drawVertical(x1, y1, lineverticallength);
            moveY(lineverticallength);
        }
        public void drawend()
        {
            rect.Y = y1;
            gPanel.DrawEllipse(p, rect);
            gPanel.DrawString("кінець", font1, Brushes.Blue, rect, stringFormat);
        }
    }
}


/* TEST
1:  int main(){
cout << "fsfsf";
cin >> x;
if (x>0) 
{
cin >> y;
}
else
{
cout << d;
}
}


2:    int main(){
for (int i=0;i<3;i++)
{
cout << "fsdfsdf";
for (int i=0;i<3;i++)
{
cout << "fsdfsdf";
}
}
}

3:
int main(){
cout << "fsfsf";
cin >> x;
if (x>0) 
{
cout << "fsfs";
if (x>0) 
{
cout << "fsfs";
}
}
}

   4:
   int main(){
cout << "fsfsf";
cin >> x;
if (x>0) 
{
cout << "fsfs";
if (x>0) 
{
cout << "fsfs";
if (x>0) 
{
cout << "fsfs";
}
}
}
} 

5:    int main(){
cout << "fsfsf";
cin >> x;
if (x>0) 
{
cout << "fsfs";
if (x>0) 
{
cout << "fsfs";
while(x>0)
{
cin >> x;
while(x>0)
{
cin >> x;
}
}
}
else 
{
while(x>0)
{
cin >> x;
}
}
}
}

    */
