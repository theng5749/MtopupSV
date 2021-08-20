Imports Oracle.ManagedDataAccess.Client
Imports System.Data
Imports nickDevClass
Imports ClosedXML
Imports System.IO
Imports ClosedXML.Excel

Partial Class UserCtrl
    Inherits System.Web.UI.Page
    Dim conn As New OracleConnection
    Dim cmd As New OracleCommand
    Dim ds As New DataSet
    Dim da As New OracleDataAdapter
    Dim db As New connectDB
    Dim fn As New SubFunction
    Dim Type As Integer = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("username").ToString <> Nothing Or Session("username").ToString <> "" Then
                lbUserName.Text = Session("username").ToString
                If Session("userlevel").ToString = "9" Then
                    ' _menu()
                    menuMember.Visible = True
                    menuRecharge.Visible = True
                    menuReceive.Visible = True
                    menuReprint.Visible = True
                    menuReceiveHistory.Visible = True
                    menuHistoryL1.Visible = True
                    menuHistory.Visible = True
                    menuUser.Visible = True
                    menuBranch.Visible = True
                    menuRechargeHistory.Visible = True
                    menuApprove.Visible = True
                    menuDiscount.Visible = True
                    menuDiscountSale.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                    menuCheckApprove.Visible = True
                    cbManage.Visible = True
                    cbReport.Visible = True
                    menuProHis.Visible = True
                    menuPromotion.Visible = True
                ElseIf Session("userlevel").ToString = "0" Then
                    ' _menu()
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
            Alert()
            LoadData()
        End If
    End Sub
    Sub LoadData()
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = db.getConnectionString()
                .Open()
                Dim sql As String = "select user_code,DIVISION_NAME, USER_NAME_ENG,POSITION,MSISDN,USER_TYPE,user_date,ACTIVE from tbl_user_plus order by user_date desc"
                da = New OracleDataAdapter(sql, conn)
                da.Fill(ds, "table")
                GridControl.RequestSource = ds.Tables(0)
                GridControl.DataBind()
            End With
        Catch ex As Exception
        Finally
            conn.Close()
            cmd.Dispose()
        End Try
    End Sub
    Sub Alert()
        alertSaveError.Visible = False
        alertSaveSucess.Visible = False
        alertInput.Visible = False
        alertCode.Visible = False
        statusactive.Visible = False
        ' btnEdit.Visible = False
        btnEdit.Disabled = True
    End Sub
    Private Sub btnUserSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        If txtusercode.Value = "" Or txtusernameeng.Value = "" Or txtuserphone.Value = "" Then
            alertInput.Visible = True
        Else
            Dim _checkCode As Boolean = False
            If _checkCode Then
                '//Alert Code
                alertCode.Visible = True
            Else
                '//Save
                Try
                    With conn
                        If .State = ConnectionState.Open Then .Close()
                        .ConnectionString = db.getConnectionString()
                        .Open()
                        Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertUserPlus, conn) With {.CommandType = CommandType.StoredProcedure}
                        cm.Parameters.Add("p_user_code", OracleDbType.NVarchar2, 10).Value = txtusercode.Value.Trim
                        cm.Parameters.Add("p_division_name", OracleDbType.NVarchar2, 35).Value = txtuserdivision.Text.Trim
                        cm.Parameters.Add("p_username_lao", OracleDbType.NVarchar2, 35).Value = txtusernameeng.Value.Trim
                        cm.Parameters.Add("p_username_eng", OracleDbType.NVarchar2, 35).Value = txtusernameeng.Value.Trim
                        cm.Parameters.Add("p_position", OracleDbType.NVarchar2, 20).Value = txtuserposition.Text.Trim
                        cm.Parameters.Add("p_msisdn", OracleDbType.NVarchar2, 10).Value = txtuserphone.Value.Trim
                        cm.Parameters.Add("p_usertype", OracleDbType.NVarchar2, 20).Value = txtuserapprove.Text.Trim
                        cm.Parameters.Add("p_userstatus", OracleDbType.NVarchar2, 20).Value = "Normal"
                        cm.Parameters.Add("p_active", OracleDbType.NVarchar2, 20).Value = "Active"
                        Dim _pass As String = txtusercode.Value.Trim & "@Ltc"
                        cm.Parameters.Add("p_userpass", OracleDbType.NVarchar2, 500).Value = fn.pp(_pass)
                        cm.ExecuteNonQuery()
                        LoadData()
                        alertSaveSucess.Visible = True
                        lbcode.Text = txtusercode.Value.Trim
                    End With
                Catch ex As Exception
                    btnAlertError.Visible = True
                    lbcode2.Text = txtusercode.Value.Trim
                Finally
                    conn.Close()
                    cmd.Dispose()
                End Try
            End If
        End If
    End Sub
    Private Sub GridControl_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles GridControl.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(1).Text = "RDO"
            e.Row.Cells(2).Text = "Check"
            e.Row.Cells(3).Text = "ລ/ດ"
            e.Row.Cells(4).Text = "ລະຫັດ"
            e.Row.Cells(5).Text = "ແຂວງ"
            e.Row.Cells(6).Text = "ພະນັກງານ"
            e.Row.Cells(7).Text = "ຕຳແໜ່ງ"
            e.Row.Cells(8).Text = "ເບີໂທ"
            e.Row.Cells(9).Text = "ປະເພດ"
            e.Row.Cells(10).Text = "ວັນທີສ້າງ"
            e.Row.Cells(11).Text = "ສະຖານະ"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(1).Text = "RDO"
            e.Row.Cells(2).Text = "Check"
        End If
    End Sub
    Private Sub GridControl_GridRowClick(sender As Object, e As EventArgs) Handles GridControl.GridRowClick
        Try
            txtusercode.Value = GridControl.GridControlElement.SelectedRow.Cells(4).Text.ToString.Trim
            txtuserdivision.SelectedValue = GridControl.GridControlElement.SelectedRow.Cells(5).Text.ToString.Trim
            txtusernameeng.Value = GridControl.GridControlElement.SelectedRow.Cells(6).Text.ToString.Trim
            txtuserposition.SelectedValue = GridControl.GridControlElement.SelectedRow.Cells(7).Text.ToString.Trim
            txtuserphone.Value = GridControl.GridControlElement.SelectedRow.Cells(8).Text.ToString.Trim
            txtuserapprove.SelectedValue = GridControl.GridControlElement.SelectedRow.Cells(9).Text.ToString.Trim
            txtstatus.SelectedValue = GridControl.GridControlElement.SelectedRow.Cells(11).Text.ToString.Trim
        Catch ex As Exception

        End Try
        txtuserid.Visible = False
        txtusercode.Visible = False
        statusactive.Visible = True
        btnEdit.Disabled = False
        btnSave.Disabled = True
    End Sub
    Private Sub btnRefresh_ServerClick(sender As Object, e As EventArgs) Handles btnRefresh.ServerClick
        Response.Redirect("UserCtrl.aspx")
    End Sub
    Private Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        If GridControl.GridControlElement.Rows.Count <> 0 Then
            Dim d = Today.Date.ToString("dd-MM-yyyy")
            Dim filename As String = "mtopup_user_sale_" & d & ".xlsx"
            Dim dt As New DataTable("USER-CS")
            For Each cell As TableCell In GridControl.GridControlElement.HeaderRow.Cells
                dt.Columns.Add(cell.Text)
            Next
            For Each row As GridViewRow In GridControl.GridControlElement.Rows
                dt.Rows.Add()
                For i As Integer = 0 To row.Cells.Count - 1
                    If i = 0 Then
                        dt.Rows(dt.Rows.Count - 1)(i) = (row.RowIndex + 1).ToString.Replace("&nbsp;", "")
                    Else
                        dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text.ToString.ToString.Replace("&nbsp;", "")
                    End If
                Next
            Next
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
        End If
    End Sub
    Private Sub btnEdit_ServerClick(sender As Object, e As EventArgs) Handles btnEdit.ServerClick
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = db.getConnectionString()
                .Open()
                Dim sql As String = "update tbl_user_plus set DIVISION_NAME='" & txtuserdivision.Text.Trim & "', USER_NAME_ENG='" & txtusernameeng.Value.Trim & "'," _
                    & " POSITION='" & txtuserposition.Text.Trim & "', MSISDN='" & txtuserphone.Value.Trim & "', USER_TYPE='" & txtuserapprove.Text.Trim & "'," _
                    & " ACTIVE='" & txtstatus.Text & "' where user_code='" & txtusercode.Value.Trim & "'"
                cmd = New OracleCommand(sql, conn)
                cmd.ExecuteNonQuery()
                alertSaveSucess.Visible = True
                lbcode.Text = txtusercode.Value.Trim & " (ອັບເດດ)"
            End With
        Catch ex As Exception
            alertSaveError.Visible = True
            lbcode2.Text = "ບໍ່ສາມາດອັບເດດໄດ້"
        Finally
            conn.Close()
            cmd.Dispose()
        End Try
        LoadData()
        btnSave.Visible = True
        btnEdit.Disabled = True
    End Sub
    Private Sub GridControl_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles GridControl.GridViewPageIndexChanged
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = db.getConnectionString()
                .Open()
                Dim sql As String = "select user_code,DIVISION_NAME, USER_NAME_ENG,POSITION,MSISDN,USER_TYPE,user_date,ACTIVE from tbl_user_plus order by user_date desc"
                da = New OracleDataAdapter(sql, conn)
                da.Fill(ds, "table")
                GridControl.RequestSource = ds.Tables("table")
                GridControl.GridControlElement.PageIndex = e.NewPageIndex
                GridControl.DataBind()
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        SearchData()
    End Sub
    Private Sub SearchData()
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = db.getConnectionString()
                .Open()
                Dim sql As String = "select user_code,DIVISION_NAME, USER_NAME_ENG,POSITION,MSISDN,USER_TYPE,user_date,ACTIVE from tbl_user_plus where user_code like '%" & txtSearchText.Text.Trim & "%' order by user_date desc"
                da = New OracleDataAdapter(sql, conn)
                da.Fill(ds, "table")
                GridControl.RequestSource = ds.Tables(0)
                GridControl.DataBind()
            End With
        Catch ex As Exception
        Finally
            conn.Close()
            cmd.Dispose()
        End Try
    End Sub
    Private Sub txtSearchText_TextChanged(sender As Object, e As EventArgs) Handles txtSearchText.TextChanged
        SearchData()
    End Sub
End Class
