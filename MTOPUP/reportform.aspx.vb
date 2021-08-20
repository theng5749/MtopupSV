
Partial Class reportform
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("username").ToString <> Nothing Or Session("username").ToString <> "" Then
                lbname.Text = Session("username").ToString.ToUpper
            Else
                Response.Redirect("startupform.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("startupform.aspx")
        End Try
    End Sub
End Class
