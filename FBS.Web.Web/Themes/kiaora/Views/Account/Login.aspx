<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

<title>Sign In</title>

<meta http-equiv="content-type" content="application/xhtml+xml; charset=utf-8" />
<meta name="robots" content="index, follow" />
<meta name="author" content="Marty Batten" />

<link rel="stylesheet" href="../../Content/core.css" media="screen,projection" type="text/css" />

</head>

<body>

<div id="signin">
	<h3>Sign In</h3>
	<form method="post" action="../Account/Login">
	    <p><%= Html.TextBox("username", "", new { @class = "signin" })%></p>
	    <p><%= Html.Password("password", "", new { @class = "signin" })%></p>
	    <p><input class="btn" type="submit" value="Sign In" /></p>
	</form>
</div>

</body>
</html>