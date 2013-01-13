<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="FBS.Web.News.Admin.SendEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发送邮件</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    
    
    <style type="text/css">
        .style1
        {
            width: 480px;
        }
        .style2
        {
            width: 223px;
        }
        #editor1
        {
            width: 699px;
        }
        .style3
        {
        	font-size:14px;
            width: 114px;
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
    <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
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
					                    邮件发送</div>
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
  
    <table>
    <tr>
    <td class="style3">
        收件人地址：</td>
    <td class="style1">
        <asp:TextBox ID="Receive" runat="server" Width="181px" class="input"></asp:TextBox>
    </td>
    </tr>
     <tr>
    <td class="style3">
        邮件标题：</td>
    <td class="style1">
        <asp:TextBox ID="MailTitle" runat="server" class="input"></asp:TextBox>
    </td>
    </tr>
     <tr>
    <td class="style3">
        邮件内容：</td>
    <td class="style1">
    </td>
    </tr>
     <tr>
    <td class="style2" colspan=2>
        <textarea class="ckeditor" id="editor1" name="ThreadContent" rows="10" 
            cols="20"></textarea></td>
    </tr>
    <tr>
    <td class="style3">
        <asp:Button ID="BT_SendEmail" runat="server" Text="发送邮件" class="button"
            onclick="BT_SendEmail_Click" />
        </td>
    <td></td>
    </tr>
    </table>
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


