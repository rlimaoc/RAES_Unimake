#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Uni.NFCom.Servicos.Enums;

namespace Uni.NFCom.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.NFCom.Xml.NFCom.RetConsReciNFCom")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlRoot("retConsReciNFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class RetConsReciNFCom : XMLBase
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlIgnore]
        public UFBrasil CUF { get; set; }

        [XmlElement("cUF")]
        public int CUFField
        {
            get => (int)CUF;
            set => CUF = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }

        [XmlElement("verAplic")]
        public string VerAplic { get; set; }

        [XmlElement("cStat")]
        public int CStat { get; set; }

        [XmlElement("xMotivo")]
        public string XMotivo { get; set; }

        [XmlElement("protNFCom")]
        public List<ProtNFCom> ProtNFCom { get; set; }

#if INTEROP

        /// <summary>
        /// Retorna o elemento da lista ProtNFCom (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista ProtNFCom)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da ProtNFCom</returns>
        public ProtNFCom GetProtNFCom(int index)
        {
            if ((ProtNFCom?.Count ?? 0) == 0)
            {
                return default;
            };

            return ProtNFCom[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista ProtNFCom
        /// </summary>
        public int GetProtNFComCount => (ProtNFCom != null ? ProtNFCom.Count : 0);

#endif
    }
}
