﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateBlogCategory.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.BlogCategoryPages.UpdateBlogCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改博文分类</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        body
        {
        	font-size:14px
        	}
      .input
        {
            width: 180px;
            height: 23px;
            line-height: 23px;
            font-size: 12px;
            font-weight: bold;
            color: #9D9E9D;
            padding: 0px 10px;
            border: 1px solid #90ACC3;
        }
        .style1
        {
            height: 29px;
        }
    </style>
    
    <script type="text/javascript">
    function Check()
    {
    var a=document.getElementById("categoryname").value;
    var b=document.getElementById("decription").value;
    var c=document.getElementById("priority").value;
    if(a=="")
    {
    alert('请填写分类名称！');
    }
    else
    {
    if(b=="")
    {
    alert('请填写分类描述！');
    }
    else
    {
    if(c=="")
    {
     alert('请填写分类优先级！');
    }
    else
    {
    if(isNaN(c))
    {
    alert('优先级应该是数字！');
    }
    else
    {
    form2.submit();
    }
    }
    }
    }
    }
    </script>
</head>




<body>
    <form id="form2" method="post" action="UpdateBlogCategory.aspx" runat="server">
    
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
					                    修改博文分类</div>
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
  
    <div>
    <div>
        <br />
    <table>
    <%--<tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    </tr>--%>
     <tr>
    <td>新分类名称：
                    </td>
    <td>
        <input type="text" name="categoryname" value="<%=_categoryname %>" id="categoryname" />
        
    </tr>
    <tr>
    <td>新分类描述：
                    </td>
    <td>
        <input type="text" name="decription" value="<%= _decription%>" id="decription" />
                    </td>
    </tr>
    <tr>
    <td class="style1">新分类优先级：
                    </td>
    <td class="style1">
        <input type="text" name="priority" id="priority" value="<%=_priority %>" />
                    </td>
    </tr><input type="hidden" value="<%=myid %>" name="categoryid" id="categoryid" />
    <tr>
    <td>
    <input type="button" onclick="Check()" value="更新博客分类" class="button" name="m_button" id="m_button" />
   <%-- <asp:Button ID="Button2" runat="server" Text="添加分类" onclick="Button2_Click" class="button"/>--%>
    </td>
    </tr>
     </table>
    </div>
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






<body>
        
</body>
</html>