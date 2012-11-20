<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Installed</title>
    <script type="text/javascript">
        window.onload = function() {
            setTimeout(function() {
                window.location = "/Home/Index/";
            }, 3000);
        }
    </script>
</head>
<body>
    <div>
    <h4>网站已经安装过,请勿重复操作！</h4>
    <h5>3秒后自动跳转到网站首页</h5>
    </div>
</body>
</html>
