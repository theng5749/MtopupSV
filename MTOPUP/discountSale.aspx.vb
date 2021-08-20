Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports System.Web.Configuration
Imports nickDevClass
Imports System.Globalization

Partial Class discountSale
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
            gridDiscount.GridControlElement.HeaderRow.Cells(4).Text = "ເງິນເລີ່ມຕົ້ນ"
            gridDiscount.GridControlElement.HeaderRow.Cells(5).Text = "ເງິນສິ້ນສຸດ"
            gridDiscount.GridControlElement.HeaderRow.Cells(6).Text = "ເປີເຊັນໂບນັດ (%)"
            gridDiscount.GridControlElement.HeaderRow.Cells(9).Text = "ບັນທຶກຂໍ້ມູນໂດຍ"
        End If

    End Sub
    Private Sub FillData()
        Dim sql As String = ""
        sql = "select  * " _
            & " from mtopup.tbl_bonus order by START_AMOUNT asc"
        cn = New OracleConnection
        Dim da As New OracleDataAdapter(sql, cn)
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da.Fill(ds, "bonus")
            cn.Close()
            gridDiscount.RequestSource = ds.Tables("bonus")
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
        alertInfo.Visible = False
        alertSaveSuccess.Visible = False
        alertSaveError.Visible = False
    End Sub
    Protected Sub btnAddDiscount_ServerClick(sender As Object, e As EventArgs) Handles btnAddDiscount.ServerClick
        _Alert()
        txtCreditPercent.Value = ""
        txtCreditStart.Value = ""
        txtCreditStop.Value = ""
        txtCreditStart.Focus()
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "myModalSave();", True)
    End Sub

    Protected Sub btConfirmSave2_ServerClick(sender As Object, e As EventArgs) Handles btConfirmSave2.ServerClick
        _Alert()
        If txtCreditStart.Value.Trim = "" Then
            _Alert()
            lbAlertInfo.Text = "ກະລຸນາປ້ອນເງິນເລີ່ມຕົ້ນ"
            alertInfo.Visible = True
            txtCreditStart.Focus()
            Exit Sub
        End If
        If txtCreditStop.Value.Trim = "" Then
            _Alert()
            lbAlertInfo.Text = "ກະລຸນາປ້ອນເງິນສິ້ນສຸດ"
            alertInfo.Visible = True
            txtCreditStop.Focus()
            Exit Sub
        End If
        If txtCreditPercent.Value.Trim = "" Then
            _Alert()
            lbAlertInfo.Text = "ກະລຸນາປ້ອນເປີເຊັນໂບນັດ"
            alertInfo.Visible = True
            txtCreditPercent.Focus()
            Exit Sub
        End If

        Try
            Dim a = CDbl(txtCreditPercent.Value.Trim).ToString("###,###,###.##")
        Catch ex As Exception
            _Alert()
            lbAlertInfo.Text = "ເປີເຊັນໂບນັດບໍ່ຖືກຕ້ອງ, ກວດສອບຄືນໃໝ່"
            alertInfo.Visible = True
            txtCreditPercent.Focus()
            Exit Sub
        End Try

        If CDbl(txtCreditStart.Value.Trim.Replace(",", "")) < 0 Or CDbl(txtCreditStop.Value.Trim.Replace(",", "")) < 0 Then
            _Alert()
            lbAlertInfo.Text = "ຂໍ້ມູນບໍ່ຖືກຕ້ອງ, ກະລຸນາກວດສອບຄືນໃໝ່"
            alertInfo.Visible = True
            Exit Sub
        End If

        Dim mStart As Double
        Dim mStop As Double
        mStart = CDbl(txtCreditStart.Value.Trim.Replace(",", ""))
        mStop = CDbl(txtCreditStop.Value.Trim.Replace(",", ""))
        If mStart > mStop Then
            _Alert()
            lbAlertInfo.Text = "ຂໍ້ມູນບໍ່ຖືກຕ້ອງ, ຈຳນວນເງິນເລີ່ມຕົ້ນຫຼາຍກວ່າຈຳນວນເງິນສິ້ນສຸດ ກະລຸນາກວດສອບຄືນໃໝ່"
            alertInfo.Visible = True
            Exit Sub
        End If

        If _addDiscount() = True Then
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "Script", "CloseMyModalSave();", True)
            FillData()
            _Alert()
            alertSaveSuccess.Visible = True
        Else
            _Alert()
            alertError.Visible = True
            Exit Sub
        End If
    End Sub

    Public Function _addDiscount() As Boolean
        Dim _return As Boolean = False

        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດເຊື່ມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ"
        End Try

        Try
            Dim cm As OracleCommand = New OracleCommand(myDataType.sqlInsertBonus, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_start_amt", OracleDbType.Int32).Value = CDbl(txtCreditStart.Value.Trim.Replace(",", ""))
            cm.Parameters.Add("p_stop_amt", OracleDbType.Int32).Value = CDbl(txtCreditStop.Value.Trim.Replace(",", ""))
            cm.Parameters.Add("p_bonus_percent", OracleDbType.Int32).Value = CDbl(txtCreditPercent.Value.Trim.Replace(",", ""))
            cm.Parameters.Add("p_user_id", OracleDbType.NVarchar2, 20).Value = Session("userid").ToString
            cm.ExecuteNonQuery()
            lbSaveSuccess.Text = "ເພີ່ມຂໍ້ມູນສ່ວນຫຼຸດສຳເລັດ"
            fc.saveLog(Session("userid").ToString, "Add new bonus - [start:" & txtCreditStart.Value.Trim & "] [stop:" & txtCreditStop.Value.Trim & "] [percent:" & txtCreditPercent.Value.Trim & "]")
            _return = True
        Catch ex As Exception
            _Alert()
            lbAlertError.Text = "ບໍ່ສາມາດເພີ່ມຂໍ້ມູນໄດ້, ຕິດຕໍ່ທີມພັດທະນາ"
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
        End Try
        Return _return
    End Function

    Public Function _deleteDiscount() As Boolean
        Dim _return As Boolean = False
        Try
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
        Catch ex As Exception
            _Alert()
            lbSaveError.Text = "ບໍ່ສາມາດເຊື່ມຕໍ່ຖານຂໍ້ມູນ, ຕິດຕໍ່ທີມພັດທະນາ"
        End Try
        Try
            Dim cm As OracleCommand = New OracleCommand(myDataType.sqlDeleteBonus, cn) With {.CommandType = CommandType.StoredProcedure}
            cm.Parameters.Add("p_start_amt", OracleDbType.Int32).Value = CDbl(gridDiscount.GridControlElement.SelectedRow.Cells(4).Text.Trim.Replace(",", ""))
            cm.Parameters.Add("p_stop_amt", OracleDbType.Int32).Value = CDbl(gridDiscount.GridControlElement.SelectedRow.Cells(5).Text.Trim.Replace(",", ""))
            cm.Parameters.Add("p_bonus_percent", OracleDbType.Int32).Value = gridDiscount.GridControlElement.SelectedRow.Cells(6).Text.Trim.Replace(",", "")
            cm.ExecuteNonQuery()
            fc.saveLog(Session("userid").ToString, "Delete bonus - [start:" & gridDiscount.GridControlElement.SelectedRow.Cells(4).Text.Trim & "] [stop:" & gridDiscount.GridControlElement.SelectedRow.Cells(5).Text.Trim & "] [percent:" & gridDiscount.GridControlElement.SelectedRow.Cells(6).Text.Trim & "]")
            lbSaveSuccess.Text = "ລຶບຂໍ້ມູນສຳເລັດ"
            _return = True
        Catch ex As Exception
            _Alert()
            lbSaveError.Text = "ບໍ່ສາມາດລຶບຂໍ້ມູນໄດ້, ຕິດຕໍ່ທີມພັດທະນາ"
            _return = False
        Finally
            cn.Close()
            cn.Dispose()
        End Try
        Return _return
    End Function

    Protected Sub gridDiscount_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridDiscount.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ລຶບ"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(4).Text = CDbl(e.Row.Cells(4).Text.Trim.ToString).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            e.Row.Cells(5).Text = CDbl(e.Row.Cells(5).Text.Trim.ToString).ToString("#,###", CultureInfo.GetCultureInfo("en-US").NumberFormat)
            e.Row.Cells(6).Text = CDbl(e.Row.Cells(6).Text.Trim.ToString).ToString("###,###,###,###,###.##")
            e.Row.Cells(6).Font.Bold = True
            e.Row.Cells(6).ForeColor = Drawing.Color.OrangeRed
        End If
    End Sub

    Protected Sub gridDiscount_GridRowClick(sender As Object, e As EventArgs) Handles gridDiscount.GridRowClick
        _Alert()
        If _deleteDiscount() = True Then
            FillData()
            _Alert()
            alertSaveSuccess.Visible = True

        Else
            _Alert()
            alertSaveError.Visible = True
        End If
    End Sub
End Class