<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuggestList.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.Suggest.SuggestList" %>
<%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>建议留言列表</title>
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
    <link href="../../../css/listfenpage.css" rel="stylesheet" type="text/css" />
    
</head>


<body>
    <form id="form2" runat="server">
    
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
					                    建议列表</div>
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
  
<h1>建议处理</h1>
  <%FBS.Service.SuggestService service = new FBS.Service.SuggestService();
      foreach (FBS.Service.ActionModels.SuggestModel model in service.GetAllSuggest((this._currentPageIndex - 1) * 8, 8))
      {%>
      <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
      <tr>
      <td>来自<%=model.UserName%>的消息:<%=model.Body%>[<font color="red"><%=model.Type %></font>]</td>
      </tr>
      <%if (!string.IsNullOrEmpty(model.Reply))
        {%>
        <tr><td>回复：<%=model.Reply %></td></tr>
      <%} %>
      <tr><td><div style="width:100px;float:left"><a href="SuggestList.aspx?id=<%=model.SuggestID %>" class="button">删除消息</a></div><a href="ReplySuggest.aspx?id=<%=model.SuggestID %>" class="button">回复消息</a></td></tr>
      </table>
         <%} %>
<div class="hcy-pagelan">

      <%
                        if (_currentPageIndex != 1)
                        {
                    %>
                     <div class="hcy-next">
                                <a href="SuggestList.aspx?pageIndex=1">第一页</a></div>
        <div class="hcy-next"><a href="SuggestList.aspx?pageIndex=<%=_currentPageIndex-1 %>">上一页</a></div>
          <% 
                        }
                        int pageNum = _suggestCount % 8 == 0 ? _suggestCount / 8 : _suggestCount / 8 + 1;
                if (pageNum > 1)
                {
                    for (int pageIndex = 1; pageIndex <= pageNum; pageIndex++)
                    {
                        string pageClass = "hcy-pagebox";
                        if (pageIndex == _currentPageIndex)
                        {
                            pageClass = "hcy-pagecurrent";
                        }
                    %>
        <div class="<%=pageClass%>"><a  href="SuggestList.aspx?pageIndex=<%=pageIndex %>">
                            <%=pageIndex %></a></div>
               <%
                        }


                    if (_currentPageIndex != pageNum)
                    {
                    %>
                    
                    <div class="hcy-next">
                        <a  href="SuggestList.aspx?pageIndex=<%=_currentPageIndex+1 %>">
                            下一页</a>
                    </div>
                    <div class="hcy-last">
                        <a  href="SuggestList.aspx?pageIndex=<%=pageNum %>">
                            最后一页</a>
                    </div>
                    <%
                        }
                }
                    %>
      
     
        
  </div>
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





