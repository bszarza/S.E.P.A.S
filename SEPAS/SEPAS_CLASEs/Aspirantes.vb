'AdNet_GNA: [generador de clases] 6.0.0.1/2017.
'fecha generación: 7/12/2022 16:00:35
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
  
Public Class Aspirantes  
    Public Sub New() 
	    Clear() 
    End Sub 

#Region "Variables" 
	Private _ID As Integer 
	Private _Nombre As String 
	Private _Apellido As String 
	Private _Dni As Decimal 
	Private _CE As Integer 
	Private _IdTipoGenero As Integer 
	Private _IdTipoCurso As Integer 
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
		 
    Public Property Nombre() As String 
		Get 
			Return _Nombre 
		End Get 
		set (Value As String) 
			_Nombre = value  
		End Set 
	End Property 
		 
    Public Property Apellido() As String 
		Get 
			Return _Apellido 
		End Get 
		set (Value As String) 
			_Apellido = value  
		End Set 
	End Property 
		 
    Public Property Dni() As Decimal 
		Get 
			Return _Dni 
		End Get 
		set (Value As Decimal) 
			_Dni = value  
		End Set 
	End Property 
		 
    Public Property CE() As Integer 
		Get 
			Return _CE 
		End Get 
		set (Value As Integer) 
			_CE = value  
		End Set 
	End Property 
		 
    Public Property IdTipoGenero() As Integer 
		Get 
			Return _IdTipoGenero 
		End Get 
		set (Value As Integer) 
			_IdTipoGenero = value  
		End Set 
	End Property 
		 
    Public Property IdTipoCurso() As Integer 
		Get 
			Return _IdTipoCurso 
		End Get 
		set (Value As Integer) 
			_IdTipoCurso = value  
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
		_Nombre = Nothing  
		_Apellido = Nothing  
		_Dni = Nothing  
		_CE = Nothing  
		_IdTipoGenero = Nothing  
		_IdTipoCurso = Nothing  
		_UltimaModificacion = Nothing  
		_FlagMostrar = Nothing  
		_Usuario = Nothing  
	End Sub	 
#End Region 


#Region "Metodos"

Public Function Insert(ByVal obj As Aspirantes, Optional ByVal oArgsLogueo As Object = Nothing) As Aspirantes
    Dim Resp As New Aspirantes
    Try
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Aspirantes", obj.USUARIO)
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
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Aspirantes", Usuario)
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
 
Public Function Update(ByVal obj As Aspirantes, Optional ByVal oArgsLogueo As Object = Nothing) As Aspirantes
    Dim Resp As Integer = 0
    Dim oRegistroActual As New Aspirantes
    Try
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES",  "Aspirantes", obj.USUARIO)
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
    Dim oDato As New ServidorDatosNet_GNA.svr("SEPAS_DES","Aspirantes", "USUARIO")
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





Public Function Obtener_registro(ByVal ID As Integer) As Aspirantes
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Aspirantes", "USUARIO")
   Dim ds As New DataSet
   Dim oAspirantes  As New Aspirantes
   Try
       oAspirantes = oSvr.ObtenerRegistro(oAspirantes, ID)
       If oAspirantes.ID <= 0 Then
           Throw New Exception("Registro no encontrado.")
       End If
   Catch ex As Exception
       Throw ex 
   End Try
   Return oAspirantes
End Function

Public Function Lista_Aspirantes(ByVal query As String) As List(Of Aspirantes)
Dim oDatos As New ServidorDatosNet_GNA.svr("SEPAS_DES", "", "USUARIO")
Dim Lst_Aspirantes As List(Of Aspirantes)
Try
   Lst_Aspirantes = oDatos.Lista_Obj(Of Aspirantes)(query)
Catch ex As Exception
   Throw ex 
End Try
Return Lst_Aspirantes
End Function
#End Region
End Class 
