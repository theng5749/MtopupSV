Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports nickDevClass
Partial Class refillform
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim fc As New SubFunction
    Dim cn As New OracleConnection
    Dim cm As New OracleCommand

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("username").ToString <> Nothing Or Session("username").ToString <> "" Then
                lbname.Text = Session("username").ToString.ToUpper
            Else
                Response.Redirect("startupform.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("startupform.aspx")
        End Try
        LoadAlert()
    End Sub
    Private Sub LoadAlert()
        alertrefillsuccess.Visible = False
        alertrefillfail.Visible = False
        alertNewMoney.Visible = False
        alertSelectMember.Visible = False
        alertPasswordEmpty.Visible = False
        alertPassword.Visible = False
    End Sub
    Private Sub LoadMember()
        cn = New OracleConnection
        Dim sql As String = ""
        If txtSearchMember.Text <> "" Then
            sql = "select  m.member_id, m.telephone, m.member_name, m.member_level, m.member_type, " _
                & " mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) as root_number, mtopup.FN_CHECKING_CREDIT(m.telephone)as member_credit,m.user_id, m.sts,m.brch_id,b.branch_name" _
                & " from mtopup.tbl_member m,mtopup.tbl_branch b where m.sts=1 and m.brch_id=b.branch_id and" _
                & " m.member_type='MTP' and m.telephone like '%" & txtSearchMember.Text.Trim & "%' order by m.member_id desc"
        Else
            sql = "select  m.member_id, m.telephone, m.member_name, m.member_level, m.member_type, " _
                & " mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) as root_number, mtopup.FN_CHECKING_CREDIT(m.telephone)as member_credit,m.user_id, m.sts,m.brch_id,b.branch_name" _
                & " from mtopup.tbl_member m,mtopup.tbl_branch b where m.sts=1 and m.brch_id=b.branch_id and" _
                & " m.member_type='MTP' and m.MEMBER_ADDRESS='" & Session("userdivision") & "' and rownum <= 25 order by m.member_id desc"
        End If

        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "member")
            cn.Close()
            GridMemberModal.RequestSource = ds.Tables("member")
            GridMemberModal.GridControlElement.PageIndex = 0
            GridMemberModal.DataBind()
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        Finally
            cn.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try

    End Sub

    Private Sub btnTelephone_ServerClick(sender As Object, e As EventArgs) Handles btnTelephone.ServerClick
        LoadMember()
    End Sub

    Private Sub GridMemberModal_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles GridMemberModal.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            'e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            If CDbl(e.Row.Cells(10).Text) > 0 Then
                e.Row.Cells(10).Text = CDbl(e.Row.Cells(10).Text).ToString(String.Format("###,###,###,###,###"))
            End If
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            'e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False

            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(10).Font.Bold = True
            e.Row.Cells(10).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(14).Font.Bold = True
            e.Row.Cells(14).ForeColor = Drawing.Color.CornflowerBlue
            If e.Row.Cells(12).Text.ToString = "1" Then
                e.Row.Cells(12).Text = "Active"
                e.Row.Cells(12).ForeColor = Drawing.Color.Green
                e.Row.Cells(12).Font.Bold = True
            Else
                e.Row.Cells(12).Text = "Deactivation"
                e.Row.Cells(12).ForeColor = Drawing.Color.Red
                e.Row.Cells(12).Font.Bold = True
            End If
        End If
    End Sub
    Private Sub GridMemberModal_GridRowClick(sender As Object, e As EventArgs) Handles GridMemberModal.GridRowClick
        LoadAlert()

        Dim _row = GridMemberModal.GridControlElement.SelectedRow
        txtMemberID.Value = _row.Cells(4).Text.Trim
        txtTelephone.Value = _row.Cells(5).Text.Trim
        txtMemberNameMT.Value = _row.Cells(6).Text.Trim
        txtOldCredit.Value = _row.Cells(10).Text.Trim
        txtNewCredit.Value = ""
        EDatePicker1.DateValue = Today.Date
        EDatePicker2.DateValue = Today.Date
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalMember();", True)
        txtNewCredit.Focus()
        'panelSave.Visible = True
    End Sub
    Private Sub btnSearchMember_ServerClick(sender As Object, e As EventArgs) Handles btnSearchMember.ServerClick
        LoadMember()
    End Sub
    Private Sub txtSearchMember_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMember.TextChanged
        LoadMember()
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        System.Threading.Thread.Sleep(200)
        LoadAlert()
        If txtMemberNameMT.Value = "" Then
            LoadAlert()
            alertSelectMember.Visible = True
            btnTelephone.Focus()
            Exit Sub
        End If

        If txtNewCredit.Value = "" Then
            LoadAlert()
            alertNewMoney.Visible = True
            txtNewCredit.Focus()
            Exit Sub
        End If
        btConfirmSave2.Disabled = False
        lbTel.Text = txtTelephone.Value.Trim
        lbTel2.Text = txtTelephone.Value.Trim
        'lbTelephone2.Text = txtTelephone.Value.Trim
        lbNewMoney2.Text = txtNewCredit.Value.Trim
        lbNewMoney3.Text = txtNewCredit.Value.Trim
        lbNewMoney.Text = txtNewCredit.Value.Trim
        lbOldMoney.Text = txtOldCredit.Value.Trim
        Dim a As Double = CDbl(txtOldCredit.Value.Replace(",", ""))
        Dim b As Double = CDbl(txtNewCredit.Value.Replace(",", ""))
        lbUpdateMoney.Text = (a + b).ToString(String.Format("###,###,###,###,###,###"))
        txtConfirmPassword.Value = ""
        txtConfirmPassword.Focus()
        Session.Add("canCharge", "Yes")
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
        txtConfirmPassword.Focus()

    End Sub

    Private Sub btConfirmSave2_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave2.ServerClick
        If txtConfirmPassword.Value = "" Then
            alertPasswordEmpty.Visible = True
            Exit Sub
        Else
            _InsertRechargeLog()


        End If
    End Sub
    Private Function _InsertRechargeLog() As Long
        Dim logID As String = ""
        Dim conn As New OracleConnection
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim amt As Double = CDbl(txtNewCredit.Value.Trim.Replace(",", ""))
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertRechargeLog, conn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_MEMBER_ID", OracleDbType.Int64).Value = CDbl(txtMemberID.Value.Trim)
                cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = txtTelephone.Value.Trim
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
End Class
