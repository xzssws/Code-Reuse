using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace OSLibrary.媒体.图像
{
    /// <summary>
    /// 批量转换图片
    /// </summary>
    class ImagePi
    {
        /// <summary>
        /// 开始转换图片
        /// </summary>
        /// <param name="path">源文件路径(完整路径,包括文件名)</param>
        /// <param name="savePath">生成图片保存路径(包括文件名包括文件名,但不包括文件格式)</param>
        /// <param name="width">新生成图片宽度</param>
        /// <param name="height">新生成图片高度</param>
        /// <param name="format">格式,默认为gif(需生成多种格式,则以"|"隔开,例如"jpg|gif|png")</param>
        /// <returns></returns>
        public static bool Start(string path, string savePath, int width, int height, string format)
        {
            if (File.Exists(path))
            {

                try
                {
                    Image resimage = Image.FromFile(path);
                    Bitmap bitmap = new Bitmap(resimage, new Size(width, height));
                    ImageFormat[] ilist = GetFormat(format);
                    if (!savePath.EndsWith(".")) savePath += ".";
                    foreach (ImageFormat imgfor in ilist)
                    {
                        string backname = imgfor.ToString().ToLower();
                        if (backname == "jpeg") backname = "jpg";
                        bitmap.Save(savePath + backname, imgfor);
                    }
                    resimage.Dispose();
                    bitmap.Dispose();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取待生成的图片格式列表,可能需要生成多种格式
        /// </summary>
        /// <param name="formats">参数,多种格式以"|"隔开</param>
        /// <returns></returns>
        protected static ImageFormat[] GetFormat(string format)
        {
            if (string.IsNullOrEmpty(format))
                format = "gif";
            else
                format = format.ToLower();
            string[] formats = format.Split('|');

            ImageFormat[] formatlist = new ImageFormat[formats.Length];
            for (int i = 0; i < formats.Length; i++)
            {
                switch (formats[i])
                {
                    case "jpg":
                        formatlist[i] = ImageFormat.Jpeg;
                        break;
                    case "png":
                        formatlist[i] = ImageFormat.Png;
                        break;
                    default:
                        formatlist[i] = ImageFormat.Gif;
                        break;
                }
            }

            return formatlist;
        }


    }
}
