Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.Web.Configuration
Imports nickDevClass
Partial Class promotion
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
                    menuRechargeHistory.Visible = True
                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Default.aspx")
        End Try

        If Not IsPostBack Then
            _Alert()
            _Panel()
            dtStart.DateValue = Today.Date
            dtStop.DateValue = Today.Date
            p1.Visible = True
            btnAddNewPro.Visible = True
            FillData()
        End If
        _gridColumnName()
    End Sub

    Sub _menu()
        menuMember.Visible = False
        menuRecharge.Visible = False
        menuReceive.Visible = False
        menuReprint.Visible = False
        menuReceiveHistory.Visible = True
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
        If gridDiscount.GridControlElement.Rows.Count <> 0 Then
            gridDiscount.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridDiscount.GridControlElement.HeaderRow.Cells(4).Text = "ປະເພດເບີ"
            gridDiscount.GridControlElement.HeaderRow.Cells(5).Text = "ເງິນເລີ່ມຕົ້ນ"
            gridDiscount.GridControlElement.HeaderRow.Cells(6).Text = "ເງິນສິ້ນສຸດ"
            gridDiscount.GridControlElement.HeaderRow.Cells(7).Text = "ໄດ້ມູນຄ່າໂທ %"
            gridDiscount.GridControlElement.HeaderRow.Cells(8).Text = "ໄດ້ໂບນັດ %"
            gridDiscount.GridControlElement.HeaderRow.Cells(9).Text = "ໄດ້ດາຕ້າ %"
            gridDiscount.GridControlElement.HeaderRow.Cells(10).Text = "ເລີ່ມໃຫ້"
            gridDiscount.GridControlElement.HeaderRow.Cells(11).Text = "ຢຸດໃຫ້"
            gridDiscount.GridControlElement.HeaderRow.Cells(12).Text = "ສະຖານະ"
            gridDiscount.GridControlElement.HeaderRow.Cells(13).Text = "ຈັດການຂໍ້ມູນວັນທີ"
            gridDiscount.GridControlElement.HeaderRow.Cells(14).Text = "ຈັດການຂໍ້ມູນໂດຍ"
        End If

    End Sub
    Private Sub FillData()
        Dim sql As String = ""
        
        sql = "select  p.product_type, p.credit_start, p.credit_stop, p.balance, p.bonus, p.data, p.start_date, p.stop_date, p.status, p.status_date, u.user_name " _
            & " from mtopup.tbl_promotion p, mtopup.tbl_userctrl u where p.user_id = u.user_id order by p.status_date asc"

        cn = New OracleConnection
        Dim da As OracleDataAdapter = New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "promotion")
            cn.Close()
            gridDiscount.RequestSource = ds.Tables("promotion")
            gridDiscount.DataBind()
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

    Sub _Alert()
        alertError.Visible = False
        alertSuccess.Visible = False
        alertWarning.Visible = False
    End Sub

    Sub _Panel()
        p1.Visible = False
        p2.Visible = False
        btnAddNewPro.Visible = False
        btnBack.Visible = False
        btnSave.Visible = False
        btnDelete.Visible = False
        txtProductType.Disabled = False
    End Sub

    Sub _clearText()
        txtProductType.Value = ""
        txtCreditStart.Value = ""
        txtCreditStop.Value = ""

    End Sub

    Protected Sub gridDiscount_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridDiscount.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(13).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ແກ້ໄຂ"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(5).Text = CDbl(e.Row.Cells(5).Text.Trim.ToString).ToString("###,###,###,###,###")
            e.Row.Cells(6).Text = CDbl(e.Row.Cells(6).Text.Trim.ToString).ToString("###,###,###,###,###")
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(5).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(6).Font.Bold = True
            e.Row.Cells(6).ForeColor = Drawing.Color.OrangeRed
            e.Row.Cells(7).Font.Bold = True
            e.Row.Cells(7).ForeColor = Drawing.Color.CornflowerBlue
            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(8).ForeColor = Drawing.Color.CornflowerBlue
            e.Row.Cells(9).Font.Bold = True
            e.Row.Cells(9).ForeColor = Drawing.Color.CornflowerBlue


            If e.Row.Cells(12).Text.ToString.Trim = "O" Then
                e.Row.Cells(12).Text = "On"
                e.Row.Cells(12).ForeColor = Drawing.Color.Green
                e.Row.Cells(12).Font.Bold = True
            Else
                e.Row.Cells(12).Text = "Off"
                e.Row.Cells(12).ForeColor = Drawing.Color.Red
                e.Row.Cells(12).Font.Bold = True
            End If
        End If
    End Sub

    Protected Sub gridDiscount_GridRowClick(sender As Object, e As EventArgs) Handles gridDiscount.GridRowClick
        _Alert()
        _Panel()
        p2.Visible = True
        btnBack.Visible = True
        btnSave.Visible = True
        btnDelete.Visible = True
        txtProductType.Disabled = True

        Try
            Dim productType As String = gridDiscount.GridControlElement.SelectedRow.Cells(4).Text.ToString.Trim
            Dim creditStart As String = gridDiscount.GridControlElement.SelectedRow.Cells(5).Text.ToString.Trim
            Dim creditStop As String = gridDiscount.GridControlElement.SelectedRow.Cells(6).Text.ToString.Trim
            Dim percentBalance As String = gridDiscount.GridControlElement.SelectedRow.Cells(7).Text.ToString.Trim
            Dim percentBonus As String = gridDiscount.GridControlElement.SelectedRow.Cells(8).Text.ToString.Trim
            Dim percentData As String = gridDiscount.GridControlElement.SelectedRow.Cells(9).Text.ToString.Trim
            Dim startDate As Date = gridDiscount.GridControlElement.SelectedRow.Cells(10).Text.ToString.Trim
            Dim endDate As Date = gridDiscount.GridControlElement.SelectedRow.Cells(11).Text.ToString.Trim

            txtProductType.Value = productType
            txtCreditStart.Value = creditStart
            txtCreditStop.Value = creditStop
            txtPercentBalance.Value = percentBalance
            txtPercentBonus.Value = percentBonus
            txtPercentData.Value = percentData
            dtStart.DateValue = startDate
            dtStop.DateValue = endDate
        Catch ex As Exception
            lbAlertError.Text = "ລະບົບມີບັນຫາ, ຕິດຕໍ່ທີມພັດທະນາ."
            alertError.Visible = True
        End Try
    End Sub

    Protected Sub btnAddNewPro_ServerClick(sender As Object, e As EventArgs) Handles btnAddNewPro.ServerClick
        _Panel()
        p2.Visible = True
        btnBack.Visible = True
        btnSave.Visible = True
        txtProductType.Value = ""
        txtCreditStart.Value = ""
        txtCreditStop.Value = ""
        txtPercentBalance.Value = ""
        txtPercentBonus.Value = ""
        txtPercentData.Value = ""
        dtStart.DateValue = Today.Date
        dtStop.DateValue = Today.Date
    End Sub

    Protected Sub btnBack_ServerClick(sender As Object, e As EventArgs) Handles btnBack.ServerClick
        _Panel()
        p1.Visible = True
        btnAddNewPro.Visible = True
        FillData()
    End Sub

    Protected Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        _Alert()
        If txtProductType.Value.ToString = "" Then
            _Alert()
            lbAlertWarning.Text = "ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ."
            alertWarning.Visible = True
            txtProductType.Focus()
            Exit Sub
        End If
        If txtCreditStart.Value.ToString = "" Then
            _Alert()
            lbAlertWarning.Text = "ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ."
            alertWarning.Visible = True
            txtCreditStart.Focus()
            Exit Sub
        End If
        If txtCreditStop.Value.ToString = "" Then
            _Alert()
            lbAlertWarning.Text = "ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ."
            alertWarning.Visible = True
            txtCreditStop.Focus()
            Exit Sub
        End If
        If txtCreditStop.Value.ToString = "" Then
            _Alert()
            lbAlertWarning.Text = "ກະລຸນາປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ."
            alertWarning.Visible = True
            txtCreditStop.Focus()
            Exit Sub
        End If
        lbItemSave.Text = txtProductType.Value.ToString.ToUpper.Trim
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
    End Sub

    Protected Sub btnDelete_ServerClick(sender As Object, e As EventArgs) Handles btnDelete.ServerClick
        lbItemDel.Text = txtProductType.Value.ToString.Trim
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalDelete();", True)
    End Sub

    Protected Sub btAlertError_ServerClick(sender As Object, e As EventArgs) Handles btAlertError.ServerClick
        _Alert()
    End Sub

    Protected Sub btAlertSuccess_ServerClick(sender As Object, e As EventArgs) Handles btAlertSuccess.ServerClick
        _Alert()
    End Sub

    Protected Sub btAlertWarning_ServerClick(sender As Object, e As EventArgs) Handles btAlertWarning.ServerClick
        _Alert()
    End Sub

    Protected Sub btConfirmSave_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave.ServerClick
        If btnDelete.Visible = False Then
            If _addNewPro() = True Then
                _Alert()
                alertSuccess.Visible = True
                _Panel()
                p1.Visible = True
                btnAddNewPro.Visible = True
                FillData()
            Else
                _Alert()
                alertError.Visible = True
            End If
        Else
            If _updatePro() = True Then
                _Alert()
                alertSuccess.Visible = True
                _Panel()
                p1.Visible = True
                btnAddNewPro.Visible = True
                FillData()
            Else
                _Alert()
                alertError.Visible = True
            End If
        End If
        
    End Sub

    Protected Sub btConfirmDelete_ServerClick(sender As Object, e As EventArgs) Handles btConfirmDelete.ServerClick
        If _disablePro() = True Then
            _Alert()
            alertSuccess.Visible = True
            _Panel()
            p1.Visible = True
            btnAddNewPro.Visible = True
            FillData()
        Else
            _Alert()
            alertError.Visible = True
        End If
    End Sub

    Public Function _addNewPro() As Boolean
        Dim _return As Boolean = False

        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດເຊື່ມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ"
        End Try

        Try
            Dim d1 As String = dtStart.DateValue.ToString("dd-MMM-yyyy")
            Dim d2 As String = dtStop.DateValue.ToString("dd-MMM-yyyy")
            Dim d As String = Today.Date.ToString("dd-MMM-yyyy")

            Dim sql As String = "insert into mtopup.tbl_promotion (product_type,credit_start,credit_stop,balance,bonus,data,start_date,stop_date,status,status_date,user_id) values(?,?,?,?,?,?,?,?,?,?,?)"
            cm = New OracleCommand(sql, cn)
            cm.Parameters.Add(":product_type", OracleDbType.NVarchar2, 20).Value = txtProductType.Value.ToString.ToUpper.Trim
            cm.Parameters.Add(":credit_start", OracleDbType.Double).Value = CDbl(txtCreditStart.Value.ToString.Replace(",", "").Trim)
            cm.Parameters.Add(":credit_stop", OracleDbType.Double).Value = CDbl(txtCreditStop.Value.ToString.Replace(",", "").Trim)
            cm.Parameters.Add(":balance", OracleDbType.Double).Value = CDbl(txtPercentBalance.Value.ToString.Trim)
            cm.Parameters.Add(":bonus", OracleDbType.Double).Value = CDbl(txtPercentBonus.Value.ToString.Trim)
            cm.Parameters.Add(":data", OracleDbType.Double).Value = CDbl(txtPercentData.Value.ToString.Trim)
            cm.Parameters.Add(":start_date", DbType.Date).Value = d1
            cm.Parameters.Add(":stop_date", DbType.Date).Value = d2
            cm.Parameters.Add(":status", OracleDbType.NVarchar2).Value = "O"
            cm.Parameters.Add(":status_date", OracleDbType.Date).Value = d
            cm.Parameters.Add(":user_id", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString
            cm.ExecuteNonQuery()
            _Alert()
            lbAlertSuccess.Text = "ເພີ່ມຂໍ້ມູນໂປຣໂມເຊິນຂອງ " & txtProductType.Value.ToString.ToUpper.Trim & " ສຳເລັດ."
            fc.saveLog(Session("userid").ToString, "Add new promotion - [productType:" & txtProductType.Value.ToString.ToUpper.Trim & "] [start:" & txtCreditStart.Value.Trim & "] [stop:" & txtCreditStop.Value.Trim & "] [percentBalance:" & txtPercentBalance.Value.ToString.Trim & "] [percentBonus: " & txtPercentBonus.Value.ToString.Trim & "] [ percentData: " & txtPercentData.Value.ToString.Trim & "]")
            _return = True
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດເພີ່ມຂໍ້ມູນໄດ້, ກະລຸນາກວດສອບຂໍ້ມູນທີ່ທ່ານປ້ອນ."
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
        End Try
        Return _return
    End Function


    Public Function _updatePro() As Boolean
        Dim _return As Boolean = False

        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດເຊື່ມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ"
        End Try

        Try
            Dim d1 As String = dtStart.DateValue.ToString("dd-MMM-yyyy")
            Dim d2 As String = dtStop.DateValue.ToString("dd-MMM-yyyy")
            Dim d As String = Today.Date.ToString("dd-MMM-yyyy")

            Dim sql As String = "update mtopup.tbl_promotion set credit_start=?,credit_stop=?,balance=?,bonus=?,data=?,start_date=?,stop_date=?,status=?,status_date=?,user_id=? where product_type=?"
            cm = New OracleCommand(sql, cn)
            cm.Parameters.Add(":credit_start", OracleDbType.Double).Value = CDbl(txtCreditStart.Value.ToString.Replace(",", "").Trim)
            cm.Parameters.Add(":credit_stop", OracleDbType.Double).Value = CDbl(txtCreditStop.Value.ToString.Replace(",", "").Trim)
            cm.Parameters.Add(":balance", OracleDbType.Double).Value = CDbl(txtPercentBalance.Value.ToString.Trim)
            cm.Parameters.Add(":bonus", OracleDbType.Double).Value = CDbl(txtPercentBonus.Value.ToString.Trim)
            cm.Parameters.Add(":data", OracleDbType.Double).Value = CDbl(txtPercentData.Value.ToString.Trim)
            cm.Parameters.Add(":start_date", OracleDbType.Date).Value = d1
            cm.Parameters.Add(":stop_date", OracleDbType.Date).Value = d2
            cm.Parameters.Add(":status", OracleDbType.NVarchar2).Value = "O"
            cm.Parameters.Add(":status_date", OracleDbType.Date).Value = d
            cm.Parameters.Add(":user_id", OracleDbType.NVarchar2).Value = Session("userid").ToString
            cm.Parameters.Add(":product_type", OracleDbType.NVarchar2).Value = txtProductType.Value.ToString.ToUpper.Trim
            cm.ExecuteNonQuery()
            _Alert()
            lbAlertSuccess.Text = "ແກ້ໄຂຂໍ້ມູນໂປຣໂມເຊິນຂອງ " & txtProductType.Value.ToString.ToUpper.Trim & " ສຳເລັດ."
            fc.saveLog(Session("userid").ToString, "Update promotion - [productType:" & txtProductType.Value.ToString.ToUpper.Trim & "] [start:" & txtCreditStart.Value.Trim & "] [stop:" & txtCreditStop.Value.Trim & "] [percentBalance:" & txtPercentBalance.Value.ToString.Trim & "] [percentBonus: " & txtPercentBonus.Value.ToString.Trim & "] [ percentData: " & txtPercentData.Value.ToString.Trim & "]")
            _return = True
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດແກ້ໄຂຂໍ້ມູນໄດ້, ກະລຸນາກວດສອບຂໍ້ມູນທີ່ທ່ານປ້ອນ."
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
        End Try
        Return _return
    End Function

    Public Function _disablePro() As Boolean
        Dim _return As Boolean = False

        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດເຊື່ມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ"
        End Try

        Try
            Dim d As String = Today.Date.ToString("dd-MMM-yyyy")

            Dim sql As String = "update mtopup.tbl_promotion set status=?,status_date=?,user_id=? where product_type=?"
            cm = New OracleCommand(sql, cn)
            cm.Parameters.Add(":status", OracleDbType.NVarchar2).Value = "F"
            cm.Parameters.Add(":status_date", OracleDbType.Date).Value = d
            cm.Parameters.Add(":user_id", OracleDbType.NVarchar2).Value = Session("userid").ToString
            cm.Parameters.Add(":product_type", OracleDbType.NVarchar2).Value = txtProductType.Value.ToString.ToUpper.Trim
            cm.ExecuteNonQuery()
            _Alert()
            lbAlertSuccess.Text = "ປິດໂປຣໂມເຊິນຂອງ " & txtProductType.Value.ToString.ToUpper.Trim & " ສຳເລັດ."
            fc.saveLog(Session("userid").ToString, "Disable promotion - [productType:" & txtProductType.Value.ToString.ToUpper.Trim & "]")
            _return = True
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດປິດໂປຣໂມເຊິນ " & txtProductType.Value.ToString.ToUpper.Trim & " ໄດ້, ຕິດຕໍ່ທີມພັດທະນາ."
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
        End Try
        Return _return
    End Function

   
End Class
