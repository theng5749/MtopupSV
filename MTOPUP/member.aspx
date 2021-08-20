<%@ Page Language="VB" AutoEventWireup="false" CodeFile="member.aspx.vb" Inherits="member" %>

<%@ Register Src="~/Lo_GridControl.ascx" TagPrefix="uc1" TagName="Lo_GridControl" %>
<%@ Register Src="~/EDatePicker.ascx" TagPrefix="uc1" TagName="EDatePicker" %>
<%@ Register Src="~/GridControl.ascx" TagPrefix="uc1" TagName="GridControl" %>
<%@ Register Src="~/Nick_GridControl.ascx" TagPrefix="uc1" TagName="Nick_GridControl" %>
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
    <%--    <script src="link/morris-0.4.3.min.js"></script>
    <script src="js/morris/chart-data-morris.js"></script>--%>
    <script src="js/tablesorter/jquery.tablesorter.js"></script>
    <script src="js/tablesorter/tables.js"></script>
    <script type="text/javascript">
        function closeModalRoot() {
            $("#modalSelectRoot").modal("hide");
        }
        function closeModalBranch() {
            $("#modalSelectBranch").modal("hide");
        }
        function closeModalBranchPlus() {
            $("#modalSelectBranchPlus").modal("hide");
        }
        function myModalSave() {
            $("#myModalSave").modal("show");
        }
        function myModalDelete() {
            $("#myModalDelete").modal("show");
        }
        function modalSelectRoot() {
            $("#modalSelectRoot").modal("show");
        }
        function modalSelectBranch() {
            $("#modalSelectBranch").modal("show");
        }
        function modalSelectBranchPlus() {
            $("#modalSelectBranchPlus").modal("show");
        }
        function modalSavePlus() {
            $("#modalSavePlus").modal("show");
        }
        function modalUpdateMemberPlus() {
            $("#modalUpdateMemberPlus").modal("show");
        }
    </script>
    <script type="text/javascript">
        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
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
                                        ທ່ານຕ້ອງການບັນທຶກຂໍ້ມູນຂອງເບີ
                                        <asp:Label ID="lbTelSave" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                        ແທ້ຫຼືບໍ່ ?
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
                            <h4 class="modal-title" id="H1">ຢືນຢັນການປິດເບີ</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການປິດເບີສະມາຊິກທີ່ທ່ານເລືອກແທ້ຫຼືບໍ່ ?
                                    </p>
                                    <p>ກົດ "ຕົກລົງ" ເພື່ອຢືນຢັນ ຫຼື້ກົດ "ຍົກເລີກ" ເພື່ອຍົກເລີກ.</p>
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
            <!-- modalSelectRoot -->
            <div class="modal fade" runat="server" id="modalSelectRoot" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H1">ເລືອກເບີແມ່</h4>
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
                                                            <asp:TextBox ID="txtSearchRoot" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ .."></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button runat="server" id="btnSearchRoot" type="button" class="btn btn-default">ຄົ້ນຫາ</button>
                                                            </span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <p></p>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <uc1:Lo_GridControl runat="server" ID="gridRootModal" GirdPageSize="1000000" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- modalSelectRoot -->
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
            <!-- modalSelectBranch -->
            <div class="modal fade" runat="server" id="modalSelectBranchPlus" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
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
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtSearchBranchPlus" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ .."></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button runat="server" id="btnSearchMemberPlus" type="button" class="btn btn-default">ຄົ້ນຫາ</button>
                                                            </span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <p></p>
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <uc1:Lo_GridControl runat="server" ID="GridBranchPlus" GirdPageSize="1000000" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- modalSelectBranch -->
            <!-- myModalSave Plus -->
            <div class="modal fade" runat="server" id="modalSavePlus" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel2">ຢືນຢັນການບັນທຶກ Save</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການບັນທຶກຂໍ້ມູນຂອງເບີ
                                        <asp:Label ID="lbTelPlus" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                        ແທ້ຫຼືບໍ່ ?
                                    </p>
                                    <p>ກົດ "ຕົກລົງ" ເພື່ອຢືນຢັນ ຫຼື້ກົດ "ຍົກເລີກ" ເພື່ອຍົກເລີກການບັນທຶກ.</p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <button id="btnCancelSavePlus" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                            <button id="btnConfirmSavePlus" runat="server" type="button" class="btn btn-primary">ຕົກລົງ</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- myModalSave -->
             <!-- myModalSave Plus -->
            <div class="modal fade" runat="server" id="modalUpdateMemberPlus" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel2">ຢືນຢັນການບັນທຶກ update</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການແກ້ໄຂ້ຂໍ້ມູນຂອງເບີ
                                        <asp:Label ID="lbTelUpdate" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                                        ແທ້ຫຼືບໍ່ ?
                                    </p>
                                    <p>ກົດ "ຕົກລົງ" ເພື່ອຢືນຢັນ ຫຼື້ກົດ "ຍົກເລີກ" ເພື່ອຍົກເລີກການບັນທຶກ.</p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer">
                            <button id="btnCancelUpdatePlus" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                            <button id="btnConfirmUpdatePlus" runat="server" type="button" class="btn btn-primary">ຕົກລົງ</button>
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
                    <a class="navbar-brand">M-TOPUP</a>
                </div>
                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse navbar-ex1-collapse">
                    <ul class="nav navbar-nav side-nav">
                        <%--<li runat="server" id="menuMainpage"><a  ><i class="fa fa-desktop"></i> ໜ້າຫຼັກ</a></li>--%>
                        <li runat="server" id="menuMember" class="active"><a href="member.aspx"><i class="fa fa-pencil-square"></i>ຂໍ້ມູນສະມາຊິກ</a></li>
                        <li runat="server" id="menuUserCtrl"><a href="userctrl.aspx"><i class="fa fa-user"></i>ພະນັກງານຂາຍ</a></li>
                        <li runat="server" id="menuRecharge"><a href="recharge.aspx"><i class="fa fa-share-square"></i>ເຕີມເງິນ</a></li>
                        <li runat="server" id="menuReceive"><a href="receive.aspx"><i class="fa fa-check-square"></i>ຮັບເງິນ</a></li>
                        <li runat="server" id="menuReprint"><a href="reprint.aspx"><i class="fa fa-print"></i>ພິມບິນຍ້ອນຫຼັງ</a></li>
                        <li class="dropdown" runat="server" id="cbManage">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i>ຈັດການຂໍ້ມູນ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuApprove"><a href="approveUser.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນຜູ້ອະນຸມັດ</a></li>
                                <li runat="server" id="menuPercentage"><a href="managePercentage.aspx"><i class="fa fa-ellipsis-h"></i> ກຳນົດເປີເຊັນ</a></li>
                                <li runat="server" id="menuUser"><a href="user.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນຜູ້ໃຊ້ລະບົບ</a></li>
                                <li runat="server" id="menuBranch"><a href="branch.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນກຸ່ມ</a></li>
                                <li runat="server" id="menuDiscount"><a href="discount.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການສ່ວນຫຼຸດ ( Discount )</a></li>
                                <li runat="server" id="menuDiscountSale"><a href="discountSale.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການໂບນັດ ( Bonus )</a></li>
                                <li runat="server" id="menuPromotion"><a href="promotion.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນໂປຣ ( Promotion )</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" runat="server" id="cbReport">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i>ລາຍງານ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                 <li runat="server" id="menuRechargeHistory"><a href="rechargeHistory.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການເຕີມເງິນ</a></li>
                                <li runat="server" id="menuProHis"><a href="proHistory.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການໃຫ້ໂປຣ</a></li>
                                <li runat="server" id="menuHistory"><a href="history.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການໂອນ</a></li>
                                <li runat="server" id="menuReceiveHistory"><a href="receiveHistory.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການຮັບເງິນ</a></li>
                                <li runat="server" id="menuHistoryL1"><a href="rechargeHistoryL1.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການເຕີມເງິນ ( L 1 )</a></li>
                                <li runat="server" id="menuCheckApprove"><a href="checkApprove.aspx"><i class="fa fa-calendar"></i>ກວດສອບການອະນຸມັດ</a></li>
                                <li runat="server" id="menuCredit"><a href="credit.aspx"><i class="fa fa-dollar"></i>ໜີ້ຄ້າງຊຳລະ</a></li>
                                <li runat="server" id="menuInvoice"><a href="invoice.aspx"><i class="fa fa-dollar"></i>ສະຫຼຸບຍອດເງິນທີ່ໄດ້ຮັບ</a></li>
                            </ul>
                        </li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right navbar-user">
                        <li class="dropdown user-dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-user"></i>
                                <asp:Label ID="lbUserName" runat="server" Text=""></asp:Label>
                                <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="changepassword.aspx"><i class="fa fa-user"></i>ປ່ຽນລະຫັດຜ່ານ</a></li>
                                <li class="divider"></li>
                                <li><a href="Default.aspx"><i class="fa fa-power-off"></i>ອອກຈາກລະບົບ</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <!-- /.navbar-collapse -->
            </nav>
            <div id="page-wrapper">
                <div class="col-md-12">
                    <div class="btn-group btn-group-justified">
                        <div class="btn-group">
                            <button style="font-weight: 600; height: 100px; font-size: large" runat="server" id="btnMainOld" type="button" class="btn btn-success">
                                ຂໍ້ມູນສະມາຊິກ
                                    <br />
                                M-Topup<br />
                                (LTC & Partner)
                            </button>
                        </div>
                        <div class="btn-group">
                            <button style="font-weight: 600; height: 100px; font-size: large" runat="server" id="btnMainNew" type="button" class="btn btn-danger">
                                ຂໍ້ມູນສະມາຊິກ
                                    <br />
                                M-Topup Plus
                                    <br />
                                (M-Services)
                            </button>
                        </div>
                    </div>
                </div>
                <div id="panelOldMTopup" runat="server" class="row">
                    <div class="col-lg-12">
                        <h1>ຂໍ້ມູນສະມາຊິກ <small>ຂໍ້ມູນສະມາຊິກ M-TOPUP</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-pencil-square"></i>ຂໍ້ມູນສະມາຊິກ</li>
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
                                    ການບັນທຶກຂໍ້ມູນສະມາຊິກເບີ
                            <asp:Label ID="lbTel" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ສຳເລັດ
                                </div>
                                <div id="alertSaveError" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການບັນທຶກຂໍ້ມູນສະມາຊິກເບີ
                            <asp:Label ID="lbTel2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ບໍ່ສຳເລັດ, ເບີນີ້ມີຢູ່ແລ້ວໃນລະບົບ !!
                                </div>
                                <div id="alertRoot" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertRoot" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາເລືອກເບີແມ່ ກ່ອນການບັນທຶກ !!
                                </div>
                                <div id="alertBranch" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertBranch" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາເລືອກກຸ່ມກ່ອນການບັນທຶກ !!
                                </div>
                                <div id="alertName" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertName" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນຊື່ ກ່ອນການບັນທຶກ !!
                                </div>
                                <div id="alertTel" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertTel" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ປ້ອນເບີທີ່ຈະໃຊ້ໃນລະບົບບໍ່ຖືກຕ້ອງ, ກະລຸນາກວດສອບຄືນໃໝ່ !!
                                </div>
                                <div id="alertContact" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertContact" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ປ້ອນເບີຕິດຕໍ່ບໍ່ຖືກຕ້ອງ, ກະລຸນາກວດສອບຄືນໃໝ່ !!
                                </div>
                                <div id="alertDelete" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="btnAlertDelete" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ສຳເລັດ. ຈຳນວນເບີສະມາຊິກທັງໝົດທີ່ຖືກປິດແມ່ນ <b>
                                        <asp:Label ID="lbCountUser" runat="server" Font-Bold="true" Text="" ForeColor="Red"></asp:Label></b> User !! s
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--MemberID, Level and group user--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-tag"></span></span>
                                        <input type="text" runat="server" id="txtMemberIDMT" class="form-control" placeholder="ລະຫັດສະມາຊິກ" readonly="true" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-sort-amount-asc"></span></span>
                                        <asp:DropDownList ID="cbLevel" runat="server" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="1">ລະດັບ 1 (Level 1)</asp:ListItem>
                                            <asp:ListItem Value="2">ລະດັບ 2 ( Level 2 )</asp:ListItem>
                                            <asp:ListItem Value="3">ລະດັບ 3 ( Level 3 )</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-4">
                                            <div runat="server" id="divRoot" class="input-group">
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btnRoot" type="button" class="btn btn-info">ເລືອກເບີແມ່</button>
                                                </span>
                                                <input runat="server" id="txtRootMT" type="text" class="form-control" placeholder="ຂໍ້ມູນຂອງເບີແມ່" readonly="true" />
                                                <input runat="server" id="txtRootMTID" type="text" class="form-control" visible="false" />
                                            </div>
                                            <p runat="server" id="divBranch2"></p>
                                            <div runat="server" id="divBranch" class="input-group">
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btnBranch" type="button" class="btn btn-info">ເລືອກກຸ່ມ</button>
                                                </span>
                                                <input runat="server" id="txtBranchName" type="text" class="form-control" placeholder="ຂໍ້ມູນຂອງກຸ່ມ" readonly="true" />
                                                <input runat="server" id="txtBranchID" type="text" class="form-control" visible="false" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <p></p>
                            <%--Member name, Address--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-user"></span></span>
                                        <input type="text" runat="server" id="txtMemberNameMT" class="form-control" placeholder="ຊື່ສະມາຊິກ" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-map-marker"></span></span>
                                        <input type="text" runat="server" id="txtMerberAddressMT" class="form-control" placeholder="ທີ່ຢູ່ສະມາຊິກ" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-credit-card"></span></span>
                                        <input type="text" runat="server" id="txtMemberNoIDMT" class="form-control" placeholder="ເລກທີບັດປະຊາຊົນ" autocomplete="off" />
                                    </div>
                                </div>

                            </div>
                            <p></p>
                            <%--Telephone-Mtopup, Contact Telephone--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-phone"></span></span>
                                        <input type="text" runat="server" id="txtTelMT" class="form-control" placeholder="ເບີທີ່ຈະໃຊ້ໃນລະບົບ" onkeypress="return allowOnlyNumber(event);" maxlength="10" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-phone"></span></span>
                                        <input type="text" runat="server" id="txtContact" class="form-control" placeholder="ເບີຕິດຕໍ່" onkeypress="return allowOnlyNumber(event);" maxlength="10" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-group"></span></span>
                                        <asp:DropDownList ID="cbMemberType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="LTC">LTC Lao Telecom</asp:ListItem>
                                            <asp:ListItem Value="PRN">PRN Partner</asp:ListItem>
                                            <asp:ListItem Value="SHP">SHP Shop</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <p></p>
                            <%--Activ--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:CheckBox ID="cbActive" runat="server" Checked="true" Text=" Active" CssClass="checkbox-inline" />
                                </div>
                                <p></p>
                                <%--Button Save, Close and Refresh--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <div class="btn-group btn-group-justified">
                                                    <div class="btn-group">
                                                        <button runat="server" id="btnSave" type="button" class="btn btn-success">
                                                            <span class="fa fa-save"></span>ບັນທຶກຂໍ້ມູນສະມາຊິກ
                                                        </button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <button runat="server" id="btnDelete" type="button" class="btn btn-danger">
                                                            <span class="fa fa-times"></span>ປິດເບີທີ່ຖືກເລືອກ ( 
                                            <span class="fa fa-check"></span>)
                                                        </button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <button runat="server" id="btCancel" type="button" class="btn btn-warning">
                                                            <span class="fa fa-refresh"></span>Refresh
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
                                <p style="height: 15px"></p>
                                <%--Tab Member All, L1, L2 and L3--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <ul class="nav nav-tabs">
                                            <li role="presentation" runat="server" id="menuAllLevel"><a href="#" runat="server" id="btnAllLevel"><span class="glyphicon glyphicon-user"></span>ສະມາຊິກທັງໝົດ</a></li>
                                            <li role="presentation" runat="server" id="menuL1"><a href="#" runat="server" id="btnL1"><span class="glyphicon glyphicon-user"></span>ສະມາຊິກ L1</a></li>
                                            <li role="presentation" runat="server" id="menuL2"><a href="#" runat="server" id="btnL2"><span class="glyphicon glyphicon-user"></span>ສະມາຊິກ L2</a></li>
                                            <li role="presentation" runat="server" id="menuL3"><a href="#" runat="server" id="btnL3"><span class="glyphicon glyphicon-user"></span>ສະມາຊິກ L3</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <%--Search and Export and Datagrid--%>
                                <div class="jumbotron">
                                    <div class="row" role="search">
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearchText" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btnSearch" type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-6"></div>
                                        <div class="col-md-2">
                                            <button runat="server" id="btnExport" type="button" class="btn btn-default" style="width: 100%"><i class="fa fa-file-text-o"></i>ບັນທຶກເປັນ Excel</button>
                                        </div>
                                    </div>
                                    <p></p>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <uc1:GridControl runat="server" ID="gridMember" />
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="statusEdit"></div>
                                    <div class="row" runat="server" id="pa" visible="false">
                                        <uc1:Nick_GridControl runat="server" ID="gridExport" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="panelNewMTopup" runat="server" class="row">
                    <div class="col-md-12">
                        <h1>M-TOPUP Plus <small>ຂໍ້ມູນສະມາຊິກ M-TOPUP Plus</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-pencil-square"></i>ຂໍ້ມູນສະມາຊິກ (M-Services)</li>
                        </ol>
                        <div runat="server" id="panelStampCode" class="input-group">
                            <span class="input-group-addon"><span class="fa fa-square"></span></span>
                            <input type="text" style="height: 60px" runat="server" id="txtstampcode" class="form-control" placeholder="Stamp Code" autocomplete="off" />
                        </div>
                        <hr />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true">
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
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <div id="AlertSaveSuccessPlus" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="Button1" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການບັນທຶກຂໍ້ມູນສະມາຊິກເບີ
                            <asp:Label ID="lbsavesucessplus" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ສຳເລັດ
                                </div>
                                <div id="AlertSaveFailPlus" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="Button2" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ການບັນທຶກຂໍ້ມູນສະມາຊິກເບີ
                            <asp:Label ID="lbsavefailplus" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ບໍ່ສຳເລັດ, ເບີນີ້ມີຢູ່ແລ້ວໃນລະບົບ !!
                                </div>
                                <div id="AlertEmptytxt" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="Button6" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ປ້ອນຂໍ້ມູນໃຫ້ຄົບຖ້ວນ, ກະລຸນາກວດສອບຄືນໃໝ່ !!
                                </div>
                                <div id="AlertStampCodeEmpty" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="Button7" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ປ້ອນ Stamp Code ກ່ອນ, ກະລຸນາກວດສອບຄືນໃໝ່ !!
                                </div>
                                 <div id="AlertSampCode" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="Button3" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    Stamp Code ບໍ່ຖືກຕ້ອງ, ກະລຸນາກວດສອບຄືນໃໝ່ !!
                                </div>
                                <div id="AlertDeletePlus" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="Button8" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ສຳເລັດ. ຈຳນວນເບີສະມາຊິກທັງໝົດທີ່ຖືກປິດແມ່ນ <b>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="" ForeColor="Red"></asp:Label></b> User !! s
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <%--MemberID, Level and group user--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-tag"></span></span>
                                        <input type="text" runat="server" id="txtMemberID" class="form-control" placeholder="ລະຫັດສະມາຊິກ" readonly="true" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-sort-amount-asc"></span></span>
                                        <asp:DropDownList ID="txtLevelPlus" runat="server" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="3">ລະດັບ 3 (Level 3)</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-4">
                                            <div runat="server" id="div2" class="input-group">
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btnGroupPlus" type="button" class="btn btn-info">ເລືອກກຸ່ມ</button>
                                                </span>
                                                <input runat="server" id="txtbranchNamePlus" type="text" class="form-control" placeholder="ຂໍ້ມູນຂອງກຸ່ມ" readonly="true" />
                                                <input runat="server" id="txtbranchIDPlus" type="text" class="form-control" visible="false" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <p></p>
                            <%--Member name, Address--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-user"></span></span>
                                        <input type="text" runat="server" id="txtUsernamePlus" class="form-control" placeholder="ຊື່ສະມາຊິກ" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-map-marker"></span></span>
                                        <input type="text" runat="server" id="txtAddressPlus" class="form-control" placeholder="ທີ່ຢູ່ສະມາຊິກ" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-credit-card"></span></span>
                                        <input type="text" runat="server" id="txtIDPlus" class="form-control" placeholder="ເລກທີບັດປະຊາຊົນ" autocomplete="off" />
                                    </div>
                                </div>

                            </div>
                            <p></p>
                            <%--Telephone-Mtopup, Contact Telephone--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-phone"></span></span>
                                        <input type="text" runat="server" id="txtMsisdnPlus" class="form-control" placeholder="ເບີທີ່ຈະໃຊ້ໃນລະບົບ" onkeypress="return allowOnlyNumber(event);" maxlength="10" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-phone"></span></span>
                                        <input type="text" runat="server" id="txtContactPlus" class="form-control" placeholder="ເບີຕິດຕໍ່" onkeypress="return allowOnlyNumber(event);" maxlength="10" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-group"></span></span>
                                        <asp:DropDownList ID="txtPartnerPlus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="MPLUS">M-Topup Plus</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <p></p>
                            <%--Activ--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:CheckBox ID="txtActivePlus" runat="server" Checked="true" Text=" Active" CssClass="checkbox-inline" />
                                </div>
                                <p></p>
                                <%--Button Save, Close and Refresh--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <div class="btn-group btn-group-justified">
                                                    <div class="btn-group">
                                                        <button runat="server" id="btnSavePlus" type="button" class="btn btn-success">
                                                            <span class="fa fa-save"></span>ບັນທຶກຂໍ້ມູນສະມາຊິກ
                                                        </button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <button runat="server" id="btnUpdatePlus" type="button" class="btn btn-danger">
                                                            </span>ອັບເດດຂໍ້ມູນສະມາຊິກ (<span class="fa fa-check"></span>)
                                                        </button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <button runat="server" id="btnRefreshPlus" type="button" class="btn btn-warning">
                                                            <span class="fa fa-refresh"></span>Refresh
                                                        </button>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnRefreshPlus" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <p style="height: 15px"></p>
                                <%--Search and Export and Datagrid--%>
                                <div class="jumbotron">
                                    <div class="row" role="search">
                                        <div class="col-md-4">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtSearchPlus" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button runat="server" id="btnSearchPlus" type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-6"></div>
                                        <div class="col-md-2">
                                            <%--<button runat="server" id="btnExportPlus" type="button" class="btn btn-default" style="width: 100%"><i class="fa fa-file-text-o"></i>ບັນທຶກເປັນ Excel</button>--%>
                                        </div>
                                    </div>
                                    <p></p>
                                    <div class="row">
                                        <div class="col-md-12 table-responsive">
                                            <uc1:GridControl runat="server" ID="GridMemberPlus" />
                                        </div>
                                        <div class="row" runat="server" id="statusEditPlus"></div>
                                        <div class="row" runat="server" id="paPlus" visible="false">
                                            <uc1:Nick_GridControl runat="server" ID="GridExportPlus" />
                                        </div>
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

