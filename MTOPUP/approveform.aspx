<%@ Page Language="VB" AutoEventWireup="false" CodeFile="approveform.aspx.vb" Inherits="approveform" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>M-TOPUP PLUS</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/justified-nav.css" rel="stylesheet" />
    <link rel="canonical" href="https://getbootstrap.com/docs/3.3/examples/justified-nav/">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- The justified navigation menu is meant for single line per list item.
           Multiple lines will require custom code not provided by Bootstrap. -->
            <div class="masthead">
                <h3 class="text-muted">M-TOPUP PLUS <small>HI
                    <asp:Label ID="lbname" runat="server" Text=""></asp:Label></small></h3>
                <nav>
                    <ul class="nav nav-justified">
                        <li><a href="homeform.aspx">ໜ້າຫຼັກ</a></li>
                        <li class="active"><a href="approveform.aspx">ອະນຸມັດ</a></li>
                        <li><a href="refillform.aspx">ຕື່ມເງິນ</a></li>
                        <li><a href="reportform.aspx">ລາຍງານ</a></li>
                        <li><a href="settingform.aspx">ຕັ້ງຄ່າ</a></li>
                        <li id="btnlogout" runat="server"><a href="startupform.aspx">ອອກຈາກລະບົບ</a></li>
                    </ul>
                </nav>
            </div>
        </div>
    </form>
</body>
</html>
