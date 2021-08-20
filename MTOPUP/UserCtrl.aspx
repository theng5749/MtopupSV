<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserCtrl.aspx.vb" Inherits="UserCtrl" %>

<%@ Register Src="~/GridControl.ascx" TagPrefix="uc1" TagName="GridControl" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
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
</head>
<script type="text/javascript">
    function closeModalAddUser() {
        $("#modalAddUser").modal("hide");
    }
</script>

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
                    <a class="navbar-brand">M-TOPUP</a>
                </div>
                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse navbar-ex1-collapse">
                    <ul class="nav navbar-nav side-nav">
                        <%--<li runat="server" id="menuMainpage"><a  ><i class="fa fa-desktop"></i> ໜ້າຫຼັກ</a></li>--%>
                        <li runat="server" id="menuMember"><a href="member.aspx"><i class="fa fa-pencil-square"></i>ຂໍ້ມູນສະມາຊິກ</a></li>
                        <li runat="server" id="menuUserCtrl" class="active"><a href="userctrl.aspx"><i class="fa fa-user"></i>ພະນັກງານຂາຍ</a></li>
                        <li runat="server" id="menuRecharge"><a href="recharge.aspx"><i class="fa fa-share-square"></i>ເຕີມເງິນ</a></li>
                        <li runat="server" id="menuReceive"><a href="receive.aspx"><i class="fa fa-check-square"></i>ຮັບເງິນ</a></li>
                        <li runat="server" id="menuReprint"><a href="reprint.aspx"><i class="fa fa-print"></i>ພິມບິນຍ້ອນຫຼັງ</a></li>
                        <li class="dropdown" runat="server" id="cbManage">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i>ຈັດການຂໍ້ມູນ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuApprove"><a href="approveUser.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນຜູ້ອະນຸມັດ</a></li>
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
                                <li runat="server" id="menuRechargeHistory"><a href="rechargeHistory.aspx"><i class="fa fa-calendar"></i> ປະຫວັດການເຕີມເງິນ</a></li>
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
                <div class="row">
                    <h1>ຂໍ້ມູນພະນັກງານ <small>FN Approve, Super, Sale</small></h1>
                    <ol class="breadcrumb">
                        <li class="active"><i class="fa fa-pencil-square"></i>ພະນັກງານຂາຍ ແລະ ຜູ້ອານຸມັດ</li>
                    </ol>
                </div>
                <div class="row">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div id="alertSaveSucess" runat="server" class="alert alert-success alert-dismissable">
                                <button runat="server" id="btnAlertSuccess" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                ການບັນທຶກຂໍ້ມູນສະມາຊິກລະຫັດ
                            <asp:Label ID="lbcode" runat="server" Font-Bold="true" Text=""></asp:Label>
                                ສຳເລັດ
                            </div>
                            <div id="alertSaveError" runat="server" class="alert alert-danger alert-dismissable">
                                <button runat="server" id="btnAlertError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                ການບັນທຶກຂໍ້ມູນສະມາຊິກເບີ
                            <asp:Label ID="lbcode2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                ບໍ່ສຳເລັດ, ລະຫັດນີ້ມີຢູ່ແລ້ວໃນລະບົບ !!
                            </div>
                            <div id="alertInput" runat="server" class="alert alert-danger alert-dismissable">
                                <button runat="server" id="Button1" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                ກະລຸນາປ້ອນຂໍ້ມູນ
                            </div>
                            <div id="alertCode" runat="server" class="alert alert-danger alert-dismissable">
                                <button runat="server" id="Button2" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                ລະຫັດ
                                <asp:Label ID="Lbcode3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                ນີ້ມີຢູ່ແລ້ວ
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div id="txtuserid" runat="server" class="input-group">
                                <span class="input-group-addon"><span class="fa fa-check-square"></span></span>
                                <input type="text" runat="server" required id="txtusercode" class="form-control" placeholder="ລະຫັດພະນັກງານ" maxlength="10" autocomplete="off" />
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon"><span class="fa fa-user"></span></span>
                                <input type="text" runat="server" required id="txtusernameeng" class="form-control" placeholder="ຊື່ ແລະ ນາມສະກຸນ (ENG)" maxlength="30" autocomplete="off" />
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon"><span class="fa fa-phone"></span></span>
                                <input type="text" runat="server" required id="txtuserphone" class="form-control" placeholder="ເບີໂທ" maxlength="15" autocomplete="off" />
                            </div>
                            <div id="statusactive" runat="server"  class="input-group">
                                <span class="input-group-addon"><span class="fa fa-square"></span></span>
                                <asp:DropDownList ID="txtstatus" runat="server" CssClass="form-control" AutoPostBack="false">
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Deactivated">Deactivated</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="input-group">
                                <span class="input-group-addon"><span class="fa fa-sort-amount-asc"></span></span>
                                <asp:DropDownList ID="txtuserdivision" runat="server" CssClass="form-control" AutoPostBack="false">
                                    <asp:ListItem Value="VTE">ນະຄອນຫຼວງ</asp:ListItem>
                                    <asp:ListItem Value="PSL">ຜົ້ງສາລີ</asp:ListItem>
                                    <asp:ListItem Value="HP">ຫົວພັນ</asp:ListItem>
                                    <asp:ListItem Value="XHKH">ຊຽງຂວາງ</asp:ListItem>
                                    <asp:ListItem Value="LNT">ຫຼວງນ້ຳທາ</asp:ListItem>
                                    <asp:ListItem Value="BOK">ບໍ່ແກ້ວ</asp:ListItem>
                                    <asp:ListItem Value="ODX">ອຸດົມໄຊ</asp:ListItem>
                                    <asp:ListItem Value="LPB">ຫຼວງພະບາງ</asp:ListItem>
                                    <asp:ListItem Value="XYBL">ໄຊຍະບູລີ</asp:ListItem>
                                    <asp:ListItem Value="XSB">ໄຊສົມບູນ</asp:ListItem>
                                    <asp:ListItem Value="VTP">ວຽງຈັນ</asp:ListItem>
                                    <asp:ListItem Value="BLX">ບໍລິຄຳໄຊ</asp:ListItem>
                                    <asp:ListItem Value="KHM">ຄຳມ່ວນ</asp:ListItem>
                                    <asp:ListItem Value="SVNKH">ສະຫວັນນະເຂດ</asp:ListItem>
                                    <asp:ListItem Value="CHPS">ຈຳປາສັກ</asp:ListItem>
                                    <asp:ListItem Value="UTP">ອັດຕະປື</asp:ListItem>
                                    <asp:ListItem Value="SLV">ສາລະວັນ</asp:ListItem>
                                    <asp:ListItem Value="SEK">ເຊກອງ</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon"><span class="fa fa-bars"></span></span>
                                <asp:DropDownList ID="txtuserposition" runat="server" CssClass="form-control" AutoPostBack="false">
                                    <asp:ListItem Value="Section">ພາກສ່ວນ</asp:ListItem>
                                    <asp:ListItem Value="ViceSection">ຮອງພາກສ່ວນ</asp:ListItem>
                                    <asp:ListItem Value="Unit">ໝ່ວຍງານ</asp:ListItem>
                                    <asp:ListItem Value="Member">ສະມາຊິກ</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="input-group">
                                <span class="input-group-addon"><span class="fa fa-check-circle"></span></span>
                                <asp:DropDownList ID="txtuserapprove" runat="server" CssClass="form-control" AutoPostBack="false">
                                    <asp:ListItem Value="FN-Approve">FN-Approve</asp:ListItem>
                                    <asp:ListItem Value="CS-Supper">CS-Supper</asp:ListItem>
                                    <asp:ListItem Value="CS-Sale">CS-Sale</asp:ListItem>
                                    <asp:ListItem Value="Super">Super</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <p></p>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-8">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <br />
                                        <div class="btn-group btn-group-justified">
                                            <div class="btn-group">
                                                <button id="btnSave" type="button" runat="server" style="font-weight: 600; height: 50px" class="btn btn-success btn-block">
                                                     <span class="fa fa-save"></span> ບັນທຶກ</button>
                                            </div>
                                            <div class="btn-group">
                                                <button id="btnEdit" type="button" runat="server" style="font-weight: 600; height: 50px" class="btn btn-primary btn-block">
                                                     <span class="fa fa-save"></span> ບັນທຶກແກ້ໄຂ</button>
                                            </div>
                                            <div class="btn-group">
                                                <button runat="server" id="btnRefresh" type="button" style="font-weight: 600; height: 50px" class="btn btn-default btn-block">
                                                    <span class="fa fa-refresh"></span> Refresh
                                                </button>
                                            </div>
                                        </div>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-2">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" role="search">
                    <div class="col-md-4">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="input-group">
                                    <%--<span class="input-group-btn">
                                        <button runat="server" id="btnAdd" type="button" class="btn btn-success" data-toggle="modal" data-target="#modalAddUser"><i class="fa fa-plus"></i> ເພີ່ມຜູ້ໃຊ້ໃໝ່</button>
                                    </span>--%>
                                    <asp:TextBox ID="txtSearchText" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ລະຫັດພະນັກງານ"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button runat="server" id="btnSearch" type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                                    </span>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <div class="col-md-6"></div>
                    <div class="col-md-2">
                        <button runat="server" id="btnExport" type="button" class="btn btn-default" style="width: 100%"><i class="fa fa-file-text-o"></i> ບັນທຶກເປັນ Excel</button>
                    </div>
                </div>
                <div class="row">
                    <hr />
                    <uc1:GridControl runat="server" ID="GridControl" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
