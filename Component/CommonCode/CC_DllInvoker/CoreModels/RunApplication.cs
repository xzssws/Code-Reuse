using CC_DllInvoker.Extends;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC_DllInvoker
{
    /// <summary>
    /// 可执行应用程序
    /// </summary>
    [Serializable]
    public class RunApplication : BaseApplication
    {
        #region 字段定义

        #region 内部

        /// <summary>
        /// 函数库调用实例
        /// </summary>

        private OneDLL DLL;

        /// <summary>
        /// 描述DLL中方法配置集合
        /// </summary>
        public List<OneMethod> Methods { get; set; }

        /// <summary>
        /// 程序运行状态
        /// </summary>
        private bool runFlag = false;

        #endregion 内部

        #endregion 字段定义

        #region 属性定义

        #region 绑定属性

        /// <summary>
        /// 应用程序实例
        /// <para>关联属性: Instances</para>
        /// </summary>

        private List<OneInstance> _instances;

        /// <summary>
        /// 应用程序实例
        /// </summary>
        public override List<OneInstance> Instances
        {
            get { return _instances; }
            set
            {
                _instances = value;
                OnPropertyChanged("Instances");
            }
        }

        #endregion 绑定属性

        #endregion 属性定义

        #region 方法定义

        #region 公开方法

        /// <summary>
        /// 编译该应用程序
        /// </summary>
        public void Build()
        {
            #region 预制方法

            this.Methods = new List<OneMethod>();
            foreach (var item in this.Instances)
            {
                //初始化方法
                item.InitMethod = new OneMethod(string.Format(ConstData.STR, item.Name, ConstData.INIT, item.ID), OneExtend.GetParaTypes(item));
                //启动方法
                item.RunMethod = new OneMethod(string.Format(ConstData.STR, item.Name, ConstData.RUN, item.ID), OneExtend.GetRunTypes(item));
                //设置参数方法
                if (item.Parameters.Count != 0)
                {
                    item.SetParameterMethod = new OneMethod(item.Name + ConstData.@SETP + item.ID, OneExtend.GetRunTypes(item));
                    this.Methods.Add(item.SetParameterMethod);
                }
                //构造方法
                item.GetBufferSizeMethod = new OneMethod(string.Format(ConstData.STR, item.Name, ConstData.GETB, item.ID), typeof(IntPtr));
                item.SaveStateMethod = new OneMethod(string.Format(ConstData.STR, item.Name, ConstData.SAVE, item.ID), typeof(Byte[]));
                item.LoadStateMethod = new OneMethod(string.Format(ConstData.STR, item.Name, ConstData.LOAD, item.ID), typeof(Byte[]));
                //加入方法
                this.Methods.Add(item.GetBufferSizeMethod);
                this.Methods.Add(item.SaveStateMethod);
                this.Methods.Add(item.LoadStateMethod);
                this.Methods.Add(item.InitMethod);
                this.Methods.Add(item.RunMethod);
            }

            #endregion 预制方法

            #region 核心生成

            if (this.DLL != null) this.DLL.UnLoadDll();
            this.DLL = new OneDLL();
            this.DLL.LoadDll(this.AppPath);
            foreach (OneMethod item in this.Methods)
            {
                item.Method = this.DLL.BuildFunction(item.MethodName, item.ParameterTypes, item.ReturnType);
            }

            #endregion 核心生成
        }

        /// <summary>
        /// 初始化应用程序
        /// </summary>
        public void Init()
        {
            try
            {
                foreach (var item in Instances)
                {
                    item.Init();
                }
            }
            catch (Exception ex)
            {
                this.ExceptionState = true;
                this.ExceptionInfo = ex.Message;
            }
        }

        /// <summary>
        /// 设置可调参数
        /// </summary>
        /// <param name="instance">实例名称</param>
        /// <param name="id">实例ID</param>
        /// <param name="paramlist">参数列表</param>
        public void SetParameter(string instance, string id, Dictionary<string, string> paramlist)
        {
            var ins = Instances.FirstOrDefault(t => t.Name == instance && t.ID.ToString() == id);
            List<object> l = new List<object>();
            foreach (var item in ins.Parameters)
            {
                if (item.ParameterType.ToLower() == "variable")
                {
                    switch (item.Type)
                    {
                        case "I32":
                            l.Add(Convert.ToInt32(item.Value));
                            break;

                        case "D8":
                            l.Add(Convert.ToBoolean(item.Value));
                            break;

                        case "R32":
                            l.Add(Convert.ToSingle(item.Value));
                            break;

                        default:
                            throw new Exception("解析类型出错" + item.Type);
                    }
                }
            }
            ins.SetParameter(l);
        }

        #endregion 公开方法

        #endregion 方法定义
    }
}