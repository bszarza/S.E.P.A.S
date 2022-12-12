Imports CnSistemas
Public Class Cronograma
    Inherits System.Web.UI.Page
    Dim Ocronograma As New CnSistemas.Cronograma
    Dim odDataset As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            CARGARGRILLA()
            cargoCursos()
            CronogramaFil()
        End If

    End Sub
    Public Sub CARGARGRILLA()
        'odDataset = Ocronograma.TraerTodos
        GdvCronograma.DataSource = odDataset
        GdvCronograma.DataBind()
    End Sub
    Public Sub cargoCursos()
        ' Dim oCursos As New CnCursos
        Dim oDs As New DataSet
        '  oDs = oCursos.TraerTodos()
        DDLCursoFil.DataSource = oDs
        DDLCursoFil.DataTextField = "Descripcion"
        DDLCursoFil.DataValueField = "ID"
        DDLCursoFil.DataBind()
    End Sub
    Public Sub CronogramaFil()

        ' odDataset = Ocronograma.Filtrado(cint(DDLCursoFil.SelectedValue))
        GdvCronograma.DataSource = odDataset
        GdvCronograma.DataBind()
    End Sub

    Private Sub GdvCronograma_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvCronograma.RowCommand

        Dim indice As Integer = e.CommandArgument

        Dim row As GridViewRow = GdvCronograma.Rows(indice)
        Dim ID As Integer = Convert.ToInt32(CType(row.FindControl("ID"), Label).Text)
        'Dim Horario As Integer = Convert.ToInt32(CType(row.FindControl("Horario"), Label).Text)


        Select Case e.CommandName

            Case "Eliminar"

                Try

                    lblALIQUIDACION_ELIMINAR.Text = "Esta seguro que desea Eliminar el Registro!!!, <br><font color='red'>Se va a Eliminar el Registro Cargado!!!</font>"
                    ColorEliminar.Text = Convert.ToString(CType(row.FindControl("ID"), Label).Text)
                    TxtApellidoEliminar.Text = Convert.ToString(CType(row.FindControl("APELLIDO"), Label).Text)

                    If (Not ClientScript.IsStartupScriptRegistered("fnEliminarColor")) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnEliminarColor", "fnEliminarColor();", True)

                    End If

                Catch ex As Exception

                    'Mostrar_Mensaje("Ha ocurrido la Siguiente Excepción: " & ex.Message)

                Finally




                End Try



            Case "Editar"

                Try

                    lblALIQUIDACION_ELIMINAR.Text = "Esta seguro que desa Editar el Registro!!!, <br><font color='red'>Se va a  Editar el Color Cargado!!!</font>"

                    txtIdEditar.Text = Convert.ToString(CType(row.FindControl("ID"), Label).Text)
                    TxtHorario.Text = Convert.ToString(CType(row.FindControl("Horario"), Label).Text)
                    TxtLunes.Text = Convert.ToString(CType(row.FindControl("Lunes"), Label).Text)
                    TxtMartes.Text = Convert.ToString(CType(row.FindControl("Martes"), Label).Text)
                    TxtMiercoles.Text = Convert.ToString(CType(row.FindControl("Miercoles"), Label).Text)
                    TxtJueves.Text = Convert.ToString(CType(row.FindControl("Jueves"), Label).Text)
                    TxtViernes.Text = Convert.ToString(CType(row.FindControl("Viernes"), Label).Text)
                    'TxtdescripcionColorEditar.Text = Convert.ToString(CType(row.FindControl("Cantidad_Ingreso"), Label).Text)
                    'TxtCantidadIngreso.Text = Convert.ToString(CType(row.FindControl("Precio_Compra"), Label).Text)

                    If (Not ClientScript.IsStartupScriptRegistered("fnEditarCronograma")) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnEditarCronograma", "fnEditarCronograma();", True)

                    End If

                Catch ex As Exception

                    'Mostrar_Mensaje("Ha ocurrido la Siguiente Excepción: " & ex.Message)
                    Response.Write(ex.Message)

                Finally




                End Try



        End Select

    End Sub

    Private Sub BtnEliminarColor_Click(sender As Object, e As EventArgs) Handles BtnEliminarColor.Click
        Try


            'Ovehiculos.Eliminar(ColorEliminar.Text, Session("usuarios"))
            'ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se elimino Correctamente');", True)
            'CARGARGRILLA()




        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se pudo eliminar');", True)


        End Try

        Response.Redirect("Aspirantes_listar.aspx")


    End Sub

    Private Sub BtnEditarColor_Click(sender As Object, e As EventArgs) Handles BtnEditarColor.Click

        Try


            Ocronograma.EjecutarSp("AC", txtIdEditar.Text, TxtLunes.Text, TxtMartes.Text, TxtMiercoles.Text, TxtJueves.Text, TxtViernes.Text, Session("usuarios"))

            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se Actualizo Correctamente el Color');", True)
            CARGARGRILLA()


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert(' No se pudo actualizar');", True)

            Response.Write(ex.Message)
        End Try
        Response.Redirect("Cronograma.aspx")

    End Sub

    Private Sub BtnAgregarColor_Click(sender As Object, e As EventArgs) Handles BtnAgregarColor.Click

        Try


            ' Ovehiculos.Agregar(TxtColorAgregar.Text, Session("usuarios"))
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se agrego Correctamente');", True)
            CARGARGRILLA()


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se pudo agregar');", True)


        End Try
        Response.Redirect("Aspirantes_listar.aspx")


    End Sub

    Private Sub DDLCursoFil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDLCursoFil.SelectedIndexChanged
        CronogramaFil()

    End Sub
End Class