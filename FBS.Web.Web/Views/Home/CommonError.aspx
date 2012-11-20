<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   
  <meta http-equiv="imagetoolbar" content="no" />
  <link rel="icon" type="image/x-icon" href="../../favicon.ico" />
  <link rel="shortcut icon" type="image/x-icon" href="../../favicon.ico" />
  <meta name="robots" content="noindex,nofollow" />
  <title>出错了！</title>

		<style>
		body {background: #f9fee8;margin: 0; padding: 20px; text-align:center; font-family:Arial, Helvetica, sans-serif; font-size:14px; color:#666666;}
		.error_page {width: 600px; padding: 50px; margin: auto;}
		.error_page h1 {margin: 20px 0 0;}
		.error_page p {margin: 10px 0; padding: 0;}		
		a {color: #9caa6d; text-decoration:none;}
		a:hover {color: #9caa6d; text-decoration:underline;}
		</style>
</head>
<body>
   <div class="error_page">
    <img alt="Kidmondo_face_sad" src="http://kidmondo.com/wp-content/themes/kidmondo2/images/kidmondo_face_sad.gif" />
    <%if (ViewData["CommonError"] != null)
      {
          string errorMessage = ViewData["CommonError"] as string; 
      %>
    <h1><%=errorMessage %></h1>
    <%} %>
    <p><a href="/Home/Index">返回首页</a></p>
  </div>
</body>
</html>
