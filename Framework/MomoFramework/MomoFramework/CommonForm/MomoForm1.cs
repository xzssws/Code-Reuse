using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MomoFrameWork
{
    public partial class DataInfo1 : DevExpress.XtraEditors.XtraForm
    {
        public DataInfo1()
        {
            InitializeComponent();
        }
        //是否显示用户自定义尾部小条
        public void SetBar(bool bl)
        {
            bar1.OptionsBar.AllowQuickCustomization = bl;
        }
        //隐藏工具条头
        public void HideHeader(bool bl)
        {
            bar1.OptionsBar.DrawDragBorder = bl;
        }
        //工具行是否全行
        public void IsRowFull(bool bl)
        {
            bar1.OptionsBar.UseWholeRow = bl;
        }
    }
}