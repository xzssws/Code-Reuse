using System;
﻿using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OSLibrary.程序集
{
    /// <summary>
    /// 程序集访问器
    /// </summary>
    class AssemblyHelp
    {
        public string AssemblyTitle
        {
            get
            {
                // 获取此程序集上的所有 Title 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // 如果至少有一个 Title 属性
                if (attributes.Length > 0)
                {
                    // 请选择第一个属性
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // 如果该属性为非空字符串，则将其返回
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // 如果没有 Title 属性，或者 Title 属性为一个空字符串，则返回 .exe 的名称
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        /// <summary>
        /// 获取程序集版本
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        /// <summary>
        /// 获取程序集注释
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                // 获取此程序集的所有 Description 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // 如果 Description 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Description 属性，则返回该属性的值
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
        /// <summary>
        /// 获取程序集产品
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                // 获取此程序集上的所有 Product 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // 如果 Product 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Product 属性，则返回该属性的值
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
        /// <summary>
        /// 获取程序集Copyright
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                // 获取此程序集上的所有 Copyright 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // 如果 Copyright 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Copyright 属性，则返回该属性的值
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        /// <summary>
        /// 获取程序集公司
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                // 获取此程序集上的所有 Company 属性
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // 如果 Company 属性不存在，则返回一个空字符串
                if (attributes.Length == 0)
                    return "";
                // 如果有 Company 属性，则返回该属性的值
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        /// <summary>
        /// 根据给出的代码生成程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="codestring">代码</param>
        /// <param name="IsExE">是否可执行</param>
        /// <param name="assemblies">引用程序集路径组</param>
        /// <returns>是否生成成功</returns>
        public static bool BuildCode(string assemblyName, string codestring, bool IsExE, params string[] assemblies)
        {
            CSharpCodeProvider p = new CSharpCodeProvider();
            ICodeCompiler cc = p.CreateCompiler();

            // 设置编译参数
            CompilerParameters options = new CompilerParameters();
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.AddRange(assemblies);
            options.GenerateExecutable = IsExE;
            options.OutputAssembly = assemblyName;

            string code = codestring;
            CodeSnippetCompileUnit cu = new CodeSnippetCompileUnit(code);
            // 开始编译
            CompilerResults cr = cc.CompileAssemblyFromDom(options, cu);

            if (cr.Errors.Count == 0)
            {
                return true;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError error in cr.Errors) sb.AppendFormat("错误编号:{0},错误行号:{1},错误列:{2},错误内容:{3}\n", error.ErrorNumber, error.Column, error.Line, error.ErrorText);
                throw new Exception(sb.ToString());
            }
        }

    }
    
    //调用C函数库帮助
    internal class CDLLInvoke
    {
        private IntPtr farProc = IntPtr.Zero;

        /// <summary>
        /// Loadlibrary 返回的函数库模块的句柄
        /// </summary>
        private IntPtr hModule = IntPtr.Zero;

        /// <summary>
        /// 参数传递方式枚举 ,ByValue 表示值传递 ,ByRef 表示址传递
        /// </summary>
        public enum ModePass
        {
            ByValue = 0x0001,
            ByRef = 0x0002
        }

        /// <summary>
        /// GetProcAddress 返回的函数指针
        /// </summary>
        /// <summary>
        /// 装载 Dll
        /// </summary>
        /// <param name="lpFileName">DLL 文件名 </param>
        public void LoadDll(string lpFileName)
        {
            hModule = LoadLibrary(lpFileName);
            if (hModule == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpFileName + "."));
        }

        public void LoadDll(IntPtr HMODULE)
        {
            if (HMODULE == IntPtr.Zero)
                throw (new Exception(" 所传入的函数库模块的句柄 HMODULE 为空 ."));
            hModule = HMODULE;
        }

        /// <summary>
        /// 获得函数指针
        /// </summary>
        /// <param name="lpProcName"> 调用函数的名称 </param>
        public void LoadFun(string lpProcName)
        {
            // 若函数库模块的句柄为空，则抛出异常
            if (hModule == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            // 取得函数指针
            farProc = GetProcAddress(hModule, lpProcName);
            // 若函数指针，则抛出异常
            if (farProc == IntPtr.Zero)
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
            hModule = LoadLibrary(lpFileName);
            // 若函数库模块的句柄为空，则抛出异常
            if (hModule == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpFileName + "."));
            // 取得函数指针
            farProc = GetProcAddress(hModule, lpProcName);
            // 若函数指针，则抛出异常
            if (farProc == IntPtr.Zero)
                throw (new Exception(" 没有找到 :" + lpProcName + " 这个函数的入口点 "));
        }

        /// <summary>
        /// 卸载 Dll
        /// </summary>
        public void UnLoadDll()
        {
            FreeLibrary(hModule);
            hModule = IntPtr.Zero;
            farProc = IntPtr.Zero;
        }

        /// <summary>
        /// Invokes the specified object array_ parameter.
        /// </summary>
        /// <param name="ObjArray_Parameter">The object array_ parameter.</param>
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
        ///  第  + (i + 1).ToString() +  个参数没有给定正确的传递方式 .
        /// </exception>
        /// <exception cref="System.PlatformNotSupportedException"></exception>
        public object Invoke(object[] ObjArray_Parameter, Type[] TypeArray_ParameterType, ModePass[] ModePassArray_Parameter, Type Type_Return)
        {
            // 下面 3 个 if 是进行安全检查 , 若不能通过 , 则抛出异常
            if (hModule == IntPtr.Zero)
                throw (new Exception(" 函数库模块的句柄为空 , 请确保已进行 LoadDll 操作 !"));
            if (farProc == IntPtr.Zero)
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
                    case ModePass.ByValue:
                        IL.Emit(OpCodes.Ldarg, i);
                        break;

                    case ModePass.ByRef:
                        IL.Emit(OpCodes.Ldarga, i);
                        break;

                    default:
                        throw (new Exception(" 第 " + (i + 1).ToString() + " 个参数没有给定正确的传递方式 ."));
                }
            }

            if (IntPtr.Size == 4)
            {// 判断处理器类型
                IL.Emit(OpCodes.Ldc_I4, farProc.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                IL.Emit(OpCodes.Ldc_I8, farProc.ToInt64());
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

        public static object Invoke(string dll,string methodname,Type[] paramterTypes,object[] paramterValues,ModePass[] paramterModes,Type returnType)
        {
            did d = new did();
            d.LoadFun(dll, methodname);
            object result = d.Invoke(paramterValues, paramterTypes, paramterModes, returnType);
            d.UnLoadDll();
            return result;
        }

        #region 私有函数

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
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>
        /// 原型是 :HMODULE LoadLibrary(LPCTSTR lpFileName);
        /// </summary>
        /// <param name="lpFileName">DLL 文件名 </param>
        /// <returns> 函数库模块的句柄 </returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        #endregion 私有函数
    }
}