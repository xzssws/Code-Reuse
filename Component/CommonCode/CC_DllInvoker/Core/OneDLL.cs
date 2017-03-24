using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace CC_DllInvoker
{
    /// <summary>
    /// C函数库调用核心
    /// </summary>
    [Serializable]
    internal class OneDLL
    {
        #region 字段定义

        /// <summary>
        /// 函数句柄
        /// </summary>
        private IntPtr FunctionHandle = IntPtr.Zero;

        /// <summary>
        /// 函数库句柄
        /// </summary>
        private IntPtr ClassHandle = IntPtr.Zero;

        #endregion 字段定义

        #region DLL外部定义

        /// <summary>
        /// 原型是 : BOOL FreeLibrary(HMODULE hModule);
        /// </summary>
        /// <param name="hModule"> 需释放的函数库模块的句柄 </param>
        /// <returns> 是否已释放指定的 Dll</returns>
        [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// 原型是 : FARPROC GetProcAddress(HMODULE hModule, LPCWSTR lpProcName);
        /// </summary>
        /// <param name="hModule"> 包含需调用函数的函数库模块的句柄 </param>
        /// <param name="lpProcName"> 调用函数的名称 </param>
        /// <returns> 函数指针 </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>
        /// 原型是 :HMODULE LoadLibrary(LPCTSTR lpFileName);
        /// </summary>
        /// <param name="lpFileName">DLL 文件名 </param>
        /// <returns> 函数库模块的句柄 </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        #endregion DLL外部定义

        #region 加载卸载DLL

        /// <summary>
        /// GetProcAddress 返回的函数指针
        /// </summary>
        /// <summary>
        /// 装载 Dll
        /// </summary>
        /// <param name="lpFileName">DLL 文件名 </param>
        public void LoadDll(string lpFileName)
        {
            ClassHandle = LoadLibrary(lpFileName);
            if (ClassHandle == IntPtr.Zero)
            {
                int errorid = Marshal.GetLastWin32Error();
                throw (new Exception(" 加载 :" + lpFileName + " 文件错误.错误代码：" + errorid));
            }
        }

        /// <summary>
        /// 装载 Dll
        /// </summary>
        /// <param name="HMODULE">Dll 句柄 </param>
        public void LoadDll(IntPtr HMODULE)
        {
            if (HMODULE == IntPtr.Zero)
                throw (new Exception(" 所传入的函数库模块的句柄 HMODULE 为空 ."));
            ClassHandle = HMODULE;
        }

        /// <summary>
        /// 获得函数指针
        /// </summary>
        /// <param name="lpProcName"> 调用函数的名称 </param>
        public void LoadFun(string lpProcName)
        {
            // 若函数库模块的句柄为空，则抛出异常
            if (ClassHandle == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            // 取得函数指针
            FunctionHandle = GetProcAddress(ClassHandle, lpProcName);
            // 若函数指针，则抛出异常
            if (FunctionHandle == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 "));
        }

        /// <summary>
        /// 获得函数指针
        /// </summary>
        /// <param name="lpFileName"> 包含需调用函数的 DLL 文件名 </param>
        /// <param name="lpProcName"> 调用函数的名称 </param>
        public void LoadFun(string lpFileName, string lpProcName)
        {
            // 取得函数库模块的句柄
            ClassHandle = LoadLibrary(lpFileName);
            // 若函数库模块的句柄为空，则抛出异常
            if (ClassHandle == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpFileName + "."));
            // 取得函数指针
            FunctionHandle = GetProcAddress(ClassHandle, lpProcName);
            // 若函数指针，则抛出异常
            if (FunctionHandle == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 "));
        }

        /// <summary>
        /// 卸载 Dll
        /// </summary>
        public void UnLoadDll()
        {
            FreeLibrary(ClassHandle);
            ClassHandle = IntPtr.Zero;
            FunctionHandle = IntPtr.Zero;
        }

        #endregion 加载卸载DLL

        #region 执行方法

        /// <summary>
        /// Invokes the specified object array_ parameter.
        /// </summary>
        /// <param name="ObjArray_Parameter"></param>
        /// <param name="TypeArray_ParameterType">Type of the type array_ parameter.</param>
        /// <param name="ModePassArray_Parameter">The mode pass array_ parameter.</param>
        /// <param name="Type_Return">The type_ return.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        ///  函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !
        /// or
        ///  函数指针为空 , 请确保已进行 LoadFun 操作 !
        /// or
        ///  参数个数及其传递方式的个数不匹配 .
        /// or
        ///  哪一个参数没有给定正确的传递方式 .
        /// </exception>
        /// <exception cref="System.PlatformNotSupportedException"></exception>
        public object Invoke(object[] ObjArray_Parameter, Type[] TypeArray_ParameterType, ParamMode[] ModePassArray_Parameter, Type Type_Return)
        {
            // 下面 3 个 if 是进行安全检查 , 若不能通过 , 则抛出异常
            if (ClassHandle == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            if (FunctionHandle == IntPtr.Zero)
                throw (new Exception(" 函数指针为空 , 请确保已进行 LoadFun 操作 !"));
            if (ObjArray_Parameter.Length != ModePassArray_Parameter.Length)
                throw (new Exception(" 参数个数及其传递方式的个数不匹配 ."));

            // 下面是创建 MyAssemblyName 对象并设置其 Name 属性
            AssemblyName MyAssemblyName = new AssemblyName();
            MyAssemblyName.Name = "InvokeFun";
            // 生成单模块配件
            AssemblyBuilder MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InvokeDll");
            // 定义要调用的方法 , 方法名为“ MyFun ”，返回类型是“ Type_Return ”参数类型是“ TypeArray_ParameterType ”
            MethodBuilder MyMethodBuilder = MyModuleBuilder.DefineGlobalMethod("MyFun", MethodAttributes.Public | MethodAttributes.Static, Type_Return, TypeArray_ParameterType);
            // 获取一个 ILGenerator ，用于发送所需的 IL
            ILGenerator IL = MyMethodBuilder.GetILGenerator();

            int i;
            for (i = 0; i < ObjArray_Parameter.Length; i++)
            {// 用循环将参数依次压入堆栈
                switch (ModePassArray_Parameter[i])
                {
                    case ParamMode.value:
                        IL.Emit(OpCodes.Ldarg, i);
                        break;

                    case ParamMode.Ref:
                        IL.Emit(OpCodes.Ldarga, i);
                        break;

                    default:
                        throw (new Exception(" 第 " + (i + 1).ToString() + " 个参数没有给定正确的传递方式 ."));
                }
            }

            if (IntPtr.Size == 4)
            {// 判断处理器类型
                IL.Emit(OpCodes.Ldc_I4, FunctionHandle.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                IL.Emit(OpCodes.Ldc_I8, FunctionHandle.ToInt64());
            }
            else
            {
                throw new PlatformNotSupportedException();
            }

            IL.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, Type_Return, TypeArray_ParameterType);
            IL.Emit(OpCodes.Ret); // 返回值
            MyModuleBuilder.CreateGlobalFunctions();
            // 取得方法信息
            MethodInfo MyMethodInfo = MyModuleBuilder.GetMethod("MyFun");

            return MyMethodInfo.Invoke(null, ObjArray_Parameter);// 调用方法，并返回其值
        }

        #endregion 执行方法

        #region 扩展

        /// <summary>
        /// 根据当前Dll生成
        /// </summary>
        /// <param name="FunctionName">方法名称</param>
        /// <param name="parameterType">方法类型数组</param>
        /// <param name="returnType">返回值类型</param>
        /// <returns>方法执行体</returns>
        public MethodInfo BuildFunction(string FunctionName, Type[] parameterType, Type returnType)
        {
            #region 验证块

            // 若函数库模块的句柄为空，则抛出异常
            if (ClassHandle == IntPtr.Zero) throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            //设置当前函数句柄为空句柄
            FunctionHandle = IntPtr.Zero;
            // 取得函数指针
            FunctionHandle = GetProcAddress(ClassHandle, FunctionName);
            // 若函数指针，则抛出异常
            if (FunctionHandle == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + FunctionName + " 这个函数的入口点 "));

            #endregion 验证块

            if (parameterType == null || parameterType.Length < 1)//如果参数为空的话 下面的生成编译会发生异常所以替丁一个默认的值 调用的时候也要有一个默认传递的参数
            {
                parameterType = new Type[] { typeof(int) };
            }

            // 下面是创建 MyAssemblyName 对象并设置其 Name 属性
            AssemblyName MyAssemblyName = new AssemblyName();
            MyAssemblyName.Name = "InvokeFun";
            // 生成单模块配件
            AssemblyBuilder MyAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(MyAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder MyModuleBuilder = MyAssemblyBuilder.DefineDynamicModule("InvokeDll");
            // 定义要调用的方法 , 方法名为“ MyFun ”，返回类型是“ Type_Return ”参数类型是“ TypeArray_ParameterType ”
            MethodBuilder MyMethodBuilder = MyModuleBuilder.DefineGlobalMethod(FunctionName, MethodAttributes.Public | MethodAttributes.Static, returnType, parameterType);
            // 获取一个 ILGenerator ，用于发送所需的 IL
            ILGenerator IL = MyMethodBuilder.GetILGenerator();

            int i;
            for (i = 0; i < parameterType.Length; i++)
            {
                IL.Emit(OpCodes.Ldarg, i);

                #region -- CHANGE : 2014年3月11日10:42:04 由于压入参数地址到栈的方式无法将引用参数正确的传递所以全部改为值类型传递,关于引用类型的传递改为采用Intptr的方式

                /*
                        // 用循环将参数依次压入堆栈
                switch (parameterMode[i])
                {
                    case ParamMode.value:
                        IL.Emit(OpCodes.Ldarg, i);
                        break;

                    case ParamMode.Ref:
                        IL.Emit(OpCodes.Ldarga, i);
                        break;

                    default:
                        throw (new Exception(" 第 " + (i + 1).ToString() + " 个参数没有给定正确的传递方式 ."));
                }
*/

                #endregion -- CHANGE : 2014年3月11日10:42:04 由于压入参数地址到栈的方式无法将引用参数正确的传递所以全部改为值类型传递,关于引用类型的传递改为采用Intptr的方式
            }

            if (IntPtr.Size == 4)
            {// 判断处理器类型
                IL.Emit(OpCodes.Ldc_I4, FunctionHandle.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                IL.Emit(OpCodes.Ldc_I8, FunctionHandle.ToInt64());
            }
            else
            {
                throw new PlatformNotSupportedException();
            }

            IL.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, returnType, parameterType);

            IL.Emit(OpCodes.Ret); // 返回值
            MyModuleBuilder.CreateGlobalFunctions();
            // 取得方法信息
            MethodInfo MyMethodInfo = MyModuleBuilder.GetMethod(FunctionName);
            return MyMethodInfo;
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="dll">Dll路径</param>
        /// <param name="methodname">方法名称</param>
        /// <param name="paramterTypes">参数类型数组(依次)</param>
        /// <param name="paramterValues">参数值数组(参照参数类型数组)</param>
        /// <param name="paramterModes">参数传递方式(参照参数类型数组 依次)</param>
        /// <param name="returnType">返回值类型</param>
        /// <returns>调用方法后的返回值</returns>
        public static object Invoke(string dll, string methodname, Type[] paramterTypes, object[] paramterValues, ParamMode[] paramterModes, Type returnType)
        {
            OneDLL d = new OneDLL();
            d.LoadFun(dll, methodname);
            object result = d.Invoke(paramterValues, paramterTypes, paramterModes, returnType);
            d.UnLoadDll();
            return result;
        }

        #endregion 扩展
    }

    /// <summary>
    /// 参数传递方式枚举
    /// </summary>
    internal enum ParamMode
    {
        /// <summary>
        /// 值传递
        /// </summary>
        value = 0x0001,

        /// <summary>
        /// 引用传递
        /// </summary>
        Ref = 0x0002
    }
}