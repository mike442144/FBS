<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
<%=Helpers.SharedData.SiteName %>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
简单介绍下呗
</asp:Content>