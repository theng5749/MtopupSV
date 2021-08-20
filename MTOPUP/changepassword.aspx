﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="changepassword.aspx.vb" Inherits="changepassword" %>


<%@ Register Src="~/Lo_GridControl.ascx" TagPrefix="uc1" TagName="Lo_GridControl" %>
<%@ Register Src="~/EDatePicker.ascx" TagPrefix="uc1" TagName="EDatePicker" %>




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
        function myModalSave() {
            $("#myModalSave").modal("show");
        }
    </script>



</head>

<body>
    <form id="form1" runat="server" style="font-family: 'Phetsarath OT'">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="wrapper">

             <!-- myModalSave -->
            <div class="modal fade" runat="server" id="myModalSave" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">ຢືນຢັນການປ່ຽນລະຫັດຜ່ານ</h4>
                        </div>
                        <div class="modal-body">
                            <p>
                                ທ່ານຕ້ອງການປ່ຽນລະຫັດຜ່ານແທ້ຫຼືບໍ່ ?
                            </p>
                            <p>ກົດ "ຕົກລົງ" ເພື່ອຢືນຢັນ ຫຼືກົດ "ຍົກເລີກ" ເພື່ອຍົກເລີກການບັນທຶກ.</p>
                        </div>
                        <div class="modal-footer">
                            <button id="btCancelSave2" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                            <button id="btConfirmSave2" runat="server" type="button" class="btn btn-primary">ຕົກລົງ</button>


                        </div>
                    </div>
                </div>
            </div>
            <!-- myModalSave -->


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
                                <li runat="server" id="menuProHis"><a href="proHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການໃຫ້ໂປຣ</a></li>
                                <li runat="server" id="menuHistory"><a href="history.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການໂອນ</a></li>
                                <li runat="server" id="menuReceiveHistory"><a href="receiveHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການຮັບເງິນ</a></li>
                                <li runat="server" id="menuHistoryL1"><a href="rechargeHistoryL1.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການເຕີມເງິນ ( L 1 )</a></li>
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
                        <h1>ປ່ຽນລະຫັດຜ່ານ <small>ປ່ຽນລະຫັດຜ່ານເຂົ້າສູ່ລະບົບ</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-user"></i> ປ່ຽນລະຫັດຜ່ານ</li>
                        </ol>
                    </div>
                </div>
                <!-- /.row -->

                <div class="jumbotron">
                     <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-4">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div id="alertSaveSucess" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="btnAlertSuccess" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການປ່ຽນລະຫັດຜ່ານ ສຳເລັດ
                                </div>
                                <div id="alertSaveError" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການປ່ຽນລະຫັດຜ່ານບໍ່ສຳເລັດ, ລະຫັດຜ່ານເກົ່າບໍ່ຖືກຕ້ອງ !!
                                </div>
                                <div id="alertPasswrodNotMatch" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertNotMatch" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການປ່ຽນລະຫັດຜ່ານບໍ່ສຳເລັດ, ລະຫັດຜ່ານໃໝ່ບໍ່ຕົງກັນ !!
                                </div>
                                <div id="alertOldPassword" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertOldPassword" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນລະຫັດຜ່ານ ກ່ອນການບັນທຶກ !!
                                </div>
                                <div id="alertNewPassword" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertNewPassword" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນລະຫັດຜ່ານໃໝ່ ກ່ອນການບັນທຶກ !!
                                </div>
                                <div id="alertConfirmPassword" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertConfirmPassword" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາຢືນຢັນລະຫັດຜ່ານໃໝ່ ກ່ອນການບັນທຶກ !!
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    </div>

                    <p></p>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="fa fa-user"></span></span>
                                    <input type="text" runat="server" id="txtUsername" class="form-control" placeholder="ຊື່ເຂົ້າລະບົບ .." autocomplete="off" readonly="true" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <p></p>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="fa fa-lock"></span></span>
                                    <input type="password" runat="server" id="txtOldPasswrod" class="form-control" placeholder="ລະຫັດຜ່ານເກົ່າ .." autocomplete="off" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <p></p>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="fa fa-lock"></span></span>
                                    <input type="password" runat="server" id="txtNewPassword" class="form-control" placeholder="ລະຫັດຜ່ານໃໝ່ .." autocomplete="off" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <p></p>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="fa fa-check"></span></span>
                                    <input type="password" runat="server" id="txtConfirmPassword" class="form-control" placeholder="ຢືນຢັນລະຫັດຜ່ານໃໝ່ .." autocomplete="off" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <p></p>


                     <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="btn-group btn-group-justified">
                                            <div class="btn-group">
                                                <button runat="server" id="btnSave" type="button" class="btn btn-success" width="73%">
                                                    <span class="fa fa-save"></span> ບັນທຶກ
                                                </button>
                                            </div>
                                            <div class="btn-group">
                                                <button runat="server" id="btCancel" type="button" class="btn btn-warning" width="73%">
                                                    <span class="fa fa-times"></span> ຍົກເລີກ
                                                </button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                

                            </div>
                        </div>
                    </div>
                </div>
                <%--jumbotron--%>


              
            </div>
            <!-- /#page-wrapper -->
        </div>
        <!-- /#wrapper -->
    </form>
</body>
</html>

