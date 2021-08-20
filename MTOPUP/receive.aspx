<%@ Page Language="VB" AutoEventWireup="false" CodeFile="receive.aspx.vb" Inherits="receive" %>


<%@ Register Src="~/Lo_GridControl.ascx" TagPrefix="uc1" TagName="Lo_GridControl" %>
<%@ Register Src="~/EDatePicker.ascx" TagPrefix="uc1" TagName="EDatePicker" %>
<%@ Register Src="~/GridControl.ascx" TagPrefix="uc1" TagName="GridControl" %>





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
        function closeModalSave() {
            $("#myModalSave").modal("hide");
            $("#myModalDiscount").modal("hide");
        }
        function myModalSave() {
            $("#myModalSave").modal("show");
        }
        function modalSelectMember() {
            $("#modalSelectMember").modal("show");
        }
        function closeModalDiscount() {
            $("#myModalDiscount").modal("hide");
        }
        function myModalDiscount() {
            $("#myModalDiscount").modal("show");
        }

        function closeModalProcess() {
            $("#myModalProcess").modal("hide");
        }
        function myModalProcess() {
            $("#myModalProcess").modal("show");
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



            <!-- myModalDiscount -->
            <div class="modal fade" runat="server" id="myModalDiscount" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H2">ຄິດໄລ່ສ່ວນຫຼຸດ</h4>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <blockquote>
                                        <p>ເງິນທີ່ຕ້ອງຈ່າຍ: <asp:Label ID="lbRealPaid" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label> ກີບ</p>
                                        <footer>ໜີ້ທີ່ຕ້ອງການຊຳລະ: <asp:Label ID="lbNewCredit" runat="server"></asp:Label> ກີບ</footer>
                                        <footer>ສ່ວນຫຼຸດ: <asp:Label ID="lbPercent" runat="server"></asp:Label> %</footer>
                                        <footer>ສ່ວນຫຼຸດ (ເງິນ): <asp:Label ID="lbDiscount" runat="server"></asp:Label> ກີບ</footer>
                                    </blockquote>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <button id="Button5" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                                    <button id="btnSubmit" runat="server" type="button" class="btn btn-primary">ຈ່າຍ</button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <!-- myModalDiscount -->


            <!-- myModalSave -->
            <div class="modal fade" runat="server" id="myModalSave" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">ຢືນຢັນການບັນທຶກ</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການຊຳລະເງິນໃຫ້ເບີ
                                <asp:Label ID="lbTel" runat="server" ForeColor="red"></asp:Label>
                                        ເປັນຈຳນວນເງິນ
                                        <asp:Label ID="lbNewMoney" runat="server" ForeColor="red"></asp:Label> ກີບ 
                                        ແທ້ຫຼືບໍ່ ?
                                    </p>
                                    <p>ປ້ອນລະຫັດຜ່ານຢືນຢັນໂຕຕົນແລ້ວກົດ "ຕົກລົງ" ເພື່ອທຳການບັນທຶກ.</p>
                                    <p>
                                        <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-key"></span></span>
                                        <input type="password" runat="server" id="txtConfirmPassword" class="form-control" placeholder="ປ້ອນລະຫັດຜ່ານ .." autocomplete="off" />
                                    </div>
                                    </p>
                                    <p>
                                        <div id="alertPasswordEmpty" runat="server" class="alert alert-warning alert-dismissable">
                                            <button runat="server" id="Button2" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            ກະລຸນາປ້ອນລະຫັດຜ່ານກ່ອນ.
                                        </div>
                                        <div id="alertPassword" runat="server" class="alert alert-danger alert-dismissable">
                                            <button runat="server" id="Button1" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <b>ຜິດພາດ : </b>ລະຫັດຜ່ານບໍ່ຖືກຕ້ອງ
                                        </div>
                                    </p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <button id="btCancelSave2" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                                    <button id="btConfirmSave2" runat="server" type="button" class="btn btn-primary">ຕົກລົງ</button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <!-- myModalSave -->





             <!-- modalSelectMember -->
            <div class="modal fade" runat="server" id="modalSelectMember" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H1">ເລືອກເບີທີ່ຕ້ອງການຊຳລະເງິນ</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="jumbotron">
                                        <div class="row" role="search">
                                            <div class="col-md-5">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtSearchMember" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ .."></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button runat="server" id="btnSearchMember" type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                                                            </span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <p></p>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <uc1:Lo_GridControl runat="server" ID="gridMemberModal" GirdPageSize="10000" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                       <%-- <div class="modal-footer">
                            <button id="Button1" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                            <button id="Button2" runat="server" type="button" class="btn btn-primary">ຕົກລົງ</button>
                        </div>--%>
                    </div>
                </div>
            </div>
            <!-- modalSelectMember -->



            <!-- myModalProcess -->
                    <div class="modal" runat="server" id="myModalProcess" data-backdrop="static" data-keyboard="false">
                        <div class="modal-body">
                            <div class="col-md-12" style="text-align: center; margin-top: 130px;">
                                <img src="images/loading3.gif" />
                            </div>
                        </div>
                    </div>
            <!-- myModalProcess -->

            <!-- myModalProcess -->
                    <div class="modal" runat="server" id="Div1" data-backdrop="static" data-keyboard="false">
                       
                    </div>
            <!-- myModalProcess -->
           




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
                        <li runat="server" id="menuUserCtrl" class="active"><a href="userctrl.aspx"><i class="fa fa-user"></i>ພະນັກງານຂາຍ</a></li>
                        <li runat="server" id="menuRecharge"><a href="recharge.aspx"><i class="fa fa-share-square"></i> ເຕີມເງິນ</a></li>
                        <li runat="server" id="menuReceive" class="active"><a href="receive.aspx"><i class="fa fa-check-square"></i> ຮັບເງິນ</a></li>
                        <li runat="server" id="menuReprint"><a href="reprint.aspx"><i class="fa fa-print"></i> ພິມບິນຍ້ອນຫຼັງ</a></li>
                        <li class="dropdown" runat="server" id ="cbManage">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i> ຈັດການຂໍ້ມູນ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuApprove"><a href="approveUser.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນຜູ້ອະນຸມັດ</a></li>
                                <li runat="server" id="menuUser"><a href="user.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນຜູ້ໃຊ້ລະບົບ</a></li>
                                <li runat="server" id="menuBranch"><a href="branch.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນກຸ່ມ</a></li>
                                <li runat="server" id="menuDiscount"><a href="discount.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການສ່ວນຫຼຸດ ( Discount )</a></li>
                                <li runat="server" id="menuDiscountSale"><a href="discountSale.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການໂບນັດ ( Bonus )</a></li>
                                <li runat="server" id="menuPromotion"><a href="promotion.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນໂປຣ ( Promotion )</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" runat="server" id="cbReport">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i> ລາຍງານ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuRechargeHistory"><a href="rechargeHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການເຕີມເງິນ</a></li>
                                <li runat="server" id="menuProHis"><a href="proHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການໃຫ້ໂປຣ</a></li>
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
                        <h1>ຮັບເງິນ <small>ຂໍ້ມູນການຮັບເງິນ</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-check-square"></i> ຮັບເງິນ</li>
                        </ol>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <div id="alertSaveSucess" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="btnAlertSuccess" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <b>ສຳເລັດ : </b>ການຊຳລະໜີ້ໃຫ້ກັບໝາຍເລກ
                            <asp:Label ID="lbTelephone2" runat="server" Font-Bold="true" Text=""></asp:Label> ເປັນຈຳນວນເງິນ <asp:Label ID="lbNewMoney2" runat="server" Font-Bold="true" Text=""></asp:Label> ກີບ 
                                    ສຳເລັດ. ( <asp:Label ID="lbOldMoney" runat="server" Font-Bold="true" Text=""></asp:Label> - <asp:Label ID="lbNewMoney3" runat="server" Font-Bold="true" Text=""></asp:Label> = <asp:Label ID="lbUpdateMoney" runat="server" Font-Bold="true"></asp:Label> ກີບ. ) - ( ເລກທີໃບຮັບເງິນ : <asp:Label ID="lbInvoiceID" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Large"></asp:Label> ) 
                                    <%--<asp:Button ID="btnPrintBill" runat="server" Text="ພິມບິນ" CssClass="btn btn-default"/>--%>
                                    <div class="btn-group">
                                        <button runat="server" id="btnPrintBill" type="button" class="btn btn-success">
                                            <span class="glyphicon glyphicon-print"></span> ພິມບິນ
                                        </button>
                                    </div>
                                </div>
                                <div id="alertSaveError" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <b>ຜິດພາດ : </b>ການຊຳລະໜີ້ໃຫ້ກັບໝາຍເລກ
                            <asp:Label ID="lbTel2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ບໍ່ສຳເລັດ !! <asp:Label ID="lbError" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </div>
                                <div id="alertSelectMember" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertSelectMember" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາເລືອກເບີທີ່ຕ້ອງການຊຳລະໜີ້ກ່ອນ !!
                                </div>
                                <div id="alertNewMoney" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertNewMoney" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນນຳນວນເງິນທີ່ຕ້ອງການຊຳລະກ່ອນ !!
                                </div>
                                <div id="alertOverMoney" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertOverMoney" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ຈຳນວນເງິນທີ່ຊຳລະ ເກີນຈຳນວນເງິນໜີ້, ກະລຸນາກວດສອບຄືນໃໝ່ !!
                                </div>
                                <div id="alertNoData" runat="server" class="alert alert-info alert-dismissable">
                                    <button runat="server" id="btnAlertNoData" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ບໍ່ມີຂໍ້ມູນໜີ້ຂອງໝາຍເລກ <asp:Label ID="lbTel3" runat="server" Font-Bold="true" Text=""></asp:Label> !!
                                </div>
                                <div id="alertGetCreditFail" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertGetCredit" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <b>ຜິດພາດ</b> : ບໍ່ສາມາດດຶງເອົາຂໍ້ມູນໜີ້ມາສະແດງໄດ້ !! <asp:Label ID="lbError2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                 <asp:PostBackTrigger ControlID="btnPrintBill" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <!-- /.row -->


                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <%--<div class="col-md-2"></div>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-btn">
                                                <button runat="server" id="btnTelephone" type="button" class="btn btn-info">ເລືອກເບີ ( ຊຳລະເງິນ )</button>
                                            </span>
                                            <input runat="server" id="txtTelephone" type="text" class="form-control" placeholder="ເບີທີ່ຕ້ອງການຈ່າຍເງິນ .." readonly="true" />
                                        </div>
                                    </div>
                               
                            

                            <div class="col-md-4">
                                 <div class="input-group">
                                <span class="input-group-addon" ><span class="fa fa-user" ></span> (ຊື່ສະມາຊິກ)</span>
                                <input type="text" runat="server" id="txtMemberNameMT" class="form-control" placeholder="ຊື່ສະມາຊິກ .." autocomplete="off" readonly="true"/>
                            </div>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="fa fa-money"></span> (ລວມໜີ້ທີ່ຕ້ອງຈ່າຍ)</span>
                                    <input type="text" runat="server" id="txtOldCredit" class="form-control" placeholder="ຈຳນວນເງິນທີ່ຕ້ອງຈ່າຍ .." autocomplete="off" readonly="true" />
                                </div>
                            </div>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <p></p>

                        <div class="row">
                           <%-- <div class="col-md-4"></div>--%>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-money"></span> (ຊຳລະ)</span>
                                            <input type="text" runat="server" id="txtNewCredit" class="form-control" placeholder="ຈຳນວນເງິນທີ່ຕ້ອງການຊຳລະ .." autocomplete="off" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return allowOnlyNumber(event);" maxlength="15" />
                                            <span class="input-group-btn">
                                                <button runat="server" id="btnCheckDiscount" type="button" class="btn btn-primary">ຄິດໄລ່</button>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                    <div class="btn-group">
                                        <button runat="server" id="btCancel" type="button" class="btn btn-warning">
                                            <span class="fa fa-times"></span> ຍົກເລີກ
                                        </button>
                                    </div>
                               
                            </div>
                                </ContentTemplate>
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btCancel" />
                                     </Triggers>
                         </asp:UpdatePanel>
                        </div>

                        <p></p>


                        <div class="jumbotron">
                            <div class="row">
                                <%--<div class="col-md-2"></div>--%>
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <%--<uc1:GridControl runat="server" ID="gridCredit" GirdPageSize="10000000" />--%>
                                            <uc1:Lo_GridControl runat="server" ID="gridCredit" GirdPageSize="10000000" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <p></p>
                            <div class="row"></div>
                            <p></p>
                        </div>
                        <%--jumbotron--%>

                        <div class="row" runat="server" id="hide" visible="false">
                            <div class="col-md-12">
                                <uc1:GridControl runat="server" ID="gridDiscount" GirdPageSize="100000" />
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtInvoiceID" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>


                
            </div>
            <!-- /#page-wrapper -->
        </div>
        <!-- /#wrapper -->
    </form>
</body>
</html>

