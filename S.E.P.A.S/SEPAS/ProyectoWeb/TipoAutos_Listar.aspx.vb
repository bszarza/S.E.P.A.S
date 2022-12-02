
Imports CnSistemas
Public Class TipoAutos_Listar

    Inherits System.Web.UI.Page


    Dim OTipoAutos As New CnGeneros
    Dim OdDataset As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not IsPostBack Then

            CARGARGRILLA()

        End If



    End Sub
    Public Sub CARGARGRILLA()
        OdDataset = OTipoAutos.TraerTodos
        GdvTipoAutos.DataSource = OdDataset
        GdvTipoAutos.DataBind()
    End Sub

    Private Sub GdvColores_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvTipoAutos.RowCommand


        Dim indice As Integer = e.CommandArgument

        Dim row As GridViewRow = GdvTipoAutos.Rows(indice)
        Dim ID As Integer = Convert.ToInt32(CType(row.FindControl("ID"), Label).Text)


        Select Case e.CommandName

            Case "Eliminar"

                Try

                    lblALIQUIDACION_ELIMINAR.Text = "Esta seguro que desea Eliminar el Registro!!!, <br><font color='red'>Se va Eliminar el registro Cargado!!!</font>"
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


            OTipoAutos.Eliminar(ColorEliminar.Text, Session("usuarios"))
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se Elimino Correctamente');", True)
            CARGARGRILLA()


            MsgBox("se Elimino Correctamente el Color")

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se Pudo Eliminar');", True)


        End Try

        Response.Redirect("Colores_listar.aspx")


    End Sub

    Private Sub BtnEditarColor_Click(sender As Object, e As EventArgs) Handles BtnEditarColor.Click


        Try


            OTipoAutos.Modificar(txtIdColorEditar.Text, TxtdescripcionColorEditar.Text, Session("usuarios"))

            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se Actualizo Correctamente');", True)
            CARGARGRILLA()



        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se Pudo Actualizar');", True)


        End Try
        Response.Redirect("TipoAutos_listar.aspx")

    End Sub

    Private Sub BtnAgregarColor_Click(sender As Object, e As EventArgs) Handles BtnAgregarColor.Click


        Try


            OTipoAutos.Agregar(TxtColorAgregar.Text, Session("usuarios"))
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('Se agrego Correctamente');", True)
            CARGARGRILLA()



        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se pudo agregar);", True)


        End Try
        Response.Redirect("TipoAutos_listar.aspx")


    End Sub

End Class