<%@ Page Language="VB" AutoEventWireup="false" CodeFile="proHistory.aspx.vb" Inherits="proHistory" %>

<%@ Register Src="~/Lo_GridControl.ascx" TagPrefix="uc1" TagName="Lo_GridControl" %>
<%@ Register Src="~/EDatePicker.ascx" TagPrefix="uc1" TagName="EDatePicker" %>
<%@ Register Src="~/GridControl.ascx" TagPrefix="uc1" TagName="GridControl" %>
<%@ Register Src="~/EDatePicker2.ascx" TagPrefix="uc1" TagName="EDatePicker2" %>






<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>M-TOPUP</title>

     <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/datepicker.css" />


    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <!-- Add custom CSS here -->
    <link href="css/sb-admin.css" rel="stylesheet">
    <link rel="stylesheet" href="font-awesome/css/font-awesome.min.css">


    <!-- JavaScript -->
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="holder/holder.js"></script>
    <script src="BootStrap/js/bootstrap-datepicker.js"></script>
    <!-- Page Specific Plugins -->
    <script src="link/raphael-min.js"></script>
    <script src="link/morris-0.4.3.min.js"></script>
    <script src="js/morris/chart-data-morris.js"></script>
    <script src="js/tablesorter/jquery.tablesorter.js"></script>
    <script src="js/tablesorter/tables.js"></script>


 <script type="text/javascript">
     function closeModalMember() {
         $("#modalSelectMember").modal("hide");
     }

     function modalSelectMember() {
         $("#modalSelectMember").modal("show");
     }
    </script>


     <script type="text/javascript">
         function allowOnlyNumber(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }

         function Comma(Num) { //function to add commas to textboxes
             Num += '';
             Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
             Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
             x = Num.split('.');
             x1 = x[0];
             x2 = x.length > 1 ? '.' + x[1] : '';
             var rgx = /(\d+)(\d{3})/;
             while (rgx.test(x1))
                 x1 = x1.replace(rgx, '$1' + ',' + '$2');
             return x1 + x2;
         }
    </script>


</head>

<body>
    <form id="form1" runat="server" style="font-family: 'Phetsarath OT'">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="wrapper">

            <!-- Sidebar -->
            <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-ex1-collapse">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand"  >M-TOPUP</a>
                </div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse navbar-ex1-collapse">
                    <ul class="nav navbar-nav side-nav">
                        <%--<li runat="server" id="menuMainpage"><a  ><i class="fa fa-desktop"></i> ໜ້າຫຼັກ</a></li>--%>
                        <li runat="server" id="menuMember"><a href="member.aspx"><i class="fa fa-pencil-square"></i> ຂໍ້ມູນສະມາຊິກ</a></li>
                        <li runat="server" id="menuRecharge"><a href="recharge.aspx"><i class="fa fa-share-square"></i> ເຕີມເງິນ</a></li>
                        <li runat="server" id="menuReceive"><a href="receive.aspx"><i class="fa fa-check-square"></i> ຮັບເງິນ</a></li>
                        <li runat="server" id="menuReprint"><a href="reprint.aspx"><i class="fa fa-print"></i> ພິມບິນຍ້ອນຫຼັງ</a></li>
                        <li class="dropdown" runat="server" id ="cbManage">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i> ຈັດການຂໍ້ມູນ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuUser"><a href="user.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນຜູ້ໃຊ້ລະບົບ</a></li>
                                <li runat="server" id="menuBranch"><a href="branch.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນກຸ່ມ</a></li>
                                <li runat="server" id="menuDiscount"><a href="discount.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການສ່ວນຫຼຸດ ( Discount )</a></li>
                                <li runat="server" id="menuDiscountSale"><a href="discountSale.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການໂບນັດ ( Bonus )</a></li>
                                <li runat="server" id="menuPromotion"><a href="promotion.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນໂປຣ ( Promotion )</a></li>
                            </ul>
                        </li>
                        <li class="dropdown active" runat="server" id="cbReport">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i> ລາຍງານ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuRechargeHistory"><a href="rechargeHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການເຕີມເງິນ</a></li>
                                <li runat="server" id="menuProHis" class="active"><a href="proHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການໃຫ້ໂປຣ</a></li>
                                <li runat="server" id="menuHistory"><a href="history.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການໂອນ</a></li>
                                <li runat="server" id="menuReceiveHistory"><a href="receiveHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການຮັບເງິນ</a></li>
                                <li runat="server" id="menuHistoryL1"><a href="rechargeHistoryL1.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການເຕີມເງິນ ( L 1 )</a></li>
                                <li runat="server" id="menuCheckApprove"><a href="checkApprove.aspx"><i class="fa fa-calendar"></i>ກວດສອບການອະນຸມັດ</a></li>
                                <li runat="server" id="menuCredit"><a href="credit.aspx"><i class="fa fa-dollar"></i> ໜີ້ຄ້າງຊຳລະ</a></li>
                                <li runat="server" id="menuInvoice"><a href="invoice.aspx"><i class="fa fa-dollar"></i> ສະຫຼຸບຍອດເງິນທີ່ໄດ້ຮັບ</a></li>
                            </ul>
                        </li>
                    </ul>

                    <ul class="nav navbar-nav navbar-right navbar-user">
                        <%--<li class="dropdown messages-dropdown">
              <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-quote-left"></i> ລາຍການ <b class="caret"></b></a>
              <ul class="dropdown-menu">
                <li class="dropdown-header">ລາຍການ</li>
                <li class="message-preview">
                  <a href="#">
                    <span class="name">ຫົວຂໍ້:</span>
                    <span class="message">ລາຍລະອຽດຫົວຂໍ້ທີ່ຕ້ອງການໃຊ້</span>
                  </a>
                </li>
                <li class="divider"></li>
                <li class="message-preview">
                  <a href="#">
                    <span class="name">ຫົວຂໍ້:</span>
                    <span class="message">ລາຍລະອຽດຫົວຂໍ້ທີ່ຕ້ອງການໃຊ້</span>
                  </a>
                </li>
                <li class="divider"></li>
                <li class="message-preview">
                  <a href="#">
                    <span class="name">ຫົວຂໍ້:</span>
                    <span class="message">ລາຍລະອຽດຫົວຂໍ້ທີ່ຕ້ອງການໃຊ້</span>
                  </a>
                </li>
              </ul>
            </li>--%>

                        <li class="dropdown user-dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-user"></i> <asp:Label ID="lbUserName" runat="server" Text=""></asp:Label> <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="changepassword.aspx"><i class="fa fa-user"></i> ປ່ຽນລະຫັດຜ່ານ</a></li>
                                <li class="divider"></li>
                                <li><a href="Default.aspx"><i class="fa fa-power-off"></i> ອອກຈາກລະບົບ</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <!-- /.navbar-collapse -->
            </nav>

            <div id="page-wrapper">

                <div class="row">
                    <div class="col-lg-12">
                        <h1>ປະຫວັດການຮັບໂປຣໂມຣເຊິນ <small>ຂໍ້ມູນປະຫວັດການຮັບໂປຣໂມຣເຊິນ</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-calendar"></i> ປະຫວັດການຮັບໂປຣໂມຣເຊິນ</li>
                        </ol>
                        <%--<div class="alert alert-success alert-dismissable">
              <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
              Welcome to SB Admin by <a class="alert-link" href="http://startbootstrap.com">Start Bootstrap</a>! Feel free to use this template for your admin needs! We are using a few different plugins to handle the dynamic tables and charts, so make sure you check out the necessary documentation links provided.
            </div>--%>
                    </div>
                </div>
                <!-- /.row -->

                <div class="jumbotron">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="input-group" role="group" aria-label="..." style="width:100%">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-mobile-phone"></span> ເບີໂທ</span>
                                            <input type="text" runat="server" id="txtTelephone" class="form-control" placeholder="205xxxxxxx" maxlength="10" onkeypress="return allowOnlyNumber(event);"/>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="col-md-2">
                            <uc1:EDatePicker runat="server" ID="EDatePicker1" />
                        </div>
                        <div class="col-md-2">
                            <uc1:EDatePicker2 runat="server" ID="EDatePicker2" />
                        </div>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <button runat="server" id="btnSearch" type="button" class="btn btn-default" style="width:100%"><i class="fa fa-search"></i> ສະແດງຂໍ້ມູນ</button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    
                            <p></p>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <h3><i class="fa fa-calendar"></i> <asp:Label ID="lbTypeHeader" runat="server" Text="ປະຫວັດການຮັບໂປຣໂມເຊິນ"></asp:Label> <small>( ຕາມຊ່ວງວັນທີ <asp:Label ID="lbDate1" runat="server" Text="Label"></asp:Label> ຫາ <asp:Label ID="lbDate2" runat="server" Text="Label"></asp:Label> )</small></h3>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                            <div class="row">
                                <%--<div class="col-md-2"></div>--%>
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <uc1:GridControl runat="server" ID="gridHistory" GirdPageSize="10" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <p></p>
                            <div class="row"></div>
                            <p></p>
                        </div>
                        <%--jumbotron--%>
               
            </div>
            <!-- /#page-wrapper -->
        </div>
        <!-- /#wrapper -->
    </form>
</body>
</html>