<%@ Page Language="VB" AutoEventWireup="false" CodeFile="signinform.aspx.vb" Inherits="signinform" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>M-TOPUP PLUS</title>
    <link href="BootStrap/css/bootsnipp.min.css" rel="stylesheet" />
    <link href="BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
            background-image: url('./img/blur.jpg');
        }
        .form-signin input[type="text"] {
            margin-bottom: -1px;
            border-bottom-left-radius: 0;
            border-bottom-right-radius: 0;
        }
        .form-signin input[type="password"] {
            margin-bottom: 10px;
            border-top-left-radius: 0;
            border-top-right-radius: 0;
        }
        .form-signin .form-control {
            position: relative;
            font-size: 16px;
            height: auto;
            padding: 10px;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }
    </style>
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-34731274-1']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
    <script type="text/javascript">
        var fb_param = {};
        fb_param.pixel_id = '6007046190250';
        fb_param.value = '0.00';
        (function () {
            var fpw = document.createElement('script');
            fpw.async = true;
            fpw.src = '//connect.facebook.net/en_US/fp.js';
            var ref = document.getElementsByTagName('script')[0];
            ref.parentNode.insertBefore(fpw, ref);
        })();
    </script>
</head>
<body style="font-family: 'Phetsarath OT'">
    <div id="wrap">
        <form runat="server" id="form1">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="container">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row" style="margin-top: 60px;">
                            <div class="col-md-4 col-md-offset-4">
                                <div runat="server" id="panel1" class="form-signin">
                                    <fieldset>
                                        <h3 class="sign-up-title" style="color: dimgray; text-align: center">ລົງຊື່ເຂົ້າໃຊ້ລະບົບ M-TOPUP PLUS</h3>
                                        <hr class="colorgraph" />
                                        <input runat="server" id="txtUsername" class="form-control email-title" placeholder="ລະຫັດພະນັກງານ ( ID CARD )" type="text" />
                                        <input runat="server" id="txtPassword" class="form-control" placeholder="ລະຫັດຜ່ານ ( Password )" type="password" value="" />
                                        <div id="alertSaveEmpty" runat="server" class="alert alert-warning alert-dismissable">
                                            <button runat="server" id="btnAlertEmpty" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <b>ແນະນຳ : </b>
                                            <asp:Label ID="lbAlertEmptyMsg" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div id="alertSaveError" runat="server" class="alert alert-danger alert-dismissable">
                                            <button runat="server" id="btnAlertError" type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                            <b>ບໍ່ສຳເລັດ : </b>
                                            <asp:Label ID="lbAlertError" runat="server" Text=""></asp:Label>
                                        </div>
                                        <asp:LinkButton ID="btnForgotPassword" runat="server" CssClass="pull-right" ForeColor="Gray">ລືມລະຫັດຜ່ານ ??</asp:LinkButton>
                                        <p></p>
                                        <div class="row"></div>
                                        <p></p>
                                        <input runat="server" id="btnLogin" class="btn btn-lg btn-danger btn-block" type="submit" value="ເຂົ້າສູ່ລະບົບ" />
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-4 col-md-offset-4">
                                <div runat="server" id="panel2" class="form-signin">
                                    <fieldset>
                                        <h3 class="sign-up-title" style="color: dimgray; text-align: center">ຕິດຕໍ່ຜູ້ເບິ່ງແຍງລະບົບເພື່ອຂໍລະຫັດຜ່ານ</h3>
                                        <hr class="colorgraph" />
                                        <p>ພະແນກໄອທີ ( IT Department )</p>
                                        <p>ລາວໂທລະຄົມ</p>
                                        <p>ສຳນັກງານໃຫ່ຍຊັ້ນ 8</p>
                                        <p style="height: 10px"></p>
                                        <p>ຜູ້ເບິ່ງແຍງລະບົບ : ນ. ເຂັມຈັນ ແກ້ວສີລາວົງ ( ເຂັມ )</p>
                                        <p>ໂທລະສັບ: <strong>020 55598065</strong></p>
                                        <div class="row" style="height: 10px"></div>
                                        <p>ພັດທະນາໂດຍ : ສົມມີໄຊ ບຸດຈັນທະລາດ ( ນິກ )</p>
                                        <p>ໂທລະສັບ: <strong>020 59977252</strong></p>
                                        <p>ພັດທະນາໂດຍ : ບົວຫຼີ້ ທິບພະວົງ ( Thely )</p>
                                        <p>ໂທລະສັບ: <strong>020 56728456</strong></p>
                                        <p></p>
                                        <div class="btn-group">
                                            <button runat="server" id="btnBack" type="button" class="btn btn-default">
                                                <span class="glyphicon glyphicon-chevron-left"></span>ກັບຄືນສູ່ໜ້າ Login
                                            </button>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </form>
    </div>

    <footer class="bs-footer" role="contentinfo">
        <div class="container" style="font-family: 'Phetsarath OT'; text-align: center">

            <p></p>
            <p>
                ພະແນກໄອທີ ສູນພັດທະນາໂປຣແກຣມ (MBA & CC)<br />
                Copyright © 2019 Lao Telecom, All Rights Reserved
            </p>
        </div>
    </footer>
    <script src="BootStrap/js/jquery-1.11.0.js"></script>
    <script src="BootStrap/js/bootstrap.min.js"></script>
</body>
</html>

