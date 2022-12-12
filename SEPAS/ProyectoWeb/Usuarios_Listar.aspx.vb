Imports CnSistemas
Public Class Usuarios_Listar
    Inherits System.Web.UI.Page

    'Dim oFiltrado As New ServidorDatosNet_GNA.svr("SEPAS_DES", "Usuarios", "USUARIO")
    Dim oUser As String = 40083172
    Dim oUsuarios As New Usuarios
    Dim OPerfil As New Perfiles
    Dim odDataset As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then

                CARGARGRILLA()
                'cargoGenero()
                'cargoCursos()
                'cargoCursos2()
                'cargoGenero2()
                'AspirantesFilCurso()
                prDrpPerfil()
                prDrpAgregarPerfil()
                'CargarAspirantes()
            End If

        Catch ex As Exception
            Response.Write(ex)
        End Try

    End Sub
    'Public Sub cargoGenero()
    '    Dim oGenero As New TipoGeneros
    '    Dim oDs As New DataSet
    '    oDs = oGenero.EjecutarSp("OT", oUser)
    '    DDLGenero.DataSource = oDs
    '    DDLGenero.DataTextField = "Descripcion"
    '    DDLGenero.DataValueField = "ID"
    '    DDLGenero.DataBind()
    'End Sub
    'Public Sub cargoGenero2()
    '    Dim oGenero As New TipoGeneros
    '    Dim oDs As New DataSet
    '    oDs = oGenero.EjecutarSp("OT", oUser)
    '    DDLGeneroEditar.DataSource = oDs
    '    DDLGeneroEditar.DataTextField = "Descripcion"
    '    DDLGeneroEditar.DataValueField = "ID"
    '    DDLGeneroEditar.DataBind()
    'End Sub
    'Public Sub cargoCursos()
    '    Dim oDs As New DataSet
    '    'oDs = OCursos.EjecutarSp("OT", oUser)
    '    DDLCurso.DataSource = oDs
    '    DDLCurso.DataTextField = "Descripcion"
    '    DDLCurso.DataValueField = "ID"
    '    DDLCurso.DataBind()
    'End Sub


    Public Sub prDrpPerfil()
        Dim oDs As New DataSet
        oDs = OPerfil.EjecutarSp("OT", oUser)
        DrpPerfil.DataSource = oDs
        DrpPerfil.DataTextField = "Descripcion"
        DrpPerfil.DataValueField = "ID"
        DrpPerfil.DataBind()
    End Sub
    Public Sub prDrpAgregarPerfil()
        Dim oDs As New DataSet
        oDs = OPerfil.EjecutarSp("OT", oUser)
        DrpAgregarPerfil.DataSource = oDs
        DrpAgregarPerfil.DataTextField = "Descripcion"
        DrpAgregarPerfil.DataValueField = "ID"
        DrpAgregarPerfil.DataBind()
    End Sub
    'Public Sub CargarAspirantes()
    '    Dim oAspirantes As New Aspirantes
    '    Dim oDs As New DataSet
    '    oDs = oAspirantes.EjecutarSp("OT", oUser)
    '    DDLAspirantesFil.DataSource = oDs
    '    DDLAspirantesFil.DataSource = oDs
    '    DDLAspirantesFil.DataTextField = "Nombre"
    '    DDLAspirantesFil.DataValueField = "IdCurso"
    '    DDLAspirantesFil.DataBind()
    'End Sub
    'Public Sub AspirantesFilCurso()

    '    odDataset = oFiltrado.Filtrado(CInt(DrpCursos.SelectedValue))
    '    GdvAspirantes.DataSource = odDataset
    '    GdvAspirantes.DataBind()
    'End Sub
    'Public Sub cargoCursos2()

    '    Dim oDs As New DataSet
    '    oDs = OCursos.EjecutarSp("OT", oUser)
    '    DDLAspirantesFil.DataSource = oDs
    '    DDLCursoEditar.DataSource = oDs
    '    DDLCursoEditar.DataTextField = "Descripcion"
    '    DDLCursoEditar.DataValueField = "ID"
    '    DDLCursoEditar.DataBind()
    'End Sub
    Public Sub CARGARGRILLA()
        Dim oDsGrilla As New DataSet
        oDsGrilla = oUsuarios.EjecutarSp("OT", CStr(Session("Usuario")))
        GdvUsuarios.DataSource = oDsGrilla
        GdvUsuarios.DataBind()
    End Sub

    Private Sub GdvColores_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvUsuarios.RowCommand


        Dim indice As Integer = e.CommandArgument

        Dim row As GridViewRow = GdvUsuarios.Rows(indice)
        Dim ID As Integer = Convert.ToInt32(CType(row.FindControl("ID"), Label).Text)


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

                    txtId.Text = Convert.ToString(CType(row.FindControl("ID"), Label).Text)
                    TxtNombreEditar.Text = Convert.ToString(CType(row.FindControl("NOMBRE"), Label).Text)
                    TxtApellidoEditar.Text = Convert.ToString(CType(row.FindControl("APELLIDO"), Label).Text)
                    TxtDniEditar.Text = Convert.ToString(CType(row.FindControl("DNI"), Label).Text)
                    'TxtContrasenia.Text = Convert.ToString(CType(row.FindControl("Contrasenia"), Label).Text)
                    'DrpAgregarPerfil.Text = Convert.ToString(CType(row.FindControl("IdPerfil"), Label).Text)

                    'TxtdescripcionColorEditar.Text = Convert.ToString(CType(row.FindControl("Cantidad_Ingreso"), Label).Text)
                    'TxtCantidadIngreso.Text = Convert.ToString(CType(row.FindControl("Precio_Compra"), Label).Text)

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


            ' OAspirantes.Eliminar(ColorEliminar.Text, Session("usuarios"))
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se elimino Correctamente');", True)
            CARGARGRILLA()




        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se pudo eliminar');", True)


        End Try

        Response.Redirect("Aspirantes_listar.aspx")


    End Sub

    Private Sub BtnEditarColor_Click(sender As Object, e As EventArgs) Handles BtnEditarColor.Click

        Dim oDs As New DataSet
        Try


            'oDs = OAspirantes.EjecutarSp("AC", txtId.Text, TxtNombreEditar.Text, TxtApellidoEditar.Text, TxtDniEditar.Text, TxtCeEditar.Text, DDLGeneroEditar.SelectedValue, DDLCursoEditar.SelectedValue, Session("usuarios"))

            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se Actualizo Correctamente el Color');", True)
            CARGARGRILLA()


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert(' No se pudo actualizar');", True)


        End Try
        Response.Redirect("Usuarios_listar.aspx")

    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click


        Try
            If TxtNombre.Text = "" Or TxtApellido.Text = "" Or TxtDni.Text = "" Or DrpAgregarPerfil.SelectedValue = "" Then
                MsgBox("Campos vacios")
            ElseIf IsNumeric(TxtNombre.Text) = True Or IsNumeric(TxtApellido.Text) = True Or IsNumeric(TxtDni.Text) = False Or IsNumeric(DrpAgregarPerfil.Text) = False Then
                MsgBox("Datos incorretos")
            Else
                oUsuarios.EjecutarSp("IN", oUser, TxtNombre.Text, TxtApellido.Text, TxtDni.Text, TxtContrasenia.Text, DrpAgregarPerfil.SelectedValue, Session("usuarios"))
                ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('se agrego Correctamente');", True)
                CARGARGRILLA()
                prDrpAgregarPerfil()
            End If




        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "msg", "alert('No se pudo agregar');", True)


        End Try
        Response.Redirect("Usuarios_listar.aspx")


    End Sub

    Private Sub DDLAspirantesFil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpPerfil.SelectedIndexChanged
        prDrpPerfil()
    End Sub

End Class