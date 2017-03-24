using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;

namespace ScreenRecord
{
    /// <summary>
    /// 屏幕监视器
    /// </summary>
    public class ScreenRecording
    {
        public ScreenRecording()
        {
            Load();
        }

        #region 引用API

        private int _X, _Y;

        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO
        {
            public bool fIcon;
            public Int32 xHotspot;
            public Int32 yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        private static extern int GetSystemMetrics(int mVal);

        [DllImport("user32.dll", EntryPoint = "GetCursorInfo")]
        private static extern bool GetCursorInfo(ref CURSORINFO cInfo);

        [DllImport("user32.dll", EntryPoint = "CopyIcon")]
        private static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll", EntryPoint = "GetIconInfo")]
        private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO iInfo);

        #endregion 引用API

        #region 全部字段

        //监视到的图片
        public event Action<Image> HaveImage;

        //监视器
        private Timer timer;

        //AVI读写类
        private AVIWriter VideoWriter;

        //AVI帧
        private Bitmap CurFrame;

        //AVI高度
        private int VideoHeight;

        //AVI宽度
        private int VideoWidth;

        //自带图片路径记录
        public List<string> Frames;

        //图片保存路径
        private string RootPath;

        //全局是否记录光标
        private bool CanCursor;

        //保存名称
        private int NameFlag;

        //速率
        private int Tick = 20;

 

        #endregion 全部字段

        #region 内核方法
        /// <summary>
        /// 内构建方法
        /// </summary>
        private void Load()
        {
            timer = new Timer();//监视器
            Frames = new List<string>();//图片路径集合
            CanCursor = true;//是否显示图标
            NameFlag = 0;//命名标志
            RootPath = Application.StartupPath.ToString();
            Tick = 80;//默认时间为80

            timer.Tick += TimerTick;//监视器触发事件
            timer.Interval = Tick;//监视器定时监测时间
        }


        /// <summary>
        /// 截取带有鼠标光标的图片
        /// </summary>
        /// <returns>屏幕截图</returns>
        private Bitmap CaptureDesktop()
        {
            try
            {
                int _CX = 0, _CY = 0;
                Bitmap _Source = new Bitmap(GetSystemMetrics(0), GetSystemMetrics(1));
                using (Graphics g = Graphics.FromImage(_Source))
                {
                    g.CopyFromScreen(0, 0, 0, 0, _Source.Size);
                    g.DrawImage(CaptureCursor(ref _CX, ref _CY), _CX, _CY);
                    g.Dispose();
                }
                _X = (800 - _Source.Width) / 2;
                _Y = (600 - _Source.Height) / 2;
                return _Source;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 截取没有鼠标光标的图片
        /// </summary>
        /// <returns>屏幕截图</returns>
        private Bitmap CaptureNoCursor()
        {
            Bitmap _Source = new Bitmap(GetSystemMetrics(0), GetSystemMetrics(1));
            using (Graphics g = Graphics.FromImage(_Source))
            {
                g.CopyFromScreen(0, 0, 0, 0, _Source.Size);
                g.Dispose();
            }
            return _Source;
        }

        /// <summary>
        /// 截取光标图片
        /// </summary>
        /// <returns>光标截图</returns>
        private Bitmap CaptureCursor(ref int _CX, ref int _CY)
        {
            IntPtr _Icon;
            CURSORINFO _CursorInfo = new CURSORINFO();
            ICONINFO _IconInfo;
            _CursorInfo.cbSize = Marshal.SizeOf(_CursorInfo);
            if (GetCursorInfo(ref _CursorInfo))
            {
                if (_CursorInfo.flags == 0x00000001)
                {
                    _Icon = CopyIcon(_CursorInfo.hCursor);

                    if (GetIconInfo(_Icon, out _IconInfo))
                    {
                        _CX = _CursorInfo.ptScreenPos.X - _IconInfo.xHotspot;
                        _CY = _CursorInfo.ptScreenPos.Y - _IconInfo.yHotspot;
                        return Icon.FromHandle(_Icon).ToBitmap();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 监视器监测时间
        /// </summary>
        private void TimerTick(object sender, EventArgs e)
        {
            GC.Collect();
            //定义图像
            Bitmap tmp;
            //根据是否显示图标截图
            tmp = CanCursor ? CaptureDesktop() : CaptureNoCursor();
            //TODO：找适合去掉它  默认录屏大小每次太麻烦
            if (NameFlag == 0)
            {
                VideoWidth = tmp.Width;
                VideoHeight = tmp.Height;
            }
            //对外开放一个方法 表示当前截屏截到的图片
            if (HaveImage != null) HaveImage(tmp);
            else HaveImage += new Action<Image>(DefaultImage);

            //帧标志递增
            NameFlag++;
        }

        /// <summary>
        /// 监视器截取到的图片进行处理
        /// <para>
        /// [默认] 来自HaveImage委托
        /// </para>
        /// </summary>
        /// <param name="img">屏幕截图</param>
        private void DefaultImage(Image img)
        {
            //定义保存路径
            string picpath = RootPath + "\\" + NameFlag + ".bmp";
            //保存图片到本地磁盘
            img.Save(picpath);
            //在帧集中添加帧的路径
            Frames.Add(picpath);
        }
        #endregion

        #region 公开方法

        #region 截图

        /// <summary>
        /// 获取屏幕截图
        /// </summary>
        /// <param name="HaveCur">是否有图标</param>
        /// <returns>屏幕截图</returns>
        public Image CreateImage(bool HaveCur)
        {
            if (HaveCur)
            {
                return CaptureDesktop();
            }
            else
            {
                return CaptureNoCursor();
            }
        }

        /// <summary>
        /// 获取光标图像
        /// </summary>
        /// <returns>光标图像</returns>
        public Image CreateCur()
        {
            int x = 0;
            int y = 0;
            Bitmap resultBit = CaptureCursor(ref x, ref y);
            Console.WriteLine("X:{0}  Y:{1}", x, y);
            return resultBit;
        }

        #endregion 截图

        #region 监控

        /// <summary>
        /// 开始监控
        /// </summary>
        public void LookStart()
        {
            Frames.Clear();//清空帧集
            timer.Start();//开始监视
        }
        /// <summary>
        /// 停止监控
        /// </summary>
        public void LookStop()
        {
            timer.Stop();//停止监视
            NameFlag = 0;//标志初始
        }

        #endregion 监控

        /// <summary>
        /// 导出录像
        /// <para>
        /// 系统默认
        /// </para>
        /// </summary>
        /// <param name="SavePath">保存路径</param>
        public void ExpVideo(string SavePath)
        {
            string[] files = Frames.ToArray<string>();
            AVIExport ai = new AVIExport();
            ai.ExpVideo(files, SavePath,1366,768);
        }

        #endregion 公开方法
    }
}
