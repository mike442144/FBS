<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.ArticleDetailsModel>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.Title %>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="post">
        <div class="title">
            <h2>
                <a href=""><%=Model.Title %></a></h2>
            <p>
                Posted by <a href="#">enks</a> on <%=Model.CreationDate.ToString("yyyy-MM-dd HH:mm") %></p>
        </div>
        <div class="entry">
            <img src="/Themes/Abrasive/Content/Images/img01.jpg" alt="" width="112" height="104"
                class="alignleft" />
                <p id='art_body'><%=Model.Body %></p>
        </div>
    </div>
</asp:Content>
