<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/FBS_Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.UserLogOnModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.UserName) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.UserName) %>
                <%= Html.ValidationMessageFor(model => model.UserName) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Password) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Password) %>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.RememberMe) %>
            </div>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.RememberMe) %>
                <%= Html.ValidationMessageFor(model => model.RememberMe) %>
            </div>
            
            <p>
                <input type="submit" value="登录" />
                <input type="reset" value="清空" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <a href="/Home/Index">返回网站</a>
    </div>

</asp:Content>

