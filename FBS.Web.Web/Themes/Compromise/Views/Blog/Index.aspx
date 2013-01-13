<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBS.Service.ActionModels.BlogStoryDspModel>>" %>
<asp:Content ID="blogsTitle" ContentPlaceHolderID="TitleContent" runat="server">
<%=ViewData["CategoryName"]??""%>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<%foreach (var m in Model)
  { %>
  <h2><a href="/b/<%=m.StoryID %>"><%=m.Title %></a></h2>
  <date><%=m.PublishTime %></date>
  <div><%=m.Description %></div>
<%} %>
</asp:Content>