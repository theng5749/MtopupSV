<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EDatePicker.ascx.vb" Inherits="EDatePicker" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <meta http-equiv="Content-Type" content="text/html"; charset="utf-8" />
    <title></title>
   <%-- <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/datepicker.css" />
    --%>
    <%--<script src="holder/holder.js"></script>--%>
    <%--<script src="BootStrap/js/jquery.js"></script>--%>
    <%--<script src="BootStrap/js/bootstrap.min.js"></script>--%>
    
</head>
<body>
    <div class="input-group date" id="date1">
        <asp:TextBox ID="ValueDate" runat="server" CssClass="form-control" /><span class="input-group-addon"><i class="fa fa-calendar" id="I1"></i></span>
    </div>
    <script>
        $(function () {
            $('#date1').datepicker({
                format: 'dd/mm/yyyy'
            });
        });
    </script>
</body>
</html>