Public Class fn_global
    Public Shared Property DNI() As String
        Get
            Return DirectCast(HttpContext.Current.Session("DNI"), String)
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("DNI") = value
        End Set
    End Property
    ' SIN USO
    Public Shared Property NOMBRE_USUARIO() As String
        Get
            Return DirectCast(HttpContext.Current.Session("NOMBRE_USUARIO"), String)
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("NOMBRE_USUARIO") = value
        End Set
    End Property
    Public Shared Property Auxiliar() As String
        Get
            Return DirectCast(HttpContext.Current.Session("Aux"), String)
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("Aux") = value
        End Set
    End Property
End Class