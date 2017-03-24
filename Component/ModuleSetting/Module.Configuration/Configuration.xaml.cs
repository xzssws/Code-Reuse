using System.Linq;
using Module.Configuration.Control;
using Module.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Runtime.Serialization.Formatters.Binary;

namespace Module.Configuration
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Configuration : Window
    {
        public Configuration()
        {
            InitializeComponent();
            ChangeTheme(new Xceed.Wpf.AvalonDock.Themes.MetroTheme());
            Load();
        }

        public void Load()
        {
            sp_ToolBox.Items.Add(new ModuleControl(new CanvasModule { Type = ModuleType.CIAA, Height = 130, Width = 300 }) { ModuleName = "CI报警区域", Height = 20 });
            sp_ToolBox.Items.Add(new ModuleControl(new CanvasModule { Type = ModuleType.ATA, Height = 30, Width = 610 }) { ModuleName = "工具栏区域", Height = 20 });
            sp_ToolBox.Items.Add(new ModuleControl(new CanvasModule { Type = ModuleType.ALA, Height = 260, Width = 610 }) { ModuleName = "列表区域", Height = 20 });
            sp_ToolBox.Items.Add(new ModuleControl(new CanvasModule { Type = ModuleType.ASA, Height = 30, Width = 610 }) { ModuleName = "状态栏区域", Height = 20 });
            sp_ToolBox.Items.Add(new ModuleControl(new CanvasModule { Type = ModuleType.NIAA, Height = 130, Width = 300 }) { ModuleName = "NI报警区域", Height = 20 });
        }

        private void _panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }
        /// <summary>
        /// 鼠标移动上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _panel_Drop(object sender, DragEventArgs e)
        {
            pre = null;
            if (e.Handled == false)
            {
                //获取当前控件
                Panel _panel = (Panel)sender;

                //获得数据对象
                IModule _module = e.Data.GetData("module") as IModule;
                //验证
                if (_panel != null && _module != null)
                {
                    var point = e.GetPosition(_panel);
                    ModuleControl _element = new ModuleControl(_module);
                    switch (_module.Type)
                    {
                        case ModuleType.NIAA:
                            _element.ModuleName = "NI报警区域";
                            break;

                        case ModuleType.CIAA:
                            _element.ModuleName = "CI报警区域";
                            break;

                        case ModuleType.ATA:
                            _element.ModuleName = "工具栏区域";
                            break;

                        case ModuleType.ALA:
                            _element.ModuleName = "报警列表区域";
                            break;

                        case ModuleType.ASA:
                            Button t = new Button();
                            t.Content = "fxzvz";
                            _panel.Children.Add(t);
                        //    layout.Modules.Add(t);
                            //_element.ModuleName = "状态栏区域";
                            break;

                        default:
                            break;
                    }
                    //添加控件

                    Canvas.SetLeft(_element, point.X);
                    Canvas.SetTop(_element, point.Y);
                    //将该面板中添加 那个控件
                    _panel.Children.Add(_element);
                    layout.Modules.Add(_element.Module);
                }
            }
        }

        private void sp_ToolBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.OriginalSource != null)
                {
                    var modulec = DesignHelper.FindAnchestor<ModuleControl>((DependencyObject)e.OriginalSource);
                    if (modulec != null)
                    {
                        base.OnMouseMove(e);
                        //如果鼠标在本控件上处于移动状态  并且鼠标左键已经点击 也就是说 鼠标左键单击该控件并进行移动时发生  检查鼠标移动时是否按下了左键
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            //创建数据对象  将模块对象添加到数据对象内
                            DataObject data = new DataObject("module", modulec.Module);
                            //启动拖放操作
                            DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
                        }
                    }
                }
            }
        }

        public ModuleControl CurrentModule
        {
            get { return (ModuleControl)GetValue(CurrentModuleProperty); }
            set { SetValue(CurrentModuleProperty, value); }
        }

        public static readonly DependencyProperty CurrentModuleProperty =
            DependencyProperty.Register("CurrentModule", typeof(ModuleControl), typeof(MainWindow), new PropertyMetadata(null));

        private void _panel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource != null)
            {
                var modulec = DesignHelper.FindAnchestor<ModuleControl>((DependencyObject)e.OriginalSource);
                if (modulec != null)
                {
                    CurrentModule = modulec;

                    foreach (var item in _panel.Children)
                    {
                        if (item is ContentControl)
                        {
                            Selector.SetIsSelected(item as ContentControl, false);
                        }
                    }
                    Selector.SetIsSelected(modulec, true);
                    this.Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        if (propertyPanel.SelectedObject != CurrentModule)
                        {
                            propertyPanel.SelectedObject = CurrentModule;
                        }
                    }));
                }
            }
        }

        private ILayout layout = new CanvasLayout() { Modules = new System.Collections.Generic.List<IModule>() };

        private void menu_open_Click(object sender, RoutedEventArgs e)
        {
        }

        private void menu_save_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _panel.Children)
            {
                var c = (item as ModuleControl);
                if (c != null)
                {
                    c.Module.Width = c.Width;
                    c.Module.Height = c.Height;
                    c.Module.Top = Canvas.GetTop(c);
                    c.Module.Left = Canvas.GetLeft(c);
                }
            }
            ModuleHandler.Save(layout, "d:\\no.lyt");
        }

        public virtual void ChangeTheme(Xceed.Wpf.AvalonDock.Themes.Theme theme)
        {
            MainDock.Theme = theme;
        }

        private ModuleControl pre;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IDataObject o = Clipboard.GetDataObject();
            byte[] d = (byte[])o.GetData(DataFormats.Serializable);
            var dd = (IModule)DeserializeObject(d);
            _panel.Children.Add(new ModuleControl(dd));
        }
        /// <summary>
        /// 把字节反序列化成相应的对象
        /// </summary>
        /// <param name="pBytes">字节流</param>
        /// <returns>object</returns>
        public object DeserializeObject(byte[] pBytes)
        {
            object _newOjb = null;
            if (pBytes == null)
                return _newOjb;
            System.IO.MemoryStream _memory = new System.IO.MemoryStream(pBytes);
            _memory.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            _newOjb = formatter.Deserialize(_memory);
            _memory.Close();
            return _newOjb;
        }
    }
}