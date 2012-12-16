<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Install.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfDbCnf>" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        require(["dijit/form/Button", "dijit/form/TextBox", "dojo/domReady!"], function (Button, TextBox) {
            var button = new Button({
                label: "下一步",
                onClick: function () {
                    dbform.submit();
                }
            }, "submitBtn");
            button.startup();

            var addr = new TextBox({
                placeHolder: "IP地址或机器名"
            }, "DbAddr");
            addr.startup();

            var dbname = new TextBox({
                placeHolder: "数据库名称"
            }, "DbName");
            dbname.startup();

            var uname = new TextBox({
                placeHolder: "用户名"
            }, "DbUser");
            uname.startup();

            var dbpsd = new TextBox({ placeHolder: "密码",type:"password"}, "DbPsd");
            dbpsd.startup();
        });
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <%using (Html.BeginForm("DbCnf", "Install", FormMethod.Post, new { @id = "dbform" }))
      {
          %>
          <%=Html.ValidationSummary(true) %>
          
          <div>
            <%=Html.LabelFor(model=>model.DbAddr) %>
          
            <%=Html.TextBoxFor(model=>model.DbAddr) %>
            <%=Html.ValidationMessageFor(model=>model.DbAddr) %>
          </div>
          <div>
            <%=Html.LabelFor(model=>model.DbName) %>
            
          
            <%=Html.TextBoxFor(model=>model.DbName) %>
            <%=Html.ValidationMessageFor(model=>model.DbName) %>
            
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.DbUser) %>
            
         
            <%=Html.TextBoxFor(model=>model.DbUser) %>
            <%=Html.ValidationMessageFor(model=>model.DbUser) %>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.DbPsd) %>
            
          
            <%=Html.PasswordFor(model=>model.DbPsd) %>
            <%=Html.ValidationMessageFor(model=>model.DbPsd) %>
          </div>
          
          <div>
            <button id="submitBtn"></button>
          </div>
          <% 
          
      }
        
         %>
    </div>
    </asp:Content>