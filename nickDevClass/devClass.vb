Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports Oracle.ManagedDataAccess.Client
Public Class connectDB
    Public Function getConnectionString() As String
        ' Return "Data Source=172.28.12.95/CCDB001;User Id=mtopup;Password=mtopupdb;Integrated Security=no;"
        Return "Data Source=(DESCRIPTION=(ADDRESS = (PROTOCOL = TCP)(HOST = 172.28.12.95)(PORT = 1521))(CONNECT_DATA=(SID=CCDB001)));User Id=mtopup;Password=mtopupdb;"
    End Function

    Public Function getConnectionWebCallString() As String
        Return "Data Source=(DESCRIPTION=(ADDRESS = (PROTOCOL = TCP)(HOST = 172.28.14.86)(PORT = 1521))(CONNECT_DATA=(SERVICE_NAME=CCDB003)));User Id=webcall;Password=webcall123;"
    End Function
End Class
'/////////////////////////////////////////////////////////////////////////
Public Class myDataType
    Public Const logPath As String = "E:\PA06\[Backup]\MTOPUP\Log\"
    Public Const logPathBill As String = "E:\PA06\[Backup]\MTOPUP\LogBill\"
    'Public Const logPathBill As String = "C:\MTOPUP\LogBill\"
    Public Const MTOPUP_USR As String = "MTOPUP"
    Public Const MTOPUP_PW As String = "mtopup@123"

    Public Const PREPAID_USR As String = "PREPAID"
    Public Const PREPAID_PW As String = "prepaid@123"


    Public Const sqlQueryUser As String = "STP_QUERY_USER_NEW"
    Public Const sqlCheckBalanceMtopup As String = "FN_CHECKING_CREDIT"
    Public Const sqlCheckPwSubs As String = "FN_CHECKING_PASSWORD"
    Public Const sqlChangePW As String = "STP_UPDATE_PW"
    Public Const sqlCheckMember As String = "FN_CHECKING_MEMBER"
    Public Const sqlCheckMemberRoot As String = "FN_CHECKING_MEMBER_ROOT"
    Public Const sqlCheckMemberLevel As String = "FN_CHECKING_LEVEL"

    Public Const sqlInsertTransfer As String = "STP_INSERT_TRANSFER"
    Public Const sqlAddjustBalanceMtopup As String = "STP_RECHARGE"
    Public Const sqlInsertSale As String = "STP_INSERT_SALE"
    Public Const sqlInsertMember As String = "STP_INSERT_MEMBER"
    Public Const sqlInsertUser As String = "STP_INSERT_USERCTRL"


    Public Const sqlUpdateTransfer As String = "STP_UPDATE_TRANSFER"
    Public Const sqlUpdateMember As String = "STP_UPDATE_MEMBER"
    Public Const sqlUpdateUser As String = "STP_UPDATE_USERCTRL"


    Public Const sqlChangePassword As String = "STP_UPDATE_USERPASSWORD"


    Public Const sqlRecharge As String = "STP_RECHARGE"
    Public Const sqlRecharge2 As String = "STP_RECHARGE_2"
    Public Const sqlInsertRechargeLog As String = "STP_INSERT_RECHAGE_LOG"
    Public Const sqlInsertProcessRefill As String = "STP_PROCESS_REFILL"
    Public Const sqlInsertFileLog As String = "STP_INSERT_FILE_LOG"
    Public Const sqlInsertUserApprove As String = "STP_INSERT_USER_APPROVE"
    Public Const sqlDeleteUserApprove As String = "STB_DELETE_USER_APPROVE"
    Public Const sqlInsertRefillLog As String = "STP_INSERT_MEMBER_REFILL"


    Public Const sqlUpdateRechargeLog As String = "STP_UPDATE_RECHARGE_LOG"

    Public Const sqlInsertInvoice As String = "STP_INSERT_INVOICE"
    Public Const sqlUpdateSale As String = "STP_UPDATE_SALE"
    Public Const sqlInsertInvoiceDetail As String = "STP_INSERT_INVOICE_DETAIL"

    Public Const sqlUpdateInvoice As String = "STP_UPDATE_INVOICE"


    Public Const sqlInsertDiscount As String = "STP_INSERT_DISCOUNT"
    Public Const sqlDeleteDiscount As String = "STP_DELETE_DISCOUNT"

    Public Const sqlInsertBonus As String = "STP_INSERT_BONUS"
    Public Const sqlDeleteBonus As String = "STP_DELETE_BONUS"

    Public Const sqlInsertBranch As String = "STP_INSERT_BRANCH"
    Public Const sqlUpdateBranch As String = "STP_UPDATE_BRANCH"

    '////M-Topup Plus
    Public Const sqlInsertUserPlus As String = "STP_INSERT_USER_PLUS"
    Public Const sqlQueryUserPlus As String = "stp_query_user_new_plus"

End Class

'//////////////////////////////////////////////////////////////////////////////

Public Class SubFunction
    Dim strcon As String = "Data Source=(DESCRIPTION=(ADDRESS = (PROTOCOL = TCP)(HOST = 172.28.12.95)(PORT = 1521))(CONNECT_DATA=(SID=CCDB001)));User Id=mtopup;Password=mtopupdb;"
    Public Function LaoAscToUni(ByVal source As String) As String
        source = source.Replace(ChrW(161), ChrW(3713))
        source = source.Replace(ChrW(162), ChrW(3714))
        source = source.Replace(ChrW(163), ChrW(3716))
        source = source.Replace(ChrW(164), ChrW(3719))
        source = source.Replace(ChrW(165), ChrW(3720))
        source = source.Replace(ChrW(167), ChrW(3722))
        source = source.Replace(ChrW(168), ChrW(3725))
        source = source.Replace(ChrW(169), ChrW(3732))
        source = source.Replace(ChrW(170), ChrW(3733))
        source = source.Replace(ChrW(171), ChrW(3734))
        source = source.Replace(ChrW(234), ChrW(3735))
        source = source.Replace(ChrW(173), ChrW(3737))
        source = source.Replace(ChrW(174), ChrW(3738))
        source = source.Replace(ChrW(175), ChrW(3739))
        source = source.Replace(ChrW(176), ChrW(3740))
        source = source.Replace(ChrW(177), ChrW(3741))
        source = source.Replace(ChrW(178), ChrW(3742))
        source = source.Replace(ChrW(179), ChrW(3743))
        source = source.Replace(ChrW(180), ChrW(3745))
        source = source.Replace(ChrW(181), ChrW(3746))
        source = source.Replace(ChrW(235), ChrW(3747))
        source = source.Replace(ChrW(236), ChrW(3749))
        source = source.Replace(ChrW(184), ChrW(3751))
        source = source.Replace(ChrW(166), ChrW(3754))
        source = source.Replace(ChrW(185), ChrW(3755))
        source = source.Replace(ChrW(186), ChrW(3757))
        source = source.Replace(ChrW(187), ChrW(3758))
        source = source.Replace(ChrW(198), ChrW(3759))
        source = source.Replace(ChrW(189), ChrW(3760))
        source = source.Replace(ChrW(241), ChrW(3761))
        source = source.Replace(ChrW(190), ChrW(3762))
        source = source.Replace(ChrW(191), ChrW(3763))
        source = source.Replace(ChrW(242), ChrW(3764))
        source = source.Replace(ChrW(243), ChrW(3765))
        source = source.Replace(ChrW(244), ChrW(3766))
        source = source.Replace(ChrW(245), ChrW(3767))
        source = source.Replace(ChrW(247), ChrW(3768))
        source = source.Replace(ChrW(248), ChrW(3769))
        source = source.Replace(ChrW(246), ChrW(3771))
        source = source.Replace(ChrW(249), ChrW(3772))
        source = source.Replace(ChrW(188), ChrW(3773))
        source = source.Replace(ChrW(192), ChrW(3776))
        source = source.Replace(ChrW(193), ChrW(3777))
        source = source.Replace(ChrW(194), ChrW(3778))
        source = source.Replace(ChrW(195), ChrW(3779))
        source = source.Replace(ChrW(196), ChrW(3780))
        source = source.Replace(ChrW(197), ChrW(3782))
        source = source.Replace(ChrW(200), ChrW(3784)) 'Mai eak
        source = source.Replace(ChrW(201), ChrW(3785)) 'Mai Tho
        'Private use
        source = source.Replace(ChrW(732), ChrW(3761) & ChrW(3785)) 'Mai kun & mai Tho
        source = source.Replace(ChrW(8250), ChrW(3764) & ChrW(3785))
        source = source.Replace(ChrW(339), ChrW(3765) & ChrW(3785))
        source = source.Replace(ChrW(8482), ChrW(3766) & ChrW(3785))
        source = source.Replace(ChrW(353), ChrW(3767) & ChrW(3785))
        source = source.Replace(ChrW(237), ChrW(3771) & ChrW(3785))
        source = source.Replace(ChrW(213), ChrW(3785) & ChrW(3763))
        source = source.Replace(ChrW(210), ChrW(3789) & ChrW(3785))
        '-----------------
        source = source.Replace(ChrW(250), ChrW(3784))
        source = source.Replace(ChrW(251), ChrW(3785))
        source = source.Replace(ChrW(252), ChrW(3786))
        source = source.Replace(ChrW(253), ChrW(3787))
        source = source.Replace(ChrW(254), ChrW(3788))
        source = source.Replace(ChrW(240), ChrW(3789))
        source = source.Replace(ChrW(224), ChrW(3792))
        source = source.Replace(ChrW(225), ChrW(3793))
        source = source.Replace(ChrW(226), ChrW(3794))
        source = source.Replace(ChrW(227), ChrW(3795))
        source = source.Replace(ChrW(228), ChrW(3796))
        source = source.Replace(ChrW(229), ChrW(3797))
        source = source.Replace(ChrW(230), ChrW(3798))
        source = source.Replace(ChrW(231), ChrW(3799))
        source = source.Replace(ChrW(232), ChrW(3800))
        source = source.Replace(ChrW(233), ChrW(3801))
        source = source.Replace(ChrW(206), ChrW(3804))
        source = source.Replace(ChrW(207), ChrW(3805))
        source = source.Replace(ChrW(8240), ChrW(3771) & ChrW(3784)) 'ໄມ້ກົງ + ໄມ້ເອກ

        LaoAscToUni = source
    End Function
    Private Function s(ByVal str As String) As String
        Dim st As String = ""
        st = str.Replace("'", "")
        st = str.Replace("-", "")
        st = str.Replace(";", "")
        Return st
    End Function
    Public Function pp(ByVal password As String) As String
        Dim sVal As String
        Dim sHex As String = "*"
        For a As Integer = 1 To password.Trim.Length
            Dim chr As Char = Mid(password.Trim, a, 1)
            sVal = Conversion.Hex(Strings.Asc(chr) + a)
            sHex = sHex & sVal
        Next
        sHex = sHex & "%="
        Return sHex
    End Function
    Public Function saveLog(UserID As String, Process As String) As Boolean
        Dim _return As Boolean = False
        Try

            Dim y As String = Now.Year.ToString
            Dim m As String = Now.Month.ToString
            If m.Length = 1 Then
                m = "0" & m
            End If
            Dim log_name As String = y & "_" & m
            Dim FILE_NAME As String = myDataType.logPath & "MTOPUP_Log_" & log_name & ".log"
            If System.IO.File.Exists(FILE_NAME) = True Then
                Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter2.WriteLine(UserID & "|" & Today.Date & "|" & Now.TimeOfDay.ToString & "|" & Process & "|")
                objWriter2.Close()
            Else
                System.IO.Directory.CreateDirectory(myDataType.logPath)
                System.IO.File.Create(FILE_NAME).Dispose()
                Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter.WriteLine("UserID|Date|Time|Process|")
                objWriter.Close()

                Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter2.WriteLine(UserID & "|" & Today.Date & "|" & Now.TimeOfDay.ToString & "|" & Process & "|")
                objWriter2.Close()
            End If
            _return = True
        Catch ex As Exception
            _return = False
        End Try
        Return _return
    End Function
    Public Function saveLogBill(info As String) As Boolean
        Dim _return As Boolean = False
        Try

            Dim y As String = Now.Year.ToString
            Dim m As String = Now.Month.ToString
            If m.Length = 1 Then
                m = "0" & m
            End If
            Dim log_name As String = y & "_" & m
            Dim FILE_NAME As String = myDataType.logPathBill & "MTOPUP_Log_Bill_" & log_name & ".log"
            If System.IO.File.Exists(FILE_NAME) = True Then
                Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter2.WriteLine(info)
                objWriter2.Close()
            Else
                System.IO.Directory.CreateDirectory(myDataType.logPathBill)
                System.IO.File.Create(FILE_NAME).Dispose()
                'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                'objWriter.WriteLine("MemberName|MSISDN|Address|Amount|InvoiceID|PaymentDate|Brch|UserName|PercentDiscount|Total|")
                'objWriter.Close()

                Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter2.WriteLine(info)
                objWriter2.Close()
            End If
            _return = True
        Catch ex As Exception
            _return = False
        End Try
        Return _return
    End Function
    Public Function GetBranchID(ByVal BranchName As String) As Integer
        Dim result As Integer = 0
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim dr As OracleDataReader
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = strcon
                .Open()
                Dim sql As String = "select branch_id from tbl_branch where BRANCH_NAME like '%" & BranchName.Trim & "%'"
                cmd = New OracleCommand(sql, conn)
                dr = cmd.ExecuteReader
                If dr.Read Then
                    result = CInt(dr(0))
                End If
            End With
        Catch ex As Exception
            result = False
        Finally
            conn.Close()
            cmd.Dispose()
        End Try
        Return result
    End Function
    Public Function UpdateStampCode(ByVal Telephone As String) As Boolean
        Dim result As Boolean = False
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = strcon
                .Open()
                Dim sql As String = "update tbl_stampcode set status='Done' where telephone='" & Telephone & "'"
                cmd = New OracleCommand(sql, conn)
                cmd.ExecuteNonQuery()
                result = True
            End With
        Catch ex As Exception
            result = False
        Finally
            conn.Close()
            cmd.Dispose()
        End Try
        Return result
    End Function
End Class

