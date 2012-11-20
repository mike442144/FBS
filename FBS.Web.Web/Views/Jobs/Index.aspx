<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
<link href="../../css/zhaoren.css" rel="stylesheet" type="text/css"/>
<title>招贤纳士</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
<div class="navitem"><a href="/Home/Index">首页</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Home/About">企业简介</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Product/Index">作品展示</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Service/Index">服务项目</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/News/NewsList">企业动态</a></div>
<div class="navfenge"></div>
<div class="navitemCurrent"><a href="/Jobs/Index">招贤纳士</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Home/Contact">联系我们</a></div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="welcomebox">
<div class="welcome">Welecome</div>
<div class="join">加入惊雷</div>
<div class="jointext1">你可以与我们一起凝聚力量</div>
<div class="jointext2">体会分享知识带来的快乐</div>
<div class="lineblack"></div>
<div class="chinesewelcome">惊雷网络欢迎您的加入！</div>
<div class="blank0"></div>
<div class="photolinebox">
<div class="leftline"></div>
<div class="teamphoto"><img src="../images/team.png" /></div>
<div class="rightline"></div>
</div>
</div>
<!--welcome的那块 end -->
<!--招聘列表开始 -->
<div class="zhaopintitle">
        <div class="zhaopintitleboxpositioname">职位名称</div>
        <div class="zhaopintitleboxconditions">职位要求</div>
        <div class="zhaopintitleboxdate">发布时间</div>
</div>

<div class="zhaopinlistcontent"> 
    <table border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="borderbottom"><div class="positionname">PHP程序员</div></td>
    <td class="borderbottom">
      <div class="conditions">
        	<ul>
            	<li>1、熟悉Web编程，对PHP有深入了解；</li>
                <li>2、对大型Web应用架构有一定经验，并能够对大用户量、高并发网络应用提出解决方案；</li>
                <li>3、熟悉MySQL数据库、Memcached，对Non-SQL有一定研究；</li>
                <li>4、常用环境搭建，如：apache、php、mysql、nginx、memcache等，和其相关的参数设置和性能优化；</li>
                <li>5、具有较为扎实的计算机基础学科知识，熟悉数据结构、操作系统等。</li>
            </ul>
        </div>
    </td>
    <td class="borderbottom"><div class="date">2011-8-23</div></td>
  </tr>
  <tr>
    <td class="borderbottom"><div class="positionname">C#程序员</div></td>
    <td class="borderbottom">
      <div class="conditions">
        	<ul>
            	<li>1、熟练掌握面向对象的程序设计及开发；</li>
                <li>2、具有丰富的ASP.NET应用开发经验，精通C#；</li>
                <li>3、具有软件设计思想，对软件设计模式有一定研究；</li>
                <li>4、有大型网站开发经验者优先或有框架开发经验者优先，熟悉ASP.NET MVC者优先；</li>
                <li>5、为人诚信，有责任心，具备良好的沟通能力和团队合作精神；</li>
                <li>6、具体较为扎实的计算机基础学科知识，熟悉数据结构、操作系统等。</li>
            </ul>
        </div>
    </td>
    <td class="borderbottom"><div class="date">2011-8-23</div></td>
  </tr>
  <tr>
    <td><div class="positionname">销售代表</div></td>
    <td>
      <div class="conditions">
        	<ul>
            	<li>1、有互联网产品销售经验者优先</li>
                <li>2、对销售工作具有很大的工作热情</li>
                <li>3、善于沟通。富有团队进取精神</li>
            </ul>
        </div>
    </td>
    <td><div class="date">2011-8-23</div></td>
  </tr>
</table>

</div>
<!--招聘列表结束 -->
<div class="blank0"></div>
<div class="messageback">
  <div class="messageback_left">
  <div class="why">为何选择惊雷</div>
  <div class="messageback_text">
    <p>您为什么要在<strong> 惊雷 </strong>工作？您敢于接受挑战，喜欢与各自领域的顶尖人才在一起工作。您希望寻求发展机会，乐于接受变化。您希望：</p>
  </div>
  <ul>
    <li></li>
    <li></li>
    <li></li>
  </ul>
  </div>
  <div class="messageback_right">
    <p>联系人：陈小姐</p>
    <p>联系电话：0513-81181061</p>
    <p>简历投递邮箱：chen.sn@0513jl.com</p>
  </div>
</div>

</asp:Content>
