<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
<%if (ViewData["Title"] != null)
  {
      var title = ViewData["Title"] as string;%>
<title><%=title %></title>
<%}
  else
  { %>
  <title>企业动态</title>
<%} %>
<link href="../../css/newsdetail.css" rel="stylesheet" type="text/css"/>
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
<div class="navitemCurrent"><a href="/News/NewsList">企业动态</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Jobs/Index">招贤纳士</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Home/Contact">联系我们</a></div>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!--左侧导航begin -->
<div class="leftnav">
<ul>
<li class="online"><a href="#">在线咨询</a></li>
<li class="caseshow"><a href="#">案例展示</a></li>
<li class="aboutlink"><a href="#">相关链接</a></li>
</ul>
</div>
<!--左侧导航end -->
<!--右侧内容begin -->
<%if (ViewData["Article"] != null)
  {
      var article = ViewData["Article"] as FBS.Service.ActionModels.ArticleDetailsModel; %>
<div class="article">
<div class="article_title"><h3><%=article.Title %></h3></div>
<div class="article_publicDate">发表时间：<%=article.CreationDate.ToShortDateString() %></div>
<%if (!string.IsNullOrEmpty(article.SourceUrl))
  {%>
<div style="text-align:left; margin:5px 0 0 5px;">原文链接：<a href="<%= article.SourceUrl%>" target="_blank"><%=article.SourceUrl%></a></div>
<%} %>
<div class="article_content">
<%=article.Body %>
<%--<p>乔布斯是 IT 界的传奇人物，但他也是一个很注重保护隐私的人，并且对媒体很是不屑。在许多人看来，他是一个不世出的奇才，也曾感受到他善意的一面，但是正如许多有创造力的人物，他也会欺骗、愤怒，甚至冷酷对待他人。要深入了解乔布斯，并非一件容易的事情。Walter Isaacson 即将上市的《乔布斯传》只引起广泛关注，因为这本传记出自乔布斯的本愿。传记作者 Walter Isaacson 曾问过乔布斯，为什么会同意他作传，乔布斯说:“我想要我的孩子们了解我。我不会永远和他们在一起，我想要他们知道为什么，并且理解我所做的事情。”</p>
<p>这是乔布斯留给孩子的遗产，一个真实的自我。因此，这将是一本真诚的传记，它不会回避丑恶，也不会刻意去神化。</p>
<p>要获得乔布斯的信任，并不是一件容易的事情。乔布斯是一个完美主义者，智力超群，极度自信，这也意味着他对人极为挑剔。在最近透露的传记细节中，乔布斯对总统奥巴马的印象也很一般。因此，对乔布斯来说，选择做传的人并非易事。在多年的寻找之后，他选择了 Walter Isaacson。</p>
<p>CNN 网站在今年 4 月的时候，透露了传记作者 Walter Isaacson 的情况。对于了解 Walter 的人来说，为乔布斯作传并不值得太过惊讶，因为他似乎有着一种特殊的才能，能够在任何场合发现最有影响力的人物，然后设法去接近他们。</p>
<p>Walter Isaacson 出生于新奥尔良的一个中产阶级家庭，他的人生几乎一番风顺。他在著名的 Isidore Newman 接受高中教育，随后进入哈佛大学。在哈佛大学，他获得罗兹奖学金（Rhodes Scholarship），因此也得以前往牛津大学。1978 年，Walter Isaacson 加入《时代周刊》的华盛顿分部，报道里根时代的白宫。</p>
<p>他很快获得提升，成为国家板编辑，随后被委任为几乎是整本杂志（包括科学，技术，艺术，法律等）的版块编辑 。在繁忙的工作中，他利用周末时间写出了《基辛格传》。</p>
<p>1996 年，他成为《时代周刊》的责任编辑，杂志社的最高职位。即使是整个出版业都在开支节流的时代，他仍然增加了员工人数，并且策划了许多影响广泛的专题，包括“时代周刊年度最具影响力 100 人物“（Time 100），”世纪人物“（Person of the Century） 等。</p>
<p>《时代周刊》的员工们怀疑一个拥有如此野心的人离开后后会做什么，也许他会参与政治，或许成为国务卿？</p>
<p>Walter 显然对新媒体更感兴趣，90 年代早期的时候，他一手推动了与 AOL 的合并。1994 年他开办了 Pathfinder——”时代华纳“门户网站，但是网站最终以失败收场。随后，Issaccson 在 CNN 董事长的职位上度过了不愉快的两年，然后在 Aspen研究所任总裁和 CEO。</p>
<p>《乔布斯传》是他所写的重要人物传记中的第四本。在此之前，他已经为基辛格、富兰克林和爱因斯坦写过传记。根据西蒙和舒斯特图书出版公司的说法（Simon & Schuster），乔布斯给予了 Walter 从未有过的权限。Walter 可以进入他的家庭，与苹果员工交流。他们还一起游览了乔布斯童年时的住所。</p>
<p>获得乔布斯的信任并非易事，保持这种信任显然更难。80 年代早期，乔布斯曾邀请《时代周刊》在硅谷的记者 Micheal Moritz 记录 Macintosh 的开发过程，但是当 Moritz 在杂志上透露了乔布斯曾拒绝承认自己的女儿 Lisa 的事情之后，合作关系就突然中断了。Micheal 不得不独自完成《小王国》。显然，Walter 是个言语谨慎的人。</p>
<p>对于 Isaacsson 来说，这并不是他第一次获得 IT 界名人的信任，1996 年他以”时代周刊年度人物“为诱惑，使比尔盖茨敞开了心扉。虽然比尔盖茨并未获得年度人物，但是 Issacsson 的故事还是登上了下一周的封面故事。你可以在这里看到。</p>
<p>Walter 如何获得了乔布斯的信任？根据西蒙和舒斯特图书公司主编 Priscilla Painton 的说法，这是 Walter 以自己的方式争取到的。</p>
<p>“那是 Walter 的主意。你了解 Walter 的本事吧 —— 他就是做到了。”</p>--%>
</div>
<div class="btn_backToNewslist"><a href="/News/NewsList">返回列表</a></div>
</div>
<%} %>
<!--右侧内容begin -->
<div class="blank0"></div>

</asp:Content>
