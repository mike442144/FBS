<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage"%>
    <p>
        Please enter your username and password. If you don't have an account,
        please <%= Html.ActionLink("Register", "Register") %>.
    </p>
    <% using (Html.BeginForm("Logon", "Account")) { %>
        <label for="username">Username:</label>
        <%= Html.TextBox("username") %>
        <%= Html.ValidationMessage("username") %>
        <label for="password">Password:</label>
        <%= Html.Password("password") %>
        <%= Html.ValidationMessage("password") %>
        <%= Html.CheckBox("rememberMe") %> <label for="rememberMe">Remember me?</label>
        <input type="submit" value="Login" />
<% } %>
