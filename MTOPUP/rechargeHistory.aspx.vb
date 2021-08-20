Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Microsoft.VisualBasic
Imports ClosedXML.Excel
Imports System.IO
Imports nickDevClass
Imports System.Globalization

Partial Class rechargeHistory
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
                    menuRechargeHistory.Visible = True
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
                ElseIf Session("userlevel").ToString = "11" Or Session("userlevel").ToString = "12" Or Session("userlevel").ToString = "13" Then
                    _menu()
                    menuMember.Visible = True
                    menuRecharge.Visible = True
                    cbReport.Visible = True
                    menuRechargeHistory.Visible = True
                ElseIf Session("userlevel").ToString = "0" Then
                    Response.Redirect("Default.aspx")
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
            EDatePicker1.DateValue = Today.Date
            EDatePicker2.DateValue = Today.Date
            lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
            lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
            _RequestHistory(EDatePicker1.DateValue.ToShortDateString, EDatePicker2.DateValue.ToShortDateString)
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
        If gridMemberModal.GridControlElement.Rows.Count <> 0 Then
            gridMemberModal.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridMemberModal.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(6).Text = "ຊື່ສະມາຊິກ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(7).Text = "Level"
            gridMemberModal.GridControlElement.HeaderRow.Cells(8).Text = "ປະເພດສະມາຊິກ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(9).Text = "ຈຳນວນເງິນທີ່ມີ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(10).Text = "ສະຖານະ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(11).Text = "ກຸ່ມ"
        End If

        If gridHistory.GridControlElement.Rows.Count <> 0 Then
            gridHistory.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridHistory.GridControlElement.HeaderRow.Cells(4).Text = "ໝາຍເລກໂທລະສັບ"
            gridHistory.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່ສະມາຊິກ"
            gridHistory.GridControlElement.HeaderRow.Cells(6).Text = "ວັນທີທີ່ເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(7).Text = "ຈຳນວນເງິນກ່ອນເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(8).Text = "ຈຳນວນເງິນທີ່ເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(9).Text = "ຈຳນວນເງິນຫລັງເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(10).Text = "ຜູ້ທີ່ທຳການເຕີມໃຫ້"
            gridHistory.GridControlElement.HeaderRow.Cells(11).Text = "ເຕີມໃຫ້ກຸ່ມ"
        End If


        If gridExport.GridControlElement.Rows.Count <> 0 Then
            gridExport.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridExport.GridControlElement.HeaderRow.Cells(0).Text = "ລຳດັບ"
            gridExport.GridControlElement.HeaderRow.Cells(1).Text = "ເລກທີການເຕີມເງິນ"
            gridExport.GridControlElement.HeaderRow.Cells(2).Text = "ລະຫັດສະມາຊິກ"
            gridExport.GridControlElement.HeaderRow.Cells(3).Text = "ໝາຍເລກໂທລະສັບ"
            gridExport.GridControlElement.HeaderRow.Cells(4).Text = "ວັນທີທີ່ເຕີມ"
            gridExport.GridControlElement.HeaderRow.Cells(5).Text = "ຈຳນວນເງິນທີ່ເຕີມ"
            gridExport.GridControlElement.HeaderRow.Cells(6).Text = "ຜູ້ທີ່ທຳການເຕີມໃຫ້"
            gridExport.GridControlElement.HeaderRow.Cells(7).Text = "ເຕີມໃຫ້ກຸ່ມ"
        End If

        If gridBranch.GridControlElement.Rows.Count <> 0 Then
            gridBranch.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridBranch.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່ກຸ່ມ"
        End If

    End Sub

    Private Sub _getMember(level As Integer)
        Dim sql As String = ""
        'If txtSearchMember.Text <> "" Then
        '    sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit, tbl_member.user_id, tbl_member.sts,tbl_branch.branch_name" _
        '                      & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and (tbl_member.telephone like '%{1}%' or tbl_member.member_name like '%{1}%' or tbl_branch.branch_name like '%{1}%') and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_id desc", level, txtSearchMember.Text.Trim)
        'Else
        '    sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit, tbl_member.user_id, tbl_member.sts,tbl_branch.branch_name" _
        '                       & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_id desc", level)
        'End If
        'cn = New OracleConnection
        'Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        'Dim ds As New DataSet
        'Try
        '    'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
        '    cn.ConnectionString = conDB.getConnectionString
        '    cn.Open()
        '    da.Fill(ds, "member")
        '    cn.Close()
        '    gridMemberModal.RequestSource = ds.Tables("member")
        '    gridMemberModal.GridControlElement.PageIndex = 0
        '    gridMemberModal.DataBind()
        'Catch ex As Exception
        '    'lbShort.ForeColor = Drawing.Color.Red
        '    'lbShort.Text = "Query Data records is error."
        '    If cn.State = ConnectionState.Open Then
        '        cn.Close()
        '    End If
        'Finally
        '    cn.Dispose()
        '    da.Dispose()
        '    ds.Dispose()
        'End Try
        '_gridColumnName()
        If txtSearchMember.Text <> "" Then 'there are phone search'
            'check phone new or old'
            Dim resultMember As Boolean = _checkMemberNew(txtSearchMember.Text.Trim)
            If resultMember Then
                Dim result As Boolean = _checkMemberCreditNew(txtSearchMember.Text.Trim)
                If result Then
                    _getMemberNew(txtSearchMember.Text.Trim)
                Else
                    'old
                    _getMemberOld(txtSearchMember.Text.Trim)
                End If
            Else
                _getMemberOld(txtSearchMember.Text.Trim)
            End If
        Else 'no phone search'
            _getMemberNew("")
        End If

    End Sub

    Private Function _checkMemberCreditNew(msisdn As String) As Boolean
        Dim result As Boolean = False
        Dim sql As String = ""

        sql = "select * from MTOPUP.tbl_credit where telephone like '%" & msisdn.Trim & "%'"
        cn = New OracleConnection
        Dim cm As OracleCommand = New OracleCommand(sql, cn)
        Dim dr As OracleDataReader

        Try
            cn.ConnectionString = conDB.getConnectionWebCallString
            cn.Open()
            dr = cm.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    If dr(3) > 0 Then
                        result = True
                    End If
                End While
            End If
        Catch ex As Exception

        Finally
            cn.Dispose()
            cm.Dispose()
        End Try

        Return result
    End Function

    Private Function _checkMemberNew(msisdn As String) As Boolean
        Dim result As Boolean = False
        Dim sql As String = ""

        sql = "select mtopup.fn_member_credit('" & msisdn.Trim & "') from dual"
        cn = New OracleConnection
        Dim cm As OracleCommand = New OracleCommand(sql, cn)
        Dim dr As OracleDataReader

        Try
            cn.ConnectionString = conDB.getConnectionWebCallString
            cn.Open()
            dr = cm.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    If dr(0) > 0 Then
                        result = True
                    End If
                End While
            End If
        Catch ex As Exception

        Finally
            cn.Dispose()
            cm.Dispose()
        End Try

        Return result
    End Function

    Private Sub _getMemberNew(msisdn As String)
        Dim sql As String = ""
        Dim con As New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter
        Dim ds As New DataSet

        If msisdn <> "" Then
            sql = "select m.member_id,m.telephone,m.member_name,m.member_level,m.member_type,c.credit_amt,m.sts,m.brch_id from MTOPUP.tbl_member m,MTOPUP.tbl_credit c where m.telephone = c.telephone and m.telephone like '%" & msisdn.Trim & "%'"
        Else
            sql = "select m.member_id,m.telephone,m.member_name,m.member_level,m.member_type,c.credit_amt,m.sts,m.brch_id from MTOPUP.tbl_member m,MTOPUP.tbl_credit c where m.telephone = c.telephone and rownum <=100"
        End If

        da = New OracleDataAdapter(sql, con)

        Try
            con.ConnectionString = conDB.getConnectionWebCallString
            con.Open()
            da.Fill(ds, "member")
            gridMemberModal.RequestSource = ds.Tables("member")
            gridMemberModal.GridControlElement.PageIndex = 0
            gridMemberModal.DataBind()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Finally
            con.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
        _gridColumnName()
    End Sub

    Private Sub _getMemberOld(msisdn As String)
        Dim sql As String = ""
        Dim con As New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter
        Dim ds As New DataSet


        sql = "select m.member_id,m.telephone,m.member_name,m.member_level,m.member_type,c.credit_amt,m.sts,m.brch_id from tbl_member m,tbl_credit c where m.telephone = c.telephone and m.telephone like '%" & msisdn.Trim & "%'"

        da = New OracleDataAdapter(sql, con)

        Try
            con.ConnectionString = conDB.getConnectionString
            con.Open()
            da.Fill(ds, "member")
            gridMemberModal.RequestSource = ds.Tables("member")
            gridMemberModal.GridControlElement.PageIndex = 0
            gridMemberModal.DataBind()
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        Finally
            con.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
        _gridColumnName()
    End Sub

    Protected Sub gridMemberModal_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridMemberModal.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            If CDbl(e.Row.Cells(9).Text) > 0 Then
                e.Row.Cells(9).Text = CDbl(e.Row.Cells(9).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(9).Font.Bold = True
            e.Row.Cells(9).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(11).Font.Bold = True
            e.Row.Cells(11).ForeColor = Drawing.Color.CornflowerBlue
            If e.Row.Cells(10).Text.ToString = "1" Then
                e.Row.Cells(10).Text = "Active"
                e.Row.Cells(10).ForeColor = Drawing.Color.Green
                e.Row.Cells(10).Font.Bold = True
            Else
                e.Row.Cells(10).Text = "Deactivation"
                e.Row.Cells(10).ForeColor = Drawing.Color.Red
                e.Row.Cells(10).Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub gridMemberModal_GridRowClick(sender As Object, e As EventArgs) Handles gridMemberModal.GridRowClick
        Dim _row = gridMemberModal.GridControlElement.SelectedRow
        txtMemberID.Value = _row.Cells(4).Text.Trim
        txtTelephone.Value = _row.Cells(5).Text.Trim
        lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalMember();", True)
        _RequestHistoryByMember(CDbl(txtMemberID.Value.ToString.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
    End Sub

    Protected Sub gridMemberModal_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridMemberModal.GridViewPageIndexChanged
        Dim level As Integer = 1
        Dim sql As String = ""
        'If txtSearchMember.Text <> "" Then
        '    sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts" _
        '                      & " from mtopup.tbl_member where member_level={0} and (telephone like '%{1}%' or member_name like '%{1}%') and sts=1 and rownum <= 100 order by member_id desc", level, txtSearchMember.Text.Trim)
        'Else
        '    sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts" _
        '                       & " from mtopup.tbl_member where member_level={0} and sts=1 and rownum <= 100 order by member_id desc", level)
        'End If
        'cn = New OracleConnection
        'Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        'Dim ds As New DataSet
        'Try
        '    'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
        '    cn.ConnectionString = conDB.getConnectionString
        '    cn.Open()
        '    da.Fill(ds, "member")
        '    cn.Close()
        '    gridMemberModal.RequestSource = ds.Tables("member")
        '    gridMemberModal.GridControlElement.PageIndex = e.NewPageIndex
        '    gridMemberModal.DataBind()
        'Catch ex As Exception
        '    'lbShort.ForeColor = Drawing.Color.Red
        '    'lbShort.Text = "Query Data records is error."
        '    If cn.State = ConnectionState.Open Then
        '        cn.Close()
        '    End If
        'Finally
        '    cn.Dispose()
        '    da.Dispose()
        '    ds.Dispose()
        'End Try
        '_gridColumnName()

        If txtSearchMember.Text <> "" Then 'there are phone search'
            'check phone new or old'
            Dim resultMember As Boolean = _checkMemberNew(txtSearchMember.Text.Trim)
            If resultMember Then
                Dim result As Boolean = _checkMemberCreditNew(txtSearchMember.Text.Trim)
                If result Then
                    _getMemberNew(txtSearchMember.Text.Trim)
                Else
                    'old
                    _getMemberOld(txtSearchMember.Text.Trim)
                End If
            Else
                _getMemberOld(txtSearchMember.Text.Trim)
            End If
        Else 'no phone search'
            _getMemberNew("")
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
        txtBranchID.Value = gridBranch.GridControlElement.SelectedRow.Cells(4).Text
        txtTelephone.Value = gridBranch.GridControlElement.SelectedRow.Cells(5).Text
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalBranch();", True)
        _RequestHistoryByBranch(CDbl(txtBranchID.Value.ToString.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
    End Sub

    Protected Sub gridBranch_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridBranch.GridViewPageIndexChanged
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
            gridBranch.GridControlElement.PageIndex = e.NewPageIndex
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

    Protected Sub txtSearchMember_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMember.TextChanged
        _getMember(1)
    End Sub
    Private Sub _RequestHistoryByMember(memberID As Double, date1 As Date, date2 As Date)
        Dim d1 As String = date1.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim d2 As String = date2.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim sql As String = ""

        'sql = String.Format(" select  mtopup.tbl_recharge_log.id, mtopup.tbl_recharge_log.member_id, mtopup.tbl_recharge_log.telephone, mtopup.tbl_recharge_log.recharge_date, mtopup.tbl_recharge_log.recharge_amt, mtopup.tbl_userctrl.user_name, mtopup.tbl_branch.branch_name" _
        '                & " from mtopup.tbl_recharge_log," _
        '                & " mtopup.tbl_branch," _
        '                  & " mtopup.tbl_userctrl," _
        '                  & " mtopup.tbl_member" _
        '                  & " where mtopup.tbl_recharge_log.user_id = mtopup.tbl_userctrl.user_id" _
        '                  & " and mtopup.tbl_recharge_log.member_id=mtopup.tbl_member.member_id" _
        '                  & " and mtopup.tbl_member.brch_id=mtopup.tbl_branch.branch_id" _
        '                  & " and mtopup.tbl_recharge_log.sts = 1" _
        '                 & " and mtopup.tbl_recharge_log.member_id={0}" _
        '                 & " and to_date(mtopup.tbl_recharge_log.recharge_date,'DD-Mon-YY')" _
        '                 & " between '{1}' and '{2}'" _
        '                 & " order by mtopup.tbl_recharge_log.id desc", memberID, d1, d2)
        sql = "select r.telephone,r.member_name,r.recharge_date,r.amt_before,r.amt,r.amt_after,r.user_id,b.branch_name from tbl_recharge_detail r,tbl_member m,tbl_branch b  where  m.telephone = r.telephone And m.brch_id = b.branch_id And  m.member_id='" & memberID & "'  and trunc(r.recharge_date) BETWEEN '" & d1 & "' and '" & d2 & "' order by r.recharge_date desc"
        'sql = "select member_id,telephone,member_name,recharge_date,amt_before,amt,amt_after,user_id from tbl_recharge_detail where member_id='" & memberID & "' and trunc(recharge_date) BETWEEN '" & d1 & "' and '" & d2 & "'  order by recharge_date desc"
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
            gridHistory.RequestSource = ds.Tables(0)
            gridHistory.GridControlElement.PageIndex = 0
            gridHistory.DataBind()


            gridExport.RequestSource = ds.Tables("history")
            gridExport.DataBind()

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

    Private Sub _RequestHistoryByBranch(branchID As Double, date1 As Date, date2 As Date)
        Dim d1 As String = date1.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim d2 As String = date2.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim sql As String = ""
        'sql = String.Format("select  id, member_id, telephone, recharge_date, recharge_amt, user_id" _
        '    & " from mtopup.tbl_recharge_log where sts = 1 and member_id= {0} and to_date(recharge_date,'DD-Mon-YY') between '{1}' and '{2}' order by id desc", CDbl(memberID), d1, d2)

        'sql = String.Format(" select  mtopup.tbl_recharge_log.id, mtopup.tbl_recharge_log.member_id, mtopup.tbl_recharge_log.telephone, mtopup.tbl_recharge_log.recharge_date, mtopup.tbl_recharge_log.recharge_amt, mtopup.tbl_userctrl.user_name, mtopup.tbl_branch.branch_name" _
        '                  & " from mtopup.tbl_recharge_log," _
        '                  & " mtopup.tbl_branch," _
        '                  & " mtopup.tbl_userctrl," _
        '                  & " mtopup.tbl_member" _
        '                  & " where mtopup.tbl_recharge_log.user_id = mtopup.tbl_userctrl.user_id" _
        '                  & " and mtopup.tbl_recharge_log.member_id=mtopup.tbl_member.member_id" _
        '                  & " and mtopup.tbl_member.brch_id=mtopup.tbl_branch.branch_id" _
        '                  & " and mtopup.tbl_recharge_log.sts = 1" _
        '                  & " and mtopup.tbl_branch.branch_id={0}" _
        '                  & " and to_date(mtopup.tbl_recharge_log.recharge_date,'DD-Mon-YY')" _
        '                  & " between '{1}' and '{2}'" _
        '                  & " order by mtopup.tbl_recharge_log.id desc", branchID, d1, d2)

        sql = "select r.telephone,r.member_name,r.recharge_date,r.amt_before,r.amt,r.amt_after,r.user_id,b.branch_name from tbl_recharge_detail r,tbl_member m,tbl_branch b  where  m.telephone = r.telephone And m.brch_id = b.branch_id  and trunc(r.recharge_date) BETWEEN '" & d1 & "' and '" & d2 & "' and m.brch_id='" & branchID & "' order by r.recharge_date desc"

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
            gridHistory.RequestSource = ds.Tables(0)
            gridHistory.GridControlElement.PageIndex = 0
            gridHistory.DataBind()

            gridExport.RequestSource = ds.Tables("history")
            gridExport.DataBind()


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

    Private Sub _RequestHistory(date1 As Date, date2 As Date)
        Dim d1 As String = date1.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim d2 As String = date2.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim sql As String = ""
        'sql = String.Format(" select  mtopup.tbl_recharge_log.id, mtopup.tbl_recharge_log.member_id, mtopup.tbl_recharge_log.telephone, mtopup.tbl_recharge_log.recharge_date, mtopup.tbl_recharge_log.recharge_amt, mtopup.tbl_userctrl.user_name, mtopup.tbl_branch.branch_name" _
        '                 & " from mtopup.tbl_recharge_log," _
        '                 & " mtopup.tbl_branch," _
        '                 & " mtopup.tbl_userctrl," _
        '                 & " mtopup.tbl_member" _
        '                 & " where mtopup.tbl_recharge_log.user_id = mtopup.tbl_userctrl.user_id" _
        '                 & " and mtopup.tbl_recharge_log.member_id=mtopup.tbl_member.member_id" _
        '                 & " and mtopup.tbl_member.brch_id=mtopup.tbl_branch.branch_id" _
        '                 & " and mtopup.tbl_recharge_log.sts = 1" _
        '                 & " and to_date(mtopup.tbl_recharge_log.recharge_date,'DD-Mon-YY')" _
        '                 & " between '{0}' and '{1}'" _
        '                 & " order by mtopup.tbl_recharge_log.id desc", d1, d2)
        sql = "select r.telephone,r.member_name,r.recharge_date,r.amt_before,r.amt,r.amt_after,r.user_id,b.branch_name from tbl_recharge_detail r,tbl_member m,tbl_branch b  where  m.telephone = r.telephone And m.brch_id = b.branch_id  and trunc(r.recharge_date) BETWEEN '" & d1 & "' and '" & d2 & "' order by r.recharge_date desc"
        'sql = "select member_id,telephone,member_name,recharge_date,amt_before,amt,amt_after,user_id from tbl_recharge_detail where trunc(recharge_date) BETWEEN '" & d1 & "' and '" & d2 & "' order by recharge_date desc"
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
            gridHistory.RequestSource = ds.Tables(0)
            gridHistory.GridControlElement.PageIndex = 0
            gridHistory.DataBind()

            gridExport.RequestSource = ds.Tables("history")
            gridExport.DataBind()

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

            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(7).Font.Bold = True
            e.Row.Cells(7).ForeColor = Drawing.Color.OrangeRed
            If CDbl(e.Row.Cells(7).Text) > 0 Then
                e.Row.Cells(7).Text = CDbl(e.Row.Cells(7).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(8).ForeColor = Drawing.Color.OrangeRed
            If CDbl(e.Row.Cells(8).Text) > 0 Then
                e.Row.Cells(8).Text = CDbl(e.Row.Cells(8).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
            e.Row.Cells(9).Font.Bold = True
            e.Row.Cells(9).ForeColor = Drawing.Color.OrangeRed
            If CDbl(e.Row.Cells(9).Text) > 0 Then
                e.Row.Cells(9).Text = CDbl(e.Row.Cells(9).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
        End If
    End Sub

    Protected Sub btnType1_Click(sender As Object, e As EventArgs) Handles btnType1.Click
        lbSearchType.Text = btnType1.Text.ToString
        txtTelephone.Value = ""
        txtMemberID.Value = ""
        txtBranchID.Value = ""
        lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
        lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
        _RequestHistory(EDatePicker1.DateValue.ToShortDateString, EDatePicker2.DateValue.ToShortDateString)
    End Sub

    Protected Sub btnType2_Click(sender As Object, e As EventArgs) Handles btnType2.Click
        lbSearchType.Text = btnType2.Text.ToString
        txtSearchBranch.Text = ""
        txtTelephone.Value = ""
        txtMemberID.Value = ""
        txtBranchID.Value = ""
        _getBranch()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectBranch();", True)
    End Sub

    Protected Sub btnType3_Click(sender As Object, e As EventArgs) Handles btnType3.Click
        lbSearchType.Text = btnType3.Text.ToString
        txtSearchMember.Text = ""
        txtTelephone.Value = ""
        txtMemberID.Value = ""
        txtBranchID.Value = ""
        _getMember(1)
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectMember();", True)
    End Sub

    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        If txtBranchID.Value.Trim.ToString = "" And txtMemberID.Value.Trim.ToString = "" Then
            lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
            lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
            _RequestHistory(EDatePicker1.DateValue.ToShortDateString, EDatePicker2.DateValue.ToShortDateString)
        ElseIf txtBranchID.Value.Trim.ToString <> "" Then
            lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
            lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
            _RequestHistoryByBranch(CDbl(txtBranchID.Value.ToString.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
        ElseIf txtMemberID.Value.Trim.ToString <> "" Then
            lbDate1.Text = EDatePicker1.DateValue.ToString("dd-MMM-yyyy")
            lbDate2.Text = EDatePicker2.DateValue.ToString("dd-MMM-yyyy")
            _RequestHistoryByMember(CDbl(txtMemberID.Value.ToString.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
        End If
    End Sub

    Protected Sub gridExport_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridExport.GridBindCompleted
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        'Dim d = Today.Date.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        'Dim filename As String = "mtopup_recharge_history_l1_" & d & ".xlsx"
        'Dim dt As New DataTable("GridView_Data")
        'For Each cell As TableCell In gridExport.GridControlElement.HeaderRow.Cells
        '    dt.Columns.Add(cell.Text)
        'Next
        'For Each row As GridViewRow In gridExport.GridControlElement.Rows
        '    dt.Rows.Add()
        '    For i As Integer = 0 To row.Cells.Count - 1
        '        dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
        '    Next
        'Next
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
        Dim d = DateTime.Now.ToString("yyyyMMddHHmmss")
        If gridHistory.GridControlElement.Rows.Count > 0 Then
            Try
                gridHistory.GridControlElement.Columns(0).Visible = False
                Response.ClearContent()
                Response.Buffer = True
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "Report-" & d & ".xls"))
                Response.ContentEncoding = Encoding.UTF8
                Response.ContentType = "application/ms-excel"
                Dim sw As New StringWriter()
                Dim htw As New HtmlTextWriter(sw)
                gridHistory.GridControlElement.RenderControl(htw)
                Response.Write(sw.ToString())
                Response.[End]()

            Catch ex As Exception
            Finally
                gridHistory.GridControlElement.Columns(0).Visible = True
            End Try
        End If
    End Sub
End Class