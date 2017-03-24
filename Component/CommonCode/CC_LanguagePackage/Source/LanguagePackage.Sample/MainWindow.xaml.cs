using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanguagePackage.Sample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //英文按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LanguageProvider.SetLanguage("en-us");
        }
        //中文按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LanguageProvider.SetLanguage("zh-cn");
        }
    }
}
