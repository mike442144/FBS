<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="top-image">
		<h2>Heading 2</h2>
	</div>
	<div id="content-main">
		<!-- <p><img src="../../Content/image.jpg" alt="Gate" /></p> -->
		<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
		<ul>
			<li>List Item</li>
			<li>List Item</li>
		</ul>
		<h3>Heading 3</h3>
		<h4>Heading 4</h4>
		<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
	</div>
	<div id="content-sub">
		<p class="twitter"><a href="#"><img src="../../Content/twitter.gif" alt="Twitter" /></a></p>
		<h3>More Nav/Links</h3>
		<ul>
			<li><a href="#">Item 1</a></li>
			<li><a href="#">Item 2</a></li>
			<li><a href="#">Item 3</a></li>
		</ul>
	</div>
	<div style="clear: both;"></div>
</asp:Content>
