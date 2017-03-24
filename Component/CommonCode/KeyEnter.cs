#region 引入名称空间定义

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

#endregion 引入名称空间定义

namespace Public.EP.Resources.Behavior
{
    /// <summary>
    /// 类
    /// </summary>
    public class KeyEnter : Behavior<UIElement>
    {
        #region 成员定义
        /// <summary>
        /// 拖放处理命令依赖属性
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(KeyEnter), new PropertyMetadata(null));
        /// <summary>
        /// 拖放处理命令参数依赖属性
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(KeyEnter), new PropertyMetadata(null));

        #endregion 成员定义

        #region 属性定义

        /// <summary>
        /// 获取或设置拖放处理命令依赖属性
        /// </summary>
        public ICommand Command
        {
            get
            {
                return (ICommand)base.GetValue(KeyEnter.CommandProperty);
            }
            set
            {
                base.SetValue(KeyEnter.CommandProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置拖放处理命令参数依赖属性
        /// </summary>
        public object CommandParameter
        {
            get
            {
                return base.GetValue(KeyEnter.CommandParameterProperty);
            }
            set
            {
                base.SetValue(KeyEnter.CommandParameterProperty, value);
            }
        }

        #endregion 属性定义

        #region 方法定义

        /// <summary>
        ///  当点击enter事去触发行为绑定命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownEvent(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == DownModifierKey && e.Key == DownKey)
            {
                Command.Execute(null);
            }
        }

        /// <summary>
        /// 连接行为处理
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.KeyDown += KeyDownEvent;
        }

        /// <summary>
        /// 断开连接行为处理
        /// </summary>
        protected override void OnDetaching()
        {
            this.AssociatedObject.KeyDown -= KeyDownEvent;
            base.OnDetaching();
        }

        #endregion 方法定义



        /// <summary>
        /// 获取或设置按下的键
        /// </summary>
        /// <value>Down key.</value>
        public Key DownKey
        {
            get { return (Key)GetValue(DownKeyProperty); }
            set { SetValue(DownKeyProperty, value); }
        }

        /// <summary>
        /// 获取或设置按下的键
        /// </summary>
        public static readonly DependencyProperty DownKeyProperty =
            DependencyProperty.Register("DownKey", typeof(Key), typeof(KeyEnter), new PropertyMetadata(Key.Enter));



        /// <summary>
        /// 获取或设置按下的控制键
        /// </summary>
        public ModifierKeys DownModifierKey
        {
            get { return (ModifierKeys)GetValue(DownModifierKeyProperty); }
            set { SetValue(DownModifierKeyProperty, value); }
        }

        /// <summary>
        /// 获取或设置按下的控制键
        /// </summary>
        public static readonly DependencyProperty DownModifierKeyProperty =
            DependencyProperty.Register("DownModifierKey", typeof(ModifierKeys), typeof(KeyEnter), new PropertyMetadata(ModifierKeys.None));
    }
}
