using System;
using System.Linq;
using System.Xml;
using static Unimake.Business.DFe.Utility.Converter;

namespace Unimake.Business.DFe.ExtensionsMethods
{
    /// <summary>
    /// Extensões para os nós dos XMLs
    /// </summary>
    public static class XmlNodeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Recupera o valor de um elemento em XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValue<T>(this XmlElement element, string name)
        {
            XmlElement value = element.Cast<XmlElement>().Where(el => el.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (value == null)
            {
                return default;
            }

            return ToAny<T>(value.InnerText);
        }

        #endregion Public Methods
    }
}