'Dim auth as new LdapAuthentication
'auth.IsAuthenticated("dni del mono", "contraseña")

Imports System.Text
Imports System.Collections
Imports System.Web.Security
Imports System.Security.Principal
Imports System.DirectoryServices


Public Class LdapAuthentication

    Private _path As String
    Private _filterAttribute As String

    Public Sub New(Optional ByVal path As String = "LDAP://10.201.0.7")
        _path = path
    End Sub

    Public Function IsAuthenticated(ByVal username As String, ByVal pwd As String) As Boolean
        Dim domainAndUsername As String = "GENDARMERIA" & "\" & username
        Dim entry As New DirectoryEntry(_path, domainAndUsername, pwd)

        Try
            'Bind to the native AdsObject to force authentication.
            Dim obj As Object = entry.NativeObject

            Dim search As New DirectorySearcher(entry)

            search.Filter = "(SAMAccountName=" & username & ")"
            search.PropertiesToLoad.Add("cn")
            Dim result As SearchResult = search.FindOne()

            If result Is Nothing Then
                Return False
            End If

            'Update the new path to the user in the directory.
            _path = result.Path
            _filterAttribute = DirectCast(result.Properties("cn")(0), String)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

End Class