<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.BlogStoryDetailsModel>" %>
<asp:Content ID="pageTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.Title %>
</asp:Content>
<asp:Content ID="styles" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript" charset="utf-8" src="/Content/ueditor/third-party/SyntaxHighlighter/shCore.js"></script>
    <link rel="stylesheet" type="text/css" href="/Content/ueditor/third-party/SyntaxHighlighter/shCoreDefault.css" />
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
    <h1><a href="/b/<%=Model.StoryID %>"><%=Model.Title %></a></h1>
    <%=Model.Content %>
    <script type="text/javascript">
        SyntaxHighlighter.highlight();
    </script>
</asp:Content>