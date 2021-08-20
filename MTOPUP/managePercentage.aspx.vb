Imports System.Data
Imports System.Globalization
Imports nickDevClass
Imports Oracle.ManagedDataAccess.Client
Partial Class managePercentage
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
        btnDelete.Disabled = True
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
        If gridPercentage.GridControlElement.Rows.Count <> 0 Then
            gridPercentage.GridControlElement.HeaderRow.Cells(4).Text = "No"
            gridPercentage.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່ Package"
            gridPercentage.GridControlElement.HeaderRow.Cells(8).Text = "ເປີເຊັນ"
            gridPercentage.GridControlElement.HeaderRow.Cells(11).Text = "ເງິນເລີ່ມຕົ້ນ"
            gridPercentage.GridControlElement.HeaderRow.Cells(12).Text = "ເງິນສິ້ນສຸດ"
        End If
    End Sub
    Sub _load()
        _Alert()
        FillDataBranch()
    End Sub
    Sub _Emptytxt()
        txtPackageName.Value = ""
        txtPercentage.Value = ""
        txtStartAmount.Value = ""
        txtEndAmount.Value = ""
    End Sub
    Sub _Alert()
        alertMainError.Visible = False
        alertMainInfo.Visible = False
        alertMainSuccess.Visible = False
        alertInfo.Visible = False
        alertUpdateSuccess.Visible = False
        alertUpdateFailed.Visible = False
    End Sub
    Private Sub FillDataBranch()
        Dim sql As String = ""
        sql = "select * from TBL_CONDITION_MTOPUP_PLUS order by 1 asc"
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "table")
            cn.Close()
            gridPercentage.RequestSource = ds.Tables("table")
            gridPercentage.DataBind()
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
    Protected Sub btnRefresh_ServerClick(sender As Object, e As EventArgs) Handles btnRefresh.ServerClick
        _load()
        _Emptytxt()
    End Sub
    Protected Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        _Alert()
        Try
            With cn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim sql As String = "Update TBL_CONDITION_MTOPUP_PLUS set CONDITION_NAME='" & txtPackageName.Value.Trim & "'," _
                    & " PERCENTAGE=" & txtPercentage.Value.Trim & "," _
                    & " CREDIT_START=" & txtStartAmount.Value.Trim.Replace(",", "") & "," _
                    & " CREDIT_STOP=" & txtEndAmount.Value.Trim.Replace(",", "") & "" _
                    & " Where ID=" & txtPackageID.Value.Trim & ""
                cm = New OracleCommand(sql, cn)
                cm.ExecuteNonQuery()
                _load()
                _Emptytxt()
                alertUpdateSuccess.Visible = True
            End With
        Catch ex As Exception
            _Alert()
            alertUpdateFailed.Visible = True
            lbUpdateFailed.Text = ex.Message.ToString
        Finally
            cn.Close()
            cm.Dispose()
        End Try
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
            'cm.Parameters.Add("p_branch_name", OracleDbType.NVarchar2, 20).Value = txtBranchName.Value.Trim
            cm.Parameters.Add("p_sts", OracleDbType.Int32).Value = 1
            Dim P_ID As New OracleParameter("p_id", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            cm.Parameters.Add(P_ID)
            cm.ExecuteNonQuery()
            '///// SaveLog ///////////
            ' fc.saveLog(Session("userid").ToString, "Add new branch - [branchID:" & P_ID.Value.ToString.Trim & "] [branchName:" & txtBranchName.Value.Trim & "] [stats:1]")
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
    Private Sub gridPercentage_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridPercentage.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            If CDbl(e.Row.Cells(11).Text) > 0 Then
                e.Row.Cells(11).Text = CDbl(e.Row.Cells(11).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
            If CDbl(e.Row.Cells(12).Text) > 0 Then
                e.Row.Cells(12).Text = CDbl(e.Row.Cells(12).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            End If
        End If
    End Sub
    Private Sub gridPercentage_GridRowClick(sender As Object, e As EventArgs) Handles gridPercentage.GridRowClick
        txtPackageID.Value = gridPercentage.GridControlElement.SelectedRow.Cells(4).Text
        txtPackageName.Value = gridPercentage.GridControlElement.SelectedRow.Cells(5).Text
        txtPercentage.Value = gridPercentage.GridControlElement.SelectedRow.Cells(8).Text
        txtStartAmount.Value = gridPercentage.GridControlElement.SelectedRow.Cells(11).Text
        txtEndAmount.Value = gridPercentage.GridControlElement.SelectedRow.Cells(12).Text
    End Sub
End Class