<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	User Account
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		User Account for
		<%= Html.Encode( Page.User.Identity.Name )%></h2>
		
	<% using( Html.BeginForm() )
	{ %>
	<div>
		<fieldset>
			<legend>Account Information</legend>
			<p>
				<label for="userName" class="inline">
					User Name:</label>
				<%= Html.Encode( Page.User.Identity.Name )%>
			</p>
			<p>
				<%= Html.ActionLink( "Change password", "ChangePassword", "Account" ) %>
			</p>
		</fieldset>
	</div>
	<% } %>
</asp:Content>
