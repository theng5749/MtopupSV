Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.Web.Configuration
Imports nickDevClass
'Imports ClosedXML.Excel
Imports System.IO

Partial Class member
    Inherits System.Web.UI.Page
    Dim conDB As New connectDB
    Dim cn As New OracleConnection
    Dim cm As New OracleCommand
    Dim fc As New SubFunction
    Dim _type As String = "None"
    Public Property SelectedText As String
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
                    menuUserCtrl.Visible = True
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
                    panelOldMTopup.Visible = True
                    panelNewMTopup.Visible = False
                    menuRechargeHistory.Visible = True
                ElseIf Session("userlevel").ToString = "11" Or Session("userlevel").ToString = "12" Or Session("userlevel").ToString = "13" Then
                    _menu()
                    menuMember.Visible = True
                    menuRecharge.Visible = True
                    cbReport.Visible = True
                    menuRechargeHistory.Visible = True
                    btnMainOld.Disabled = True
                    panelOldMTopup.Visible = False
                    panelNewMTopup.Visible = True
                    LoadMemberPlus("Init")
                    btnSavePlus.Disabled = False
                    _Alert()
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
            _load()
            FillData("")
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
        menuUserCtrl.Visible = False
        menuCheckApprove.Visible = False
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
            gridBranch.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridBranch.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່ກຸ່ມ"
        End If
        If gridRootModal.GridControlElement.Rows.Count <> 0 Then
            gridRootModal.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridRootModal.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
            gridRootModal.GridControlElement.HeaderRow.Cells(6).Text = "ຊື່ສະມາຊິກ"
            gridRootModal.GridControlElement.HeaderRow.Cells(7).Text = "ລະດັບສະມາຊິກ"
            gridRootModal.GridControlElement.HeaderRow.Cells(8).Text = "ປະເພດສະມາຊິກ"
            gridRootModal.GridControlElement.HeaderRow.Cells(11).Text = "ສະຖານະ"
            gridRootModal.GridControlElement.HeaderRow.Cells(12).Text = "ລະຫັດກຸ່ມ"
            gridRootModal.GridControlElement.HeaderRow.Cells(13).Text = "ກຸ່ມ"
        End If

        If gridMember.GridControlElement.Rows.Count <> 0 Then
            gridMember.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridMember.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
            gridMember.GridControlElement.HeaderRow.Cells(6).Text = "ຈຳນວນເງິນທີ່ມີ"
            gridMember.GridControlElement.HeaderRow.Cells(8).Text = "ລະດັບສະມາຊິກ"
            gridMember.GridControlElement.HeaderRow.Cells(9).Text = "ຊື່ສະມາຊິກ"
            gridMember.GridControlElement.HeaderRow.Cells(12).Text = "ປະເພດສະມາຊິກ"
            gridMember.GridControlElement.HeaderRow.Cells(13).Text = "ເບີແມ່ (Root)"
            gridMember.GridControlElement.HeaderRow.Cells(15).Text = "ບັນທຶກຂໍ້ມູນໂດຍ"
            gridMember.GridControlElement.HeaderRow.Cells(16).Text = "ສະຖານະ"
            gridMember.GridControlElement.HeaderRow.Cells(17).Text = "ລະຫັດກຸ່ມ"
            gridMember.GridControlElement.HeaderRow.Cells(18).Text = "ກຸ່ມ"
        End If
        If GridMemberPlus.GridControlElement.Rows.Count <> 0 Then
            GridMemberPlus.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            GridMemberPlus.GridControlElement.HeaderRow.Cells(5).Text = "ໝາຍເລກໂທລະສັບ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(6).Text = "ຈຳນວນເງິນທີ່ມີ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(8).Text = "ລະດັບສະມາຊິກ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(9).Text = "ຊື່ສະມາຊິກ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(12).Text = "ປະເພດສະມາຊິກ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(15).Text = "ບັນທຶກຂໍ້ມູນໂດຍ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(16).Text = "ສະຖານະ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(17).Text = "ລະຫັດກຸ່ມ"
            GridMemberPlus.GridControlElement.HeaderRow.Cells(18).Text = "ກຸ່ມ"
        End If

        If gridExport.GridControlElement.Rows.Count <> 0 Then
            gridExport.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridExport.GridControlElement.HeaderRow.Cells(0).Text = "ລຳດັບ"
            gridExport.GridControlElement.HeaderRow.Cells(1).Text = "ເບີ MTopup"
            gridExport.GridControlElement.HeaderRow.Cells(2).Text = "ຈຳນວນເງິນທີ່ມີ"
            gridExport.GridControlElement.HeaderRow.Cells(3).Text = "ເບີຕິດຕໍ່"
            gridExport.GridControlElement.HeaderRow.Cells(4).Text = "Level"
            gridExport.GridControlElement.HeaderRow.Cells(5).Text = "ຊື່"
            gridExport.GridControlElement.HeaderRow.Cells(6).Text = "ທີ່ຢູ່"
            gridExport.GridControlElement.HeaderRow.Cells(7).Text = "ປະເພດສະມາຊິກ"
            gridExport.GridControlElement.HeaderRow.Cells(8).Text = "ເບີແມ່"
            gridExport.GridControlElement.HeaderRow.Cells(9).Text = "ຈັດການຂໍ້ມູນໂດຍ"
            gridExport.GridControlElement.HeaderRow.Cells(10).Text = "ສະຖານະ"
            gridExport.GridControlElement.HeaderRow.Cells(11).Text = "ກຸ່ມ"
        End If

    End Sub
    Protected Sub _load()
        If Session("userlevel").ToString <> "9" Then
            cbLevel.Items(0).Enabled = False
            cbLevel.SelectedIndex = 1
            _tabMenu()
            'menuAllLevel.Visible = False
            menuL1.Visible = False
            menuAllLevel.Attributes.Add("class", "active")
            Session.Add("tab", "0")
        Else
            _tabMenu()
            menuAllLevel.Attributes.Add("class", "active")
            Session.Add("tab", "0")
            cbLevel.SelectedIndex = 0
        End If
        statusEdit.Disabled = True
        If cbLevel.SelectedIndex = 0 Then
            txtRootMTID.Value = ""
            divBranch.Visible = True
            divBranch2.Visible = False
            divRoot.Visible = False
        Else
            txtBranchID.Value = ""
            divBranch.Visible = False
            divBranch2.Visible = False
            divRoot.Visible = True
        End If
        _Alert()
        txtMemberIDMT.Value = ""
        txtMemberNameMT.Value = ""
        txtMerberAddressMT.Value = ""
        txtMemberNoIDMT.Value = ""
        txtTelMT.Value = ""
        txtContact.Value = ""
        txtRootMT.Value = ""
        txtRootMTID.Value = ""
    End Sub
    Sub _clear()
        statusEdit.Disabled = True
        txtMemberIDMT.Value = ""
        cbLevel.SelectedIndex = 0
        txtMemberNameMT.Value = ""
        txtMerberAddressMT.Value = ""
        txtMemberNoIDMT.Value = ""
        txtTelMT.Value = ""
        txtContact.Value = ""
        txtRootMT.Value = ""
        txtRootMTID.Value = ""
        txtBranchID.Value = ""
        txtBranchName.Value = ""
        If cbLevel.SelectedIndex = 0 And Session("userlevel").ToString.Trim <> "0" Then
            txtRootMTID.Value = ""
            divBranch.Visible = True
            divBranch2.Visible = False
            divRoot.Visible = False
        Else
            txtBranchID.Value = ""
            divBranch.Visible = False
            divBranch2.Visible = False
            divRoot.Visible = True
        End If
    End Sub
    Private Sub _Alert()
        alertSaveError.Visible = False
        alertSaveSucess.Visible = False
        alertRoot.Visible = False
        alertTel.Visible = False
        alertContact.Visible = False
        alertName.Visible = False
        alertBranch.Visible = False
        alertDelete.Visible = False
        '/// MTOPUP Plus
        AlertDeletePlus.Visible = False
        AlertStampCodeEmpty.Visible = False
        AlertSaveFailPlus.Visible = False
        AlertSaveSuccessPlus.Visible = False
        AlertEmptytxt.Visible = False
        AlertSampCode.Visible = False
        panelStampCode.Visible = False
        btnUpdatePlus.Disabled = True
    End Sub
    Protected Sub btnRoot_ServerClick(sender As Object, e As EventArgs) Handles btnRoot.ServerClick
        Dim rootLevel As Integer = cbLevel.SelectedIndex
        txtSearchRoot.Text = ""
        _getRoot(txtSearchRoot.Text, rootLevel)
        _Alert()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectRoot();", True)
    End Sub
    Private Sub _getRoot(searchText As String, rootLevel As Integer)
        Dim sql As String = ""

        Dim userLevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim
        If Session("userlevel").ToString.Trim = "0" Then
            If txtSearchRoot.Text <> "" Then
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                  & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and (tbl_member.telephone like '%{1}%' or tbl_member.member_name like '%{1}%') and tbl_member.brch_id=" & brchID & " and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100  order by tbl_member.member_id desc", rootLevel, searchText)
            Else
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                   & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and tbl_member.brch_id=" & brchID & " and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_id desc", rootLevel)
            End If
        Else
            If txtSearchRoot.Text <> "" Then
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                  & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and (tbl_member.telephone like '%{1}%' or tbl_member.member_name like '%{1}%') and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100  order by tbl_member.member_id desc", rootLevel, searchText)
            Else
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                   & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_id desc", rootLevel)
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
            gridRootModal.RequestSource = ds.Tables("member")
            gridRootModal.GridControlElement.PageIndex = 0
            gridRootModal.DataBind()
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
    Private Sub _getBranch()
        Dim sql As String = ""
        If txtSearchBranch.Text <> "" Then
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where branch_id!=15 and sts=1 and branch_name like '%{0}%' order by branch_id desc", txtSearchBranch.Text.Trim)
        Else
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where branch_id!=15 and sts=1 order by branch_id desc")
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
            gridBranch.GridControlElement.PageIndex = 0
            gridBranch.DataBind()
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
    Private Sub FillData(ByVal telephone As String)
        Dim sql As String = ""
        Dim userlevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim
        Dim menu As String = Session("tab").ToString.Trim
        If menu = "0" Then
            If userlevel = "0" Then
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_level asc"
                End If
            End If


        ElseIf menu = "1" Then
            If userlevel = "0" Then
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=1 and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and tbl_member.member_level=1 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=1 and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "select * from view_member_details"
                End If
            End If


        ElseIf menu = "2" Then
            If userlevel = "0" Then
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            End If

        ElseIf menu = "3" Then
            If userlevel = "0" Then
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%'  or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=3 and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and tbl_member.member_level=3 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If telephone.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=3 and rownum <= 100 order by tbl_member.member_level asc", telephone.Trim)
                Else
                    sql = "view_member_detail_level3"
                End If
            End If
        End If
        cn = New OracleConnection
        Dim da As New OracleDataAdapter
        da = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "member")
            ds.Tables.Clear()
            da.Fill(ds, "member")
            cn.Close()
            gridMember.RequestSource = ds.Tables("member")
            gridMember.GridControlElement.PageIndex = 0
            gridMember.DataBind()

            If ds.Tables(0).Rows.Count <> 0 Then
                Dim dsEx As New DataSet
                Dim i As Double = 0
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        dsEx.Tables.Add()
                        dsEx.Tables(0).Rows.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                        dsEx.Tables(0).Columns.Add()
                    Else
                        dsEx.Tables(0).Rows.Add()
                    End If
                    dsEx.Tables(0).Rows(i).Item(0) = ds.Tables(0).Rows(i).Item(1).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(1) = ds.Tables(0).Rows(i).Item(2).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(2) = ds.Tables(0).Rows(i).Item(3).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(3) = ds.Tables(0).Rows(i).Item(4).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(4) = ds.Tables(0).Rows(i).Item(5).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(5) = ds.Tables(0).Rows(i).Item(6).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(6) = ds.Tables(0).Rows(i).Item(8).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(7) = ds.Tables(0).Rows(i).Item(9).ToString.Trim
                    dsEx.Tables(0).Rows(i).Item(8) = ds.Tables(0).Rows(i).Item(11).ToString.Trim
                    If ds.Tables(0).Rows(i).Item(12).ToString.Trim = "1" Then
                        dsEx.Tables(0).Rows(i).Item(9) = "Active"
                    Else
                        dsEx.Tables(0).Rows(i).Item(9) = "Deactived"
                    End If
                    dsEx.Tables(0).Rows(i).Item(10) = ds.Tables(0).Rows(i).Item(14).ToString.Trim
                Next
                gridExport.RequestSource = dsEx.Tables(0)
                gridExport.DataBind()
            Else
                gridExport.RequestSource = Nothing
                gridExport.DataBind()
            End If

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
    Protected Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        FillData(txtSearchText.Text.Trim.ToString)
    End Sub
    Protected Sub txtSearchText_TextChanged(sender As Object, e As EventArgs) Handles txtSearchText.TextChanged
        FillData(txtSearchText.Text.Trim.ToString)
    End Sub
    Protected Sub gridMember_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridMember.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(17).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(17).Visible = False

            e.Row.Cells(5).Font.Bold = True

            'kuadva tha man level 1 man br hai show tha br man super admin
            If Session("userlevel").ToString <> "9" Then
                If e.Row.Cells(8).Text = "1" Then
                    e.Row.Visible = False
                End If
            End If

            e.Row.Cells(18).ForeColor = Drawing.Color.CornflowerBlue
            e.Row.Cells(18).Font.Bold = True
            If e.Row.Cells(16).Text.ToString = "1" Then
                e.Row.Cells(16).Text = "Active"
                e.Row.Cells(16).ForeColor = Drawing.Color.Green
                e.Row.Cells(16).Font.Bold = True
            Else
                e.Row.Cells(16).Text = "Deactivated"
                e.Row.Cells(16).ForeColor = Drawing.Color.Red
                e.Row.Cells(16).Font.Bold = True
            End If

            e.Row.Cells(6).Font.Bold = True
            e.Row.Cells(6).ForeColor = Drawing.Color.OrangeRed
            If CDbl(e.Row.Cells(6).Text) > 0 Then
                e.Row.Cells(6).Text = CDbl(e.Row.Cells(6).Text).ToString("#,##0")
            End If
        End If
    End Sub
    Protected Sub gridRootModal_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridRootModal.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(12).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(12).Visible = False

            e.Row.Cells(13).ForeColor = Drawing.Color.CornflowerBlue
            e.Row.Cells(13).Font.Bold = True

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
    Protected Sub cbLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLevel.SelectedIndexChanged
        _Alert()

        If statusEdit.Disabled = False And cbLevel.SelectedIndex = 0 Then
            txtRootMTID.Value = ""
            divBranch.Visible = True
            divBranch2.Visible = False
            divRoot.Visible = False

        ElseIf statusEdit.Disabled = False And cbLevel.SelectedIndex <> 0 Then
            divBranch.Visible = False
            divBranch2.Visible = False
            btnRoot.Disabled = False
            divRoot.Visible = True

        ElseIf statusEdit.Disabled = True And cbLevel.SelectedIndex = 0 Then
            txtRootMTID.Value = ""
            divBranch.Visible = True
            divBranch2.Visible = False
            divRoot.Visible = False
        ElseIf statusEdit.Disabled = True And cbLevel.SelectedIndex <> 0 Then
            divBranch.Visible = False
            divBranch2.Visible = False
            btnRoot.Disabled = False
            divRoot.Visible = True
        End If
        txtRootMT.Value = ""
        txtRootMTID.Value = ""
        'txtBranchID.Value = ""
        'txtBranchName.Value = ""
    End Sub
    Protected Sub txtSearchRoot_TextChanged(sender As Object, e As EventArgs) Handles txtSearchRoot.TextChanged
        Dim rootLevel As Integer = cbLevel.SelectedIndex
        _getRoot(txtSearchRoot.Text, rootLevel)
    End Sub
    Protected Sub btnSearchRoot_ServerClick(sender As Object, e As EventArgs) Handles btnSearchRoot.ServerClick
        Dim rootLevel As Integer = cbLevel.SelectedIndex
        _getRoot(txtSearchRoot.Text, rootLevel)
    End Sub
    Protected Sub gridRootModal_GridRowClick(sender As Object, e As EventArgs) Handles gridRootModal.GridRowClick
        txtBranchID.Value = gridRootModal.GridControlElement.SelectedRow.Cells(12).Text.ToString.Trim
        txtBranchName.Value = gridRootModal.GridControlElement.SelectedRow.Cells(13).Text.ToString.Trim
        txtRootMTID.Value = gridRootModal.GridControlElement.SelectedRow.Cells(4).Text
        txtRootMT.Value = "( " & gridRootModal.GridControlElement.SelectedRow.Cells(5).Text & " ) " & gridRootModal.GridControlElement.SelectedRow.Cells(6).Text
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalRoot();", True)
    End Sub
    Private Function AddMember() As Long
        Dim rp As Long = 0
        cn = New OracleConnection
        Dim rootID As Double
        Dim branchID As Double
        If txtRootMTID.Value <> "" Then
            rootID = CDbl(txtRootMTID.Value)
        Else
            rootID = 0
        End If
        If txtBranchID.Value <> "" Then
            branchID = CDbl(txtBranchID.Value)
        Else
            branchID = CDbl(Session("userbranch").ToString)
        End If
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            Dim sts As Integer = 0
            If cbActive.Checked = True Then
                sts = 1
            Else
                sts = 0
            End If
            If statusEdit.Disabled = True Then
                'Add news member
                Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertMember, cn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = txtTelMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_LEVEL", OracleDbType.Int32).Value = CInt(cbLevel.SelectedValue)
                cm.Parameters.Add("P_STS", OracleDbType.Int32).Value = sts
                cm.Parameters.Add("P_MEMBER_NAME", OracleDbType.NVarchar2, 50).Value = txtMemberNameMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_ADDRESS", OracleDbType.NVarchar2, 30).Value = txtMerberAddressMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_NO_ID", OracleDbType.NVarchar2, 50).Value = txtMemberNoIDMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_CONTACT_NUMBER", OracleDbType.NVarchar2, 50).Value = txtContact.Value.Trim
                cm.Parameters.Add("P_MEMBER_TYPE", OracleDbType.NVarchar2, 3).Value = Mid(cbMemberType.SelectedValue.ToUpper.Trim, 1, 3)
                cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString
                cm.Parameters.Add("P_ROOT_ID", OracleDbType.Int64).Value = rootID
                cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = branchID
                Dim P_MEMBER_ID As New OracleParameter("P_MEMBER_ID", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
                cm.Parameters.Add(P_MEMBER_ID)
                cm.ExecuteNonQuery()
                '///////// SaveLog  ////////////
                fc.saveLog(Session("userid").ToString, "Add new member - [id:" & P_MEMBER_ID.Value.ToString & "] [name:" & txtMemberNameMT.Value.ToString & "] [level:" & cbLevel.SelectedValue & "] [telephone:" & txtTelMT.Value.ToString & "] [status:" & sts & "]")
                Try
                    rp = P_MEMBER_ID.Value.ToString()
                Catch ex As Exception

                End Try
                _Alert()
                alertSaveSucess.Visible = True
                statusEdit.Disabled = True
                txtSearchText.Text = txtTelMT.Value.Trim
                If cbLevel.SelectedValue.ToString.Trim = "1" Then
                    _tabMenu()
                    menuL1.Attributes.Add("class", "active")
                    Session.Add("tab", "1")
                ElseIf cbLevel.SelectedValue.ToString.Trim = "2" Then
                    _tabMenu()
                    menuL2.Attributes.Add("class", "active")
                    Session.Add("tab", "2")
                ElseIf cbLevel.SelectedValue.ToString.Trim = "3" Then
                    _tabMenu()
                    menuL3.Attributes.Add("class", "active")
                    Session.Add("tab", "3")
                End If

                FillData(txtSearchText.Text.Trim.ToString)
            Else
                'Update member
                cm = New OracleCommand(myDataType.sqlUpdateMember, cn) With {.CommandType = CommandType.StoredProcedure}
                cm.Parameters.Add("p_member_id", OracleDbType.Int64).Value = CDbl(txtMemberIDMT.Value.Trim)
                cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = txtTelMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_LEVEL", OracleDbType.Int64).Value = CInt(cbLevel.SelectedValue)
                cm.Parameters.Add("P_STS", OracleDbType.Int32).Value = sts
                cm.Parameters.Add("P_MEMBER_NAME", OracleDbType.NVarchar2, 30).Value = txtMemberNameMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_ADDRESS", OracleDbType.NVarchar2, 50).Value = txtMerberAddressMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_NO_ID", OracleDbType.NVarchar2, 30).Value = txtMemberNoIDMT.Value.Trim
                cm.Parameters.Add("P_MEMBER_CONTACT_NUMBER", OracleDbType.NVarchar2, 50).Value = txtContact.Value.Trim
                cm.Parameters.Add("P_MEMBER_TYPE", OracleDbType.NVarchar2, 3).Value = Mid(cbMemberType.SelectedValue.ToUpper.Trim, 1, 3)
                cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString
                cm.Parameters.Add("P_ROOT_ID", OracleDbType.Int64).Value = rootID
                cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = branchID
                cm.ExecuteNonQuery()
                '///////// SaveLog  ////////////
                fc.saveLog(Session("userid").ToString, "Edit member - [id:" & txtMemberIDMT.Value.ToString & "] [name:" & txtMemberNameMT.Value.ToString & "] [level:" & cbLevel.SelectedValue & "] [telephone:" & txtTelMT.Value.ToString & "] [status:" & sts & "]")
                _Alert()
                alertSaveSucess.Visible = True
                statusEdit.Disabled = True
                txtSearchText.Text = txtTelMT.Value.Trim
                If cbLevel.SelectedValue.ToString.Trim = "1" Then
                    _tabMenu()
                    menuL1.Attributes.Add("class", "active")
                    Session.Add("tab", "1")
                ElseIf cbLevel.SelectedValue.ToString.Trim = "2" Then
                    _tabMenu()
                    menuL2.Attributes.Add("class", "active")
                    Session.Add("tab", "2")
                ElseIf cbLevel.SelectedValue.ToString.Trim = "3" Then
                    _tabMenu()
                    menuL3.Attributes.Add("class", "active")
                    Session.Add("tab", "3")
                End If

                FillData(txtSearchText.Text.Trim.ToString)
            End If
        Catch ex As Exception
            Dim a As String = ex.ToString
            _Alert()
            alertSaveError.Visible = True
            statusEdit.Disabled = False
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try


        Return rp
    End Function
    Protected Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        lbTelSave.Text = txtTelMT.Value
        lbTel.Text = txtTelMT.Value
        lbTel2.Text = txtTelMT.Value
        If cbLevel.SelectedIndex > 0 Then
            If txtRootMTID.Value = "" Then
                _Alert()
                alertRoot.Visible = True
                btnRoot.Focus()
                Exit Sub
            End If
        End If
        If divBranch.Visible = True And txtBranchID.Value.Trim = "" Then
            _Alert()
            alertBranch.Visible = True
            Exit Sub
        End If
        If txtMemberNameMT.Value.Trim = "" Then
            _Alert()
            alertName.Visible = True
            txtMemberNameMT.Focus()
            Exit Sub
        End If
        If (txtTelMT.Value.Trim.StartsWith("205") And txtTelMT.Value.Trim.Length = 10) Or (txtTelMT.Value.Trim.StartsWith("305") And txtTelMT.Value.Trim.Length = 9) Then
        Else
            _Alert()
            alertTel.Visible = True
            txtTelMT.Focus()
            Exit Sub
        End If
        If (txtContact.Value.Trim.StartsWith("205") And txtContact.Value.Trim.Length = 10) Or (txtContact.Value.Trim.StartsWith("305") And txtContact.Value.Trim.Length = 9) Then
        Else
            _Alert()
            alertContact.Visible = True
            txtContact.Focus()
            Exit Sub
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
    End Sub
    Protected Sub btCancel_ServerClick(sender As Object, e As EventArgs) Handles btCancel.ServerClick
        _load()
    End Sub
    Protected Sub btConfirmSave_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave.ServerClick
        Dim member_id = AddMember()
        txtMemberIDMT.Value = member_id
        If member_id.ToString() <> "" Then
            Dim resultCheckMemCredit As Boolean = _checkMemberNew(txtTelMT.Value.Trim)
            If resultCheckMemCredit = False Then
                AddMemberNew(member_id)

            End If

        End If
        _clear()
        'If CLng(txtMemberIDMT.Value) > 0 Then
        '    'txtSearchText.Text = ""
        '    '_tabMenu()
        '    'menuAllLevel.Attributes.Add("class", "active")
        '    'Session.Add("tab", "0")
        '    'FillData("")
        'End If
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

    Private Sub AddMemberNew(memberId As Long)
        cn = New OracleConnection
        Dim rootID As Double
        Dim branchID As Double
        If txtRootMTID.Value <> "" Then
            rootID = CDbl(txtRootMTID.Value)
        Else
            rootID = 0
        End If
        If txtBranchID.Value <> "" Then
            branchID = CDbl(txtBranchID.Value)
        Else
            branchID = CDbl(Session("userbranch").ToString)
        End If
        Try
            cn.ConnectionString = conDB.getConnectionWebCallString
            cn.Open()

            'Add news member
            Dim cm As OracleCommand = New OracleCommand("mtopup.STP_INSERT_MEMBER", cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_member_id", OracleDbType.Int64).Value = memberId
            cm.Parameters.Add("p_telephone", OracleDbType.NVarchar2, 10).Value = txtTelMT.Value.Trim
            cm.Parameters.Add("p_member_level", OracleDbType.Int32).Value = CInt(cbLevel.SelectedValue)
            cm.Parameters.Add("p_sts", OracleDbType.Int32).Value = 1
            cm.Parameters.Add("p_member_name", OracleDbType.NVarchar2, 50).Value = txtMemberNameMT.Value.Trim
            cm.Parameters.Add("p_member_address", OracleDbType.NVarchar2, 30).Value = txtMerberAddressMT.Value.Trim
            cm.Parameters.Add("p_member_no_id", OracleDbType.NVarchar2, 50).Value = txtMemberNoIDMT.Value.Trim
            cm.Parameters.Add("p_member_contact_number", OracleDbType.NVarchar2, 50).Value = txtContact.Value.Trim
            cm.Parameters.Add("p_member_type", OracleDbType.NVarchar2, 3).Value = Mid(cbMemberType.SelectedValue.ToUpper.Trim, 1, 3)
            cm.Parameters.Add("p_user_id", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString
            cm.Parameters.Add("p_root_id", OracleDbType.Int64).Value = rootID
            cm.Parameters.Add("p_branch_id", OracleDbType.Int64).Value = branchID
            cm.ExecuteNonQuery()

        Catch ex As Exception
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try



    End Sub
    Protected Sub btnAlertContact_ServerClick(sender As Object, e As EventArgs) Handles btnAlertContact.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertError_ServerClick(sender As Object, e As EventArgs) Handles btnAlertError.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertName_ServerClick(sender As Object, e As EventArgs) Handles btnAlertName.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertRoot_ServerClick(sender As Object, e As EventArgs) Handles btnAlertRoot.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertSuccess_ServerClick(sender As Object, e As EventArgs) Handles btnAlertSuccess.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertTel_ServerClick(sender As Object, e As EventArgs) Handles btnAlertTel.ServerClick
        _Alert()
    End Sub
    Protected Sub btnAlertBranch_ServerClick(sender As Object, e As EventArgs) Handles btnAlertBranch.ServerClick
        _Alert()
    End Sub
    Protected Sub gridMember_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridMember.GridViewPageIndexChanged
        Dim sql As String = ""
        Dim userlevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim
        Dim menu As String = Session("tab").ToString.Trim

        If menu = "0" Then
            If userlevel = "0" Then
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_level asc"
                End If
            End If


        ElseIf menu = "1" Then
            If userlevel = "0" Then
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=1 and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and tbl_member.member_level=1 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=1 and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=1 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            End If


        ElseIf menu = "2" Then
            If userlevel = "0" Then
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=2 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            End If


        ElseIf menu = "3" Then
            If userlevel = "0" Then
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%'  or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=" & brchID & " and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=3 and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.brch_id=" & brchID & " and tbl_member.member_level=3 and rownum <= 100 order by tbl_member.member_level asc"
                End If
            Else
                If txtSearchText.Text.Trim <> "" Then
                    sql = String.Format("select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                        & " from mtopup.tbl_member,mtopup.tbl_branch where (tbl_member.telephone like '%{0}%' or tbl_member.member_level like '%{0}%' or tbl_member.member_name like '%{0}%' or tbl_member.member_type like '%{0}%' or tbl_branch.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) like '%{0}%') and tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=3 and rownum <= 100 order by tbl_member.member_level asc", txtSearchText.Text.Trim)
                Else
                    sql = "select tbl_member.member_id, tbl_member.telephone, mtopup.FN_CHECKING_CREDIT(tbl_member.telephone) as member_credit,tbl_member.member_contact_number, tbl_member.member_level, tbl_member.member_name, tbl_member.member_address, tbl_member.member_no_id, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.root_id, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                        & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.brch_id=tbl_branch.branch_id and tbl_member.member_level=3 and rownum <= 100 order by tbl_member.member_level asc"
                End If
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
            gridMember.RequestSource = ds.Tables("member")
            gridMember.GridControlElement.PageIndex = e.NewPageIndex
            gridMember.DataBind()
        Catch ex As Exception
            'lbShort.ForeColor = Drawing.Color.Red
            'lbShort.Text = "Query Data records is error."
        Finally
            cn.Close()
            cn.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
        _gridColumnName()
    End Sub
    Protected Sub gridRootModal_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridRootModal.GridViewPageIndexChanged
        Dim sql As String = ""
        Dim userLevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim
        If Session("userlevel").ToString.Trim = "0" Then
            If txtSearchRoot.Text <> "" Then
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                  & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and (tbl_member.telephone like '%{1}%' or tbl_member.member_name like '%{1}%') and tbl_member.brch_id=" & brchID & " and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100  order by tbl_member.member_id desc", cbLevel.SelectedIndex, txtSearchRoot.Text.Trim)
            Else
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                   & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and tbl_member.brch_id=" & brchID & " and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_id desc", cbLevel.SelectedIndex)
            End If
        Else
            If txtSearchRoot.Text <> "" Then
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                  & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and (tbl_member.telephone like '%{1}%' or tbl_member.member_name like '%{1}%') and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100  order by tbl_member.member_id desc", cbLevel.SelectedIndex, txtSearchRoot.Text.Trim)
            Else
                sql = String.Format("select  tbl_member.member_id, tbl_member.telephone, tbl_member.member_name, tbl_member.member_level, tbl_member.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(tbl_member.root_id) as root_number, tbl_member.user_id, tbl_member.sts,tbl_member.brch_id,tbl_branch.branch_name" _
                                   & " from mtopup.tbl_member,mtopup.tbl_branch where tbl_member.member_level={0} and tbl_member.sts=1 and tbl_member.brch_id=tbl_branch.branch_id and rownum <= 100 order by tbl_member.member_id desc", cbLevel.SelectedIndex)
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
            gridRootModal.RequestSource = ds.Tables("member")
            gridRootModal.GridControlElement.PageIndex = e.NewPageIndex
            gridRootModal.DataBind()
        Catch ex As Exception
            'lbShort.ForeColor = Drawing.Color.Red
            'lbShort.Text = "Query Data records is error."
        Finally
            cn.Close()
            cn.Dispose()

            da.Dispose()
            ds.Dispose()
        End Try
        _gridColumnName()
    End Sub
    Protected Sub btnBranch_ServerClick(sender As Object, e As EventArgs) Handles btnBranch.ServerClick
        txtSearchBranch.Text = ""
        _getBranch()
        _Alert()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectBranch();", True)
    End Sub
    Protected Sub txtSearchBranch_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBranch.TextChanged
        _getBranch()
    End Sub
    Protected Sub gridBranch_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridBranch.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
        End If
    End Sub
    Protected Sub gridBranch_GridRowClick(sender As Object, e As EventArgs) Handles gridBranch.GridRowClick
        txtBranchID.Value = gridBranch.GridControlElement.SelectedRow.Cells(4).Text
        txtBranchName.Value = gridBranch.GridControlElement.SelectedRow.Cells(5).Text
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalBranch();", True)
    End Sub
    Protected Sub gridBranch_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridBranch.GridViewPageIndexChanged
        Dim sql As String = ""
        If txtSearchBranch.Text <> "" Then
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where branch_id!=15 and sts=1 and branch_name like '%{0}%' order by branch_id desc", txtSearchBranch.Text.Trim)
        Else
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where branch_id!=15 and sts=1 order by branch_id desc")
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
            gridBranch.GridControlElement.PageIndex = e.NewPageIndex
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
    Protected Sub btnDelete_ServerClick(sender As Object, e As EventArgs) Handles btnDelete.ServerClick
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalDelete();", True)
    End Sub
    Protected Sub btConfirmDelete_ServerClick(sender As Object, e As EventArgs) Handles btConfirmDelete.ServerClick
        Dim i As Integer = 0
        For Each _row As GridViewRow In gridMember.GridControlElement.Rows
            Dim chk As CheckBox = _row.FindControl("CheckBox1")
            If chk.Checked = True Then
                Try
                    'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
                    cn.ConnectionString = conDB.getConnectionString
                    cn.Open()
                    cm = New OracleCommand(myDataType.sqlUpdateMember, cn) With {.CommandType = CommandType.StoredProcedure}
                    cm.Parameters.Add("p_member_id", OracleDbType.Int64).Value = CDbl(_row.Cells(4).Text.Trim)
                    cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = _row.Cells(5).Text.Trim
                    cm.Parameters.Add("P_MEMBER_LEVEL", OracleDbType.Int64).Value = CInt(_row.Cells(7).Text.Trim)
                    cm.Parameters.Add("P_STS", OracleDbType.Int16).Value = 0
                    cm.Parameters.Add("P_MEMBER_NAME", OracleDbType.NVarchar2, 50).Value = _row.Cells(8).Text.Trim
                    cm.Parameters.Add("P_MEMBER_ADDRESS", OracleDbType.NVarchar2, 30).Value = _row.Cells(9).Text.Trim
                    cm.Parameters.Add("P_MEMBER_NO_ID", OracleDbType.NVarchar2, 50).Value = _row.Cells(10).Text.Trim
                    cm.Parameters.Add("P_MEMBER_CONTACT_NUMBER", OracleDbType.NVarchar2, 50).Value = _row.Cells(6).Text.Trim
                    cm.Parameters.Add("P_MEMBER_TYPE", OracleDbType.NVarchar2, 3).Value = Mid(_row.Cells(11).Text.Trim, 1, 3)
                    cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString
                    cm.Parameters.Add("P_ROOT_ID", OracleDbType.Int64).Value = CDbl(_row.Cells(13).Text.Trim)
                    cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = CDbl(_row.Cells(16).Text.Trim)
                    cm.ExecuteNonQuery()
                    i += 1
                    '///////// SaveLog  ////////////
                    fc.saveLog(Session("userid").ToString, "Edit member - [id:" & _row.Cells(4).Text.Trim & "] [name:" & _row.Cells(8).Text.Trim & "] [level:" & _row.Cells(7).Text.Trim & "] [telephone:" & _row.Cells(5).Text.Trim & "] [status:0]")
                    FillData(txtSearchText.Text.Trim)
                Catch ex As Exception

                Finally
                    cn.Close()
                    cn.Dispose()
                    cm.Dispose()
                End Try
            End If
        Next
        lbCountUser.Text = i.ToString
        _Alert()
        alertDelete.Visible = True
    End Sub
    Sub _tabMenu()
        menuL1.Attributes.Remove("class")
        menuL2.Attributes.Remove("class")
        menuL3.Attributes.Remove("class")
        menuAllLevel.Attributes.Remove("class")
    End Sub
    Protected Sub gridMember_GridRowClick(sender As Object, e As EventArgs) Handles gridMember.GridRowClick
        statusEdit.Disabled = False
        _Alert()
        Dim row = gridMember.GridControlElement.SelectedRow
        txtMemberIDMT.Value = row.Cells(4).Text.Trim
        cbLevel.SelectedValue = row.Cells(8).Text.Trim
        If cbLevel.SelectedValue = 1 Then
            btnRoot.Disabled = True
        Else
            btnRoot.Disabled = False
        End If
        If row.Cells(13).Text = "&nbsp;" Or row.Cells(13).Text = "" Or row.Cells(13).Text = " " Then
            txtRootMT.Value = ""
            txtRootMTID.Value = ""
        Else
            txtRootMT.Value = row.Cells(13).Text
            txtRootMTID.Value = row.Cells(14).Text
            btnRoot.Disabled = False
        End If
        txtMemberNameMT.Value = row.Cells(9).Text.Trim
        txtMerberAddressMT.Value = row.Cells(10).Text.Trim
        txtTelMT.Value = row.Cells(5).Text.Trim
        txtContact.Value = row.Cells(7).Text.Trim
        txtBranchID.Value = row.Cells(17).Text.Trim
        txtBranchName.Value = row.Cells(18).Text.Trim
        Dim mType As Integer = 0
        If row.Cells(12).Text = "LTC" Then
            mType = 0
        ElseIf row.Cells(12).Text = "PRN" Then
            mType = 1
        ElseIf row.Cells(12).Text = "SHP" Then
            mType = 2
        End If
        cbMemberType.SelectedIndex = mType
        txtMemberNoIDMT.Value = row.Cells(11).Text.Trim
        If row.Cells(16).Text = "Active" Then
            cbActive.Checked = True
        Else
            cbActive.Checked = False
        End If

        If row.Cells(8).Text = "1" Then
            divRoot.Visible = False
            divBranch.Visible = True
            divBranch2.Visible = False
        Else
            divRoot.Visible = True
            divBranch.Visible = False
            divBranch2.Visible = False
        End If

        txtSearchText.Text = gridMember.GridControlElement.SelectedRow.Cells(5).Text.ToString.Trim
        If gridMember.GridControlElement.SelectedRow.Cells(8).Text.ToString.Trim = "1" Then
            _tabMenu()
            menuL2.Attributes.Add("class", "active")
            Session.Add("tab", "2")
            FillData(txtSearchText.Text.Trim)
        ElseIf gridMember.GridControlElement.SelectedRow.Cells(8).Text.ToString.Trim = "2" Then
            _tabMenu()
            menuL3.Attributes.Add("class", "active")
            Session.Add("tab", "3")
            FillData(txtSearchText.Text.Trim)
        End If
    End Sub
    Protected Sub btnL1_ServerClick(sender As Object, e As EventArgs) Handles btnL1.ServerClick
        statusEdit.Disabled = True
        _clear()
        txtSearchText.Text = ""
        _tabMenu()
        menuL1.Attributes.Add("class", "active")
        Session.Add("tab", "1")
        FillData(txtSearchText.Text.Trim)
    End Sub
    Protected Sub btnL2_ServerClick(sender As Object, e As EventArgs) Handles btnL2.ServerClick
        statusEdit.Disabled = True
        _clear()
        txtSearchText.Text = ""
        _tabMenu()
        menuL2.Attributes.Add("class", "active")
        Session.Add("tab", "2")
        FillData(txtSearchText.Text.Trim)
    End Sub
    Protected Sub btnL3_ServerClick(sender As Object, e As EventArgs) Handles btnL3.ServerClick
        statusEdit.Disabled = True
        _clear()
        txtSearchText.Text = ""
        _tabMenu()
        menuL3.Attributes.Add("class", "active")
        Session.Add("tab", "3")
        FillData(txtSearchText.Text.Trim)
    End Sub
    Protected Sub btnAllLevel_ServerClick(sender As Object, e As EventArgs) Handles btnAllLevel.ServerClick
        statusEdit.Disabled = True
        _clear()
        txtSearchText.Text = ""
        _tabMenu()
        menuAllLevel.Attributes.Add("class", "active")
        Session.Add("tab", "0")
        FillData(txtSearchText.Text.Trim)
    End Sub
    Protected Sub btnExport_ServerClick(sender As Object, e As EventArgs) Handles btnExport.ServerClick
        If gridExport.GridControlElement.Rows.Count <> 0 Then
            Dim d = Today.Date.ToString("dd-MMM-yyyy")
            Dim filename As String = "mtopup_member_" & d & ".xlsx"
            Dim dt As New DataTable("GridView_Data")
            For Each cell As TableCell In gridExport.GridControlElement.HeaderRow.Cells
                dt.Columns.Add(cell.Text)
            Next
            For Each row As GridViewRow In gridExport.GridControlElement.Rows
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
    '////////MTOPUP PLUS
    Private Sub btnMainNew_ServerClick(sender As Object, e As EventArgs) Handles btnMainNew.ServerClick
        LoadMemberPlus("Init")
        statusEditPlus.Disabled = True
        panelOldMTopup.Visible = False
        panelNewMTopup.Visible = True
        btnSavePlus.Disabled = False
        _Alert()
    End Sub
    Private Sub btnMainOld_ServerClick(sender As Object, e As EventArgs) Handles btnMainOld.ServerClick
        panelOldMTopup.Visible = True
        panelNewMTopup.Visible = False
    End Sub
    Private Sub _getBranchPlus()
        Dim sql As String = ""
        If txtSearchBranch.Text <> "" Then
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where branch_id >= 19 and sts=1 and branch_name like '%{0}%' order by branch_id desc", txtSearchBranchPlus.Text.Trim)
        Else
            sql = String.Format("select branch_id,branch_name, sts" _
                              & " from mtopup.tbl_branch where branch_id >= 19 and sts=1 order by branch_id desc")
        End If
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "table")
            cn.Close()
            GridBranchPlus.RequestSource = ds.Tables("table")
            GridBranchPlus.GridControlElement.PageIndex = 0
            GridBranchPlus.DataBind()
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
    Private Sub Emptytxt()
        panelStampCode.Visible = False
        txtActivePlus.Checked = False
        txtAddressPlus.Value = ""
        txtbranchIDPlus.Value = ""
        txtbranchNamePlus.Value = ""
        txtContactPlus.Value = ""
        txtMemberID.Value = ""
        txtLevelPlus.Text = ""
        txtMsisdnPlus.Value = ""
        txtSearchPlus.Text = ""
        txtUsernamePlus.Value = ""
        txtIDPlus.Value = ""
    End Sub
    Private Sub LoadMemberPlus(ByVal Type As String)
        Dim conn As New OracleConnection
        Dim da As New OracleDataAdapter
        Dim ds As New DataSet
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim sql As String = ""
                If Type = "Search" Then
                    sql = String.Format("select m.member_id, m.telephone, mtopup.FN_CHECKING_CREDIT(m.telephone) as member_credit," _
                       & " m.member_contact_number, m.member_level, m.member_name," _
                       & " m.member_address, m.member_no_id, m.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) as root_number, " _
                       & " m.root_id, m.user_id, m.sts,m.brch_id,b.branch_name" _
                       & " from mtopup.tbl_member m,mtopup.tbl_branch b" _
                       & " where (m.telephone like '%{0}%' or m.member_level like '%{0}%' or m.member_name like '%{0}%' or " _
                       & " m.member_type Like '%{0}%' or b.branch_name like '%{0}%' or mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) like '%{0}%')" _
                       & " And m.brch_id=b.branch_id And m.member_type='MTP'" _
                       & " order by m.add_date desc", txtSearchPlus.Text.Trim)
                ElseIf Type = "Update" Then
                    sql = "select m.member_id, m.telephone, mtopup.FN_CHECKING_CREDIT(m.telephone) as member_credit," _
                        & " m.member_contact_number, m.member_level, m.member_name," _
                        & " m.member_address, m.member_no_id, m.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) as root_number, " _
                        & " m.root_id, m.user_id, m.sts,m.brch_id,b.branch_name" _
                        & " from mtopup.tbl_member m,mtopup.tbl_branch b" _
                        & " where m.brch_id=b.branch_id and m.member_type='MTP'" _
                        & " and m.member_level=3 and m.telephone='" & txtMsisdnPlus.Value.Trim & "' order by m.status_date desc"
                ElseIf Type = "Save" Then
                    sql = "select m.member_id, m.telephone, mtopup.FN_CHECKING_CREDIT(m.telephone) as member_credit," _
                        & " m.member_contact_number, m.member_level, m.member_name," _
                        & " m.member_address, m.member_no_id, m.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) as root_number, " _
                        & " m.root_id, m.user_id, m.sts,m.brch_id,b.branch_name" _
                        & " from mtopup.tbl_member m,mtopup.tbl_branch b" _
                        & " where m.brch_id=b.branch_id and m.member_type='MTP'" _
                        & " and m.member_level=3 and user_id='" & Session("userid").ToString.Trim & "' order by m.add_date desc"
                Else
                    sql = "select m.member_id, m.telephone, mtopup.FN_CHECKING_CREDIT(m.telephone) as member_credit," _
                       & " m.member_contact_number, m.member_level, m.member_name," _
                       & " m.member_address, m.member_no_id, m.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) as root_number, " _
                       & " m.root_id, m.user_id, m.sts,m.brch_id,b.branch_name" _
                       & " from mtopup.tbl_member m,mtopup.tbl_branch b" _
                       & " where m.brch_id=b.branch_id and m.member_type='MTP'" _
                       & " and m.member_level=3 and rownum <= 100 order by m.add_date desc"
                End If
                da = New OracleDataAdapter(sql, conn)
                da.Fill(ds, "table")
                GridMemberPlus.RequestSource = ds.Tables("table")
                GridMemberPlus.DataBind()
                _gridColumnName()
                panelOldMTopup.Visible = False
                panelNewMTopup.Visible = True
            End With
        Catch ex As Exception
        Finally
            conn.Close()
            da.Dispose()
        End Try
    End Sub

    Private Sub btnGroupPlus_ServerClick(sender As Object, e As EventArgs) Handles btnGroupPlus.ServerClick
        txtSearchBranchPlus.Text = ""
        _getBranchPlus()
        _Alert()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSelectBranchPlus();", True)
    End Sub
    Private Sub GridBranchPlus_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles GridBranchPlus.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(5).Text = "ສາຂາ"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ເລືອກ"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(6).Visible = False
        End If
    End Sub
    Private Sub GridBranchPlus_GridRowClick(sender As Object, e As EventArgs) Handles GridBranchPlus.GridRowClick
        txtbranchIDPlus.Value = GridBranchPlus.GridControlElement.SelectedRow.Cells(4).Text
        txtbranchNamePlus.Value = GridBranchPlus.GridControlElement.SelectedRow.Cells(5).Text
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "closeModalBranchPlus();", True)
    End Sub
    Private Sub GridMemberPlus_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles GridMemberPlus.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(17).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(5).Font.Bold = True

            'kuadva tha man level 1 man br hai show tha br man super admin
            If Session("userlevel").ToString <> "9" Then
                If e.Row.Cells(8).Text = "1" Then
                    e.Row.Visible = False
                End If
            End If

            e.Row.Cells(18).ForeColor = Drawing.Color.CornflowerBlue
            e.Row.Cells(18).Font.Bold = True
            If e.Row.Cells(16).Text.ToString = "1" Then
                e.Row.Cells(16).Text = "Active"
                e.Row.Cells(16).ForeColor = Drawing.Color.Green
                e.Row.Cells(16).Font.Bold = True
            ElseIf e.Row.Cells(16).Text.ToString = "0" Then
                e.Row.Cells(16).Text = "Waiting Approve"
                e.Row.Cells(16).ForeColor = Drawing.Color.Orange
                e.Row.Cells(16).Font.Bold = True
            Else
                e.Row.Cells(16).Text = "Deactivated"
                e.Row.Cells(16).ForeColor = Drawing.Color.Red
                e.Row.Cells(16).Font.Bold = True
            End If

            e.Row.Cells(6).Font.Bold = True
            e.Row.Cells(6).ForeColor = Drawing.Color.OrangeRed
            If CDbl(e.Row.Cells(6).Text) > 0 Then
                e.Row.Cells(6).Text = CDbl(e.Row.Cells(6).Text).ToString("#,##0")
            End If
        End If
    End Sub
    Private Sub GridMemberPlus_GridRowClick(sender As Object, e As EventArgs) Handles GridMemberPlus.GridRowClick
        _Alert()
        panelOldMTopup.Visible = False
        panelNewMTopup.Visible = True
        statusEditPlus.Disabled = False
        Dim row = GridMemberPlus.GridControlElement.SelectedRow
        txtMemberID.Value = row.Cells(4).Text.Trim
        txtMsisdnPlus.Value = row.Cells(5).Text.Trim
        txtContactPlus.Value = row.Cells(7).Text.Trim
        txtLevelPlus.SelectedValue = row.Cells(8).Text.Trim
        txtUsernamePlus.Value = row.Cells(9).Text.Trim
        txtAddressPlus.Value = row.Cells(10).Text.Trim
        txtIDPlus.Value = row.Cells(11).Text.Trim
        If row.Cells(16).Text.ToString = "Waiting Approve" Then
            txtActivePlus.Checked = True
            panelStampCode.Visible = True
        ElseIf row.Cells(16).Text.ToString = "Active" Then
            panelStampCode.Visible = False
            txtActivePlus.Checked = True
        Else
            txtActivePlus.Checked = False
            panelStampCode.Visible = False
        End If
        txtbranchIDPlus.Value = row.Cells(17).Text.Trim
        txtbranchNamePlus.Value = row.Cells(18).Text.Trim
        btnSavePlus.Disabled = True
        btnUpdatePlus.Disabled = False
    End Sub
    Private Sub GridMemberPlus_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles GridMemberPlus.GridViewPageIndexChanged
        Dim sql As String = ""
        Dim userLevel As String = Session("userlevel").ToString.Trim
        Dim brchID As String = Session("userbranch").ToString.Trim
        sql = "select m.member_id, m.telephone, mtopup.FN_CHECKING_CREDIT(m.telephone) as member_credit," _
                       & " m.member_contact_number, m.member_level, m.member_name," _
                       & " m.member_address, m.member_no_id, m.member_type, mtopup.FN_GET_TELEPHONE_BY_MEMBER(m.root_id) as root_number, " _
                       & " m.root_id, m.user_id, m.sts,m.brch_id,b.branch_name" _
                       & " from mtopup.tbl_member m,mtopup.tbl_branch b" _
                       & " where m.brch_id=b.branch_id and m.member_type='MTP'" _
                       & " and m.member_level=3 and rownum <= 100 order by m.add_date desc"
        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "member")
            cn.Close()
            GridMemberPlus.RequestSource = ds.Tables("member")
            GridMemberPlus.GridControlElement.PageIndex = e.NewPageIndex
            GridMemberPlus.DataBind()
        Catch ex As Exception
        Finally
            cn.Close()
            cn.Dispose()

            da.Dispose()
            ds.Dispose()
        End Try
        _gridColumnName()
        panelOldMTopup.Visible = False
        panelNewMTopup.Visible = True
    End Sub
    Private Sub btnSearchPlus_ServerClick(sender As Object, e As EventArgs) Handles btnSearchPlus.ServerClick
        LoadMemberPlus("Search")
    End Sub
    Private Sub txtSearchPlus_TextChanged(sender As Object, e As EventArgs) Handles txtSearchPlus.TextChanged
        LoadMemberPlus("Search")
    End Sub
    Private Sub btnSavePlus_ServerClick(sender As Object, e As EventArgs) Handles btnSavePlus.ServerClick
        panelNewMTopup.Visible = True
        panelOldMTopup.Visible = False
        If panelStampCode.Visible = True And txtstampcode.Value = "" Then
            AlertStampCodeEmpty.Visible = True
            Exit Sub
        Else
            AlertStampCodeEmpty.Visible = False
        End If
        If txtMsisdnPlus.Value = "" Or txtAddressPlus.Value = "" Or txtContactPlus.Value = "" Or txtUsernamePlus.Value = "" Or txtLevelPlus.Text = "" Then
            AlertEmptytxt.Visible = True
        Else
            lbTelPlus.Text = txtMsisdnPlus.Value.Trim
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalSavePlus();", True)
        End If
    End Sub
    Private Sub btnRefreshPlus_ServerClick(sender As Object, e As EventArgs) Handles btnRefreshPlus.ServerClick
        panelOldMTopup.Visible = False
        panelNewMTopup.Visible = True
        LoadMemberPlus("Init")
        Emptytxt()
        btnSavePlus.Disabled = False
    End Sub
    Private Function CheckStampCode(ByVal StampCode As String, ByVal Msisdn As String) As Boolean
        Dim result As Boolean = False
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim dr As OracleDataReader
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = conDB.getConnectionString()
                .Open()
                Dim sql As String = "select stampcode from tbl_stampcode where msisdn='" & Msisdn.Trim & "' and stampcode='" & StampCode.Trim & "' and status='regi'"
                cmd = New OracleCommand(sql, conn)
                dr = cmd.ExecuteReader()
                If dr.Read Then
                    result = True
                Else
                    result = False
                End If
            End With
        Catch ex As Exception
            result = False
        End Try
        Return result
    End Function

    Private Sub btnConfirmSavePlus_ServerClick(sender As Object, e As EventArgs) Handles btnConfirmSavePlus.ServerClick
        cn = New OracleConnection
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            Dim sts As Integer = 0
            If txtActivePlus.Checked = True Then
                sts = 1
            Else
                sts = 2
            End If
            Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertMember, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = txtMsisdnPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_LEVEL", OracleDbType.Int32).Value = CInt(txtLevelPlus.SelectedValue)
            cm.Parameters.Add("P_STS", OracleDbType.Int32).Value = sts
            cm.Parameters.Add("P_MEMBER_NAME", OracleDbType.NVarchar2, 50).Value = txtUsernamePlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_ADDRESS", OracleDbType.NVarchar2, 30).Value = txtAddressPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_NO_ID", OracleDbType.NVarchar2, 50).Value = txtIDPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_CONTACT_NUMBER", OracleDbType.NVarchar2, 50).Value = txtContactPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_TYPE", OracleDbType.NVarchar2, 3).Value = "MTP"
            cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString
            cm.Parameters.Add("P_ROOT_ID", OracleDbType.Int64).Value = 0
            cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = CDbl(txtbranchIDPlus.Value)
            Dim P_MEMBER_ID As New OracleParameter("P_MEMBER_ID", OracleDbType.Int64) With {.Direction = ParameterDirection.Output}
            cm.Parameters.Add(P_MEMBER_ID)
            cm.ExecuteNonQuery()
            '///////// SaveLog  ////////////
            fc.saveLog(Session("userid").ToString, "Add new member - [id:" & P_MEMBER_ID.Value.ToString & "] [name:" & txtUsernamePlus.Value.ToString & "] [level:" & txtLevelPlus.SelectedValue & "] [telephone:" & txtMsisdnPlus.Value.ToString & "] [status:" & sts & "]")
            _Alert()
            lbTel.Text = txtMsisdnPlus.Value.Trim
            AlertSaveSuccessPlus.Visible = True
            Emptytxt()
            LoadMemberPlus("Save")
        Catch ex As Exception
            AlertSaveFailPlus.Visible = True
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
    End Sub
    Private Sub btnUpdatePlus_ServerClick(sender As Object, e As EventArgs) Handles btnUpdatePlus.ServerClick
        panelNewMTopup.Visible = True
        panelOldMTopup.Visible = False
        If panelStampCode.Visible = True And txtstampcode.Value = "" Then
            AlertStampCodeEmpty.Visible = True
            Exit Sub
        Else
            AlertStampCodeEmpty.Visible = False
        End If
        If txtMsisdnPlus.Value = "" Or txtAddressPlus.Value = "" Or txtContactPlus.Value = "" Or txtUsernamePlus.Value = "" Or txtLevelPlus.Text = "" Then
            AlertEmptytxt.Visible = True
        Else
            lbTelUpdate.Text = txtMsisdnPlus.Value.Trim
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "modalUpdateMemberPlus();", True)
        End If
    End Sub
    Private Sub btnConfirmUpdatePlus_ServerClick(sender As Object, e As EventArgs) Handles btnConfirmUpdatePlus.ServerClick
        cn = New OracleConnection
        If txtstampcode.Value <> "" Then
            AlertStampCodeEmpty.Visible = False
            If CheckStampCode(txtstampcode.Value.Trim, txtMsisdnPlus.Value.Trim) = False Then
                panelNewMTopup.Visible = True
                panelOldMTopup.Visible = False
                AlertSampCode.Visible = True
                Exit Sub
            End If
        End If
        Dim _branchID As Integer = 0
        If txtAddressPlus.Value.Length = 3 Then
            _branchID = fc.GetBranchID(txtAddressPlus.Value)
        Else
            _branchID = CInt(txtBranchID.Value.Trim)
        End If
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            Dim sts As Integer = 0
            If txtActivePlus.Checked = True Then
                sts = 1
            Else
                sts = 2
            End If
            Dim _username As String = Session("userid").ToString
            cm = New OracleCommand(myDataType.sqlUpdateMember, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_member_id", OracleDbType.Int64).Value = CDbl(txtMemberID.Value.Trim)
            cm.Parameters.Add("P_TELEPHONE", OracleDbType.NVarchar2, 10).Value = txtMsisdnPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_LEVEL", OracleDbType.Int64).Value = CInt(txtLevelPlus.SelectedValue)
            cm.Parameters.Add("P_STS", OracleDbType.Int32).Value = sts
            cm.Parameters.Add("P_MEMBER_NAME", OracleDbType.NVarchar2, 30).Value = txtUsernamePlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_ADDRESS", OracleDbType.NVarchar2, 50).Value = txtAddressPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_NO_ID", OracleDbType.NVarchar2, 30).Value = txtIDPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_CONTACT_NUMBER", OracleDbType.NVarchar2, 50).Value = txtContactPlus.Value.Trim
            cm.Parameters.Add("P_MEMBER_TYPE", OracleDbType.NVarchar2, 3).Value = "MTP"
            cm.Parameters.Add("P_USER_ID", OracleDbType.NVarchar2, 20).Value = _username.Trim
            cm.Parameters.Add("P_ROOT_ID", OracleDbType.Int64).Value = 0
            cm.Parameters.Add("P_BRANCH_ID", OracleDbType.Int64).Value = _branchID
            cm.ExecuteNonQuery()
            '///////// SaveLog  ////////////
            fc.UpdateStampCode(txtMsisdnPlus.Value.Trim)
            fc.saveLog(Session("userid").ToString, "Edit member - [id:" & txtMemberID.Value.ToString & "] [name:" & txtUsernamePlus.Value.ToString & "] [level:" & txtLevelPlus.SelectedValue & "] [telephone:" & txtMsisdnPlus.Value.ToString & "] [status:" & sts & "]")
            _Alert()
            panelNewMTopup.Visible = True
            panelOldMTopup.Visible = False
            AlertSampCode.Visible = False
            lbsavesucessplus.Text = txtMsisdnPlus.Value.Trim
            AlertSaveSuccessPlus.Visible = True
            LoadMemberPlus("Update")
            Emptytxt()
        Catch ex As Exception
            _Alert()
            panelNewMTopup.Visible = True
            panelOldMTopup.Visible = False
            AlertSampCode.Visible = False
            lbsavefailplus.Text = txtMsisdnPlus.Value.Trim
            AlertSaveFailPlus.Visible = True
            Emptytxt()
        Finally
            cn.Close()
            cn.Dispose()
            cm.Dispose()
        End Try
    End Sub


End Class