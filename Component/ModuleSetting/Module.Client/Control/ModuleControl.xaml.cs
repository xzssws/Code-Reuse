using Module.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Module.Client.Control
{
    /// <summary>
    /// ModuleControl.xaml 的交互逻辑
    /// </summary>
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
    }
}