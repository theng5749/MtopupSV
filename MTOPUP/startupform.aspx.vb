
Partial Class startupform
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Session.Clear()
        Session.RemoveAll()
    End Sub
End Class
