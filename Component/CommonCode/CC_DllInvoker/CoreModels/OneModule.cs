using System;

namespace CC_DllInvoker
{
    /// <summary>
    /// 模块
    /// </summary>
    [Serializable]
    public class OneModule : System.ComponentModel.INotifyPropertyChanged
    {
        #region 属性定义

        /// <summary>
        /// 模块名称 属性字段
        /// <para>关联属性: Name</para>
        /// </summary>
        private string _name;

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; if (PropertyChanged != null)PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Name")); }
        }

        /// <summary>
        /// 输入参数列表 属性字段
        /// <para>关联属性: Inputs</para>
        /// </summary>
        private System.Collections.ObjectModel.ObservableCollection<IOneParameter> _inputs;

        /// <summary>
        /// 输入参数列表
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<IOneParameter> Inputs
        {
            get
            {
                if (_inputs == null) _inputs = new System.Collections.ObjectModel.ObservableCollection<IOneParameter>();
                return _inputs;
            }
            set { _inputs = value; if (PropertyChanged != null)PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Inputs")); }
        }

        /// <summary>
        /// 输出参数列表 属性字段
        /// <para>关联属性: Outputs</para>
        /// </summary>
        private System.Collections.ObjectModel.ObservableCollection<IOneParameter> _outputs;

        /// <summary>
        /// 输出参数列表
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<IOneParameter> Outputs
        {
            get
            {
                if (_outputs == null) _outputs = new System.Collections.ObjectModel.ObservableCollection<IOneParameter>();
                return _outputs;
            }
            set
            {
                _outputs = value;
                if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Outputs"));
            }
        }

        /// <summary>
        /// 可调与不可调参数 属性字段
        /// <para>关联属性: Parameters</para>
        /// </summary>
        private System.Collections.ObjectModel.ObservableCollection<IOneParameter> _parameters;

        /// <summary>
        /// 可调与不可调参数
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<IOneParameter> Parameters
        {
            get
            {
                if (_parameters == null) _parameters = new System.Collections.ObjectModel.ObservableCollection<IOneParameter>();
                return _parameters;
            }
            set
            {
                _parameters = value;
                if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Parameters"));
            }
        }

        /// <summary>
        /// 标志ID 属性字段
        /// <para>关联属性: ID</para>
        /// </summary>
        private string _id;

        /// <summary>
        /// 标志ID
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; if (PropertyChanged != null)PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ID")); }
        }

        #endregion 属性定义

        #region 事件

        /// <summary>
        /// 在更改属性值时发生。
        /// </summary>
        [field: NonSerialized]
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion 事件
    }
}