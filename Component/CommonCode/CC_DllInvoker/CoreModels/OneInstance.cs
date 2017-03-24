using CC_DllInvoker.Extends;
using System;
using System.Collections.Generic;

namespace CC_DllInvoker
{
    /// <summary>
    /// 功能实例
    /// </summary>
    [Serializable]
    public partial class OneInstance
    {
        #region 字段定义

        #region 公开

        /// <summary>
        /// 运行锁
        /// </summary>
        public static object lockObj = 0;

        #endregion 公开

        #region 内部

        /// <summary>
        /// 状态缓存区
        /// </summary>
        private byte[] StateBuffer;

        #endregion 内部

        #endregion 字段定义

        #region 方法定义

        #region 公开

        /// <summary>
        /// 计算方法
        /// </summary>
        public void Start()
        {
            lock (lockObj)
            {
                //判断是否初始化
                if (!this.IsInited) this.Init();
                //参数列表个数
                object[] objs = new object[this.Inputs.Count + this.Outputs.Count];
                //读取点
                //ReadPoint(InputPoints, objs);
                this.Run(objs);
                //写入点
                //WritePoint(OutputPoints, objs);
            }
        }

        /// <summary>
        /// 写入点 仅限计算使用
        /// </summary>
        /// <param name="OutputPoints">The output points.</param>
        /// <param name="objs">The objs.</param>
        private void WritePoint(Dictionary<string, DataPoint> OutputPoints, object[] objs)
        {
            List<DataPoint> OutputValue = new List<DataPoint>();

            for (int j = Inputs.Count; j < Inputs.Count + Outputs.Count; j++)
            {
                int count = j - Inputs.Count;
                switch (Outputs[count].Type)
                {
                    case "I32":
                        IntPtr IValue = (IntPtr)objs[j];
                        int ivalue = IValue.GetStruct<Int32>();
                        OutputValue.Add(new DataPoint { ID = OutputPoints[Outputs[count].SignalName].ID, Name = Outputs[count].SignalName, Type = 0, Value = ivalue });
                        break;

                    case "D8":
                        IntPtr DValue = (IntPtr)objs[j];
                        bool dvalue = DValue.GetStruct<bool>();
                        OutputValue.Add(new DataPoint { ID = OutputPoints[Outputs[count].SignalName].ID, Name = Outputs[count].SignalName, Type = 1, Value = Convert.ToSingle(dvalue) });
                        break;

                    case "R32":
                        IntPtr RValue = (IntPtr)objs[j];
                        float rvalue = RValue.GetStruct<float>();
                        OutputValue.Add(new DataPoint { ID = OutputPoints[Outputs[count].SignalName].ID, Name = Outputs[count].SignalName, Type = 2, Value = rvalue });
                        break;

                    case "ASIG":
                        IntPtr ASIGValue = (IntPtr)objs[j];
                        ASIG asigValue = ASIGValue.GetStruct<ASIG>();
                        OutputValue.Add(new DataPoint { ID = OutputPoints[Outputs[count].SignalName].ID, Name = Outputs[count].SignalName, Qualitycode = asigValue.quality.ToString(), Type = 3, Value = asigValue.value, Reason_dcs = asigValue.reason_dcs, UpdateTime = DateTime.Now.Ticks });
                        break;

                    case "DSIG":
                        IntPtr DSIGValue = (IntPtr)objs[j];
                        DSIG dsigValue = DSIGValue.GetStruct<DSIG>();
                        OutputValue.Add(new DataPoint { ID = OutputPoints[Outputs[count].SignalName].ID, Name = Outputs[count].SignalName, Qualitycode = dsigValue.quality.ToString(), Type = 4, Value = dsigValue.value ? 1 : 0, Reason_dcs = dsigValue.reason_dcs, UpdateTime = DateTime.Now.Ticks });
                        break;
                }
            }
        }

        /// <summary>
        /// 读取点 仅限计算使用
        /// </summary>
        /// <param name="InputPoints">The input points.</param>
        /// <param name="objs">The objs.</param>
        private void ReadPoint(Dictionary<string, DataPoint> InputPoints, object[] objs)
        {
            int i = 0;
            foreach (var item in Inputs)
            {
                object ovalue = null;
                switch (item.Type)
                {
                    case "I32":
                        ovalue = Convert.ToInt32(InputPoints[item.SignalName].Value);
                        objs[i] = ovalue.GetIntptr();
                        break;

                    case "D8":
                        ovalue = Convert.ToBoolean(InputPoints[item.SignalName].Value);
                        objs[i] = ovalue.GetIntptr();
                        break;

                    case "ASIG":
                        ASIG asig = new ASIG { value = InputPoints[item.SignalName].Value, quality = Convert.ToInt32(InputPoints[item.Name].Qualitycode), reason_dcs = InputPoints[item.Name].Reason_dcs };
                        ovalue = asig;
                        objs[i] = ovalue.GetIntptr();
                        break;

                    case "DSIG":
                        DSIG dsig = new DSIG { value = Convert.ToBoolean(InputPoints[item.Name].Value), quality = Convert.ToInt32(InputPoints[item.Name].Qualitycode), reason_dcs = InputPoints[item.Name].Reason_dcs };
                        ovalue = dsig;
                        objs[i] = ovalue.GetIntptr();
                        break;

                    case "R32":
                        ovalue = Convert.ToSingle(InputPoints[item.SignalName].Value);
                        objs[i] = ovalue.GetIntptr();
                        break;
                }
                i++;
            }
            //给输出参数赋值
            foreach (var item in Outputs)
            {
                object ovalue = null;
                switch (item.Type)
                {
                    case "I32": ovalue = 0; objs[i] = ovalue.GetIntptr(); break;
                    case "D8": ovalue = false; objs[i] = ovalue.GetIntptr(); break;
                    case "ASIG": ASIG asig = new ASIG(); ovalue = asig; objs[i] = ovalue.GetIntptr(); break;
                    case "DSIG": DSIG dsig = new DSIG(); ovalue = dsig; objs[i] = ovalue.GetIntptr(); break;
                    case "R32": ovalue = 0.0F; objs[i] = ovalue.GetIntptr(); break;
                }
                i++;
            }
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <exception cref="System.Exception">解析类型出错 + item.Type</exception>
        public void Init()
        {
            List<object> l = new List<object>();
            foreach (var item in Parameters)
            {
                switch (item.Type)
                {
                    case "I32":
                        l.Add(Convert.ToInt32(item.InitValue));
                        break;

                    case "D8":
                        l.Add(Convert.ToBoolean(item.InitValue));
                        break;

                    case "R32":
                        l.Add(Convert.ToSingle(item.InitValue));
                        break;

                    default:
                        throw new Exception("解析类型出错" + item.Type);
                }
            }
            InitMethod.Invoke(l.ToArray());
            IsInited = true;
        }

        /// <summary>
        /// 保存状态
        /// </summary>
        /// <returns></returns>
        public bool SaveState()
        {
            //获得引用
            IntPtr size = StateBufferSize.GetIntptr();
            //执行函数获得缓存区大小指针
            GetBufferSizeMethod.Invoke(size);
            //获得后将内存地址转化为Int 大小
            StateBufferSize = size.GetStruct<int>();
            //新建缓存区用于存储状态
            StateBuffer = new byte[StateBufferSize];
            //执行保存到缓存区
            SaveStateMethod.Invoke(StateBuffer);

            return true;
        }

        /// <summary>
        /// 加载状态
        /// </summary>
        /// <returns></returns>
        public bool LoadState()
        {
            //如果当前缓存区对象不为空
            if (StateBuffer != null)
            {
                //加载缓存区
                LoadStateMethod.Invoke(StateBuffer);
                return true;
            }
            return false;
        }

        #endregion 公开

        #region 预定函数实例

        /// <summary>
        /// 初始化方法实例
        /// </summary>
        internal OneMethod InitMethod { get; set; }

        /// <summary>
        /// 计算方法实例
        /// </summary>
        internal OneMethod RunMethod { get; set; }

        /// <summary>
        /// 设置参数方法实例
        /// </summary>
        internal OneMethod SetParameterMethod { get; set; }

        /// <summary>
        /// 保存状态方法实例
        /// </summary>
        internal OneMethod SaveStateMethod { get; set; }

        /// <summary>
        /// 加载状态方法实例
        /// </summary>
        internal OneMethod LoadStateMethod { get; set; }

        /// <summary>
        /// 获得缓冲区大小方法实例
        /// </summary>
        internal OneMethod GetBufferSizeMethod { get; set; }

        #endregion 预定函数实例

        #region 封装的函数实例执行方法

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="param">执行参数</param>
        /// <returns>
        /// 参数返回值
        /// </returns>
        public object Run(params object[] param)
        {
            object result = RunMethod.Invoke(param);
            return result;
        }

        /// <summary>
        /// 设置可调节参数
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// 是否设置成功
        /// </returns>
        public bool SetParameter(params object[] param)
        {
            try
            {
                SetParameterMethod.Invoke(param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 获得函数执行状态
        /// </summary>
        /// <returns>
        /// 状态
        /// </returns>
        public string GetState()
        {
            string state = "";
            IntPtr p_state = state.GetIntptr();
            GetBufferSizeMethod.Invoke(p_state);
            string result = p_state.GetStruct<string>();
            return result;
        }

        #endregion 封装的函数实例执行方法

        #endregion 方法定义

        #region 属性定义

        /// <summary>
        /// 实例名称(方法名称)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 周期
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// 缓存区大小
        /// </summary>
        public int StateBufferSize { get; set; }

        /// <summary>
        /// 是否进行初始化了
        /// </summary>
        public bool IsInited { get; set; }

        /// <summary>
        /// 应用程序ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool WithState { get; set; }

        /// <summary>
        /// 输入参数集合
        /// </summary>
        public List<IOneParameter> Inputs { get; set; }

        /// <summary>
        /// 输出参数集合
        /// </summary>
        public List<IOneParameter> Outputs { get; set; }

        /// <summary>
        /// 参数集合
        /// </summary>
        public List<IOneParameter> Parameters { get; set; }

        /// <summary>
        /// 模块集合
        /// </summary>
        public List<OneModule> Modules { get; set; }

        #endregion 属性定义
    }
}