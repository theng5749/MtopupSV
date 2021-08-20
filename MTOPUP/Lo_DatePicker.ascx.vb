
Partial Class Lo_DatePicker
    Inherits System.Web.UI.UserControl
    Property DateValue As Date
        Get
            Dim _return As Date = ValueDate.Text
            Return _return
        End Get
        Set(value As Date)
            ValueDate.Text = value
        End Set
    End Property
End Class
