'AdNet_GNA: [generador de clases] 6.0.0.1/2017.
'fecha generación: 10/12/2022 18:49:15
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
  
Public Class TipoCursos  
    Public Sub New() 
	    Clear() 
    End Sub 

#Region "Variables" 
	Private _ID As Integer 
	Private _Descripcion As String 
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
		 
    Public Property Descripcion() As String 
		Get 
			Return _Descripcion 
		End Get 
		set (Value As String) 
			_Descripcion = value  
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
		_Descripcion = Nothing  
		_UltimaModificacion = Nothing  
		_FlagMostrar = Nothing  
		_Usuario = Nothing  
	End Sub	 
#End Region 


#Region "Metodos"

Public Function Insert(ByVal obj As TipoCursos, Optional ByVal oArgsLogueo As Object = Nothing) As TipoCursos
    Dim Resp As New TipoCursos
    Try
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS", "TipoCursos", obj.USUARIO)
        Resp = oSvr.Insert(obj)
 
        ''If Not oArgsLogueo Is Nothing Then
        ''    Dim Jserializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        ''    Dim OSegLog As New SEG_LOG_GENERAL.SEG_LOG_GENERAL
        ''    oArgsLogueo.BASE = "SEPAS"
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
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS", "TipoCursos", Usuario)
        ds = oSvr.EjecutarSp(sp, Parametros)
 
        ''If Not oArgsLogueo Is Nothing Then
        ''    Dim Jserializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        ''    Dim OSegLog As New SEG_LOG_GENERAL.SEG_LOG_GENERAL
        ''    oArgsLogueo.BASE = "SEPAS"
        ''    oArgsLogueo.OBJETO = Jserializer.Serialize(obj)
        ''    oArgsLogueo.METODO = "Insert"
        ''    OSegLog = OSegLog.Insert(oArgsLogueo)
        ''End If
    Catch ex As Exception
        Throw ex
    End Try
    Return ds
End Function
 
Public Function Update(ByVal obj As TipoCursos, Optional ByVal oArgsLogueo As Object = Nothing) As TipoCursos
    Dim Resp As Integer = 0
    Dim oRegistroActual As New TipoCursos
    Try
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS",  "TipoCursos", obj.USUARIO)
        oRegistroActual = oSvr.ObtenerRegistro(oRegistroActual, obj.ID)
        If oRegistroActual.ID > 0 Then
           oRegistroActual = oSvr.Update(obj)
            'If Not oArgsLogueo Is Nothing Then
            '       Dim Jserializer As New System.Web.Script.Serialization.JavaScriptSerializer()
            '       Dim OSegLog As New SEG_LOG_GENERAL.SEG_LOG_GENERAL
            '       oArgsLogueo.BASE = "SEPAS"
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
    Dim oDato As New ServidorDatosNet_GNA.svr("SEPAS","TipoCursos", "USUARIO")
    Try
        Return oDato.Delete(ID)
    Catch ex As Exception
        Throw ex
    End Try
End Function

Public Function Lista(ByVal Sp As String, ByVal ParamArray Parametros() As Object) As DataSet
    Dim oDatos As New ServidorDatosNet_GNA.svr("SEPAS", "", "USUARIO")
    Dim ds As New DataSet
    Try
        ds = oDatos.EjecutarSpLIBRE(Sp,Parametros)
    Catch ex As Exception
        Throw ex 
    End Try
    Return ds
End Function
Public Function Query(ByVal _query As String) As DataTable
    Dim oDatos As New ServidorDatosNet_GNA.svr("SEPAS", "", "USUARIO")
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





Public Function Obtener_registro(ByVal ID As Integer) As TipoCursos
    Dim oSvr As New ServidorDatosNet_GNA.svr("SEPAS", "TipoCursos", "USUARIO")
   Dim ds As New DataSet
   Dim oTipoCursos  As New TipoCursos
   Try
       oTipoCursos = oSvr.ObtenerRegistro(oTipoCursos, ID)
       If oTipoCursos.ID <= 0 Then
           Throw New Exception("Registro no encontrado.")
       End If
   Catch ex As Exception
       Throw ex 
   End Try
   Return oTipoCursos
End Function

Public Function Lista_TipoCursos(ByVal query As String) As List(Of TipoCursos)
Dim oDatos As New ServidorDatosNet_GNA.svr("SEPAS", "", "USUARIO")
Dim Lst_TipoCursos As List(Of TipoCursos)
Try
   Lst_TipoCursos = oDatos.Lista_Obj(Of TipoCursos)(query)
Catch ex As Exception
   Throw ex 
End Try
Return Lst_TipoCursos
End Function
#End Region
End Class 
