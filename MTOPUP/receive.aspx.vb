Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.Web.Configuration
Imports nickDevClass
Partial Class receive
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim cn As New OracleConnection
    Dim cm As New OracleCommand
    Dim fc As New SubFunction
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
                    menuRechargeHistory.Visible = True
                    menuHistory.Visible = True
                    menuApprove.Visible = True
                    menuUserCtrl.Visible = True
                    menuUser.Visible = True
                    menuBranch.Visible = True
                    menuDiscount.Visible = True
                    menuDiscountSale.Visible = True
                    menuCredit.Visible = True
                    menuInvoice.Visible = True
                    cbManage.Visible = True
                    cbReport.Visible = True
                    menuCheckApprove.Visible = True
                    menuProHis.Visible = True
                    menuPromotion.Visible = True
                    menuRechargeHistory.Visible = True
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
            _Alert()
            _getDiscount()
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
        menuCheckApprove.Visible = False
        menuUser.Visible = False
        menuRechargeHistory.Visible = False
        menuUserCtrl.Visible = False
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
            gridMemberModal.GridControlElement.HeaderRow.Cells(8).Text = "ປະເພດສະມາຊິກ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(10).Text = "ຈັດການຂໍ້ມູນໂດຍ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(11).Text = "ສະຖານະ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(13).Text = "ລະຫັດກຸ່ມ"
            gridMemberModal.GridControlElement.HeaderRow.Cells(14).Text = "ກຸ່ມ"
        End If

        If gridCredit.GridControlElement.Rows.Count <> 0 Then
            gridCredit.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridCredit.GridControlElement.HeaderRow.Cells(6).Text = "ເວລາທີ່ໄດ້ຮັບເງິນຈາກ L1"
            gridCredit.GridControlElement.HeaderRow.Cells(7).Text = "ໝາຍເລກໂທລະສັບ"
            gridCredit.GridControlElement.HeaderRow.Cells(8).Text = "ຈຳນວນເງິນທີ່ໄດ້ຮັບຈາກ L1"
            gridCredit.GridControlElement.HeaderRow.Cells(9).Text = "ຈຳນວນເງິນທີ່ຕ້ອງຊຳລະ"
            gridCredit.GridControlElement.HeaderRow.Cells(10).Text = "ຈຳນວນເງິນທີ່ໄດ້ຊຳລະແລ້ວ"
            gridCredit.GridControlElement.HeaderRow.Cells(11).Text = "ໝາຍເລກເບີທີ່ເຕີມໃຫ້"
        End If
    End Sub
    Sub _getDiscount()
        Dim Sql As String = "select * from mtopup.tbl_discount"
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(Sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "discount")
            cn.Close()
            gridDiscount.RequestSource = ds.Tables("discount")
            gridDiscount.DataBind()
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

    Sub _Alert()
        alertNewMoney.Visible = False
        alertSaveError.Visible = False
        alertSaveSucess.Visible = False
        alertSelectMember.Visible = False
        alertGetCreditFail.Visible = False
        alertNoData.Visible = False
        alertOverMoney.Visible = False

        alertPassword.Visible = False
        alertPasswordEmpty.Visible = False
    End Sub

    Protected Sub btnTelephone_ServerClick(sender As Object, e As EventArgs) Handles btnTelephone.ServerClick
        txtSearchMember.Text = ""
        _getMember(2)
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectMember();", True)
    End Sub
    Private Sub _getMember(level As Integer)
        Dim sql As String = ""
        Dim userlevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim
        If userlevel = "0" Then
            If txtSearchMember.Text <> "" Then
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts, tbl_member.member_address,tbl_member.brch_id,tbl_branch.branch_name" _
                                  & " from mtopup.tbl_member, mtopup.tbl_branch where tbl_member.member_level={0} and (tbl_member.telephone like '%{1}%' or tbl_member.member_name like '%{1}%' or tbl_branch.branch_name like '%{1}%') and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and rownum <= 100  order by tbl_member.member_id desc", level, txtSearchMember.Text.Trim)
            Else
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts, tbl_member.member_address,tbl_member.brch_id,tbl_branch.branch_name" _
                                   & " from mtopup.tbl_member, mtopup.tbl_branch where tbl_member.member_level={0} and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and rownum <= 100 order by member_id desc", level)
            End If
        Else
            If txtSearchMember.Text <> "" Then
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts, tbl_member.member_address,tbl_member.brch_id,tbl_branch.branch_name" _
                                  & " from mtopup.tbl_member, mtopup.tbl_branch where tbl_member.member_level={0} and (tbl_member.telephone like '%{1}%' or tbl_member.member_name like '%{1}%' or tbl_branch.branch_name like '%{1}%') and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100  order by tbl_member.member_id desc", level, txtSearchMember.Text.Trim)
            Else
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts, tbl_member.member_address,tbl_member.brch_id,tbl_branch.branch_name" _
                                   & " from mtopup.tbl_member, mtopup.tbl_branch where tbl_member.member_level={0} and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by member_id desc", level)
            End If
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
            gridMemberModal.RequestSource = ds.Tables("member")
            gridMemberModal.GridControlElement.PageIndex = 0
            gridMemberModal.DataBind()
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

    Protected Sub gridMemberModal_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridMemberModal.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False

            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(14).Font.Bold = True
            e.Row.Cells(14).ForeColor = Drawing.Color.CornflowerBlue

            If e.Row.Cells(11).Text.ToString = "1" Then
                e.Row.Cells(11).Text = "Active"
                e.Row.Cells(11).ForeColor = Drawing.Color.Green
                e.Row.Cells(11).Font.Bold = True
            Else
                e.Row.Cells(11).Text = "Deactivation"
                e.Row.Cells(11).ForeColor = Drawing.Color.Red
                e.Row.Cells(11).Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub gridCredit_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridCredit.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            If CDbl(e.Row.Cells(8).Text) > 0 Then
                e.Row.Cells(8).Text = CDbl(e.Row.Cells(8).Text).ToString(String.Format("###,###,###,###,###"))
            End If
            If CDbl(e.Row.Cells(9).Text) > 0 Then
                e.Row.Cells(9).Text = CDbl(e.Row.Cells(9).Text).ToString(String.Format("###,###,###,###,###"))
            End If
            If CDbl(e.Row.Cells(10).Text) > 0 Then
                e.Row.Cells(10).Text = CDbl(e.Row.Cells(10).Text).ToString(String.Format("###,###,###,###,###"))
            End If
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(7).Font.Bold = True
            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(9).Font.Bold = True
            e.Row.Cells(10).Font.Bold = True
            e.Row.Cells(8).ForeColor = Drawing.Color.CadetBlue
            e.Row.Cells(9).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(10).ForeColor = Drawing.Color.LimeGreen
        End If
    End Sub

    Protected Sub gridMemberModal_GridRowClick(sender As Object, e As EventArgs) Handles gridMemberModal.GridRowClick
        _Alert()
        Dim _row = gridMemberModal.GridControlElement.SelectedRow
        txtTelephone.Value = _row.Cells(5).Text.Trim
        txtMemberNameMT.Value = _row.Cells(6).Text.Trim
        Session("memberType") = _row.Cells(8).Text.Trim.ToString
        txtAddress.Text = _row.Cells(12).Text.Trim
        txtNewCredit.Value = ""
        txtOldCredit.Value = 0
        lbTel3.Text = txtTelephone.Value
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalMember();", True)
        gridCredit.GridControlElement.PageIndex = 0
        gridCredit.RequestSource = _getCredit(txtTelephone.Value.Trim)

        If gridCredit.GridControlElement.Rows.Count <> 0 Then
            'btnSave.Disabled = False
            btnCheckDiscount.Disabled = False
            For r As Integer = 1 To gridCredit.GridControlElement.Rows.Count
                txtOldCredit.Value = CDbl(txtOldCredit.Value) + CDbl((gridCredit.GridControlElement.Rows(r - 1).Cells(9).Text).Replace(",", ""))
            Next
            txtOldCredit.Value = CDbl(txtOldCredit.Value).ToString("###,###,###,###,###,###")
        Else
            'btnSave.Disabled = True
            btnCheckDiscount.Disabled = True
            If alertGetCreditFail.Visible = False Then
                _Alert()
                alertNoData.Visible = True
            Else
                txtOldCredit.Value = 0
            End If
        End If
        txtNewCredit.Focus()
        _gridColumnName()
    End Sub

    Private Function _getCredit(telephone As String) As DataTable
        Dim dt As DataTable = Nothing
        cn = New OracleConnection
        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Dim sql As String = String.Format("select rownum, sale_id, sale_date, telephone, sale_amt, balance_amt, paid_amt, root_telephone from mtopup.tbl_sale where telephone = '{0}' and sts = 1 and balance_amt > 0 order by sale_id asc", telephone)
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            If cn.State = ConnectionState.Open Then
                da = New OracleDataAdapter(sql, cn)
                ds = New DataSet
                da.Fill(ds, "sale")
                dt = ds.Tables("sale")
                cn.Close()
            End If
        Catch ex As Exception
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            lbError2.Text = ex.ToString
            _Alert()
            alertGetCreditFail.Visible = True
        Finally
            cn.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
        Return dt
    End Function


    Protected Sub gridMemberModal_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridMemberModal.GridViewPageIndexChanged
        Dim level As Integer = 2
        Dim sql As String = ""
        If txtSearchMember.Text <> "" Then
            sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                              & " from mtopup.tbl_member where member_level={0} and (telephone like '%{1}%' or member_name like '%{1}%') and sts=1 and rownum <= 100  order by member_id desc", level, txtSearchMember.Text.Trim)
        Else
            sql = String.Format("select  member_id, telephone, member_name, member_level, member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(root_id) as root_number, mtopup.FN_CHECKING_CREDIT(telephone) as member_credit, user_id, sts, member_address" _
                               & " from mtopup.tbl_member where member_level={0} and sts=1 and rownum <= 100 order by member_id desc", level)
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
            gridMemberModal.RequestSource = ds.Tables("member")
            gridMemberModal.GridControlElement.PageIndex = e.NewPageIndex
            gridMemberModal.DataBind()
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
    Protected Sub txtSearchMember_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMember.TextChanged
        _getMember(2)
    End Sub


    Protected Sub btCancel_ServerClick(sender As Object, e As EventArgs) Handles btCancel.ServerClick
        _Alert()
        btnCheckDiscount.Disabled = False
        txtTelephone.Value = ""
        txtMemberNameMT.Value = ""
        txtOldCredit.Value = ""
        txtNewCredit.Value = ""
        gridCredit.RequestSource = Nothing
    End Sub

    Protected Sub btnAlertError_ServerClick(sender As Object, e As EventArgs) Handles btnAlertError.ServerClick
        _Alert()
    End Sub

    Protected Sub btnAlertGetCredit_ServerClick(sender As Object, e As EventArgs) Handles btnAlertGetCredit.ServerClick
        _Alert()
    End Sub

    Protected Sub btnAlertNewMoney_ServerClick(sender As Object, e As EventArgs) Handles btnAlertNewMoney.ServerClick
        _Alert()
    End Sub

    Protected Sub btnAlertNoData_ServerClick(sender As Object, e As EventArgs) Handles btnAlertNoData.ServerClick
        _Alert()
    End Sub

    Protected Sub btnAlertOverMoney_ServerClick(sender As Object, e As EventArgs) Handles btnAlertOverMoney.ServerClick
        _Alert()
    End Sub

    Protected Sub btnAlertSelectMember_ServerClick(sender As Object, e As EventArgs) Handles btnAlertSelectMember.ServerClick
        _Alert()
    End Sub

    Protected Sub btnAlertSuccess_ServerClick(sender As Object, e As EventArgs) Handles btnAlertSuccess.ServerClick
        _Alert()
    End Sub


    Protected Sub btnCheckDiscount_ServerClick(sender As Object, e As EventArgs) Handles btnCheckDiscount.ServerClick
        _Alert()
        If txtMemberNameMT.Value = "" Then
            _Alert()
            alertSelectMember.Visible = True
            btnTelephone.Focus()
            Exit Sub
        End If

        If txtNewCredit.Value = "" Then
            _Alert()
            alertNewMoney.Visible = True
            txtNewCredit.Focus()
            Exit Sub
        End If

        If CDbl(txtNewCredit.Value.Trim.Replace(",", "")) < 0 Then
            txtNewCredit.Value = ""
            txtNewCredit.Focus()
            Exit Sub
        End If

        If CDbl(txtNewCredit.Value.Trim.Replace(",", "")) > CDbl(txtOldCredit.Value.Trim.Replace(",", "")) Then
            _Alert()
            alertOverMoney.Visible = True
            txtNewCredit.Focus()
            Exit Sub
        End If

        btConfirmSave2.Disabled = False
        lbTel.Text = txtTelephone.Value.Trim
        lbTel2.Text = txtTelephone.Value.Trim
        lbTelephone2.Text = txtTelephone.Value.Trim
        lbNewMoney2.Text = txtNewCredit.Value.Trim
        lbNewMoney3.Text = txtNewCredit.Value.Trim
        lbNewMoney.Text = txtNewCredit.Value.Trim
        lbOldMoney.Text = txtOldCredit.Value.Trim
        Dim a As Double = CDbl(txtOldCredit.Value.Replace(",", ""))
        Dim b As Double = CDbl(txtNewCredit.Value.Replace(",", ""))
        If (a - b) = 0 Then
            lbUpdateMoney.Text = 0
        Else
            lbUpdateMoney.Text = (a - b).ToString(String.Format("###,###,###,###,###,###"))
        End If
        txtConfirmPassword.Value = ""

        Dim percent As Double = 0
        Dim discount As Double = 0
        Dim i As Integer = 0
        Dim money As Double = CDbl(txtNewCredit.Value.Trim.Replace(",", ""))
        Dim _rowC = gridDiscount.GridControlElement.Rows
        For i = 0 To _rowC.Count - 1
            If money >= CDbl(_rowC(i).Cells(4).Text) And money <= CDbl(_rowC(i).Cells(5).Text) Then
                percent = CDbl(_rowC(i).Cells(6).Text)
            End If
        Next

        '//////////////// Tha pen pha nuk ngarn LTC man ja br me suan loud
        If Session("memberType") = "LTC" Then
            percent = 0
            discount = 0
        End If
        '/////////////////////////////////////////////////////////////////


        discount = money / 100 * percent
        lbNewCredit.Text = txtNewCredit.Value
        If percent <> 0 Then
            lbPercent.Text = percent.ToString("###,###,###,###,###,###,###.##")
            lbDiscount.Text = discount.ToString("###,###,###,###,###,###,###")
        Else
            lbPercent.Text = "0"
            lbDiscount.Text = "0"
        End If

        lbRealPaid.Text = (money - discount).ToString("###,###,###,###,###,###,###")



        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalDiscount();", True)
    End Sub

    Protected Sub btnSubmit_ServerClick(sender As Object, e As EventArgs) Handles btnSubmit.ServerClick
        _Alert()
        Session.Add("canPay", "Yes")
        btConfirmSave2.Disabled = False
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
    End Sub





    Public Structure UserResult
        Dim STATUS As Boolean
        Dim USR As String
        Dim NAME As String
        Dim LEVEL As Integer
        Dim DESC As String
    End Structure
    Private UserDetail As New UserResult

    Private Function _Login() As Boolean
        Dim _return As Boolean = False
        UserDetail.STATUS = False
        UserDetail.USR = Session("userid")
        UserDetail.NAME = ""
        UserDetail.LEVEL = -1
        UserDetail.DESC = ""
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            cm = New OracleCommand(myDataType.sqlQueryUser.Trim, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 10).Value = UserDetail.USR.Trim
            Dim USER_PASS As New OracleParameter("P_USER_PASS", OracleDbType.NVarchar2, 200) With {.Direction = ParameterDirection.Output}
            Dim USER_NAME As New OracleParameter("P_USER_NAME", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
            Dim USER_LEVEL As New OracleParameter("P_USER_LEVEL", OracleDbType.Int32) With {.Direction = ParameterDirection.Output}
            Dim USER_DESC As New OracleParameter("P_USER_DESC", OracleDbType.NVarchar2, 35) With {.Direction = ParameterDirection.Output}
            Dim USER_BRCH As New OracleParameter("P_BRANCH_ID", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            cm.Parameters.Add(USER_PASS)
            cm.Parameters.Add(USER_NAME)
            cm.Parameters.Add(USER_LEVEL)
            cm.Parameters.Add(USER_DESC)
            cm.Parameters.Add(USER_BRCH)
            cm.ExecuteNonQuery()
            If Not USER_PASS.IsNullable Then
                If fc.pp(txtConfirmPassword.Value.Trim) = (USER_PASS.Value).ToString.Trim Then
                    UserDetail.STATUS = True
                    UserDetail.NAME = IIf(USER_NAME.IsNullable, "", (USER_NAME.Value).ToString.Trim)
                    UserDetail.LEVEL = IIf(USER_LEVEL.IsNullable, 0, CInt(USER_LEVEL.Value))
                    UserDetail.DESC = IIf(USER_DESC.IsNullable, "", (USER_DESC.Value).ToString.Trim)
                End If
            End If
        Catch ex As Exception
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        If UserDetail.STATUS = True Then
            'password thuek torng
            _return = True
        Else
            'password br thuekkkk
            _return = False
        End If
        Return _return
    End Function

    Protected Sub btConfirmSave2_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave2.ServerClick

        If Session("canPay") = "No" Then
            If Session("nick") = "S" Then
                _Alert()
                alertSaveSucess.Visible = True
                lbInvoiceID.Text = txtInvoiceID.Text
                txtNewCredit.Value = ""
                txtOldCredit.Value = lbUpdateMoney.Text
                gridCredit.GridControlElement.PageIndex = 0
                gridCredit.RequestSource = _getCredit(txtTelephone.Value.Trim)
                If gridCredit.GridControlElement.Rows.Count <> 0 Then
                    'btnSave.Disabled = False
                    btnCheckDiscount.Disabled = False
                Else
                    'btnSave.Disabled = True
                    btnCheckDiscount.Disabled = True
                End If
                txtNewCredit.Focus()
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalSave();", True)
            ElseIf Session("nick") = "E" Then
                _Alert()
                alertSaveError.Visible = True
            End If
        Else
            Session("canPay") = "No"
            If txtConfirmPassword.Value = "" Then
                _Alert()
                alertPasswordEmpty.Visible = True
                Session("canPay") = "Yes"
                btConfirmSave2.Disabled = False
                Exit Sub
            End If
            If _Login() = False Then
                _Alert()
                alertPassword.Visible = True
                txtConfirmPassword.Value = ""
                Session("canPay") = "Yes"
                btConfirmSave2.Disabled = False
            Else
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalSave();", True)
                txtInvoiceID.Text = _InsertInvoice()
                If CLng(txtInvoiceID.Text) <> 0 Then
                    lbInvoiceID.Text = txtInvoiceID.Text
                    If _UpdateSale() = True Then
                        _UpdateInvoice(1)
                        fc.saveLog(Session("userid").ToString, "Receive Money - [Invoice_ID:" & txtInvoiceID.Text.ToString & "] [Telephone:" & txtTelephone.Value.ToString & "] [Amount:" & txtNewCredit.Value.ToString & "]")
                        Session.Add("bill", txtMemberNameMT.Value & "|" & txtTelephone.Value & "|" & txtAddress.Text & "|" & txtNewCredit.Value & " ກີບ|" & txtInvoiceID.Text & "|" & Today.Date.ToString("dd-MMM-yyyy") & "|" & _getBranchName(Session("userbranch").ToString) & "|" & Session("username").ToString & "|" & lbPercent.Text & "% ( " & lbDiscount.Text & " ກີບ )" & "|" & lbRealPaid.Text & " ກີບ|" & lbUpdateMoney.Text.Trim & " ກີບ")
                        fc.saveLogBill(Session("bill"))
                        _Alert()
                        alertSaveSucess.Visible = True
                        Session("nick") = "S"
                        txtNewCredit.Value = ""
                        txtOldCredit.Value = lbUpdateMoney.Text

                        gridCredit.GridControlElement.PageIndex = 0
                        gridCredit.RequestSource = _getCredit(txtTelephone.Value.Trim)
                        If gridCredit.GridControlElement.Rows.Count <> 0 Then
                            'btnSave.Disabled = False
                            btnCheckDiscount.Disabled = False
                        Else
                            'btnSave.Disabled = True
                            btnCheckDiscount.Disabled = True
                        End If
                        txtNewCredit.Focus()
                    Else
                        _UpdateInvoice(0)
                        gridCredit.GridControlElement.PageIndex = 0
                        gridCredit.RequestSource = _getCredit(txtTelephone.Value.Trim)
                        _Alert()
                        alertSaveError.Visible = True
                        Session("nick") = "E"
                    End If
                Else
                    _Alert()
                    alertSaveError.Visible = True
                    Session("nick") = "E"
                End If
                btConfirmSave2.Visible = True
            End If
        End If
        _gridColumnName()
    End Sub

    Private Function _InsertInvoice() As Long
        Dim p_invoice_id As Long = 0
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            Dim amt As Double = CDbl(txtNewCredit.Value.Trim.Replace(",", ""))
            Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertInvoice, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_total_amt", OracleDbType.Int64).Value = CDbl(txtNewCredit.Value.Trim.Replace(",", ""))
            cm.Parameters.Add("p_discount_amt", OracleDbType.Int64).Value = CDbl(lbDiscount.Text.Trim.Replace(",", ""))
            cm.Parameters.Add("p_user_id", OracleDbType.NVarchar2, 10).Value = Session("userid").ToString.Trim
            Dim P_ID As New OracleParameter("p_invoice_id", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            cm.Parameters.Add(P_ID)
            cm.ExecuteNonQuery()
            Try
                p_invoice_id = CLng(P_ID.Value)
            Catch ex As Exception

            End Try
        Catch ex As Exception
            Dim a As String = ex.ToString
            lbError.Text = a.ToString
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        Return p_invoice_id
    End Function


    Private Function _UpdateSale() As Boolean
        Dim _return As Boolean = False
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            Dim money As Double = CDbl(txtNewCredit.Value.Trim.Replace(",", ""))
            Dim paid As Double = 0
            Dim i As Integer = 0
            Dim _row = gridCredit.GridControlElement.Rows
            For i = 0 To _row.Count - 1
                If money >= CDbl(_row(i).Cells(9).Text.Replace(",", "")) Then
                    paid = CDbl(_row(i).Cells(9).Text.Replace(",", ""))
                    money = money - paid
                ElseIf money < CDbl(_row(i).Cells(9).Text.Replace(",", "")) And money > 0 Then
                    paid = money
                    money = 0
                Else
                    paid = 0
                End If
                If paid > 0 Then
                    Dim cm As OracleCommand = New OracleCommand(myDataType.sqlUpdateSale, cn) With {.CommandType = CommandType.StoredProcedure}
                    cm.Parameters.Add("p_sale_id", OracleDbType.Int64).Value = CDbl((_row(i).Cells(5).Text).Replace(",", ""))
                    cm.Parameters.Add("p_paid_amt", OracleDbType.Int64).Value = paid
                    cm.Parameters.Add("p_sts", OracleDbType.Int64).Value = 1
                    cm.Parameters.Add("p_status_user", OracleDbType.NVarchar2, 10).Value = Session("userid").ToString.Trim
                    cm.ExecuteNonQuery()
                    If _InsertInvoiceDetail(CDbl((_row(i).Cells(5).Text).Replace(",", "")), paid) = False Then
                        _return = False
                        Exit For
                    Else
                        _return = True
                    End If
                Else
                    Exit For
                End If
            Next
        Catch ex As Exception
            Dim a As String = ex.ToString
            lbError.Text = a.ToString
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
        Return _return
    End Function

    Private Function _InsertInvoiceDetail(sale_id As Double, amount As Double) As Boolean
        Dim _return As Boolean = False
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            'cn.ConnectionString = conDB.getConnectionString
            'cn.Open()
            Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertInvoiceDetail, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_invoice_id", OracleDbType.Int64).Value = CDbl(txtInvoiceID.Text.Trim)
            cm.Parameters.Add("p_sale_id", OracleDbType.Int64).Value = sale_id
            cm.Parameters.Add("p_amount", OracleDbType.Int64).Value = amount
            cm.ExecuteNonQuery()
            _return = True
        Catch ex As Exception
            _return = False
            lbError.Text = ex.ToString
        Finally
            'cn.Close()
            'cn.Dispose()
            cm.Dispose()
        End Try
        Return _return
    End Function



    Private Function _UpdateInvoice(sts As Integer) As Boolean
        Dim _return As Boolean = False
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            Dim amt As Double = CDbl(txtNewCredit.Value.Trim.Replace(",", ""))
            Dim cm As OracleCommand = New OracleCommand(myDataType.sqlUpdateInvoice, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_invoice_id", OracleDbType.Int64).Value = CDbl(txtInvoiceID.Text.Trim)
            cm.Parameters.Add("p_sts", OracleDbType.Int64).Value = sts
            cm.Parameters.Add("p_status_user", OracleDbType.NVarchar2, 10).Value = Session("userid").ToString.Trim
            cm.ExecuteNonQuery()
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

    Protected Sub btnPrintBill_ServerClick(sender As Object, e As EventArgs) Handles btnPrintBill.ServerClick
        Dim PageUrl As String = "receiveSlip.aspx"
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & PageUrl & "');", True)
    End Sub


    Public Function _getBranchName(id As Double) As String
        Dim _return As String = Nothing
        Dim sql As String = ""
       
        sql = String.Format("select branch_name" _
                               & " from mtopup.tbl_branch where branch_id={0}", id)
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "branch")
            _return = ds.Tables(0).Rows(0).Item(0).ToString
        Catch ex As Exception
           
        Finally
            cn.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
        Return _return
    End Function

End Class

' System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)

