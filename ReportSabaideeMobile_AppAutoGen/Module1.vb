Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class connectDB

    Public Function getConnectionString() As String
        Return "Provider=MSDAORA;Data Source=orab1;User ID=USD;password=usd@db; Unicode=false"
    End Function
End Class


Module Module1
    'Dim logPath As String = "C:\01_PAD_Project\PA06\LogFile\SabaideeMobileReport\"
    Dim logPath As String = "C:\SabaideeMobileReport\"
    Dim numDate As Integer = 0
    Dim mDate1 As String = Now.AddDays(numDate).ToString("dd-MMM-yyyy")
    Dim mDate2 As String = Now.AddDays(numDate).ToString("dd-MMM-yyyy")
    Dim FILE_NAME As String = logPath & "M-Topup-" & Now.AddDays(numDate).ToString("ddMMyyyy") & ".txt"
    Dim j As Double = 1
    Dim conDB As New connectDB
    Dim cn As New OleDbConnection
    Dim cm As New OleDbCommand
    Sub Main()
        Try
            Dim files As String() = Directory.GetFiles(logPath)
            For Each file As String In files
                Dim fi As New FileInfo(file)
                If fi.LastAccessTime < DateTime.Now.AddMonths(-7) And fi.FullName <> logPath & "msisdn.txt" Then
                    fi.Delete()
                End If
            Next
        Catch ex As Exception
        End Try

        Try
            Dim fi As New FileInfo(FILE_NAME)
            fi.Delete()
        Catch ex As Exception
        End Try

        Dim msisdnFile As String = logPath & "msisdn.txt"
        Dim StrWer As StreamReader
        Dim pathFolder As String = msisdnFile  '// path ທີ່ຢູຂອງ folder ທີ່ເກັບ textfile ໄວ້.
        Dim list As New ArrayList
        Dim Field(1) As String     '/// 1 ແມ່ນຈຳນວນ field ທີ່ມີໃນ textfile
        Dim i As Integer
        Dim j As Integer
        Dim msisdn As String


        Dim _return As Boolean = False
        Try
            StrWer = File.OpenText(msisdnFile)
            i = 0
            Do Until StrWer.EndOfStream
                list.Add(StrWer.ReadLine)
                i += 1
            Loop

            For j = 0 To i - 1
                msisdn = list(j)
                _RequestHistory(msisdn, mDate1, mDate2)
            Next
            StrWer.Close()
            SendSMS("2059977252", "Success !! M Top Up Sabaidee Mobile. " & "M-Topup-" & Now.AddDays(numDate).ToString("ddMMyyyy") & ".txt")
        Catch ex As Exception
            SendSMS("2059977252", "Failed !! " & ex.ToString.Trim)
            _return = False
            Exit Sub
        End Try

    End Sub
    Private Sub _RequestHistory(telephone As String, date1 As Date, date2 As Date)
        Dim d1 As String = date1.ToString("dd-MMM-yy")
        Dim d2 As String = date2.ToString("dd-MMM-yy")
        Dim sql As String = ""

        sql = String.Format("select  trans_id,trans_date,telephone,msisdn,trans_type,trans_amt,sts,sts_desc" _
       & " from mtopup.tbl_transfer where telephone='{0}' and to_date(trans_date,'DD-Mon-YY') between '{1}' and '{2}' order by trans_id desc", telephone, d1, d2)

        cn = New OleDbConnection
        Dim da As New OleDbDataAdapter
        Dim ds As New DataSet
        Try
            'cn.ConnectionString = WebConfigurationManager.AppSettings.Item("cnString").ToString.Trim
            cn.ConnectionString = conDB.getConnectionString
            cn.Open()
            da = New OleDbDataAdapter(sql, cn)
            da.Fill(ds, "history")
            cn.Close()
            Dim i As Integer = 0
            Dim info As String = ""
            For i = 0 To ds.Tables(0).Rows.Count - 1
                info = j & "|" & ds.Tables(0).Rows(i).Item(0).ToString.Trim & "|" & ds.Tables(0).Rows(i).Item(1).ToString.Trim & "|" & ds.Tables(0).Rows(i).Item(2).ToString.Trim & "|" & ds.Tables(0).Rows(i).Item(3).ToString.Trim & "|" & ds.Tables(0).Rows(i).Item(4).ToString.Trim & "|" & ds.Tables(0).Rows(i).Item(5).ToString.Trim & "|" & ds.Tables(0).Rows(i).Item(6).ToString.Trim & "|" & ds.Tables(0).Rows(i).Item(7).ToString.Trim & "|"
                _saveLog(info)
                j += 1
            Next

        Catch ex As Exception
            SendSMS("2059977252", "Failed !! " & ex.ToString.Trim)
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
            Exit Sub
        Finally
            cn.Dispose()
            da.Dispose()
            ds.Dispose()
        End Try
    End Sub
    Public Function _saveLog(info As String) As Boolean
        Console.WriteLine(info)
        Dim _return As Boolean = False
        Try
            If System.IO.File.Exists(FILE_NAME) = True Then
                Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter2.WriteLine(info)
                objWriter2.Close()
            Else
                System.IO.Directory.CreateDirectory(logPath)
                System.IO.File.Create(FILE_NAME).Dispose()
                Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter.WriteLine("No.|TransID|DateTime|TopupFrom|TopupTo|TopupType|Amount|StatusCode|StatusDesc|")
                objWriter.Close()

                Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter2.WriteLine(info)
                objWriter2.Close()
            End If
            _return = True
        Catch ex As Exception
            SendSMS("2059977252", "Failed !! " & ex.ToString.Trim)
            _return = False
        End Try
        Return _return
    End Function

    Private Function SendSMS(ByVal Msisdn As String, ByVal Msg As String) As String
        System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True
        Dim result As String = Nothing
        Try
            Dim sms As New smsService.ServiceSoapClient
            result = sms.SubmitSMS("MTopup Report for Sabaidee mobile", "LTC-DEV", Msisdn, Msg, "PA06").ToString
        Catch ex As Exception
            result = Nothing
        End Try
        Return result
    End Function

End Module
