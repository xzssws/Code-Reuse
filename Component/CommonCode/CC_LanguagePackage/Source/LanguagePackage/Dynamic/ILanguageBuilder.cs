
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguagePackage.Dynamic
{
    /// <summary>
    /// 语言生成接口
    /// </summary>
    public interface ILanguageBuilder
    {
        /// <summary>
        /// 获取或设置 语言字典
        /// </summary>
        string[] Data { get; set; }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns>生成后文本</returns>
        string Build();
    }
}
