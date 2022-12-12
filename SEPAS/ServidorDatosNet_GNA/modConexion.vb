Imports Microsoft.VisualBasic
Imports System.IO
Imports System
Imports System.Xml
Imports System.Data
Imports System.Collections.Specialized
Imports System.Configuration
'Imports SvrDatosSeg
'Imports ENCR
'Imports AdNet
Module modConexion

    Public Function ObtenerStringConexion_usuario(ByVal strConexion As String) As String
        Dim _EX As String = "AdNet-0002"    ' PARA DEVOLVER ERROR Y SABER DONDE SE PRODUCE.

        'DESENCRIPTAR XML
        Dim xml_tmp As String = ""
        Try
            xml_tmp = strConexion
        Catch ex As Exception
            Throw New Exception(_EX & "/" & ex.Message & "/revise datos de conexion.")  'si se produce un error, devuelvo en _EX el codigo para identificar donde fue el error (ej: SVR001, para saber que fue aqui)
        End Try
        Return xml_tmp
    End Function
    'PARA LEVANTAR DEL WEBCONFIG LA CADENA DE CONEXION
    Public Function ObtenerStringConexion(ByVal strConexion As String) As String
        Return ConfigurationManager.ConnectionStrings(strConexion).ConnectionString.ToString()
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
        'Dim xNode As XmlNode
        Dim _string As String = ""  ' = "bwH2a9YszWqLFV7b8CKdUXx5NzcRXXqEmKjjxB8jigKRDLX8r6S6Qu3lWCfnS9+Cn3zbRxr+nYWr1WSZC/1Sydv92z9W/g4JGkalSyOT24t8Rc4fpgxRrH6LaFs7ZaaPrDDruv0Aa9DzsE/9ypLLd2CGVnfxp7Ll4v63XLl8gUA="
        Try
            oDom.Load(strPath)
            'xNode = oDom.SelectSingleNode("/aplicaciones/" & strAplicacion & "/" & strParametro)
            'xNode = oDom.SelectSingleNode("/aplicaciones/" & strAplicacion & "/" & strParametro"Subject/Items/Item[@id='" & IDToUse & "']")
            'Dim doc As New XmlDocument()
            'doc.Load("books.xml")

            'Display all the book titles.
            Dim elemList As XmlNodeList = oDom.GetElementsByTagName("aplicaciones")
            Dim i As Integer = 0
            For i = 1 To elemList(i).ChildNodes.Count
                Debug.Print(elemList(i).ChildNodes(i).Name.ToString)
                If elemList(i).ChildNodes(i).Name.ToString = strAplicacion Then
                    Debug.Print("nada")
                End If
            Next i

        Catch ex As Exception
        End Try
        Return _string

    End Function


    Public Function ObtenerStringDeArchivoXML3(ByVal strPath As String, ByVal strAplicacion As String, ByVal strParametro As String) As String
        Dim xmldoc As XmlDocument = New XmlDocument()
        ' Dim ConcatString As String = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & "encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?><kml xmlns=" & Chr(34) & "http://www.opengis.net/kml/2.2" & Chr(34) & ">"
        Dim _string As String = ""
        xmldoc.Load(strPath)
        Dim DocumentNodeList As XmlNodeList = xmldoc.GetElementsByTagName("aplicaciones")
        For Each DocumentNode As XmlNode In DocumentNodeList
            'xmldoc.ParentNode.ParentNode.RemoveChild(childnode)
            Dim ChildNodeList As XmlNodeList = DocumentNode.ChildNodes
            For Each ChildNode As XmlNode In ChildNodeList
                'If ChildNode.Name = strAplicacion Then
                If ChildNode.Name.ToUpper.ToString = strAplicacion.ToUpper.ToString Then
                    Dim VNODE As XmlNode = ChildNode.SelectSingleNode("ccss")
                    If VNODE IsNot Nothing Then
                        _string = VNODE.InnerText
                        Exit For
                    End If
                End If
            Next
        Next
        Return _string
    End Function

End Module
