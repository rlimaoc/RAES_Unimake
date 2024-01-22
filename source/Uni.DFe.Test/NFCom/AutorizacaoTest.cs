using System;
using System.Collections.Generic;
using Uni.Business.DFe.Servicos;
using Uni.Business.DFe.Servicos.NFCom;
using Uni.Business.DFe.Xml.NFCom;
using Xunit;

namespace Uni.DFe.Test.NFCom62
{
    /// <summary>
    /// Testar o serviço de envio da NFCom
    /// </summary>
    public class AutorizacaoTest
    {
        /// <summary>
        /// Enviar uma NFe no modo síncrono somente para saber se a conexão com o webservice está ocorrendo corretamente e se quem está respondendo é o webservice correto.
        /// Efetua o envio por estado + ambiente para garantir que todos estão funcionando.
        /// </summary>
        /// <param name="ufBrasil">UF para onde deve ser enviado a NFe</param>
        /// <param name="tipoAmbiente">Ambiente para onde deve ser enviado a NFe</param>
        [Theory]
        [Trait("DFe", "NFCom")]
        [InlineData(UFBrasil.SP, TipoAmbiente.Homologacao)]
        public void EnviarNFCeSincrono(UFBrasil ufBrasil, TipoAmbiente tipoAmbiente)
        {
            var xml = new NFCom
            {
                InfNFCom = new List<InfNFCom>
                {
                    new InfNFCom
                    {
                        Versao = "1.00",

                        Ide = new Ide
                        {
                            CUF = ufBrasil,
                            TpAmb = tipoAmbiente,
                            Mod = ModeloDFe.NFCom,
                            Serie = 666,
                            NNF = 57962,
                            DhEmi = DateTime.Now,
                            TpEmis = TipoEmissao.Normal,
                            NSiteAutoriz = 0,
                            CMunFG = 3509502,
                            FinNFCom = FinalidadeNFCom.Normal,
                            TpFat = TipoFaturamento.Normal,
                            VerProc = "TESTE 1.00"
                        },
                        Emit = new Emit
                        {
                            CNPJ = "26871414000180",
                            IE = "795796295117",
                            CRT = CRT.SimplesNacional,
                            XNome = "UNIMAKE SOLUCOES CORPORATIVAS LTDA",
                            XFant = "UNIMAKE - PARANAVAI",
                            EnderEmit = new EnderEmit
                            {
                                XLgr = "RUA ANTONIO FELIPE",
                                Nro = "1500",
                                XBairro = "CENTRO",
                                CMun = 3509502,
                                XMun = "Campinas",
                                UF = ufBrasil,
                                CEP = "09097462",
                                Fone = "04431414900"
                            },
                        },
                        Dest = new Dest
                        {
                            XNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                            CNPJ = "02131087000161",
                            IndIEDest = IndicadorIEDestinatario.ContribuinteICMS,
                            IE = "124815618820",
                            IM = "36549",
                            EnderDest = new EnderDest
                            {
                                XLgr = "AVENIDA DE TESTE DE NFE",
                                Nro = "82",
                                XBairro = "CAMPOS ELISEOS",
                                CMun = 3543402,
                                XMun = "RIBEIRAO PRETO",
                                UF = UFBrasil.SP,
                                CEP = "14080000",
                                Fone = "01666994533",
                                Email = "testenfe@hotmail.com"
                            },
                        },
                        Assinante = new Assinante
                        {
                            ICodAssinante = "666",
                            TpAssinante = TipoAssinante.Comercial,
                            TpServUtil = TipoServicoUtilizado.Internet,
                            NContrato = "X666",
                            DContratoIni = new DateTimeOffset(new DateTime(2023, 1, 20)),
                            //DContratoFim = new DateTimeOffset(new DateTime(2024, 1, 19)),
                        },
                        Det = new List<Det> {
                            new Det
                            {
                                NItem = 1,
                                Prod = new Prod
                                {
                                    CProd = "01042",
                                    XProd = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                                    CClass = ClassificacaoItem.ServicoNaoMedidoInternet,
                                    UMed = UnidadeBasicaMedida.GB,
                                    QFaturada = 120,
                                    VItem = 0.7075,
                                    VProd = 84.90,
                                },
                                Imposto = new Imposto
                                {
                                    IndSemCST = 1,
                                    PIS = new PIS
                                    {
                                        CST = "08",
                                        VBC = 0,
                                        PPIS = 0,
                                        VPIS = 0,
                                    },
                                    COFINS = new COFINS
                                    {
                                        CST = "08",
                                        VBC = 0,
                                        PCOFINS = 0,
                                        VCOFINS = 0,
                                    }
                                },
                            }
                        },
                        Total = new Total
                        {
                            VProd = 84.90,
                            ICMSTot = new ICMSTot
                            {
                                VBC = 0,
                                VICMS = 0,
                                VICMSDeson = 0,
                                VFCP =0,
                            },
                            VCOFINS = 0,
                            VPIS = 0,
                            VFUNTTEL = 0,
                            VFUST = 0,
                            VRetTribTot = new VRetTribTot
                            {
                                VRetPIS = 0,
                                VRetCOFINS = 0,
                                VRetCSLL = 0,
                                VIRRF = 0,
                            },
                            VDesc = 0,
                            VOutro = 0,
                            VNF = 89.40,
                        },
                        GFidelidade = new GFidelidade
                        {
                            QtdSaldoPts = "1860",
                            DRefSaldoPts = new DateTimeOffset(new DateTime(2024, 1, 2)),
                            QtdPtsResg = "1500",
                            DRefResgPts = new DateTimeOffset(new DateTime(2023, 12, 20)),
                        },
                        AutXML = new List<AutXML>
                        {
                            new AutXML { CPF = "51939970822" },
                            new AutXML { CPF = "98390968851" },
                            new AutXML { CPF = "57862975863" },
                        },
                        InfAdic = new InfAdic
                        {
                            InfCpl = ";CONTROLE: 0000241197;PEDIDO(S) ATENDIDO(S): 300474;Empresa optante pelo simples nacional, conforme lei compl. 128 de 19/12/2008;Permite o aproveitamento do credito de ICMS no valor de R$ 2,40, correspondente ao percentual de 2,83% . Nos termos do Art. 23 - LC 123/2006 (Resolucoes CGSN n. 10/2007 e 53/2008);Voce pagou aproximadamente: R$ 6,69 trib. federais / R$ 5,94 trib. estaduais / R$ 0,00 trib. municipais. Fonte: IBPT/empresometro.com.br 18.2.B A3S28F;",
                        },
                        GRespTec = new GRespTec
                        {
                            CNPJ = "06117473000150",
                            XContato = "Contato teste para NFCom",
                            Email = "testenfe@gmail.com",
                            Fone = "04431414900"
                        }
                    }
                }
            };


            var configuracao = new Configuracao
            {
                TipoDFe = TipoDFe.NFCom,
                TipoEmissao = TipoEmissao.Normal,
                CertificadoDigital = PropConfig.CertificadoDigital,
                CodigoUF = (int)ufBrasil,
                CSC = "121233",
                CSCIDToken = 1
            };

            var autorizacao = new Autorizacao(xml, configuracao);
            autorizacao.Executar();

            Assert.True(configuracao.CodigoUF.Equals((int)ufBrasil), "UF definida nas configurações diferente de " + ufBrasil.ToString());
            Assert.True(configuracao.TipoAmbiente.Equals(tipoAmbiente), "Tipo de ambiente definido nas configurações diferente de " + tipoAmbiente.ToString());
            Assert.True(autorizacao.Result.CUF.Equals(ufBrasil), "Webservice retornou uma UF e está diferente de " + ufBrasil.ToString());
            Assert.True(autorizacao.Result.TpAmb.Equals(tipoAmbiente), "Webservice retornou um Tipo de ambiente diferente " + tipoAmbiente.ToString());
        }
    }
}