Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.Web.Configuration
Imports nickDevClass
Imports System.Net
Imports System.IO
Imports System.Globalization

Partial Class recharge
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim fc As New SubFunction
    Dim cn As New OracleConnection
    Dim cm As New OracleCommand
    Dim fullpath As String
    Protected theHeadering As String
    Public Structure ProcessRefillModel
        Public Telephone As String
        Public TelephoneIT As String
        Public TelephoneFN As String
        Public Amount As String
        Public OldAmount As String
        Public MemberID As String
        Public ResultCode As String
        Public ResultDesc As String
        Public Name As String
    End Structure
    Public Structure FileLogModel
        Public LogID As String
        Public Msisdn As String
        Public ImagePath As String
        Public ImageName As String
        Public ResultCode As String
        Public ResultDesc As String
        Public DocNo As String
        Public DocType As String
    End Structure
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("username").ToString <> Nothing Or Session("username").ToString <> "" Then
                lbUserName.Text = Session("username").ToString
                If Session("userlevel").ToString = "9" Then
                    _menu()
                    TextBox1.Text = DateTime.Today.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
                    menuMember.Visible = True
                    menuRecharge.Visible = True
                    menuReceive.Visible = True
                    menuReprint.Visible = True
                    menuReceiveHistory.Visible = True
                    menuHistoryL1.Visible = True
                    menuCheckApprove.Visible = True
                    menuHistory.Visible = True
                    menuApprove.Visible = True
                    menuUserCtrl.Visible = True
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
                    menuRechargeHistory.Visible = True
                ElseIf Session("userlevel").ToString = "11" Or Session("userlevel").ToString = "12" Or Session("userlevel").ToString = "13" Then
                    _menu()
                    menuMember.Visible = True
                    menuRecharge.Visible = True
                    cbReport.Visible = True
                    menuRechargeHistory.Visible = True
                    'btnMtopupOld.Disabled = True
                    'panelMtopupOld.Visible = False
                    'panelMtopupNew.Visible = False
                    'panelMtopupBSS.Visible = True
                ElseIf Session("userlevel").ToString = "0" Then
                    Response.Redirect("Default.aspx")
                ElseIf Session("userlevel").ToString = "7" Then
                    Response.Redirect("Default.aspx")
                ElseIf Session("userlevel").ToString = "6" Then
                    Response.Redirect("Default.aspx")
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
            'EnableEventValidation = False
            _Alert()
            EDatePicker1.DateValue = Today.Date
            EDatePicker2.DateValue = Today.Date
            'EDatePicker3.DateValue = Today.Date
            'EDatePicker4.DateValue = Today.Date
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
        menuApprove.Visible = False
        menuUserCtrl.Visible = False
        menuUser.Visible = False
        menuCheckApprove.Visible = False
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
    Sub _Alert()
        alertVASSuccess.Visible = False
        alertNewMoney.Visible = False
        alertSaveError.Visible = False
        alertSaveSucess.Visible = False
        alertSelectMember.Visible = False
        alertPassword.Visible = False
        alertPasswordEmpty.Visible = False
        alertVASError.Visible = False
        alertImage.Visible = False
        '///MTOPUP PLUS
        'AlertAmountEmptyPlus.Visible = False
        'AlertSaveSucessPlus.Visible = False
        'AlertSaveFailPlus.Visible = False
        'AlertTelephoneEmptyPlus.Visible = False
        'AlertAmountOver.Visible = False
        'AlertOverBalance.Visible = False
        'AlertMaxBalance.Visible = False
    End Sub
    Sub _btn(ByVal status As Boolean)
        If status Then
            btnSave.Disabled = True
            btnCancel.Disabled = True
            btnITApprove.Disabled = True
            btnFinanceApprove.Disabled = True
        Else
            btnSave.Disabled = False
            btnCancel.Disabled = False
            btnITApprove.Disabled = False
            btnFinanceApprove.Disabled = False
        End If
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
            'gridMemberModal.GridControlElement.HeaderRow.Cells(11).Text = "ຈຳນວນ Data"

        End If
        'If GridMemberPlus.GridControlElement.Rows.Count <> 0 Then
        '    GridMemberPlus.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(6).Text = "ຊື່ສະມາຊິກ"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(7).Text = "Level"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(8).Text = "ປະເພດສະມາຊິກ"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(10).Text = "ຈຳນວນເງິນທີ່ມີ"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(12).Text = "ສະຖານະ"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(13).Text = "ລະຫັດກຸ່ມ"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(14).Text = "ກຸ່ມ"
        '    GridMemberPlus.GridControlElement.HeaderRow.Cells(15).Text = "ຈຳນວນ Data"
        'End If

        If gridHistory.GridControlElement.Rows.Count <> 0 Then
            gridHistory.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridHistory.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
            gridHistory.GridControlElement.HeaderRow.Cells(6).Text = "ວັນທີທີ່ເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(7).Text = "ຈຳນວນເງິນທີ່ເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(8).Text = "ຜູ້ທີ່ທຳການເຕີມໃຫ້"
        End If

        'If GridHistoryPlus.GridControlElement.Rows.Count <> 0 Then
        '    GridHistoryPlus.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
        '    GridHistoryPlus.GridControlElement.HeaderRow.Cells(6).Text = "ໝາຍເລກໂທລະສັບ"
        '    GridHistoryPlus.GridControlElement.HeaderRow.Cells(7).Text = "ວັນທີທີ່ເຕີມ"
        '    GridHistoryPlus.GridControlElement.HeaderRow.Cells(8).Text = "ຈຳນວນເງິນທີ່ເຕີມ"
        '    GridHistoryPlus.GridControlElement.HeaderRow.Cells(9).Text = "ຜູ້ທີ່ທຳການເຕີມໃຫ້"
        'End If

        If GridControlITApprove.GridControlElement.Rows.Count <> 0 Then
            GridControlITApprove.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            GridControlITApprove.GridControlElement.HeaderRow.Cells(4).Text = "ຜູ້ອະນຸມັດ"
            GridControlITApprove.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
            GridControlITApprove.GridControlElement.HeaderRow.Cells(6).Text = "ພະແນກ"
            GridControlITApprove.GridControlElement.HeaderRow.Cells(7).Text = "ສະຖານະ"
        End If
    End Sub
    Protected Sub btnTelephone_ServerClick(sender As Object, e As EventArgs) Handles btnTelephone.ServerClick
        txtSearchMember.Text = ""
        _getMember(1)
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectMember();", True)
        _btn(False)
        btnSave.Disabled = True
        btnFinanceApprove.Disabled = True
    End Sub
    Private Sub _getMember(level As Integer)
        Dim sql As String = ""
        Dim da As OracleDataAdapter = New OracleDataAdapter
        Dim ds As New DataSet

        '/// MTOPUP OLD
        If level = 1 Then
            'If Session("userid").ToString.Trim = "NICK" And Session("userid").ToString.Trim = "KHEM" And Session("userid").ToString.Trim = "thely" And Session("userid").ToString.Trim = "SAYCHAI" Then
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
            'End If

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

    Private Sub _getUserApprove(level As Integer)
        Dim sql As String = ""
        '/// MTOPUP OLD
        If level = 1 Then
            '//IT
            sql = "select * from tbl_user_approve where status = 1 and department = 'IT' order by 1 asc"
        Else
            '//Finance
            sql = "select * from tbl_user_approve where status = 1 and department = 'FINANCE' order by 1 asc"
        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "approve")
            cn.Close()
            GridControlITApprove.RequestSource = ds.Tables("approve")
            GridControlITApprove.GridControlElement.PageIndex = 0
            GridControlITApprove.DataBind()
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

            'If e.Row.Cells(11).Text Is Nothing Then
            '    e.Row.Cells(11).Text = "0"
            'Else
            '    e.Row.Cells(11).Text = CDbl(e.Row.Cells(11).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)

            'End If
        End If
    End Sub
    Protected Sub gridMemberModal_GridRowClick(sender As Object, e As EventArgs) Handles gridMemberModal.GridRowClick
        _Alert()
        Dim _row = gridMemberModal.GridControlElement.SelectedRow
        txtMemberID.Value = _row.Cells(4).Text.Trim
        txtTelephone.Value = _row.Cells(5).Text.Trim
        txtMemberNameMT.Value = _row.Cells(6).Text.Trim
        txtOldCredit.Value = _row.Cells(9).Text.Trim
        'txtOldData.Value = _row.Cells(15).Text.Trim
        txtNewCredit.Value = ""
        EDatePicker1.DateValue = Today.Date
        EDatePicker2.DateValue = Today.Date
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalMember();", True)
        txtNewCredit.Focus()
        _RequestHistory(txtTelephone.Value.Trim, EDatePicker1.DateValue, EDatePicker2.DateValue)
    End Sub
    Protected Sub gridMemberModal_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridMemberModal.GridViewPageIndexChanged
        Dim level As Integer = 1
        Dim sql As String = ""



        If txtSearchMember.Text <> "" Then
            sql = String.Format("Select  tbl_member.member_id," _
            & " tbl_member.telephone," _
            & " tbl_member.member_name," _
            & " tbl_member.member_level," _
            & " tbl_member.member_type," _
            & " mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) As root_number," _
            & " mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) As member_credit," _
            & " tbl_member.user_id," _
            & " tbl_member.sts," _
            & " tbl_member.brch_id," _
            & " tbl_branch.branch_name," _
            & " mtopup.FN_CHECKING_DATA(tbl_member.telephone) As member_data" _
            & " from mtopup.tbl_member, mtopup.tbl_branch" _
            & " where tbl_member.member_level={0}" _
            & " And (tbl_member.telephone Like '%{1}%'" _
            & " or tbl_member.member_name Like '%{1}%'" _
            & " or tbl_branch.branch_name Like '%{1}%')" _
            & " and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id" _
            & " and rownum <= 100 order by tbl_member.member_id desc", level, txtSearchMember.Text.Trim)
        Else
            sql = String.Format("select tbl_member.member_id," _
            & " tbl_member.telephone," _
            & " tbl_member.member_name," _
            & " tbl_member.member_level," _
            & " tbl_member.member_type," _
            & " mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number," _
            & " mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit," _
            & " tbl_member.user_id," _
            & " tbl_member.sts," _
            & " tbl_member.brch_id," _
            & " tbl_branch.branch_name," _
            & " mtopup.FN_CHECKING_DATA(tbl_member.telephone) As member_data" _
            & " from mtopup.tbl_member,mtopup.tbl_branch" _
            & " where tbl_member.member_level={0}" _
            & " And tbl_member.sts=1 And tbl_member.brch_id=tbl_branch.branch_id" _
            & " And rownum <= 100 order by tbl_member.member_id desc", level)
        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "member")
            cn.Close()
            gridMemberModal.RequestSource = ds.Tables("member")
            gridMemberModal.GridControlElement.PageIndex = e.NewPageIndex
            gridMemberModal.DataBind()
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
    Protected Sub txtSearchMember_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMember.TextChanged
        _getMember(1)
    End Sub
    Protected Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        System.Threading.Thread.Sleep(200)
        _Alert()
        If txtMemberNameMT.Value = "" Then
            _Alert()
            alertSelectMember.Visible = True
            btnTelephone.Focus()
            Exit Sub
        End If
        If txtNewCredit.Value = "" Then
            _Alert()
            alertNewMoney.Visible = True
            txtNewCredit.Focus()
            Exit Sub
        End If
        If Image1.ImageUrl = "" Then
            btnUpload.Focus()
            Exit Sub
        End If
        If rbDocType.SelectedValue = 1 Then
            If textdocNo.Value = "" Then
                textdocNo.Focus()
                Exit Sub
            Else
                _Alert()
                Dim docNo As String = textdocNo.Value.Trim
                Dim selectDocNoResult As Boolean = _selectDocNo(docNo)
                If selectDocNoResult Then
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "Script", "modalDocument();", True)
                    Exit Sub
                End If
            End If
        End If

        btConfirmSave2.Disabled = False
        lbTel.Text = txtTelephone.Value.Trim
        lbTel2.Text = txtTelephone.Value.Trim
        lbTelephone2.Text = txtTelephone.Value.Trim
        lbNewMoney2.Text = txtNewCredit.Value.Trim
        'lbNewMoney3.Text = txtNewCredit.Value.Trim
        lbNewMoney.Text = txtNewCredit.Value.Trim
        ' lbOldMoney.Text = txtOldCredit.Value.Trim
        Dim a As Double = CDbl(txtOldCredit.Value.Replace(",", ""))
        Dim b As Double = CDbl(txtNewCredit.Value.Replace(",", ""))
        '  lbUpdateMoney.Text = (a + b).ToString(String.Format("###,###,###,###,###,###"))
        txtConfirmPassword.Value = ""
        txtConfirmPassword.Focus()
        Session.Add("canCharge", "Yes")
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
        txtConfirmPassword.Focus()
    End Sub
    Protected Sub btCancel_ServerClick(sender As Object, e As EventArgs) Handles btnCancel.ServerClick
        _Alert()
        txtMemberID.Value = ""
        txtTelephone.Value = ""
        txtMemberNameMT.Value = ""
        txtOldCredit.Value = ""
        txtNewCredit.Value = ""
        EDatePicker1.DateValue = Today.Date
        EDatePicker2.DateValue = Today.Date
        gridHistory.RequestSource = Nothing
    End Sub
    Protected Sub btnAlertError_ServerClick(sender As Object, e As EventArgs) Handles btnAlertError.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertNewMoney_ServerClick(sender As Object, e As EventArgs) Handles btnAlertNewMoney.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertSelectMember_ServerClick(sender As Object, e As EventArgs) Handles btnAlertSelectMember.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertSuccess_ServerClick(sender As Object, e As EventArgs) Handles btnAlertSuccess.ServerClick
        _Alert()
    End Sub
    Protected Sub btConfirmSave2_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave2.ServerClick
        If Session("canCharge") = "No" Then
            If Session("nick2") = "S" Then
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalSave();", True)
                _Alert()
                alertSaveSucess.Visible = True
                txtNewCredit.Value = ""
                '   txtOldCredit.Value = lbUpdateMoney.Text
                txtNewCredit.Focus()
                _RequestHistory(CDbl(txtMemberID.Value.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
            ElseIf Session("nick2") = "E" Then
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalSave();", True)
                _Alert()
                alertSaveError.Visible = True
            End If
        Else
            Session("canCharge") = "No"
            If txtConfirmPassword.Value = "" Then
                _Alert()
                alertPasswordEmpty.Visible = True
                Session("canCharge") = "Yes"
                Exit Sub
            End If
            btConfirmSave2.Disabled = True
            If _Login(txtConfirmPassword.Value.Trim) = False Then
                Session("canCharge") = "Yes"
                _Alert()
                alertPassword.Visible = True
                txtConfirmPassword.Value = ""
                btConfirmSave2.Disabled = False
            Else
                btConfirmSave2.Disabled = False
                '///Insert Refill
                _RequestHistory(CDbl(txtMemberID.Value.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
                Dim Result As New ProcessRefillModel
                Result.MemberID = txtMemberID.Value.Trim
                Result.Amount = txtNewCredit.Value.Trim
                Result.Name = txtMemberNameMT.Value.Trim
                Result.OldAmount = txtOldCredit.Value.Trim
                Result.ResultCode = "01"
                Result.ResultDesc = "VAS Refill"
                Result.Telephone = txtTelephone.Value.Trim
                Result.TelephoneFN = txtFinanceMsisdn.Value.Trim
                Result.TelephoneIT = txtITMsisdn.Value.Trim
                Dim _log As String = _InsertRefill(Result)
                If _log > 0 Then
                    Dim _model As New FileLogModel
                    '//Insert file
                    Dim _imagepath As String = ""
                    Dim _imagename As String = ""
                    If Session("filename") IsNot Nothing Then
                        _model.LogID = _log
                        _model.Msisdn = txtTelephone.Value.Trim
                        _model.ImagePath = Session("fullpath")
                        _model.ImageName = Session("filename")
                        _model.ResultCode = ""
                        _model.ResultDesc = ""
                        _model.DocNo = ""
                        _model.DocType = rbDocType.SelectedItem.Text
                        If rbDocType.SelectedValue = 2 Then
                            'select id'
                            Dim resultSelect As Integer = _selectId()
                            'then +1'
                            resultSelect += 1
                            _model.DocNo = resultSelect.ToString
                            'update'
                            _updateID(resultSelect)
                        ElseIf rbDocType.SelectedValue = 1 Then
                            _model.DocNo = textdocNo.Value.Trim
                        End If
                        '_model.DocType = rbDocType.SelectedItem.Text
                        If Session("fullpath") IsNot Nothing Then
                            InsertFileLog(_model)
                        End If
                    End If

                    If Result.TelephoneIT IsNot Nothing Then
                        InsertMemberRefill(_log, txtITMsisdn.Value.Trim, "IT")
                    End If
                    If Result.TelephoneFN IsNot Nothing Then
                        InsertMemberRefill(_log, txtFinanceMsisdn.Value.Trim, "FN")
                    End If


                    '//Submit SMS
                    Dim ltc As New ltcService.LTCService
                    'Dim serviceSMS As New sms.smservice
                    'Dim _ltc As New API.SMS
                    Dim _SMSAPI As New SMSAPI.smservice
                    Dim _user As String = "MTOPUP"
                    Dim _pass As String = "mtopup@123"
                    Dim _sms As New ltcService.ResultSentSMS
                    Dim _msgIT As String = txtITApprove.Value.Trim & " ກະລຸນາອະນຸມັດການຕື່ມເງິນ M-Topup ຂອງບໍລິສັດ " & Result.Name.Trim _
                                        & " http://mtopupplus.laotel.com/home/index?id=" & _log & "&type=IT"
                    'Dim _result As Boolean = _ltc.SubmitMessage(Result.TelephoneIT, _msgIT, "MTOPUP").ResultStatus
                    Dim _result As Boolean = _SMSAPI.SubmitSMS(Result.TelephoneIT, _msgIT, "MTOPUP")

                    If _result Then
                        Dim _msgFN As String = txtFinanceApprove.Value.Trim & " ກະລຸນາອະນຸມັດການຕື່ມເງິນ M-Topup ຂອງບໍລິສັດ " & Result.Name.Trim _
                                      & " http://mtopupplus.laotel.com/home/index?id=" & _log & "&type=FN"
                        'Dim _res As Boolean = _ltc.SubmitMessage(Result.TelephoneFN, _msgFN, "MTOPUP").ResultStatus
                        Dim _res As Boolean = _SMSAPI.SubmitSMS(Result.TelephoneFN, _msgFN, "MTOPUP")

                        If _res Then
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalSave();", True)
                            _Alert()
                            alertVASSuccess.Visible = True
                            lbAmountVAS.Text = ", ຈຳນວນເງິນທີ່ຕື່ມ: " & txtNewCredit.Value
                            lbOldAmountVAS.Text = ", ຈຳນວນເງິນກ່ອນຕື່ມ: " & txtOldCredit.Value
                            lbTelephoneVAS.Text = " ເບີໂທ: " & txtTelephone.Value
                            txtNewCredit.Focus()
                            txtNewCredit.Value = ""
                            _RequestHistory(CDbl(txtMemberID.Value.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
                        Else
                            _Alert()
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalSave();", True)
                            lbVASERR.Text = _sms.ErrDesc
                            alertVASError.Visible = True
                        End If
                    End If
                End If

                'txtLogID.Value = _InsertRechargeLog(txtMemberID.Value.Trim, txtTelephone.Value.Trim, txtNewCredit.Value.Trim)
                'If CLng(txtLogID.Value) <> 0 Then
                '    If _InsertRecharge(txtMemberID.Value.Trim, txtTelephone.Value.Trim, txtNewCredit.Value.Trim) = True Then
                '        _UpdateRechargeLog(1, txtLogID.Value.Trim)
                '        fc.saveLog(Session("userid").ToString, "Recharge Money - [MemberID:" & txtMemberID.Value.ToString & "] [Telephone:" & txtTelephone.Value.ToString & "] [Amount:" & txtNewCredit.Value.ToString & "]")
                '        Session("nick2") = "S"
                '        _Alert()
                '        alertSaveSucess.Visible = True
                '        txtNewCredit.Value = ""
                '        txtOldCredit.Value = lbUpdateMoney.Text
                '        txtNewCredit.Focus()
                '        _RequestHistory(CDbl(txtMemberID.Value.Trim), EDatePicker1.DateValue, EDatePicker2.DateValue)
                '    Else
                '        '// recharge br sum let.
                '        _UpdateRechargeLog(0, txtLogID.Value.Trim)
                '        Session("nick2") = "E"
                '        _Alert()
                '        alertSaveError.Visible = True
                '    End If
                'Else
                '    '/// insert log br lum let der kha derrrr.
                '    Session("nick2") = "E"
                '    _Alert()
                '    alertSaveError.Visible = True
                'End If

            End If
        End If
    End Sub

    Private Function _selectId() As Integer
        Dim result As Integer = 0
        Dim sql As String = ""
        sql = "SELECT * FROM tbl_auto_increase"
        cn = New OracleConnection
        Dim cm As OracleCommand = New OracleCommand(sql, cn)
        Dim dr As OracleDataReader

        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            dr = cm.ExecuteReader
            If dr.Read Then
                result = dr(0)
            End If

        Catch ex As Exception

        Finally
            cn.Dispose()
            cm.Dispose()
        End Try
        Return result
    End Function

    Private Function _selectDocNo(docNo As String) As Boolean
        Dim result As Boolean = False
        Dim sql As String = ""
        sql = String.Format("select * from tbl_file_log where doc_no='{0}'", docNo)
        cn = New OracleConnection
        Dim cm As OracleCommand = New OracleCommand(sql, cn)
        Dim dr As OracleDataReader
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            dr = cm.ExecuteReader
            If dr.HasRows Then
                result = True
            End If

        Catch ex As Exception

        Finally
            cn.Dispose()
            cm.Dispose()
        End Try

        Return result
    End Function
    Private Sub _updateID(id As Integer)
        Dim sql As String = ""
        sql = String.Format("update tbl_auto_increase set id = {0}", id)
        cn = New OracleConnection
        Dim cm As OracleCommand = New OracleCommand(sql, cn)

        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            cm.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            cn.Dispose()
            cm.Dispose()
        End Try

    End Sub
    Private Function _InsertRechargeLog(ByVal MemberID As String, ByVal Telephone As String, ByVal Amount As String) As Long
        Dim logID As String = ""
        Dim conn As New OracleConnection
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim amt As Double = CDbl(Amount.Trim.Replace(",", ""))
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertRechargeLog, conn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_MEMBER_ID", OracleDbType.Int64).Value = CDbl(MemberID.Trim)
                cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = Telephone.Trim
                cm.Parameters.Add("P_RECHARGE_AMT", OracleDbType.Int64).Value = amt
                cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 10).Value = Session("userid").ToString.Trim
                Dim P_ID As New OracleParameter("P_ID", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
                cm.Parameters.Add(P_ID)
                cm.ExecuteNonQuery()
                Try
                    logID = P_ID.Value.ToString()
                Catch ex As Exception
                    lbError.Text = ex.ToString
                End Try
            End With
        Catch ex As Exception
            Dim a As String = ex.ToString
            lbError.Text = a.ToString
        Finally
            conn.Close()
            conn.Dispose()
            cm.Dispose()
        End Try
        Return logID
    End Function
    Private Function _InsertRefill(model As ProcessRefillModel) As String
        Dim logID As String = ""
        Dim conn As New OracleConnection
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim amt As Double = CDbl(model.Amount.Trim.Replace(",", ""))
                Dim Oldamt As Double = CDbl(model.OldAmount.Trim.Replace(",", ""))
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertProcessRefill, conn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("p_msisdn", OracleDbType.NVarchar2, 20).Value = model.Telephone.Trim
                cm.Parameters.Add("p_msisdn_approce_it", OracleDbType.NVarchar2, 20).Value = model.TelephoneIT.Trim
                cm.Parameters.Add("p_msisdn_approce_fn", OracleDbType.NVarchar2, 20).Value = model.TelephoneFN.Trim
                cm.Parameters.Add("p_userid", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString.Trim
                cm.Parameters.Add("p_vas_status", OracleDbType.Int64).Value = 1
                cm.Parameters.Add("p_it_status", OracleDbType.Int64).Value = 0
                cm.Parameters.Add("p_fn_status", OracleDbType.Int64).Value = 0
                cm.Parameters.Add("p_result_code", OracleDbType.NVarchar2, 20).Value = model.ResultCode.Trim
                cm.Parameters.Add("p_result_desc", OracleDbType.NVarchar2, 120).Value = model.ResultDesc.Trim
                cm.Parameters.Add("p_amount", OracleDbType.Int64).Value = amt
                cm.Parameters.Add("p_old_amount", OracleDbType.Int64).Value = Oldamt
                cm.Parameters.Add("p_name", OracleDbType.NVarchar2, 50).Value = model.Name.Trim
                cm.Parameters.Add("p_member_id", OracleDbType.Int64).Value = model.MemberID.Trim
                Dim P_ID As New OracleParameter("p_id", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
                cm.Parameters.Add(P_ID)
                cm.ExecuteNonQuery()
                Try
                    logID = P_ID.Value.ToString()
                Catch ex As Exception
                    ' lbError.Text = ex.ToString
                End Try
            End With
        Catch ex As Exception
            Dim a As String = ex.ToString
            lbError.Text = a.ToString
        Finally
            conn.Close()
            conn.Dispose()
            cm.Dispose()
        End Try
        Return logID
    End Function
    Private Function _InsertRecharge(ByVal MemberID As String, ByVal Telephone As String, ByVal Amount As String) As Boolean
        Dim _return As Boolean = False
        Dim _con As New OracleConnection
        Try
            With _con
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim amt As Double = CDbl(Amount.Trim.Replace(",", ""))
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlRecharge2, _con) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_MEMBER_ID", OracleDbType.Int64).Value = CDbl(MemberID.Trim)
                cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = Telephone.Trim
                cm.Parameters.Add("P_RECHARGE_AMT", OracleDbType.Int64).Value = amt
                cm.ExecuteNonQuery()
                _return = True
            End With
        Catch ex As Exception
            Dim a As String = ex.ToString
            lbError.Text = a.ToString
            _return = False
        Finally
            _con.Close()
            _con.Dispose()
            cm.Dispose()
        End Try
        Return _return
    End Function
    Private Function _UpdateRechargeLog(sts As Integer, ByVal LogID As String) As Boolean
        Dim _return As Boolean = False
        Dim _conn As New OracleConnection
        Try
            With _conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                ' Dim amt As Double = CDbl(txtNewCredit.Value.Trim.Replace(",", ""))
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlUpdateRechargeLog, _conn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_ID", OracleDbType.Int64).Value = CDbl(LogID.Trim)
                cm.Parameters.Add("P_STS", OracleDbType.Int64).Value = sts
                cm.ExecuteNonQuery()
                _return = True
            End With
        Catch ex As Exception
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        Return _return
    End Function
    Private Sub _RequestHistory(telephone As String, date1 As Date, date2 As Date)


        Dim d1 As String = date1.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim d2 As String = date2.ToString("dd-MMM-yy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat)
        Dim _date As String = ""
        Dim sql As String = ""
        Dim cn As OracleConnection = New OracleConnection()
        Dim da As OracleDataAdapter = New OracleDataAdapter()
        Dim ds As New DataSet

        'sql = String.Format("select  id, member_id, telephone, recharge_date, recharge_amt, user_id" _
        '    & " from mtopup.tbl_recharge_log where sts = 1 And member_id= {0} And to_date(recharge_date,'DD-Mon-YY') between '{1}' and '{2}' order by id desc", CDbl(memberID), d1, d2)



        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()

            'sql = "select  id, member_id, telephone, trunc(recharge_date), recharge_amt, user_id from tbl_recharge_log where telephone= '" & telephone & "' And trunc(recharge_date) between '" & d1 & "' and '" & d2 & "' order by id desc"
            sql = "select id, msisdn, recorddate, amount, userid from tbl_process_refill where vasstatus=1 and itstatus=1 and financestatus=1  and msisdn= '" & telephone & "' And trunc(recorddate) between '" & d1 & "' and '" & d2 & "'  order by id desc"

            ds.Clear()
            da = New OracleDataAdapter(sql, cn)
            da.Fill(ds, "history")
            cn.Close()
            gridHistory.RequestSource = ds.Tables("history")
            gridHistory.GridControlElement.PageIndex = 0
            gridHistory.DataBind()

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                _date = "<tr><td style='width:10%;'>" & i & "</td> <td style='width:25%;'>" & ds.Tables(0).Rows(i).Item(2) & "</td> <td style='width:25%;'>" & ds.Tables(0).Rows(i).Item(3) & "</td> <td style='width:25%;'>" & ds.Tables(0).Rows(i).Item(4) & "</td> <td style='width:15%;'>" & ds.Tables(0).Rows(i).Item(5) & "</td></tr>"
                theHeadering += _date
            Next


            'theHeadering = "<td>1</td> <td> test</td> <td> test</td> <td>test </td> <td>test </td><br/>"


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
            e.Row.Cells(4).Visible = False

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(7).Font.Bold = True
            e.Row.Cells(7).ForeColor = Drawing.Color.OrangeRed
            If CDbl(e.Row.Cells(7).Text) <> 0 Then
                'e.Row.Cells(8).Text = CDbl(e.Row.Cells(8).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
                e.Row.Cells(7).Text = CDbl(e.Row.Cells(7).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
        End If
    End Sub
    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        _RequestHistory(txtTelephone.Value.Trim, EDatePicker1.DateValue, EDatePicker2.DateValue)

    End Sub
    Protected Sub gridHistory_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridHistory.GridViewPageIndexChanged
        Dim d1 As String = EDatePicker1.DateValue.ToString("dd-MMM-yy")
        Dim d2 As String = EDatePicker2.DateValue.ToString("dd-MMM-yy")
        Dim sql As String = ""
        sql = String.Format("select  id, member_id, telephone, recharge_date, recharge_amt, user_id" _
            & " from mtopup.tbl_recharge_log where sts = 1 and member_id= {0} and to_date(recharge_date,'DD-Mon-YY') between '{1}' and '{2}' order by id desc", CDbl(txtMemberID.Value.Trim), d1, d2)
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "history")
            cn.Close()
            gridHistory.RequestSource = ds.Tables("history")
            'gridHistory.GridControlElement.PageIndex = e.NewPageIndex
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
    Public Structure UserResult
        Dim STATUS As Boolean
        Dim USR As String
        Dim NAME As String
        Dim LEVEL As Integer
        Dim DESC As String
    End Structure
    Private UserDetail As New UserResult
    Private Function _Login(ByVal Password As String) As Boolean
        Dim _return As Boolean = False
        UserDetail.STATUS = False
        UserDetail.USR = Session("userid")
        UserDetail.NAME = ""
        UserDetail.LEVEL = -1
        UserDetail.DESC = ""
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            cm = New OracleCommand(myDataType.sqlQueryUser.Trim, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 10).Value = UserDetail.USR.Trim
            Dim USER_PASS As New OracleParameter("P_USER_PASS", OracleDbType.NVarchar2, 200) With {.Direction = ParameterDirection.Output}
            Dim USER_NAME As New OracleParameter("P_USER_NAME", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
            Dim USER_LEVEL As New OracleParameter("P_USER_LEVEL", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            Dim USER_DESC As New OracleParameter("P_USER_DESC", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
            Dim USER_BRCH As New OracleParameter("P_BRANCH_ID", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            cm.Parameters.Add(USER_PASS)
            cm.Parameters.Add(USER_NAME)
            cm.Parameters.Add(USER_LEVEL)
            cm.Parameters.Add(USER_DESC)
            cm.Parameters.Add(USER_BRCH)
            cm.ExecuteNonQuery()
            If Not USER_PASS.IsNullable Then
                If fc.pp(Password.Trim) = (USER_PASS.Value).ToString.Trim Then
                    UserDetail.STATUS = True
                    UserDetail.NAME = IIf(USER_NAME.IsNullable, "", (USER_NAME.Value).ToString.Trim)
                    UserDetail.LEVEL = IIf(USER_LEVEL.IsNullable, 0, CInt(USER_LEVEL.Value))
                    UserDetail.DESC = IIf(USER_DESC.IsNullable, "", (USER_DESC.Value).ToString.Trim)
                End If
            End If
        Catch ex As Exception
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        If UserDetail.STATUS = True Then
            'password thuek torng
            _return = True
        Else
            'password br thuekkkk
            _return = False
        End If
        Return _return
    End Function
    Function AddComma(ByVal Values As String) As String
        Dim result As String = ""
        If Values.StartsWith("-") Then
            result = Values.Trim
        Else
            If Values.Length = 11 Then
                result = Values.Insert(2, ",").Insert(6, ",").Insert(10, ",")
            ElseIf Values.Length = 10 Then
                result = Values.Insert(1, ",").Insert(5, ",").Insert(9, ",")
            ElseIf Values.Length = 9 Then
                result = Values.Insert(3, ",").Insert(7, ",")
            ElseIf Values.Length = 8 Then
                result = Values.Insert(2, ",").Insert(6, ",")
            ElseIf Values.Length = 7 Then
                result = Values.Insert(1, ",").Insert(5, ",")
            ElseIf Values.Length = 6 Then
                result = Values.Insert(3, ",")
            ElseIf Values.Length = 5 Then
                result = Values.Insert(2, ",")
            ElseIf Values.Length = 4 Then
                result = Values.Insert(1, ",")
            Else
                result = Values
            End If
        End If
        Return result
    End Function
    Private Sub btnITApprove_ServerClick(sender As Object, e As EventArgs) Handles btnITApprove.ServerClick
        _getUserApprove(1)
        btnITApprove.Disabled = True
        btnFinanceApprove.Disabled = False
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalITConfirm();", True)
    End Sub
    Private Sub btnFinanceApprove_ServerClick(sender As Object, e As EventArgs) Handles btnFinanceApprove.ServerClick
        _getUserApprove(2)
        btnFinanceApprove.Disabled = True
        btnSave.Disabled = False
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalITConfirm();", True)
    End Sub
    Private Sub GridControlITApprove_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles GridControlITApprove.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            'e.Row.Cells(8).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            'e.Row.Cells(8).Visible = False
            ' e.Row.Cells(0).Text = "ເລືອກ"
        End If
    End Sub
    Private Sub GridControlITApprove_GridRowClick(sender As Object, e As EventArgs) Handles GridControlITApprove.GridRowClick
        Try
            Dim ltc As New ltcService.LTCService
            Dim _msisdn As String = "None"
            Dim _name As String = "None"
            Dim _user As String = "MTOPUP"
            Dim _pass As String = "mtopup@123"
            Dim _row = GridControlITApprove.GridControlElement.SelectedRow
            _name = _row.Cells(4).Text.Trim
            _msisdn = _row.Cells(5).Text.Trim
            'Dim _msg As String = "ສະບາຍດີ " & _name & " ກະລຸນາເຂົ້າໄປອະນຸມັດການຕື່ມເງິນ M-Topup ຂອງບໍລິສັດ " & txtMemberNameMT.Value.Trim _
            '                    & "https://authen.laotel.com/Account/Login?fromsite=https://mtopupplus.laotel.com"
            If _row.Cells(6).Text.Trim = "IT" Then
                '///Send SMS IT
                txtITMsisdn.Value = _row.Cells(5).Text.Trim
                txtITApprove.Value = _row.Cells(4).Text.Trim
                'Dim smsResult As New ltcService.ResultSentSMS
                'smsResult = ltc.SendSMS(_msisdn.Trim, _msg, _user, _pass)
                'Dim _resultCode As String = smsResult.ErrCode
                'Dim _resultDesc As String = smsResult.ErrDesc
            Else
                txtFinanceMsisdn.Value = _row.Cells(5).Text.Trim
                txtFinanceApprove.Value = _row.Cells(4).Text.Trim
            End If
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "CloseModalITConfirm();", True)
        Catch ex As Exception

        End Try
    End Sub
    Private Function SubmitSMS(ByVal Reciept As String, ByVal Message As String, Optional ByVal header As String = "LaoTelecom") As Boolean
        Dim rsult As Boolean = False
        Dim RequetURL As String = "http://10.30.6.95:8800?"
        Dim SmsData As String
        Dim Request As WebRequest
        Dim Response As HttpWebResponse
        Message = Message
        SmsData = "PhoneNumber=" & Reciept.Trim & ""
        SmsData &= "&Text=" & Message & "&"
        SmsData &= "Sender=" & header
        Request = WebRequest.Create(RequetURL & SmsData)
        Try
            Response = Request.GetResponse
            If Response.StatusDescription.ToUpper.Trim = "OK" Then
                rsult = True
            End If
            Response.Close()
        Catch ex As Exception
            rsult = False
        End Try
        Return rsult
    End Function
    Private Function Encodeurl(ByVal str As String) As String
        str = str.Replace("%", "%25")
        str = str.Replace("+", "%2B")
        str = str.Replace("""", "%22")
        str = str.Replace("<", "%3C")
        str = str.Replace(">", "%3E")
        str = str.Replace("&", "%26")
        str = str.Replace("#", "%23")
        str = str.Replace("*", "%2A")
        str = str.Replace("!", "%21")
        str = str.Replace(",", "%2C")
        str = str.Replace("‘", "%27")
        str = str.Replace("\", "%5C")
        str = str.Replace("=", "%3D")
        str = str.Replace("@", "%40")
        str = str.Replace("_", "%5F")
        Return str
    End Function

    Private Function InsertFileLog(model As FileLogModel) As Boolean
        Dim result As Boolean = False
        Dim conn As New OracleConnection
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertFileLog, conn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_RECHARGE_ID", OracleDbType.NVarchar2, 20).Value = model.LogID.Trim
                cm.Parameters.Add("P_MSISDN", OracleDbType.NVarchar2, 20).Value = model.Msisdn.Trim
                cm.Parameters.Add("P_IMAGE_PATH", OracleDbType.NVarchar2, 250).Value = model.ImagePath.Trim
                cm.Parameters.Add("P_IMAGE_NAME", OracleDbType.NVarchar2, 50).Value = model.ImageName.Trim
                cm.Parameters.Add("P_DOC_NO", OracleDbType.NVarchar2, 100).Value = model.DocNo.Trim
                cm.Parameters.Add("P_DOC_TYPE", OracleDbType.NVarchar2, 50).Value = model.DocType.Trim
                cm.ExecuteNonQuery()
                result = True
            End With
        Catch ex As Exception
            Dim errr As String = ex.Message
        Finally
            conn.Close()
            conn.Dispose()
            cm.Dispose()
        End Try
        Return result
    End Function

    Private Function InsertMemberRefill(ByVal Id As String, ByVal MsisdnIT As String, ByVal Type As String) As Boolean
        Dim result As Boolean = False
        Dim conn As New OracleConnection
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertRefillLog, conn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("p_id", OracleDbType.NVarchar2, 20).Value = Id.Trim
                cm.Parameters.Add("p_msisdn", OracleDbType.NVarchar2, 20).Value = MsisdnIT.Trim
                cm.Parameters.Add("p_status", OracleDbType.NVarchar2, 20).Value = "0"
                cm.Parameters.Add("p_type", OracleDbType.NVarchar2, 20).Value = Type.Trim
                cm.ExecuteNonQuery()
                result = True
            End With
        Catch ex As Exception
            Dim errr As String = ex.Message
        Finally
            conn.Close()
            conn.Dispose()
            cm.Dispose()
        End Try
        Return result
    End Function

    Protected Sub UploadFile(sender As Object, e As EventArgs) Handles btnUpload.Click
        _Alert()
        If FileUpload1.HasFile Then
            _Alert()
            Dim folderPath As String = Server.MapPath("~/Files/")
            'Check whether Directory (Folder) exists.
            If Not Directory.Exists(folderPath) Then
                'If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(folderPath)
            End If
            'Full path
            Dim fi As New IO.FileInfo(FileUpload1.FileName)
            Dim extn As String = fi.Extension
            If extn = ".jpg" Or extn = ".png" Or extn = ".jpeg" Or extn = ".JPG" Or extn = ".PNG" Or extn = ".JPEG" Then
                _Alert()
                Dim filename1 As String = Path.GetFileNameWithoutExtension(FileUpload1.FileName)
                Dim _datetime As String = DateTime.Now.ToString("yyyyMMddHHmmss")
                Dim _filename As String = filename1 + "-" + _datetime + extn
                fullpath = "http://172.28.12.35:2425/Files/" & Path.GetFileName(_filename)
                Dim _fullpath As String = fullpath.Replace(" ", "%20")
                Session("fullpath") = _fullpath
                Session("filename") = _filename
                'Save the File to the Directory (Folder).
                FileUpload1.SaveAs(folderPath & Path.GetFileName(_filename))
                'Display the Picture in Image control.
                Image1.ImageUrl = "~/Files/" & Path.GetFileName(_filename)
            Else
                _Alert()
                alertImage.Visible = True
                Image1.ImageUrl = ""
            End If

        End If
    End Sub

    Protected Sub ImageFull_ServerClick(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "Script", "modalFullImage();", True)
        ImageFull.ImageUrl = Image1.ImageUrl
    End Sub

    Private Sub btnSearchMember_ServerClick(sender As Object, e As EventArgs) Handles btnSearchMember.ServerClick
        _getMember(1)
    End Sub
End Class

