
Imports System.IO
Imports System.Data
Imports nickDevClass
Partial Class RePrint
    Inherits System.Web.UI.Page
    Dim myDataType As New myDataType
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
                    menuBranch.Visible = True
                    menuDiscount.Visible = True
                    menuRechargeHistory.Visible = True
                    menuDiscountSale.Visible = True
                    menuCredit.Visible = True
                    menuUserCtrl.Visible = True
                    menuCheckApprove.Visible = True
                    menuInvoice.Visible = True
                    cbManage.Visible = True
                    cbReport.Visible = True
                    menuProHis.Visible = True
                    menuPromotion.Visible = True
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
            gridReprint.RequestSource = _getData()
            gridReprint.DataBind()
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
        menuApprove.Visible = False
        menuUser.Visible = False
        menuCheckApprove.Visible = False
        menuUserCtrl.Visible = False
        menuBranch.Visible = False
        menuRechargeHistory.Visible = False
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
        If gridReprint.GridControlElement.Rows.Count <> 0 Then
            gridReprint.GridControlElement.HeaderRow.ForeColor = Drawing.Color.CornflowerBlue
            gridReprint.GridControlElement.HeaderRow.Cells(4).Text = "ຊື່ສະມາຊິກ"
            gridReprint.GridControlElement.HeaderRow.Cells(5).Text = "ເບີໂທ"
            gridReprint.GridControlElement.HeaderRow.Cells(6).Text = "ທີ່ຢູ່"
            gridReprint.GridControlElement.HeaderRow.Cells(7).Text = "ມູນຄ່າຊຳລະ"
            gridReprint.GridControlElement.HeaderRow.Cells(8).Text = "ເລກທີໃບບິນ"
            gridReprint.GridControlElement.HeaderRow.Cells(9).Text = "ວັນທີຈ່າຍ"
            gridReprint.GridControlElement.HeaderRow.Cells(10).Text = "ກຸ່ມ"
        End If
    End Sub
    Public Function _getData() As DataTable
        Dim _return As DataTable = Nothing
        Dim StrWer As StreamReader
        Dim pathFolder As String = myDataType.logPathBill '// path ທີ່ຢູຂອງ folder ທີ່ເກັບ textfile ໄວ້.
        Dim list As New ArrayList
        Dim spt() As String
        Dim i As Integer
        Dim j As Integer
        Dim tx As String

        Dim y As String = Now.Year.ToString
        Dim m As String = Now.Month.ToString
        If m.Length = 1 Then
            m = "0" & m
        End If
        Dim log_name As String = y & "_" & m
        Dim FILE_NAME As String = myDataType.logPathBill & "MTOPUP_Log_Bill_" & log_name & ".log"

        Dim dt As New DataTable

        Try
            StrWer = File.OpenText(FILE_NAME)
            i = 0
            Do Until StrWer.EndOfStream
                'list.Add(StrWer.ReadLine)
                dt.Rows.Add()
                tx = StrWer.ReadLine
                spt = tx.Split("|")

                For j = 0 To spt.Length - 1
                    If i = 0 Then
                        dt.Columns.Add()
                    End If
                    dt.Rows(i).Item(j) = spt(j).ToString
                Next
                dt.Rows(i).Item(j - 1) = tx
                i += 1
            Loop
            StrWer.Close()
            _return = dt
        Catch ex As Exception
            _return = Nothing
        End Try
        Return _return
    End Function

    Public Function _getDataSearch(searchString As String) As DataTable
        Dim _return As DataTable = Nothing
        Dim StrWer As StreamReader
        Dim pathFolder As String = myDataType.logPathBill '// path ທີ່ຢູຂອງ folder ທີ່ເກັບ textfile ໄວ້.
        Dim list As New ArrayList
        Dim spt() As String
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim l As Integer
        Dim index As Integer = 0
        Dim tx As String

        Dim y As String = Now.Year.ToString
        Dim m As String = Now.Month.ToString
        If m.Length = 1 Then
            m = "0" & m
        End If
        Dim log_name As String = y & "_" & m
        Dim FILE_NAME As String = myDataType.logPathBill & "MTOPUP_Log_Bill_" & log_name & ".log"

        Dim dt As New DataTable

        Try
            StrWer = File.OpenText(FILE_NAME)
            i = 0
            Do Until StrWer.EndOfStream
                'list.Add(StrWer.ReadLine)
                l = 0
                k = 0
                j = 0
                tx = StrWer.ReadLine
                spt = tx.Split("|")

                For k = 0 To spt.Length - 1
                    If spt(k).ToString.Contains(searchString) Then
                        l = 1
                    End If
                Next

                If l = 1 Then
                    dt.Rows.Add()
                    For j = 0 To spt.Length - 1
                        If index = 0 Then
                            dt.Columns.Add()
                        End If
                        dt.Rows(index).Item(j) = spt(j).ToString
                    Next
                    dt.Rows(index).Item(j - 1) = tx
                    index += 1
                End If
                i += 1
            Loop
            StrWer.Close()
            _return = dt
        Catch ex As Exception
            _return = Nothing
        End Try
        Return _return
    End Function

    Protected Sub gridReprint_GridBindCompleted(sender As Object, e As GridViewRowEventArgs) Handles gridReprint.GridBindCompleted
        If e.Row.RowType = DataControlRowType.Header Then
            'hide columns
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = e.Row.Cells(0).Controls(0)
            btn.Text = "ພິມບິນ"
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(5).Font.Bold = True
            e.Row.Cells(7).Font.Bold = True
            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(7).ForeColor = Drawing.Color.CadetBlue
            e.Row.Cells(8).ForeColor = Drawing.Color.OrangeRed
        End If
    End Sub

    Protected Sub gridReprint_GridRowClick(sender As Object, e As EventArgs) Handles gridReprint.GridRowClick
        Session.Add("bill", gridReprint.GridControlElement.SelectedRow.Cells(14).Text.ToString)
        Dim PageUrl As String = "receiveSlip.aspx"
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & PageUrl & "');", True)
    End Sub

    Protected Sub gridReprint_GridViewPageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gridReprint.GridViewPageIndexChanged
        If txtSearchText.Text.Trim <> "" Then
            gridReprint.RequestSource = _getDataSearch(txtSearchText.Text.Trim)
        Else
            gridReprint.RequestSource = _getData()
        End If
        gridReprint.GridControlElement.PageIndex = e.NewPageIndex
        gridReprint.DataBind()
        _gridColumnName()
    End Sub

    Protected Sub txtSearchText_TextChanged(sender As Object, e As EventArgs) Handles txtSearchText.TextChanged
        If txtSearchText.Text.Trim.ToString <> "" Then
            gridReprint.RequestSource = _getDataSearch(txtSearchText.Text.Trim.ToString)
            gridReprint.GridControlElement.PageIndex = 0
            gridReprint.DataBind()
        Else
            gridReprint.RequestSource = _getData()
            gridReprint.GridControlElement.PageIndex = 0
            gridReprint.DataBind()
        End If

    End Sub
End Class
