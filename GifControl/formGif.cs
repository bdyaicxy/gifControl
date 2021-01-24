using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Media.Imaging;

namespace GifControl
{
    public partial class formGif : Form
    {
        int istartMouseX;
        int istartMouseY;
        int icurIndex = 0;
        int icenterIndex = 0;
        bool bStartCtrol = false;
        bool bProcessing = false;

        gifAnalisys gifObj = null;
        public formGif()
        {
            InitializeComponent();
            //LoadTest();

            //GifBitmapEncoder
        }

        private void LoadTest()
        {
            gifObj = LoadGif(@"F:\MyProject\GifControl\GifControl\gif\star.gif");
            ShowImage(0);
        }

        private gifAnalisys LoadGif(string path)
        {
            gifAnalisys gifObj = new gifAnalisys(path);
            //Image img = gifObj.GetCurrentFrameImage(1);
            //img.Save(@"F:\MyProject\GifControl\GifControl\output\1.png", ImageFormat.Png);
            return gifObj;
        }

        private void ShowImage(int iIndex)
        {
            if(gifObj!=null)
            {
                imgMain.Image = gifObj.GetCurrentFrameImage(iIndex);
                icurIndex = iIndex;
            }
            
        }

        private void imgMain_MouseDown(object sender, MouseEventArgs e)
        {
            //将当前状态设置为操作状态
            bStartCtrol = true;
            istartMouseX = e.X;
            istartMouseY = e.Y;
            Console.WriteLine("X:{0};Y:{1}",istartMouseX,istartMouseY);
            icenterIndex = icurIndex;
        }

        private void imgMain_MouseMove(object sender, MouseEventArgs e)
        {
            if(bStartCtrol==true&& bProcessing==false)
            {
                //
                try
                {
                    bProcessing = true;

                    int icurMouseX = e.X;
                    int icurMouseY = e.Y;
                    Console.WriteLine("X:{0};Y:{1}", icurMouseX, icurMouseY);
                    //获得鼠标移动长度
                    int imoveX = icurMouseX - istartMouseX;
                    int imoveY = icurMouseY - istartMouseY;
                    Console.WriteLine("moveX:{0};moveY:{1}", imoveX, imoveY);

                    int iXImage = (int)imoveX / 1;//每移动三个像素则移动一个索引
                    int iXMoveTo = icenterIndex + iXImage;
                    //Console.WriteLine()
                    if (iXMoveTo < 0) iXMoveTo= iXMoveTo%(gifObj.FramesCount - 1) + (gifObj.FramesCount-1);
                    else if (iXMoveTo >= gifObj.FramesCount) iXMoveTo = iXMoveTo % (gifObj.FramesCount-1);
                    //if (iXMoveTo < 0 || iXMoveTo >= gifObj.FramesCount) return;//如果计算结果在可用范围值之外，则抛弃移动
                    ShowImage(iXMoveTo);
                }
                finally
                {
                    bProcessing = false;

                }
            }
        }

        private void imgMain_MouseUp(object sender, MouseEventArgs e)
        {
            bStartCtrol = false;
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                string strPath = ofd.FileName;
                gifObj = LoadGif(strPath);
                ShowImage(0);
            }
        }
    }
}
