<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Nick_GridControl.ascx.vb" Inherits="Nick_GridControl" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <meta http-equiv="Content-Type" content="text/html"; charset="utf-8" />
    <title></title>
<%--    <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" type="text/css" href="BootStrap/css/datepicker.css" />
    
    <script src="holder/holder.js"></script>
    <script src="BootStrap/js/jquery.js"></script>
    <script src="BootStrap/js/bootstrap.min.js"></script>
    <script src="BootStrap/js/bootstrap-datepicker.js"></script>--%>
</head>
<body>

    
       <%-- <div class="panel panel-default">
            <div class="panel-body">--%>
                <div class="row">
                    <div class="col-xs-6 col-sm-6 col-md-12" style="width: 100%; height:350px; overflow-y: scroll">
                        <asp:GridView ID="GridView1" CssClass="table table-striped table-hover table-bordered" runat="server" GridLines="Both">
                            <Columns>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1%>.
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                            <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText="" />
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Bottom" />
                        </asp:GridView>
                    </div>
                </div>
            <%--</div>
        </div>--%>
   
</body>
</html>