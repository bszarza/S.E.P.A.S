Imports System.Configuration
Imports System.Xml
Imports System.Collections.Specialized


Public MustInherit Class Servidor


    Protected m_FuenteDatos As String
    Protected m_Conexion As String

    'Esta funcion crea un objeto servidor partiendo de una cadena de conexion
    Public Shared Function Crear(ByVal FuenteDatos As String, Optional cString_de_usuario As String = "") As Servidor
        Dim Tipo As String = "SQLSERVER"

        ''Dim Conexion As String = FuenteDatos
        Dim Conexion As String
        If cString_de_usuario.Length > 0 Then
            Conexion = cString_de_usuario
        Else
            Conexion = modConexion.ObtenerStringConexion(FuenteDatos)
        End If

        Dim srv As Servidor
        Select Case Tipo.ToUpper()
            Case "SQLSERVER"
                srv = New SQLServer(Conexion)
            Case Else
                Throw New ApplicationException("Tipo de Motor no soportado: " & Tipo)
        End Select

        srv.m_FuenteDatos = FuenteDatos
        srv.m_Conexion = Conexion

        Return srv
    End Function

    Public Shared Function Crear(ByVal FuenteDatos As String, ByVal App As String, ByRef appSettings As NameValueCollection) As Servidor

        Dim str As String
        str = modConexion.ObtenerStringWebConfig(FuenteDatos, appSettings)

        Dim Tipo As String = "SQLSERVER"

        Dim Conexion As String = str

        Dim srv As Servidor
        Select Case Tipo.ToUpper()
            Case "SQLSERVER"
                srv = New SQLServer(Conexion)
                'Case "ORACLE"
                '    srv = New Oracle(Conexion)
                'Case "OLEDB"
                '    srv = New OleDB(Conexion)
                'Case "OLEDB"
                '    srv = New OleDB(Conexion)
            Case Else
                Throw New ApplicationException("Tipo de Motor no soportado: " & Tipo)
        End Select

        srv.m_FuenteDatos = FuenteDatos
        srv.m_Conexion = Conexion
        Return srv
    End Function

    Public MustOverride Function EjecutarSP(ByVal Nombre As String,
       ByVal ParamArray Parametros() As Object) As Integer

    Public MustOverride Function TraerDatos(ByVal Nombre As String,
       ByVal ParamArray Parametros() As Object) As System.Data.DataSet

    Public MustOverride Sub IniciarTransaccion()
    Public MustOverride Sub Commit()
    Public MustOverride Sub RollBack()
    Public MustOverride Function EnTransaccion() As Boolean



End Class
