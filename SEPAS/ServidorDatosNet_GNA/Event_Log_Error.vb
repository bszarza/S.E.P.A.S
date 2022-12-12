Option Explicit On
Option Strict On
Imports System
Imports System.Diagnostics
Imports System.Threading
Public Class Event_Log_Error
    'Public Shared Sub Main(ByVal App As String, ByVal Usuario As String, ByVal msg As String)
    '    Dim EvtLogSource As String = App
    '    Const EvtLogName As String = "Eventos"
    '    Dim EventLog1 As New EventLog(EvtLogName, ".", EvtLogSource)
    '    If Not EventLog.SourceExists(App) Then
    '        If Not System.Diagnostics.EventLog.SourceExists(EvtLogSource) Then
    '            System.Diagnostics.EventLog.CreateEventSource(EvtLogSource, EvtLogName)
    '        End If
    '        EventLog1.WriteEntry(msg)
    '        EventLog1.Source = EvtLogSource
    '        Return
    '    End If
    '    Dim myLog As New EventLog()
    '    myLog.Source = App
    '    myLog.WriteEntry(msg)
    'End Sub


    Public Enum ErrorType
        Critical
        Minor
        Information
    End Enum

    'Private Const source = "Sample App"
    'Private Const log = "Application"

    '  Private Shared Sub LogError(ByVal _App As String, ByVal msg As String, ByVal type As ErrorType, ByVal ex As Exception)
    '  End Sub

    Public Shared Sub Main(ByVal _App As String, ByVal msg As String, ByVal ex As Exception)
        Try
            If Not EventLog.SourceExists(_App) Then
                EventLog.CreateEventSource(_App, msg)
            End If

            'If Type = ErrorType.Information Then
            'EventLog.WriteEntry(_App, ex.ToString(), EventLogEntryType.Information)
            'ElseIf Type = ErrorType.Minor Then
            ' EventLog.WriteEntry(_App, ex.ToString(), EventLogEntryType.Warning)
            ' Else
            EventLog.WriteEntry(_App, ex.ToString(), EventLogEntryType.Error)
            'End If
        Catch
        End Try

    End Sub

End Class
