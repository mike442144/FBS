<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfSiteCnf>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SiteCnf</title>
</head>
<body>
    <div>
    <%using (Html.BeginForm("SiteCnf", "Install", FormMethod.Post, new { @id = "siteform" }))
      {
          %>
          <%=Html.ValidationSummary(true) %>
          
          <div>
            <%=Html.LabelFor(model=>model.SiteName) %>
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.SiteName)%>
            <%=Html.ValidationMessageFor(model => model.SiteName)%>
          </div>
          <div>
            <%=Html.LabelFor(model=>model.SiteDesc) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.SiteDesc)%>
            <%=Html.ValidationMessageFor(model => model.SiteDesc)%>
            
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.SiteUrl) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.SiteUrl)%>
            <%=Html.ValidationMessageFor(model => model.SiteUrl)%>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.FounderName) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.FounderName)%>
            <%=Html.ValidationMessageFor(model => model.FounderName)%>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.FounderPsd) %>
            
          </div>
          <div>
            <%=Html.PasswordFor(model => model.FounderPsd)%>
            <%=Html.ValidationMessageFor(model => model.FounderPsd)%>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.FounderEmail) %>
            
          </div>
          <div>
            <%=Html.PasswordFor(model => model.FounderEmail)%>
            <%=Html.ValidationMessageFor(model => model.FounderEmail)%>
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
