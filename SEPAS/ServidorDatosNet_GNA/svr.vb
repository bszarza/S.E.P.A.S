Imports System
Imports System.Diagnostics
Imports System.Threading
Imports System.IO
Imports ServidorDatosNet_GNA.Encryption
Imports System.Reflection
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlConnection
Imports System.Globalization
Imports System.Collections.Specialized
Imports System.Collections.Generic
Imports ServidorDatosNet_GNA.Event_Log_Error
Imports System.ComponentModel
Imports System.Security.Principal
Imports System.Runtime.Remoting
Imports System.DirectoryServices.ActiveDirectory
Imports System.Text
Imports System.Data.SqlClient.SqlCommand
Imports System.Collections
Imports System.Net.Mime.MediaTypeNames
Imports ServidorDatosNet_GNA.My.MyProject
Imports System.Dynamic
Imports System.Xml.Serialization

Public Class svr

    Public Tabla As String
    Public FuenteDatos As String
    Public App As String
    Private vusuario As String
    Dim strConexion As String = ""
    Protected m_Servidor As Servidor
    Private _webConfig As Boolean
    Private _appSettings As NameValueCollection
    Const _usr_err_datos As String = "Hubo un error en la conexión a datos."
    Private _ConexionString_de_usuario As String

    Dim cErrorList As New List(Of cError)()

    Sub New(ByVal FuenteDatos As String, ByVal Tabla As String, ByVal usuario As String, Optional Command_TimeOut As Integer = 0)
        Me.Tabla = Tabla
        Me.FuenteDatos = FuenteDatos
        strConexion = modConexion.ObtenerStringConexion(FuenteDatos)
        Me.Command_TimeOut_ = Command_TimeOut
        Me.App = App
        vusuario = usuario
        _webConfig = False
    End Sub
    Sub New(ByVal FuenteDatos As String, ByVal usuario As String, Optional Command_TimeOut As Integer = 0)
        Me.FuenteDatos = FuenteDatos
        strConexion = modConexion.ObtenerStringConexion(FuenteDatos)
        Me.Command_TimeOut_ = Command_TimeOut
        Me.App = App
        vusuario = usuario
        _webConfig = False
    End Sub
    Sub New(ByVal FuenteDatos As String, ByRef appSettings As NameValueCollection, ByVal Tabla As String, ByVal usuario As String, Optional Command_TimeOut As Integer = 0)
        Me.Tabla = Tabla
        Me.FuenteDatos = FuenteDatos
        strConexion = modConexion.ObtenerStringWebConfig(FuenteDatos, appSettings)
        Me.Command_TimeOut_ = Command_TimeOut
        Me.App = App
        vusuario = usuario
        _webConfig = True
        _appSettings = appSettings
    End Sub
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
    Public Property ConexionString_de_usuario() As String
        Get
            Return _ConexionString_de_usuario
        End Get
        Set(ByVal value As String)
            _ConexionString_de_usuario = value
        End Set
    End Property
    Public Property DefaultPropHelper As Object
    Public Function Insert(ByVal oNuevo As Object) As Object
        Dim oClase_base As String = ""
        Dim oClase_nombre As String = ""

        If UCase(oNuevo.GetType.BaseType.Name.ToString) <> "OBJECT" Then
            oClase_base = oNuevo.GetType.BaseType.Name.ToString
            oClase_nombre = oNuevo.GetType.BaseType.Name.ToString
        Else
            oClase_base = oNuevo.GetType.Name.ToString
            oClase_nombre = oNuevo.GetType.Name.ToString
        End If

        Dim propiedad_nombre As String = ""
        Dim propiedad_tipo As String
        Dim propiedad_valor As Object
        Dim sqlCommand As New SqlCommand()
        Dim sqlCommand_text As String = "insert into " & oClase_nombre
        Dim sqlCommand_campos As String = " ("
        Dim sqlCommand_valores As String = " values ("
        Dim _ID As Int32 = 0
        Dim errorMessages As New StringBuilder()
        Try
            Dim t As Type = CType(oNuevo.GetType().UnderlyingSystemType, Type)
            For Each propiedad As PropertyInfo In t.GetProperties(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
                propiedad_nombre = propiedad.Name ' propiedad.Name
                propiedad_tipo = propiedad.PropertyType.Name
                propiedad_valor = Nothing
                Dim Propiedad_tipo_db = TypeToDbType(propiedad_tipo)
                If UCase(propiedad_nombre) <> "ID" Then
                    If IsNothing(propiedad.GetValue(oNuevo, {})) = False Then
                        propiedad_valor = propiedad.GetValue(oNuevo, {})
                        If Not IsNothing(propiedad_valor) Then
                            If InStr(propiedad_tipo, "DateTime") > 0 Then
                                If InStr(propiedad_valor.ToString, "1/1900") > 0 Or InStr(propiedad_valor.ToString, "1/0001") > 0 Then
                                    propiedad_valor = DBNull.Value
                                End If
                            End If
                        Else
                            If InStr(propiedad_tipo, "DateTime") > 0 Then
                                If IsDate(propiedad_valor) Then
                                    propiedad_valor = Convert.ToDateTime(propiedad_valor).ToString("MM/dd/yyyy")
                                End If
                            End If
                        End If
                        sqlCommand.Parameters.AddWithValue("@" & propiedad_nombre, propiedad_valor)
                        sqlCommand_campos += propiedad_nombre & ", "
                        sqlCommand_valores += "@" & propiedad_nombre & ", "
                    Else
                        propiedad_valor = Nothing
                    End If
                End If
            Next
            sqlCommand_campos = Left(sqlCommand_campos, Len(sqlCommand_campos) - 2) & ")"
            sqlCommand_valores = Left(sqlCommand_valores, Len(sqlCommand_valores) - 2) & ")"
            sqlCommand_text += sqlCommand_campos & sqlCommand_valores
            sqlCommand_text += "; Select Scope_Identity()"
            Debug.Print(sqlCommand_text)
            Using connection As New SqlConnection(strConexion)
                sqlCommand.Connection = connection
                sqlCommand.CommandText = sqlCommand_text
                sqlCommand.CommandTimeout = Command_TimeOut_
                connection.Open()
                _ID = sqlCommand.ExecuteScalar()
                If _ID > 0 Then
                    oNuevo = Me.ObtenerRegistro(oNuevo, _ID)
                Else
                    oNuevo = Nothing
                End If
                If connection.State <> ConnectionState.Closed Then
                    connection.Close()
                    connection.Dispose()
                End If
            End Using
        Catch ex As Exception
            Dim oErr As New cError()
            oErr.Exc_Client_msg = "Error al insertar registro."
            oErr.Exc_Message = ex.Message
            oErr.Exc_Source = ex.Source
            Throw oErr
        End Try
        Return oNuevo
    End Function
    Public Function Update(ByVal oNuevo As Object) As Object
        Dim oClase_base As String = oNuevo.GetType.BaseType.Name.ToString
        Dim oClase_nombre As String = oNuevo.GetType.BaseType.Name.ToString

        If UCase(oNuevo.GetType.BaseType.Name.ToString) <> "OBJECT" Then
            oClase_base = oNuevo.GetType.BaseType.Name.ToString
            oClase_nombre = oNuevo.GetType.BaseType.Name.ToString
        Else
            oClase_base = oNuevo.GetType.Name.ToString
            oClase_nombre = oNuevo.GetType.Name.ToString
        End If


        Dim propiedad_nombre As String = ""
        Dim propiedad_tipo As String = ""
        Dim propiedad_valor As Object = Nothing
        Dim sqlCommand_text As String = "UPDATE " & oClase_nombre & " Set "
        Dim sqlCommand_campos As String = " "
        Dim sqlCommand_valores As String = " "
        Dim sqlCommand As New SqlCommand()
        Dim _ID As Integer = 0
        Try
            Dim t As Type = CType(oNuevo.GetType().UnderlyingSystemType, Type)

            For Each propiedad As PropertyInfo In t.GetProperties(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
                propiedad_nombre = propiedad.Name ' propiedad.Name
                propiedad_tipo = propiedad.PropertyType.Name
                propiedad_valor = Nothing
                Dim Propiedad_tipo_db = TypeToDbType(propiedad_tipo)
                If propiedad_nombre <> "ID" Then
                    If IsNothing(propiedad.GetValue(oNuevo, {})) = False Then
                        'tiene valor asignado
                        propiedad_valor = propiedad.GetValue(oNuevo, {})
                        If Not IsNothing(propiedad_valor) Then
                            If InStr(propiedad.ToString, "DateTime") > 0 Then
                                If InStr(propiedad_valor.ToString, "1/1900") > 0 Or InStr(propiedad_valor.ToString, "1/0001") > 0 Then
                                    propiedad_valor = DBNull.Value
                                End If
                            ElseIf InStr(propiedad.ToString, "Boolean") > 0 Then
                                Debug.Print("boolean")
                            End If
                        End If
                        sqlCommand.Parameters.AddWithValue("@" & propiedad_nombre, propiedad_valor)
                        sqlCommand_campos += propiedad_nombre & " =  @" & propiedad_nombre & ", "
                    Else
                        propiedad_valor = Nothing
                    End If
                Else
                    _ID = propiedad.GetValue(oNuevo, {})
                End If
            Next
            sqlCommand_campos = Left(sqlCommand_campos, Len(sqlCommand_campos) - 2)
            sqlCommand_text += sqlCommand_campos & " where ID = " & _ID
            Debug.Print(sqlCommand_text)


            Using connection As New SqlConnection(strConexion)
                sqlCommand.Connection = connection
                sqlCommand.CommandText = sqlCommand_text
                sqlCommand.CommandTimeout = Command_TimeOut_
                connection.Open()
                sqlCommand.ExecuteNonQuery()
                If _ID > 0 Then
                    oNuevo = Me.ObtenerRegistro(oNuevo, _ID)
                Else
                    oNuevo = Nothing
                End If
                If connection.State <> ConnectionState.Closed Then
                    connection.Close()
                    connection.Dispose()
                End If
            End Using
        Catch ex As Exception
            Dim oErr As New cError()
            oErr.Exc_Client_msg = "Error al actualizar registro."
            oErr.Exc_Message = ex.Message
            oErr.Exc_Source = ex.Source
            Throw oErr

        End Try

        Return oNuevo
    End Function
    Private Function Byte2Str(ByVal gByte() As Byte) As String
        Dim gTmp As New System.Text.StringBuilder
        For X As Int32 = 0 To (gByte.Length - 1)
            gTmp.Append(Chr(gByte(X)))
        Next
        Return gTmp.ToString
    End Function
    Public Function Delete(ByVal ID As Integer) As Integer
        Dim sql As String = ""
        sql = "Delete " & Tabla & " where ID =  " & ID
        Dim oCmd As New SqlCommand()
        Dim oConTemp = New SqlConnection()
        Dim resp As Integer
        Try
            oConTemp.ConnectionString = strConexion
            oCmd.CommandType = Data.CommandType.Text
            oCmd.CommandText = sql
            oCmd.Connection = oConTemp
            oConTemp.Open()
            Dim filas As Integer = oCmd.ExecuteNonQuery()
            resp = filas
            oConTemp.Close()
        Catch ex As Exception
            oConTemp.Close()
            Throw New ApplicationException(Err_list("Error al borrar", ex))
        Finally
            oConTemp.Close()
        End Try
        Return resp

    End Function
    Public Function ObtenerRegistro(ByVal _obj As Object, ByVal ID As Integer) As Object
        Dim objConn As String = ""
        Dim ds As New DataSet("ds1")
        Dim oErr As New cError()
        Try
            objConn = modConexion.ObtenerStringConexion(FuenteDatos)
            Dim da As New SqlDataAdapter("Select * From " & Tabla & " where ID = " & ID, objConn)
            da.SelectCommand.CommandTimeout = Command_TimeOut_
            da.Fill(ds, "ds")
            da.Dispose()
            Dim dt_list As New List(Of DataTable)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim obj_campo As String = ""
                Dim obj_tipo As String = ""
                Dim siguiente As Boolean = False

                Dim valor As Object
                Dim t As Type = CType(_obj.GetType().UnderlyingSystemType, Type)
                For Each propiedad As PropertyInfo In t.GetProperties(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
                    For Each col As DataColumn In ds.Tables(0).Columns
                        If col.ColumnName = propiedad.Name Then
                            If Not IsNothing(ds.Tables(0).Rows.Item(0).Item(col.ColumnName)) Then
                                Debug.Print(ds.Tables(0).Rows.Item(0).Item(col.ColumnName).ToString)
                                valor = ds.Tables(0).Rows.Item(0).Item(col.ColumnName)
                                If IsDBNull(valor) Then
                                    valor = Nothing
                                End If
                                propiedad.SetValue(_obj, valor, Nothing)
                                valor = Nothing
                                Exit For
                            End If
                        End If
                        valor = Nothing
                    Next
                Next
            Else
                oErr.Exc_Client_msg = "Registro no encontrado."
                Throw oErr
            End If
            da.Dispose()
            ds = Nothing
            da = Nothing

        Catch ex As Exception
            oErr.Exc_Message = ex.Message
            oErr.Exc_Source = ex.Source
            Throw oErr
        End Try
        Return _obj
    End Function
    Public Shared Function DataTableToList(Of T As {Class, New})(table As DataTable) As List(Of T)
        Try
            Dim list As New List(Of T)()

            For Each row As Object In table.AsEnumerable()
                Dim obj As New T()

                For Each prop As Object In obj.[GetType]().GetProperties()
                    Try
                        Dim propertyInfo As PropertyInfo = obj.[GetType]().GetProperty(prop.Name)
                        propertyInfo.SetValue(obj, Convert.ChangeType(row(prop.Name), propertyInfo.PropertyType), Nothing)
                    Catch
                        Continue For 'For 'Try
                    End Try
                Next

                list.Add(obj)
            Next

            Return list
        Catch
            Return Nothing
        End Try
    End Function
    Private Sub CastData(ByVal dato As DataRow)
        Dim Resp
        Dim _parametro As String = ""
        Dim Tipo As String
        Tipo = dato.GetType().ToString
        Select Case Tipo
            Case = "System.String" : parametro = String.Empty
        End Select
        Resp = IIf(IsDBNull(dato), Nothing, dato)
    End Sub
    Public Shared Sub SetValue(inputObject As Object, propertyName As String, propertyVal As Object)
        Dim type As Type = inputObject.[GetType]()
        Dim propertyInfo As System.Reflection.PropertyInfo = type.GetProperty(propertyName)
        Dim propertyType As Type = propertyInfo.PropertyType
        propertyVal = Convert.ChangeType(propertyVal, propertyType)
        propertyInfo.SetValue(inputObject, propertyVal, Nothing)
    End Sub
    Public Function Update_bak(oNuevo As Object, oActual As Object, ByVal t_cmd As Tipo_Cmd) As Object
        Dim sql As String = ""
        Dim _nombre As String = ""
        sql = "update " & Tabla & " " & vbCrLf & "  Set "
        Dim spa As String = ""
        Dim p As String = ""
        Dim v As String = ""
        Dim Resp As Integer
        Dim tipo As String
        Dim _ID As Integer = oNuevo.ID
        Dim _f As String = ""
        Dim Actualizar As Boolean = False
        Try
            For Each propiedad As PropertyInfo In oNuevo.GetType.GetProperties()
                tipo = propiedad.PropertyType.ToString
                If propiedad.Name.ToString <> "ID" Then
                    If Not IsNothing(propiedad.GetValue(oNuevo, {})) Then
                        _f = propiedad.GetValue(oNuevo, {})
                        If Comparar({propiedad.Name.ToString, _f}, oActual) = False Then
                            Actualizar = True
                            Select Case tipo
                                Case Is = "System.Int32" : spa = "" : v = spa & propiedad.GetValue(oNuevo, {}) & spa
                                Case Is = "System.String" : spa = "'" : v = spa & propiedad.GetValue(oNuevo, {}) & spa
                                Case Is = "System.Double" : spa = "'" : v = spa & propiedad.GetValue(oNuevo, {}) & spa
                                Case Is = "System.Boolean" : spa = "'" : v = spa & propiedad.GetValue(oNuevo, {}) & spa
                                Case Is = "System.DateTime" : spa = "'" : v = spa & propiedad.GetValue(oNuevo, {}) & spa
                                Case Is = "System.Decimal"
                                    spa = "" : v = spa & propiedad.GetValue(oNuevo, {}) & spa
                                    v = spa & propiedad.GetValue(oNuevo, {}).ToString.Replace(",", ".") & spa
                                Case Else : spa = "'" : v = spa & propiedad.GetValue(oNuevo, {}) & spa
                            End Select
                            p = propiedad.Name.ToString

                            sql = sql & p & " = " & v & "," & vbCrLf
                        End If
                    End If
                Else
                    _ID = propiedad.GetValue(oNuevo, {})
                End If
            Next
            sql = Mid(sql, 1, Len(sql) - 3)
            sql = sql & " where ID = " & _ID
            If Actualizar Then
                If Resp > 0 Then
                    oNuevo = Me.ObtenerRegistro(oNuevo, _ID)
                Else
                    oNuevo = Nothing
                End If
            End If
        Catch ex As Exception
            Dim Err_list As String
            Err_list = "Error al retornar registro." & ">>"
            Err_list += ex.Source.ToString & ">>"
            Err_list += ex.Message.ToString
            Throw New ApplicationException(Err_list)
        End Try
        Return oNuevo
    End Function
    Private Function _EsValorPorDefecto(ByVal _attrib As String, ByVal _value As Object, ByVal VACIO As Object) As Boolean
        Dim vCompare As Boolean = False
        For Each propiedad As PropertyInfo In VACIO.GetType.GetProperties()
            If (propiedad.Name.ToString) = _attrib Then
                If propiedad.GetValue(VACIO, {}) Is Nothing Then
                    If Not _value Is Nothing Then
                        vCompare = True
                        Exit For
                    End If
                Else
                    If propiedad.GetValue(VACIO, {}) <> _value Then
                        vCompare = True
                        Exit For
                    Else
                        vCompare = False
                        Exit For
                    End If
                End If
            End If
        Next
        Return vCompare
    End Function
    Private Function CMD_Execute(ByVal Conexion As String, oCmd As SqlCommand, ByVal query As String, Optional T_cmd As Tipo_Cmd = Tipo_Cmd.returnID) As Integer
        Dim ds As New DataSet
        Dim oConTemp = New SqlConnection()
        Dim resp As Integer
        Dim newID As Integer = -1
        Try
            oConTemp.ConnectionString = Conexion
            oCmd.CommandType = Data.CommandType.Text
            oCmd.CommandText = query
            oCmd.Connection = oConTemp
            oConTemp.Open()
            If T_cmd = Tipo_Cmd.executenonquery Then  'devuelvo cantidad de filas afectadas
                resp = oCmd.ExecuteNonQuery()
            ElseIf T_cmd = Tipo_Cmd.executescalar Then
                oCmd.CommandText = query & "; SELECT CAST(scope_identity() AS int) AS ID;" 'devuelvo ID insertado
                resp = oCmd.ExecuteScalar()
            ElseIf T_cmd = 2 Then
                oCmd.CommandText = query & "; SELECT CAST(scope_identity() AS int) AS ID;" 'devuelvo ID insertado
                resp = CInt(oCmd.ExecuteScalar())
            End If
            oConTemp.Close()
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al ejecutar CMD", ex))
        Finally
            oConTemp.Close()
        End Try
        Return resp
    End Function
    Public Function Query(ByVal _query As String) As DataTable
        Dim constr As String = strConexion
        Dim xSS As String = ""
        Dim dt As New DataTable
        Dim _sql As String = ""
        Dim Alerta As Boolean = False
        If UCase(_query) = "" Then Exit Function
        _sql = ""
        Select Case UCase(_query)
            Case (InStr(UCase(_query), "DELETE") > 0) : Alerta = True : xSS = "Delete"
            Case (InStr(UCase(_query), "TRUNCATE") > 0) : Alerta = True : xSS = "TRUNCATE"
            Case (InStr(UCase(_query), "DROP") > 0) : Alerta = True : xSS = "Drop"
        End Select

        If Alerta Then
            Dim oErr As New cError()
            oErr.Exc_Client_msg = "Error al ejecutar instrucción QUERY (xss)."
            oErr.Exc_Message = "Comando denegado encontrado."
            oErr.Exc_Source = "svr.Query"
            Throw oErr
            Exit Function
        End If

        Try
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand(_query)
                    cmd.CommandTimeout = Command_TimeOut_
                    cmd.Connection = con
                    con.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        dt.Load(dr)
                    End Using
                    If con.State <> ConnectionState.Closed Then
                        con.Close()
                        con.Dispose()
                    End If
                End Using
            End Using
        Catch ex As Exception
            Dim oErr As New cError()
            oErr.Exc_Client_msg = "Error al ejecutar instrucción QUERY.-"
            oErr.Exc_Message = ex.Message
            oErr.Exc_Source = ex.Source
            Throw oErr
        End Try
        Return dt
    End Function
    Public Shared Function Err_list(ByVal msg As String, ByVal ex_ As Exception) As String
        Dim xml_serializer As New XmlSerializer(GetType(cError))
        Dim string_writer As New StringWriter
        xml_serializer.Serialize(string_writer, oException)
        Return string_writer.ToString
    End Function
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
            Dim oErr As New cError()
            oErr.Exc_Client_msg = "Error al agregar registro."
            oErr.Exc_Message = ex.Message
            oErr.Exc_Source = ex.Source
            Throw oErr
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
                    srv = Servidor.Crear(FuenteDatos)
                End If
            Else
                srv = m_Servidor
            End If
            cant = srv.EjecutarSP(Tabla & "_EL", Parametros)
            If cant <> 1 Then
                Throw New ApplicationException("No se encontro el Registro buscado")
            End If
        Catch ex As Exception
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
                    srv = Servidor.Crear(FuenteDatos)
                End If
            Else
                srv = m_Servidor
            End If

            cant = srv.EjecutarSP(Tabla & "_AC", Parametros)
            If cant <> 1 Then
                Throw New ApplicationException("No se encontro el Registro buscado")
            End If
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al Modificar", ex))
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
                    srv = Servidor.Crear(FuenteDatos)
                End If
            Else
                srv = m_Servidor
            End If

            ds = srv.TraerDatos(Tabla & "_OU", Parametros)
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al ejecutar _OU", ex))
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
                    srv = Servidor.Crear(FuenteDatos)
                End If
            Else
                srv = m_Servidor
            End If

            ds = srv.TraerDatos(Tabla & "_OT", Parametros)
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al ejecutar _OT", ex))
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
                    srv = Servidor.Crear(FuenteDatos)
                End If
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_filtrado", Parametros)
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al ejecutar _filtrado", ex))
        End Try
        Return ds
    End Function
    Public Overridable Function EjecutarSp(ByVal sp As String, ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos)
                End If
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_" & sp, Parametros)
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al ejecutar Sp", ex))
        End Try
        Return ds
    End Function
    Public Overridable Function EjecutarSpLIBRE_cs_usuario(ByVal sp As String, ByVal cString_de_usuario As String, ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(sp, cString_de_usuario)
                End If
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(sp, Parametros)
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al ejecutar SpLIBRE", ex))
        End Try
        Return ds
    End Function
    Public Overridable Function EjecutarSpLIBRE(ByVal sp As String, ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                If _webConfig Then
                    srv = Servidor.Crear(FuenteDatos, App, _appSettings)
                Else
                    srv = Servidor.Crear(FuenteDatos)
                End If

            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(sp, Parametros)
        Catch ex As Exception
            Throw New ApplicationException(Err_list("Error al ejecutar SpLIBRE", ex))
        End Try
        Return ds
    End Function
    Public Sub logW(ByVal Origen As String, ByVal msg As String, ByVal usuario As String, ex As Exception)
        Event_Log_Error.Main(Origen, msg, ex)
        Return
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
    Private Sub Attrib_Default()
        Dim attributes As AttributeCollection =
    TypeDescriptor.GetProperties(Me)("ID_PERSONA").Attributes
        Dim myAttribute As DefaultValueAttribute =
    CType(attributes(GetType(DefaultValueAttribute)), DefaultValueAttribute)
        Console.WriteLine(("The default value is: " & myAttribute.Value.ToString()))
    End Sub
    Private Shared Function EsValorPorDefecto(prop As PropertyInfo, ByVal tipo As String) As Boolean
        Dim Resp As Boolean = False
        Dim attributes = prop.GetCustomAttributes(GetType(DefaultValueAttribute), True)
        If attributes.Length > 0 Then
            Dim defaultAttr = DirectCast(attributes(0), DefaultValueAttribute)
            If (defaultAttr.Value) = "►" Then
                Resp = True
            End If
        End If
        If prop.PropertyType.IsValueType Then
            Debug.Print(Activator.CreateInstance(prop.PropertyType))
        End If
        Return Resp
    End Function
    Private Function Comparar(ByVal nuevo As String(), ByVal actual As Object) As Boolean
        Dim Resp As Boolean = False
        For Each actual_prop As PropertyInfo In actual.GetType.GetProperties()
            If actual_prop.Name = nuevo(0) Then
                If actual_prop.GetValue(actual, {}) = nuevo(1) Then
                    Resp = True
                Else
                    Resp = False
                End If
                Exit For
            End If
        Next
        Return Resp
    End Function
    Public Shared Sub RecorrerDatos(DatosCliente As Object)
        For Each Propiedad As PropertyInfo In DatosCliente.GetType().GetProperties
            ContieneDatos(Propiedad, DatosCliente)
        Next
    End Sub
    Private Shared Sub ContieneDatos(propiedad As PropertyInfo, tipo As Object)
        Dim Dato = propiedad.GetValue(tipo, Nothing)
        If Not Dato Is Nothing AndAlso Dato.GetType.GetGenericArguments.Count > 0 Then
            If Dato.count() = 0 Then
            Else
                For Each item In Dato
                    For Each PropiedadItem As PropertyInfo In item.GetType().GetProperties
                        ContieneDatos(PropiedadItem, item)
                    Next
                Next
            End If
        ElseIf Not Dato Is Nothing Then
            If TypeOf (Dato) Is String Then
                propiedad.SetValue(tipo, "Relleno la Propiedad", Nothing)
            Else
                For Each prop As PropertyInfo In Dato.GetType.GetProperties
                    Dim subDato = prop.GetValue(Dato, Nothing)
                    If TypeOf (subDato) Is String AndAlso subDato = "" Then
                        prop.SetValue(Dato, "Relleno la Propiedad", Nothing)
                    End If
                Next
            End If
        End If
    End Sub
    Public Shared Function Convert_file_to_byteArray(ByVal data As FileStream) As Byte()
        If IsNothing(data) Then Return Nothing
        Dim br As BinaryReader = New BinaryReader(data)
        Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(data.Length))
        br.Close()
        data.Close()
        Return bytes
    End Function

    Private Function Convert_(ByVal cell_nombre As String, ByVal cell_tipo As String, ByVal cell_valor As String) As Object
        Dim Resp As Object
        Resp = Nothing
        Select Case cell_tipo
            Case "System.Int16", "System.Int32", "System.Int64"
                Resp = IIf(IsDBNull(cell_valor), Nothing, CInt(cell_valor))
            Case "System.Boolean"
                If IsNothing(cell_valor) = False Then
                    Resp = CBool(cell_valor)
                Else
                    Resp = Nothing
                End If

            Case "System.Byte"
                If IsNothing(cell_valor) = False Then
                    Resp = CByte(cell_valor)
                Else
                    Resp = Nothing
                End If

            Case "System.DateTime"
                If IsDate(cell_valor) Then
                    Resp = CDate(cell_valor)
                Else
                    Resp = Nothing
                End If
            Case "System.Double"
                Resp = IIf(IsDBNull(cell_valor), Nothing, CDbl(cell_valor))
            Case "System.Byte"
            Case "System.String", "System.Char"
                Resp = IIf(IsDBNull(cell_valor), "", CStr(cell_valor))
            Case "System.DBNull"
                Resp = IIf(IsDBNull(cell_valor), Nothing, Nothing)
            Case "System.Decimal"
                Resp = IIf(IsDBNull(cell_valor), Nothing, CDec(cell_valor))
            Case Else
                Resp = IIf(IsDBNull(cell_valor), "", CStr(cell_valor))
        End Select

        Return Resp

    End Function
    Private Shared Function GetItem(Of T)(dr As DataRow) As T
        Dim temp As Type = GetType(T)
        Dim obj As T = Activator.CreateInstance(Of T)()

        For Each column As DataColumn In dr.Table.Columns
            For Each pro As PropertyInfo In temp.GetProperties()
                If pro.Name = column.ColumnName Then
                    pro.SetValue(obj, dr(column.ColumnName), Nothing)
                Else
                    Continue For
                End If
            Next
        Next
        Return obj
    End Function
    Public Shared Function GetTypeForSqlType(_Type As SqlDbType) As Type
        Select Case _Type
            Case SqlDbType.BigInt
                Return GetType(System.Int64)
            Case SqlDbType.Bit
                Return GetType(System.Boolean)
            Case SqlDbType.[Char]
                Return GetType(String)
            Case SqlDbType.DateTime
                Return GetType(System.DateTime)
            Case SqlDbType.[Decimal]
                Return GetType(System.Double)
            Case SqlDbType.Float
                Return GetType(System.Double)
            Case SqlDbType.Image
                Return GetType(Byte())
            Case SqlDbType.Int
                Return GetType(System.Int32)
            Case SqlDbType.Money
                Return GetType(System.Decimal)
            Case SqlDbType.NChar
                Return GetType(String)
            Case SqlDbType.NText
                Return GetType(String)
            Case SqlDbType.NVarChar
                Return GetType(String)
            Case SqlDbType.Real
                Return GetType(System.Single)
            Case SqlDbType.SmallDateTime
                Return GetType(System.DateTime)
            Case SqlDbType.SmallInt
                Return GetType(System.Int16)
            Case SqlDbType.SmallMoney
                Return GetType(System.Decimal)
            Case SqlDbType.Text
                Return GetType(String)
            Case SqlDbType.Timestamp
                Return GetType(System.DateTime)
            Case SqlDbType.TinyInt
                Return GetType(Byte)
            Case SqlDbType.UniqueIdentifier
                Return GetType(Guid)
            Case SqlDbType.VarBinary
                Return GetType(Byte())
            Case SqlDbType.VarChar
                Return GetType(String)
            Case SqlDbType.[Variant]
                Return GetType(Object)
            Case Else
                Throw New System.Exception("Unknown SqlType " + _Type)
        End Select
    End Function
    Private Function TypeToDbType(T As String) As SqlDbType
        Select Case T
            Case "Integer"
                Return SqlDbType.Int
                Exit Select
            Case "Int32"
                Return SqlDbType.Int
                Exit Select
            Case "Int16"
                Return SqlDbType.SmallInt
                Exit Select
            Case "Decimal"
                Return SqlDbType.Decimal
                Exit Select
            Case "Double"
                Return SqlDbType.Float
                Exit Select
            Case "Boolean"
                Return SqlDbType.Bit
                Exit Select
            Case "String", "Char"
                Return SqlDbType.VarChar
                Exit Select
            Case "DateTime"
                Return SqlDbType.DateTime
                Exit Select
            Case "Boolean"
                Return SqlDbType.Bit
                Exit Select
            Case "Byte[]"
                Return SqlDbType.VarBinary
            Case Else

                Return DbType.[Object]
                Exit Select
        End Select
    End Function
    Public Function Lista_Obj(Of T)(ByVal query As String) As List(Of T)
        Dim lista As List(Of T)
        Try
            lista = BindDataList(Of T)(Me.Query(query))
        Catch ex As Exception
            Throw New ApplicationException("Error ejecutar Lista_Obj. " & ex.Message)
        End Try
        Return lista
    End Function
    Private Function BindData(Of T)(dt As DataTable) As T
        Dim dr As DataRow = dt.Rows(0)
        Dim columns As New List(Of String)()
        For Each dc As DataColumn In dt.Columns
            columns.Add(dc.ColumnName)
        Next

        Dim ob = Activator.CreateInstance(Of T)()

        Dim fields = GetType(T).GetFields()
        For Each fieldInfo As Object In fields
            If columns.Contains(fieldInfo.Name) Then
                fieldInfo.SetValue(ob, dr(fieldInfo.Name))
            End If
        Next

        Dim properties = GetType(T).GetProperties()
        For Each propertyInfo As Object In properties
            If columns.Contains(propertyInfo.Name) Then
                If IsDBNull(dr(propertyInfo.Name)) Then
                    propertyInfo.SetValue(ob, Nothing, Nothing)
                Else
                    propertyInfo.SetValue(ob, dr(propertyInfo.Name), Nothing)
                End If
            End If
        Next
        Return ob
    End Function
    Private Function BindDataList(Of T)(dt As DataTable) As List(Of T)
        Dim drlist As New List(Of DataRow)()
        Dim pepe As ObjectHandle = Activator.CreateInstance("Pepe", "Clase")
        For Each row As DataRow In dt.Rows
            drlist.Add(CType(row, DataRow))
        Next row
        Dim objeto_respuesta As Object = New ExpandoObject()
        objeto_respuesta.ID = 0
        Dim columns As New List(Of String)()
        For Each dc As DataColumn In dt.Columns
            columns.Add(UCase(dc.ColumnName))
            CType(objeto_respuesta, IDictionary(Of String, Object)).Add(dc.ColumnName, Nothing)
        Next
        Dim properties = GetType(T).GetProperties()

        Dim lst As New List(Of T)()
        Try

            For Each dr As DataRow In dt.Rows
                Dim ob = Activator.CreateInstance(Of T)()
                Dim _tmp As String = ""
                For Each propertyInfo As Object In objeto_respuesta
                    _tmp = DirectCast(propertyInfo, System.Collections.Generic.KeyValuePair(Of String, Object)).[Key].ToString
                    If columns.Contains(_tmp) = True Then
                        objeto_respuesta.SetValue(objeto_respuesta, dr(propertyInfo.Name), Nothing)
                        If IsDBNull(dr(propertyInfo.Name)) Then
                            propertyInfo.SetValue(ob, Nothing, Nothing)
                        Else
                            If dr(propertyInfo.Name).GetType.FullName = "System.Char" Then
                                Debug.Print("tipo char detectado")
                            End If
                            propertyInfo.SetValue(ob, dr(propertyInfo.Name), Nothing)
                        End If
                    Else
                        Debug.Print("sin propiedad..")
                    End If
                Next
                lst.Add(ob)
            Next
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return lst
    End Function
    Private Function BindDataList_BACK(Of T)(dt As DataTable) As List(Of T)
        Dim drlist As New List(Of DataRow)()
        For Each row As DataRow In dt.Rows
            drlist.Add(CType(row, DataRow))
        Next row
        Dim columns As New List(Of String)()
        For Each dc As DataColumn In dt.Columns
            columns.Add(UCase(dc.ColumnName))
        Next
        Dim fields = GetType(T).GetFields()
        Dim properties = GetType(T).GetProperties()
        Dim lst As New List(Of T)()
        Try

            For Each dr As DataRow In dt.Rows
                Dim ob = Activator.CreateInstance(Of T)()
                For Each fieldInfo As Object In fields
                    If columns.Contains(UCase(fieldInfo.Name)) Then
                        fieldInfo.SetValue(ob, dr(fieldInfo.Name))
                    End If
                Next
                For Each propertyInfo As Object In properties
                    If columns.Contains(propertyInfo.Name) = True Then
                        If IsDBNull(dr(propertyInfo.Name)) Then
                            propertyInfo.SetValue(ob, Nothing, Nothing)
                        Else
                            If dr(propertyInfo.Name).GetType.FullName = "System.Char" Then
                                'tipo char!!!
                                Debug.Print("tipo char detectado")
                            End If
                            propertyInfo.SetValue(ob, dr(propertyInfo.Name), Nothing)
                        End If
                    Else
                        'campo no contemplado en el obj, pero si en la consulta
                        Debug.Print("sin propiedad..")
                    End If
                Next
                lst.Add(ob)
            Next
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return lst
    End Function
    Public Shared Function Adnet_null_byte() As Byte()
        Dim vacio() As Byte = System.Text.Encoding.ASCII.GetBytes("")
        Return vacio
    End Function
    Public Shared Function null_date() As DateTime
        Return CDate("1/1/1900 12:00:00")
    End Function

    Private vCommand_TimeOut_ As Integer
    Private Property Command_TimeOut_() As Integer
        Get
            Return vCommand_TimeOut_
        End Get
        Set(ByVal value As Integer)
            vCommand_TimeOut_ = value
        End Set
    End Property
    Public Shared Function getdate() As DateTime
        Return Now
    End Function
End Class


