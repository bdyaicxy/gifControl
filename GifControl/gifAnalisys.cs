using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GifControl
{
    public class gifAnalisys
    {
        private string filePath;
        private Image mImage;
        private FrameDimension mDimension;
        private int mWidth;
        private int mHeight;
        private int mFramesCount;


        public gifAnalisys(string path)
        {
            filePath = path;
            FileStream fs = File.OpenRead(path);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            MemoryStream ms = new MemoryStream(buffer);
            //mImage = Image.FromStream(ms);
            mImage = Image.FromFile(path);
            mDimension = new FrameDimension(mImage.FrameDimensionsList[0]);
            mFramesCount = mImage.GetFrameCount(mDimension);
            mWidth = mImage.Width;
            mHeight = mImage.Height;
        }
        public int Width
        {
            get { return mWidth; }
        }
        public Image Image
        {
            get { return mImage; }
        }
        public int Height
        {
            get { return mHeight; }
        }
        public int FramesCount
        {
            get { return mFramesCount; }
        }

        public int SelectActiveFrame(int index)
        {
            int res = mImage.SelectActiveFrame(mDimension, index);
            mWidth = mImage.Width;
            mHeight = mImage.Height;
            return res;
        }

        public Bitmap GetCurrentFrameImage()
        {
            Bitmap frame = new Bitmap(mWidth, mHeight);
            Graphics.FromImage(frame).DrawImage(mImage, Point.Empty);
            return frame;
        }

        public Bitmap GetCurrentFrameImage(int index)
        {
            SelectActiveFrame(index);
            return GetCurrentFrameImage();
        }


    }
}
