<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindRecommend.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.Recommend.FindRecommend" %>

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
  
<h1>查找推荐机构</h1>
<form action="Recommend.aspx">
<input  type="text" name="username"/>
<input type="submit" value="查找" class="button"/>
</form>
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
