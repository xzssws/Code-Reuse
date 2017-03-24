using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LanguagePackage.Dynamic
{
    /// <summary>
    /// 语言生成工厂类
    /// </summary>
    /// <remarks>
    /// 出于后期对多国语言考虑加入的工厂类
    /// </remarks>
    public class LanguageBuilderFactory
    {
        #region 单例模式

        private LanguageBuilderFactory()
        {

        }

        private static readonly object LockObj = new object();

        private static LanguageBuilderFactory _this;

        /// <summary>
        /// 获得对象实例
        /// </summary>
        public static LanguageBuilderFactory This
        {
            get
            {
                lock (LockObj)
                {
                    if (_this == null)
                    {
                        _this = new LanguageBuilderFactory();
                    }
                    return _this;
                }
            }
        }

        #endregion


        /// <summary>
        /// 创建语言生成器
        /// </summary>
        /// <param name="builderType">生成器类型</param>
        /// <returns>资源字典</returns>
        /// <remarks>
        /// 默认返回生成器1
        /// </remarks>
        public ILanguageBuilder CreateLanguageBuilder(BuilderType builderType)
        {
            switch (builderType)
            {
                case BuilderType.L1:
                    return new LanguageBuilder1();
                case BuilderType.L2:
                    return new LanguageBuilder2();
                default:
                    return new LanguageBuilder1();
            }
        }
    }
    public enum BuilderType
    {
        L1, L2
    }
}
