Imports CnSistemas
Public Class Vehiculos_Alta
    Inherits System.Web.UI.Page
    Dim ovehiculos As New CnVehiculos
    Dim odDataset As New DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            'cargoMarca()
            'cargoModelo()
            'cargoColor()
            'cargoTipoAuto()
        End If
    End Sub
    Public Sub cargoMarca()
        Dim oMarca As New CnMaterias
        Dim oDs As New DataSet
        oDs = oMarca.TraerTodos()
        DDLMarca.DataSource = oDs
        DDLMarca.DataTextField = "DESCRIPCION"
        DDLMarca.DataValueField = "ID"
        DDLMarca.DataBind()
    End Sub
    Public Sub cargoModelo()
        Dim oModelo As New CnCronograma
        Dim oDs As New DataSet
        oDs = oModelo.TraerTodos()
        DDLModelo.DataSource = oDs
        DDLModelo.DataTextField = "DESCRIPCION"
        DDLModelo.DataValueField = "ID"
        DDLModelo.DataBind()
    End Sub
    Public Sub cargoColor()
        ' Dim oColor As New CnColor
        Dim oDs As New DataSet
        ' oDs = oColor.TraerTodos()
        DDLColor.DataSource = oDs
        DDLColor.DataTextField = "DESCRIPCION"
        DDLColor.DataValueField = "ID"
        DDLColor.DataBind()
    End Sub
    Public Sub cargoTipoAuto()
        Dim oTipoAuto As New CnGeneros
        Dim oDs As New DataSet
        oDs = oTipoAuto.TraerTodos()
        DDLTipoauto.DataSource = oDs
        DDLTipoauto.DataTextField = "DESCRIPCION"
        DDLTipoauto.DataValueField = "ID"
        DDLTipoauto.DataBind()
    End Sub

    Protected Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click
        Dim odataset As New DataSet
        Dim oVehiculo As New CnVehiculos
        odataset = oVehiculo.EjecutarSp("IN", CInt(DDLColor.SelectedValue), CInt(DDLTipoauto.SelectedValue), CInt(DDLMarca.SelectedValue), CInt(DDLModelo.SelectedValue), TxtDescripcion.Text, TxtPventa.Text, TxtCantidad.Text, Session("Usuarios"))
    End Sub
End Class