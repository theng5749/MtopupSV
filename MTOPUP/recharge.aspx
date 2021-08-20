<%@ Page Language="VB" AutoEventWireup="false" CodeFile="recharge.aspx.vb" Inherits="recharge" %>

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
    <style>
        .radioButtonList input[type="radio"] {
            width: auto;      
            float: left;  
        }
        .radioButtonList label {      
            width: auto;      
            display: inline;      
            float: left;    
            margin-right:20px;
        }
        .tbHistoryRefill {
            border-collapse: collapse;
            width: 100%;

        }
        .tbHistoryRefill td th {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;

        }
        .tbHistoryRefill tr {
            height:50px;
        }
        .tbHistoryRefill tr:nth-child(even) {
            background-color: #dddddd;

        }
    </style>
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
    <script src="js/tablesorter/jquery.tablesorter.js"></script>
    <script src="js/tablesorter/tables.js"></script>
    <script type="text/javascript">
        function closeModalMember() {
            $("#modalSelectMember").modal("hide");
        }
        function closeModalSave() {
            $("#myModalSave").modal("hide");
        }
        function myModalSave() {
            $("#myModalSave").modal("show");
        }
        function myModalITConfirm() {
            $("#myModalITConfirm").modal("show");
        }
        function CloseModalITConfirm() {
            $("#myModalITConfirm").modal("hide");
        }
        function modalSelectMember() {
            $("#modalSelectMember").modal("show");
        }
        function modalFullImage() {
            $("#modalFullImage").modal("show");
        }
        function modalDocument() {
            $("#modalDocument").modal("show");
        }
        $(document).ready(function () {
            $('#docComment').hide();
            $('input:radio[name="rbDocType"]').change(function () {
                if ($(this).val() == '1') {
                    //alert("ເອກະສານຄົບຖ້ວນ");
                    $('#docNoGroup').show();
                    $('#docComment').hide();
                } else {
                    //alert("ເອກະສານດ່ວນ");
                    $('#docNoGroup').hide();
                    $('#docComment').show();
                }
            });
        })
        docComment
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

        <%--var rbl = document.getElementById("<%= rbDocType.ClientID%>");
        var value = rbl.value;
        if (value === '2') {
            alert("no");
        } else {
            alert("yes");
        }--%>
    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family: 'Phetsarath OT'">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="wrapper">
            <!-- myModalSave -->
            <div class="modal fade" runat="server" id="myModalSave" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel1">ຢືນຢັນການຕື່ມເງິນ</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການເຕີມເງິນໃຫ້ເບີ
                                <asp:Label ID="lbTel" runat="server" ForeColor="red"></asp:Label>
                                        ເປັນຈຳນວນເງິນ
                                <asp:Label ID="lbNewMoney" runat="server" ForeColor="red"></asp:Label>
                                        ກີບ ແທ້ຫຼືບໍ່ ?
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
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div id="overlay">
                                        <div id="modalprogress">
                                            <div id="theprogress">
                                                <p style="text-align: center">
                                                    <asp:Image ID="Image2" runat="server" Height="50px" ImageUrl="~/Images/support-loading.gif" Width="50px" />
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
            <!-- modalSelectMember -->
            <div class="modal fade" runat="server" id="modalSelectMember" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">ເລືອກເບີທີ່ຕ້ອງການເຕີມເງິນ</h4>
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
                                                <uc1:Lo_GridControl runat="server" ID="gridMemberModal" GirdPageSize="1000000" />
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
            <!-- modalSelectRoot -->
            <!-- modalITConfirm -->
            <div class="modal fade" runat="server" id="myModalITConfirm" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">ເລືອກຜູ້ອະນຸມັດ</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="jumbotron">
                                        <div class="row" role="search">
                                            <div class="col-md-5">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <%-- <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ .."></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button runat="server" id="Button15" type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                                                            </span>--%>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <p></p>
                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                            <ContentTemplate>
                                                <uc1:GridControl runat="server" ID="GridControlITApprove" />
                                                <%--<uc1:Lo_GridControl runat="server" ID="Lo_GridControl1" GirdPageSize="1000000" />--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- modal full image -->
            
            <div class="modal fade" runat="server" id="modalFullImage" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <asp:Image ID="ImageFull" runat="server" Height="100%" Width="100%" />
                        
                    </div>
                </div>
            </div>
            <!-- modal alert document -->
            
            <div class="modal fade" runat="server" id="modalDocument" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" >ແຈ້ງເຕືອນ</h4>
                        </div>
                        <div class="modal-body">
                            ເລກໃບສະເໜີນີ້ ມີຢູ່ແລ້ວ!!
                        </div>
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <button id="Button5" runat="server" type="button" class="btn btn-default" data-dismiss="modal">ຍົກເລີກ</button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

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
                        <li runat="server" id="menuRecharge" class="active"><a href="recharge.aspx"><i class="fa fa-share-square"></i>ເຕີມເງິນ</a></li>
                        <li runat="server" id="menuReceive"><a href="receive.aspx"><i class="fa fa-check-square"></i>ຮັບເງິນ</a></li>
                        <li runat="server" id="menuReprint"><a href="reprint.aspx"><i class="fa fa-print"></i>ພິມບິນຍ້ອນຫຼັງ</a></li>
                        <li class="dropdown" runat="server" id="cbManage">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-caret-square-o-down"></i>ຈັດການຂໍ້ມູນ <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li runat="server" id="menuApprove"><a href="approveUser.aspx"><i class="fa fa-ellipsis-h"></i>ຈັດການຂໍ້ມູນຜູ້ອະນຸມັດ</a></li>
                                <li runat="server" id="menuPercentage"><a href="managePercentage.aspx"><i class="fa fa-ellipsis-h"></i>ກຳນົດເປີເຊັນ</a></li>
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
                                <li runat="server" id="menuRechargeHistory"><a href="RechargeHistory.aspx"><i class="fa fa-calendar"></i>ປະຫວັດການເຕີມເງິນ</a></li>
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
                    <div class="col-lg-12">
                        <h1>ເຕີມເງິນ <small>ຂໍ້ມູນການເຕີມເງິນ MTOPUP</small></h1>
                        <ol class="breadcrumb">
                            <li class="active"><i class="fa fa-share-square"></i>ເຕີມເງີນ Partner & Bank</li>
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

                        <asp:UpdatePanel ID="UpdatePanel51" runat="server" Visible="false">
                            <ContentTemplate>
                                <%--  <div id="alertSucessVAS" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertSucessVAS" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <b>ລຳເລັດ : </b>ການເຕີມເງິນໃຫ້ກັບໝາຍເລກ
                            <asp:Label ID="lbTelephoneSuccess" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ເປັນຈຳນວນເງີນ
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </div>--%>
                                <div id="alertSaveSucess" runat="server" class="alert alert-success alert-dismissable">
                                    <button runat="server" id="btnAlertSuccess" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <b>ສຳເລັດ : </b>ເຕີມເງິນໃຫ້ກັບໝາຍເລກ
                            <asp:Label ID="lbTelephone2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ເປັນຈຳນວນເງິນ
                                    <asp:Label ID="lbNewMoney2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ກີບ 
                                    ສຳເລັດ. (
                                <%--    <asp:Label ID="lbOldMoney" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        +
                                    <asp:Label ID="lbNewMoney3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        =
                                    <asp:Label ID="lbUpdateMoney" runat="server" Font-Bold="true"></asp:Label>
                                        ກີບ. )--%>
                                </div>
                                <div id="alertSaveError" runat="server" class="alert alert-danger alert-dismissable">
                                    <button runat="server" id="btnAlertError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <b>ຜິດພາດ : </b>ການເຕີມເງິນໃຫ້ກັບໝາຍເລກ
                            <asp:Label ID="lbTel2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                    ບໍ່ສຳເລັດ !!
                                    <asp:Label ID="lbError" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </div>
                                <div id="alertSelectMember" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertSelectMember" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາເລືອກເບີທີ່ຕ້ອງການເຕີມເງິນກ່ອນ !!
                                </div>
                                <div id="alertNewMoney" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertNewMoney" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາປ້ອນນຳນວນເງິນທີ່ຕ້ອງການເຕີມກ່ອນ !!
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="container-fluid">
                                        <div id="alertVASSuccess" runat="server" class="alert alert-success alert-dismissable">
                                            <button runat="server" id="Button3" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <p>ການຕື່ມເງີນຂອງ VAS ແມ່ນສຳເລັດ</p>
                                            <asp:Label ID="lbTelephoneVAS" runat="server" Font-Bold="true" Text=""></asp:Label>
                                            <asp:Label ID="lbOldAmountVAS" runat="server" Font-Bold="true" Text=""></asp:Label>
                                            <asp:Label ID="lbAmountVAS" runat="server" Font-Bold="true" Text=""></asp:Label>
                                            <p>ລໍຖ້າການອະນຸມັດຂອງພະແນກໄອທີ ແລະ ພະແນກການເງິນ</p>
                                        </div>
                                        <div id="alertVASError" runat="server" class="alert alert-danger alert-dismissable">
                                            <button runat="server" id="Button4" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <p>ການຕື່ມເງີນຂອງ VAS ແມ່ນບໍ່ສຳເລັດ</p>
                                            <p>ບໍ່ສາມາດສົ່ງຂໍ້ຄວາມໃຫ້ ພະແນກໄດ້</p>
                                            <asp:Label ID="lbVASERR" runat="server" Font-Bold="true" Text=""></asp:Label>
                                            <p>ກະລຸນາຕິດຕໍ່ຫາ Adminstrator ເພື່ອກວດສອບ</p>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-btn">
                                                <button runat="server" id="btnTelephone" type="button" class="btn btn-info">ເລືອກເບີ ( ເຕີມເງິນ )</button>
                                            </span>
                                            <input runat="server" id="txtTelephone" type="text" class="form-control" placeholder="ເບີທີ່ຕ້ອງການເຕີມເງິນ .." readonly />
                                            <input runat="server" id="txtMemberID" type="text" class="form-control" visible="false" />
                                            <input runat="server" id="txtLogID" type="text" class="form-control" visible="false" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-user"></span>(ຊື່ສະມາຊິກ)</span>
                                            <input type="text" runat="server" id="txtMemberNameMT" class="form-control" placeholder="ຊື່ສະມາຊິກ .." autocomplete="off" readonly />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-money"></span>(ເງິນທີ່ມີ)</span>
                                            <input type="text" runat="server" id="txtOldCredit" class="form-control" placeholder="ຈຳນວນເງິນທີ່ມີປະຈະບັນ .." autocomplete="off" readonly />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p></p>
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-money"></span>(ເງິນທີ່ຕ້ອງການເຕີມ)</span>
                                            <input type="text" runat="server" id="txtNewCredit" class="form-control" placeholder="ຈຳນວນເງິນທີ່ຕ້ອງການເຕີມ .." autocomplete="off" onkeyup="javascript:this.value=Comma(this.value);" onkeypress="return allowOnlyNumber(event);" maxlength="15" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-btn">
                                                <button runat="server" id="btnITApprove" type="button" class="btn btn-danger">ຜູ້ອະນຸມັດ (IT)</button>
                                            </span>
                                            <input runat="server" id="txtITMsisdn" type="text" class="form-control" visible="false" />
                                            <input runat="server" id="txtITApprove" type="text" class="form-control" placeholder="ເລືອກຜູ້ອະນຸມັດໄອທີ" readonly />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="input-group">
                                            <span class="input-group-btn">
                                                <input runat="server" id="txtFinanceMsisdn" type="text" class="form-control" visible="false" />
                                                <button runat="server" id="btnFinanceApprove" type="button" class="btn btn-primary">ຜູ້ອະນຸມັດ (Finance)</button>
                                            </span>
                                            <input runat="server" id="txtFinanceApprove" type="text" class="form-control" placeholder="ເລືອກຜູ້ອະນຸມັດການເງິນ" readonly />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <p></p>
                        

                        <div class="row container-fluid">
                            <hr />
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile"/>

                            <div id="alertImage" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="btnAlertImage" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ກະລຸນາເລືອກຮູບທີ່ເປັນນາມສະກຸນ .jpg ຫລື .png !!
                                </div>
                            <hr />
                            
                            <div class="row">
                                <div class="col-lg-4 col-md-12 col-sm-12">
                                    <button runat="server" onserverclick="ImageFull_ServerClick" type="button" class="btn btn-danger" style="margin-top:-10px;margin-bottom:10px;">ເບິ່ງຮູບເຕັມ</button><br />
                                    <asp:ImageButton ID="Image1" OnClick="ImageFull_ServerClick" runat="server" Height="200" Width="200" />
                                </div>

                                <%-- radio button --%>
                                <div class="col-lg-8 col-md-12 col-sm-12">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12">
                                            <asp:RadioButtonList ID="rbDocType" runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList">
                                                <asp:ListItem Text="ເອກະສານຄົບຖ້ວນ" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="ເອກະສານດ່ວນ" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="col-lg-6 col-md-10">
                                            <div class="input-group" id="docNoGroup" style="margin-top: 10px;">
                                                <span class="input-group-addon" id="textdoc">ເລກໃບສະເໜີ</span>
                                                <input type="text" runat="server" id="textdocNo" class="form-control" placeholder="XXXX/XXXX" autocomplete="off" maxlength="100"/>
                                            </div>
                                        </div>
                                       <%-- <div class="col-lg-12 col-md-10">
                                            <div class="input-group" id="docComment" style="margin-top: 10px;">
                                                <textarea class="form-control  input-lg" id="textdocComment" placeholder="" cols="20" rows="3" runat="server"> </textarea>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                                
                            </div>

                            
                            <hr />
                            <%--<div id="alertDocNo" runat="server" class="alert alert-warning alert-dismissable">
                                    <button runat="server" id="Button5" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    ເລກທີໃບສະເໜີນີ້ ມີຢູ່ແລ້ວ!!
                                </div>--%>
                            
                        </div>
                        

                        <div class="row">
                            <div class="col-md-4">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="btn-group btn-group-justified">
                                            <div class="btn-group">
                                                <button runat="server" id="btnSave" type="button" class="btn btn-success">
                                                    <span class="fa fa-save"></span>ຕົກລົງ
                                                </button>
                                            </div>

                                            <div class="btn-group">
                                                <button runat="server" id="btnCancel" type="button" class="btn btn-warning">
                                                    <span class="fa fa-times"></span>ຍົກເລີກ
                                                </button>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnCancel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <p></p>
                        <div class="jumbotron">
                            <h3><i class="fa fa-calendar"></i>ປະຫວັດການເຕີມເງິນ <small>(ຕາມຊ່ວງວັນທີ)</small></h3>
                            <div class="row">
                                <div class="col-md-3">
                                    <uc1:EDatePicker runat="server" ID="EDatePicker1" />
                                </div>
                                <div class="col-md-3">
                                    <uc1:EDatePicker2 runat="server" ID="EDatePicker2" />
                                </div>
                                <div class="col-md-2">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <button runat="server" id="btnSearch" type="button" class="btn btn-default"><i class="fa fa-search"></i></button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <uc1:GridControl runat="server" ID="gridHistory" GirdPageSize="10000"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <p></p>
   <%--                         <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <table class="tbHistoryRefill" style="width:100%;">
                                                    <tr>
                                                        <th style="width:10%;">No.</th>
                                                        <th style="width:25%;">ໝາຍເລກໂທລະສັບ</th>
                                                        <th style="width:25%;">ວັນທີທີ່ເຕີມ</th>
                                                        <th style="width:25%;">ຈຳນວນເງິນທີ່ເຕີມ</th>
                                                        <th style="width:15%;">ຜູ້ທີ່ທຳການເຕີມໃຫ້</th>
                                                    </tr>--%>
                                                
                                                    <%--<% If (theHeadering.Count > 0) Then
                                                            For i As Integer = 0 To theHeadering.Count
                                                                theHeadering(i).ToString()

                                                            Next
                                                        End If
                                                         %>--%>
                                                    <%= theHeadering %>
                         <%--                       
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="TextBox1" Visible="false" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <p></p>
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

