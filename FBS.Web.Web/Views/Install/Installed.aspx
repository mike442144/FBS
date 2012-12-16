<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Install.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ContentPlaceHolderID="Head">
    <script type="text/javascript">
        window.onload = function() {
            setTimeout(function() {
                window.location = "/Home/Index/";
            }, 3000);
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent">
    <div>
    <h4>网站已经安装过,请勿重复操作！</h4>
    <h5>3秒后自动跳转到网站首页</h5>
    </div>
    </asp:Content>