<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated)
    {
%>
Welcome <b>
    <%= Html.Encode(Page.User.Identity.Name) %></b>! [
<%= Html.ActionLink("Logout", "Logout", "Account") %>
]
<br />
<%= Html.ActionLink("Update Profile", "Edit", "Account", new { userName = Page.User.Identity.Name }, null ) %>
<%
    }
    else
    {
        using (Html.BeginForm("Login", "Account", FormMethod.Post))
        {
%>
<table cellpadding='2'>
    <tr>
        <td>
        </td>
        <td class="limelabel">
            Username:
        </td>
        <td class="limelabel">
            Password:
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="padding-right: 10px;">
            <%= Html.ActionLink("Login |", "Login", "Account")%>
            <%= Html.ActionLink("Register", "Register", "Account")%>
        </td>
        <td>
            <%= Html.TextBox("userName", null, (object)new { @size = "12" })%>
        </td>
        <td>
            <%= Html.TextBox("password", null, (object)new { @size = "12" })%>
        </td>
        <td>
            <%= Html.Hidden("rememberMe", "false") %>
            <%= Html.Hidden("returnUrl", Request.Url) %>
            <input id="gobutton" type="submit" value="Go" />
        </td>
    </tr>
</table>
<%
    }
    }
%>
