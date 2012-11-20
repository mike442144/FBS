<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
<title>企业简介</title>
<link href="../../css/introduce.css" rel="stylesheet" type="text/css"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
<div class="navitem"><a href="/Home/Index">首页</a></div>
<div class="navfenge"></div>
<div class="navitemCurrent"><a href="/Home/About">企业简介</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Product/Index">作品展示</a></div>
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

    <!--展示begin -->
<div class="bigshowbox">
  <div class="bigshowbox_topcontain">
  <div class="leftteamimgshow">
    <img src="../../images/teamren.png" width="750" height="402" border="0" />
  </div>
  <div class="rightkouhao">
  <table border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td colspan="4" style="color:#FAF703; font-size:16px; text-align:center;">我们的团队</td>
    </tr>
  <tr>
    <td>&nbsp;</td>
    <td colspan="3">&nbsp;</td>
    </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="float:left; vertical-align:bottom;padding-left:5px; padding-right:20px;">团队</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td style="float:right; vertical-align:bottom; padding-left:20px; padding-right:5px;">创意</td>
    <td style="font-size:12px;vertical-align:top;padding-left:5px; padding-right:25px;">Team</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td style="font-size:12px; float:right; vertical-align:top;padding-right:15px;">Idea</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="vertical-align:bottom; text-align:center;">行动</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td style="font-size:12px; text-align:center; vertical-align:top;">Action</td>
    <td style="float:left; vertical-align:bottom;padding-left:5px; padding-right:25px;">效率</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td style="font-size:12px; float:left; vertical-align:top;padding-left:5px; padding-right:5px;">Efficiency</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
  </div>
  </div>
  <div class="blank0"></div>
  <div class="gonggao">
<strong>最新动态：</strong>过去，我们如惊雷前的蓄势待发；现在，我们似随惊雷闪电绽放；未来，我们一定让你如惊雷贯耳</div>
</div>


<!--展示end -->
<div class="blank0"></div>
<!--公司简介begin -->
<div class="companyIntroducebox">
<div class="titlebox">
<div class="titlebox_title"> >> 公司简介</div>
<div class="titlebox_kouhao">过去，我们如惊雷前的蓄势待发；现在，我们似随惊雷闪电绽放；未来，我们一定让你如惊雷贯耳</div>
</div>
<div class="jiabbianblank8"></div>
<div class="companyIntroducetext">
<%--<p>南通惊雷网络科技有限公司（下称惊雷网络），成立于2009年，是南通一家专业的计算机技术类公司。公司为各个层次的用户提供网站建设、软件定制、企业解决方案制作等专业服务。公司以“创意团队行动效率”为核心价值，希望通过我们团队的创意和效率，更好的为每个客户服务。</p>
<p>三年来，惊雷网络一直秉承以用户需求为核心，在对南通本地市场开拓的同时，也把业务领域扩大到苏州、南京等地。优质、用心的服务赢得了众多企业的信赖和好评，在当地逐渐树立起公司良好品牌。我们相信，通过我们的不断努力和追求，一定能够实现惊雷网络和客户企业的互利双赢！南通惊雷网络科技有限公司（下称惊雷网络），成立于2009年，是南通一家专业的计算机技术类公司。公司为各个层次的用户提供网站建设、软件定制、企业解决方案制作等专业服务。公司以“创意团队行动效率”为核心价值，希望通过我们团队的创意和效率，更好的为每个客户服务。三年来，惊雷网络一直秉承以用户需求为核心，在对南通本地市场开拓的同时，也把业务领域扩大到苏州、南京等地。优质、用心的服务赢得了众多企业的信赖和好评，在当地逐渐树立起公司良好品牌。我们相信，通过我们的不断努力和追求，一定能够实现惊雷网络和客户企业的互利双赢！</p>--%>
<%if (ViewData["Introduce"] != null)
  {
      var aboutcompany = ViewData["Introduce"] as FBS.Service.ActionModels.ArticleDetailsModel;%>
<%=aboutcompany.Body%>
<%}
  else
  {%>
  <span>暂无企业资讯...</span>
<%} %>
</div>
</div>
<!--公司简介end -->

<!--成员介绍begin -->
<div class="member">
<div class="member_title">
<div style="width:280px!important;width:300px; margin:0 auto;">
<img src="../../images/star.png" style="float:left;" />
<span style="float:left;">霸气的惊雷网络因你的加入而更加精彩</span>
<img src="../../images/star.png" style="float:left;"/>
</div>
<div class="blank0"></div>
</div>

<div class="memberintrodce">

<div class="memberbox">
<div class="memberphoto"><img src="../../images/chenjinglei.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">陈惊雷</p>
<p class="position">技术总监</p>
<ul class="personIntroduce">
<li>&nbsp;</li>
<li>&nbsp;</li>
<li>&nbsp;</li>
<li>&nbsp;</li>
</ul>
</div>
</div>

<div class="memberbox">
<div class="memberphoto"><img src="../../images/yaoxiaohui.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">姚小惠</p>
<p class="position">美术设计师</p>
<ul class="personIntroduce">
<li>&nbsp;</li>
<li>&nbsp;</li>
<li>&nbsp;</li>
<li>&nbsp;</li>
</ul>
</div>
</div>

<div class="memberbox">
<div class="memberphoto"><img src="../../images/chensainan.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">陈赛男</p>
<p class="position">行政专员</p>
<ul class="personIntroduce">
<li>喜欢睡觉、听音乐</li>
<li>经常会上网淘小说，</li>
<li>是超级恋家的宅女一</li>
<li>枚</li>
</ul>
</div>
</div>

</div>

<div class="blank0"></div>

<div class="memberintrodce">

<div class="memberbox">
<div class="memberphoto"><img src="../../images/gudan.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">顾丹</p>
<p class="position">客户经理</p>
<ul class="personIntroduce">
<li>热爱阳光，热爱潇洒</li>
<li>，热爱生活。热爱工</li>
<li>作，但我更热爱与惊</li>
<li>雷一起成长的日子</li>
</ul>
</div>
</div>

<div class="memberbox">
<div class="memberphoto"><img src="../../images/caojun.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">曹军</p>
<p class="position">软件设计师</p>
<ul class="personIntroduce">
<li>不要等每一盏灯都熄</li>
<li>灭，才期盼光明；不</li>
<li>要等折断了翅膀，才</li>
<li>怀念广阔的蓝天。</li>
</ul>
</div>
</div>

<div class="memberbox">
<div class="memberphoto"><img src="../../images/sunxue.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">孙雪</p>
<p class="position">美术设计师</p>
<ul class="personIntroduce">
<li>漫画 动画 只要是美</li>
<li>型的二次元都喜欢 </li>
<li>还有，喜欢绘画。</li>
<li>&nbsp;</li>
</ul>
</div>
</div>

</div>

<div class="blank0"></div>

<div class="memberintrodce">

<div class="memberbox">
<div class="memberphoto"><img src="../../images/qianyinpeng.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">钱银鹏</p>
<p class="position">销售</p>
<ul class="personIntroduce">
<li>真正彪悍的人生不需</li>
<li>要任何解释！</li>
<li>&nbsp;</li>
<li>&nbsp;</li>
</ul>
</div>
</div>

<div class="memberbox">
<div class="memberphoto"><img src="../../images/guoguoyu.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">郭国玉</p>
<p class="position">软件设计师</p>
<ul class="personIntroduce">
<li>谁也不能随随便便成</li>
<li>功，它来自彻底的自</li>
<li>我管理和毅力。</li>
<li>&nbsp;</li>
</ul>
</div>
</div>

<div class="memberbox">
<div class="memberphoto"><img src="../../images/wangjinxing.png" height="173" width="168" /></div>
<div class="membertext">
<p class="name">王金星</p>
<p class="position">美术设计师</p>
<ul class="personIntroduce">
<li>简单简洁 不复杂就</li>
<li>行闲暇逛街、收集美</li>
<li>图。</li>
<li>&nbsp;</li>
</ul>
</div>
</div>

</div>

<div class="blank0"></div>

</div>

</div>

<!--成员介绍end -->

</asp:Content>