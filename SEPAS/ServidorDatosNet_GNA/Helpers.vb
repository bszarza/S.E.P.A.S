Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text.RegularExpressions
Imports System.Net.Sockets
Imports System.Net
Imports System.Linq
Imports System.IO
Imports System.Diagnostics.CodeAnalysis
Imports System

Namespace Helpers
    Public Class HelperFunctions
        'Public Shared Function GetResolvedConnecionIpAddress(serverNameOrUrl As String, ByRef resolvedIpAddress As String) As Boolean
        '    Dim isResolved As Object = False
        '    Dim resolvIp As IPAddress = Nothing
        '    Try
        '        If Not IPAddress.TryParse(serverNameOrUrl, resolvIp) Then
        '            Dim hostEntry As Object = Dns.GetHostEntry(serverNameOrUrl)

        '            If hostEntry IsNot Nothing AndAlso hostEntry.AddressList IsNot Nothing AndAlso hostEntry.AddressList.Length > 0 Then
        '                If hostEntry.AddressList.Length = 1 Then
        '                    resolvIp = hostEntry.AddressList(0)
        '                    isResolved = True
        '                Else
        '                    For Each var As Object In hostEntry.AddressList.Where(Function(var) var.AddressFamily = AddressFamily.InterNetwork)
        '                        resolvIp = var
        '                        isResolved = True
        '                        Exit For
        '                    Next
        '                End If
        '            End If
        '        Else
        '            isResolved = True
        '        End If
        '    Catch generatedExceptionName As Exception
        '        isResolved = False
        '        resolvIp = Nothing
        '    Finally
        '        If resolvIp IsNot Nothing Then
        '            resolvedIpAddress = resolvIp.ToString()
        '        End If
        '    End Try

        '    resolvedIpAddress = Nothing
        '    Return isResolved
        'End Function

        Public Shared Function SerializeObject(Of T)(source As T) As String
            Dim serializer As New XmlSerializer(GetType(T))

            Using sw As New StringWriter()
                Using writer As New XmlTextWriter(sw)
                    serializer.Serialize(writer, source)
                    Return sw.ToString()
                End Using
            End Using
        End Function

        Public Shared Function DeSerializeObject(Of T)(xml As String) As T
            Using sr As New StringReader(xml)
                Dim serializer As New XmlSerializer(GetType(T))
                Return CType(serializer.Deserialize(sr), T)
            End Using
        End Function

        Public Shared Function ReturnZeroIfNull(value As Object) As Object
            If value = DBNull.Value Then
                Return 0
            End If
            If value Is Nothing Then
                Return 0
            End If
            Return value
        End Function

        Public Shared Function ReturnEmptyIfNull(value As Object) As Object
            If value = DBNull.Value Then
                Return String.Empty
            End If
            If value Is Nothing Then
                Return String.Empty
            End If
            Return value
        End Function


        Public Shared Function ReturnFalseIfNull(value As Object) As Object
            If value = DBNull.Value Then
                Return False
            End If
            If value Is Nothing Then
                Return False
            End If
            Return value
        End Function


        Public Shared Function ReturnDateTimeMinIfNull(value As Object) As Object
            If value = DBNull.Value Then
                Return DateTime.MinValue
            End If
            If value Is Nothing Then
                Return DateTime.MinValue
            End If
            Return value
        End Function


        Public Shared Function ReturnNullIfDbNull(value As Object) As Object
            If value = DBNull.Value Then
                Return ControlChars.NullChar
            End If
            If value Is Nothing Then
                Return ControlChars.NullChar
            End If
            Return value
        End Function

        'This function formats the display-name of a user,
        'and removes unnecessary extra information.
        Public Shared Function FormatUserDisplayName(Optional displayName As String = Nothing, Optional defaultValue As String = "tBill Users", Optional returnNameIfExists As Boolean = False, Optional returnAddressPartIfExists As Boolean = False) As String
            'Get the first part of the Users's Display Name if s/he has a name like this: "firstname lastname (extra text)"
            'removes the "(extra text)" part
            If Not String.IsNullOrEmpty(displayName) Then
                If returnNameIfExists Then
                    Return Regex.Replace(displayName, "\ \(\w{1,}\)", "")
                End If
                Return (displayName.Split(" "c))(0)
            End If
            If returnAddressPartIfExists Then
                Dim emailParts As Object = defaultValue.Split("@"c)
                Return emailParts(0)
            End If
            Return defaultValue
        End Function


        Public Shared Function FormatUserTelephoneNumber(telephoneNumber As String) As String
            Dim result As Object = String.Empty

            If Not String.IsNullOrEmpty(telephoneNumber) Then
                'result = telephoneNumber.ToLower().Trim().Trim('+').Replace("tel:", "");
                result = telephoneNumber.ToLower().Trim().Replace("tel:", "")

                If result.Contains(";") Then
                    If Not result.ToLower().Contains(";ext=") Then
                        result = result.Split(";"c)(0)
                    End If
                End If
            End If

            Return result
        End Function


        Public Shared Function IsValidEmail(emailAddress As String) As Boolean
            Const pattern As String = "\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"

            Return Regex.IsMatch(emailAddress, pattern)
        End Function

        ''' <summary>
        ''' Convert DateTime to string
        ''' </summary>
        ''' <param name="datetTime"></param>
        ''' <param name="excludeHoursAndMinutes">if true it will execlude time from datetime string. Default is false</param>
        ''' <returns></returns>

        Public Shared Function ConvertDate(datetTime As DateTime, Optional excludeHoursAndMinutes As Boolean = False) As String
            If datetTime <> DateTime.MinValue Then
                If excludeHoursAndMinutes Then
                    Return datetTime.ToString("yyyy-MM-dd")
                End If
                Return datetTime.ToString("yyyy-MM-dd HH:mm:ss.fff")
            End If
            Return Nothing
        End Function

        Public Shared Function ConvertSecondsToReadable(secondsParam As Integer) As String
            Dim hours As Object = Convert.ToInt32(Math.Floor(CType((secondsParam / 3600), Double)))
            Dim minutes As Object = Convert.ToInt32(Math.Floor(CType((secondsParam - (hours * 3600)), Double) / 60))
            Dim seconds As Object = secondsParam - (hours * 3600) - (minutes * 60)

            Dim hoursStr As Object = hours.ToString()
            Dim minsStr As Object = minutes.ToString()
            Dim secsStr As Object = seconds.ToString()

            If hours < 10 Then
                hoursStr = "0" + hoursStr
            End If

            If minutes < 10 Then
                minsStr = "0" + minsStr
            End If
            If seconds < 10 Then
                secsStr = "0" + secsStr
            End If

            Return hoursStr + ":"c + minsStr + ":"c + secsStr
        End Function

        Public Shared Function ConvertSecondsToReadable(secondsParam As Long) As String
            Dim hours As Object = Convert.ToInt32(Math.Floor(CType((secondsParam / 3600), Double)))
            Dim minutes As Object = Convert.ToInt32(Math.Floor(CType((secondsParam - (hours * 3600)), Double) / 60))
            Dim seconds As Object = Convert.ToInt32(secondsParam - (hours * 3600) - (minutes * 60))

            Dim hoursStr As Object = hours.ToString()
            Dim minsStr As Object = minutes.ToString()
            Dim secsStr As Object = seconds.ToString()

            If hours < 10 Then
                hoursStr = "0" + hoursStr
            End If

            If minutes < 10 Then
                minsStr = "0" + minsStr
            End If
            If seconds < 10 Then
                secsStr = "0" + secsStr
            End If

            Return hoursStr + ":"c + minsStr + ":"c + secsStr
        End Function
    End Class
End Namespace