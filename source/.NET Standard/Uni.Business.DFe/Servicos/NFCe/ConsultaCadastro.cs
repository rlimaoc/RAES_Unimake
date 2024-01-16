﻿#if INTEROP
using System.Runtime.InteropServices;
#endif
using System;
using Uni.Business.DFe.Xml.NFe;
using Uni.Exceptions;

namespace Uni.Business.DFe.Servicos.NFCe
{
    /// <summary>
    /// Enviar o XML de consulta cadastro do contribuinte para o web-service
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Servicos.NFCe.ConsultaCadastro")]
    [ComVisible(true)]
#endif
    public class ConsultaCadastro: NFe.ConsultaCadastro
    {
        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="consCad">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public ConsultaCadastro(ConsCad consCad, Configuracao configuracao) : base(consCad, configuracao) { }

        /// <summary>
        /// Construtor
        /// </summary>
        public ConsultaCadastro() : base() { }

        #endregion Public Constructors

        /// <summary>
        /// Validar o XML
        /// </summary>
        protected override void XmlValidar()
        {
            var validar = new ValidarSchema();
            validar.Validar(ConteudoXML, TipoDFe.NFe.ToString() + "." + Configuracoes.SchemaArquivo, Configuracoes.TargetNS);

            if(!validar.Success)
            {
                throw new ValidarXMLException(validar.ErrorMessage);
            }
        }
    }
}