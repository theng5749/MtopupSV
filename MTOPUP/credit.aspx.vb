Imports System.Data
Imports Microsoft.VisualBasic
Imports ClosedXML.Excel
Imports System.IO
Imports nickDevClass
Imports Oracle.ManagedDataAccess.Client
Imports System.Globalization

Partial Class credit
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
                    menuCheckApprove.Visible = True
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
            _FillData()
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
        menuCheckApprove.Visible = False
        menuDiscountSale.Visible = False
        menuCredit.Visible = False
        menuInvoice.Visible = False
        cbManage.Visible = False
        cbReport.Visible = False
        menuProHis.Visible = False
        menuPromotion.Visible = False
    End Sub
    Sub _gridColumnName()
        If gridCredit.GridControlElement.Rows.Count <> 0 Then
            gridCredit.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridCredit.GridControlElement.HeaderRow.Cells(4).Text = "ໝາຍເລກໂທລະສັບ"
            gridCredit.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່"
            gridCredit.GridControlElement.HeaderRow.Cells(6).Text = "ວັນທີທີ່ເລີມມີການຂາຍໃຫ້"
            gridCredit.GridControlElement.HeaderRow.Cells(7).Text = "ຈຳນວນຄັ້ງທີ່ໄດ້ຂາຍໃຫ້"
            gridCredit.GridControlElement.HeaderRow.Cells(8).Text = "ຈຳນວນເງິນທີ່ໄດ້ຂາຍໃຫ້"
            gridCredit.GridControlElement.HeaderRow.Cells(9).Text = "ຈຳນວນເງິນທີ່ໄດ້ຊຳລະແລ້ວ"
            gridCredit.GridControlElement.HeaderRow.Cells(10).Text = "ຈຳນວນເງິນທີ່ຄ້າງຊຳລະ"
            gridCredit.GridControlElement.HeaderRow.Cells(11).Text = "ກຸ່ມ"
        End If

        If gridCreditExport.GridControlElement.Rows.Count <> 0 Then
            gridCreditExport.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridCreditExport.GridControlElement.HeaderRow.Cells(0).Text = "ລຳດັບ"
            gridCreditExport.GridControlElement.HeaderRow.Cells(1).Text = "ໝາຍເລກໂທລະສັບ"
            gridCreditExport.GridControlElement.HeaderRow.Cells(2).Text = "ຊື່"
            gridCreditExport.GridControlElement.HeaderRow.Cells(3).Text = "ວັນທີທີ່ເລີມມີການຂາຍໃຫ້"
            gridCreditExport.GridControlElement.HeaderRow.Cells(4).Text = "ຈຳນວນຄັ້ງທີ່ໄດ້ຂາຍໃຫ້"
            gridCreditExport.GridControlElement.HeaderRow.Cells(5).Text = "ຈຳນວນເງິນທີ່ໄດ້ຂາຍໃຫ້"
            gridCreditExport.GridControlElement.HeaderRow.Cells(6).Text = "ຈຳນວນເງິນທີ່ໄດ້ຊຳລະແລ້ວ"
            gridCreditExport.GridControlElement.HeaderRow.Cells(7).Text = "ຈຳນວນເງິນທີ່ຄ້າງຊຳລະ"
            gridCreditExport.GridControlElement.HeaderRow.Cells(8).Text = "ກຸ່ມ"
        End If
    End Sub
    Sub _FillData()
        btnPrint.Disabled = True
        Dim sql As String = ""

        Dim userlevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim

        If userlevel = "0" Then
            If txtSearchText.Text.Trim <> "" Then
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and (v_sale.telephone like '%{0}%' or tbl_branch.branch_name like '%{0}%') group by v_sale.telephone ,tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc", txtSearchText.Text.Trim)
            Else
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " group by v_sale.telephone, tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc")
            End If
        Else
            If txtSearchText.Text.Trim <> "" Then
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id and (v_sale.telephone like '%{0}%' or tbl_branch.branch_name like '%{0}%') group by v_sale.telephone ,tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc", txtSearchText.Text.Trim)
            Else
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id group by v_sale.telephone ,tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc")
            End If
        End If


        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "credit")
            cn.Close()
            gridCredit.RequestSource = ds.Tables("credit")
            gridCredit.DataBind()

            gridCreditExport.RequestSource = ds.Tables("credit")
            gridCreditExport.DataBind()
            If ds.Tables(0).Rows.Count <> 0 Then
                btnPrint.Disabled = False
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

    Protected Sub txtSearchText_TextChanged(sender As Object, e As EventArgs) Handles txtSearchText.TextChanged
        _FillData()
    End Sub

    Protected Sub gridCredit_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridCredit.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False

            e.Row.Cells(4).Font.Bold = True
            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(9).Font.Bold = True
            e.Row.Cells(10).Font.Bold = True
            e.Row.Cells(11).Font.Bold = True

            e.Row.Cells(8).ForeColor = Drawing.Color.CadetBlue
            e.Row.Cells(9).ForeColor = Drawing.Color.Green
            e.Row.Cells(10).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(11).ForeColor = Drawing.Color.CornflowerBlue

            If e.Row.Cells(7).Text = "0" Then
                e.Row.Cells(8).Text = "0 ຄັ້ງ"
            Else
                e.Row.Cells(7).Text = CDbl(e.Row.Cells(7).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ຄັ້ງ"
            End If
            If e.Row.Cells(7).Text = "0" Then
                e.Row.Cells(7).Text = "0 ກີບ"
            Else
                e.Row.Cells(8).Text = CDbl(e.Row.Cells(8).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
            If e.Row.Cells(9).Text = "0" Then
                e.Row.Cells(9).Text = "0 ກີບ"
            Else
                e.Row.Cells(9).Text = CDbl(e.Row.Cells(9).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
            If e.Row.Cells(10).Text = "0" Then
                e.Row.Cells(10).Text = "0 ກີບ"
            Else
                e.Row.Cells(10).Text = CDbl(e.Row.Cells(10).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat) & " ກີບ"
            End If
        End If
    End Sub

    Protected Sub gridCredit_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridCredit.GridViewPageIndexChanged
        Dim sql As String = ""
        Dim userlevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim

        If userlevel = "0" Then
            If txtSearchText.Text.Trim <> "" Then
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and (v_sale.telephone like '%{0}%' or tbl_branch.branch_name like '%{0}%') group by v_sale.telephone ,tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc", txtSearchText.Text.Trim)
            Else
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " group by v_sale.telephone, tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc")
            End If
        Else
            If txtSearchText.Text.Trim <> "" Then
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id and (v_sale.telephone like '%{0}%' or tbl_branch.branch_name like '%{0}%') group by v_sale.telephone ,tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc", txtSearchText.Text.Trim)
            Else
                sql = String.Format("select v_sale.telephone, tbl_member.member_name, min(v_sale.sale_date),sum(v_sale.rec), sum(v_sale.sale_amt),sum(v_sale.paid_amt),sum(v_sale.balance_amt),tbl_branch.branch_name from mtopup.v_sale,mtopup.tbl_member,mtopup.tbl_branch where v_sale.balance_amt>0 and v_sale.telephone=tbl_member.telephone and tbl_member.brch_id=tbl_branch.branch_id group by v_sale.telephone ,tbl_member.member_name, tbl_member.brch_id , tbl_branch.branch_name order by 2 asc")
            End If
        End If

        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "credit")
            cn.Close()
            gridCredit.RequestSource = ds.Tables("credit")
            gridCredit.GridControlElement.PageIndex = e.NewPageIndex
            gridCredit.DataBind()
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

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '       server control at run time. 

    End Sub

    Protected Sub btnPrint_ServerClick(sender As Object, e As EventArgs) Handles btnPrint.ServerClick
        Dim d = Today.Date.ToString("dd-MMM-yyyy")
        Dim filename As String = "mtopup_credit_" & d & ".xlsx"
        Dim dt As New DataTable("GridView_Data")
        For Each cell As TableCell In gridCreditExport.GridControlElement.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In gridCreditExport.GridControlElement.Rows
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

    Protected Sub gridCreditExport_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridCreditExport.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = "ລຳດັບ"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
End Class
