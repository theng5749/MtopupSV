Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.Web.Configuration
Imports nickDevClass
Partial Class proHistory
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim cn As New OracleConnection
    Dim cm As New OracleCommand
    Dim fc As New SubFunction
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("username").ToString <> Nothing Or Session("username").ToString <> "" Then
                lbUserName.Text = Session("username").ToString
                If Session("userlevel").ToString = "9" Then
                    _menu()
                    menuMember.Visible = True
                    menuRecharge.Visible = True
                    menuReceive.Visible = True
                    menuReprint.Visible = True
                    menuReceiveHistory.Visible = True
                    menuHistoryL1.Visible = True
                    menuHistory.Visible = True
                    menuUser.Visible = True
                    menuCheckApprove.Visible = True
                    menuBranch.Visible = True
                    menuDiscount.Visible = True
                    menuDiscountSale.Visible = True
                    menuRechargeHistory.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                    cbManage.Visible = True
                    cbReport.Visible = True
                    menuProHis.Visible = True
                    menuPromotion.Visible = True
                ElseIf Session("userlevel").ToString = "0" Then
                    _menu()
                    menuMember.Visible = True
                    'menuRecharge.Visible = True
                    menuReceive.Visible = True
                    menuReprint.Visible = True
                    menuReceiveHistory.Visible = True
                    menuHistory.Visible = True
                    menuInvoice.Visible = True
                    menuCredit.Visible = True
                    cbReport.Visible = True
                ElseIf Session("userlevel").ToString = "7" Then
                    _menu()
                    cbReport.Visible = True
                    menuHistory.Visible = True
                ElseIf Session("userlevel").ToString = "6" Then
                    _menu()
                    cbReport.Visible = True
                    menuHistory.Visible = True
                ElseIf Session("userlevel").ToString = "8" Then
                    Response.Redirect("Default.aspx")
                Else
                    Response.Redirect("Default.aspx")
                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Default.aspx")
        End Try

        If Not IsPostBack Then
            EDatePicker1.DateValue = Today.Date
            EDatePicker2.DateValue = Today.Date
            lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
            lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
            _RequestHistory()
        End If
        _gridColumnName()
    End Sub

    Sub _menu()
        menuMember.Visible = False
        menuRecharge.Visible = False
        menuReceive.Visible = False
        menuReprint.Visible = False
        menuReceiveHistory.Visible = False
        menuHistoryL1.Visible = False
        menuHistory.Visible = False
        menuUser.Visible = False
        menuBranch.Visible = False
        menuDiscount.Visible = False
        menuDiscountSale.Visible = False
        menuCredit.Visible = False
        menuCheckApprove.Visible = False
        menuInvoice.Visible = False
        cbManage.Visible = False
        cbReport.Visible = False
        menuProHis.Visible = False
        menuPromotion.Visible = False
    End Sub

    Sub _gridColumnName()
        If gridHistory.GridControlElement.Rows.Count <> 0 Then
            gridHistory.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridHistory.GridControlElement.HeaderRow.Cells(4).Text = "ເບີໂທລະສັບ"
            gridHistory.GridControlElement.HeaderRow.Cells(5).Text = "ມູນຄ່າໂທ ( % )"
            gridHistory.GridControlElement.HeaderRow.Cells(6).Text = "ໂບນັດ ( % )"
            gridHistory.GridControlElement.HeaderRow.Cells(7).Text = "ດາຕ້າ ( % )"
            gridHistory.GridControlElement.HeaderRow.Cells(8).Text = "ເວລາທີ່ໄດ້"
            gridHistory.GridControlElement.HeaderRow.Cells(9).Text = "ເລກທີການໂອນ"
        End If
    End Sub


    Private Sub _RequestHistory()
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yyyy") '
        Dim telephone As String = txtTelephone.Value.ToString.Trim
        lbDate1.Text = d1
        lbDate2.Text = d2
        Dim sql As String = ""
        If txtTelephone.Value.ToString.Trim = "" Then
            sql = String.Format("select  msisdn, balance, bonus, data, record_date, trans_id " _
           & " from mtopup.tbl_promotion_log where record_date between '{0}' and '{1}' order by trans_id desc", d1, d2)
        Else
            sql = String.Format("select  msisdn, balance, bonus, data, record_date, trans_id " _
          & " from mtopup.tbl_promotion_log where msisdn='" & telephone & "' and record_date between '{0}' and '{1}' order by trans_id desc", d1, d2)
        End If

        cn = New OracleConnection
        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da = New OracleDataAdapter(sql, cn)
            da.Fill(ds, "history")
            cn.Close()
            gridHistory.RequestSource = ds.Tables("history")
            gridHistory.GridControlElement.PageIndex = 0
            gridHistory.DataBind()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        Finally
            cn.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
        _gridColumnName()
    End Sub

    Protected Sub gridHistory_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridHistory.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False

            e.Row.Cells(4).Font.Bold = True

            If e.Row.Cells(5).Text <> "0" Then
                e.Row.Cells(5).Font.Bold = True
                e.Row.Cells(5).ForeColor = Drawing.Color.OrangeRed
            End If

            If e.Row.Cells(6).Text <> "0" Then
                e.Row.Cells(6).Font.Bold = True
                e.Row.Cells(6).ForeColor = Drawing.Color.OrangeRed
            End If
            If e.Row.Cells(7).Text <> "0" Then
                e.Row.Cells(7).Font.Bold = True
                e.Row.Cells(7).ForeColor = Drawing.Color.OrangeRed
            End If

        End If
    End Sub


    Protected Sub gridHistory_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridHistory.GridViewPageIndexChanged
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yyyy") '
        Dim telephone As String = txtTelephone.Value.ToString.Trim
        lbDate1.Text = d1
        lbDate2.Text = d2
        Dim sql As String = ""
        If txtTelephone.Value.ToString.Trim = "" Then
            sql = String.Format("select  msisdn, balance, bonus, data, record_date, trans_id " _
           & " from mtopup.tbl_promotion_log where record_date between '{0}' and '{1}' order by trans_id desc", d1, d2)
        Else
            sql = String.Format("select  msisdn, balance, bonus, data, record_date, trans_id " _
          & " from mtopup.tbl_promotion_log where msisdn='" & telephone & "' and record_date between '{0}' and '{1}' order by trans_id desc", d1, d2)
        End If

        cn = New OracleConnection
        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da = New OracleDataAdapter(sql, cn)
            da.Fill(ds, "history")
            cn.Close()
            gridHistory.RequestSource = ds.Tables("history")
            gridHistory.GridControlElement.PageIndex = e.NewPageIndex
            gridHistory.DataBind()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        Finally
            cn.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
        _gridColumnName()
    End Sub

    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        _RequestHistory()
    End Sub
End Class
