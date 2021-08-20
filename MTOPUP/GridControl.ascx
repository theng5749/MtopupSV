<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GridControl.ascx.vb" Inherits="GridControl" %>
<!DOCTYPE html>

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
                    <div class="col-xs-6 col-sm-6 col-md-12" style="width: 100%">
                        <asp:GridView ID="GridView1" CssClass="table table-hover table-striped tablesorter" runat="server" GridLines="None" AllowPaging="True" AllowSorting="True" style="text-align: left">
                            <Columns>
                                <asp:ButtonField CommandName="Select" Text="ເລືອກ">
                                <ControlStyle CssClass="btn btn-default" />
                                </asp:ButtonField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" onclick="RadioCheck(this);" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="RadioButton1" runat="server" onclick="RadioCheck(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1%>.
                                        </ItemTemplate>
                                    </asp:TemplateField>

                            </Columns>
                            <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText="" />
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Bottom" />
                        </asp:GridView>
                        <script type="text/javascript">
                            function RadioCheck(rb) {
                                var gv = document.getElementById("<%=GridView1.ClientID%>");
                                var rbs = gv.getElementsByTagName("input");

                                var row = rb.parentNode.parentNode;
                                for (var i = 0; i < rbs.length; i++) {
                                    if (rbs[i].type == "radio") {
                                        if (rbs[i].checked && rbs[i] != rb) {
                                            rbs[i].checked = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        </script>
                    </div>
                </div>
            <%--</div>
        </div>--%>
   
</body>
</html>