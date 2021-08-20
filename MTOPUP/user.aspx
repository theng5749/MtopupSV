﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user.aspx.vb" Inherits="user" %>

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
        function closeModalRoot() {
            $("#modalSelectRoot").modal("hide");
        }
        function closeModalBranch() {
            $("#modalSelectBranch").modal("hide");
        }
        function myModalSave() {
            $("#myModalSave").modal("show");
        }
        function modalSelectRoot() {
            $("#modalSelectRoot").modal("show");
        }
        function modalSelectBranch() {
            $("#modalSelectBranch").modal("show");
        }
        function myModalSave() {
            $("#myModalSave").modal("show");
        }
        function myModalDelete() {
            $("#myModalDelete").modal("show");
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
                            <h4 class="modal-title" id="myModalLabel">ຢືນຢັນການບັນທຶກ</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການບັນທຶກຂໍ້ມູນຂອງ <asp:Label ID="lbFirstName" runat="server" ForeColor="red" Font-Bold="true"></asp:Label> ແທ້ຫຼືບໍ່ ?
                                    </p>
                                    <p>ກົດ "ຕົກລົງ" ເພື່ອຢືນຢັນ ຫຼື້ກົດ "ຍົກເລີກ" ເພື່ອຍົກເລີກການບັນທຶກ.</p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <button id="btCancelSave" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                            <button id="btConfirmSave" runat="server" type="button" class="btn btn-primary">ຕົກລົງ</button>


                        </div>
                    </div>
                </div>
            </div>
            <!-- myModalSave -->
             <!-- myModalDelete -->
            <div class="modal fade" runat="server" id="myModalDelete" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H1">ຢືນຢັນການປິດ User</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການປິດ User ທີ່ທ່ານເລືອກແທ້ຫຼືບໍ່ ?
                                    </p>
                                    <p>ກົດ "ຕົກລົງ" ເພື່ອຢືນຢັນ ຫຼືກົດ "ຍົກເລີກ" ເພື່ອຍົກເລີກ.</p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <button id="btCancelDelete" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                            <button id="btConfirmDelete" runat="server" type="button" class="btn btn-primary">ຕົກລົງ</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- myModalDelete -->         
            <!-- modalSelectBranch -->
            <div class="modal fade" runat="server" id="modalSelectBranch" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">ເລືອກກຸ່ມ</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="jumbotron">
                                        <div class="row" role="search">
                                            <div class="col-md-8">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtSearchBranch" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ .."></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button runat="server" id="btnSearchBranch" type="button" class="btn btn-default">ຄົ້ນຫາ</button>
                                                            </span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <p></p>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <uc1:Lo_GridControl runat="server" ID="gridBranch" GirdPageSize="1000000" />
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
            <!-- modalSelectBranch -->         
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
                        <li class="dropdown active" runat="server" id ="cbManage">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i> ຈັດການຂໍ້ມູນ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuApprove"><a href="approveUser.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນຜູ້ອະນຸມັດ</a></li>
                                <li runat="server" id="menuUser" class="active"><a href="user.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນຜູ້ໃຊ້ລະບົບ</a></li>
                                <li runat="server" id="menuBranch"><a href="branch.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນກຸ່ມ</a></li>
                                <li runat="server" id="menuDiscount"><a href="discount.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການສ່ວນຫຼຸດ ( Discount )</a></li>
                                <li runat="server" id="menuDiscountSale"><a href="discountSale.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການໂບນັດ ( Bonus )</a></li>
                                <li runat="server" id="menuPromotion"><a href="promotion.aspx"><i class="fa fa-ellipsis-h"></i> ຈັດການຂໍ້ມູນໂປຣ ( Promotion )</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" runat="server" id="cbReport">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i> ລາຍງານ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuRechargeHistory"><a href="rechargeHistory.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການເຕີມເງິນ</a></li>
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
                       <%-- <li class="dropdown messages-dropdown">
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
                        <h1>ຂໍ້ມູນຜູ້ໃຊ້ <small>ຈັດການຂໍ້ມູນຜູ້ໃຊ້ລະບົບ</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-user"></i> ຂໍ້ມູນຜູ້ໃຊ້ລະບົບ</li>
                        </ol>
                        <asp:UpdateProgress ID="prgLoadingStatus" runat="server" DynamicLayout="true">
                            <ProgressTemplate>
                                <div id="overlay">
                                    <div id="modalprogress" style="height: 10000px">
                                        <div id="theprogress">
                                            <p style="height: 120px"></p>
                                            <p style="text-align: center">
                                                <asp:Image ID="Image1" runat="server" Height="50px" ImageUrl="~/Images/support-loading.gif" Width="50px" />
                                            </p>
                                            <p style="height: 10px"></p>
                                            <p style="text-align: center">
                                                <asp:Label ID="Label1" runat="server" Text="ກຳລັງດຳເນີນການ, ກະລຸນາລໍຖ້າ" Font-Size="Large" ForeColor="Gray"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div id="alertSaveSucess" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="btnAlertSuccess" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການບັນທຶກຂໍ້ມູນຂອງ
                            <asp:Label ID="lbAlertName" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ສຳເລັດ
                                </div>
                                <div id="alertDelete" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="btnAlertDelete" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ປິດ User ສຳເລັດ. ຈຳນວນ User ທັງໝົດທີ່ຖືກປິດແມ່ນ <b><asp:Label ID="lbCountUser" runat="server" Font-Bold="true" Text="" ForeColor="Red"></asp:Label></b> User !!
                                </div>
                                <div id="alertSaveError" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການບັນຂໍ້ມູນູຂອງ
                            <asp:Label ID="lbAlertName2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ບໍ່ສຳເລັດ, username: " <asp:Label ID="lbAlertUser" runat="server" Font-Bold="true" Text=""></asp:Label> " ມີຢູ່ແລ້ວໃນລະບົບ.   ( ກະລຸນາປ່ຽນຊື່ເຂົ້າລະບົບໃໝ່ )
                                </div>
                                <div id="alertFirstName" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertFirstName" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນຊື່ ກ່ອນການບັນທຶກ !!
                                </div>
                                 <div id="alertUsername" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertUsername" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນຊື່ເຂົ້າລະບົບ ກ່ອນການບັນທຶກ !!
                                </div>
                                 <div id="alertPassword" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertPassword" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນລະຫັດຜ່ານ ກ່ອນການບັນທຶກ !!
                                </div>
                                <div id="alertBranch" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertBranch" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາເລືອກກຸ່ມກ່ອນການບັນທຶກ !!
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <!-- /.row -->

                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="input-group">
                                <span class="input-group-addon" ><span class="fa fa-user" ></span></span>
                                <input type="text" runat="server" id="txtFirstName" maxlength="30" class="form-control" placeholder="ປ້ອນຊື່ແທ້" autocomplete="off"/>
                            </div>
                            </div>
                             <div class="col-md-4">
                                <div class="input-group">
                                <span class="input-group-addon" ><span class="fa fa-user" ></span></span>
                                <input type="text" runat="server" id="txtUserNameMT" maxlength="15" class="form-control" placeholder="ປ້ອນຊື່ເຂົ້າລະບົບ" autocomplete="off"/>
                            </div>
                            </div>
                             <div class="col-md-4">
                                 <div class="input-group">
                                     <span class="input-group-addon" ><span class="fa fa-phone" ></span></span>
                                     <input type="text" runat="server" id="txttelephone" maxlength="10" class="form-control" placeholder="ເບີໂທຕິດຕໍ່" autocomplete="off" />
                                    <%-- <span class="input-group-btn" runat="server" id="tabReset">
                                         <button runat="server" id="btResetPassword" type="button" class="btn btn-info">Reset</button>
                                     </span>--%>
                                 </div>
                                 
                            </div>
                        </div>
                        <p></p>
                        <div class="row">                           
                            <div class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="fa fa-sort-amount-asc" ></span></span>
                                    <asp:DropDownList ID="cbLevel" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="7">Call Center</asp:ListItem>
                                        <asp:ListItem Value="6">NOC</asp:ListItem>
                                        <asp:ListItem Value="8">ພະແນກການເງິນ</asp:ListItem>
                                        <asp:ListItem Value="0">ຜູ້ບໍລິຫານລະບົບ ( Administrator )</asp:ListItem>
                                        <asp:ListItem Value="9">ຜູ້ບໍລິຫານລະບົບ ລະດັບສູງ ( Super Administrator )</asp:ListItem>
                                        <asp:ListItem Value="11">CS Sale</asp:ListItem>
                                        <asp:ListItem Value="12">Super</asp:ListItem>
                                        <asp:ListItem Value="13">Finance</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4" runat="server" id="tabBranch">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div runat="server" id="divBranch" class="input-group">
                                            <span class="input-group-btn">
                                                <button runat="server" id="btnBranch" type="button" class="btn btn-info">ເລືອກກຸ່ມ</button>
                                            </span>
                                            <input runat="server" id="txtBranchName" type="text" class="form-control" placeholder="ຂໍ້ມູນຂອງກຸ່ມ" readonly="true" />
                                            <input runat="server" id="txtBranchID" type="text" class="form-control" visible="false" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group">
                                     <span class="input-group-addon"><span class="fa fa-align-left" ></span></span>
                                     <asp:TextBox ID="txtUserDes" runat="server" CssClass="form-control" placeholder="ຂໍ້ມູນລາຍລະອຽດກ່ຽວກັບຜູ້ໃຊ້"></asp:TextBox>
                                 </div>
                            </div>
                        </div>
                        <p></p>
                        <div class="row">
                            <div class="col-md-4">
                                 <asp:CheckBox ID="cbActive" Text="Active" runat="server" CssClass="checkbox-inline" Checked="true" />
                            </div>
                        </div>
                        <p></p>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="btn-group btn-group-justified">
                                            <div class="btn-group">
                                                <button runat="server" id="btnSave" type="button" class="btn btn-success">
                                                    <span class="fa fa-save"></span> ບັນທຶກຂໍ້ມູນຜູ້ໃຊ້
                                                </button>
                                            </div>
                                            <div class="btn-group">
                                                <button runat="server" id="btnDelete" type="button" class="btn btn-danger">
                                                    <span class="fa fa-times"></span> ປິດ User ໃຊ້ທີ່ຖືກເລືອກ ( 
                                            <span class="fa fa-check"></span> )
                                                </button>
                                            </div>
                                            <div class="btn-group">
                                                <button runat="server" id="btCancel" type="button" class="btn btn-warning">
                                                    <span class="fa fa-refresh"></span> Refresh
                                                </button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btCancel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <p></p>
                        <div class="row"></div>
                        <p></p>

                        
                        <div class="jumbotron">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                       <asp:TextBox ID="txtSearchText" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button runat="server" id="btnSearch" type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<uc1:GridControl runat="server" ID="gridUser" />--%>
                                    <uc1:Lo_GridControl runat="server" ID="gridUser" GirdPageSize="1000000" />
                                </div>
                            </div>
                            <div class="row" runat="server" id="statusEdit"></div>
                        </div><!-- /#jumbotron -->
                    </div>
                </div>

            </div>
            <!-- /#page-wrapper -->
        </div>
        <!-- /#wrapper -->
    </form>
</body>
</html>

