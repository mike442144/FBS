<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
<title>企业动态</title>
<link href="../../css/companystate.css" rel="stylesheet" type="text/css"/>
<link href="../../css/fenpageblack.css" rel="Stylesheet" type="text/css" />
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

    <!--展示 begin -->
<table class="bigshowbox" cellpadding="0" cellspacing="0">
  <tr>
    <td class="top_left"></td>
    <td class="top_middle"></td>
    <td class="top_right"></td>
  </tr>
  <tr>
    <td class="middle_left"></td>
    <td class="kouhao">
      <div class="kouhaoleft"></div>
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td colspan="2" rowspan="2">&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="2" rowspan="2"><img src="../../images/k3.png" width="104px" height="82px"/></td>
        <td colspan="2" rowspan="2">&nbsp;</td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        <td style="vertical-align:bottom;">团队</td>
        </tr>
      <tr>
        <td colspan="2" rowspan="2"></td>
        <td style="vertical-align:top">创意</td>
        <td style="vertical-align:top;font-size:12px;">Team</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="2" rowspan="2"><img src="../../images/k4.png" width="104px" height="82px"/></td>
        </tr>
      <tr>
        <td style="vertical-align:top; font-size:12px;">Idea</td>
        <td>&nbsp;</td>
        <td style="vertical-align:bottom;">行动</td>
        <td>&nbsp;</td>
        </tr>
      <tr>
        <td colspan="2" rowspan="2">&nbsp;</td>
        <td colspan="2" rowspan="2"><img src="../../images/k2.png" width="104px" height="82px"/></td>
        <td style="vertical-align:top;font-size:12px;">Action</td>
        <td style="vertical-align:bottom;">效率</td>
        <td colspan="2" rowspan="2">&nbsp;</td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        <td style="vertical-align:top;font-size:12px;">Efficiency</td>
        </tr>
      <tr>
        <td colspan="2">&nbsp;</td>
        <td colspan="2">&nbsp;</td>
        <td colspan="2">&nbsp;</td>
        <td colspan="2">&nbsp;</td>
        </tr>
    </table>
    </td>
    <td class="middle_right"></td>
  </tr>
  <tr>
    <td class="bottom_left"></td>
    <td class="bottom_middle"></td>
    <td class="bottom_right"></td>
  </tr>
</table>
<!--展示 end -->
<div class="blank0"></div>

<div class="newsbox">
  <div class="sitemap">
  <a href="/Home/Index">首页</a> >> <a href="/News/NewsList">企业新闻</a>
  </div>
  <div class="newslistbox">
  
  <%if (ViewData["ArticleList"] != null)
    {
        var articleList = ViewData["ArticleList"] as IList<FBS.Service.ActionModels.ArticleDspModel>;
        int pagecount = 0;
        //string categoryid = ViewData["CategoryID"].ToString();
        pagecount = Convert.ToInt32(ViewData["ArticleCount"]);
        int index = Convert.ToInt32(ViewData["PageIndex"]);
        if (articleList.Count != 0)
        {
            foreach (FBS.Service.ActionModels.ArticleDspModel model in articleList)
            {
               %>
    <div class="newslist">
      <ul>
        <li class="newstitle"><a href="/News/News/<%=model.ArticleID.ToString() %>">
       <%-- 百种平板电脑亮相CES 联想首次展出乐PAD百种平板电脑亮相CES 联想首次展出乐PAD--%>
       <%=model.Title%>
        </a></li>
        <li class="newsdate"><%--[10-07-26]--%>
        [<%=model.CreatedOn.ToShortDateString()%>]
        </li>
      </ul>
    </div>
    
    <%} %>
       <div class="pagelan">
                        <!-- 总页数大于1时有第一页 -->
                            <%if (index > 1)
                              { %>
                              <div class="pagebox"> <a href="/News/NewsList/<%=index-1 %>" class="previousPage">
                                    &#8249;</a></div>
                          
                            <%} %>
                            
                            <%
      //中间的分页
      if (index > 5 && index + 5 <= pagecount)
      {
                                  %>
                                 <div class="pagebox"> <a href="/News/NewsList/1">1</a></div>
                            <div style="width:21px; float:left">...</div>
                                 <%
      for (int i = index - 3; i <= index + 3; i++)
      {
          string pageClass = "page";
          if (i == index)
          {
              pageClass = "currentPage";
          }
                            %>
                            <div class="pagebox">
                                <a href="/News/NewsList/<%=i %>" class="<%=pageClass%>">
                                    <%=i%></a></div>                      
                            <%
      }
                                   %>
                                  <div style="width:21px; float:left">...</div>
                            <div class="pagebox"><a href="/News/NewsList/<%=pagecount%>" class="page"><%=pagecount%></a></div>
                                  <%
      }
      //末尾的分页
      if (index > 5 && index + 5 > pagecount)
      {
          if (pagecount > 7)
          {
                                  %>
                                 <div class="pagebox"> <a href="/News/NewsList/1" class="page">1</a></div>
                            <div style="width:21px; float:left">...</div>
                                 <%
      }
          for (int i = pagecount - 5; i <= pagecount; i++)
          {
              string pageClass = "page";
              if (i == index)
              {
                  pageClass = "currentPage";
              }
                            %>
                            <div class="pagebox">
                                <a href="/News/NewsList/<%=i %>" class="<%=pageClass%>">
                                    <%=i%></a></div>                      
                            <%
      }

      }
      //前面的分页
      if (index <= 5 && pagecount > 6)
      {

          for (int i = 1; i <= 6; i++)
          {
              string pageClass = "page";
              if (i == index)
              {
                  pageClass = "currentPage";
              }
                            %>
                            <div class="pagebox">
                                <a href="/News/NewsList/<%=i %>" class="<%=pageClass%>">
                                    <%=i%></a></div>                      
                            <%
      }
                                  %>
                                  <div class="points">&#8230;</div>
                            <div class="pagebox"><a href="/News/NewsList/<%=pagecount%>" class="page"><%=pagecount%></a></div>
                                  <%
                                  
      }
      if (index <= 5 && pagecount <= 6 && pagecount != 1)
      {

          for (int i = 1; i <= pagecount; i++)
          {
              string pageClass = "page";
              if (i == index)
              {
                  pageClass = "currentPage";
              }
                            %>
                            <div class="pagebox">
                                <a href="/News/NewsList/<%=i %>" class="<%=pageClass%>">
                                    <%=i%></a></div>                      
                            <%
      }


      }
      if (pagecount > 1 && index != pagecount)
      {
                            %>
                           
                            <div class="pagebox"><a href="/News/NewsList/<%=index+1 %>" class="nextPage">&#8250;</a></div>
                            
                            <%} %>
 
                        </div>
    <%}
        else
        {
            %>暂无企业新闻...<%}
    }%>
  </div>
  <div class="blank0"></div>
</div>

</asp:Content>