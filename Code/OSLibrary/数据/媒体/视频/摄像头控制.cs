﻿using System;
using System.Runtime.InteropServices;

namespace 摄像头控制
{
    public class VideoAPI
    {
        [DllImport("avicap32.dll")]
        public static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);

        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);

        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);

        public const int WM_USER = 0x400;

        public const int WS_CHILD = 0x40000000;

        public const int WS_VISIBLE = 0x10000000;

        public const int SWP_NOMOVE = 0x2;

        public const int SWP_NOZORDER = 0x4;

        public const int WM_CAP_DRIVER_CONNECT = WM_USER + 10;

        public const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;

        public const int WM_CAP_SET_CALLBACK_FRAME = WM_USER + 5;

        public const int WM_CAP_SET_PREVIEW = WM_USER + 50;

        public const int WM_CAP_SET_PREVIEWRATE = WM_USER + 52;

        public const int WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;

        public const int WM_CAP_START = WM_USER;

        public const int WM_CAP_SAVEDIB = WM_CAP_START + 25;
    }

    public class cVideo
    {
        private IntPtr lwndC;
        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;

        public cVideo(IntPtr handle, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
        }

        public void StartWebCam()
        {
            byte[] lpszName = new byte[100];
            byte[] lpszVer = new byte[100];
            //      VideoAPI.capCreateCaptureWindowA(0,lpszName,100,lpszVer,100);
            this.lwndC = VideoAPI.capCreateCaptureWindowA(lpszName, VideoAPI.WS_CHILD | VideoAPI.WS_VISIBLE, 0, 0, mWidth, mHeight, mControlPtr, 0);
            if (VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_DRIVER_CONNECT, 0, 0))
            {
                VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_SET_PREVIEWRATE, 100, 0);
                VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_SET_PREVIEW, true, 0);
            }
        }

        public void CloseWebcam()
        {
            VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }

        public void GetImage(IntPtr hWndC, string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            VideoAPI.SendMessage(lwndC, VideoAPI.WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }

        private cVideo video=null;

        private void button1_Click(object sender, EventArgs e)
        {
            //video = new cVideo(pictureBox1.Handle, pictureBox1.Width, pictureBox1.Height);
            video.StartWebCam();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            video.CloseWebcam();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //video.GetImage(pictureBox1.Handle, "d:\\Image.jpg");
        }
    }
}