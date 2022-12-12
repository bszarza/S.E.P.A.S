Public MustInherit Class ClaseBaseNet

    Public Tabla As String
    Public FuenteDatos As String
    Public App As String

    Protected m_Servidor As Servidor
    'Sub New(ByVal Tabla As String)
    '    Me.Tabla = Tabla
    '    Me.FuenteDatos = "DEFAULT"
    'End Sub

    'Sub New(ByVal FuenteDatos As String, ByVal Tabla As String)
    '    Me.Tabla = Tabla
    '    Me.FuenteDatos = FuenteDatos
    'End Sub

    Sub New(ByVal App As String, ByVal Tabla As String)
        Me.Tabla = Tabla
        Me.App = App
    End Sub

    Sub New(ByVal oServidor As Servidor, ByVal Tabla As String)
        Me.Tabla = Tabla
        m_Servidor = oServidor
    End Sub
    Public Overridable Function Agregar(ByVal ParamArray Parametros() As Object) As Integer
        Dim cant As Integer
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                srv = Servidor.Crear(FuenteDatos, App)
            Else
                srv = m_Servidor
            End If

            cant = srv.EjecutarSP(Tabla & "_IN", Parametros)

            'If cant <> 1 Then
            '    Throw New ApplicationException("No se encontro el Registro buscado")
            'End If

            Return cant

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Overridable Sub Eliminar(ByVal ParamArray Parametros() As Object)
        Dim cant As Integer
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                srv = Servidor.Crear(FuenteDatos, App)
            Else
                srv = m_Servidor
            End If
            cant = srv.EjecutarSP(Tabla & "_EL", Parametros)
            'If cant <> 1 Then
            '    Throw New ApplicationException("No se encontro el Registro buscado")
            'End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overridable Sub Modificar(ByVal ParamArray Parametros() As Object)
        Dim cant As Integer
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                srv = Servidor.Crear(FuenteDatos, App)
            Else
                srv = m_Servidor
            End If

            cant = srv.EjecutarSP(Tabla & "_AC", Parametros)
            'If cant <> 1 Then
            '    Throw New ApplicationException("No se encontro el Registro buscado")
            'End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overridable Function TraerUno(ByVal ParamArray Parametros() As Object) As System.Data.DataSet

        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                srv = Servidor.Crear(FuenteDatos, App)
            Else
                srv = m_Servidor
            End If

            ds = srv.TraerDatos(Tabla & "_OU", Parametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function

    Public Overridable Function TraerTodos(ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                srv = Servidor.Crear(FuenteDatos, App)
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_OT", Parametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function



    Public Overridable Function Filtrado(ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        Dim ds As Data.DataSet
        Try
            Dim srv As Servidor
            If m_Servidor Is Nothing Then
                srv = Servidor.Crear(FuenteDatos, App)
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_FIL", Parametros)
        Catch ex As Exception
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
                srv = Servidor.Crear(FuenteDatos, App)
            Else
                srv = m_Servidor
            End If
            ds = srv.TraerDatos(Tabla & "_" & sp, Parametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function


End Class
