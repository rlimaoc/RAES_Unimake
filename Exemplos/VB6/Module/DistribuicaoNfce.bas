Attribute VB_Name = "DistribuicaoNfce"
Option Explicit

Public Sub XMLDistribuicaoNFCe()
'---------------------------------------------------------------------------------------------------------------
'-------------------------------------
' Resgatar o XML guardado
'-------------------------------------
    Dim doc As New DOMDocument60
    Dim xmlDistrib
    doc.Load "e:\baseswin\ferreira\xmlnfce\xmlvalidar\Validado\31220525975590000107650010000723331000735927-nfe.xml"

    Dim xmlNFe

    Set xmlNFe = CreateObject("Uni.Business.DFe.Xml.NFe.EnviNFe")
    xmlNFe.Versao = "4.00"
    xmlNFe.IdLote = "000000000000001"
    xmlNFe.IndSinc = 1
    xmlDistrib = doc.xml
    xmlNFe.AddNFe GetFromFileNFe()

    
'---------------------------------------------------------------------------------------------------------------
    '-------------------------------------
    ' Criar o XML da consulta situa��o da NFe
    '-------------------------------------
    Dim xmlConsSit
    Set xmlConsSit = CreateObject("Uni.Business.DFe.Xml.NFe.ConsSitNFe")
    xmlConsSit.Versao = "4.00"
    xmlConsSit.TpAmb = 2
    xmlConsSit.ChNFe = "31220525975590000107650010000723331000735927"    '"41200106117473000150550010000606641403753210"


'---------------------------------------------------------------------------------------------------------------
    '-------------------------------------
    ' Criar uma configura��o b�sica para efetuarmos a consulta situa��o da NFe pela sua chave.
    '-------------------------------------
    Dim localConfig
    Set localConfig = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
    localConfig.TipoDFe = 1
    localConfig.CSC = "26dfd008794772f3d44dbe7626d1088d"
    localConfig.CSCIDToken = 1
    localConfig.CodigoUF = 31
    localConfig.TipoEmissao = 1
    Dim certificado
    Set certificado = SelecionarCertificado.SelecionarCertificado
    localConfig.CertificadoDigital = certificado


'---------------------------------------------------------------------------------------------------------------
    '-------------------------------------
    'Criar o servi�o para consumir o servi�o de consulta protocolo
    '-------------------------------------
    Dim consultaProtocolo
    Set consultaProtocolo = CreateObject("Uni.Business.DFe.Servicos.NFCe.ConsultaProtocolo")
    consultaProtocolo.Executar (xmlConsSit), (localConfig)

'---------------------------------------------------------------------------------------------------------------

    '-------------------------------------
    'Criar objeto para consumir o servi�o de envio da NFe para finalizar a nota gerando o arquivo de distribui��o
    '-------------------------------------

    If (consultaProtocolo.result.CStat = 100) Then
        Dim autorizacao, RetConsReciNFe
        Set autorizacao = CreateObject("Uni.Business.DFe.Servicos.NFCe.Autorizacao")
        autorizacao.SetXMLConfiguracao (xmlNFe), (localConfig)
        RetConsReciNFe = Null
        autorizacao.AddRetConsSitNFes (consultaProtocolo.result)
        autorizacao.GravarXmlDistribuicao ("E:\Baseswin\Ferreira\Verify\")
    End If

End Sub
