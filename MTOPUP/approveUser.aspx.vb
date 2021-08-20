Imports System.Data
Imports nickDevClass
Imports Oracle.ManagedDataAccess.Client
Partial Class approveUser
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim cn As New OracleConnection
    Dim fc As New SubFunction
    Dim cm As New OracleCommand

    Public Structure RequestAddApprover
        Public NameApprove As String
        Public TelephoneApprove As String
        Public DepartmentApprove As String
    End Structure
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
                    menuRechargeHistory.Visible = True
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
            FillData("")
            _clearText()
        End If
        _gridColumnName()

    End Sub
    Sub _clearText()
        txtNameApprove.Value = ""
        txtteleApprove.Value = ""
        ddDepartment.SelectedIndex = 0
        alertsuccessadd.Visible = False
        alertfailadd.Visible = False
    End Sub

    Sub _menu()
        menuMember.Visible = False
        menuRecharge.Visible = False
        menuReceive.Visible = False
        menuReprint.Visible = False
        menuRechargeHistory.Visible = False
        menuReceiveHistory.Visible = False
        menuHistoryL1.Visible = False
        menuHistory.Visible = False
        menuApprove.Visible = False
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
    Sub _gridColumnName()
        If gridUserApprove.GridControlElement.Rows.Count <> 0 Then
            gridUserApprove.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridUserApprove.GridControlElement.HeaderRow.Cells(4).Text = "ຊື່"
            gridUserApprove.GridControlElement.HeaderRow.Cells(5).Text = "ເບີໂທ"
            gridUserApprove.GridControlElement.HeaderRow.Cells(6).Text = "ພາກສ່ວນ"
            gridUserApprove.GridControlElement.HeaderRow.Cells(7).Text = "ສະຖານະ"
        End If

    End Sub

    Sub FillData(ByVal searchString As String)
        Dim sql As String = ""
        If searchString.Trim <> "" Then
            sql = "Select * FROM tbl_user_approve"
        Else
            sql = "select * FROM tbl_user_approve"
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
            gridUserApprove.RequestSource = ds.Tables("member")
            gridUserApprove.DataBind()
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


    Private Sub gridUserApprove_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridUserApprove.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            If e.Row.Cells(7).Text = "1" Then
                e.Row.Cells(7).Text = "Active"
            Else
                e.Row.Cells(7).Text = "Inactive"
            End If
        End If
    End Sub
    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        If txtNameApprove.Value = "" Then
            txtNameApprove.Focus()
        ElseIf txtteleApprove.Value = "" Then
            txtteleApprove.Focus()
        Else
            Dim _model As RequestAddApprover
            _model.NameApprove = txtNameApprove.Value.Trim
            _model.TelephoneApprove = txtteleApprove.Value.Trim
            _model.DepartmentApprove = ddDepartment.SelectedItem.Text.Trim
            Dim result As Boolean = InsertUserApprove(_model)
            If result = True Then
                alertsuccessadd.Visible = True
            Else
                alertfailadd.Visible = True
            End If
            _clearText()
            FillData("")
        End If
    End Sub

    Private Function InsertUserApprove(model As RequestAddApprover) As Boolean
        Dim result As Boolean = False
        Dim conn As New OracleConnection
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertUserApprove, conn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_NAME", OracleDbType.NVarchar2, 50).Value = model.NameApprove.Trim
                cm.Parameters.Add("P_MSISDN", OracleDbType.NVarchar2, 20).Value = model.TelephoneApprove.Trim
                cm.Parameters.Add("P_DEPARTMENT", OracleDbType.NVarchar2, 20).Value = model.DepartmentApprove.Trim
                cm.Parameters.Add("P_STATUS", OracleDbType.NVarchar2, 20).Value = "1"
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

    Private Sub btCancel_ServerClick(sender As Object, e As EventArgs) Handles btCancel.ServerClick
        _clearText()
        FillData("")
    End Sub

    Private Sub btnDelete_ServerClick(sender As Object, e As EventArgs) Handles btnDelete.ServerClick
        'Dim i As Integer = 0
        'For Each _row As GridViewRow In gridUserApprove.GridControlElement.Rows
        '    Dim chk As CheckBox = _row.FindControl("CheckBox1")
        '    If chk.Checked = True Then
        '        Try
        '            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
        '            cn.ConnectionString = conDB.getConnectionString
        '            cn.Open()
        '            cm = New OracleCommand(myDataType.sqlDeleteUserApprove, cn) With {.CommandType = CommandType.StoredProcedure}
        '            cm.Parameters.Add("P_MSISDN", OracleDbType.NVarchar2, 20).Value = _row.Cells(5).Text.Trim
        '            cm.Parameters.Add("P_DEPARTMENT", OracleDbType.NVarchar2, 20).Value = _row.Cells(6).Text.Trim
        '            cm.ExecuteNonQuery()
        '            i += 1
        '        Catch ex As Exception

        '        Finally
        '            cn.Close()
        '            cn.Dispose()
        '            cm.Dispose()
        '        End Try
        '    End If
        '    _clearText()
        '    FillData("")
        'Next

        For Each _row As GridViewRow In gridUserApprove.GridControlElement.Rows
            Dim chkTemp As CheckBox = DirectCast(_row.FindControl("CheckBox1"), CheckBox)
            If chkTemp IsNot Nothing Then
                If chkTemp.Checked Then
                    Try

                        cn.ConnectionString = conDB.getConnectionString
                        cn.Open()
                        cm = New OracleCommand(myDataType.sqlDeleteUserApprove, cn) With {.CommandType = CommandType.StoredProcedure}
                        cm.Parameters.Add("P_MSISDN", OracleDbType.NVarchar2, 20).Value = _row.Cells(5).Text.Trim
                        cm.Parameters.Add("P_DEPARTMENT", OracleDbType.NVarchar2, 20).Value = _row.Cells(6).Text.Trim
                        cm.ExecuteNonQuery()
                    Catch ex As Exception

                    Finally
                        cn.Close()
                        cn.Dispose()
                        cm.Dispose()
                    End Try
                End If
            End If
        Next
        _clearText()
        FillData("")
    End Sub
End Class
