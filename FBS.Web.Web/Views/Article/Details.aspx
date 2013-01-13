<%@ Page Title="详细" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.ArticleDetailsModel>" %>
<asp:Content ContentPlaceHolderID="Head" runat='server'>
<script type="text/javascript">
    require(["dojo/request/xhr", "dojo/domReady!"], function (xhr) {
        var reg = /\/article\/details\/([a-f0-9\-]{36})/ig;
        var str = document.location.pathname;
        var result = reg.exec(str);
        if (result && result.length === 2) {
            dojo.byId('msg').innerHTML = 'loading...please wait';
            xhr("/Article/Details/", {
                method: "POST",
                handleAs: "json",
                data: { id: result[1] }
            }).then(function (data) {
                //TODO
                dojo.byId('msg').innerHTML = data.Body;
            });
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<span id='msg'></span>    
    
</asp:Content>