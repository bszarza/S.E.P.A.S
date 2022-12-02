
Imports CnSistemas
Public Class Colores_Listar

    Inherits System.Web.UI.Page



    Dim OdDataset As New DataSet



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then



        End If



    End Sub
    'Public Sub CARGARGRILLA()
    '    OdDataset = Ocolores.TraerTodos
    '    GdvColores.DataSource = OdDataset
    '    GdvColores.DataBind()
    'End Sub

    Private Sub GdvColores_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvColores.RowCommand


        Dim indice As Integer = e.CommandArgument

        Dim row As GridViewRow = GdvColores.Rows(indice)
        Dim ID As Integer = Convert.ToInt32(CType(row.FindControl("ID"), Label).Text)


        Select Case e.CommandName

            Case "Eliminar"

                Try

                    lblALIQUIDACION_ELIMINAR.Text = "Esta seguro que desa Eliminar el Registro!!!, <br><font color='red'>Se van a Eliminar el Color Cargado!!!</font>"
                    ColorEliminar.Text = Convert.ToString(CType(row.FindControl("ID"), Label).Text)

                    If (Not ClientScript.IsStartupScriptRegistered("fnEliminarColor")) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnEliminarColor", "fnEliminarColor();", True)

                    End If

                Catch ex As Exception

                    ' Mostrar_Mensaje("Ha ocurrido la Siguiente Excepción: " & ex.Message)

                Finally




                End Try



            Case "Editar"

                Try

                    lblALIQUIDACION_ELIMINAR.Text = "Esta seguro que desa Editar el Registro!!!, <br><font color='red'>Se va a  Editar el Color Cargado!!!</font>"

                    txtIdColorEditar.Text = Convert.ToString(CType(row.FindControl("ID"), Label).Text)
                    TxtdescripcionColorEditar.Text = Convert.ToString(CType(row.FindControl("DESCRIPCION"), Label).Text)

                    If (Not ClientScript.IsStartupScriptRegistered("fnEditarColor")) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnEditarColor", "fnEditarColor();", True)

                    End If

                Catch ex As Exception

                    ' Mostrar_Mensaje("Ha ocurrido la Siguiente Excepción: " & ex.Message)

                Finally




                End Try



        End Select

    End Sub

    Private Sub BtnEliminarColor_Click(sender As Object, e As EventArgs) Handles BtnEliminarColor.Click
        Try


            'Ocolores.Eliminar(ColorEliminar.Text, Session("usuarios"))
            'ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('Se Elimino Correctamente');", True)
            MostrarAlerta("Se elimino correctamente")





            'MsgBox("se Elimino Correctamente el Color")

        Catch ex As Exception
            MostrarAlerta("No se pudo Eliminar")

            'ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se pudo Eliminar');", True)
            'MsgBox("No se Pudo Eliminar el Color" & ex.Message)

        End Try

        Response.Redirect("Colores_listar.aspx")


    End Sub

    Private Sub BtnEditarColor_Click(sender As Object, e As EventArgs) Handles BtnEditarColor.Click


        Try
            If TxtdescripcionColorEditar.Text = "" Then


                MostrarAlerta("No Puede estar vacio")

                'If (Not ClientScript.IsStartupScriptRegistered("fnAlerta")) Then

                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnAlerta", "fnAlerta();", True)

                'End If
                'Exit Sub
            End If

            'Ocolores.Modificar(txtIdColorEditar.Text, TxtdescripcionColorEditar.Text, Session("usuarios"))
            MostrarAlerta("Se modifico correctamente")
            'ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se Actualizo Correctamente el Color');", True)

            'CARGARGRILLA()

            'MsgBox("se Actualizo Correctamente el Color")

        Catch ex As Exception
            MostrarAlerta("No se pudo modificar")
            'ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se Actualizo el Color');", True)


        End Try
        Response.Redirect("Colores_listar.aspx")

    End Sub

    Private Sub BtnAgregarColor_Click(sender As Object, e As EventArgs) Handles BtnAgregarColor.Click


        Try


            'Ocolores.Agregar(TxtColorAgregar.Text, Session("usuarios"))

            MostrarAlerta("Se Agrego correctamente")

            'CARGARGRILLA()


        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se Pudo Agregar el Color');", True)
            MostrarAlerta("No se pudo agregar")


        End Try
        Response.Redirect("Colores_listar.aspx")


    End Sub
    Private Sub MostrarAlerta(ByVal strmensaje As String)

        mensaje.Value = strmensaje

        If (Not ClientScript.IsStartupScriptRegistered("fnAlerta")) Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnAlerta", "fnAlerta();", True)

        End If
    End Sub
End Class