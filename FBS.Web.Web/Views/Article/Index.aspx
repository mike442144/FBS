<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FBS.Service.ActionModels.ArticleDspModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Title
            </th>
            <th>
                BriefTitle
            </th>
            <th>
                ArticleID
            </th>
            <th>
                CreatedOn
            </th>
            <th>
                ClickCount
            </th>
            <th>
                ImgName
            </th>
            <th>
                SourceUrl
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%= Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%> |
                <%= Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })%>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.BriefTitle) %>
            </td>
            <td>
                <%= Html.Encode(item.ArticleID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.CreatedOn)) %>
            </td>
            <td>
                <%= Html.Encode(item.ClickCount) %>
            </td>
            <td>
                <%= Html.Encode(item.ImgName) %>
            </td>
            <td>
                <%= Html.Encode(item.SourceUrl) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
</asp:Content>

