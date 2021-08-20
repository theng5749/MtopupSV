<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reportform.aspx.vb" Inherits="reportform" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>M-TOPUP PLUS</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/justified-nav.css" rel="stylesheet" />
    <link rel="canonical" href="https://getbootstrap.com/docs/3.3/examples/justified-nav/">
    <link rel="stylesheet" href="font-awesome/css/font-awesome.min.css">
    <script src="js/jquery-1.10.2.js"></script>
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
                        <li><a href="approveform.aspx">ອະນຸມັດ</a></li>
                        <li><a href="refillform.aspx">ຕື່ມເງິນ</a></li>
                        <li class="active"><a href="reportform.aspx">ລາຍງານ</a></li>
                        <li><a href="settingform.aspx">ຕັ້ງຄ່າ</a></li>
                        <li id="btnlogout" runat="server"><a href="startupform.aspx">ອອກຈາກລະບົບ</a></li>
                    </ul>
                </nav>

            </div>
            <div class="row">
                <hr />
                <div class="col-md-12">
                    <div class="col-md-4">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-2">
                                        <i class="fa fa-quote-left fa-2x"></i>
                                    </div>
                                    <div class="col-xs-11">
                                        <p>
                                            <asp:Label ID="lbapprove" runat="server" Text="0 ລາຍການ" Font-Size="25pt"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer announcement-bottom">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <a href="#">
                                            <p>
                                                ລາຍການທີ່ຕ້ອງອະນຸມັດ
                                            </p>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="panel panel-danger">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-2">
                                        <i class="fa fa-quote-left fa-2x"></i>
                                    </div>
                                    <div class="col-xs-11">
                                        <p>
                                            <asp:Label ID="lbtotalrefill" runat="server" Text="0 ກີບ" Font-Size="25pt"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer announcement-bottom">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <a href="#">
                                            <p>
                                                ລວມມູນຄ່າຕື່ມ M-TOPUP
                                            </p>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-2">
                                        <i class="fa fa-quote-left fa-2x"></i>
                                    </div>
                                    <div class="col-xs-11">
                                        <p>
                                            <asp:Label ID="lbsale" runat="server" Text="0 ກີບ" Font-Size="25pt"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer announcement-bottom">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <a href="#">
                                            <p>ລວມມູນຄ່າການຂາຍ</p>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
