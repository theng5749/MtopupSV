Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports nickDevClass
Partial Class user
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim cn As New OracleConnection
    Dim fc As New SubFunction
    Dim cm As New OracleCommand
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("username").ToString <> Nothing Or Session("username").ToString <> "" Then
                lbUserName.Text = Session("username").ToString
                Dim lv As String = Session("userlevel").ToString
                If Session("userlevel").ToString = "9" Then
                    _menu()
                    menuMember.Visible = True
                    menuRecharge.Visible = True
                    menuReceive.Visible = True
                    menuReprint.Visible = True
                    menuReceiveHistory.Visible = True
                    menuHistoryL1.Visible = True
                    menuHistory.Visible = True
                    menuApprove.Visible = True
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
            _load()
            FillData("")
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
        menuUser.Visible = False
        menuBranch.Visible = False
        menuDiscount.Visible = False
        menuDiscountSale.Visible = False
        menuCheckApprove.Visible = False
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
        If gridUser.GridControlElement.Rows.Count <> 0 Then
            gridUser.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridUser.GridControlElement.HeaderRow.Cells(4).Text = "ຊື່ແທ້"
            gridUser.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່ເຂົ້າລະບົບ"
            gridUser.GridControlElement.HeaderRow.Cells(7).Text = "ຕຳແໜ່ງໃນລະບົບ"
            gridUser.GridControlElement.HeaderRow.Cells(8).Text = "ໝາຍເຫດ"
            gridUser.GridControlElement.HeaderRow.Cells(9).Text = "ສະຖານະ"
            'gridUser.GridControlElement.HeaderRow.Cells(11).Text = "ລະຫັດກຸ່ມ"
            gridUser.GridControlElement.HeaderRow.Cells(11).Text = "ກຸ່ມ"
            gridUser.GridControlElement.HeaderRow.Cells(12).Text = "ເບີໂທ"
        End If
    End Sub
    Sub _load()
        _Alert()
        _Clear()
        tabBranch.Visible = False
        txtBranchID.Value = "15"
        statusEdit.Disabled = True
    End Sub
    Sub _Clear()
        txtUserNameMT.Disabled = False
        txtFirstName.Value = ""
        txtUserNameMT.Value = ""
        txttelephone.Value = ""
        'txtPasswordMT.Disabled = False
        'tabReset.Visible = False
        txtUserDes.Text = ""
        cbLevel.SelectedIndex = 0
        cbActive.Checked = True
        txtBranchID.Value = ""
        txtBranchName.Value = ""
    End Sub
    Sub _Alert()
        alertFirstName.Visible = False
        alertPassword.Visible = False
        alertSaveError.Visible = False
        alertSaveSucess.Visible = False
        alertUsername.Visible = False
        alertDelete.Visible = False
        alertBranch.Visible = False
    End Sub
    Sub FillData(ByVal searchString As String)
        Dim sql As String = ""
        If searchString.Trim <> "" Then
            sql = String.Format("select  tbl_userctrl.user_name, tbl_userctrl.user_id, tbl_userctrl.user_pass, tbl_userctrl.user_level, tbl_userctrl.user_desc, tbl_userctrl.user_sts,tbl_userctrl.brch_id,tbl_branch.branch_name,tbl_userctrl.telephone" _
                                & " from mtopup.tbl_userctrl,mtopup.tbl_branch where (tbl_userctrl.user_id like '%{0}%' or tbl_userctrl.user_name like '%{0}%' or tbl_branch.branch_name like '%{0}%') and tbl_userctrl.user_level!=9 and tbl_userctrl.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_userctrl.user_date desc", searchString.Trim)
        Else
            sql = "select  tbl_userctrl.user_name, tbl_userctrl.user_id, tbl_userctrl.user_pass, tbl_userctrl.user_level, tbl_userctrl.user_desc, tbl_userctrl.user_sts,tbl_userctrl.brch_id,tbl_branch.branch_name,tbl_userctrl.telephone" _
                & " from mtopup.tbl_userctrl,mtopup.tbl_branch where tbl_userctrl.user_level!=9 and tbl_userctrl.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_userctrl.user_date desc"
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
            gridUser.RequestSource = ds.Tables("member")
            gridUser.DataBind()
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
    Protected Sub txtSearchText_TextChanged(sender As Object, e As EventArgs) Handles txtSearchText.TextChanged
        FillData(txtSearchText.Text.Trim.ToString)
    End Sub
    Protected Sub gridUser_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridUser.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(10).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(10).Visible = False
            If e.Row.Cells(7).Text = "0" Then
                e.Row.Cells(7).Text = "Admin"
            ElseIf e.Row.Cells(7).Text = "7" Then
                e.Row.Cells(7).Text = "Call Center"
            ElseIf e.Row.Cells(7).Text = "6" Then
                e.Row.Cells(7).Text = "NOC"
            ElseIf e.Row.Cells(7).Text = "8" Then
                e.Row.Cells(7).Text = "ພະແນກການເງິນ"
            ElseIf e.Row.Cells(7).Text = "9" Then
                e.Row.Cells(7).Text = "Super Admin"
            ElseIf e.Row.Cells(7).Text = "10" Then
                e.Row.Cells(7).Text = "System"
            ElseIf e.Row.Cells(7).Text = "11" Then
                e.Row.Cells(7).Text = "CS Sale"
            ElseIf e.Row.Cells(7).Text = "12" Then
                e.Row.Cells(7).Text = "Super"
            ElseIf e.Row.Cells(7).Text = "13" Then
                e.Row.Cells(7).Text = "Finance"
            End If

            If e.Row.Cells(9).Text.ToString = "1" Then
                e.Row.Cells(9).Text = "Active"
                e.Row.Cells(9).ForeColor = Drawing.Color.Green
                e.Row.Cells(9).Font.Bold = True
            Else
                e.Row.Cells(9).Text = "Deactivated"
                e.Row.Cells(9).ForeColor = Drawing.Color.Red
                e.Row.Cells(9).Font.Bold = True
            End If

            If e.Row.Cells(10).Text.ToString.Trim = "15" Then
                e.Row.Cells(11).ForeColor = Drawing.Color.Orange
            Else
                e.Row.Cells(11).ForeColor = Drawing.Color.CornflowerBlue
                e.Row.Cells(11).Font.Bold = True
            End If
        End If
    End Sub
    Protected Sub gridUser_GridRowClick(sender As Object, e As EventArgs) Handles gridUser.GridRowClick
        statusEdit.Disabled = False
        _Alert()
        Dim row = gridUser.GridControlElement.SelectedRow
        txtFirstName.Value = row.Cells(4).Text.ToString
        txtUserNameMT.Value = row.Cells(5).Text.ToString
        txtBranchID.Value = row.Cells(10).Text.ToString
        txtBranchName.Value = row.Cells(11).Text.ToString
        txttelephone.Value = row.Cells(12).Text.ToString
        'txtPasswordMT.Value = "Data encrypted"
        'txtPasswordMT.Disabled = True
        txtUserNameMT.Disabled = True
        'tabReset.Visible = True
        If row.Cells(8).Text.ToString = "&nbsp;" Or row.Cells(8).Text.ToString = "" Then
            txtUserDes.Text = ""
        Else
            txtUserDes.Text = row.Cells(8).Text.ToString
        End If

        If row.Cells(7).Text = "Admin" Then
            cbLevel.SelectedValue = "0"
        ElseIf row.Cells(7).Text = "Call Center" Then
            cbLevel.SelectedValue = "7"
        ElseIf row.Cells(7).Text = "Super Admin" Then
            cbLevel.SelectedValue = "9"
        ElseIf row.Cells(7).Text = "ພະແນກການເງິນ" Then
            cbLevel.SelectedValue = "8"
        ElseIf row.Cells(7).Text = "NOC" Then
            cbLevel.SelectedValue = "6"
        ElseIf row.Cells(7).Text = "CS Sale" Then
            cbLevel.SelectedValue = "11"
        ElseIf row.Cells(7).Text = "Super" Then
            cbLevel.SelectedValue = "12"
        ElseIf row.Cells(7).Text = "Finance" Then
            cbLevel.SelectedValue = "13"
        ElseIf row.Cells(7).Text = "System" Then
            txtBranchID.Value = "15"
        End If
        If row.Cells(9).Text = "Active" Then
            cbActive.Checked = True
        Else
            cbActive.Checked = False
        End If
        If cbLevel.SelectedValue = "0" Or cbLevel.SelectedValue = "11" Or cbLevel.SelectedValue = "12" Or cbLevel.SelectedValue = "13" Then
            tabBranch.Visible = True
        Else
            tabBranch.Visible = False
        End If
    End Sub
    Protected Sub gridUser_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridUser.GridViewPageIndexChanged
        Dim sql As String = ""
        If txtSearchText.Text.Trim <> "" Then
            sql = String.Format("select  tbl_userctrl.user_name, tbl_userctrl.user_id, tbl_userctrl.user_pass, tbl_userctrl.user_level, tbl_userctrl.user_desc, tbl_userctrl.user_sts,tbl_userctrl.brch_id,tbl_branch.branch_name" _
                                & " from mtopup.tbl_userctrl,mtopup.tbl_branch where (tbl_userctrl.user_id like '%{0}%' or tbl_userctrl.user_name like '%{0}%') and tbl_userctrl.user_level!=9 and tbl_userctrl.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_userctrl.user_name asc", txtSearchText.Text.Trim)
        Else
            sql = "select  tbl_userctrl.user_name, tbl_userctrl.user_id, tbl_userctrl.user_pass, tbl_userctrl.user_level, tbl_userctrl.user_desc, tbl_userctrl.user_sts,tbl_userctrl.brch_id,tbl_branch.branch_name" _
                & " from mtopup.tbl_userctrl,mtopup.tbl_branch where tbl_userctrl.user_level!=9 and tbl_userctrl.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_userctrl.user_name asc"
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
            gridUser.RequestSource = ds.Tables("member")
            gridUser.GridControlElement.PageIndex = e.NewPageIndex
            gridUser.DataBind()
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
    Protected Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        lbFirstName.Text = txtFirstName.Value
        lbAlertName.Text = txtFirstName.Value
        lbAlertName2.Text = txtFirstName.Value
        lbAlertUser.Text = txtUserNameMT.Value
        If txtFirstName.Value.Trim = "" Then
            _Alert()
            alertFirstName.Visible = True
            txtFirstName.Focus()
            Exit Sub
        End If
        If txtUserNameMT.Value.Trim = "" Then
            _Alert()
            alertUsername.Visible = True
            txtUserNameMT.Focus()
            Exit Sub
        End If
        'If txtPasswordMT.Value.Trim = "" Then
        '    _Alert()
        '    alertPassword.Visible = True
        '    txtPasswordMT.Focus()
        '    Exit Sub
        'End If
        If txtBranchID.Value.Trim = "" Then
            _Alert()
            alertBranch.Visible = True
            Exit Sub
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
    End Sub
    Protected Sub btCancel_ServerClick(sender As Object, e As EventArgs) Handles btCancel.ServerClick
        _load()
    End Sub
    Protected Sub btConfirmSave_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave.ServerClick
        If AddUser() = True Then
            _Alert()
            _Clear()
            alertSaveSucess.Visible = True
            statusEdit.Disabled = True
            FillData("")
        Else
            _Alert()
            alertSaveError.Visible = True
            statusEdit.Disabled = False
        End If
    End Sub
    Private Function AddUser() As Boolean
        Dim _return As Boolean = False
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            Dim sts As Integer = 0
            If cbActive.Checked = True Then
                sts = 1
            Else
                sts = 0
            End If
            If statusEdit.Disabled = True Then
                'Add news member
                cm = New OracleCommand(myDataType.sqlInsertUser, cn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 30).Value = txtUserNameMT.Value.ToUpper.Trim
                cm.Parameters.Add("P_USER_PASS", OracleDbType.NVarchar2, 30).Value = fc.pp("#Ltc@123")
                cm.Parameters.Add("P_USER_NAME", OracleDbType.NVarchar2, 50).Value = txtFirstName.Value.Trim
                cm.Parameters.Add("P_USER_LEVEL", OracleDbType.Int64).Value = CInt(cbLevel.SelectedValue)
                cm.Parameters.Add("P_USER_STS", OracleDbType.Int16).Value = sts
                cm.Parameters.Add("P_USER_DESC", OracleDbType.NVarchar2, 200).Value = txtUserDes.Text.Trim
                cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = CDbl(txtBranchID.Value.Trim)
                cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 15).Value = txttelephone.Value.Trim
                cm.ExecuteNonQuery()
                '///// SaveLog ///////////
                fc.saveLog(Session("userid").ToString, "Add new user - [userid:" & txtUserNameMT.Value.ToUpper.Trim & "] [name:" & txtFirstName.Value.Trim & "] [level:" & CInt(cbLevel.SelectedValue) & "] [stats:" & sts & "]")
            Else
                'Update member
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlUpdateUser, cn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 30).Value = txtUserNameMT.Value.ToUpper.Trim
                Dim _level As Double = CInt(cbLevel.SelectedValue)
                Dim _branch As Double = CDbl(txtBranchID.Value.Trim)
                cm.Parameters.Add("P_USER_PASS", OracleDbType.NVarchar2, 30).Value = gridUser.GridControlElement.SelectedRow.Cells(6).Text.Trim
                cm.Parameters.Add("P_USER_NAME", OracleDbType.NVarchar2, 50).Value = txtFirstName.Value.Trim
                cm.Parameters.Add("P_USER_LEVEL", OracleDbType.Int64).Value = _level
                cm.Parameters.Add("P_USER_STS", OracleDbType.Int64).Value = sts
                cm.Parameters.Add("P_USER_DESC", OracleDbType.NVarchar2, 200).Value = txtUserDes.Text.Trim
                cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = _branch
                cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 15).Value = txttelephone.Value.Trim
                cm.ExecuteNonQuery()
                '///// SaveLog ///////////
                fc.saveLog(Session("userid").ToString, "Edit user - [userid:" & txtUserNameMT.Value.ToUpper.Trim & "] [name:" & txtFirstName.Value.Trim & "] [level:" & CInt(cbLevel.SelectedValue) & "] [stats:" & sts & "]")
            End If
            _return = True
        Catch ex As Exception
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        Return _return
    End Function
    Protected Sub btnAlertDelete_ServerClick(sender As Object, e As EventArgs) Handles btnAlertDelete.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertError_ServerClick(sender As Object, e As EventArgs) Handles btnAlertError.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertFirstName_ServerClick(sender As Object, e As EventArgs) Handles btnAlertFirstName.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertPassword_ServerClick(sender As Object, e As EventArgs) Handles btnAlertPassword.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertSuccess_ServerClick(sender As Object, e As EventArgs) Handles btnAlertSuccess.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertUsername_ServerClick(sender As Object, e As EventArgs) Handles btnAlertUsername.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertBranch_ServerClick(sender As Object, e As EventArgs) Handles btnAlertBranch.ServerClick
        _Alert()
    End Sub
    'Protected Sub btResetPassword_ServerClick(sender As Object, e As EventArgs) Handles btResetPassword.ServerClick
    '    txtPasswordMT.Value = ""
    '    txtPasswordMT.Disabled = False
    '    txtPasswordMT.Focus()
    'End Sub
    Protected Sub btnDelete_ServerClick(sender As Object, e As EventArgs) Handles btnDelete.ServerClick
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalDelete();", True)
    End Sub
    Protected Sub btConfirmDelete_ServerClick(sender As Object, e As EventArgs) Handles btConfirmDelete.ServerClick
        Dim i As Integer = 0
        For Each _row As GridViewRow In gridUser.GridControlElement.Rows
            Dim chk As CheckBox = _row.FindControl("CheckBox1")
            If chk.Checked = True Then
                Try
                    'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
                    cn.ConnectionString = conDB.getConnectionString
                    cn.Open()
                    cm = New OracleCommand(myDataType.sqlUpdateUser, cn) With {.CommandType = CommandType.StoredProcedure}
                    cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 30).Value = _row.Cells(5).Text.ToUpper.Trim
                    cm.Parameters.Add("P_USER_PASS", OracleDbType.NVarchar2, 30).Value = _row.Cells(6).Text.Trim
                    cm.Parameters.Add("P_USER_NAME", OracleDbType.NVarchar2, 50).Value = _row.Cells(4).Text.Trim
                    Dim lv As Integer = 0
                    If _row.Cells(7).Text.Trim = "Admin" Then
                        lv = 0
                    ElseIf _row.Cells(7).Text.Trim = "Call Center" Then
                        lv = 7
                    ElseIf _row.Cells(7).Text.Trim = "Super User" Then
                        lv = 9
                    End If
                    cm.Parameters.Add("P_USER_LEVEL", OracleDbType.Int64).Value = lv
                    cm.Parameters.Add("P_USER_STS", OracleDbType.Int32).Value = 0
                    cm.Parameters.Add("P_USER_DESC", OracleDbType.NVarchar2, 200).Value = _row.Cells(8).Text.ToUpper.Trim
                    cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = CDbl(_row.Cells(10).Text)
                    cm.ExecuteNonQuery()
                    i += 1
                    fc.saveLog(Session("userid").ToString, "Edit user - [userid:" & _row.Cells(5).Text.ToUpper.Trim & "] [name:" & _row.Cells(4).Text.Trim & "] [level:" & lv & "] [stats:0]")
                Catch ex As Exception

                Finally
                    cn.Close()
                    cn.Dispose()
                    cm.Dispose()
                End Try
            End If
            FillData(txtSearchText.Text.Trim)
        Next
        lbCountUser.Text = i.ToString
        _Alert()
        alertDelete.Visible = True
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
        _Alert()
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
        txtBranchID.Value = gridBranch.GridControlElement.SelectedRow.Cells(4).Text
        txtBranchName.Value = gridBranch.GridControlElement.SelectedRow.Cells(5).Text
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalBranch();", True)
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
    Protected Sub cbLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLevel.SelectedIndexChanged
        If cbLevel.SelectedValue = "0" Or cbLevel.SelectedValue = "11" Or cbLevel.SelectedValue = "12" Or cbLevel.SelectedValue = "13" Then
            txtBranchID.Value = ""
            txtBranchName.Value = ""
            tabBranch.Visible = True
        Else
            txtBranchID.Value = "15"
            tabBranch.Visible = False
        End If
    End Sub
End Class

' System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
