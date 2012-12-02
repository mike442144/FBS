<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfDbCnf>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DbCnf</title>
    <link media="screen" rel="Stylesheet" href="../../Scripts/dojo-release-1.8.1/dijit/themes/soria/soria.css" />
    <script src="../../Scripts/dojo-release-1.8.1/dojo/dojo.js" type="text/javascript"></script>
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
</head>
<body class="soria">
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
</body>
</html>
