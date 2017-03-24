using System.Collections.Generic;
using System.ComponentModel;

namespace CC_DllInvoker
{
    /// <summary>
    /// 应用程序实体类
    /// </summary>
    [System.Serializable]
    public class BaseApplication : INotifyPropertyChanged
    {
        /// <summary>
        /// 应用程序编号
        /// <para>关联属性: AppID</para>
        /// </summary>
        private string _appID;

        /// <summary>
        /// 应用程序编号
        /// </summary>
        public virtual string AppID
        {
            get { return _appID; }
            set { _appID = value; OnPropertyChanged("AppID"); }
        }

        /// <summary>
        /// 应用程序名称
        /// <para>关联属性: AppName</para>
        /// </summary>
        private string _appName;

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public virtual string AppName
        {
            get { return _appName; }
            set { _appName = value; OnPropertyChanged("AppName"); }
        }

        /// <summary>
        /// 应用程序缩略语
        /// <para>关联属性: AppAbbreviation</para>
        /// </summary>
        private string _appAbbreviation;

        /// <summary>
        /// 应用程序缩略语
        /// </summary>
        public virtual string AppAbbreviation
        {
            get { return _appAbbreviation; }
            set { _appAbbreviation = value; OnPropertyChanged("AppAbbreviation"); }
        }

        /// <summary>
        /// 应用程序定义
        /// <para>关联属性: AppDefined</para>
        /// </summary>
        private string _appDefined;

        /// <summary>
        /// 应用程序定义
        /// </summary>
        public virtual string AppDefined
        {
            get { return _appDefined; }
            set { _appDefined = value; OnPropertyChanged("AppDefined"); }
        }

        /// <summary>
        /// XML文件路径
        /// <para>关联属性: ConfigPath</para>
        /// </summary>
        private string _configPath;

        /// <summary>
        /// XML文件路径
        /// </summary>
        public virtual string ConfigPath
        {
            get { return _configPath; }
            set { _configPath = value; OnPropertyChanged("ConfigPath"); }
        }

        /// <summary>
        /// DLL文件路径
        /// <para>关联属性: AppPath</para>
        /// </summary>
        private string _appPath;

        /// <summary>
        /// DLL文件路径
        /// </summary>
        public virtual string AppPath
        {
            get { return _appPath; }
            set { _appPath = value; OnPropertyChanged("AppPath"); }
        }

        /// <summary>
        /// XML文件名称
        /// <para>关联属性: ConfigFileName</para>
        /// </summary>
        private string _configFileName;

        /// <summary>
        /// XML文件名称
        /// </summary>
        public virtual string ConfigFileName
        {
            get { return _configFileName; }
            set { _configFileName = value; OnPropertyChanged("ConfigFileName"); }
        }

        /// <summary>
        /// DLL文件名称
        /// <para>关联属性: AppFileName</para>
        /// </summary>
        private string _appFileName;

        /// <summary>
        /// DLL文件名称
        /// </summary>
        public virtual string AppFileName
        {
            get { return _appFileName; }
            set { _appFileName = value; OnPropertyChanged("AppFileName"); }
        }

        /// <summary>
        /// XML文件信息
        /// <para>关联属性: ConfigInfo</para>
        /// </summary>
        private string _configInfo;

        /// <summary>
        /// XML文件信息
        /// </summary>
        public virtual string ConfigInfo
        {
            get { return _configInfo; }
            set { _configInfo = value; OnPropertyChanged("ConfigInfo"); }
        }

        /// <summary>
        /// DLL文件信息
        /// <para>关联属性: AppInfo</para>
        /// </summary>
        private string _appInfo;

        /// <summary>
        /// DLL文件信息
        /// </summary>
        public virtual string AppInfo
        {
            get { return _appInfo; }
            set { _appInfo = value; OnPropertyChanged("AppInfo"); }
        }

        /// <summary>
        /// DLL文件版本
        /// <para>关联属性: AppVersion</para>
        /// </summary>
        private string _appVersion;

        /// <summary>
        /// DLL文件版本
        /// </summary>
        public virtual string AppVersion
        {
            get { return _appVersion; }
            set { _appVersion = value; OnPropertyChanged("AppVersion"); }
        }

        /// <summary>
        /// XML文件版本
        /// <para>关联属性: ConfigVersion</para>
        /// </summary>
        private string _configVersion;

        /// <summary>
        /// XML文件版本
        /// </summary>
        public virtual string ConfigVersion
        {
            get { return _configVersion; }
            set { _configVersion = value; OnPropertyChanged("ConfigVersion"); }
        }

        /// <summary>
        /// 应用程序启动方式
        /// <para>关联属性: StartType</para>
        /// </summary>
        private StartType startType;

        /// <summary>
        /// 应用程序启动方式
        /// </summary>
        public virtual StartType StartType
        {
            get { return startType; }
            set { startType = value; OnPropertyChanged("StartType"); }
        }

        /// <summary>
        /// 应用程序选中状态
        /// <para>关联属性: IsChecked</para>
        /// </summary>
        private bool _isChecked;

        /// <summary>
        /// 应用程序选中状态
        /// </summary>
        public virtual bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; OnPropertyChanged("IsChecked"); }
        }

        /// <summary>
        /// 应用程序故障后重启次数
        /// <para>关联属性: RestartNum</para>
        /// </summary>
        private int _restartNum;

        /// <summary>
        /// 应用程序故障后重启次数
        /// </summary>
        public virtual int RestartNum
        {
            get { return _restartNum; }
            set { _restartNum = value; OnPropertyChanged("RestartNum"); }
        }

        /// <summary>
        /// 前置应用程序列表
        /// <para>关联属性: PreAppList</para>
        /// </summary>
        private string _preAppList;

        /// <summary>
        /// 前置应用程序列表
        /// </summary>
        public virtual string PreAppList
        {
            get { return _preAppList; }
            set { _preAppList = value; OnPropertyChanged("PreAppList"); }
        }

        /// <summary>
        /// 后置应用程序列表
        /// <para>关联属性: PostAppList</para>
        /// </summary>
        private string _postAppList;

        /// <summary>
        /// 后置应用程序列表
        /// </summary>
        public virtual string PostAppList
        {
            get { return _postAppList; }
            set { _postAppList = value; OnPropertyChanged("PostAppList"); }
        }

        /// <summary>
        /// 应用程序故障后是否重启
        /// <para>关联属性: IsRestartApp</para>
        /// </summary>
        private bool _isRestartApp;

        /// <summary>
        /// 应用程序故障后是否重启
        /// </summary>
        public virtual bool IsRestartApp
        {
            get { return _isRestartApp; }
            set { _isRestartApp = value; OnPropertyChanged("IsRestartApp"); }
        }

        /// <summary>
        /// 运行状态
        /// <para>关联属性: RunState</para>
        /// </summary>
        private RunState _runState;

        /// <summary>
        /// 运行状态
        /// </summary>
        public virtual RunState RunState
        {
            get { return _runState; }
            set { _runState = value; OnPropertyChanged("RunState"); }
        }

        /// <summary>
        /// 异常状态
        /// <para>关联属性: ExceptionState</para>
        /// </summary>
        private bool _exceptionState;

        /// <summary>
        /// 异常状态
        /// </summary>
        public virtual bool ExceptionState
        {
            get { return _exceptionState; }
            set { _exceptionState = value; OnPropertyChanged("ExceptionState"); }
        }

        /// <summary>
        /// 异常信息 属性字段
        /// <para>关联属性: ExceptionInfo</para>
        /// </summary>
        private string _exceptionInfo;

        /// <summary>
        /// 异常信息
        /// </summary>
        public virtual string ExceptionInfo
        {
            get { return _exceptionInfo; }
            set { _exceptionInfo = value; OnPropertyChanged("ExceptionInfo"); }
        }

        /// <summary>
        /// 应用程序实例
        /// <para>关联属性: Instances</para>
        /// </summary>
        private List<OneInstance> _instances;

        /// <summary>
        /// 应用程序实例
        /// </summary>
        public virtual List<OneInstance> Instances
        {
            get { return _instances; }
            set
            {
                _instances = value;
                OnPropertyChanged("Instances");
            }
        }

        /// <summary>
        /// 主服务器状态
        /// </summary>
        public string MasterStatus;

        /// <summary>
        /// 从服务器状态
        /// </summary>
        public string SlaveStatus;

        [field: System.NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }
    }
}