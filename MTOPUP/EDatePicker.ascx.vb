
Partial Class EDatePicker
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

    Property TextEnable As Boolean
        Get
            Return ValueDate.Enabled
        End Get
        Set(value As Boolean)
            ValueDate.Enabled = value
        End Set
    End Property
End Class
