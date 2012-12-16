<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Install.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfCheck>" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">
<script type="text/javascript">
        require(["dijit/form/Button", "dojo/domReady!"], function (Button) {
            var button = new Button({
                label: "下一步",
                onClick: function () {
                    top.location = "/Install/DbCnf";
                }
            }, "nextBtn");
            button.startup();
        });
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <h1><%="系统版本："+Model.OSVersion %></h1>
    <h2><%=".Net运行时版本："+Model.RunTimeVersion %></h2>
    </div>
    <button id="nextBtn"></button>
    </asp:Content>