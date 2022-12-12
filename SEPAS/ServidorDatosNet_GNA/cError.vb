Imports System.Runtime.Serialization

Public Class cError
    Inherits Exception

    Private _Exc_Client_msg As String
    Private _Exc_StackTrace As String
    Private _Exc_Source As String
    Private _Exc_Message As String
    Public Property Exc_Client_msg() As String
        Get
            Return _Exc_Client_msg
        End Get
        Set(ByVal value As String)
            _Exc_Client_msg = value
        End Set
    End Property


    Public Property Exc_StackTrace() As String
        Get
            Return _Exc_StackTrace
        End Get
        Set(ByVal value As String)
            _Exc_StackTrace = value
        End Set
    End Property


    Public Property Exc_Source() As String
        Get
            Return _Exc_Source
        End Get
        Set(ByVal value As String)
            _Exc_Source = value
        End Set
    End Property


    Public Property Exc_Message() As String
        Get
            Return _Exc_Message
        End Get
        Set(ByVal value As String)
            _Exc_Message = value
        End Set
    End Property

End Class
