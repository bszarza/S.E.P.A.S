Public Class MasterPager
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub MostrarSucces(ByVal strmensaje As String)

        lblmensaje.Value = strmensaje

        If (Not Page.ClientScript.IsStartupScriptRegistered("fnMostrarSucces")) Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnMostrarSucces", "fnMostrarSucces();", True)

        End If
    End Sub
    Public Sub MostrarWarning(ByVal strmensaje As String)

        lblmensaje.Value = strmensaje

        If (Not Page.ClientScript.IsStartupScriptRegistered("fnMostrarWarning")) Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnMostrarWarning", "fnMostrarWarning();", True)

        End If
    End Sub
    Public Sub MostrarInfo(ByVal strmensaje As String)

        lblmensaje.Value = strmensaje

        If (Not Page.ClientScript.IsStartupScriptRegistered("fnMostrarInfo")) Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "fnMostrarInfo", "fnMostrarInfo();", True)

        End If
    End Sub
    Public Sub MostrarDanger(ByVal strmensaje As String)

        lblmensaje.Value = strmensaje

        If (Not Page.ClientScript.IsStartupScriptRegistered("MostrarDanger")) Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "MostrarDanger", "MostrarDanger();", True)

        End If
    End Sub
End Class