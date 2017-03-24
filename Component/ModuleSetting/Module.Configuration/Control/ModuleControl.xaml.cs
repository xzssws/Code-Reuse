using Module.Core;
using System;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Module.Configuration.Control
{
    /// <summary>
    /// ModuleControl.xaml 的交互逻辑
    /// </summary>
    [Serializable]
    public partial class ModuleControl : UserControl
    {
        public ModuleControl()
        {
            InitializeComponent();
        }
   
        public string ModuleName
        {
            get { return (string)GetValue(ModuleNameProperty); }
            set { SetValue(ModuleNameProperty, value); }
        }

        public static readonly DependencyProperty ModuleNameProperty =
            DependencyProperty.Register("ModuleName", typeof(string), typeof(ModuleControl), new PropertyMetadata("MODULE"));

        public ModuleControl(IModule c)
        {
            InitializeComponent();
            this.Module = c;
        }

        public double X
        {
            get { return  Math.Round(Canvas.GetLeft(this)); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(ModuleControl), new FrameworkPropertyMetadata(0.0, XChanged));


        private static void XChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Canvas.SetLeft((d as UIElement), (double)e.NewValue);
        }
     
        public double Y
        {
            get { return  Math.Round(Canvas.GetTop(this)); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(ModuleControl), new FrameworkPropertyMetadata(0.0, YChanged));

        private static void YChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Canvas.SetTop((d as UIElement), (double)e.NewValue);
        }
        /// <summary>
        /// 模块 属性字段
        /// <para>关联属性: Module</para>
        /// </summary>
        private IModule _module;
        /// <summary>
        /// 模块
        /// </summary>
        public IModule Module
        {
            get { return _module; }
            set
            {
                _module = value;
                this.Height = _module.Height;
                this.Width = _module.Width;
                Canvas.SetLeft(this, _module.Left);
                Canvas.SetTop(this, _module.Top);
            }
        }




        public int TIndex
        {
            get { return (int)Panel.GetZIndex(this); }
            set { Panel.SetZIndex(this, value); }
        }

     

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var parent = DesignHelper.FindAnchestor<Panel>(this);
            parent.Children.Remove(this);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var parent = DesignHelper.FindAnchestor<Panel>(this);
            parent.Children.Remove(this);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Serializable, SerializeObject(this));
        }
        public byte[] SerializeObject(object pObj)
        {
            if (pObj == null)
                return null;
            System.IO.MemoryStream _memory = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(_memory, pObj);
            _memory.Position = 0;
            byte[] read = new byte[_memory.Length];
            _memory.Read(read, 0, read.Length);
            _memory.Close();
            return read;
        }
    }
}