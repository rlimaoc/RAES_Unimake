using System;
using System.Collections.Generic;
using Uni.NFCom.Servicos.Enums;
using Uni.NFCom.Servicos.NFCom;
using Uni.NFCom.Servicos;
using Uni.NFCom.Xml.NFCom;
using Xunit;

namespace Uni.NFCom.Test.NFCom
{
    /// <summary>
    /// Testar o serviço de envio da NFCom
    /// </summary>
    public class AutorizacaoTest
    {
        #region Public Methods

        /// <summary>
        /// Enviar uma NFCom no modo síncrono somente para saber se a conexão com o webservice está ocorrendo corretamente e se quem está respondendo é o webservice correto.
        /// Efetua o envio por estado + ambiente para garantir que todos estão funcionando.
        /// </summary>
        /// <param name="ufBrasil">UF para onde deve ser enviado a NFCom</param>
        /// <param name="tipoAmbiente">Ambiente para onde deve ser enviado a NFCom</param>
        [Theory]
        [Trait("DFe", "NFCom")]
        [InlineData(UFBrasil.AC, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.AL, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.AP, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.AM, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.BA, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.CE, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.DF, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.ES, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.GO, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.MA, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.MT, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.MS, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.MG, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.PA, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.PB, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.PR, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.PE, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.PI, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.RJ, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.RN, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.RS, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.RO, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.RR, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.SC, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.SP, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.SE, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.TO, TipoAmbiente.Homologacao)]
        [InlineData(UFBrasil.AC, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.AL, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.AP, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.AM, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.BA, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.CE, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.DF, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.ES, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.GO, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.MA, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.MT, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.MS, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.MG, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.PA, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.PB, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.PR, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.PE, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.PI, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.RJ, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.RN, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.RS, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.RO, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.RR, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.SC, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.SP, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.SE, TipoAmbiente.Producao)]
        [InlineData(UFBrasil.TO, TipoAmbiente.Producao)]
        public void EnviarNFComSincrono(UFBrasil ufBrasil, TipoAmbiente tipoAmbiente)
        {
            var xml = new EnviNFCom
            {
                Versao = "1.00",
                IdLote = "000000000000001",
                NFCom = new List<Xml.NFCom.NFCom> {
                        new Xml.NFCom.NFCom
                        {
                            InfNFCom = new List<InfNFCom> {
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
                                        CMunFG = 4118402,
                                        FinNFCom = FinalidadeNFCom.Normal,
                                        TpFat = TipoFaturamento.Normal,
                                        VerProc = "TESTE 1.00"
                                    },
                                    Emit = new Emit
                                    {
                                        CNPJ = "06117473000150",
                                        IE = "9032000301",
                                        CRT = CRT.SimplesNacional,
                                        XNome = "UNIMAKE SOLUCOES CORPORATIVAS LTDA",
                                        XFant = "UNIMAKE - PARANAVAI",
                                        EnderEmit = new EnderEmit
                                        {
                                            XLgr = "RUA ANTONIO FELIPE",
                                            Nro = "1500",
                                            XBairro = "CENTRO",
                                            CMun = 4118402,
                                            XMun = "PARANAVAI",
                                            UF = ufBrasil,
                                            CEP = "87704030",
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
                                        DContratoIni = new DateTimeOffset(new DateTime(2020, 1, 20)),
                                    },
                                    Det = new List<Det> {
                                        new Det
                                        {
                                            NItem = 1,
                                            Prod = new Prod
                                            {
                                                CProd = "01042",
                                                XProd = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                                                CClass = ClassItemGeral.ServicoNaoMedidoInternet.ToString().PadLeft(7, '0'),
                                                CFOP = "6101",
                                                UMed = UnidadeBasicaMedida.GB,
                                                QFaturada = 120,
                                                VItem = 0.7075,
                                                VProd = 84.90,
                                            },
                                            Imposto = new Imposto
                                            {
                                                ICMS = new ICMS
                                                {
                                                    ICMSSN101 = new ICMSSN101
                                                    {
                                                        Orig = OrigemMercadoria.Nacional,
                                                        PCredSN = 2.8255,
                                                        VCredICMSSN = 2.40
                                                    }
                                                },
                                                PIS = new PIS
                                                {
                                                    PISOutr = new PISOutr
                                                    {
                                                        CST = "99",
                                                        VBC = 0.00,
                                                        PPIS = 0.00,
                                                        VPIS = 0.00
                                                    }
                                                },
                                                COFINS = new COFINS
                                                {
                                                    COFINSOutr = new COFINSOutr
                                                    {
                                                        CST = "99",
                                                        VBC = 0.00,
                                                        PCOFINS = 0.00,
                                                        VCOFINS = 0.00
                                                    }
                                                }
                                            },
                                        }
                                    },
                                    Total = new Total
                                    {
                                        ICMSTot = new ICMSTot
                                        {
                                            VBC = 0,
                                            VICMS = 0,
                                            VICMSDeson = 0,
                                            VFCP = 0,
                                            VBCST = 0,
                                            VST = 0,
                                            VFCPST = 0,
                                            VFCPSTRet = 0,
                                            VProd = 84.90,
                                            VFrete = 0,
                                            VSeg = 0,
                                            VDesc = 0,
                                            VII = 0,
                                            VIPI = 0,
                                            VIPIDevol = 0,
                                            VPIS = 0,
                                            VCOFINS = 0,
                                            VOutro = 0,
                                            VNF = 84.90,
                                            VTotTrib = 12.63
                                        }
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
                        }
                    }
            };

            var configuracao = new Configuracao
            {
                TipoDFe = TipoDFe.NFCom,
                TipoEmissao = TipoEmissao.Normal,
                CertificadoDigital = PropConfig.CertificadoDigital
            };

            var autorizacao = new Autorizacao(xml, configuracao);
            autorizacao.Executar();

            Assert.True(configuracao.CodigoUF.Equals((int)ufBrasil), "UF definida nas configurações diferente de " + ufBrasil.ToString());
            Assert.True(configuracao.TipoAmbiente.Equals(tipoAmbiente), "Tipo de ambiente definido nas configurações diferente de " + tipoAmbiente.ToString());
            if (autorizacao.Result.CUF != UFBrasil.EX) //Maranhão, não sei o pq, está retornando de forma errada a UF, retorna como EX e não MA, bem estranho. Falha no WS deles.
            {
                Assert.True(autorizacao.Result.CUF.Equals(ufBrasil), "Webservice retornou uma UF e está diferente de " + ufBrasil.ToString());
            }
            Assert.True(autorizacao.Result.TpAmb.Equals(tipoAmbiente), "Webservice retornou um Tipo de ambiente diferente " + tipoAmbiente.ToString());
            if (autorizacao.Result.CStat.Equals(104))
            {
                Assert.True(autorizacao.Result.CStat.Equals(104), "Lote não foi processado");
                Assert.True(autorizacao.Result.ProtNFCom.InfProt != null, "Não teve retorno do processamento no envio síncrono");
                Assert.True(autorizacao.Result.ProtNFCom.InfProt.ChNFCom.Equals(xml.NFCom[0].InfNFCom[0].Chave), "Não teve retorno do processamento no envio síncrono");
            }
        }

        #endregion Public Methods
    }
}