using System;
using System.Collections.Generic;

namespace CC_DllInvoker
{
    /// <summary>
    /// DLL库描述对象(属性调用部分)
    /// </summary>
    [Serializable]
    public partial class OneDescribe : IDisposable
    {
        #region 字段定义

        /// <summary>
        /// 函数库调用实例
        /// </summary>
        private OneDLL DLL;

        #endregion 字段定义

        #region 属性定义

        /// <summary>
        /// DLL配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DLL函数库路径
        /// </summary>
        public string LibraryPath { get; set; }

        /// <summary>
        /// 描述DLL中方法配置集合
        /// </summary>
        public List<OneMethod> Methods { get; set; }

        #endregion 属性定义

        #region 方法定义

        /// <summary>
        /// 调用已经生成的方法
        /// </summary>
        /// <param name="MethodName">方法名称</param>
        /// <param name="parameters">参数集合(如果没有可以不填)</param>
        /// <returns>方法返回值</returns>

        public virtual object Invoke(string MethodName, params object[] parameters)
        {
            return Methods.Find(t => t.MethodName == MethodName).Invoke(parameters);
        }

        /// <summary>
        /// 调用已经生成的方法
        /// </summary>
        /// <param name="MethodName">方法名称</param>
        /// <param name="parameters">参数集合(如果没有可以不填)</param>
        /// <returns>方法返回值</returns>

        public virtual T InvokeT<T>(string MethodName, params object[] parameters)
        {
            object result = Methods.Find(t => t.MethodName == MethodName).Invoke(parameters);
            return (T)result;
        }

        /// <summary>
        /// 释放该对象
        /// </summary>

        public virtual void Dispose()
        {
            this.DLL.UnLoadDll();
        }

        /// <summary>
        /// 加载对象
        /// </summary>

        public virtual void Compile()
        {
            if (this.DLL != null) this.DLL.UnLoadDll();
            this.DLL = new OneDLL();
            this.DLL.LoadDll(this.LibraryPath);
            foreach (OneMethod item in this.Methods)
            {
                item.Method = this.DLL.BuildFunction(item.MethodName, item.ParameterTypes, item.ReturnType);
            }
        }

        #endregion 方法定义
    }

    /// <summary>
    /// DLL库描述对象(帮助生成部分与对象池)
    /// </summary>
    public partial class OneDescribe
    {
        #region 字段定义

        /// <summary>
        /// OneDescribe 类库描述对象
        /// </summary>
        private static Dictionary<string, OneDescribe> _cacheDescribe = new Dictionary<string, OneDescribe>();

        #endregion 字段定义

        #region 方法定义

        /// <summary>
        /// 获取OneDescribe对象或创建一个只有名称OneDescribe对象
        /// </summary>
        /// <param name="param">Describe对象的名称</param>
        /// <returns>已经存在的Describe,或者一个新的只赋值Name属性的Describe</returns>

        public static OneDescribe GetDescribe(string param)
        {
            //定义对象
            OneDescribe obj;
            //如果对象池中有对象则直接返回
            if (_cacheDescribe.TryGetValue(param, out obj)) return obj;

            //没有则创建
            obj = new OneDescribe();
            //并且在对象池集合中添加该对象
            _cacheDescribe.Add(param, obj);
            //返回
            return obj;
        }

        /// <summary>
        /// 创建配置载体
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="librarypath">函数库路径</param>
        /// <param name="methods">方法描述集合</param>
        /// <returns>运行载体</returns>

        public static OneDescribe LoadDescribe(string name, string librarypath, params OneMethod[] methods)
        {
            OneDescribe Describe = GetDescribe(name);
            Describe.Name = name;
            Describe.LibraryPath = librarypath;
            Describe.Methods = new List<OneMethod>(methods);
            Describe.Compile();
            return Describe;
        }

        /// <summary>
        /// 卸载加载过的Dll
        /// </summary>

        public static void UpLoad()
        {
            foreach (var item in _cacheDescribe.Values)
            {
                item.Dispose();
            }
        }

        /// <summary>
        /// 卸载指定名称的DLL库
        /// </summary>
        /// <param name="Name"></param>

        public static void UpLoad(string Name)
        {
            GetDescribe(Name).DLL.UnLoadDll();
        }

        #endregion 方法定义
    }
}