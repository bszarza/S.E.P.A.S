'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Aspirantes_Listar

    '''<summary>
    '''Control DDLAspirantesFil.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDLAspirantesFil As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control GdvAspirantes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GdvAspirantes As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control IDEliminar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents IDEliminar As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control TxtNombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtNombre As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtApellido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtApellido As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtDni.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDni As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtCe.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCe As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DDLGenero.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDLGenero As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDLCurso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDLCurso As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control BtnAgregarColor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnAgregarColor As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtId.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtId As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtNombreEditar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtNombreEditar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtApellidoEditar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtApellidoEditar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtDniEditar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDniEditar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtCeEditar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCeEditar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DDLGeneroEditar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDLGeneroEditar As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDLCursoEditar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDLCursoEditar As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control BtnEditarColor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnEditarColor As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control Div5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Div5 As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control lblALIQUIDACION_ELIMINAR.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblALIQUIDACION_ELIMINAR As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control ColorEliminar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ColorEliminar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxtApellidoEliminar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtApellidoEliminar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control BtnEliminarColor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnEliminarColor As Global.System.Web.UI.WebControls.Button
End Class
