using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ScreenRecord
{
    /// <summary>
    /// 视频导出
    /// </summary>
    public class AVIExport
    {
        #region 字段属性

        /// <summary>
        /// AVI操作对象
        /// </summary>
        private AVIWriter VideoWriter;

        /// <summary>
        /// AVI帧
        /// </summary>
        private Bitmap CurFrame;

        /// <summary>
        /// 异步导出完成回调方法
        /// </summary>
        public event Action SaveFileAsyncMethod;

        private BackgroundWorker _savePathWorker;

        /// <summary>
        /// 异步操作对象
        /// </summary>
        private BackgroundWorker SavePathWorker
        {
            get
            {
                if (_savePathWorker == null) _savePathWorker = new System.ComponentModel.BackgroundWorker();
                //初始化完成事件
                _savePathWorker.RunWorkerCompleted += (sender, e) =>
                {
                    if (SaveFileAsyncMethod != null) SaveFileAsyncMethod();
                    SavePathWorker.Dispose();
                };

                return _savePathWorker;
            }
            set { _savePathWorker = value; }
        }

        #endregion 字段属性

        #region 导出方法
        /// <summary>
        /// 导出视频
        /// </summary>
        /// <param name="frame">帧率</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="Width">视频宽度</param>
        /// <param name="Height">视频高度</param>
        /// <param name="images">图片组</param>
        public void ExpVideo(uint frame, string SavePath, int Width, int Height, params Image[] images)
        {
            try
            {
                GC.Collect();
                VideoWriter = new AVIWriter();
                VideoWriter._frameRate = frame;
                //AVI中所有图像皆不能小于width及height
                CurFrame = VideoWriter.Create(SavePath, 1, Width, Height);

                for (int i = 0; i < images.Length; i++)
                {
                    Bitmap cache = new Bitmap(images[i]);
                    cache.RotateFlip(RotateFlipType.Rotate180FlipX);
                    VideoWriter.LoadFrame(cache);
                    VideoWriter.AddFrame();
                    cache.Dispose();
                    images[i].Dispose();
                }

                //关闭对象
                VideoWriter.Close();
                //释放当前帧
                CurFrame.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 导出视频
        /// </summary>
        /// <param name="SavePath">保存路径</param>
        /// <param name="frame">帧率</param>
        /// <param name="images">图片组</param>
        /// <returns>完成状态</returns>
        public bool ExpVideo(string SavePath, uint frame = 12, params Image[] images)
        {
            if (images != null && images.Length > 0)
            {
                int Width = images[0].Width;
                int Height = images[1].Height;
                ExpVideo(frame, SavePath, Width, Height, images);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 导出视频
        /// </summary>
        /// <param name="SavePath">保存路径</param>
        /// <param name="images">图片集合</param>
        /// <returns>完成状态</returns>
        public bool ExpVideo(string SavePath, List<Image> images)
        {
            Image[] imageList = images.ToArray();
            return ExpVideo(SavePath, 12, imageList);
            //分流处理
        }

        /// <summary>
        /// 导出视频
        /// </summary>
        /// <param name="ImageFiles">图片文件数组</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="frame">帧率</param>
        public void ExpVideo(string[] ImageFiles, string SavePath, int Width, int Height, uint frame = 12)
        {
            try
            {
                GC.Collect();

                VideoWriter = new AVIWriter();
                VideoWriter._frameRate = frame;
                //avi中所有图像皆不能小于width及height
                CurFrame = VideoWriter.Create(SavePath, 1, Width, Height);
                //遍历帧集
                int count = ImageFiles.Length;

                for (int i = 0; i < count; i++)
                {
                    string FileName = ImageFiles[i].ToString();
                    //获得图像
                    Image image = Image.FromFile(FileName);
                    Bitmap cache = new Bitmap(image);
                    //由于转化为avi后呈现相反，所以翻转
                    cache.RotateFlip(RotateFlipType.Rotate180FlipX);
                    //载入图像
                    VideoWriter.LoadFrame(cache);
                    //添加帧
                    VideoWriter.AddFrame();
                    cache.Dispose();
                    image.Dispose();
                    GC.Collect();
                }
                //关闭对象
                VideoWriter.Close();
                //释放当前帧
                CurFrame.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region 异步导出

        //TODO:异步完成事件 需要一个参数代表是否成功 或者异常

        /// <summary>
        /// 异步保存方法
        /// </summary>
        /// <param name="SaveFile">导出路径</param>
        public void BeginSaveFile(string SaveFile)
        {
            SavePathWorker.DoWork += (sender, e) =>
                ExpVideo(e.Argument.ToString());

            SavePathWorker.RunWorkerAsync(SaveFile);
        }

        /// <summary>
        /// 异步导出文件组
        /// </summary>
        /// <param name="ImageFiles">文件路径集合</param>
        /// <param name="SavePath">导出路径</param>
        /// <param name="Width">视频宽度</param>
        /// <param name="Height">视频高度</param>
        public void BeginSaveFile(string[] ImageFiles, string SavePath, int Width, int Height)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                AVIParam param = e.Argument as AVIParam;
                ExpVideo(param.imageFiles, param.saveFile, param.width, param.height);
            };

            SavePathWorker.RunWorkerAsync(new AVIParam
            {
                imageFiles = ImageFiles,
                saveFile = SavePath,
                width = Width,
                height = Height
            });
        }

        /// <summary>
        /// 异步导出视频文件
        /// </summary>
        /// <param name="frame">帧速</param>
        /// <param name="SavePath">保存路径</param>
        /// <param name="Width">视频宽度</param>
        /// <param name="Height">视频高度</param>
        /// <param name="images">图片数组</param>
        public void BeginExpVideo(uint frame, string SavePath, int Width, int Height, params Image[] images)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                AVIParam param = e.Argument as AVIParam;
                ExpVideo(param.frame, param.saveFile, param.width, param.height, param.images);
            };

            SavePathWorker.RunWorkerAsync(new AVIParam
            {
                frame = frame,
                saveFile = SavePath,
                width = Width,
                height = Height,
                images = images
            });
        }

        /// <summary>
        /// 异步导出视频文件
        /// </summary>
        /// <param name="SavePath">保存路径</param>
        /// <param name="frame">帧速</param>
        /// <param name="images">图片数组</param>
        public void BeginExpVideo(string SavePath, int frame = 12, params Image[] images)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                AVIParam param = e.Argument as AVIParam;
                ExpVideo(param.saveFile, param.frame, param.images);
            };

            SavePathWorker.RunWorkerAsync(new AVIParam { saveFile = SavePath, frame = 12, images = images });
        }

        /// <summary>
        /// 异步导出视频文件
        /// </summary>
        /// <param name="SavePath">保存路径</param>
        /// <param name="images">图片集合</param>
        public void BeginExpVideo(string SavePath, List<Image> images)
        {
            SavePathWorker.DoWork += (sender, e) =>
            {
                AVIParam param = e.Argument as AVIParam;
                ExpVideo(param.saveFile, param.imglist);
            };

            SavePathWorker.RunWorkerAsync(new AVIParam
            {
                saveFile = SavePath,
                imglist = images
            });
        }

        #endregion 
    }
}