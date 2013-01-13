﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateForum.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.tiebaPages.UpdateForum" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改贴吧</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 95px;
            font-size:14px
        }
        .style2
        {
            height: 30px;
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
					                    修改论坛信息</div>
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
    <table style="width: 812px">
    <tr>
    <td class="style1">贴吧名称：</td>
    <td>
        <asp:TextBox ID="TB_Name" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style1">贴吧描述：</td>
    <td>
        <asp:TextBox ID="TB_Desription" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
    <tr>
    <td class="style2" colspan="2">
        <asp:Button ID="BT_AddForum" runat="server" Text="修改贴吧"  class="button"
            onclick="BT_UpdateForum_Click" />
        </td>
    </tr>
    </table>
    </div>
    <asp:Label Visible="false" runat="server" ID="myid"></asp:Label>
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
