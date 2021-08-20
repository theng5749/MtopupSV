<%@ Page Language="VB" AutoEventWireup="false" CodeFile="receiveSlip.aspx.vb" Inherits="receiveSlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family:"Saysettha OT";
            font-size:1.3em;
        }
        #wrap
        {
            width:1024px;
            margin: 5px auto;
    }
    .title
    {
        text-align:right;
        padding:5px;
        font-weight:bold;
        width:15%;
    }
    td
    {
        width:30%;
        height:15px;
    }
    .pg-title
    {
        text-align:center;
        font-weight:bolder;
        font-size:larger;
    }     
    </style>

    <script type="text/javascript" src="Script/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Script/jquery-1.4.1-vsdoc.js"></script>
    <script type="text/javascript" src="Script/jquery-ui-1.8.14.custom.min.js"></script>
    <script type="text/javascript" src="Script/jquery.printElement.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
             //$('#form1').printElement({ printMode: 'popup' });
            window.print();
            //window.close();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="wrap">
    <div style="position:relative;"><img src = "images/222222.png" style="width:53px; position:absolute; top:5px; left:5px; height: 75px;" alt="logo" /></div><br />
        <table style="width:100%;">
            <tr>
                <td class="pg-title" colspan="4">
                    <asp:Label ID="lblTitle" runat="server" 
                        Text="ໃບຮັບເງິນ"></asp:Label>
                    &nbsp;<hr style="color: #000000" />
                    </td>
            </tr>
            <tr>
                <td class="title">
                    ຊື່ລູກຄ້າ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbMemberName" runat="server"></asp:Label>
                </td>
                <td class="title">
                    ເລກທີ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbInvoiceID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="title">
                    ເບີໂທລະສັບ :</td>
                <td class="style2">
                    &nbsp;<asp:Label ID="lbTelephone" runat="server"></asp:Label>
                    </td>
                <td class="title">
                    ວັນທີຈ່າຍ :</td>
                <td class="style2">
                    &nbsp;<asp:Label ID="lbDatePay" runat="server"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td class="title">
                    ທີ່ຢູ່ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbAddress" runat="server"></asp:Label>
                </td>
                <td class="title">
                    ສູນບໍລິການ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbBrch" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="title">
                    ຈຳນວນ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbAmount" runat="server"></asp:Label>
                </td>
                <td class="title">
                    ພະນັກງານ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbUserID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="title">
                    ສ່ວນຫຼຸດ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbDiscount" runat="server"></asp:Label>
                </td>
                <td class="title">
                   </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td class="title">
                    ຈ່າຍຕົວຈິງ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbRealPaid" runat="server"></asp:Label>
                </td>
                <td class="title">
                    ໜີ້ຍັງເຫຼືອ :</td>
                <td>
                    &nbsp;<asp:Label ID="lbCredit" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
        <hr style="color: #000000" />

        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; font-weight: bold">
                    &nbsp; ຜູ້ມອບເງິນ</td>
                <td style="text-align: center; font-weight: bold">
                    &nbsp; </td>
                <td style="text-align: center; font-weight: bold">
                    ຜູ້ຮັບເງິນ</td>
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
                    ຊື່ແຈ້ງ:................................ </td>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    ຊື່ແຈ້ງ:................................ </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    ວັນທີ ___/___/________</td>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    ວັນທີ ___/___/________</td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label1" runat="server" Text="ໝາຍເຫດ :" Font-Bold="True"></asp:Label>



        <br />
        <asp:Label ID="lbComment" runat="server" Text=""></asp:Label>
    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>

</body>
</html>
