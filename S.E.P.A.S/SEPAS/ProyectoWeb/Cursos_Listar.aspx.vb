
Imports CnSistemas
Public Class TipoModelo_Listar

    Inherits System.Web.UI.Page


    Dim OtipoModelo As New CnCronograma

    Dim OdDataset As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            CARGARGRILLA()

        End If



    End Sub
    Public Sub CARGARGRILLA()
        Dim OCursos As New CnCursos
        Dim odDataset As New DataSet
        odDataset = OCursos.TraerTodos
        GdvTipoModelo.DataSource = OdDataset
        GdvTipoModelo.DataBind()
    End Sub

    Private Sub GdvColores_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvTipoModelo.RowCommand


        Dim indice As Integer = e.CommandArgument

        Dim row As GridViewRow = GdvTipoModelo.Rows(indice)
        Dim ID As Integer = Convert.ToInt32(CType(row.FindControl("ID"), Label).Text)


        Select Case e.CommandName

            Case "Eliminar"

                Try

                    lblALIQUIDACION_ELIMINAR.Text = "Esta seguro que desa Eliminar el Registro!!!, <br><font color='red'>Se va Eliminar el registro Cargado!!!</font>"
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


            OtipoModelo.Eliminar(ColorEliminar.Text, Session("usuarios"))
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se elimino Correctamente');", True)
            CARGARGRILLA()




        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se Pudo Eliminar');", True)


        End Try

        Response.Redirect("Cursos_listar.aspx")


    End Sub

    Private Sub BtnEditarColor_Click(sender As Object, e As EventArgs) Handles BtnEditarColor.Click


        Try


            OtipoModelo.Modificar(txtIdColorEditar.Text, TxtdescripcionColorEditar.Text, Session("usuarios"))
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se Actualizo Correctamente');", True)

            CARGARGRILLA()



        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se pudo Actualizar');", True)

        End Try
        Response.Redirect("Cursos_listar.aspx")

    End Sub

    Private Sub BtnAgregarColor_Click(sender As Object, e As EventArgs) Handles BtnAgregarColor.Click


        Try


            OtipoModelo.Agregar(TxtColorAgregar.Text, Session("usuarios"))
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se agrego Correctamente');", True)
            CARGARGRILLA()



        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se Pudo Agregar');", True)


        End Try
        Response.Redirect("Cursos_listar.aspx")


    End Sub

End Class