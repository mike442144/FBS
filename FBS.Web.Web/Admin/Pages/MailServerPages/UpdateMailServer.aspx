<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateMailServer.aspx.cs" Inherits="FBS.Web.News.Admin.UpdateMailServer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>更新邮件服务</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
     <style type="text/css">
        .style1
        {
            width: 299px;
        }
        .style2
        {
            width: 173px;
            font-size:14px;
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
					                    更新邮件服务</div>
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
    <td class="style2">服务器名称：</td>
    <td class="style1">
        <asp:TextBox ID="TB_ServerName" ReadOnly=true runat="server" class="input"></asp:TextBox>
                    <asp:Label ID="Label1" Visible=false runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
     <tr>
    <td class="style2">优先级：</td>
    <td class="style1">
        <asp:TextBox ID="TB_Priority" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
     <tr>
    <td class="style2">邮件服务器地址：</td>
    <td class="style1">
        <asp:TextBox ID="TB_ServerAddr" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
     <tr>
    <td class="style2">邮件服务器端口：</td>
    <td class="style1">
        <asp:TextBox ID="TB_Port" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
     <tr>
    <td class="style2">用户名：</td>
    <td class="style1">
        <asp:TextBox ID="TB_UserName" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
     <tr>
    <td class="style2">账户密码：</td>
    <td class="style1">
        <asp:TextBox ID="TB_Password" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
     <tr>
    <td class="style2">邮件地址：</td>
    <td class="style1">
        <asp:TextBox ID="TB_MailAddr" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
     <tr>
    <td class="style2">发信人姓名：</td>
    <td class="style1">
        <asp:TextBox ID="TB_SendUserName" runat="server" class="input"></asp:TextBox>
                    </td>
    </tr>
     <tr>
    <td class="style2">是否支持SSL：</td>
    <td class="style1">
        <asp:DropDownList ID="DropD_SSLSupport" runat="server">
        <asp:ListItem Text="是" Value="true" ></asp:ListItem>
        <asp:ListItem Text="否" Value="false"></asp:ListItem>
        </asp:DropDownList>
                    </td>
    </tr>
     <tr>
    <td class="style2">状态：</td>
    <td class="style1">
        <asp:CheckBox ID="CHB_ON" Text="开启" runat="server" AutoPostBack="True"  Font-Size="12px"
            oncheckedchanged="CHB_ON_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="CHB_OFF" Text="关闭" runat="server"  AutoPostBack="True"   Font-Size="12px"
            oncheckedchanged="CHB_OFF_CheckedChanged" />
                    </td>
     <tr>
    <td class="style2">
        <asp:Button ID="BT_AddServer" runat="server" Text="修改服务器设置"  class="button"
            onclick="BT_AddServer_Click" /></td>
    <td class="style1"></td>
    </tr>
               
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

