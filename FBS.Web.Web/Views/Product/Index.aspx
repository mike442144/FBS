<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
<title>产品展示</title>
<link href="../../css/workshow.css" rel="Stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">

<div class="navitem"><a href="/Home/Index">首页</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Home/About">企业简介</a></div>
<div class="navfenge"></div>
<div class="navitemCurrent"><a href="/Product/Index">作品展示</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Service/Index">服务项目</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/News/NewsList">企业动态</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Jobs/Index">招贤纳士</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Home/Contact">联系我们</a></div>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--左侧导航begin -->
<div class="leftnav">

<div class="leftnav_block">
<div class="leftnav_title">我们的作品</div>
<ul>
<li><a href="#">我们的投资组合</a></li>
<li><a href="#">我们的奖项</a></li>
<li><a href="#">我们的荣誉</a></li>
</ul>
</div>

</div>
<!--左侧导航end -->
<!--右侧内容begin -->
<div class="workshow">

<div class="workshow_title"><h3>我们的作品</h3></div>

<div class="resultblock_up">

<div class="workshow_result">显示结果1-9共100</div>
<div class="next"><a href="#">下一页&raquo;</a></div>
<div class="return"><a href="#">&laquo;返回</a>&nbsp;|&nbsp;</div>

</div>
<div class="blank0"></div>

<div class="workshow_content">
  <div class="workshowbox_block">
     <ul>
       <li>
         <a href="#">
           <img src="../../images/bigshow/huayu.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/jiaju.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/jiaoyu.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/jiudian.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/njl.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/qinghang.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/fxzhk.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/qinghang.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
       
       <li>
         <a href="#">
           <img src="../../images/bigshow/qinghang.png" width="168" height="100"/>
           <span>XXX 网站</span>
         </a>
       </li>
     </ul>  
  </div>
  
</div>

<div class="blank0"></div>

<div class="resultblock_down">

<div class="next"><a href="#">下一页&raquo;</a></div>
<div class="return"><a href="#">&laquo;返回</a>&nbsp;|&nbsp;</div>

</div>
<div class="blank0"></div>
</div>
<!--右侧内容begin -->
<div class="blank0"></div>

</asp:Content>
