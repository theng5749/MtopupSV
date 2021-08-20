Imports System.Data
Imports nickDevClass
Imports Oracle.ManagedDataAccess.Client

Partial Class changepassword
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim conn As New OracleConnection
    Dim fc As New SubFunction
    Dim cmd As New OracleCommand
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
                    menuApprove.Visible = True
                    menuUser.Visible = True
                    menuBranch.Visible = True
                    menuDiscount.Visible = True
                    menuDiscountSale.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                    cbManage.Visible = True
                    cbReport.Visible = True
                    menuPromotion.Visible = True
                    menuProHis.Visible = True
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
                    _menu()
                    'menuReceiveHistory.Visible = True
                    cbReport.Visible = True
                    menuHistoryL1.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                ElseIf Session("userlevel").ToString = "11" Then
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
            _Alert()
            txtUsername.Value = Session("userid").ToString
        End If
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
        menuPromotion.Visible = False
        menuProHis.Visible = False
    End Sub
    Sub _Alert()
        alertSaveSucess.Visible = False
        alertSaveError.Visible = False
        alertOldPassword.Visible = False
        alertNewPassword.Visible = False
        alertConfirmPassword.Visible = False
        alertPasswrodNotMatch.Visible = False
    End Sub
    Protected Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        If txtOldPasswrod.Value.Trim = "" Then
            _Alert()
            alertOldPassword.Visible = True
            txtOldPasswrod.Focus()
            Exit Sub
        End If

        If txtNewPassword.Value.Trim = "" Then
            _Alert()
            alertNewPassword.Visible = True
            txtNewPassword.Focus()
            Exit Sub
        End If

        If txtConfirmPassword.Value.Trim = "" Then
            _Alert()
            alertConfirmPassword.Visible = True
            txtConfirmPassword.Focus()
            Exit Sub
        End If

        If txtNewPassword.Value.Trim <> txtConfirmPassword.Value.Trim Then
            _Alert()
            alertPasswrodNotMatch.Visible = True
            Exit Sub
        End If

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)

    End Sub
    Protected Sub btnAlertConfirmPassword_ServerClick(sender As Object, e As EventArgs) Handles btnAlertConfirmPassword.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertError_ServerClick(sender As Object, e As EventArgs) Handles btnAlertError.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertNewPassword_ServerClick(sender As Object, e As EventArgs) Handles btnAlertNewPassword.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertNotMatch_ServerClick(sender As Object, e As EventArgs) Handles btnAlertNotMatch.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertOldPassword_ServerClick(sender As Object, e As EventArgs) Handles btnAlertOldPassword.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertSuccess_ServerClick(sender As Object, e As EventArgs) Handles btnAlertSuccess.ServerClick
        _Alert()
    End Sub
    Protected Sub btConfirmSave2_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave2.ServerClick
        _Login()
    End Sub
    Private Function ChangePassword() As Boolean
        Dim _return As Boolean = False
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString
                conn.Open()
                Dim sts As Integer = 0
                cmd = New OracleCommand(myDataType.sqlChangePassword, conn) With {.CommandType = CommandType.StoredProcedure}
                cmd.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 30).Value = txtUsername.Value.ToUpper.Trim
                cmd.Parameters.Add("P_USER_PASS", OracleDbType.NVarchar2, 30).Value = fc.pp(txtNewPassword.Value.Trim)
                cmd.ExecuteNonQuery()
                '///// SaveLog ///////////
                fc.saveLog(Session("userid").ToString, "Change Password")
                _return = True
            End With

        Catch ex As Exception
            _return = False
        Finally
            conn.Close()
            conn.Dispose()
            cmd.Dispose()
        End Try
        Return _return
    End Function
    Public Structure UserResult
        Dim STATUS As Boolean
        Dim USR As String
        Dim NAME As String
        Dim LEVEL As Integer
        Dim DESC As String
    End Structure
    Public UserDetail As New UserResult
    Sub _Login()
        UserDetail.STATUS = False
        UserDetail.USR = txtUsername.Value.ToUpper.Trim
        UserDetail.NAME = ""
        UserDetail.LEVEL = -1
        UserDetail.DESC = ""
        Try
            conn.ConnectionString = conDB.getConnectionString
            conn.Open()
            cmd = New OracleCommand(myDataType.sqlQueryUser.Trim, conn) With {.CommandType = CommandType.StoredProcedure}
            cmd.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 10).Value = UserDetail.USR.Trim
            Dim USER_PASS As New OracleParameter("P_USER_PASS", OracleDbType.NVarchar2, 200) With {.Direction = ParameterDirection.Output}
            Dim USER_NAME As New OracleParameter("P_USER_NAME", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
            Dim USER_LEVEL As New OracleParameter("P_USER_LEVEL", OracleDbType.Int32) With {.Direction = ParameterDirection.Output}
            Dim USER_DESC As New OracleParameter("P_USER_DESC", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
            Dim USER_BRCH As New OracleParameter("P_BRANCH_ID", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            cmd.Parameters.Add(USER_PASS)
            cmd.Parameters.Add(USER_NAME)
            cmd.Parameters.Add(USER_LEVEL)
            cmd.Parameters.Add(USER_DESC)
            cmd.Parameters.Add(USER_BRCH)
            cmd.ExecuteNonQuery()
            If Not USER_PASS.IsNullable Then
                If fc.pp(txtOldPasswrod.Value.Trim) = (USER_PASS.Value).ToString.Trim Then
                    UserDetail.STATUS = True
                    UserDetail.NAME = IIf(USER_NAME.IsNullable, "", (USER_NAME.Value).ToString.Trim)
                    UserDetail.LEVEL = IIf(USER_LEVEL.IsNullable, 0, CInt(USER_LEVEL.Value))
                    UserDetail.DESC = IIf(USER_DESC.IsNullable, "", (USER_DESC.Value).ToString.Trim)
                End If
            End If
        Catch ex As Exception
        Finally
            conn.Close()
            conn.Dispose()
            cmd.Dispose()
        End Try
        If UserDetail.STATUS = True Then
            If ChangePassword() = True Then
                txtNewPassword.Value = ""
                txtConfirmPassword.Value = ""
                _Alert()
                alertSaveSucess.Visible = True
            Else
                _Alert()
                alertSaveError.Visible = True
            End If
        Else
            _Alert()
            alertSaveError.Visible = True
        End If
    End Sub
End Class

' System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
