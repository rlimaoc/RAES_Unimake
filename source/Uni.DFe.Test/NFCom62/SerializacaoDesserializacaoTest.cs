using System.IO;
using System.Xml;
using Uni.Business.DFe.Utility;
using Uni.Business.DFe.Xml.NFCom;
using Xunit;

namespace Uni.DFe.Test.NFCom62
{
    /// <summary>
    /// Testar a serialização e desserialização dos XMLs do NFe
    /// </summary>
    public class SerializacaoDesserializacaoTest
    {
        /// <summary>
        /// Testar a serialização e desserialização do XML ProcEventoNFCom
        /// </summary>
        [Theory]
        [Trait("DFe", "NFCom")]
        [InlineData(@"..\..\..\NFCom62\Resources\procEventoNFCom_110111.xml")]
        public void SerializacaoDesserializacaoProcEventoNFCom(string arqXML)
        {
            Assert.True(File.Exists(arqXML), "Arquivo " + arqXML + " não foi localizado para a realização da serialização/desserialização.");

            var doc = new XmlDocument();
            doc.Load(arqXML);

            var xml = XMLUtility.Deserializar<ProcEventoNFCom>(doc);
            var doc2 = xml.GerarXML();

            Assert.True(doc.InnerText == doc2.InnerText, "XML gerado pela DLL está diferente do conteúdo do arquivo serializado.");
        }

        /// <summary>
        /// Testar a serialização e desserialização do XML RetConsSitNFCom
        /// </summary>
        [Theory]
        [Trait("DFe", "NFCom")]
        [InlineData(@"..\..\..\NFCom62\Resources\retConsSitNFCom.xml")]
        public void SerializacaoDesserializacaoRetConsSitNFCom(string arqXML)
        {
            Assert.True(File.Exists(arqXML), "Arquivo " + arqXML + " não foi localizado para a realização da serialização/desserialização.");

            var doc = new XmlDocument();
            doc.Load(arqXML);

            var xml = XMLUtility.Deserializar<RetConsSitNFCom>(doc);
            var doc2 = xml.GerarXML();

            Assert.True(doc.InnerText == doc2.InnerText, "XML gerado pela DLL está diferente do conteúdo do arquivo serializado.");
        }
    }
}