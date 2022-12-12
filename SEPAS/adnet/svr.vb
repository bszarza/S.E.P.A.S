Imports System
Imports System.Diagnostics
Imports System.Threading
Imports System.IO
Imports AdNet.Encryption
Imports System.Reflection
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Globalization
Imports System.Collections.Specialized

'Public MustInherit Class ClaseBaseNet
'Public Tabla As String
'Public FuenteDatos As String
'Public App As String
'Private vusuario As String

Public Class svr

    Public Tabla As String
    Public FuenteDatos As String
    Public App As String
    Private vusuario As String
    Dim strConexion As String = ""
    Protected m_Servidor As Servidor
    Private _webConfig As Boolean
    Private _appSettings As NameValueCollection

    Sub New(ByVal FuenteDatos As String, ByVal Tabla As String, ByVal usuario As String)
        'System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        Me.Tabla = Tabla
        Me.FuenteDatos = FuenteDatos
        strConexion = modConexion.ObtenerStringConexion(FuenteDatos)
        Me.App = App
        vusuario = usuario
        _webConfig = False
    End Sub
    Sub New(ByVal FuenteDatos As String, ByRef appSettings As NameValueCollection, ByVal Tabla As String, ByVal usuario As String)
        'System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        Me.Tabla = Tabla
        Me.FuenteDatos = FuenteDatos
        strConexion = modConexion.ObtenerStringWebConfig(FuenteDatos, appSettings)
        Me.App = App
        vusuario = usuario
        _webConfig = True
        _appSettings = appSettings
    End Sub

    Public co
    Public Enum Tipo_Cmd
        returnID = 2
        executenonquery = 1
        executescalar = 0
    End Enum

    Public Enum Tipo_Borrado
        Logico = 0
        Fisico = 1
    End Enum
    Public ReadOnly Property StringConexion
        Get
            Return strConexion
        End Get

    End Property
    'Sub New(ByVal Servidor As String, ByVal Tabla As String)
    '    _Servidor = Servidor
    '    _Tabla = Tabla
    '    strConexion = modConexion.ObtenerStringConexion(Servidor)

    'End Sub
    Public Function Insert(_obj As Object, ByVal VACIO As Object, ByVal t_cmd As Tipo_Cmd) As Integer
        Dim sql As String = ""
        Dim _nombre As String = ""
        sql = ""
        Dim spa As String = ""
        Dim p As String = "("
        Dim v As String = "("
        Dim b As String = "("
        Dim tipo As String

        Try
            For Each propiedad As PropertyInfo In _obj.GetType.GetProperties()
                Dim var As String
                'Debug.Print(propiedad.Name)
                If propiedad.Name.ToString <> "ID" Then
                    tipo = propiedad.PropertyType.ToString
                    'If compare(propiedad.Name.ToString, propiedad.GetValue(_obj, {}), VACIO) = True Or tipo = "System.Boolean" Then
                    'DETERMINAR TIPO DE ATRIBUTO
                    If EsValorPorDefecto(propiedad.Name.ToString, propiedad.GetValue(_obj, {}), VACIO) Or tipo = "System.Boolean" Or tipo = "System.Integer" Or tipo = "System.Int32" Or tipo = "System.Decimal" Then
                        Select Case tipo
                            Case Is = "System.Int32" : spa = "" : var = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.String" : spa = "'" : var = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.Boolean" : spa = "'" : var = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.DateTime" : spa = "'" : var = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.Decimal" : spa = "" : var = spa & propiedad.GetValue(_obj, {}) & spa
                                var = spa & propiedad.GetValue(_obj, {}).ToString.Replace(",", ".") & spa
                            Case Else : spa = "'" : var = spa & propiedad.GetValue(_obj, {}) & spa
                        End Select
                        p = p & propiedad.Name.ToString & ","

                        v = v & var & ","

                    End If
                End If
            Next
            'v = v & spa & propiedad.GetValue(_obj, {}) & spa & ","


            p = Mid(p, 1, Len(p) - 1) & ")"
            v = Mid(v, 1, Len(v) - 1) & ")"
            'b = Mid(b, 1, Len(b) - 1) & ")"

            sql = "insert into " & Tabla & p & " Values " & v
        Catch ex As Exception
            Throw New ApplicationException("Error al generar Cmd. " & ex.Message)
        End Try
        Return CMD_Execute(strConexion, sql, t_cmd)
    End Function


    Public Function Delete(ByVal ID As Integer, ByVal t_borrado As Tipo_Borrado) As Integer
        Dim sql As String = ""
        sql = ""
        Try
            If t_borrado = Tipo_Borrado.Fisico Then
                sql = "Delete " & Tabla & " where ID =  " & ID
            ElseIf t_borrado = Tipo_Borrado.Logico Then
                sql = "Update " & Tabla & " set FLAG_MOSTRAR=1, FECHA_MODIFICACION = GETDATE() where ID =  " & ID
            End If
        Catch ex As Exception
            Throw New ApplicationException("Error al generar Cmd. " & ex.Message)
        End Try
        Return CMD_Execute(strConexion, sql, Tipo_Cmd.executenonquery)
    End Function


    Public Function ObtenerRegistro(ByVal ID As Integer, ByVal tabla As String) As DataSet
        Dim objConn As String = ""
        Dim ds As New DataSet("ds1")
        Try
            objConn = modConexion.ObtenerStringConexion(FuenteDatos)
            Dim da As New SqlDataAdapter("Select * From " & tabla, objConn)
            'da.FillSchema(ds, SchemaType.Source, "ds")
            da.Fill(ds, "ds")
        Catch ex As Exception
            Throw New ApplicationException("Error ejecución CMD. -" & ex.Message)
        End Try
        Return ds
    End Function

    Public Function Update(_obj As Object, ByVal VACIO As Object, ByVal t_cmd As Tipo_Cmd) As Integer
        Dim sql As String = ""
        Dim _nombre As String = ""
        sql = "update " & Tabla & " " & "  Set "
        Dim spa As String = ""
        Dim p As String = ""
        Dim v As String = ""
        Dim tipo As String
        Dim _ID As Integer = _obj.ID
        Try
            For Each propiedad As PropertyInfo In _obj.GetType.GetProperties()
                tipo = propiedad.PropertyType.ToString
                If propiedad.Name.ToString <> "ID" Then
                    'If compare(propiedad.Name.ToString, propiedad.GetValue(_obj, {}), VACIO) = True Or (tipo = "System.Boolean" Or tipo = "System.Integer" Or tipo = "System.Int32") Then
                    If EsValorPorDefecto(propiedad.Name.ToString, propiedad.GetValue(_obj, {}), VACIO) Or tipo = "System.Boolean" Or tipo = "System.Integer" Or tipo = "System.Int32" Or tipo = "System.Decimal" Then
                        'DETERMINAR TIPO DE ATRIBUTO
                        Select Case tipo
                            Case Is = "System.Int32" : spa = "" : v = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.String" : spa = "'" : v = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.Double" : spa = "'" : v = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.Boolean" : spa = "'" : v = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.DateTime" : spa = "'" : v = spa & propiedad.GetValue(_obj, {}) & spa
                            Case Is = "System.Decimal"
                                spa = "" : v = spa & propiedad.GetValue(_obj, {}) & spa
                                v = spa & propiedad.GetValue(_obj, {}).ToString.Replace(",", ".") & spa
                            Case Else : spa = "'" : v = spa & propiedad.GetValue(_obj, {}) & spa
                                'Dim tmp As Decimal = propiedad.GetValue(_obj, {})
                                'v = spa & propiedad.GetValue(_obj, {}).ToString.Replace(",", ".") & spa
                                'v = spa & propiedad.GetValue(_obj, {}) & spa
                        End Select
                        p = propiedad.Name.ToString

                        sql = sql & p & " = " & v & ","
                    End If
                Else
                    _ID = propiedad.GetValue(_obj, {})
                End If
            Next
            sql = Mid(sql, 1, Len(sql) - 1)
            sql = sql & " where ID = " & _ID
        Catch ex As Exception
            Throw New ApplicationException("Error al generar Cmd. " & ex.Message)
        End Try
        Return CMD_Execute(strConexion, sql, t_cmd)
    End Function


    Private Function EsValorPorDefecto(ByVal _attrib As String, ByVal _value As Object, ByVal VACIO As Object) As Boolean
        'COMPARAR ATRIBUTO+VALOR X ATRIBUTO+VALOR
        'SI <> ENTONCES = TRUE = INSERTAR EN QUERY
        Dim vCompare As Boolean = False
        For Each propiedad As PropertyInfo In VACIO.GetType.GetProperties()
            If (propiedad.Name.ToString) = _attrib Then
                If propiedad.GetValue(VACIO, {}) Is Nothing Then
                    If Not _value Is Nothing Then
                        'ES DISTINTO AL VALOR POR DEFAULT, ENCONTCES TENER EN CUENTA PARA LA QUERY
                        'Return True
                        vCompare = True
                        Exit For
                    End If
                Else
                    If propiedad.GetValue(VACIO, {}) <> _value Then
                        'ES DISTINTO AL VALOR POR DEFAULT, ENCONTCES TENER EN CUENTA PARA LA QUERY
                        'Return True
                        vCompare = True
                        Exit For
                    Else
                        vCompare = False
                        'Return False
                        Exit For
                    End If
                End If


            End If
        Next
        Return vCompare
    End Function




    Private Function CMD_Execute(ByVal Conexion As String, ByVal _query As String, ByVal T_cmd As Tipo_Cmd) As Integer
        Dim ds As New DataSet
        Dim oConTemp = New SqlConnection()
        Dim resp As Integer
        Dim newID As Integer = -1
        Try
            oConTemp.ConnectionString = Conexion
            Dim oCmd As New SqlCommand()
            oCmd.CommandType = CommandType.Text
            oCmd.Connection = oConTemp
            oConTemp.Open()
            'Ejecucion
            If T_cmd = Tipo_Cmd.executenonquery Then  'devuelto 0=OK 1=ERR
                oCmd.CommandText = _query
                resp = oCmd.ExecuteNonQuery()
            ElseIf T_cmd = Tipo_Cmd.executescalar Then
                oCmd.CommandText = _query & "; SELECT CAST(scope_identity() AS int) AS ID;" 'devuelvo ID insertado
                resp = oCmd.ExecuteScalar()
            ElseIf T_cmd = 2 Then
                oCmd.CommandText = _query & "; SELECT CAST(scope_identity() AS int) AS ID;" 'devuelvo ID insertado
                'resp = oCmd.ExecuteScalar
                resp = CInt(oCmd.ExecuteScalar())
            End If
            oConTemp.Close()
        Catch ex As Exception
            oConTemp.Close()
            Throw New ApplicationException("Error ejecución CMD: " & Mid(_query, 1, 10) & "-" & ex.Message)
        Finally
            oConTemp.Close()
        End Try
        Return resp
    End Function

    Public Function Query(ByVal _query As String) As DataTable
        Dim constr As String = strConexion
        Dim dt As New DataTable
        If UCase(_query) = "" Then
            If (InStr(UCase(_query), "DELETE") > 0) Or (InStr(UCase(_query), "TRUNCATE") > 0) Then
                Return dt
                Exit Function
            End If
        End If
        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(_query)
                    cmd.Connection = con
                    con.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        dt.Load(dr)
                    End Using
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            Throw New ApplicationException("Error ejecución query: " & Mid(_query, 1, 10) & "-" & ex.Message)
        End Try
        Return dt
    End Function



    'Sub New(ByVal FuenteDatos As String, ByVal Tabla As String, ByVal usuario As String)
    '    Me.Tabla = Tabla
    '    Me.FuenteDatos = FuenteDatos
    '    Me.App = App
    '    vusuario = usuario
    'End Sub

    ' Sub New(ByVal App As String, ByVal Tabla As String)
    '     Me.Tabla = Tabla
    '     Me.App = App
    ' End Sub

    'Sub New(ByVal oServidor As Servidor, ByVal Tabla As String)
    '   Me.Tabla = Tabla
    '   m_Servidor = oServidor
    'end Sub

    Public Overridable Function Agregar(ByVal ParamArray Parametros() As Object) As Integer
        Dim cant As Integer
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                srv = Servidor.Crear(FuenteDatos, App, _appSettings)
            Else
                srv = m_Servidor
            End If

            cant = srv.EjecutarSP(Tabla & "_IN", Parametros)

            If cant <> 1 Then
                Throw New ApplicationException("No se encontro el Registro buscado")
            End If

            Return Parametros(0)

        Catch ex As Exception
            logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try

    End Function

    Public Overridable Sub Eliminar(ByVal ParamArray Parametros() As Object)
        Dim cant As Integer
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos, App)
                End If
            Else
                srv = m_Servidor
            End If
            cant = srv.EjecutarSP(Tabla & "_EL", Parametros)
            If cant <> 1 Then
                Throw New ApplicationException("No se encontro el Registro buscado")
            End If
        Catch ex As Exception
            logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try

    End Sub

    Public Overridable Sub Modificar(ByVal ParamArray Parametros() As Object)
        Dim cant As Integer
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos, App)
                End If
            Else
                srv = m_Servidor
            End If

            cant = srv.EjecutarSP(Tabla & "_AC", Parametros)
            If cant <> 1 Then
                Throw New ApplicationException("No se encontro el Registro buscado")
            End If
        Catch ex As Exception
            logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try

    End Sub

    Public Overridable Function TraerUno(ByVal ParamArray Parametros() As Object) As System.Data.DataSet

        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos, App)
                End If
            Else
                srv = m_Servidor
            End If

            ds = srv.TraerDatos(Tabla & "_OU", Parametros)
        Catch ex As Exception
            logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try
        Return ds
    End Function

    Public Overridable Function TraerTodos(ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos, App)
                End If
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_OT", Parametros)
        Catch ex As Exception
            logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try
        Return ds
    End Function



    Public Overridable Function Filtrado(ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos, App)
                End If
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_filtrado", Parametros)
        Catch ex As Exception
            logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try
        Return ds
    End Function

    'Ejecuto un stores procedure
    Public Overridable Function EjecutarSp(ByVal sp As String, ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos, App)
                End If
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_" & sp, Parametros)
        Catch ex As Exception
            'logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try
        Return ds
    End Function

    'Ejecuto un stores procedure
    Public Overridable Function EjecutarSpLIBRE(ByVal sp As String, ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos, App)
                End If

            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(sp, Parametros)
        Catch ex As Exception
            logW(FuenteDatos, "usr: " & vusuario & " -> " & ex.Message.ToString, "")
            Throw ex
        End Try
        Return ds
    End Function

    Public Sub log(ByVal ID As String, ByVal msg As String)
        Dim rootPath As String = System.AppDomain.CurrentDomain.BaseDirectory   'root web
        Try
            Using W As StreamWriter = File.AppendText(rootPath & "App_data\log\" & ID & ".txt")
                'W.Write(vbCrLf + "fecha : ")
                W.WriteLine(DateTime.Now.ToString)
                W.WriteLine("msg: " & msg)
                W.WriteLine("-------------------------------")
            End Using
        Catch ex As Exception
        End Try
    End Sub


    Public Sub logW(ByVal Origen As String, ByVal msg As String, ByVal nada As String)
        If Not EventLog.SourceExists(Origen) Then
            EventLog.CreateEventSource(Origen, "Error...")
            Return
        End If

        Dim myLog As New EventLog()
        myLog.Source = Origen
        myLog.WriteEntry(msg)
    End Sub

    Public Function EnCrypta(ByVal cadena As String) As String
        Dim Rsp As String
        Rsp = Encrypt(cadena)
        Return Rsp
    End Function

    Public Function DesCrypta(ByVal cadena As String) As String
        Dim Rsp As String = ""
        Rsp = DesCrypta(cadena)
        Return Rsp
    End Function


End Class
