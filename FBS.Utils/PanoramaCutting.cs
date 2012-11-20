using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FBS.Utils
{
    public class PanoramaCutting
    {
        private string m_uplpadpath, m_temppath;
        public PanoramaCutting(string uploadpath, string temppath)
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
            //System.IO.File file;
            //string[] ss = filename.Split('.');
            string temppath = string.Format("{0}-{1}-{2}.{3}", filename.Substring(0, filename.LastIndexOf(".")), width, height, filename.Substring(filename.LastIndexOf(".") + 1));
            temppath = temppath.Substring(temppath.IndexOf('/') + 1);
            string realpath = HttpContext.Current.Server.MapPath(m_temppath) + temppath;
            if (!System.IO.File.Exists(realpath))
            {
                Bitmap bmp1 = new Bitmap(HttpContext.Current.Server.MapPath(m_uplpadpath) + filename);
                Bitmap bmp2 = ResizeImage(bmp1, width, height, config);
                ImageFormat iforamt=ImageFormat.Jpeg;
                //switch (System.IO.Path.GetExtension(filename))
                //{
                //    case ".jpg":
                //    case ".jpeg":
                //        iforamt = ImageFormat.Jpeg;
                //        break;
                //    case ".gif":
                //        iforamt = ImageFormat.Gif;
                //        break;
                //    case ".png":
                //        iforamt = ImageFormat.Png;
                //        break;
                //    case ".bmp":
                //        iforamt = ImageFormat.Jpeg;
                //        break;
                //    default:
                //        iforamt = ImageFormat.Jpeg;
                //        break;
                //}
                bmp2.Save(realpath, iforamt);
            }
            return temppath;
        }
        public Bitmap ResizeImage(Bitmap bmp1, int width, int height, params object[] config)//全景切割
        {
            Bitmap bmp2 = new Bitmap(width, height);
            double rate1 = (double)bmp1.Width / (double)bmp1.Height;
            double rate2 = (double)bmp2.Width / (double)bmp2.Height;
            Rectangle destRect = new Rectangle();
            if (rate1 >= rate2)//原图更宽，则按照原图宽比例计算不失真的高
            {
                int destheight = (int)Math.Round((double)width / rate1);
                destRect.X = 0;
                destRect.Y = (int)Math.Round((double)(height - destheight) / 2);
                destRect.Width = width;
                destRect.Height = destheight;
            }
            else//原图更高,则按照原图的宽高比计算不失真的宽
            {
                int destwidth = (int)Math.Round((double)height * rate1);
                destRect.X = (int)Math.Round((double)(width - destwidth) / 2);
                destRect.Y = 0;
                destRect.Width = destwidth;
                destRect.Height = height;
            }
            Graphics g = Graphics.FromImage(bmp2);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(bmp1, destRect, new Rectangle(0, 0, bmp1.Width, bmp1.Height), GraphicsUnit.Pixel);
            if (config.Length > 0)
            {
                g.DrawRectangle((Pen)config[0], new Rectangle(1, 1, width - 2, height - 2));
            }
            g.Dispose();
            return bmp2;
        }
    }
}
