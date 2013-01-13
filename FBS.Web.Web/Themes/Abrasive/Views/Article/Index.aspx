<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<FBS.Service.ActionModels.ArticleDspModel>>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    ÎÒµÄ²©¿Í
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <%foreach (var model in Model)
      { %>
    <div class="post">
        <div class="title">
            <h2>
                <a href=""><%=model.Title%></a></h2>
            <p>
                Posted by <a href="#">enks</a> on <%=model.CreatedOn.ToString("yyyy-MM-dd HH:mm")%></p>
        </div>
        <div class="entry">
            <img src="/Themes/Abrasive/Content/Images/img01.jpg" alt="" width="112" height="104"
                class="alignleft" />
                <p id='art_body'><%%></p>
        </div>
    </div>
    <%} %>
</asp:Content>
