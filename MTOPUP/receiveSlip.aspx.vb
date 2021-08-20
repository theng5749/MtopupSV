
Partial Class receiveSlip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim info As String
        Try
            info = Session("bill")
            Dim st() As String = info.Split("|")
            lbMemberName.Text = st(0)
            lbTelephone.Text = st(1)
            lbAddress.Text = st(2)
            lbAmount.Text = st(3)
            lbInvoiceID.Text = st(4)
            lbDatePay.Text = st(5)
            lbBrch.Text = st(6)
            lbUserID.Text = st(7)
            lbDiscount.Text = st(8)
            lbRealPaid.Text = st(9)
            lbCredit.Text = st(10)
        Catch ex As Exception

        End Try
    End Sub
End Class
