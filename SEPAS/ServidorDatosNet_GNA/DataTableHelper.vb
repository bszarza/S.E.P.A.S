Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports System

Public Class DataTableHelper
    Public Shared Function ExportDataTable(table As DataTable) As List(Of List(Of Object))
        Dim result As New List(Of List(Of Object))
        For Each row As DataRow In table.Rows
            Dim values As New List(Of Object)
            For Each column As DataColumn In table.Columns
                If row.IsNull(column) Then
                    values.Add(Nothing)
                Else
                    values.Add(row.Item(column))
                End If
            Next
            result.Add(values)
        Next
        Return result
    End Function

End Class