<%@ Page Language="VB" AutoEventWireup="false" CodeFile="managePercentage.aspx.vb" Inherits="managePercentage" %>

<%@ Register Src="~/GridControl.ascx" TagPrefix="uc1" TagName="GridControl" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>M-TOPUP PLUS</title>
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
        function CloseMyModalSave() {
            $("#myModalSave").modal("hide");
        }
        function myModalEdit() {
            $("#myModalEdit").modal("show");
        }
        function CloseMyModalEdit() {
            $("#myModalEdit").modal("hide");
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
                                        ທ່ານຕ້ອງການບັນທຶກຂໍ້ມູນກຸ່ມ "<asp:Label ID="lbBranchName" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>" ແທ້ຫຼືບໍ່ ?
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
            <!-- myModalEdit -->
            <div class="modal fade" runat="server" id="myModalEdit" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">ແກ້ໄຂຂໍ້ມູນກຸ່ມ</h4>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-home"></span></span>
                                                <input type="text" runat="server" id="txtBranchNameEdit" class="form-control" placeholder="ຊື່ກຸ່ມ" autocomplete="off" />
                                                <input type="text" runat="server" id="txtBranchIDEdit" class="form-control" visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <p></p>
                                    <div id="alertInfo" runat="server" class="alert alert-warning alert-dismissable">
                                        <button runat="server" id="btnAlertInfo" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                        ແນະນຳ :
                                        <asp:Label ID="lbAlertInfo" runat="server" Text=""></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <button id="btCancelSave2" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                                    <button id="btConfirmSave2" runat="server" type="button" class="btn btn-primary">ບັນທຶກ</button>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
            <!-- myModalEdit -->
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
                        <li runat="server" id="menuMember"><a href="member.aspx"><i class="fa fa-pencil-square"></i>ຂໍ້ມູນສະມາຊິກ</a></li>
                        <li runat="server" id="menuRecharge"><a href="recharge.aspx"><i class="fa fa-share-square"></i>ເຕີມເງິນ</a></li>
                        <li runat="server" id="menuReceive"><a href="receive.aspx"><i class="fa fa-check-square"></i>ຮັບເງິນ</a></li>
                        <li runat="server" id="menuReprint"><a href="reprint.aspx"><i class="fa fa-print"></i>ພິມບິນຍ້ອນຫຼັງ</a></li>
                        <li class="dropdown active" runat="server" id="cbManage">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i>ຈັດການຂໍ້ມູນ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuPercentage"><a href="managePercentage.aspx"><i class="fa fa-ellipsis-h"></i>ກຳນົດເປີເຊັນ</a></li>
                                <li runat="server" id="menuUser"><a href="user.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນຜູ້ໃຊ້ລະບົບ</a></li>
                                <li runat="server" id="menuBranch" class="active"><a href="branch.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນກຸ່ມ</a></li>
                                <li runat="server" id="menuDiscount"><a href="discount.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການສ່ວນຫຼຸດ ( Discount )</a></li>
                                <li runat="server" id="menuDiscountSale"><a href="discountSale.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການໂບນັດ ( Bonus )</a></li>
                                <li runat="server" id="menuPromotion"><a href="promotion.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນໂປຣ ( Promotion )</a></li>
                            </ul>
                        </li>
                        <li class="dropdown" runat="server" id="cbReport">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i>ລາຍງານ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuProHis"><a href="proHistory.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການໃຫ້ໂປຣ</a></li>
                                <li runat="server" id="menuHistory"><a href="history.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການໂອນ</a></li>
                                <li runat="server" id="menuReceiveHistory"><a href="receiveHistory.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການຮັບເງິນ</a></li>
                                <li runat="server" id="menuHistoryL1"><a href="rechargeHistoryL1.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການເຕີມເງິນ ( L 1 )</a></li>
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
                <div class="row">
                    <div class="col-lg-12">
                        <h1>ເປີເຊັນ <small>ກຳນົດເປີເຊັນຂອງ MTOPUP PLUS</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-home"></i>ເປີເຊັນ</li>
                        </ol>
                    </div>
                </div>
                <!-- /.row -->
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="jumbotron">
                            <div class="row">
                                <div class="col-md-12">
                                    <p></p>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-book"></span></span>
                                                <input type="text" runat="server" id="txtPackageName" class="form-control" placeholder="ຊື່ Package" autocomplete="off" />
                                            </div>
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-pencil"></span></span>
                                                <input type="text" runat="server" maxlength="3" id="txtPercentage" class="form-control" placeholder="ເປີເຊັນ %" autocomplete="off" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-money"></span></span>
                                                <input type="text" runat="server" id="txtStartAmount" class="form-control" placeholder="ເງິນເລີ່ມຕົ້ນ" autocomplete="off" />
                                            </div>
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-money"></span></span>
                                                <input type="text" runat="server" id="txtEndAmount" class="form-control" placeholder="ເງິນສິ້ນສຸດ" autocomplete="off" />
                                            </div>
                                        </div>
                                        <input type="text" runat="server" id="txtPackageID" class="form-control" visible="false" autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div id="alertMainSuccess" runat="server" class="alert alert-success alert-dismissable">
                                            <button runat="server" id="btnAlertMainSuccess" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <b>ສຳເລັດ</b> :
                                               <asp:Label ID="lbAlertMainSuccess" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div id="alertMainError" runat="server" class="alert alert-danger alert-dismissable">
                                            <button runat="server" id="btnAlertMainError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <b>ບໍ່ສຳເລັດ</b> :
                                               <asp:Label ID="lbAlertMainError" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div id="alertMainInfo" runat="server" class="alert alert-warning alert-dismissable">
                                            <button runat="server" id="btnAlertMainInfo" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <b>ແນະນຳ</b> :
                                               <asp:Label ID="lbAlertMainInfo" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div id="alertUpdateSuccess" runat="server" class="alert alert-success alert-dismissable">
                                            <button runat="server" id="Button1" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            ການແກ້ໄຂຂໍ້ມູນສຳເລັດ
                                      <%--  <asp:Label ID="Label1" runat="server" Text=""></asp:Label>--%>
                                        </div>
                                        <div id="alertUpdateFailed" runat="server" class="alert alert-danger alert-dismissable">
                                            <button runat="server" id="Button2" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            ຜິດພາດ
                                        <asp:Label ID="lbUpdateFailed" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="btn-group btn-group-justified">
                                    <div class="btn-group">
                                        <button runat="server" id="btnSave" type="button" class="btn btn-success">
                                            <span class="fa fa-save"></span>ບັນທຶກຂໍ້ມູນກຸ່ມ
                                        </button>
                                    </div>
                                    <div class="btn-group">
                                        <button runat="server" id="btnDelete" type="button" class="btn btn-danger">
                                            <span class="fa fa-times"></span>ລຶບກຸ່ມທີ່ຖືກເລືອກ ( 
                                            <span class="fa fa-check"></span>)
                                        </button>
                                    </div>
                                    <div class="btn-group">
                                        <button runat="server" id="btnRefresh" type="button" class="btn btn-warning">
                                            <span class="fa fa-refresh"></span>Refresh
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <p></p>
                            <div class="row"></div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12">
                                    <uc1:GridControl runat="server" ID="gridPercentage" GirdPageSize="100000" />
                                </div>
                            </div>
                            </div>
                        </div>
                        <!-- jumbotron -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /#page-wrapper -->
        </div>
        <!-- /#wrapper -->
    </form>
</body>
</html>
