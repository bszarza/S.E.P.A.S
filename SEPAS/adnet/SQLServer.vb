Imports System.Data.SqlClient

Public Class SQLServer
    Inherits Servidor

    Private oCon As SqlConnection
    Private oTrx As SqlTransaction

    Public Sub New(ByVal Conexion As String)
        m_Conexion = Conexion
        If Conexion.Length < 6 Then
            Throw New ApplicationException("String de conexion invalido: " & Conexion)
        End If
        oCon = New SqlConnection(Conexion)
    End Sub

    Public Overrides Function EjecutarSP(ByVal Nombre As String,
                    ByVal ParamArray Parametros() As Object) As Integer

        Dim cant As Integer
        Dim oConTemp As SqlConnection

        Try
            'Valido
            If Nombre.Length < 3 Then
                Throw New ApplicationException("Nombre de SP invalido")
            End If

            oConTemp = New SqlConnection(m_Conexion)

            'Creo el objeto comando
            Dim oCmd As New SqlCommand(Nombre, oConTemp)
            oCmd.CommandType = Data.CommandType.StoredProcedure

            'Traigo los parametros de la base
            oConTemp.Open()
            SqlCommandBuilder.DeriveParameters(oCmd)

            'veo si estoy en una transaccion
            If EnTransaccion() Then
                oConTemp.Close()
                oCmd.Connection = oCon
                oCmd.Transaction = oTrx
            End If

            'Pasar los parametros al array
            If oCmd.Parameters.Count <> Parametros.Length + 1 Then
                Throw New ApplicationException("Cantidad de parametros pasados invalidos.")
            End If

            Dim i As Integer
            For i = 0 To Parametros.Length - 1
                oCmd.Parameters(i + 1).Value = Parametros(i)
            Next

            'Ejecucion
            cant = oCmd.ExecuteNonQuery()

            'Devuelvo los parametros
            For i = 0 To Parametros.Length - 1
                Parametros(i) = oCmd.Parameters(i + 1).Value
            Next
            oConTemp.Close()

        Catch ex As Exception
            'Throw ex
            Throw New ApplicationException("Error en la ejecucion del SP: " + Nombre & "-" & ex.Message)

        Finally

            If Not EnTransaccion() Then
                If Not EnTransaccion() Then
                    'If oConTemp.State.ToString = Data.ConnectionState.Open Then
                    '    oConTemp.Close()
                    'End If
                End If
            End If
        End Try

        Return cant

    End Function

    Public Overrides Function TraerDatos(ByVal Nombre As String, ByVal ParamArray Parametros() As Object) As System.Data.DataSet
        'Dim cant As Integer
        Dim ds As New Data.DataSet()
        Dim oConTemp As SqlConnection


        Try
            'Valido
            If Nombre.Length < 3 Then
                Throw New ApplicationException("Nombre de SP invalido")
            End If

            oConTemp = New SqlConnection(m_Conexion)

            'Creo el objeto comando
            Dim oCmd As New SqlCommand(Nombre, oConTemp)
            oCmd.CommandType = Data.CommandType.StoredProcedure

            'Traigo los parametros de la base
            oConTemp.Open()
            SqlCommandBuilder.DeriveParameters(oCmd)

            'veo si estoy en una transaccion
            If EnTransaccion() Then
                oConTemp.Close()
                oCmd.Connection = oCon
                oCmd.Transaction = oTrx
            End If

            'Pasar los parametros al array
            If oCmd.Parameters.Count <> Parametros.Length + 1 Then
                Throw New ApplicationException("Cantidad de parametros pasados invalidos.")
            End If

            Dim i As Integer
            For i = 0 To Parametros.Length - 1
                oCmd.Parameters(i + 1).Value = Parametros(i)
            Next

            'Armo el DataAdapter
            Dim DA As New SqlDataAdapter(oCmd)

            'Obtengo el DataSet
            DA.Fill(ds)

            'Devuelvo los parametros
            For i = 0 To Parametros.Length - 1
                Parametros(i) = oCmd.Parameters(i + 1).Value
            Next
            oConTemp.Close()

        Catch ex As Exception
            'Throw ex
            Throw New ApplicationException("Error en la ejecucion del SP: " + Nombre & "-" & ex.Message)

        Finally
            If Not EnTransaccion() Then
                'If oConTemp.State = Data.ConnectionState.Open Then
                '    oConTemp.Close()
                'End If
            End If
        End Try

        Return ds
    End Function


    Public Overrides Function EnTransaccion() As Boolean
        Return Not (oTrx Is Nothing)
    End Function

    Public Overrides Sub IniciarTransaccion()
        oCon.Open()
        oTrx = oCon.BeginTransaction()
    End Sub

    Public Overrides Sub Commit()
        If Not EnTransaccion() Then
            Throw New ApplicationException("Commit sin TRX")
        End If

        oTrx.Commit()
        oTrx = Nothing
        oCon.Close()
    End Sub

    Public Overrides Sub RollBack()
        If Not EnTransaccion() Then
            Throw New ApplicationException("RollBack sin TRX")
        End If

        oTrx.Rollback()
        oTrx = Nothing
        oCon.Close()

    End Sub
End Class







