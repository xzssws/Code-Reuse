using System.ComponentModel;
using System.Windows.Media;

namespace Module.Controls
{
    /// <summary>
    /// 抽象控件数据
    /// </summary>
    public abstract class ControlData : INotifyPropertyChanged
    {
        /// <summary>
        /// 宽度 属性字段
        /// <para>关联属性: Width</para>
        /// </summary>
        private double _width;

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged("Width"); }
        }

        /// <summary>
        /// 高度 属性字段
        /// <para>关联属性: Height</para>
        /// </summary>
        private double _height;

        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }

        /// <summary>
        /// 边框色 属性字段
        /// <para>关联属性: BorderColor</para>
        /// </summary>
        private Brush _bordercolor;

        /// <summary>
        /// 边框色
        /// </summary>
        public Brush BorderColor
        {
            get
            {
                if (_bordercolor == null) _bordercolor = Brushes.Gray;
                return _bordercolor;
            }
            set
            {
                _bordercolor = value;
                OnPropertyChanged("BorderColor");
            }
        }

        /// <summary>
        /// 边框线 属性字段
        /// <para>关联属性: BorderLine</para>
        /// </summary>
        private int _borderline;

        /// <summary>
        /// 边框线
        /// </summary>
        public int BorderLine
        {
            get
            {
                if (_borderline == null) _borderline = 1;
                return _borderline;
            }
            set
            {
                _borderline = value;
                OnPropertyChanged("BorderLine");
            }
        }

        /// <summary>
        /// 背景色 属性字段
        /// <para>关联属性: BackColor</para>
        /// </summary>
        private Brush _backcolor;

        /// <summary>
        /// 背景色
        /// </summary>
        public Brush BackColor
        {
            get
            {
                if (_backcolor == null) _backcolor = Brushes.DimGray;
                return _backcolor;
            }
            set
            {
                _backcolor = value;
                OnPropertyChanged("BackColor");
            }
        }

        /// <summary>
        /// 前景色 属性字段
        /// <para>关联属性: ForeColor</para>
        /// </summary>
        private Brush _forecolor;

        /// <summary>
        /// 前景色
        /// </summary>
        public Brush ForeColor
        {
            get
            {
                if (_forecolor == null) _forecolor = Brushes.Black;
                return _forecolor;
            }
            set
            {
                _forecolor = value;
                OnPropertyChanged("ForeColor");
            }
        }

        /// <summary>
        /// 字体 属性字段
        /// <para>关联属性: FontFamily</para>
        /// </summary>
        private FontFamily _fontfamily;

        /// <summary>
        /// 字体
        /// </summary>
        public FontFamily FontFamily
        {
            get
            {
                if (_fontfamily == null) _fontfamily = new FontFamily("宋体");
                return _fontfamily;
            }
            set
            {
                _fontfamily = value;
                OnPropertyChanged("FontFamily");
            }
        }

        /// <summary>
        /// 字体大小 属性字段
        /// <para>关联属性: FontSize</para>
        /// </summary>
        private int _fontsize;

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize
        {
            get
            {
                if (_fontsize == null) _fontsize = new int();
                return _fontsize;
            }
            set
            {
                _fontsize = value;
                OnPropertyChanged("FontSize");
            }
        }

        /// <summary>
        /// X坐标 属性字段
        /// <para>关联属性: X</para>
        /// </summary>
        private int _x;

        /// <summary>
        /// X坐标
        /// </summary>
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }

        /// <summary>
        /// Y坐标 属性字段
        /// <para>关联属性: Y</para>
        /// </summary>
        private int _y;

        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        /// 可用 属性字段
        /// <para>关联属性: Enabled</para>
        /// </summary>
        private bool _enabled;

        /// <summary>
        /// 可用
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        /// <summary>
        /// 隐藏 属性字段
        /// <para>关联属性: IsHide</para>
        /// </summary>
        private bool _ishide;

        /// <summary>
        /// 隐藏
        /// </summary>
        public bool IsHide
        {
            get
            {
                return _ishide;
            }
            set
            {
                _ishide = value;
                OnPropertyChanged("IsHide");
            }
        }

        /// <summary>
        /// 角度 属性字段
        /// <para>关联属性: du</para>
        /// </summary>
        private double _du;

        /// <summary>
        /// 角度
        /// </summary>
        public double du
        {
            get
            {
                return _du;
            }
            set
            {
                _du = value;
                OnPropertyChanged("du");
            }
        }

        /// <summary>
        /// 中文文本 属性字段
        /// <para>关联属性: ChineseText</para>
        /// </summary>
        private string _chinesetext;

        /// <summary>
        /// 中文文本
        /// </summary>
        public string ChineseText
        {
            get
            {
                return _chinesetext;
            }
            set
            {
                _chinesetext = value;
                OnPropertyChanged("ChineseText");
            }
        }

        /// <summary>
        /// 英文文本 属性字段
        /// <para>关联属性: EnglishText</para>
        /// </summary>
        private string _englishtext;

        /// <summary>
        /// 英文文本
        /// </summary>
        public string EnglishText
        {
            get
            {
                return _englishtext;
            }
            set
            {
                _englishtext = value;
                OnPropertyChanged("EnglishText");
            }
        }

        /// <summary>
        /// 名称 属性字段
        /// <para>关联属性: Name</para>
        /// </summary>
        private string _name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        #region INotifyPropertyChanged 成员

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 通知属性变更.
        /// </summary>
        /// <param name="property">属性名称</param>
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(property));
        }

        #endregion INotifyPropertyChanged 成员
    }
}