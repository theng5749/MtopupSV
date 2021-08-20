Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Microsoft.VisualBasic
Imports ClosedXML.Excel
Imports System.IO
Imports nickDevClass
Imports System.Globalization

Partial Class report
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
                    menuDiscountSale.Visible = True
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
                    Response.Redirect("Default.aspx")
                ElseIf Session("userlevel").ToString = "6" Then
                    Response.Redirect("Default.aspx")
                ElseIf Session("userlevel").ToString = "8" Then
                    _menu()
                    'menuReceiveHistory.Visible = True
                    menuHistoryL1.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                    cbReport.Visible = True
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
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(4).Text = "ຈຳນວນໜີ້ທີ່ຊຳລະ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(5).Text = "ຈຳນວນເງິນສ່ວນຫຼຸດ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(6).Text = "ຈຳນວນເງິນທີ່ໄດ້ຮັບ"
            gridReceiveTotal.GridControlElement.HeaderRow.Cells(7).Text = "ຊື່ຄົນອອກບິນ"
        End If
        If gridReceiveTotal2.GridControlElement.Rows.Count <> 0 Then
            gridReceiveTotal2.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridReceiveTotal2.GridControlElement.HeaderRow.Cells(4).Text = "ຈຳນວນໜີ້ທີ່ຊຳລະ"
            gridReceiveTotal2.GridControlElement.HeaderRow.Cells(5).Text = "ຈຳນວນເງິນສ່ວນຫຼຸດ"
            gridReceiveTotal2.GridControlElement.HeaderRow.Cells(6).Text = "ຈຳນວນເງິນທີ່ໄດ້ຮັບ"
            gridReceiveTotal2.GridControlElement.HeaderRow.Cells(7).Text = "ຊື່ກຸ່ມ"
        End If

        If gridExport.GridControlElement.Rows.Count <> 0 Then
            gridExport.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridExport.GridControlElement.HeaderRow.Cells(0).Text = "ລຳດັບ"
            gridExport.GridControlElement.HeaderRow.Cells(1).Text = "ຈຳນວນໜີ້ທີ່ຊຳລະ"
            gridExport.GridControlElement.HeaderRow.Cells(2).Text = "ຈຳນວນເງິນສ່ວນຫຼຸດ"
            gridExport.GridControlElement.HeaderRow.Cells(3).Text = "ຈຳນວນເງິນທີ່ໄດ້ຮັບ"
            If gridReceiveTotal.Visible = True Then
                gridExport.GridControlElement.HeaderRow.Cells(4).Text = "ຊື່ຄົນອອກບິນ"
            End If
            If gridReceiveTotal2.Visible = True Then
                gridExport.GridControlElement.HeaderRow.Cells(4).Text = "ຊື່ກຸ່ມ"
            End If
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
            If Session("userlevel").ToString.Trim = "9" Or Session("userlevel").ToString.Trim = "8" Then
                sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),0 as Pay,mtopup.tbl_userctrl.user_name" _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts = 1" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and mtopup.tbl_userctrl.user_id in (select distinct(mtopup.tbl_invoice.user_id) from mtopup.tbl_invoice)" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " and mtopup.tbl_branch.branch_id={2}" _
            & " group by mtopup.tbl_userctrl.user_name order by 1 desc", d1, d2, CDbl(txtBranchID.Value.Trim.ToString))
            Else
                sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),0 as Pay,mtopup.tbl_userctrl.user_name" _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts = 1" _
            & " and mtopup.tbl_invoice.user_id=mtopup.tbl_userctrl.user_id" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and mtopup.tbl_userctrl.user_id in (select distinct(mtopup.tbl_invoice.user_id) from mtopup.tbl_invoice)" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " and mtopup.tbl_branch.branch_id={2}" _
            & " group by mtopup.tbl_userctrl.user_name order by 1 desc", d1, d2, CDbl(txtBranchID.Value.Trim.ToString))
            End If


        Else
            If Session("userlevel").ToString.Trim = "9" Or Session("userlevel").ToString.Trim = "8" Then
                sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),0 as Pay,mtopup.tbl_branch.branch_name " _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts=1" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and mtopup.tbl_userctrl.user_id in (select distinct(mtopup.tbl_invoice.user_id) from mtopup.tbl_invoice)" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " group by mtopup.tbl_branch.branch_name order by 1 desc", d1, d2)
            Else
                sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),0 as Pay,mtopup.tbl_branch.branch_name " _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts=1" _
            & " and mtopup.tbl_invoice.user_id=mtopup.tbl_userctrl.user_id" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and mtopup.tbl_userctrl.user_id in (select distinct(mtopup.tbl_invoice.user_id) from mtopup.tbl_invoice)" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " group by mtopup.tbl_branch.branch_name order by 1 desc", d1, d2)
            End If


        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "getMoney")
            cn.Close()
            Try
                Dim amt As Double = 0
                Dim discount As Double = 0
                Dim i As Integer
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    amt = amt + CDbl(ds.Tables(0).Rows(i).Item(0))
                    discount = discount + CDbl(ds.Tables(0).Rows(i).Item(1))
                Next
                If amt = 0 Then
                    lbAmt.Text = "0 ກີບ"
                Else
                    lbAmt.Text = amt.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
                If discount = 0 Then
                    lbDiscount.Text = "0 ກີບ"
                Else
                    lbDiscount.Text = discount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
                If amt - discount = 0 Then
                    lbRealGet.Text = "0 ກີບ"
                Else
                    lbRealGet.Text = (amt - discount).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
            Catch ex As Exception
                lbAmt.Text = "0 ກີບ"
                lbDiscount.Text = "0 ກີບ"
                lbRealGet.Text = "0 ກີບ"
            End Try

            If txtBranchID.Value.Trim.ToString <> "" Then
                gridReceiveTotal2.Visible = False
                gridReceiveTotal.Visible = True
                gridReceiveTotal.RequestSource = ds.Tables(0)
                gridReceiveTotal.DataBind()

                gridExport.RequestSource = ds.Tables(0)
                gridExport.DataBind()
            Else
                gridReceiveTotal.Visible = False
                gridReceiveTotal2.Visible = True
                gridReceiveTotal2.RequestSource = ds.Tables(0)
                gridReceiveTotal2.DataBind()

                gridExport.RequestSource = ds.Tables(0)
                gridExport.DataBind()
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


    Protected Sub gridReceiveTotal_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridReceiveTotal.GridViewPageIndexChanged
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yy")
        Dim sql As String = ""
        If txtBranchID.Value.Trim.ToString <> "" Then
            sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),mtopup.tbl_userctrl.user_name" _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts = 1" _
            & " and mtopup.tbl_invoice.user_id=mtopup.tbl_userctrl.user_id" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " and mtopup.tbl_branch.branch_id={2}" _
            & " group by mtopup.tbl_userctrl.user_name order by 1 desc", d1, d2, CDbl(txtBranchID.Value.Trim.ToString))

        Else

            sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),mtopup.tbl_branch.branch_name " _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts=1" _
            & " and mtopup.tbl_invoice.user_id=mtopup.tbl_userctrl.user_id" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " group by mtopup.tbl_branch.branch_name order by 1 desc", d1, d2)



        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "getMoney")
            cn.Close()
            Try
                Dim amt As Double = 0
                Dim discount As Double = 0
                Dim i As Integer
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    amt = amt + CDbl(ds.Tables(0).Rows(i).Item(0))
                    discount = discount + CDbl(ds.Tables(0).Rows(i).Item(1))
                Next
                If amt = 0 Then
                    lbAmt.Text = "0 ກີບ"
                Else
                    lbAmt.Text = amt.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
                If discount = 0 Then
                    lbDiscount.Text = "0 ກີບ"
                Else
                    lbDiscount.Text = discount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
                If amt - discount = 0 Then
                    lbRealGet.Text = "0 ກີບ"
                Else
                    lbRealGet.Text = (amt - discount).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
            Catch ex As Exception
                lbAmt.Text = "0 ກີບ"
                lbDiscount.Text = "0 ກີບ"
                lbRealGet.Text = "0 ກີບ"
            End Try

            If txtBranchID.Value.Trim.ToString <> "" Then
                gridReceiveTotal2.Visible = False
                gridReceiveTotal.Visible = True
                gridReceiveTotal.RequestSource = ds.Tables(0)
                gridReceiveTotal.GridControlElement.PageIndex = e.NewPageIndex
                gridReceiveTotal.DataBind()
            Else
                gridReceiveTotal.Visible = False
                gridReceiveTotal2.Visible = True
                gridReceiveTotal2.RequestSource = ds.Tables(0)
                gridReceiveTotal2.GridControlElement.PageIndex = e.NewPageIndex
                gridReceiveTotal2.DataBind()
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


    Protected Sub gridReceiveTotal2_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridReceiveTotal2.GridViewPageIndexChanged
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yy")
        Dim sql As String = ""
        If txtBranchID.Value.Trim.ToString <> "" Then
            sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),mtopup.tbl_userctrl.user_name" _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts = 1" _
            & " and mtopup.tbl_invoice.user_id=mtopup.tbl_userctrl.user_id" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " and mtopup.tbl_branch.branch_id={2}" _
            & " group by mtopup.tbl_userctrl.user_name order by 1 desc", d1, d2, CDbl(txtBranchID.Value.Trim.ToString))

        Else

            sql = String.Format("select sum(mtopup.tbl_invoice.total_amt),sum(mtopup.tbl_invoice.discount_amt),mtopup.tbl_branch.branch_name " _
            & " from mtopup.tbl_invoice,mtopup.tbl_userctrl,mtopup.tbl_branch" _
            & " where mtopup.tbl_invoice.sts=1" _
            & " and mtopup.tbl_invoice.user_id=mtopup.tbl_userctrl.user_id" _
            & " and mtopup.tbl_userctrl.brch_id=mtopup.tbl_branch.branch_id" _
            & " and to_date(mtopup.tbl_invoice.invoice_date,'DD-Mon-YY') between '{0}' and '{1}'" _
            & " group by mtopup.tbl_branch.branch_name order by 1 desc", d1, d2)



        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "getMoney")
            cn.Close()
            Try
                Dim amt As Double = 0
                Dim discount As Double = 0
                Dim i As Integer
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    amt = amt + CDbl(ds.Tables(0).Rows(i).Item(0))
                    discount = discount + CDbl(ds.Tables(0).Rows(i).Item(1))
                Next
                If amt = 0 Then
                    lbAmt.Text = "0 ກີບ"
                Else
                    lbAmt.Text = amt.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
                If discount = 0 Then
                    lbDiscount.Text = "0 ກີບ"
                Else
                    lbDiscount.Text = discount.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
                If amt - discount = 0 Then
                    lbRealGet.Text = "0 ກີບ"
                Else
                    lbRealGet.Text = (amt - discount).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
                End If
            Catch ex As Exception
                lbAmt.Text = "0 ກີບ"
                lbDiscount.Text = "0 ກີບ"
                lbRealGet.Text = "0 ກີບ"
            End Try

            If txtBranchID.Value.Trim.ToString <> "" Then
                gridReceiveTotal2.Visible = False
                gridReceiveTotal.Visible = True
                gridReceiveTotal.RequestSource = ds.Tables(0)
                gridReceiveTotal.GridControlElement.PageIndex = e.NewPageIndex
                gridReceiveTotal.DataBind()
            Else
                gridReceiveTotal.Visible = False
                gridReceiveTotal2.Visible = True
                gridReceiveTotal2.RequestSource = ds.Tables(0)
                gridReceiveTotal2.GridControlElement.PageIndex = e.NewPageIndex
                gridReceiveTotal2.DataBind()
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
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(6).Text = CDbl(e.Row.Cells(4).Text) - CDbl(e.Row.Cells(5).Text)
            e.Row.Cells(4).ForeColor = Drawing.Color.CadetBlue
            e.Row.Cells(5).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(6).ForeColor = Drawing.Color.Green
            e.Row.Cells(4).Font.Bold = True
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(6).Font.Bold = True
            If e.Row.Cells(4).Text = "0" Then
                e.Row.Cells(4).Text = "0 ກີບ"
            Else
                e.Row.Cells(4).Text = CDbl(e.Row.Cells(4).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
            If e.Row.Cells(5).Text = "0" Then
                e.Row.Cells(5).Text = "0 ກີບ"
            Else
                e.Row.Cells(5).Text = CDbl(e.Row.Cells(5).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
            If e.Row.Cells(6).Text = "0" Then
                e.Row.Cells(6).Text = "0 ກີບ"
            Else
                e.Row.Cells(6).Text = CDbl(e.Row.Cells(6).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
        End If
    End Sub
    Protected Sub gridReceiveTotal2_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridReceiveTotal2.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(6).Text = CDbl(e.Row.Cells(4).Text) - CDbl(e.Row.Cells(5).Text)
            e.Row.Cells(4).ForeColor = Drawing.Color.CadetBlue
            e.Row.Cells(5).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(6).ForeColor = Drawing.Color.Green
            e.Row.Cells(4).Font.Bold = True
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(6).Font.Bold = True
            If e.Row.Cells(4).Text = "0" Then
                e.Row.Cells(4).Text = "0 ກີບ"
            Else
                e.Row.Cells(4).Text = CDbl(e.Row.Cells(4).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
            If e.Row.Cells(5).Text = "0" Then
                e.Row.Cells(5).Text = "0 ກີບ"
            Else
                e.Row.Cells(5).Text = CDbl(e.Row.Cells(5).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
            If e.Row.Cells(6).Text = "0" Then
                e.Row.Cells(6).Text = "0 ກີບ"
            Else
                e.Row.Cells(6).Text = CDbl(e.Row.Cells(6).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
        End If
    End Sub

    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
        _getMoney()
    End Sub


    Protected Sub gridExport_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridExport.GridBindCompleted
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(3).Text = CDbl(e.Row.Cells(1).Text) - CDbl(e.Row.Cells(2).Text)
        End If
    End Sub
    Protected Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        Dim d = Today.Date.ToString("dd-MMM-yyyy")
        Dim filename As String = "mtopup_receive_total_" & d & ".xlsx"
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
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '       server control at run time. 

    End Sub
End Class
