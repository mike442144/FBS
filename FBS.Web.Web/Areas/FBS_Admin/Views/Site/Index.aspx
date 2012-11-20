<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/FBS_Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfSiteCnf>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">SiteName</div>
        <div class="display-field"><%= Html.Encode(Model.SiteName) %></div>
        
        <div class="display-label">SiteDesc</div>
        <div class="display-field"><%= Html.Encode(Model.SiteDesc) %></div>
        
        <div class="display-label">SiteUrl</div>
        <div class="display-field"><%= Html.Encode(Model.SiteUrl) %></div>
        
        <div class="display-label">FounderName</div>
        <div class="display-field"><%= Html.Encode(Model.FounderName) %></div>
        
        <div class="display-label">FounderPsd</div>
        <div class="display-field"><%= Html.Encode(Model.FounderPsd) %></div>
        
        <div class="display-label">FounderEmail</div>
        <div class="display-field"><%= Html.Encode(Model.FounderEmail) %></div>
        
    </fieldset>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>


</asp:Content>

