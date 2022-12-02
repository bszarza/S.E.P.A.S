Imports CnSistemas
Public Class Login
    Inherits System.Web.UI.Page
    Dim opersonas As New CnPersonas
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnIngresar_Click(sender As Object, e As EventArgs) Handles BtnIngresar.Click
        Try


            If txtusuario.Text = "" Or txtclave.Text = "" Then
                MsgBox("EL CAMPO NO PUEDE ESTAR VACIO")

            Else
                Dim ods As New DataSet
                ods = opersonas.EjecutarSp("fil", CInt(txtusuario.Text), txtclave.Text)
                If ods.Tables(0).Rows.Count > 0 Then
                    Session("Usuarios") = txtusuario.Text
                    Response.Redirect("Aspirantes_Listar.aspx")
                Else
                    MsgBox("USUARIO/CONTRASEÑA INCORRECTA")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class