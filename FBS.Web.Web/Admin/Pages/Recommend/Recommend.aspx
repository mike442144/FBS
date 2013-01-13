<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recommend.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.Recommend.Recommend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>推荐机构</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style3
        {
            height: 71px;
            font-size:14px
        }
        .style4
        {
            height: 58px;
            width: 490px;
        }
        .style5
        {
            height: 72px;
            width: 490px;
            font-size:14px
        }
        a
        {
        	text-decoration:none;
        	}
    </style>
    <link href="../../css/rightList.css" rel="stylesheet" type="text/css" />
</head>
<body>
     <form id="form2" action="Recommend.aspx" method="post">
    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td width="17" valign="top" background="images/mail_leftbg.gif">
            <img src="../../images/left-top-right.gif" width="17" height="29"/>
		</td>
		<td valign="top" background="images/content-bg.gif">
		<table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" class="left_topbg">
			<tr>
				<td height="31">
				<div class="titlebt">
					                    推荐机构</div>
				</td>
			</tr>
		</table>
		</td>
		<td width="16" valign="top" background="images/mail_rightbg.gif">
		<img src="../../images/nav-right-bg.gif" width="16" height="29" /></td>
	</tr>
	<tr>
		<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
		<td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
  
<h1>推荐机构</h1>
<%
    //Service.BlogService service = new Service.BlogService();
    if (uslist.Count != 0)
    {
        foreach (Service.ActionModels.UserStateModel model in uslist)
        {
     %>
     <table style="width: 780px; margin:20px auto; border:solid 1px red;padding:15px;">
     <tr><td><h4>查找结果</h4></td></tr>
     <tr><td>
     博主机构名称:[<%=model.UserName%>] </td></tr>
     <%if (model.Display == false)
       {%>
      <tr><td> <a href="Recommend.aspx?op=1&userid=<%=model.UserID %>">首页推荐</a></td></tr>
       <%}
       else
       { %>
      <tr><td> <a href="Recommend.aspx?op=0&userid=<%=model.UserID %>">取消首页推荐</a></td></tr>
     <%} %>
      </table>
      <%}
    }
    else
    {%>
    <table style="width: 780px; margin:20px auto; border:solid 1px red;padding:15px;">
    <tr><td><h4>对不起，您查找的用户可能不是单位注册，或者用户名博客名输入错误，请确认后再查找</h4></td></tr>
    </table>
    <%} %>
      
     <%--  <%
    Service.BlogService service = new Service.BlogService();
    foreach (Service.ActionModels.UserStateModel model in service.GetAllRecommendCompany())
    {
     %>
     <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
     <tr><td>
     博主机构名称:[<%=model.UserName %>] </td></tr>
     <%if (model.Display == false)
       {%>
      <tr><td> <a href="Recommend.aspx?op=1&userid=<%=model.UserID %>">首页推荐</a></td></tr>
       <%}
       else
       { %>
      <tr><td> <a href="Recommend.aspx?op=0&userid=<%=model.UserID %>">取消首页推荐</a></td></tr>
     <%} %>
      </table>
      <%} %>--%>
<%--<div id="pagelan">
      <%
                        if (_currentPageIndex != 1)
                        {
                    %>
        <div class="next"><a href="Recommend.aspx?pageIndex=<%=_currentPageIndex-1 %>">上一页</a></div>
          <% 
                        }
                        int pageNum = _companyCount % 8 == 0 ? _companyCount / 8 : _companyCount / 8 + 1;
                if (pageNum > 1)
                {
                    for (int pageIndex = 1; pageIndex <= pageNum; pageIndex++)
                    {
                        string pageClass = "page";
                        if (pageIndex == _currentPageIndex)
                        {
                            pageClass = "currentPage";
                        }
                    %>
        <div class="pagebox"><a class="<%=pageClass%>" href="Recommend.aspx?pageIndex=<%=pageIndex %>">
                            <%=pageIndex %></a></div>
               <%
                        }


                    if (_currentPageIndex != pageNum)
                    {
                    %>
                    
                    <div>
                        <a class="next" href="Recommend.aspx?pageIndex=<%=_currentPageIndex+1 %>">
                            下一页</a>
                    </div>
                    <div>
                        <a class="last" href="Recommend.aspx?pageIndex=<%=pageNum %>">
                            最后一页</a>
                    </div>
                    <%
                        }
                }
                    %>
      
     
        
  </div>--%>
                </td>
		<td background="../images/mail_rightbg.gif">&nbsp;</td>
	</tr>
	<tr>
		<td valign="bottom" background="../images/mail_leftbg.gif">
            <img src="../../images/buttom_left2.gif" width="17" height="17" />
	</td>
		
		
		<td valign="bottom" background="../images/mail_rightbg.gif">
		<img src="../../images/buttom_bgs.gif" style="width: 100%; height: 16px" /></td>
	</tr>
	</table>
    
    
    
    </form>
    
</body>
</html>
