<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/FBS_Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowThemes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ShowThemes</h2>
<form method="post" action="Site/FetchThemes">
<input type="submit" value="查看皮肤" />
</form>
</asp:Content>
