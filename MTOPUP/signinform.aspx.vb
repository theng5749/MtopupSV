Imports System.Data
Imports nickDevClass
Imports Oracle.ManagedDataAccess.Client
Partial Class signinform
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

                cm = New OracleCommand(myDataType.sqlQueryUserPlus.Trim, cn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("p_user_code", OracleDbType.NVarchar2, 10).Value = mUser.Trim
                Dim USER_DIVISION As New OracleParameter("p_division_name", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
                Dim USER_NAME_ENG As New OracleParameter("p_user_name", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
                Dim USER_POSITION As New OracleParameter("p_user_position", OracleDbType.NVarchar2, 20) With {.Direction = ParameterDirection.Output}
                Dim USER_MSISDN As New OracleParameter("p_msisdn", OracleDbType.NVarchar2, 10) With {.Direction = ParameterDirection.Output}
                Dim USER_TYPE As New OracleParameter("p_user_type", OracleDbType.NVarchar2, 20) With {.Direction = ParameterDirection.Output}
                Dim USER_PASS As New OracleParameter("p_user_pass", OracleDbType.NVarchar2, 500) With {.Direction = ParameterDirection.Output}
                cm.Parameters.Add(USER_DIVISION)
                cm.Parameters.Add(USER_NAME_ENG)
                cm.Parameters.Add(USER_POSITION)
                cm.Parameters.Add(USER_MSISDN)
                cm.Parameters.Add(USER_TYPE)
                cm.Parameters.Add(USER_PASS)
                cm.ExecuteNonQuery()
                If Not USER_PASS.IsNullable Then
                    If fc.pp(mPass.Trim) = (USER_PASS.Value).ToString.Trim Then
                        UserDetail.STATUS = True
                        UserDetail.DIVISION = IIf(USER_DIVISION.IsNullable, "", (USER_DIVISION.Value).ToString.Trim)
                        UserDetail.NAME = IIf(USER_NAME_ENG.IsNullable, 0, USER_NAME_ENG.Value).ToString
                        UserDetail.POSITION = IIf(USER_POSITION.IsNullable, "", (USER_POSITION.Value).ToString.Trim)
                        UserDetail.MSISDN = IIf(USER_MSISDN.IsNullable, 0, USER_MSISDN.Value).ToString
                        UserDetail.TYPE = IIf(USER_TYPE.IsNullable, 0, USER_TYPE.Value).ToString
                    End If
                End If
            Catch ex As Exception
                Dim _err As String = "Err:" & ex.Message
            Finally
                cn.Close()
                cn.Dispose()
            End Try
            If UserDetail.STATUS = True Then
                ' fc.saveLog(UserDetail.USR.ToUpper.Trim, "Login")
                Session.Add("usercode", mUser.Trim)
                Session.Add("username", UserDetail.NAME.Trim)
                Session.Add("userdivision", UserDetail.DIVISION)
                Session.Add("userposition", UserDetail.POSITION)
                Session.Add("usermsisdn", UserDetail.MSISDN)
                Session.Add("usertype", UserDetail.TYPE)
                Session.Timeout = 30000
                Response.Redirect("homeform.aspx")
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
        Dim DIVISION As String
        Dim NAME As String
        Dim POSITION As String
        Dim MSISDN As String
        Dim TYPE As String
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
