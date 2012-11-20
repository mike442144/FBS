<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
        
        //从cookie获取用户名及用户的ID
        HttpCookie hc = Request.Cookies[FormsAuthentication.FormsCookieName];
        FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(hc.Value);
%>
        欢迎 <%=fat.UserData %>[ <%= Html.ActionLink("登出", "LogOut", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("登录", "Login", "Account") %> ]
<%
    }
%>

