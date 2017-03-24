using System;
using System.Reflection;

namespace CC_DllInvoker
{
    /// <summary>
    /// 方法描述对象
    /// </summary>
    [Serializable]
    public class OneMethod
    {
        #region 构造函数

        /// <summary>
        /// 构建一个方法描述对象
        /// </summary>
        /// <param name="returnType">方法返回值</param>
        /// <param name="name">方法名称</param>
        /// <param name="paramtypes">方法参数类型列表</param>
        public OneMethod(Type returnType, string name, params Type[] paramtypes)
        {
            this.MethodName = name;
            this.ReturnType = returnType;
            this.ParameterTypes = paramtypes;
        }

        /// <summary>
        /// 构建一个方法描述对象
        /// </summary>
        /// <param name="name">方法名称</param>
        /// <param name="paramtypes">方法参数对象</param>
        public OneMethod(string name, params Type[] paramtypes)
        {
            this.MethodName = name;
            this.ParameterTypes = paramtypes;
        }

        #endregion 构造函数

        #region 属性定义

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 方法的所有参数类型(可为空)
        /// </summary>
        public Type[] ParameterTypes { get; set; }

        /// <summary>
        /// 方法返回值类型(可为空)
        /// </summary>
        public Type ReturnType { get; set; }

        /// <summary>
        /// 方法类型
        /// </summary>
        public MethodInfo Method { get; set; }

        #endregion 属性定义

        #region 方法定义

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="parameters">参数值</param>
        /// <returns>返回值</returns>
        public object Invoke(params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0) parameters = new object[] { 0 };
            object ts = Method.Invoke(null, parameters);
            return ts;
        }

        #endregion 方法定义
    }
}