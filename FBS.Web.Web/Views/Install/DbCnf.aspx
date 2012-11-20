<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfDbCnf>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DbCnf</title>
</head>
<body>
    <div>
    <%using (Html.BeginForm("DbCnf", "Install", FormMethod.Post, new { @id = "dbform" }))
      {
          %>
          <%=Html.ValidationSummary(true) %>
          
          <div>
            <%=Html.LabelFor(model=>model.DbAddr) %>
          </div>
          <div class="">
            <%=Html.TextBoxFor(model=>model.DbAddr) %>
            <%=Html.ValidationMessageFor(model=>model.DbAddr) %>
          </div>
          <div>
            <%=Html.LabelFor(model=>model.DbName) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model=>model.DbName) %>
            <%=Html.ValidationMessageFor(model=>model.DbName) %>
            
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.DbUser) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model=>model.DbUser) %>
            <%=Html.ValidationMessageFor(model=>model.DbUser) %>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.DbPsd) %>
            
          </div>
          <div>
            <%=Html.PasswordFor(model=>model.DbPsd) %>
            <%=Html.ValidationMessageFor(model=>model.DbPsd) %>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.DbTablePrefix) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model=>model.DbTablePrefix) %>
          </div>
          
          <div>
            <input type="submit" value="下一步" />
          </div>
          <% 
          
      }
        
         %>
    </div>
</body>
</html>
