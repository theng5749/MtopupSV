Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.IO
Imports System.Web.Configuration
Imports nickDevClass
Partial Class Login
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim cn As New OracleConnection
    Dim fc As New SubFunction
    Dim cm As New OracleCommand
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                If Session("userid").ToString <> Nothing Or Session("userid").ToString <> "" Then
                    fc.saveLog(Session("userid").ToString, "Logout")
                End If
            Catch ex As Exception
            End Try
            Session.RemoveAll()
            alertSaveEmpty.Visible = False
            alertSaveError.Visible = False
            panel1.Visible = True
            panel2.Visible = False
            Dim user As String = Request.QueryString("user")
            Dim password As String = Request.QueryString("password")
            Try
                If user <> Nothing And password <> Nothing Then
                    _Login(user.Trim, password.Trim)
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
    Sub _Login(mUser As String, mPass As String)
        If mUser.Trim <> "" And mPass.Trim <> "" Then
            UserDetail.STATUS = False
            UserDetail.USR = mUser.ToUpper.Trim
            UserDetail.NAME = ""
            UserDetail.LEVEL = -1
            UserDetail.DESC = ""
            Try
                Try
                    cn.ConnectionString = conDB.getConnectionString
                    cn.Open()
                Catch ex As Exception
                    txtUsername.Value = ""
                    txtPassword.Value = ""
                    lbAlertError.Text = " ບໍ່ສາມາດເຊື່ອມຕໍ່ຖານຂໍ້ມູນໄດ້, ຕິດຕໍ່ທີມພັດທະນາ"
                    alertSaveEmpty.Visible = False
                    alertSaveError.Visible = True
                    Exit Sub
                End Try

                cm = New OracleCommand(myDataType.sqlQueryUser.Trim, cn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 10).Value = UserDetail.USR.Trim
                Dim USER_PASS As New OracleParameter("P_USER_PASS", OracleDbType.NVarchar2, 200) With {.Direction = ParameterDirection.Output}
                Dim USER_NAME As New OracleParameter("P_USER_NAME", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
                Dim USER_LEVEL As New OracleParameter("P_USER_LEVEL", OracleDbType.NVarchar2, 3) With {.Direction = ParameterDirection.Output}
                Dim USER_DESC As New OracleParameter("P_USER_DESC", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
                Dim USER_BRCH As New OracleParameter("P_BRANCH_ID", OracleDbType.NVarchar2, 3) With {.Direction = ParameterDirection.Output}
                cm.Parameters.Add(USER_PASS)
                cm.Parameters.Add(USER_NAME)
                cm.Parameters.Add(USER_LEVEL)
                cm.Parameters.Add(USER_DESC)
                cm.Parameters.Add(USER_BRCH)
                cm.ExecuteNonQuery()
                If Not USER_PASS.IsNullable Then
                    If fc.pp(mPass.Trim) = (USER_PASS.Value).ToString.Trim Then
                        UserDetail.STATUS = True
                        UserDetail.NAME = IIf(USER_NAME.IsNullable, "", (USER_NAME.Value).ToString.Trim)
                        UserDetail.LEVEL = IIf(USER_LEVEL.IsNullable, 0, USER_LEVEL.Value).ToString
                        UserDetail.DESC = IIf(USER_DESC.IsNullable, "", (USER_DESC.Value).ToString.Trim)
                        UserDetail.BRCH = IIf(USER_BRCH.IsNullable, 0, USER_BRCH.Value).ToString
                    End If
                End If
            Catch ex As Exception
                Dim _err As String = "Err:" & ex.Message
            Finally
                cn.Close()
                cn.Dispose()
            End Try
            If UserDetail.STATUS = True Then
                fc.saveLog(UserDetail.USR.ToUpper.Trim, "Login")
                Session.Add("userid", UserDetail.USR.ToUpper.Trim)
                Session.Add("username", UserDetail.NAME.Trim)
                Session.Add("userlevel", UserDetail.LEVEL)
                Session.Add("userbranch", UserDetail.BRCH)
                Session.Timeout = 30000
                If Session("userlevel").ToString = "9" Or Session("userlevel") = "11" Or Session("userlevel") = "12" Or Session("userlevel") = "13" Then
                    Response.Redirect("member.aspx")
                ElseIf Session("userlevel").ToString = "0" Then
                    Response.Redirect("member.aspx")
                ElseIf Session("userlevel").ToString = "7" Then
                    Response.Redirect("history.aspx")
                ElseIf Session("userlevel").ToString = "6" Then
                    Response.Redirect("history.aspx")
                ElseIf Session("userlevel").ToString = "8" Then
                    Response.Redirect("rechargeHistoryL1.aspx", False)
                End If
            Else
                txtUsername.Value = ""
                txtPassword.Value = ""
                lbAlertError.Text = " ຂໍ້ມູນບໍ່ຖືກຕ້ອງ, ກະລຸນາລອງໃໝ່"
                alertSaveEmpty.Visible = False
                alertSaveError.Visible = True
                txtUsername.Focus()
                Exit Sub
            End If
        Else

            If txtUsername.Value.Trim = "" Then
                lbAlertEmptyMsg.Text = " ກະລຸນາປ້ອນຊື່ເຂົ້າລະບົບ"
                alertSaveEmpty.Visible = True
                alertSaveError.Visible = False
                txtUsername.Focus()
                Exit Sub
            End If
            If txtPassword.Value.Trim = "" Then
                lbAlertEmptyMsg.Text = " ກະລຸນາປ້ອນລະຫັດຜ່ານ"
                alertSaveEmpty.Visible = True
                alertSaveError.Visible = False
                txtPassword.Focus()
                Exit Sub
            End If
        End If
    End Sub
    Public Structure UserResult
        Dim STATUS As Boolean
        Dim USR As String
        Dim NAME As String
        Dim LEVEL As String
        Dim DESC As String
        Dim BRCH As String
    End Structure
    Public UserDetail As New UserResult
    Protected Sub btnLogin_ServerClick(sender As Object, e As EventArgs) Handles btnLogin.ServerClick
        alertSaveEmpty.Visible = False
        alertSaveError.Visible = False
        _Login(txtUsername.Value.Trim, txtPassword.Value.Trim)
    End Sub
    Protected Sub btnForgotPassword_Click(sender As Object, e As EventArgs) Handles btnForgotPassword.Click
        txtUsername.Value = ""
        txtPassword.Value = ""
        alertSaveEmpty.Visible = False
        alertSaveError.Visible = False
        panel1.Visible = False
        panel2.Visible = True
    End Sub
    Protected Sub btnAlertEmpty_ServerClick(sender As Object, e As EventArgs) Handles btnAlertEmpty.ServerClick
        alertSaveEmpty.Visible = False
        alertSaveError.Visible = False
    End Sub
    Protected Sub btnAlertError_ServerClick(sender As Object, e As EventArgs) Handles btnAlertError.ServerClick
        alertSaveEmpty.Visible = False
        alertSaveError.Visible = False
    End Sub
    Protected Sub btnBack_ServerClick(sender As Object, e As EventArgs) Handles btnBack.ServerClick
        panel1.Visible = True
        panel2.Visible = False
    End Sub
End Class
