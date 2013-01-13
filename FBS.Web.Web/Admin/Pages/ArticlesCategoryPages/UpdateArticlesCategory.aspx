<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateArticlesCategory.aspx.cs"
    Inherits="FBS.Web.News.Backstage.UpdateArticlesCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改新闻分类</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .input
        {
            width: 180px;
            height: 23px;
            line-height: 23px;
            font-size: 14px;
            font-weight: bold;
            color: #9D9E9D;
            padding: 0px 10px;
            border: 1px solid #90ACC3;
        }
        #content
        {
            text-align: center;
            font-size: 14px;
            margin-right: 15px;
        }
        .style1
        {
            width: 540px;
        }
    </style>
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
					                    修改新闻分类</div>
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
        <table id="content" style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
            <tr>
                <td>
                    选择父分类：
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ArticleCategory" runat="server" class="input">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    新分类名称：
                </td>
                <td class="style1">
                    <asp:TextBox ID="TBnewCategory" runat="server" class="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    新分类描述：
                </td>
                <td class="style1">
                    <asp:TextBox ID="TBdescr" runat="server" class="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    新分类优先级：
                </td>
                <td class="style1">
                    <asp:TextBox ID="TByouxianji" runat="server" class="input"> </asp:TextBox>
                </td>
            </tr>
            <tr>
                <asp:Label ID="Label1" Visible="false" runat="server" Text=""></asp:Label>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="修改分类" OnClick="Button2_Click" class="button"/>
                </td>
            </tr>
        </table>
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



