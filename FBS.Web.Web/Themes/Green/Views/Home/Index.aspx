<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
       The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller. The ASP.NET MVC framework provides an alternative to the ASP.NET Web Forms pattern for creating MVC-based Web applications. 
       <br /><br /><br />
       The ASP.NET MVC framework is a lightweight, highly testable presentation framework that (as with Web Forms-based applications) is integrated with existing ASP.NET features, such as master pages and membership-based authentication. The MVC framework is defined in the System.Web.Mvc namespace and is a fundamental, supported part of the System.Web namespace. 
        <span class="options">
            <ul>
                <li><a href="">More</a></li>
                <li><a href="">Comments</a></li>
            </ul>
       </span>
    </p>
    <h2>MVC Design Template for Green</h2>
    <p>
        The shining green design template is built for blog or personal website. If you wish to know more about this MVC design template. please visit <a href="http://www.protienshow.com">here</a>.
        <br /><br /><br />
        If you like my design, I recommand you to <a href="http://www.asp.net/mvc/gallery/View.aspx?itemid=75">visit</a> another MVC design template that I made for e-commerce website, it uses jQuery and features client-side interactive, tableless design, CSS sprites technique, wide browser compatibility. 
       <span class="options">
            <ul>
                <li><a href="">More</a></li>
                <li><a href="">Comments</a></li>
            </ul>
       </span>
    </p>
    <h2>My Photo Gallery</h2>
    <p>
        <div class="gallery">
            <div class="photo">
                Photo 1
            </div>  
            <div class="photo">
                Photo 2
            </div> 
            <div class="photo">
                Photo 3
            </div>  
            <div class="photo">
                Photo 4
            </div>  
        </div>
    </p>
    
</asp:Content>
