using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;//导入命名空间
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Media;
using System.IO;
using Pluz;
namespace 俄罗斯方块
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool IsStrat = false;
        Graphics g;
        Brush BackGround = Brushes.Yellow;
        Brush TheEnd = Brushes.RoyalBlue;
        int[,] Back = new int[15, 10];
        Brush CellBrush;
        Cell f;
        //积分器
        int SumTotal;

        private void Form1_Load(object sender, EventArgs e)
        {
          
            this.KeyPreview = true;

            oldSpeed = this.timer1.Interval;
            //画列边界(第0，9列)
            for (int hang = 0; hang < Back.GetLength(0); hang++)
            {
                Back[hang, 0] = Back[hang, Back.GetLength(1) - 1] = 2;
            }
            //画行边界(第14行)
            for (int lie = 1; lie < Back.GetLength(1) - 1; lie++)
            {
                Back[Back.GetLength(0) - 1, lie] = 2;
            }
           
            f = RandomCell();
            FillBrushColor();
            if (IsStrat)
            {

            }
            else 
            {
                f.Initial= new int[4,4];
                timer1.Enabled = false;
            }
         


        }
        private void pnlMap_Paint(object sender, PaintEventArgs e)
        {
           
                DrawBack();

                DrawCell();
          
               
         
           

        }
        //画地图
        public void DrawBack()
        {
            g = this.pnlMap.CreateGraphics();
            for (int hang = 0; hang < Back.GetLength(0); hang++)
            {
                for (int lie = 0; lie < Back.GetLength(1); lie++)
                {
                    if (Back[hang, lie] == 0)
                        g.FillRectangle(BackGround, lie * 30, hang * 30, 28, 28);
                    else if (Back[hang, lie] == 1)
                        g.FillRectangle(CellBrush, lie * 30, hang * 30, 28, 28);
                    else if (Back[hang, lie] == 2)
                        g.FillRectangle(TheEnd, lie * 30, hang * 30, 28, 28);
                }
            }
        }
        public void FillBrushColor()
        {
            Random rd = new Random();
            int i = rd.Next(1, 7);
            switch (i)
            {
                case 1:
                    CellBrush = Brushes.Tomato;
                    break;
                case 2:
                    CellBrush = Brushes.Red;
                    break;
                case 3:
                    CellBrush = Brushes.Green;
                    break;
                case 4:
                    CellBrush = Brushes.Pink;
                    break;
                case 5:
                    CellBrush = Brushes.Blue;
                    break;
                case 6:
                    CellBrush = Brushes.Purple;
                    break;
            }
        }
        public Cell RandomCell()
        {
            Cell c = null;
            Random rd = new Random();
            int i = rd.Next(1, 8);
            switch (i)
            {
                case 1:
                    c = new L_LeftBox();
                    return c;

                case 2:
                    c = new L_RightBox();
                    return c;

                case 3:
                    c = new Pencil();
                    return c;

                case 4:
                    c = new SBox();
                    return c;

                case 5:
                    c = new ShanBox();
                    return c;

                case 6:
                    c = new TianBox();
                    return c;

                case 7:
                    c = new ZBox();
                    return c;

            }
            return c;
        }
        //画方块
        public void DrawCell()
        {
            for (int hang = 0; hang < 4; hang++)
            {
                for (int lie = 0; lie < 4; lie++)
                {
                    if (f.Initial[hang, lie] == 1)
                        g.FillRectangle(TheEnd, lie * 30 + f.ColIndex * 30, hang * 30 + f.RowIndex * 30, 28, 28);
                }
            }
        }

        //判断是否能够移动
        public bool IsReplace()
        {
            //4*4
            for (int hang = 0; hang < 4; hang++)
            {
                for (int lie = 0; lie < 4; lie++)
                {
                    //方块的值为1，对应地图上的位置不为0则不能移动
                    if (f.Initial[hang, lie] == 1 && Back[hang + f.RowIndex, lie + f.ColIndex] != 0)
                        return false;//不能移动
                }
            }
            return true;//可以移动
        }

        //冻结
        public void NextOne()
        {
            //1.将方块值为1，对应地图的位置的值改为1
            for (int hang = 0; hang < 4; hang++)
            {
                for (int lie = 0; lie < 4; lie++)
                {
                    if (f.Initial[hang, lie] == 1)
                    {
                        Back[hang + f.RowIndex, lie + f.ColIndex] = 1;
                    }
                }
            }
            //2.产生新方块

            f = RandomCell();
        }


        public static SoundPlayer sound = new SoundPlayer();

        /*播放声音*/
        public static void PlaySound(string soundstr)
        {
            switch (soundstr)
            {
                case "FinishOneLine": //消除一行的声音
                    if (!File.Exists("FinishOneLine.wav")) return;
                    sound.SoundLocation = "FinishOneLine.wav";
                    break;
                case "Over": //当无法操作时
                    if (!File.Exists("CanNotDo.wav")) return;
                    sound.SoundLocation = "CanNotDo.wav";
                    break;
            }
            sound.Play();
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string getPree = e.KeyChar.ToString().ToLower();
            XmlDocument xml = new XmlDocument();
            xml.Load("Config.xml");
            XmlNode root = xml.DocumentElement;
            foreach (XmlNode roo in root.ChildNodes)
            {
                if (roo.InnerText == getPree)
                {
                    if (roo.Name == "change")
                    {

                        int[,] old = f.Initial;
                        f.Change();
                        if (!IsReplace())
                        {
                            f.Initial = old;


                        }



                    }
                    else if (roo.Name == "down")
                    {

                        if (IsLoser())
                        {
                            f.Down();
                            if (!IsReplace())
                            {

                                f.RowIndex--;
                                NextOne();
                                ClearLine();

                            }
                        }
                        else 
                        {
                            DialogResult dr= MessageBox.Show("您失败了");
                            PlaySound("Over");
                            ///
                            ///
                            if (dr == DialogResult.OK) 
                            {
                               
                                Application.Exit();
                             
                            }
                        }
                       


                    }
                    else if (roo.Name == "left")
                    {
                        f.Left();
                        if (!IsReplace())
                        {

                            f.ColIndex++;

                        }

                    }
                    else if (roo.Name == "right")
                    {
                        f.Right();
                        if (!IsReplace())
                        {

                            f.ColIndex--;

                        }

                    }else if(roo.Name=="shoot")
                    {
                        while(true)
                        {
                            f.Down();
                            if (!IsReplace())
                            {

                                f.RowIndex--;
                                NextOne();
                                ClearLine();
                                break;
                            }

                        }
                    }
                    DrawBack();
                    DrawCell();

                }
            }
        }
        int sumTotal=5;
        public void ClearLine()
        {

            for (int rows = 0; rows < Back.GetLength(0); rows++)
            {
                for (int cols = 0; cols < Back.GetLength(1); cols++)
                {
                    if (Back[rows, cols] == 0)
                    {
                        break;
                    }
                    if (cols == Back.GetLength(1) - 2 && Back[rows, cols] == 1)
                    {
                        for (int h = rows; h > 0; h--)
                        {
                            for (int l = 1; l < Back.GetLength(1) - 1; l++)
                            {
                                Back[h, l] = Back[h - 1, l];
                             
                            }
                           
                        }
                        SumTotal += sumTotal;
                        PlaySound("FinishOneLine");
                    }
                }
              
                lblTotal.Text = SumTotal + "";
              

            }
         
           


        }
        public bool IsLoser() 
        {
            bool flag=true;
            for (int col = 0; col < Back.GetLength(1); col++)
            {
                for (int row = 0; row < Back.GetLength(0); row++)
                {
                    if (Back[row, col] == 0)
                    {
                        break;
                    }
                    if (Back[row, col] == 1)
                    {
                       
                         flag=false;
                        

                    }

                        
                       
                    
                }              

            }
            return flag;
        }
        bool flage=true;
        private void timer1_Tick(object sender, EventArgs e)
        {
                    
                    f.Down();
                    if (IsLoser())
                    {
                        if (!IsReplace())
                        {

                            f.RowIndex--;
                            NextOne();



                        }
                        DrawBack();
                        DrawCell();
                    }

         
                }
        int oldSpeed;
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer1.Interval = oldSpeed;

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 800;
            sumTotal = 10;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            sumTotal = 20;
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsStrat = true;
            f = RandomCell();
            FillBrushColor();
            timer1.Enabled = true;
        }
        string thePass="暂停";
        private void 暂停ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (IsStrat)
            {
                //默认为暂停为false ;
                if (thePass == "暂停")
                {
                    timer1.Enabled = false;
                    this.KeyPreview = false;
                    暂停ToolStripMenuItem1.Text = "开始";
                    //((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems[0].Text = "开始";
                    thePass = "开始";
                }
                else
                {
                    timer1.Enabled = true;
                    this.KeyPreview = true;
                    暂停ToolStripMenuItem1.Text = "暂停";
                    //((ToolStripMenuItem)menuStrip1.Items[0]).DropDownItems[0].Text = "暂停";
                }
            }
            else 
            {
                MessageBox.Show("请先开始游戏");
            }
          
         
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutFrom s = new AboutFrom();
            s.ShowDialog();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config c = new Config();
            c.ShowDialog();
        }
       
    }
}
    

