Imports System.Data
Imports System.Globalization
Imports nickDevClass
Imports Oracle.ManagedDataAccess.Client
Partial Class checkApprove
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
                    menuRechargeHistory.Visible = True
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
            _RequestHistory()

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
        menuProHis.Visible = False
        menuPromotion.Visible = False

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
        End If

        If gridHistory.GridControlElement.Rows.Count <> 0 Then
            gridHistory.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridHistory.GridControlElement.HeaderRow.Cells(4).Text = "ໝາຍເລກໂທລະສັບ"
            gridHistory.GridControlElement.HeaderRow.Cells(5).Text = "ວັນທີທີ່ເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(6).Text = "ຈຳນວນເງິນທີ່ເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(7).Text = "ຜູ້ທີ່ເຕີມ"
            gridHistory.GridControlElement.HeaderRow.Cells(8).Text = "ຜູ້ອະນຸມັດ IT"
            gridHistory.GridControlElement.HeaderRow.Cells(9).Text = "ສະຖານະ"
            gridHistory.GridControlElement.HeaderRow.Cells(10).Text = "ຜູ້ອະນຸມັດ FN"
            gridHistory.GridControlElement.HeaderRow.Cells(11).Text = "ສະຖານະ"
        End If
    End Sub
    Private Sub btnTelephone_ServerClick(sender As Object, e As EventArgs) Handles btnTelephone.ServerClick
        _getMember(1)
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectMember();", True)
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

        End If
    End Sub

    Protected Sub gridMemberModal_GridRowClick(sender As Object, e As EventArgs) Handles gridMemberModal.GridRowClick
        Dim _row = gridMemberModal.GridControlElement.SelectedRow
        txtMemberID.Value = _row.Cells(4).Text.Trim
        txtTelephone.Value = _row.Cells(5).Text.Trim
        txtMemberNameMT.Value = _row.Cells(6).Text.Trim
        txtOldCredit.Value = _row.Cells(9).Text.Trim
        'txtOldData.Value = _row.Cells(15).Text.Trim
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalMember();", True)
        _RequestHistory()
    End Sub

    Protected Sub gridHistory_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridHistory.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False

            e.Row.Cells(4).Font.Bold = True
            e.Row.Cells(6).ForeColor = Drawing.Color.OrangeRed
            If CDbl(e.Row.Cells(6).Text) > 0 Then
                e.Row.Cells(6).Text = CDbl(e.Row.Cells(6).Text).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
                e.Row.Cells(6).Font.Bold = True
            End If

            If e.Row.Cells(9).Text.ToString = "1" Then
                e.Row.Cells(9).Text = "ອະນຸມັດແລ້ວ"
                e.Row.Cells(9).ForeColor = Drawing.Color.Green
                e.Row.Cells(9).Font.Bold = True
            Else
                e.Row.Cells(9).Text = "ລໍຖ້າການອະນຸມັດ"
                e.Row.Cells(9).ForeColor = Drawing.Color.Red
                e.Row.Cells(9).Font.Bold = True
            End If

            If e.Row.Cells(11).Text.ToString = "1" Then
                e.Row.Cells(11).Text = "ອະນຸມັດແລ້ວ"
                e.Row.Cells(11).ForeColor = Drawing.Color.Green
                e.Row.Cells(11).Font.Bold = True
            Else
                e.Row.Cells(11).Text = "ລໍຖ້າການອະນຸມັດ"
                e.Row.Cells(11).ForeColor = Drawing.Color.Red
                e.Row.Cells(11).Font.Bold = True
            End If
        End If
    End Sub

    Private Sub btnSearchMember_ServerClick(sender As Object, e As EventArgs) Handles btnSearchMember.ServerClick
        _getMember(1)
    End Sub

    Private Sub txtSearchMember_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMember.TextChanged
        _getMember(1)
    End Sub


    Private Sub _RequestHistory()
        Dim sql As String = ""

        If txtTelephone.Value.Trim <> "" Then
            'sql = "select p.msisdn,p.recorddate,p.amount,p.userid,p.msisdnapproveit,p.itstatus,p.msisdnapprovefn,p.financestatus from tbl_process_refill p where p.msisdn='" & txtTelephone.Value.Trim & "' and ROWNUM <= 30 order by p.recorddate desc"
            sql = "select * from (select msisdn,recorddate,amount,userid,msisdnapproveit,itstatus,msisdnapprovefn,financestatus from tbl_process_refill  where msisdn='" & txtTelephone.Value.Trim & "'  order by recorddate desc) where ROWNUM < = 30"
        End If

        cn = New OracleConnection
        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da = New OracleDataAdapter(sql, cn)
            da.Fill(ds, "history")
            cn.Close()
            gridHistory.RequestSource = ds.Tables(0)
            gridHistory.GridControlElement.PageIndex = 0
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
End Class
