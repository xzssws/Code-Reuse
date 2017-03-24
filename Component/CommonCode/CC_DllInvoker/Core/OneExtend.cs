using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace CC_DllInvoker.Extends
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class OneExtend
    {
        #region 静态方法

        /// <summary>
        /// 结构体转换成句柄
        /// </summary>
        /// <param name="structobj">结构体对下岗呢</param>
        /// <returns>句柄</returns>
        public static IntPtr GetIntptr(this object structobj)
        {
            IntPtr sp = Marshal.AllocCoTaskMem(Marshal.SizeOf(structobj));
            Marshal.StructureToPtr(structobj, sp, false);
            return sp;
        }

        /// <summary>
        /// 结构体的句柄转换为对象
        /// </summary>
        /// <typeparam name="T">结构体类型</typeparam>
        /// <param name="handle">句柄</param>
        /// <returns>结果提对象</returns>
        public static T GetStruct<T>(this IntPtr handle)
        {
            return (T)Marshal.PtrToStructure(handle, typeof(T));
        }

        /// <summary>
        /// 获得结构体
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="type">结构类型</param>
        /// <returns></returns>
        public static object GetStruct(this IntPtr handle, Type type)
        {
            return Marshal.PtrToStructure(handle, type);
        }

        /// <summary>
        /// 根据实例获取实例的参数列表中的数据类型
        /// </summary>
        /// <param name="typename">类型名称 I32：int D8：bool ASIG：ASIG DSIG：DSIG R32：Float</param>
        /// <param name="isOutput">是否输入参数</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">发生错误不存在该类型 + typename</exception>
        public static Type GetDataType(string typename, bool isOutput = false)
        {
            if (isOutput)
            {
                return typeof(IntPtr);
            }
            switch (typename)
            {
                case "I32":
                    return typeof(int);

                case "D8":
                    return typeof(bool);

                case "ASIG":
                    return typeof(ASIG);

                case "DSIG":
                    return typeof(DSIG);

                case "R32":
                    return typeof(float);

                default:
                    throw new Exception("发生错误不存在该类型" + typename);
            }
        }

        /// <summary>
        /// 获得应用实例中主运行函数的参数列表
        /// </summary>
        /// <param name="instance">应用实例</param>
        /// <returns>
        /// 参数类型列表
        /// </returns>
        public static Type[] GetRunTypes(OneInstance instance)
        {
            Type[] types = new Type[instance.Inputs.Count + instance.Outputs.Count];
            int i = 0;
            foreach (var item in instance.Inputs)
            {
                types[i] = typeof(IntPtr);
                i++;
            }
            foreach (var item in instance.Outputs)
            {
                string typename = item.Type;
                types[i] = typeof(IntPtr);
                i++;
            }
            return types;
        }

        /// <summary>
        /// 获取应用实例中可调和不可调参数的参数类型列表
        /// </summary>
        /// <param name="instance">应用实例</param>
        /// <returns>
        /// 参数类型列表
        /// </returns>
        [field: NonSerializedAttribute()]
        public static Type[] GetParaTypes(OneInstance instance)
        {
            Type[] types = new Type[instance.Parameters.Count(t => t.ParameterType.ToLower() == "variable" || t.ParameterType.ToLower() == "fixed")];

            int i = 0;

            foreach (var item in instance.Parameters)
            {
                if (item.ParameterType.ToLower() == "variable" || item.ParameterType.ToLower() == "fixed")
                {
                    types[i] = GetDataType(item.Type);
                    i++;
                }
            }
            return types;
        }

        #endregion 静态方法
    }
}