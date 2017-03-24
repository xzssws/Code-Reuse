using CC_DllInvoker.Extends;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CC_DllInvoker
{
    /// <summary>
    /// 解析器
    /// </summary>
    /// <typeparam name="T">实现IOneParameter接口的类型</typeparam>
    public static class OneResolve<T> where T : IOneParameter, new()
    {
        #region 方法定义

        #region 公开方法

        /// <summary>
        /// 获得应用程序
        /// </summary>
        /// <param name="xmlpath">应用程序配置文件路径</param>
        /// <returns>解析完的应用程序</returns>
        /// <exception cref="System.Exception">解析XML失败： + ex.Message</exception>
        public static BaseApplication GetApplication(string xmlpath)
        {
            try
            {
                RunApplication op = new RunApplication();

                List<OneInstance> ins = new List<OneInstance>();
                //加载文档获取跟节点
                XDocument xe = XDocument.Load(xmlpath);
                XElement @root = xe.Root;
                //获取跟节点属性
                op.MasterStatus = @root.Attribute("MasterStatus").Value;
                op.SlaveStatus = @root.Attribute("SlaveStatus").Value;
                op.ConfigPath = xmlpath;
                op.ConfigFileName = System.IO.Path.GetFileName(op.ConfigPath);
                op.AppAbbreviation = @root.Attribute("Abbreviation").Value;
                op.AppDefined = @root.Attribute("Description").Value;
                op.AppName = @root.Attribute("Name").Value;
                op.AppFileName = @root.Attribute("DLL").Value;

                //获得实例集合
                var @N_instances = @root.Elements("Instance");
                //遍历所有实例
                foreach (var item in @N_instances)
                {
                    OneInstance instance = new OneInstance();
                    //获取Instance实例的属性值
                    instance.WithState = bool.Parse(item.Attribute("WithState").Value);
                    instance.Period = int.Parse(item.Attribute("Cycle").Value);
                    instance.ID = int.Parse(item.Attribute("id").Value);
                    instance.Name = item.Attribute("FuncName").Value;

                    var @N_initPara = item.Element("Parameters").Elements("Parameter");
                    instance.Parameters = GetInitPara(@N_initPara);

                    var @N_modules = item.Element("Modules").Elements("Module");
                    instance.Modules = GetModules(@N_modules, instance.Parameters, instance);

                    //获取输入参数节点并添加到instance对象的输入参数集合
                    var @N_inputs = item.Element("Inputs").Elements("Input");
                    instance.Inputs = GetInputs(@N_inputs);
                    //获取输入参数节点并添加到instance对象的输入参数集合
                    var @N_outputs = item.Element("Outputs").Elements("Output");//获得输出参数集合
                    instance.Outputs = GetOutputs(@N_outputs);

                    ins.Add(instance);
                }
                op.Instances = ins;
                return op;
            }
            catch (Exception ex)
            {
                throw new Exception("解析XML失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 创建接口实例方法
        /// </summary>
        /// <returns>IOneParameter</returns>
        public static IOneParameter CreateParam()
        {
            return new T();
        }

        #endregion 公开方法

        #region 内部方法

        /// <summary>
        /// instance下的Output
        /// </summary>
        /// <param name="N_outputs">输出参数集合</param>
        /// <returns>返回输出参数集合</returns>
        private static List<IOneParameter> GetOutputs(IEnumerable<XElement> @N_outputs)
        {
            List<IOneParameter> outputs = new List<IOneParameter>();
            foreach (var item in @N_outputs)
            {
                IOneParameter output = CreateParam();
                output.Name = item.Attribute("Name").Value;
                output.SignalName = item.Attribute("Name").Value;
                output.Type = item.Attribute("Type").Value;
                output.SourceName = item.Attribute("OutputName").Value;
                output.Value = Activator.CreateInstance(OneExtend.GetDataType(output.Type, true));
                outputs.Add(output);
            }
            return outputs;
        }

        /// <summary>
        /// instance下的Input
        /// </summary>
        /// <param name="N_inputs">输入参数集合</param>
        /// <returns>返回输入参数集合</returns>
        private static List<IOneParameter> GetInputs(IEnumerable<XElement> @N_inputs)
        {
            List<IOneParameter> inputs = new List<IOneParameter>();
            foreach (var item in @N_inputs)
            {
                IOneParameter input = CreateParam();
                input.Name = item.Attribute("Name").Value;
                input.SignalName = item.Attribute("Name").Value;
                input.Type = item.Attribute("Type").Value;
                input.SourceName = item.Attribute("InputName").Value;
                input.Value = Activator.CreateInstance(OneExtend.GetDataType(input.Type), false);
                inputs.Add(input);
            }
            return inputs;
        }

        /// <summary>
        /// 获取instance下的Parameter参数
        /// </summary>
        /// <param name="N_initPara"></param>
        /// <returns></returns>
        private static List<IOneParameter> GetInitPara(IEnumerable<XElement> @N_initPara)
        {
            List<IOneParameter> initParas = new List<IOneParameter>();
            foreach (var item in @N_initPara)
            {
                IOneParameter initPara = CreateParam();
                initPara.Name = item.Attribute("ParameterName").Value;
                initPara.ParameterType = item.Attribute("ParameterType").Value;
                initPara.InitValue = item.Attribute("InitValue").Value;
                initPara.Type = item.Attribute("Type").Value;
                if (item.Attribute("Description").Value != null)
                {
                    initPara.Description = item.Attribute("Description").Value;
                }
                initPara.Value = initPara.InitValue;
                initParas.Add(initPara);
            }
            return initParas;
        }

        /// <summary>
        /// 获取instance内的模块
        /// </summary>
        /// <param name="N_modules"></param>
        /// <returns></returns>
        private static List<OneModule> GetModules(IEnumerable<XElement> @N_modules, List<IOneParameter> Paralist, OneInstance instance = null)
        {
            List<OneModule> modules = new List<OneModule>();
            foreach (var item in @N_modules)
            {
                OneModule mo = new OneModule();
                mo.Name = item.Attribute("ModuleName").Value;
                mo.ID = item.Attribute("InstNumber").Value;
                //获取输入参数节点并添加到instance对象的输入参数集合
                var @N_inputs = item.Element("Inputs").Elements("Input");
                mo.Inputs = new System.Collections.ObjectModel.ObservableCollection<IOneParameter>(GetModuleInputs(@N_inputs));
                //获取出参数节点并添加到instance对象的输入参数集合
                var @N_outputs = item.Element("Outputs").Elements("Output");//获得输出参数集合
                mo.Outputs = new System.Collections.ObjectModel.ObservableCollection<IOneParameter>(GetModuleOutputs(@N_outputs));
                //获取可调参数节点并添加到instance对象的输入参数集合
                var @N_parameters = item.Element("Parameters").Elements("Parameter");//获得设置参数集合
                mo.Parameters = new System.Collections.ObjectModel.ObservableCollection<IOneParameter>(GetModuleParameters(@N_parameters, Paralist));
                modules.Add(mo);
            }
            return modules;
        }

        /// <summary>
        /// 获得所有输入参数
        /// </summary>
        /// <param name="N_Inputs">Parameter节点集合</param>
        /// <returns>输入参数集合</returns>
        private static List<IOneParameter> GetModuleInputs(IEnumerable<XElement> @N_Inputs)
        {
            List<IOneParameter> inputs = new List<IOneParameter>();
            foreach (var item in @N_Inputs)
            {
                IOneParameter input = CreateParam();
                input.Name = item.Attribute("InputName").Value.ToUpper();
                input.Type = item.Attribute("Type").Value;
                input.SourceModule = item.Attribute("SourceModule").Value;
                input.SourceData = item.Attribute("SourceModuleInstNumber").Value;
                input.SourceName = item.Attribute("SourceName").Value;
                input.SignalName = item.Attribute("InputName").Value;
                input.SourceTag = item.Attribute("SourceTag").Value;
                input.Value = Activator.CreateInstance(OneExtend.GetDataType(input.Type), false);
                inputs.Add(input);
            }
            return inputs;
        }

        /// <summary>
        /// 获得所有输出参数
        /// </summary>
        /// <param name="N_Outputs">Parameter节点集合</param>
        /// <returns>输出参数集合</returns>
        private static List<IOneParameter> GetModuleOutputs(IEnumerable<XElement> @N_Outputs)
        {
            List<IOneParameter> outputs = new List<IOneParameter>();
            foreach (var item in @N_Outputs)
            {
                if (item.Attribute("OutputName").Value != "saturated")
                {
                    IOneParameter output = CreateParam();
                    output.Type = item.Attribute("Type").Value;
                    output.Name = item.Attribute("OutputName").Value;
                    output.SignalName = item.Attribute("OutputName").Value;
                    output.TargetName = item.Attribute("TargetName").Value;
                    output.Value = Activator.CreateInstance(OneExtend.GetDataType(output.Type, true));
                    outputs.Add(output);
                }
            }
            return outputs;
        }

        /// <summary>
        /// 获得所有可调不可调参数
        /// </summary>
        /// <param name="N_Parameters">Parameter节点集合</param>
        /// <returns>可调参数集合</returns>
        private static List<IOneParameter> GetModuleParameters(IEnumerable<XElement> @N_Parameters, List<IOneParameter> paralist)
        {
            List<IOneParameter> parameters = new List<IOneParameter>();
            foreach (var item in @N_Parameters)
            {
                IOneParameter parameter = CreateParam();
                parameter.Name = item.Attribute("ParameterName").Value;
                parameter.Type = item.Attribute("Type").Value;
                parameter.ParameterType = item.Attribute("ParameterType").Value;
                parameter.SourceName = item.Attribute("SourceName").Value;
                parameters.Add(parameter);
            }
            return parameters;
        }

        #endregion 内部方法

        #endregion 方法定义
    }
}