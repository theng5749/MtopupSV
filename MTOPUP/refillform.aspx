<%@ Page Language="VB" AutoEventWireup="false" CodeFile="refillform.aspx.vb" Inherits="refillform" %>

<%@ Register Src="~/Lo_GridControl.ascx" TagPrefix="uc1" TagName="Lo_GridControl" %>
<%@ Register Src="~/GridControl.ascx" TagPrefix="uc1" TagName="GridControl" %>
<%@ Register Src="~/EDatePicker.ascx" TagPrefix="uc1" TagName="EDatePicker" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>M-TOPUP PLUS</title>

   <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/datepicker.css" />


    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <!-- Add custom CSS here -->
    <link rel="stylesheet" href="font-awesome/css/font-awesome.min.css">
    <link href="css/justified-nav.css" rel="stylesheet" />


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
         }
         function myModalSave() {
             $("#myModalSave").modal("show");
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
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                        <li class="active"><a href="refillform.aspx">ຕື່ມເງິນ</a></li>
                        <li><a href="reportform.aspx">ລາຍງານ</a></li>
                        <li><a href="settingform.aspx">ຕັ້ງຄ່າ</a></li>
                        <li id="btnlogout" runat="server"><a href="startupform.aspx">ອອກຈາກລະບົບ</a></li>
                    </ul>
                </nav>
            </div>
            <div class="row">
                 <!-- modalSelectMember -->
            <div class="modal fade" runat="server" id="modalSelectMember" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H1">ເລືອກເບີທີ່ຕ້ອງການເຕີມເງິນ</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="jumbotron">
                                        <div class="row">
                                            <div class="col-md-5">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                           <asp:TextBox ID="txtSearchMember" runat="server" AutoPostBack="true" CssClass="form-control" placeholder="ຄົ້ນຫາ .."></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button runat="server" id="btnSearchMember" class="btn-sm" type="button">
                                                                    <i class="fa fa-search"></i>
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <p></p>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <uc1:Lo_GridControl runat="server" ID="GridMemberModal" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                   <!-- myModalSave -->
            <div class="modal fade" runat="server" id="myModalSave" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">ຢືນຢັນການບັນທຶກ</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <p>
                                        ທ່ານຕ້ອງການເຕີມເງິນໃຫ້ເບີ
                                <asp:Label ID="lbTel" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                        ເປັນຈຳນວນເງິນ
                                        <asp:Label ID="lbNewMoney" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label> ກີບ 
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
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h1>ເຕີມເງິນ <small>ຂໍ້ມູນການເຕີມເງິນ</small></h1>
                    <ol class="breadcrumb">
                        <li class="active"><i class="fa fa-share-square"></i>ເຕີມເງີນ</li>
                    </ol>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="alertrefillsuccess" runat="server" class="alert alert-success alert-dismissable">
                                <button runat="server" id="btnAlertSuccess" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                <b>ສຳເລັດ : </b>ເຕີມເງິນໃຫ້ກັບໝາຍເລກ
                            <asp:Label ID="lbmsisdn" runat="server" Font-Bold="true" Text=""></asp:Label>
                                ເປັນຈຳນວນເງິນ
                            <asp:Label ID="lbNewMoney2" runat="server" Font-Bold="true" Text=""></asp:Label>
                                ກີບສຳເລັດ. (
                                <asp:Label ID="lbOldMoney" runat="server" Font-Bold="true" Text=""></asp:Label>
                                +
                                <asp:Label ID="lbNewMoney3" runat="server" Font-Bold="true" Text=""></asp:Label>
                                =
                                <asp:Label ID="lbUpdateMoney" runat="server" Font-Bold="true"></asp:Label>
                                ກີບ. )
                            </div>
                            <div id="alertrefillfail" runat="server" class="alert alert-danger alert-dismissable">
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
                        <%--<div class="col-md-2"></div>--%>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <button runat="server" id="btnTelephone" type="button" data-toggle="modal" data-target="#modalSelectMember" class="btn btn-info">ເລືອກເບີ ( ເຕີມເງິນ )</button>
                                        </span>
                                        <input runat="server" id="txtTelephone" type="text" class="form-control" placeholder="ເບີທີ່ຕ້ອງການເຕີມເງິນ .." readonly="true" />
                                        <input runat="server" id="txtMemberID" type="text" class="form-control" visible="false" />
                                        <input runat="server" id="txtLogID" type="text" class="form-control" visible="false" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-user"></span>(ຊື່ສະມາຊິກ)</span>
                                        <input type="text" runat="server" id="txtMemberNameMT" class="form-control" placeholder="ຊື່ສະມາຊິກ .." autocomplete="off" readonly="true" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-money"></span>(ເງິນທີ່ມີ)</span>
                                        <input type="text" runat="server" id="txtOldCredit" class="form-control" placeholder="ຈຳນວນເງິນທີ່ມີປະຈະບັນ .." autocomplete="off" readonly="true" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <p></p>
                    <div class="row" id="panelSave" runat="server">
                        <div class="col-md-4">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <div class="btn-group btn-group-justified">
                                        <div class="btn-group">
                                            <button runat="server" data-toggle="modal" data-target="#myModalSave" id="btnSave" type="button" class="btn btn-success">
                                                <span class="fa fa-save"></span>ຕົກລົງ
                                            </button>
                                        </div>

                                        <div class="btn-group">
                                            <button runat="server" id="btCancel" type="button" class="btn btn-warning">
                                                <span class="fa fa-times"></span>ຍົກເລີກ
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
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="jumbotron">
                        <h3><i class="fa fa-calendar"></i>ປະຫວັດການເຕີມເງິນ <small>(ຕາມຊ່ວງວັນທີ)</small></h3>
                        <div class="row">
                            <div class="col-md-3">
                                <uc1:EDatePicker runat="server" ID="EDatePicker1" />
                            </div>
                            <div class="col-md-3">
                                <uc1:EDatePicker runat="server" ID="EDatePicker2" />
                            </div>
                            <div class="col-md-2">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <button runat="server" style="font-size:5px" id="btnSearch" type="button" class="btn btn-default"><i style="font-size:10px" class="fa fa-search"></i></button>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <p></p>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <uc1:GridControl runat="server" ID="GridControl" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
