<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <title>Home Page</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <a class="baslik" href="<%= Url.Action("#","Home") %>"><%= Html.Encode(ViewData["Message"]) %></a>
    <div class="icerik">
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a><br><br>
    </div>
    <div class="ayrintilar">
        <div class="ayrinti_satir">
            <span class="ic_yazar">Mehmet Duran</span>
            <span class="ic_tarih">28 June Sunday 2009</span>
            <span class="ic_yorum">2</span>
            <span class="ic_okunma">100</span>
            <span class="ic_puan">5.0</span>
        </div>
        <div class="ayrinti_satir">
            <span class="ic_kategori"><a href="<%= Url.Action("#","Home") %>">Category 1</a><a href="<%= Url.Action("#","Home") %>">Category 2</a></span>
            <span class="ic_etiket"><a href="<%= Url.Action("#","Home") %>">Tag 1</a><a href="<%= Url.Action("#","Home") %>">Tag 2</a><a href="<%= Url.Action("#","Home") %>">Tag 3</a><a href="<%= Url.Action("#","Home") %>">Tag 4</a></span>
        </div>
    </div>
    <!--First Part End-->
    <a class="baslik" href="<%= Url.Action("#","Home") %>">Blog Writing Head</a>
    <div class="icerik">
        Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aliquam id tellus. Nulla orci enim, vulputate et, pharetra eget, imperdiet non, sem. Mauris sit amet mi nec nulla porttitor dapibus. Curabitur leo sem, lacinia sed, commodo eu, mattis sit amet, felis. Ut tortor. Donec porttitor orci et neque. Curabitur eget diam at libero egestas suscipit. In tortor est, ullamcorper eu, dapibus et, condimentum nec, nunc. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Duis vel purus eget diam aliquam luctus. Nullam risus ipsum, aliquam et, lacinia sit amet, fermentum vel, mi. In hac habitasse platea dictumst. Maecenas et dui non tortor lobortis feugiat. Donec eleifend iaculis arcu. Cras vitae leo nec nunc rhoncus laoreet. Integer eget enim. Nunc dignissim cursus mi. Donec eros.
    </div>
    <div class="ayrintilar">
        <div class="ayrinti_satir">
            <span class="ic_yazar">Mehmet Duran</span>
            <span class="ic_tarih">28 June Sunday 2009</span>
            <span class="ic_yorum">11</span>
            <span class="ic_okunma">200</span>
            <span class="ic_puan">4.2</span>
        </div>
        <div class="ayrinti_satir">
            <span class="ic_kategori"><a href="<%= Url.Action("#","Home") %>">Category 1</a><a href="<%= Url.Action("#","Home") %>">Category 2</a></span>
            <span class="ic_etiket"><a href="<%= Url.Action("#","Home") %>">Tag 1</a><a href="<%= Url.Action("#","Home") %>">Tag 2</a><a href="<%= Url.Action("#","Home") %>">Tag 3</a><a href="<%= Url.Action("#","Home") %>">Tag 4</a><a href="<%= Url.Action("#","Home") %>">Tag 5</a><a href="<%= Url.Action("#","Home") %>">Tag 6</a></span>
        </div>
    </div>
    
    <!--page numbers-->
    <div id="sayfalar">
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">Previous</a>
        <a class="aktif_sayfa" href="<%= Url.Action("#","Home") %>">1</a>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">2</a>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">3</a>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">4</a>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">5</a>
        <span>....</span>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">20</a>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">21</a>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">22</a>
        <a class="sayfa" href="<%= Url.Action("#","Home") %>">Next</a>
    </div>
</asp:Content>