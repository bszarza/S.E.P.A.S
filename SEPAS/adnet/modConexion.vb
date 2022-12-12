Imports Microsoft.VisualBasic
Imports System.IO
Imports System
Imports System.Xml
Imports System.Data
Imports System.Collections.Specialized
'Imports SvrDatosSeg
'Imports ENCR
Imports AdNet
Module modConexion

    Public Function ObtenerStringConexion(ByVal strConexion As String) As String
        Dim _EX As String = "AdNet-0001"    ' PARA DEVOLVER ERROR Y SABER DONDE SE PRODUCE.

        'DESENCRIPTAR XML
        Dim xml_tmp As String = ""
        Try
            Dim rootPath As String = ""
            rootPath = System.AppDomain.CurrentDomain.BaseDirectory   'root web

            ml_tmp = ObtenerStringDeArchivoXML(rootPath & "ccss\ccss.xml", strConexion, "ccss")  'ccss NODO XML donde está la connectionstring
            'xml_tmp = IS_SEG.seg.DesCrypta(xml_tmp)  'devuelvo la connectionstring desencriptada
            xml_tmp = Encryption.Decrypt(xml_tmp)
        Catch ex As Exception
            Throw New Exception(_EX & "/" & ex.Message & "/revise datos de conexion.")  'si se produce un error, devuelvo en _EX el codigo para identificar donde fue el error (ej: SVR001, para saber que fue aqui)
        End Try
        Return xml_tmp
    End Function
    Public Function ObtenerStringWebConfig(ByVal nombreCookie As String, ByRef appSettings As NameValueCollection) As String
        Dim _EX As String = "Error de conección desde webconfig"    ' PARA DEVOLVER ERROR Y SABER DONDE SE PRODUCE.

        'DESENCRIPTAR XML
        Dim cadenaConexion As String = ""
        Try

            cadenaConexion = Encryption.SelfDecrypt(appSettings(nombreCookie))
        Catch ex As Exception
            Throw New Exception(_EX & "/" & ex.Message & "/revise datos de conexion.")  'si se produce un error, devuelvo en _EX el codigo para identificar donde fue el error (ej: SVR001, para saber que fue aqui)
        End Try
        Return cadenaConexion
    End Function
    Public Function ObtenerStringDeArchivoXML(ByVal strPath As String, ByVal strAplicacion As String, ByVal strParametro As String) As String
        Dim oDom As New XmlDocument()
        Dim ds As New DataSet
        Dim pepe As String = ""  ' = "bwH2a9YszWqLFV7b8CKdUXx5NzcRXXqEmKjjxB8jigKRDLX8r6S6Qu3lWCfnS9+Cn3zbRxr+nYWr1WSZC/1Sydv92z9W/g4JGkalSyOT24t8Rc4fpgxRrH6LaFs7ZaaPrDDruv0Aa9DzsE/9ypLLd2CGVnfxp7Ll4v63XLl8gUA="
        Try
            oDom.Load(strPath)
        Catch ex As Exception
        End Try
        Return oDom.SelectSingleNode("/aplicaciones/" & strAplicacion & "/" & strParametro).InnerText
        'ds = oDom.ReadNode( strPath)

        'Try
        '    Dim doc As New XmlDocument()
        'doc.Load(strPath)
        'Dim child As XmlNode = doc.SelectSingleNode("/aplicaciones/" & strAplicacion & "/" & strParametro)
        'If Not (child Is Nothing) Then
        ' pepe = child.InnerText
        ' End If
        'Dim nr As New XmlNodeReader(child)
        'While nr.Read()
        ''Debug.Print(nr.Name)

        'Debug.Print(nr.Value)
        'Return nr.Value
        'End While
        'End If

        'pepe = oDom.SelectSingleNode("/aplicaciones/" & strAplicacion & "/" & strParametro).InnerText.ToString
        'pepe = oDom.ReadNode(("/aplicaciones/" & strAplicacion & "/" & strParametro).InnerText.ToString

        'Catch ex As Exception
        'End Try
        'oDom = Nothing
        'Return pepe
    End Function





End Module
