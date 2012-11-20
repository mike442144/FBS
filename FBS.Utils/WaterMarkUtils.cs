using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Globalization;

namespace FBS.Utils
{
    public class WaterMarkUtils
    {
        /// <summary>
        /// 插入水印
        /// </summary>
        /// <param name="basePath">处理图片的目录</param>
        /// <param name="photoFilename">图片的名称 例如：1.jpg</param>
        /// <param name="watermarkFilename">水印图片的名称</param>
        /// <param name="copyrightString">版权文字</param>
        /// <param name="ext">图片扩展名</param>
        /// <param name="opacity">是否透明，true透明</param>
        /// <returns></returns>
        public static string AddWatermarkLayer(string basePath, string photoFilename, string watermarkFilename, string copyrightString, string ext, bool opacity)
        {
            //Create the image object from the path
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(basePath + photoFilename);

            //Get the dimensions of imgPhoto
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //Create a new object from the imgPhoto
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //Load the watermark image saved as .bmp and set with background color of green (Alpha=0, R=106, G=125, B=106)
            System.Drawing.Image imgWatermark = System.Drawing.Image.FromFile(basePath + watermarkFilename);

            //Size of imgWatermark
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //Draws the imgPhoto to the graphics object position at (x-0, y=0) 100% of original
            grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

            //To maximize the size of the Logo text using seven different fonts
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();

            //Determines the largest possible size of the font
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                crSize = grPhoto.MeasureString(copyrightString, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //For photos with varying heights, determines a five percent position from bottom of image
            int yPixlesFromBottom = (int)(phHeight * .05);
            float yPosFromBottom = (int)((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
            float xCenterOfImg = (phWidth / 2);

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //Create a brush with 60% Black (Alpha 153)
            SolidBrush semiTransparBrushOne = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Creates a shadow effect
            grPhoto.DrawString(copyrightString, crFont, semiTransparBrushOne, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);

            //Create a brush with 60% White (Alpha 153)
            SolidBrush semiTransparBrushTwo = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draws the same text on top of the previous
            grPhoto.DrawString(copyrightString, crFont, semiTransparBrushTwo, new PointF(xCenterOfImg + 1, yPosFromBottom + 1), StrFormat);

            //Set the above into a bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grWatermark = Graphics.FromImage(bmWatermark);
            grWatermark.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //Apply two color manipulations for the watermark
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            System.Drawing.Imaging.ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            float[][] colorMatrixElements=null;
            if (opacity)
            {
                //Change the opacity of watermark by setting 3rd row, 3rd col to .3f
               float[][] col = { 
                   new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                   new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                   new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                   new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                   new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                            };
               colorMatrixElements = col;
            }
            else
            {
                float[][] col = { 
                   new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                   new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                   new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                   new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                   new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                                };
                colorMatrixElements = col;
            }

            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            //Draw the watermark in the upper right hand corner of photo
            int xPosOfWm = ((phWidth - wmWidth));
            int yPosOfWm = phHeight - wmHeight - 10;

            grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, GraphicsUnit.Pixel, imageAttributes);

            //Finally, replace the original image with new
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();

            //Save as original name but concatenate _watermarked to it
            string newFilename = Guid.NewGuid().ToString() + ext;

            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);

            imgPhoto.Save(basePath+"/" +ymd+"/"+ newFilename, ImageFormat.Jpeg);
            imgWatermark.Dispose();
            imgPhoto.Dispose();

            return newFilename;

        }
    }
}
