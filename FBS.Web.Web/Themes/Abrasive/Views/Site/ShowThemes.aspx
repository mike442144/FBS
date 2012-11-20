<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="themesTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="themesContent" ContentPlaceHolderID="MainContent" runat="server">
<%Html.RenderPartial("JsClient"); %>
        <form method="post" action="/Site/FetchThemes">
<input type="button" value="查看皮肤" onclick="getThemeJson()" />
</form>
    
</asp:Content>
