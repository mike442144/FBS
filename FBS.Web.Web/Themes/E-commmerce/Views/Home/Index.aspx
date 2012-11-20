<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function(){
                 LoadBanner('banners');
            });
        function enlarge(ctr)
        {
            var gridItem = $('#' + ctr.id);
            var className =gridItem.attr('class');
            
            
            if(gridItem.hasClass('gridItem'))
            {
                //grid
                
                gridItem.toggleClass('gridItem_on');
            }
            else
            {
                //list
                gridItem.toggleClass('gridItem_v_on');
            }
        }
        function displaySwitch(type)
        {
            var grid = $(".topic .grid");
            var list = $(".topic .list");
            if(type == 'grid')
            {
                if(grid.hasClass('grid_on'))
                    grid.removeClass('grid_on');
                else
                    grid.addClass('grid_on');
            }
            else
            {
                if(list.hasClass('list_on'))
                    list.removeClass('list_on');
                else
                    list.addClass('list_on');   
            }
        }
        function changeDisplay(ctr, id)
        {
            var source = $('#' + ctr.id);
            if(source.hasClass('listview'))
            {
                $('#' + id + ' .gridItem').removeClass('gridItem').addClass('gridItem_v');
            }
            else
            {
                $('#' + id + ' .gridItem_v').removeClass('gridItem_v').addClass('gridItem');
            }
        }
        function LoadBanner(ctr)
        {
            $('#' + ctr + ' .banner:gt(0) ').css("display","none");
            $('#' + ctr + ' ul li a').mouseover(function(){
                var index = $('#' + ctr + ' ul li a').index(this);
                $('#' + ctr + ' .banner').css("display","none");
                $('#' + ctr + ' .banner:eq(' + index+ ') ').fadeIn('slow');
            });
        }
    </script>
        <div id="banners" class="banners">
            <div class="banner">
                <img src="../../Content/images/Banner.gif" />
            </div>
            <div class="banner">
                <img src="../../Content/images/Banner_2.png" />
            </div>
            <div class="banner">
                <img src="../../Content/images/Banner_1.png" />
            </div>
            <ul>
                <li><a href="#0">ASP.NET MVC Design Competition</a></li>
                <li><a href="#1">SQL Server 2008 Express</a></li>
                <li><a href="#2">Visual Studio 2008</a></li>
            </ul>
        </div>
        <div id="gridTopic" class="topic">
            <h2>Feature Products</h2>
            <div class="display">
                <div id="gridTopic_grid" class="gridview" onmouseover="displaySwitch('grid')" onmouseout="displaySwitch('grid')" onclick="changeDisplay(this, 'grid')"></div>
                <div id="gridTopic_list" class="listview" onmouseover="displaySwitch('list')" onmouseout="displaySwitch('list')" onclick="changeDisplay(this, 'grid')"></div>
            </div>
        </div>
        <div id="grid" class="grid">
            <div id="p_1" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/Xbox360.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360 Arcade</a></div>
                    <div class="desc">The Xbox 360 Arcade console is everything you need to hit the ground running. Plug in the console and connect the wireless controller and you're playing.</div>
                </div>
            </div>
            <div id="p_2" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/Xbox360WirelessController.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360 Wireless controller</a></div>
                    <div class="desc">High-performance wireless gaming now comes in black! Using optimized technology, the black Xbox 360 Wireless Controller lets you enjoy a 30-foot range and up to 40 hours of life on the two included AA batteries</div>
                </div>
            </div>
            <div id="p_3" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/Xbox360WheelController.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360 Wireless Racing Wheel</a></div>
                    <div class="desc">Racing has never felt so real! Hold on tight as you hug corner after corner, skid through the sand, or trade paint with rival cars fighting for position—the wireless wheel* simulates all the resistance and force, immersing you in a relentless and unparalleled racing experience.</div>
                </div>
            </div>
            <div id="p_4" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/Xbox360Headset.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360 Headset</a></div>
                    <div class="desc">The Xbox 360 Headset heightens the experience of the Xbox LIVE® online gaming community, allowing you to strategize with teammates, trash-talk opponents, or just chat with friends while playing your favorite games</div>
                </div>
            </div>
            <div  id="p_5" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/Xbox360MemoryUnit.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360 Memory Unit (512MB)</a></div>
                    <div class="desc">Take your games everywhere you go with eight times the space of the original Xbox 360™ Memory Unit. With 512 MB of memory and a keychain carrying case</div>
                </div>
            </div>
            <div id="p_6" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/xbox360MessengerKit.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360 Messenger Kit</a></div>
                    <div class="desc">Chatting with friends and family on Xbox LIVE® and Windows-based PCs is easy using the Xbox 360 Messenger Kit. </div>
                </div>
            </div>
            <div id="p_7" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/xbox360Halo3EditionConsole.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360 Halo 3 Special Edition Console</a></div>
                    <div class="desc">The Xbox 360™ Halo® 3 Special Edition Console features an exclusive "Spartan green and gold" finish and comes bundled with a matching Xbox 360 Wireless Controller, 20GB Hard Drive, Headset, Play & Charge Kit, and exclusive Halo 3 Gamer Pics</div>
                </div>
            </div>
            <div id="p_8" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
                <div class="frame">
                    <img src="../../Content/images/xbox360HardDrive.jpg" />
                </div>
                <div class="content">
                    <div class="price">$199.99</div>
                    <div class="addtocart"><a href="#"><img src="../../Content/images/AddToCart.png" /></a></div>
                    <div class="name"><a href="">Xbox 360™ Hard Drive (120 GB)</a></div>
                    <div class="desc">The Xbox 360 120GB Hard Drive is the best option for media enthusiasts who game on Xbox 360™. It is the largest storage option for Xbox 360. Expand your Xbox 360 experience with downloadable content</div>
                </div>
            </div>
        </div>
        
</asp:Content>
