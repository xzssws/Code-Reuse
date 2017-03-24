#region 引入名称空间定义

using System;
using System.Collections.Generic;

#endregion 引入名称空间定义

namespace LanguagePackage
{
    /// <summary>
    /// 享元类
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    [Serializable]
    internal class Flyweight<T> : Dictionary<string, T>
    {
        #region 属性定义

        /// <summary>
        /// 获取或设置<see cref="T"/>对象
        /// </summary>
        /// <param name="id">The 编号.</param>
        /// <returns>T.</returns>
        new public T this[string id]
        {
            get
            {
                T oo = default(T);
                if (string.IsNullOrEmpty(id)) return oo;
                base.TryGetValue(id, out oo);
                return oo;
            }
            set
            {
                T oo = default(T);
                if (base.TryGetValue(id, out oo))
                {
                    base[id] = value;
                }
                else
                {
                    this.Add(id, value);
                }
            }
        }

        #endregion 属性定义

        #region 方法定义

        /// <summary>
        /// 获取特定类型值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public T Get<T>(string key) where T : class
        {
            object value = this[key];
            if (value != null && value is T)
            {
                return value as T;
            }
            else
            {
                return null;
            }
        }

        #endregion 方法定义
    }
}