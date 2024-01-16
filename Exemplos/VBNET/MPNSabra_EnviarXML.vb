  Public Sub EnviarNFe()

        Configurar()

        '     AutorizarPorArquivoNFe()

        '     Exit Sub


        Dim Xml = New Uni.Business.DFe.Xml.NFe.EnviNFe
        Dim NFe = New Uni.Business.DFe.Xml.NFe.NFe
        Dim InfNFe = New Uni.Business.DFe.Xml.NFe.InfNFe

        '
        ' Lendo o arquivo WandreyNfe.xml
        '
        Dim Doc = New System.Xml.XmlDocument

        Doc.Load("c:\mpnsabra\wandreynfe.xml")

        With Xml
            .IdLote = "000000000000001"
            .IndSinc = SimNao.Sim
            .Versao = "4.00"

        End With

        MsgBox("Leu o arquivo .xml ")
        Try


            Xml.NFe.Add(Uni.Business.DFe.Utility.XMLUtility.Deserializar(Of Uni.Business.DFe.Xml.NFe.NFe)(Doc))

            '
            '   Neste ponto est� apresentando o erro "Vari�vel de objeto ou vari�vel com bloco n�o definida"
            '
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        MsgBox("N�o Passou da Deserializa��o")




        '
        ' Lendo o arquivo WandreyEnviNfe
        '
        Dim Doc2 = New System.Xml.XmlDocument

        Doc2.Load("c:\mpnsabra\wandreyenvinfe.xml")

        MsgBox("Leu o arquivo .xml ")

        Try


            Xml.NFe.Add(Uni.Business.DFe.Utility.XMLUtility.Deserializar(Of Uni.Business.DFe.Xml.NFe.EnviNFe)(Doc2))

            '
            '   Neste ponto est� apresentando o erro "Vari�vel de objeto ou vari�vel com bloco n�o definida"
            '
            '
            ' Se trocar a instru��o para EnviNFe
            'Xml.EnviNFe.Add(Uni.Business.DFe.Utility.XMLUtility.Deserializar(Of Uni.Business.DFe.Xml.NFe.EnviNFe)(Doc2))
            ' Apresenta erro de n�o existir EnviNFe no XML (XML.EnviNFE)
            '

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


        MsgBox("N�o Passou da Deserializa��o")


End Sub
