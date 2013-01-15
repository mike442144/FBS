<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<FBS.Service.ActionModels.BlogStoryDspModel>>" %>
<asp:Content ID="blogsTitle" ContentPlaceHolderID="TitleContent" runat="server">
<%=ViewData["CategoryName"] ?? Helpers.SharedData.SiteName%>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
<%foreach (var m in Model)
  { %>
  <h2><a href="/b/<%=m.StoryID %>" target="_self"><%=m.Title %></a></h2>
  <time class="meta"><%=m.PublishTime.ToString("yyyy-MM-dd HH:mm") %></time>
  &nbsp;&nbsp;&nbsp;&nbsp;
  <span class="meta"><a href="/Blog/Index/<%=m.CategoryID %>" target="_self"><%=m.CategoryName %></a></span>
  <span class="meta">тд╤а(<%=m.ReadCount %>)</span>
  <p class="entry"><%=m.Description %>[......]</p>
  <p style="text-align:right;"><a href="/b/<%=m.StoryID %>">х╚нд</a></p>
<%} %>
</asp:Content>