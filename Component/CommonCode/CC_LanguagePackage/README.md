# LanguagePackage 语言包
这是一个用于WPF程序，用资源字典的方式实现多语言的一种解决方案（目前只支持两种语言相互切换）
## 如何使用
### 基础工作
1.添加 LanguagePackage 项目引用。
2.编辑 LanguagePackage 项目目录下的Lang.dic文件，定义语言。  
      
	--代表注释（使用SQL语言的风格）
    --[标记位置说明]
    #{按钮1}:中文=Chinese
    #{按钮2}:英文=English
    --如果不写语言的话，那么这就是语言
    语言2=Language2

3.打开LanguagePackage项目目录下的static目录下的zh-cn.tt和en-us.tt 并保存（T4模板用于生成静态语言包，当动态语言无法使用时使用静态的 保险）。
### 使用语言
1.在项目的App.xaml 中声明

	<Application ......
	             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	             StartupUri="MainWindow.xaml">
	    <Application.Resources>
	        <ResourceDictionary>
	            <ResourceDictionary.MergedDictionaries>
	                <ResourceDictionary Source="pack://application:,,,/LanguagePackage;component/Static/zh-cn.xaml" />
	            </ResourceDictionary.MergedDictionaries>
	        </ResourceDictionary>
	    </Application.Resources>
	</Application>

2.在页面中使用

    <Window x:Class="LanguagePackage.Sample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:y="clr-namespace:LanguagePackage;assembly=LanguagePackage"
        Title="MainWindow" Height="350" Width="525">
    <Grid>
        <Button Content="{y:Language 定义的语言}" HorizontalAlignment="Left" Margin="10,40,0,0" VerticalAlignment="Top" Height="30" Width="75" />

3.切换语言   

    //英文
    LanguageProvider.SetLanguage("en-us");
    //中文
    LanguageProvider.SetLanguage("zh-cn"); 
  
4.继续你的程序吧  
[SecondDoc.md](SecondDoc.md "继续")  

5.求打赏，求包养   
![](http://i.imgur.com/t5JKA6U.png)
