Imports System.Data
Imports nickDevClass
Imports Oracle.ManagedDataAccess.Client
Partial Class branch
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim myDataType As New myDataType
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
                    cbManage.Visible = True
                    cbReport.Visible = True
                    menuProHis.Visible = True
                    menuPromotion.Visible = True
                    menuRechargeHistory.Visible = True
                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Default.aspx")
        End Try
        If Not IsPostBack Then
            _load()
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
        menuCredit.Visible = False
        menuInvoice.Visible = False
        cbManage.Visible = False
        cbReport.Visible = False
        menuProHis.Visible = False
        menuPromotion.Visible = False
    End Sub

    Sub _gridColumnName()
        If gridBranch.GridControlElement.Rows.Count <> 0 Then
            gridBranch.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່ກຸ່ມ"
            gridBranch.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
        End If
    End Sub
    Sub _load()
        _Alert()
        FillDataBranch()
        txtBranchID.Value = ""
        txtBranchName.Value = ""
    End Sub
    Sub _Alert()
        alertMainError.Visible = False
        alertMainInfo.Visible = False
        alertMainSuccess.Visible = False
        alertInfo.Visible = False
    End Sub
    Private Sub FillDataBranch()
        Dim sql As String = ""
        If txtSearchBranch.Text.Trim = "" Then
            sql = "select  branch_id,branch_name" _
                           & " from mtopup.tbl_branch where sts=1 and branch_id!=15 order by branch_name asc"
        Else
            sql = "select  branch_id,branch_name" _
                           & " from mtopup.tbl_branch where sts=1 and branch_id!=15 and branch_name like '%" & txtSearchBranch.Text.Trim & "%' order by branch_name asc"
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

    Protected Sub btnRefresh_ServerClick(sender As Object, e As EventArgs) Handles btnRefresh.ServerClick
        _load()
    End Sub

    Protected Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        _Alert()
        If txtBranchName.Value.Trim = "" Then
            _Alert()
            lbAlertMainInfo.Text = "ກະລຸນາປ້ອນຊື່ກຸ່ມກ່ອນການບັນທຶກ"
            alertMainInfo.Visible = True
            Exit Sub
        End If
        lbBranchName.Text = txtBranchName.Value.Trim
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
    End Sub

    Protected Sub btConfirmSave_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave.ServerClick
        If _AddBranch() > 0 Then
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "CloseMyModalSave();", True)
            lbAlertMainSuccess.Text = "ບັນທຶກຂໍ້ມູນກຸ່ມ """ & txtBranchName.Value.Trim & """ ສຳເລັດ."
            _Alert()
            alertMainSuccess.Visible = True
            txtBranchID.Value = ""
            txtBranchName.Value = ""
            FillDataBranch()
        Else
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "CloseMyModalSave();", True)
            _Alert()
            alertMainError.Visible = True
            txtBranchID.Value = ""
            txtBranchName.Value = ""
        End If
    End Sub
    Private Function _AddBranch() As Double
        Dim rp As Double = 0
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
        Catch ex As Exception
            lbAlertMainError.Text = "ບໍ່ສາມາດເຊື່ອມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ"
            Return rp
            Exit Function
        End Try
        Try
            'Add news branch
            cm = New OracleCommand(myDataType.sqlInsertBranch, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_branch_name", OracleDbType.NVarchar2, 20).Value = txtBranchName.Value.Trim
            cm.Parameters.Add("p_sts", OracleDbType.Int32).Value = 1
            Dim P_ID As New OracleParameter("p_id", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            cm.Parameters.Add(P_ID)
            cm.ExecuteNonQuery()
            '///// SaveLog ///////////
            fc.saveLog(Session("userid").ToString, "Add new branch - [branchID:" & P_ID.Value.ToString.Trim & "] [branchName:" & txtBranchName.Value.Trim & "] [stats:1]")
            Try
                rp = P_ID.Value.ToString
            Catch ex As Exception

            End Try
        Catch ex As Exception
            lbAlertMainError.Text = ex.ToString
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        Return rp
    End Function

    Private Function _EditBranch() As Boolean
        Dim _return As Boolean = False
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
        Catch ex As Exception
            lbAlertMainError.Text = "ບໍ່ສາມາດເຊື່ອມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ"
            Return _return
            Exit Function
        End Try
        Try
            'Update Branch
            Dim cm As OracleCommand = New OracleCommand(myDataType.sqlUpdateBranch, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_branch_id", OracleDbType.Int32).Value = CDbl(txtBranchIDEdit.Value.Trim)
            cm.Parameters.Add("p_branch_name", OracleDbType.NVarchar2, 20).Value = txtBranchNameEdit.Value.Trim
            cm.Parameters.Add("p_sts", OracleDbType.Int16, 1).Value = 1
            cm.ExecuteNonQuery()
            '///// SaveLog ///////////
            fc.saveLog(Session("userid").ToString, "Edit branch - [branchID:" & txtBranchIDEdit.Value.Trim & "] [branchName:" & txtBranchNameEdit.Value.Trim & "] [stats:1]")
            _return = True
        Catch ex As Exception
            lbAlertMainError.Text = ex.ToString
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        Return _return
    End Function

    Protected Sub gridBranch_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridBranch.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            'e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
        End If
    End Sub

    Protected Sub gridBranch_GridRowClick(sender As Object, e As EventArgs) Handles gridBranch.GridRowClick
        _Alert()
        txtBranchID.Value = ""
        txtBranchName.Value = ""
        Dim _row = gridBranch.GridControlElement.SelectedRow
        txtBranchIDEdit.Value = _row.Cells(4).Text.ToString
        txtBranchNameEdit.Value = _row.Cells(5).Text.ToString
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalEdit();", True)
    End Sub

    Protected Sub btConfirmSave2_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave2.ServerClick
        If txtBranchNameEdit.Value.Trim = "" Then
            _Alert()
            lbAlertInfo.Text = "ກະລຸນາປ້ອນຊື່ກຸ່ມກ່ອນການບັນທຶກ."
            alertInfo.Visible = True
            txtBranchNameEdit.Focus()
            Exit Sub
        End If

        If _EditBranch() = True Then
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "CloseMyModalEdit();", True)
            _Alert()
            lbAlertMainSuccess.Text = "ແກ້ໄຂຂໍ້ມູນກຸ່ມ """ & txtBranchNameEdit.Value.Trim & """ ສຳເລັດ."
            alertMainSuccess.Visible = True
            txtBranchNameEdit.Value = ""
            txtBranchIDEdit.Value = ""
            FillDataBranch()
        End If
    End Sub

    Protected Sub txtSearchBranch_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBranch.TextChanged
        FillDataBranch()
    End Sub

    Protected Sub btnDelete_ServerClick(sender As Object, e As EventArgs) Handles btnDelete.ServerClick
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalDelete();", True)
    End Sub

    Protected Sub btConfirmDelete_ServerClick(sender As Object, e As EventArgs) Handles btConfirmDelete.ServerClick
        Dim i As Integer = 0
        For Each _row As GridViewRow In gridBranch.GridControlElement.Rows
            Dim chk As CheckBox = _row.FindControl("CheckBox1")
            If chk.Checked = True Then
                Try
                    cn.ConnectionString = conDB.getConnectionString
                    cn.Open()
                Catch ex As Exception
                    _Alert()
                    lbAlertMainError.Text = "ບໍ່ສາມາດເຊື່ອມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ."
                    btnAlertMainError.Visible = True
                    Exit Sub
                End Try

                Try
                    Dim cm As OracleCommand = New OracleCommand(myDataType.sqlUpdateBranch, cn) With {.CommandType = CommandType.StoredProcedure}
                    cm.Parameters.Add("p_branch_id", OracleDbType.Int32).Value = CDbl(_row.Cells(4).Text.Trim)
                    cm.Parameters.Add("p_branch_name", OracleDbType.NVarchar2, 20).Value = _row.Cells(5).Text.Trim
                    cm.Parameters.Add("p_sts", OracleDbType.Int32, 1).Value = 0
                    cm.ExecuteNonQuery()
                    i += 1
                    fc.saveLog(Session("userid").ToString, "Delete branch - [branchID:" & _row.Cells(4).Text.ToUpper.Trim & "] [branch_name:" & _row.Cells(5).Text.Trim & "] [stats:0]")
                Catch ex As Exception

                Finally
                    cn.Close()
                    cn.Dispose()
                    cm.Dispose()
                End Try
            End If
            FillDataBranch()
        Next
        _Alert()
        lbAlertMainSuccess.Text = "ລຶບຂໍ້ມູນກຸ່ມຈຳນວນ ( " & i & " ກຸ່ມ ) ສຳເລັດ"
        alertMainSuccess.Visible = True
    End Sub
End Class
