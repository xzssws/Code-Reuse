using System.Collections.Generic;
using System.Drawing;

namespace ScreenRecord
{
    internal class AVIParam
    {
        /// <summary>
        /// 保存文件路径
        /// </summary>
        internal string saveFile { get; set; }

        /// <summary>
        /// 图片文件路径
        /// </summary>
        internal string[] imageFiles { get; set; }

        /// <summary>
        /// 图片组
        /// </summary>
        internal Image[] images { get; set; }

        /// <summary>
        /// 图片集合
        /// </summary>
        internal List<Image> imglist { get; set; }

        /// <summary>
        /// 帧速
        /// </summary>
        internal uint frame { get; set; }

        /// <summary>
        /// 视频文件宽度
        /// </summary>
        internal int width { get; set; }

        /// <summary>
        /// 视频文件高度
        /// </summary>
        internal int height { get; set; }
    }
}