Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Microsoft.VisualBasic
Imports ClosedXML.Excel
Imports System.IO
Imports nickDevClass
Partial Class receiveTotal
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim cn As New OracleConnection
    Dim fc As New SubFunction
    Dim cm As New OracleCommand

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
                    menuBranch.Visible = True
                    menuDiscount.Visible = True
                    menuCheckApprove.Visible = True
                    menuDiscountSale.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                    cbManage.Visible = True
                    cbReport.Visible = True
                    menuProHis.Visible = True
                    menuPromotion.Visible = True
                    menuRechargeHistory.Visible = True
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
                    Response.Redirect("Default.aspx")
                ElseIf Session("userlevel").ToString = "6" Then
                    Response.Redirect("Default.aspx")
                ElseIf Session("userlevel").ToString = "8" Then
                    '_menu()
                    'menuReceiveHistory.Visible = True
                    'menuHistoryL1.Visible = True
                    'menuInvoice.Visible = True
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
            If Session("userlevel").ToString = "0" Then
                txtBranchID.Value = Session("userbranch").ToString
                showBranch.Visible = False
            End If
            EDatePicker1.DateValue = Today.Date
            EDatePicker2.DateValue = Today.Date
            lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
            lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
            _getMoney()
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
        menuCheckApprove.Visible = False
        menuDiscount.Visible = False
        menuDiscountSale.Visible = False
        menuCredit.Visible = False
        menuInvoice.Visible = False
        cbManage.Visible = False
        cbReport.Visible = False
        menuProHis.Visible = False
        menuPromotion.Visible = False
    End Sub

    Sub _gridColumnName()
        If gridBranch.GridControlElement.Rows.Count <> 0 Then
            gridBranch.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridBranch.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່ກຸ່ມ"
        End If

        If gridReceiveTotal.GridControlElement.Rows.Count <> 0 Then
            gridReceiveTotal.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(5).Text = "ຮັບຈາກເບີ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(6).Text = "ຊື່"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(7).Text = "ວັນເວລາຮັບເງິນ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(8).Text = "ຈຳນວນໜີ້ທີ່ຊຳລະ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(9).Text = "ສ່ວນຫຼຸດ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(10).Text = "ກຸ່ມ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(11).Text = "ຄົນອອກບິນ"
        End If

        If gridExport.GridControlElement.Rows.Count <> 0 Then
            gridExport.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridExport.GridControlElement.HeaderRow.Cells(0).Text = "ລຳດັບ"
            gridExport.GridControlElement.HeaderRow.Cells(1).Text = "ເລກທີໃບຮັບເງິນ"
            gridExport.GridControlElement.HeaderRow.Cells(2).Text = "ຮັບຈາກເບີ"
            gridExport.GridControlElement.HeaderRow.Cells(3).Text = "ຊື່"
            gridExport.GridControlElement.HeaderRow.Cells(4).Text = "ວັນເວລາຮັບເງິນ"
            gridExport.GridControlElement.HeaderRow.Cells(5).Text = "ຈຳນວນໜີ້ທີ່ຊຳລະ"
            gridExport.GridControlElement.HeaderRow.Cells(6).Text = "ສ່ວນຫຼຸດ"
            gridExport.GridControlElement.HeaderRow.Cells(7).Text = "ກຸ່ມ"
            gridExport.GridControlElement.HeaderRow.Cells(8).Text = "ຄົນອອກບິນ"
        End If
    End Sub
    Private Sub _getBranch()
        Dim sql As String = ""
        If txtSearchBranch.Text <> "" Then
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where sts=1 and branch_id!=15 and branch_name like '%{0}%' order by branch_id desc", txtSearchBranch.Text.Trim)
        Else
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where sts=1 and branch_id!=15 order by branch_id desc")
        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "branch")
            cn.Close()
            gridBranch.RequestSource = ds.Tables("branch")
            gridBranch.GridControlElement.PageIndex = 0
            gridBranch.DataBind()
        Catch ex As Exception
            'lbShort.ForeColor = Drawing.Color.Red
            'lbShort.Text = "Query Data records is error."
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

    Protected Sub btnBranch_ServerClick(sender As Object, e As EventArgs) Handles btnBranch.ServerClick
        txtSearchBranch.Text = ""
        _getBranch()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectBranch();", True)
    End Sub

    Protected Sub txtSearchBranch_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBranch.TextChanged
        _getBranch()
    End Sub

    Protected Sub gridBranch_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridBranch.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
        End If
    End Sub

    Protected Sub gridBranch_GridRowClick(sender As Object, e As EventArgs) Handles gridBranch.GridRowClick
        txtBranchID.Value = gridBranch.GridControlElement.SelectedRow.Cells(4).Text.ToString
        txtBranchName.Value = gridBranch.GridControlElement.SelectedRow.Cells(5).Text.ToString
        _getMoney()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalBranch();", True)
    End Sub

    Protected Sub btRefresh_ServerClick(sender As Object, e As EventArgs) Handles btRefresh.ServerClick
        txtBranchID.Value = ""
        txtBranchName.Value = ""
        _getMoney()
    End Sub


    Sub _getMoney()
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yy")
        Dim sql As String = ""
        If txtBranchID.Value.Trim.ToString <> "" Then
            sql = String.Format("select tbl_invoice.invoice_id,tbl_member.telephone, tbl_member.member_name,tbl_invoice.invoice_date,tbl_invoice.total_amt,tbl_invoice.discount_amt,tbl_branch.branch_name, tbl_invoice.status_user" _
            & " from mtopup.tbl_invoice, mtopup.tbl_invoice_detail,mtopup.tbl_sale,mtopup.tbl_userctrl, mtopup.tbl_branch, mtopup.tbl_member" _
            & " where tbl_invoice.sts = 1" _
            & " and tbl_invoice.user_id=tbl_userctrl.user_id" _
            & " and tbl_userctrl.brch_id=tbl_branch.branch_id" _
            & " and tbl_invoice.invoice_id=tbl_invoice_detail.invoice_id" _
            & " and tbl_invoice_detail.sale_id=tbl_sale.sale_id" _
            & " and tbl_sale.member_id=tbl_member.member_id" _
            & " and to_date(tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " and tbl_branch.branch_id={2} order by tbl_invoice.invoice_date desc", d1, d2, CDbl(txtBranchID.Value.Trim.ToString))
        Else
            sql = String.Format("select tbl_invoice.invoice_id,tbl_member.telephone, tbl_member.member_name,tbl_invoice.invoice_date,tbl_invoice.total_amt,tbl_invoice.discount_amt,tbl_branch.branch_name, tbl_invoice.status_user" _
            & " from mtopup.tbl_invoice, mtopup.tbl_invoice_detail,mtopup.tbl_sale,mtopup.tbl_userctrl, mtopup.tbl_branch, mtopup.tbl_member" _
            & " where tbl_invoice.sts = 1" _
            & " and tbl_invoice.user_id=tbl_userctrl.user_id" _
            & " and tbl_userctrl.brch_id=tbl_branch.branch_id" _
            & " and tbl_invoice.invoice_id=tbl_invoice_detail.invoice_id" _
            & " and tbl_invoice_detail.sale_id=tbl_sale.sale_id" _
            & " and tbl_sale.member_id=tbl_member.member_id" _
            & " and to_date(tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}' order by tbl_invoice.invoice_date desc", d1, d2)
        End If

        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "money")
            cn.Close()
            Try
                Dim amt As Double = 0
                Dim discount As Double = 0
                Dim i As Integer
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    amt = amt + CDbl(ds.Tables(0).Rows(i).Item(2))
                    discount = discount + CDbl(ds.Tables(0).Rows(i).Item(3))
                Next
                If amt = 0 Then
                    lbAmt.Text = "0 ກີບ"
                Else
                    lbAmt.Text = amt.ToString("###,###,###,###,###,###") & " ກີບ"
                End If
                If discount = 0 Then
                    lbDiscount.Text = "0 ກີບ"
                Else
                    lbDiscount.Text = discount.ToString("###,###,###,###,###,###") & " ກີບ"
                End If
                If amt - discount = 0 Then
                    lbRealGet.Text = "0 ກີບ"
                Else
                    lbRealGet.Text = (amt - discount).ToString("###,###,###,###,###,###") & " ກີບ"
                End If
            Catch ex As Exception
                lbAmt.Text = "0 ກີບ"
                lbDiscount.Text = "0 ກີບ"
                lbRealGet.Text = "0 ກີບ"
            End Try

            'If Session("userlevel").ToString <> "0" Or ds.Tables(0).Rows.Count = 0 Then
            '    gridReceiveTotal.Visible = False
            'Else
            gridReceiveTotal.Visible = True
            gridReceiveTotal.RequestSource = ds.Tables(0)
            gridReceiveTotal.DataBind()

            gridExport.RequestSource = ds.Tables(0)
            gridExport.DataBind()
            'End If

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


    Protected Sub gridReceiveTotal_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridReceiveTotal.GridViewPageIndexChanged
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yy")
        Dim sql As String = ""
        If txtBranchID.Value.Trim.ToString <> "" Then
            sql = String.Format("select tbl_invoice.invoice_id,tbl_member.telephone, tbl_member.member_name,tbl_invoice.invoice_date,tbl_invoice.total_amt,tbl_invoice.discount_amt,tbl_branch.branch_name, tbl_invoice.status_user" _
            & " from mtopup.tbl_invoice, mtopup.tbl_invoice_detail,mtopup.tbl_sale,mtopup.tbl_userctrl, mtopup.tbl_branch, mtopup.tbl_member" _
            & " where tbl_invoice.sts = 1" _
            & " and tbl_invoice.user_id=tbl_userctrl.user_id" _
            & " and tbl_userctrl.brch_id=tbl_branch.branch_id" _
            & " and tbl_invoice.invoice_id=tbl_invoice_detail.invoice_id" _
            & " and tbl_invoice_detail.sale_id=tbl_sale.sale_id" _
            & " and tbl_sale.member_id=tbl_member.member_id" _
            & " and to_date(tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " and tbl_branch.branch_id={2} order by tbl_invoice.invoice_date desc", d1, d2, CDbl(txtBranchID.Value.Trim.ToString))
        Else
            sql = String.Format("select tbl_invoice.invoice_id,tbl_member.telephone, tbl_member.member_name,tbl_invoice.invoice_date,tbl_invoice.total_amt,tbl_invoice.discount_amt,tbl_branch.branch_name, tbl_invoice.status_user" _
            & " from mtopup.tbl_invoice, mtopup.tbl_invoice_detail,mtopup.tbl_sale,mtopup.tbl_userctrl, mtopup.tbl_branch, mtopup.tbl_member" _
            & " where tbl_invoice.sts = 1" _
            & " and tbl_invoice.user_id=tbl_userctrl.user_id" _
            & " and tbl_userctrl.brch_id=tbl_branch.branch_id" _
            & " and tbl_invoice.invoice_id=tbl_invoice_detail.invoice_id" _
            & " and tbl_invoice_detail.sale_id=tbl_sale.sale_id" _
            & " and tbl_sale.member_id=tbl_member.member_id" _
            & " and to_date(tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}' order by tbl_invoice.invoice_date desc", d1, d2)
        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "money")
            cn.Close()
            Try
                Dim amt As Double = 0
                Dim discount As Double = 0
                Dim i As Integer
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    amt = amt + CDbl(ds.Tables(0).Rows(i).Item(2))
                    discount = discount + CDbl(ds.Tables(0).Rows(i).Item(3))
                Next
                If amt = 0 Then
                    lbAmt.Text = "0 ກີບ"
                Else
                    lbAmt.Text = amt.ToString("###,###,###,###,###,###") & " ກີບ"
                End If
                If discount = 0 Then
                    lbDiscount.Text = "0 ກີບ"
                Else
                    lbDiscount.Text = discount.ToString("###,###,###,###,###,###") & " ກີບ"
                End If
                If amt - discount = 0 Then
                    lbRealGet.Text = "0 ກີບ"
                Else
                    lbRealGet.Text = (amt - discount).ToString("###,###,###,###,###,###") & " ກີບ"
                End If
            Catch ex As Exception
                lbAmt.Text = "0 ກີບ"
                lbDiscount.Text = "0 ກີບ"
                lbRealGet.Text = "0 ກີບ"
            End Try

            If Session("userlevel").ToString <> "0" Or ds.Tables(0).Rows.Count = 0 Then
                gridReceiveTotal.Visible = False
            Else
                gridReceiveTotal.Visible = True
                gridReceiveTotal.RequestSource = ds.Tables(0)
                gridReceiveTotal.GridControlElement.PageIndex = e.NewPageIndex
                gridReceiveTotal.DataBind()
            End If

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
    Protected Sub gridReceiveTotal_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridReceiveTotal.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False

            e.Row.Cells(8).ForeColor = Drawing.Color.CadetBlue
            e.Row.Cells(9).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(10).ForeColor = Drawing.Color.CornflowerBlue
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(9).Font.Bold = True
            e.Row.Cells(10).Font.Bold = True
            If e.Row.Cells(8).Text = "0" Then
                e.Row.Cells(8).Text = "0 ກີບ"
            Else
                e.Row.Cells(8).Text = CDbl(e.Row.Cells(8).Text).ToString("###,###,###,###,###,###") & " ກີບ"
            End If
            If e.Row.Cells(9).Text = "0" Then
                e.Row.Cells(9).Text = "0 ກີບ"
            Else
                e.Row.Cells(9).Text = CDbl(e.Row.Cells(9).Text).ToString("###,###,###,###,###,###") & " ກີບ"
            End If
        End If
    End Sub
    Protected Sub gridExport_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridExport.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
        _getMoney()
    End Sub

    Protected Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        If gridExport.GridControlElement.Rows.Count <> 0 Then
            Dim d = Today.Date.ToString("dd-MMM-yyyy")
            Dim filename As String = "mtopup_receive_history_" & d & ".xlsx"
            Dim dt As New DataTable("GridView_Data")
            For Each cell As TableCell In gridExport.GridControlElement.HeaderRow.Cells
                dt.Columns.Add(cell.Text)
            Next
            For Each row As GridViewRow In gridExport.GridControlElement.Rows
                dt.Rows.Add()
                For i As Integer = 0 To row.Cells.Count - 1
                    dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
                Next
            Next
            'Using wb As New XLWorkbook
            '    wb.Worksheets.Add(dt)
            '    Response.Clear()
            '    Response.Buffer = True
            '    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            '    Response.AddHeader("content-disposition", "attachment; filename=" & filename)
            '    Using MyMemoryStream As New MemoryStream()
            '        wb.SaveAs(MyMemoryStream)
            '        MyMemoryStream.WriteTo(Response.OutputStream)
            '        Response.Flush()
            '        Response.[End]()
            '    End Using
            'End Using
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '       server control at run time. 

    End Sub
End Class
