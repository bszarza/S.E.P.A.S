Imports CnSistemas
Public Class Login
    Inherits System.Web.UI.Page
    Dim oUsuarios As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Usuarios", "USUARIO")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'ESTE COMENTARIO ES UNA PRUEBA DE LOS CAMBIOS
    Protected Sub BtnIngresar_Click(sender As Object, e As EventArgs) Handles BtnIngresar.Click
        Try


            If txtusuario.Text = "" Or txtclave.Text = "" Then
                MsgBox("EL CAMPO NO PUEDE ESTAR VACIO")

            Else
                Dim ods As New DataSet
                ods = oUsuarios.TraerUno(CInt(txtusuario.Text), txtclave.Text)
                If ods.Tables(0).Rows.Count > 0 Then
                    Session("Usuario") = txtusuario.Text
                    'fn_global.DNI = txtusuario.Text
                    Response.Redirect("Aspirantes_Listar.aspx")
                Else
                    MsgBox("USUARIO/CONTRASEÑA INCORRECTA")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class