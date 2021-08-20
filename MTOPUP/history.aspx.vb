Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.Web.Configuration
Imports nickDevClass
Imports System.Globalization

Partial Class history
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
                    menuBranch.Visible = True
                    menuDiscount.Visible = True
                    menuDiscountSale.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                    menuCheckApprove.Visible = True
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
        If gridHistory.GridControlElement.Rows.Count <> 0 Then
            gridHistory.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridHistory.GridControlElement.HeaderRow.Cells(6).Text = "ເບີຕົ້ນທາງ"
            gridHistory.GridControlElement.HeaderRow.Cells(7).Text = "ເບີປາຍທາງ"
            gridHistory.GridControlElement.HeaderRow.Cells(8).Text = "ປະເພດການໂອນ"
            gridHistory.GridControlElement.HeaderRow.Cells(9).Text = "ປະເພດເບີປາຍທາງ"
            gridHistory.GridControlElement.HeaderRow.Cells(10).Text = "ຈຳນວນເງິນທີ່ໂອນ"
            gridHistory.GridControlElement.HeaderRow.Cells(11).Text = "ໄດ້ໂບນັດ"
            gridHistory.GridControlElement.HeaderRow.Cells(13).Text = "ເວລາໂອນ"
            gridHistory.GridControlElement.HeaderRow.Cells(14).Text = "ຈຳນວນເງິນກ່ອນໂອນ"
            gridHistory.GridControlElement.HeaderRow.Cells(15).Text = "ຈຳນວນເງິນຍັງເຫຼືອ"
            gridHistory.GridControlElement.HeaderRow.Cells(17).Text = "ສະຖານະການໂອນ"
        End If


        If gridMemberModal.GridControlElement.Rows.Count <> 0 Then
            gridMemberModal.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridMemberModal.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(6).Text = "ຊື່ສະມາຊິກ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(7).Text = "ລະດັບສະມາຊິກ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(8).Text = "ປະເພດສະມາຊິກ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(12).Text = "ສະຖານະ"
        End If


    End Sub
    Private Sub _getMember()
        Dim sql As String = ""
        Dim userLevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim
        If Session("userlevel").ToString.Trim = "0" Then
            If txtSearchMember.Text <> "" Then
                sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                                  & " from mtopup.tbl_member where (telephone like '%{0}%' or member_name like '%{0}%') and tbl_member.brch_id=" & brchID & " and sts=1 and rownum <= 100 order by member_id desc", txtSearchMember.Text.Trim)
            Else
                sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                                   & " from mtopup.tbl_member where tbl_member.brch_id=" & brchID & " and sts=1 and rownum <= 100 order by member_id desc")
            End If
        Else
            If txtSearchMember.Text <> "" Then
                sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                                  & " from mtopup.tbl_member where (telephone like '%{0}%' or member_name like '%{0}%') and sts=1 and rownum <= 100 order by member_id desc", txtSearchMember.Text.Trim)
            Else
                sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                                   & " from mtopup.tbl_member where sts=1 and rownum <= 100 order by member_id desc")
            End If
        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "member")
            cn.Close()
            gridMemberModal.RequestSource = ds.Tables("member")
            gridMemberModal.GridControlElement.PageIndex = 0
            gridMemberModal.DataBind()
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

    Private Sub _RequestHistory(telephone As String, date1 As Date, date2 As Date)
        Dim d1 As String = date1.ToString("dd-MMM-yy")
        Dim d2 As String = date2.ToString("dd-MMM-yy")
        lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
        Dim sql As String = ""
        If lbSearchType.Text.Trim = "ປະຫວັດການໂອນ" Then
            sql = String.Format("select  trans_id,member_id,telephone,msisdn,trans_type,paid_type,trans_amt,bonus_amt,sts,trans_date,m_amt,0,bl_amt,sts_desc " _
           & " from mtopup.tbl_transfer where telephone='{0}' and to_date(trans_date,'DD-Mon-YY') between '{1}' and '{2}' order by trans_id desc", telephone, d1, d2)
        Else
            sql = String.Format("select  trans_id,member_id,telephone,msisdn,trans_type,paid_type,trans_amt,bonus_amt,sts,trans_date,m_amt,0,bl_amt,sts_desc " _
                & " from mtopup.tbl_transfer where msisdn='{0}' and to_date(trans_date,'DD-Mon-YY') between '{1}' and '{2}' and (sts_desc like '%Operator successed%' or sts_desc like '%Operation success%') order by trans_id desc", telephone, d1, d2)
        End If

        cn = New OracleConnection
        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
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

    Protected Sub btnTelephone_ServerClick(sender As Object, e As EventArgs) Handles btnTelephone.ServerClick
        txtSearchMember.Text = ""
        _getMember()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectMember();", True)
    End Sub

    Protected Sub gridMemberModal_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridMemberModal.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            'e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            If CDbl(e.Row.Cells(10).Text) > 0 Then
                e.Row.Cells(10).Text = CDbl(e.Row.Cells(10).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            'e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(10).Font.Bold = True
            e.Row.Cells(10).ForeColor = Drawing.Color.OrangeRed
            If e.Row.Cells(12).Text.ToString = "1" Then
                e.Row.Cells(12).Text = "Active"
                e.Row.Cells(12).ForeColor = Drawing.Color.Green
                e.Row.Cells(12).Font.Bold = True
            Else
                e.Row.Cells(12).Text = "Deactivation"
                e.Row.Cells(12).ForeColor = Drawing.Color.Red
                e.Row.Cells(12).Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub gridMemberModal_GridRowClick(sender As Object, e As EventArgs) Handles gridMemberModal.GridRowClick
        Dim _row = gridMemberModal.GridControlElement.SelectedRow
        txtTelephone.Value = _row.Cells(5).Text.Trim
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalMember();", True)
        _RequestHistory(txtTelephone.Value.Trim, EDatePicker1.DateValue, EDatePicker2.DateValue)


    End Sub

    Protected Sub gridMemberModal_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridMemberModal.GridViewPageIndexChanged
        Dim sql As String = ""
        If txtSearchMember.Text <> "" Then
            sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                              & " from mtopup.tbl_member where (telephone like '%{0}%' or member_name like '%{0}%') and sts=1 and rownum <= 100 order by member_id desc", txtSearchMember.Text.Trim)
        Else
            sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                               & " from mtopup.tbl_member where sts=1 and rownum <= 100 order by member_id desc")
        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)

        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "member")
            cn.Close()
            gridMemberModal.RequestSource = ds.Tables("member")
            gridMemberModal.GridControlElement.PageIndex = e.NewPageIndex
            gridMemberModal.DataBind()
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

    Protected Sub txtSearchMember_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMember.TextChanged
        _getMember()
    End Sub

    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick

        _RequestHistory(txtTelephone.Value.Trim, EDatePicker1.DateValue, EDatePicker2.DateValue)
    End Sub

    Protected Sub gridHistory_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridHistory.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(16).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            Dim a As New Double

            a = (CDbl(e.Row.Cells(14).Text) - CDbl(e.Row.Cells(10).Text))
            If a > 0 Then
                e.Row.Cells(15).Text = a.ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            Else
                e.Row.Cells(15).Text = e.Row.Cells(14).Text
            End If

            If CDbl(e.Row.Cells(15).Text) > 0 Then
                e.Row.Cells(15).Text = CDbl(e.Row.Cells(15).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If

            If CDbl(e.Row.Cells(10).Text) > 0 Then
                e.Row.Cells(10).Text = CDbl(e.Row.Cells(10).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
            If CDbl(e.Row.Cells(11).Text) > 0 Then
                e.Row.Cells(11).Text = CDbl(e.Row.Cells(11).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
            If CDbl(e.Row.Cells(14).Text) > 0 Then
                e.Row.Cells(14).Text = CDbl(e.Row.Cells(14).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If

            If lbSearchType.Text.Trim = "ປະຫວັດການໂອນ" Then
                e.Row.Cells(6).Font.Bold = True
            Else
                e.Row.Cells(7).Font.Bold = True
            End If



            e.Row.Cells(10).Font.Bold = True
            e.Row.Cells(11).Font.Bold = True
            'e.Row.Cells(10).ForeColor = Drawing.Color.CadetBlue
            e.Row.Cells(14).Font.Bold = True
            e.Row.Cells(15).Font.Bold = True

            If e.Row.Cells(17).Text.Trim.Contains("Operator successed") Or e.Row.Cells(17).Text.Trim.Contains("Operation successed") Or e.Row.Cells(17).Text.Trim.Contains("Operation success") Then
                e.Row.Cells(17).ForeColor = Drawing.Color.Green
            Else
                e.Row.Cells(17).ForeColor = Drawing.Color.Red
            End If


            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(16).Visible = False
        End If
    End Sub

    Protected Sub gridHistory_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridHistory.GridViewPageIndexChanged
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yy")
        Dim sql As String = ""
        If lbSearchType.Text.Trim = "ປະຫວັດການໂອນ" Then
            sql = String.Format("select  trans_id,member_id,telephone,msisdn,trans_type,paid_type,trans_amt,bonus_amt,sts,trans_date,m_amt,0,bl_amt,sts_desc " _
           & " from mtopup.tbl_transfer where telephone='{0}' and to_date(trans_date,'DD-Mon-YY') between '{1}' and '{2}' order by trans_id desc", txtTelephone.Value.Trim, d1, d2)
        Else
            sql = String.Format("select  trans_id,member_id,telephone,msisdn,trans_type,paid_type,trans_amt,bonus_amt,sts,trans_date,m_amt,0,bl_amt,sts_desc " _
                & " from mtopup.tbl_transfer where msisdn='{0}' and to_date(trans_date,'DD-Mon-YY') between '{1}' and '{2}' and sts_desc like '%Operator successed%' or sts_desc like '%Operation success%' order by trans_id desc", txtTelephone.Value.Trim, d1, d2)
        End If

        cn = New OracleConnection
        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
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

    Protected Sub btnType1_Click(sender As Object, e As EventArgs) Handles btnType1.Click
        lbSearchType.Text = btnType1.Text
        lbTypeHeader.Text = btnType1.Text
        _RequestHistory(txtTelephone.Value.Trim, EDatePicker1.DateValue, EDatePicker2.DateValue)
    End Sub

    Protected Sub btnType2_Click(sender As Object, e As EventArgs) Handles btnType2.Click
        lbSearchType.Text = btnType2.Text
        lbTypeHeader.Text = btnType2.Text
        lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
        _RequestHistory(txtTelephone.Value.Trim, EDatePicker1.DateValue, EDatePicker2.DateValue)
    End Sub
End Class

' System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
