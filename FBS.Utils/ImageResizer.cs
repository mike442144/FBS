using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;

namespace FBS.Utils
{
    public class ImageResizer
    {
        private string m_uplpadpath, m_temppath;
        public ImageResizer(string uploadpath, string temppath)
        {
            m_uplpadpath = uploadpath;
            m_temppath = temppath;
        }
        private void CheckTempDir()
        {
            DirectoryInfo di = new DirectoryInfo(this.m_temppath);
            if (!di.Exists)
                di.Create();
        }
        public string GetImage(string filename, int width, int height, params object[] config)
        {
            CheckTempDir();
            if (filename == null) return null;
            string tempImageName = Guid.NewGuid().ToString().Replace("-",string.Empty)+".jpg" ;//string.Format("{0}_{1}_{2}.{3}", filename.Substring(0, filename.LastIndexOf(".")), width, height, filename.Substring(filename.LastIndexOf(".") + 1));
            string realpath = HttpContext.Current.Server.MapPath(m_temppath) + tempImageName;
            
            if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(m_uplpadpath) + filename))
            {
                throw new NullReferenceException("图片不存在!");
            }
            Bitmap bmp1 = new Bitmap(HttpContext.Current.Server.MapPath(m_uplpadpath) + filename);
            Bitmap bmp2;
            if(bmp1.Width<width&&bmp1.Height<height)
                bmp2 = ResizeImage(bmp1, bmp1.Width, bmp1.Height, config);
            else
                bmp2 = ResizeImage(bmp1, width, height, config);
            ImageFormat iforamt = ImageFormat.Jpeg;
            bmp2.Save(realpath, iforamt);
            
            return tempImageName;
        }
        public Bitmap ResizeImage(Bitmap sourceBmp, int width, int height, params object[] config)//全景切割
        {
            double widthRateHeight = (double)sourceBmp.Width / (double)sourceBmp.Height;
            double targetWidthRateHeight = (double)width / (double)height;
            Rectangle destRect = new Rectangle();
            Bitmap targetBmp = null;
            if (widthRateHeight >= targetWidthRateHeight)
            {
                int newHeight = (int)Math.Round((double)width / widthRateHeight);
                destRect.Width = width;
                destRect.Height = newHeight;
                destRect.X = 0;
                destRect.Y = 0;//(int)Math.Round((double)(height - newHeight) / 2);
                targetBmp = new Bitmap(width, newHeight);
            }
            else
            {
                int newWidth = (int)Math.Round((double)height * widthRateHeight);
                destRect.Width = newWidth;
                destRect.Height = height;
                destRect.X = 0;//(int)Math.Round((double)(width - newWidth) / 2);
                destRect.Y = 0;
                targetBmp = new Bitmap(newWidth, height);
            }

            Graphics graphics = Graphics.FromImage(targetBmp);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(sourceBmp, destRect, new Rectangle(0, 0, sourceBmp.Width, sourceBmp.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            return targetBmp;

            //Bitmap bmp2 = new Bitmap(width, height);
            //double rate1 = (double)bmp1.Width / (double)bmp1.Height;
            //double rate2 = (double)bmp2.Width / (double)bmp2.Height;
            
            //if (rate1 >= rate2)//原图更宽，则按照原图宽比例计算不失真的高
            //{
            //    int destheight = (int)Math.Round((double)width / rate1);
            //    destRect.X = 0;
            //    destRect.Y = (int)Math.Round((double)(height - destheight) / 2);
            //    destRect.Width = width;
            //    destRect.Height = destheight;
            //}
            //else//原图更高,则按照原图的宽高比计算不失真的宽
            //{
            //    int destwidth = (int)Math.Round((double)height * rate1);
            //    destRect.X = (int)Math.Round((double)(width - destwidth) / 2);
            //    destRect.Y = 0;
            //    destRect.Width = destwidth;
            //    destRect.Height = height;
            //}
            //Graphics g = Graphics.FromImage(bmp2);
            ////g.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //g.DrawImage(bmp1, destRect, new Rectangle(0, 0, bmp1.Width, bmp1.Height), GraphicsUnit.Pixel);
            //if (config.Length > 0)
            //{
            //    g.DrawRectangle((Pen)config[0], new Rectangle(1, 1, width - 2, height - 2));
            //}
            //g.Dispose();
            
            //return bmp2;
        }



        public Bitmap ResizeImage(Bitmap sourceBmp, int width,int maxwidth)//全景切割
        {
            double widthRateHeight = (double)sourceBmp.Width / (double)sourceBmp.Height;
            //double targetWidthRateHeight = (double)width / (double)height;
            double theheight = width / widthRateHeight;//实际要转变不失真的高度
            //double targetWidthRateHeight =
            Rectangle destRect = new Rectangle();
            Bitmap targetBmp = null;
            if(width<=maxwidth)//未超过最大宽度
            {
                int newHeight = (int)Math.Round((double)width / widthRateHeight);
                destRect.Width = width;
                destRect.Height = newHeight;
                destRect.X = 0;
                destRect.Y = 0;//(int)Math.Round((double)(height - newHeight) / 2);
                targetBmp = new Bitmap(width, newHeight);
            } 
            else//超过最大宽度
            {
                int newWidth = maxwidth-3;
                destRect.Width = newWidth;
                int height = (int)Math.Round((double)newWidth / widthRateHeight);
                destRect.Height = height;
                destRect.X = 0;//(int)Math.Round((double)(width - newWidth) / 2);
                destRect.Y = 0;
                targetBmp = new Bitmap(newWidth, height);
            }

            Graphics graphics = Graphics.FromImage(targetBmp);
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            graphics.DrawImage(sourceBmp, destRect, new Rectangle(0, 0, sourceBmp.Width, sourceBmp.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            return targetBmp;
        }

    }
}
