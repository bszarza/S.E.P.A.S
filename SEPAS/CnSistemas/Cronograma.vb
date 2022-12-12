'AdNet_GNA: [generador de clases] 6.0.0.1/2017.
'fecha generación: 7/12/2022 16:00:03
'para ServidorDatosNet_GNA.dll 1.0+
'División Desarrollo - Informática GNA
Imports System.IO 
Imports System.Text 
Imports System.Data 
Imports System.Data.SqlClient 
Imports System.Data.SqlClient.SqlException 
Imports System.Reflection 
Imports System.Globalization 
Imports ServidorDatosNet_GNA 
Imports ServidorDatosNet_GNA.svr 
'Imports ServidorDatosNet_GNA_LOG Activar para loguear actividad
Imports System.Threading
Imports System.Configuration
Public Class Cronograma  
    Public Sub New() 
	    Clear() 
    End Sub 

#Region "Variables" 
	Private _ID As Integer 
	Private _IdHorario As Integer 
	Private _IdCurso As Integer 
	Private _Lunes As String 
	Private _Martes As String 
	Private _Miercoles As String 
	Private _Jueves As String 
	Private _Viernes As String 
	Private _UltimaModificacion As DateTime? 
	Private _FlagMostrar As Boolean? 
	Private _Usuario As Decimal 
#End Region 
 
#Region "Propiedades" 
    Public Property ID() As Integer 
		Get 
			Return _ID 
		End Get 
		set (Value As Integer) 
			_ID = value  
		End Set 
	End Property 
		 
    Public Property IdHorario() As Integer 
		Get 
			Return _IdHorario 
		End Get 
		set (Value As Integer) 
			_IdHorario = value  
		End Set 
	End Property 
		 
    Public Property IdCurso() As Integer 
		Get 
			Return _IdCurso 
		End Get 
		set (Value As Integer) 
			_IdCurso = value  
		End Set 
	End Property 
		 
    Public Property Lunes() As String 
		Get 
			Return _Lunes 
		End Get 
		set (Value As String) 
			_Lunes = value  
		End Set 
	End Property 
		 
    Public Property Martes() As String 
		Get 
			Return _Martes 
		End Get 
		set (Value As String) 
			_Martes = value  
		End Set 
	End Property 
		 
    Public Property Miercoles() As String 
		Get 
			Return _Miercoles 
		End Get 
		set (Value As String) 
			_Miercoles = value  
		End Set 
	End Property 
		 
    Public Property Jueves() As String 
		Get 
			Return _Jueves 
		End Get 
		set (Value As String) 
			_Jueves = value  
		End Set 
	End Property 
		 
    Public Property Viernes() As String 
		Get 
			Return _Viernes 
		End Get 
		set (Value As String) 
			_Viernes = value  
		End Set 
	End Property 
		 
    Public Property UltimaModificacion() As DateTime? 
		Get 
			Return _UltimaModificacion 
		End Get 
		set (Value As DateTime?) 
			_UltimaModificacion = value  
		End Set 
	End Property 
		 
    Public Property FlagMostrar() As Boolean? 
		Get 
			Return _FlagMostrar 
		End Get 
		set (Value As Boolean?) 
			_FlagMostrar = value  
		End Set 
	End Property 
		 
    Public Property Usuario() As Decimal 
		Get 
			Return _Usuario 
		End Get 
		set (Value As Decimal) 
			_Usuario = value  
		End Set 
	End Property 
		 
#End Region 
 
#Region "Utilidades" 
	Public Sub Clear() 
		_ID = Nothing  
		_IdHorario = Nothing  
		_IdCurso = Nothing  
		_Lunes = Nothing  
		_Martes = Nothing  
		_Miercoles = Nothing  
		_Jueves = Nothing  
		_Viernes = Nothing  
		_UltimaModificacion = Nothing  
		_FlagMostrar = Nothing  
		_Usuario = Nothing  
	End Sub	 
#End Region 


#Region "Metodos"

Public Function Insert(ByVal obj As Cronograma, Optional ByVal oArgsLogueo As Object = Nothing) As Cronograma
    Dim Resp As New Cronograma
    Try
			Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Cronograma", "USUARIO")
			Resp = oSvr.Insert(obj)
 
        ''If Not oArgsLogueo Is Nothing Then
        ''    Dim Jserializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        ''    Dim OSegLog As New SEG_LOG_GENERAL.SEG_LOG_GENERAL
        ''    oArgsLogueo.BASE = "SEPAS_DES"
        ''    oArgsLogueo.OBJETO = Jserializer.Serialize(obj)
        ''    oArgsLogueo.METODO = "Insert"
        ''    OSegLog = OSegLog.Insert(oArgsLogueo)
        ''End If
    Catch ex As Exception
        Throw ex 
    End Try
    Return Resp
End Function
 

Public Function EjecutarSp (ByVal sp As String, byval Usuario as String, ByVal ParamArray Parametros() As Object) As System.Data.DataSet
    Dim ds As New  Dataset
    Try
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Cronograma", Usuario)
        ds = oSvr.EjecutarSp(sp, Parametros)
 
        ''If Not oArgsLogueo Is Nothing Then
        ''    Dim Jserializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        ''    Dim OSegLog As New SEG_LOG_GENERAL.SEG_LOG_GENERAL
        ''    oArgsLogueo.BASE = "SEPAS_DES"
        ''    oArgsLogueo.OBJETO = Jserializer.Serialize(obj)
        ''    oArgsLogueo.METODO = "Insert"
        ''    OSegLog = OSegLog.Insert(oArgsLogueo)
        ''End If
    Catch ex As Exception
        Throw ex
    End Try
    Return ds
End Function
 
Public Function Update(ByVal obj As Cronograma, Optional ByVal oArgsLogueo As Object = Nothing) As Cronograma
    Dim Resp As Integer = 0
    Dim oRegistroActual As New Cronograma
    Try
			Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Cronograma", "USUARIO")
			oRegistroActual = oSvr.ObtenerRegistro(oRegistroActual, obj.ID)
        If oRegistroActual.ID > 0 Then
           oRegistroActual = oSvr.Update(obj)
            'If Not oArgsLogueo Is Nothing Then
            '       Dim Jserializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            '       Dim OSegLog As New SEG_LOG_GENERAL.SEG_LOG_GENERAL
            '       oArgsLogueo.BASE = "SEPAS_DES"
            '       oArgsLogueo.OBJETO = Jserializer.Serialize(obj)
            '       oArgsLogueo.METODO = "Update"
            '       OSegLog = OSegLog.Insert(oArgsLogueo)
            'End If
        End If
    Catch ex As Exception
        Throw ex 
    End Try
    Return oRegistroActual
End Function
 
Public Function Delete(ByVal ID As Integer) As Integer
    Dim oDato As New ServidorDatosNet_GNA.svr("SEPAS_DES","Cronograma", "USUARIO")
    Try
        Return oDato.Delete(ID)
    Catch ex As Exception
        Throw ex
    End Try
End Function

Public Function Lista(ByVal Sp As String, ByVal ParamArray Parametros() As Object) As DataSet
    Dim oDatos As New ServidorDatosNet_GNA.svr("SEPAS_DES", "", "USUARIO")
    Dim ds As New DataSet
    Try
        ds = oDatos.EjecutarSpLIBRE(Sp,Parametros)
    Catch ex As Exception
        Throw ex 
    End Try
    Return ds
End Function
Public Function Query(ByVal _query As String) As DataTable
    Dim oDatos As New ServidorDatosNet_GNA.svr("SEPAS_DES", "", "USUARIO")
    Dim dt As New DataTable
    Try
        dt = oDatos.Query(_query)
    Catch ex As Exception
        Throw ex 
    End Try
    Return dt
End Function

Public Function Getdate() As DateTime
   Return Now.ToString("dd-MM-yyyy hh: mm : ss.fff", CultureInfo.InvariantCulture)
End Function





Public Function Obtener_registro(ByVal ID As Integer) As Cronograma
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Cronograma", "USUARIO")
   Dim ds As New DataSet
   Dim oCronograma  As New Cronograma
   Try
       oCronograma = oSvr.ObtenerRegistro(oCronograma, ID)
       If oCronograma.ID <= 0 Then
           Throw New Exception("Registro no encontrado.")
       End If
   Catch ex As Exception
       Throw ex 
   End Try
   Return oCronograma
End Function

Public Function Lista_Cronograma(ByVal query As String) As List(Of Cronograma)
Dim oDatos As New ServidorDatosNet_GNA.svr("SEPAS_DES", "", "USUARIO")
Dim Lst_Cronograma As List(Of Cronograma)
Try
   Lst_Cronograma = oDatos.Lista_Obj(Of Cronograma)(query)
Catch ex As Exception
   Throw ex 
End Try
Return Lst_Cronograma
End Function
#End Region
End Class 
