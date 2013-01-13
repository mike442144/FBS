<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" ViewStateMode="Disabled" Inherits="FBS.Web.News.Backstage.login" %>

<!DOCTYPE html">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta content="FBS" name="Description" />
<meta content="FBS" name="Keywords" />
<title>FBS</title>
<link type="text/css" rel="Stylesheet" href="/Scripts/dojo-release-1.8.1/dijit/themes/dijit.css" />
<link type="text/css" rel="Stylesheet" href="css/metro_teal.css" />
<script type="text/javascript" src="/Scripts/dojo-release-1.8.1/dojo/dojo.js" djConfig="parseOnLoad:true,isDebug:true"></script>
<style type="text/css">
body
{
    /* Font styles */
    font-family: 'Segoe UI Light', 'Open Sans', Verdana, Arial, Helvetica, sans-serif;
    background: #eedfcc url(images/bg3.jpg) no-repeat center top;
	-webkit-background-size: cover;
	-moz-background-size: cover;
	background-size: cover;
	overflow:hidden;
}
form
{
    /* Size and position */
    width: 300px;
    margin: 60px auto 30px;
    padding: 10px;
    position: relative;

    
    /*color: white;
    text-shadow: 0 2px 1px rgba(0,0,0,0.3);*/
}
form input[type="text"],form input[type="password"]
{
    width:320px;
}
form h1
{
    font-weight:200;
    color:White;
    text-shadow: 0 1px 1px rgba(0,0,0,0.5);
}
form label
{
    color:#fff;
    text-shadow: 0 1px 1px rgba(0,0,0,0.5);
}
</style>
</head>
<body class="metro">
<form action="login.aspx" method="post" runat="server">
	<h1>Login</h1>
	<p>
		<!--<label for="login">用户名或邮箱</label>-->
		<asp:TextBox ID="txtName" name="login" runat="server" data-dojo-type="dijit/form/TextBox" style="width:320px;" placeholder="Username or email"></asp:TextBox>
	</p>
	<p>
		<!--<label for="password">密码</label>-->
		<asp:TextBox ID="txtPwd" name="password" runat="server" data-dojo-type="dijit/form/TextBox" TextMode="Password" style="width:320px;" placeholder="Password"></asp:TextBox>
	</p>

	<p>
		<asp:Button ID="btn" runat="server" data-dojo-type="dijit/form/Button" data-dojo-props="label:'登录'" OnClick="ImageButton1_Click" />
	</p>
<span id="msg" runat="server"></span>
</form>
</body>
</html>
