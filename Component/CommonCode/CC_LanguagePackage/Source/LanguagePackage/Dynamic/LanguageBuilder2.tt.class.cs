using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguagePackage.Dynamic
{
    /// <summary>
    /// 语言2生成类
    /// </summary>
    public partial class LanguageBuilder2 : ILanguageBuilder
    {
        /// <summary>
        /// 获取或设置语言字典
        /// </summary>
        public string[] Data { get; set; }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns>生成后文本</returns>
        public string Build()
        {
            return this.TransformText();
        }
    }
}
