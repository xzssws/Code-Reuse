using System.Windows;

namespace Test.CC_LanguageDictionary
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

        private void chk_Language_Checked(object sender, RoutedEventArgs e)
        {
            LanguageProvider.SetLanguage("zh-cn");
        }

        private void btn_ShowAnthor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("徐金泽".l());
        }

        private void chk_Language_Unchecked(object sender, RoutedEventArgs e)
        {
            LanguageProvider.SetLanguage("en-us");
        }
    }
}