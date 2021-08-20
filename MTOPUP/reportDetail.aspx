<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reportDetail.aspx.vb" Inherits="reportDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family:"Saysettha OT";
            font-size:12pt;
        }
        #wrap
        {
            width:1024px;
            margin: 5px auto;
    }
        .auto-style2 {
            width: 35px;
        }
        .auto-style4 {
            width: 100px;
        }
    </style>

    

    <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/datepicker.css" />


    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet"/>
    <!-- Add custom CSS here -->
    <link href="css/sb-admin.css" rel="stylesheet"/>
    <link rel="stylesheet" href="font-awesome/css/font-awesome.min.css"/>


    <script type="text/javascript" src="Script/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Script/jquery-1.4.1-vsdoc.js"></script>
    <script type="text/javascript" src="Script/jquery-ui-1.8.14.custom.min.js"></script>
    <script type="text/javascript" src="Script/jquery.printElement.js"></script>
   

    <style type="text/css" media="print">
        @page {
            size: landscape;
        }
    </style>

     <script type="text/javascript">
         $(document).ready(function () {
             // $('#form1').printElement({ printMode: 'popup' });
             window.print();
             window.close();
         });
    </script>

</head>
<body>
    <form id="form1" runat="server" style="font-family:'Phetsarath OT'" >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="wrap">
                    <div style="text-align:center">
                        <asp:Label ID="lblMainTitle" runat="server"
                            Text="ສະຫຼຸບການເບີກ-ຈ່າຍ-ສົ່ງສາງ-ຂາຍສິນຄ້າປະເພດມູນຄ່າໂທ M Top Up ຂອງ ລລທ ກຸ່ມແຂວງທົ່ວປະເທດ ປະຈຳເດືອນ 11 ປີ 2014"></asp:Label>
                        </td>
                    </div>
                    <p></p>
                    <div style="text-align:center">
                        <asp:Label ID="lblTitle" runat="server" 
                        Text="ທົ່ວປະເທດ"></asp:Label>
                    </div>
                    <p></p>
        
        
                    <table style="width: 100%; text-align:center; padding:1pt; border:1pt;" border="1">
                        <tr style="height:50px">
                            <td class="auto-style2">ລ/ດ</td>
                            <td class="auto-style4">ລາຍການ</td>
                            <td>ຍົກຍອດ</td>
                            <td>ຮັບເຂົ້າ</td>
                            <td>ຮັບຄືນ</td>
                            <td>ລວມ</td>
                            <td>ສົ່ງສາງ</td>
                            <td>ປ່ຽນແທນ ແລະ ແຖມ</td>
                            <td>ຂາຍ</td>
                            <td>ສ່ວນຫຼຸດ</td>
                            <td>ເງິນຕົວຈິງ</td>
                            <td>ຍອດເຫຼືອ</td>
                        </tr>
                        <tr style="height:30px">
                            <td colspan="2">ລວມ</td>
                            <td style="text-align:center">66,170,500</td>
                            <td>50,000,000</td>
                            <td>&nbsp;</td>
                            <td>116,170,500</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>31,110,000</td>
                            <td>2,488,800</td>
                            <td>28,621,200</td>
                            <td>85,060,500</td>
                        </tr>
                        <tr style="height:30px">
                            <td>1</td>
                            <td>ຂາຍຜ່ານ LTC</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>31,110,000</td>
                            <td>2,488,800</td>
                            <td>28,621,200</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                            <td>&nbsp;</td>
                            <td>ຂາຍຍ່ອຍທົ່ວໄປ</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                            <td>&nbsp;</td>
                            <td>ຂາຍຍ່ອຍ 5%</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                            <td>&nbsp;</td>
                            <td>6%</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                           <td>&nbsp;</td>
                            <td>8%</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>31,110,000</td>
                            <td>2,488,800</td>
                            <td>28,621,200</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="12" style="background-color:whitesmoke">&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                            <td>2</td>
                            <td>Dealer</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                            <td>&nbsp;</td>
                            <td>Dealer 8.5%</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                            <td>&nbsp;</td>
                            <td>Dealer 9%</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:30px">
                            <td>&nbsp;</td>
                            <td>Dealer 10%</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
        
        


                    <br />
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; font-weight: bold">
                    &nbsp;</td>
                <td style="text-align:right; font-weight: bold" colspan="2">
                    ນະຄອນຫຼວງວຽງຈັນ, ວັນທີ 10/12/2014</td>
            </tr>
            <tr>
                <td><br /></td>
            </tr>
            <tr>
                <td style="text-align: center; font-weight: bold; width:200px;">
                    &nbsp;ພະແນກການຂາຍ
                </td>
                <td style="text-align: center; font-weight: bold;">
                    &nbsp;ພາກສ່ວນວິເຄາະ ແລະ ວາງແຜນການຂາຍ</td>
               <td style="text-align: center; font-weight: bold; width:200px;">
                    &nbsp;ຜູ້ສະຫຼຸບ
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    &nbsp;</td>
            </tr>
        </table>
        <br />



        <br />
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>

</body>
</html>
