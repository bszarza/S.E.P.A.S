
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.IO
Imports System.Web



Module modConexion


    Public Function ObtenerStringConexion(ByVal strConexion As String) As String

        Dim strcon As String


        Dim FilePath As String

        FilePath = System.AppDomain.CurrentDomain.BaseDirectory


        strcon = ObtenerStringDeArchivoXML(FilePath & "Conexion\cnxml.xml", strConexion, "connectionstring")





        Return strcon
    End Function

    Public Function ObtenerStringDeArchivoXML(ByVal strPath As String, _
    ByVal strAplicacion As String, ByVal strParametro As String) As String
        Dim oDom As New XmlDocument()
        oDom.Load(strPath)
        Return oDom.SelectSingleNode("/aplicaciones/" & strAplicacion & "/" & strParametro).InnerText
        oDom = Nothing
    End Function










End Module




