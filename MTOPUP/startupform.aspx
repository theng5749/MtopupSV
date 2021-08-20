<%@ Page Language="VB" AutoEventWireup="false" CodeFile="startupform.aspx.vb" Inherits="startupform" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>STARTUP</title>
    <link href="BootStrap/css/bootsnipp.min.css" rel="stylesheet" />
    <link href="BootStrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="col-md-12" style="padding-top:150px">
                <div class="panel panel-body">
                    <div class="col-md-4">
                        <a style="height: 150px" class="btn btn-success btn-bg btn-block" href="http://localhost:1561/default.aspx">
                            <h4><span class="glyphicon glyphicon-check"></span></h4>
                            <h5 style="font-weight: 600">M-TOPUP</h5>
                            <h5 style="font-weight: 600">Partner</h5>
                        </a>
                    </div>
                    <div class="col-md-4">
                        <a style="height: 150px" class="btn btn-danger btn-bg btn-block" href="http://localhost:1561/signinform.aspx">
                            <h4><span class="glyphicon glyphicon-credit-card"></span></h4>
                            <h5 style="font-weight: 600">M-TOPUP PLUS</h5>
                            <h5 style="font-weight: 600">M Services</h5>
                        </a>
                    </div>
                    <div class="col-md-4">
                        <a style="height: 150px" class="btn btn-default btn-bg btn-block" href="http://10.30.6.37:3434/">
                            <h4><span class="glyphicon glyphicon-ok-sign"></span></h4>
                            <h5 style="font-weight: 600">ລົງທະບຽນ</h5>
                            <h5 style="font-weight: 600">Register</h5>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
