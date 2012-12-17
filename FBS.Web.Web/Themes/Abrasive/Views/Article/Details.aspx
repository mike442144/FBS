<%@ Page Language="C#" MasterPageFile="../Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="post">
        <div class="title">
            <h2>
                <a href="#">%art.Title</a></h2>
            <p>
                Posted by <a href="#">enks</a> on %art.CreationDate</p>
        </div>
        <div class="entry">
            <img src="/Themes/Abrasive/Content/Images/img01.jpg" alt="" width="112" height="104"
                class="alignleft" />
                <p id='art_body'>%art.Body</p>
        </div>
    </div>
</asp:Content>
