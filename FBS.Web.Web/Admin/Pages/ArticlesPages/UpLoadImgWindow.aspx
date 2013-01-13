<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpLoadImgWindow.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.ArticlesPages.UpLoadImgWindow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../css/right.css" rel="stylesheet" type="text/css" />
    <title>图片上传</title>
</head>

<body>
    <form id="form2" runat="server">
    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td width="17" valign="top" background="../../images/mail_leftbg.gif">
            <img src="../../images/left-top-right.gif" width="17" height="29"/>
		</td>
		<td valign="top" background="../../images/content-bg.gif">
		<table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" class="left_topbg">
			<tr>
				<td height="31">
				<div class="titlebt">
					                    图片上传</div>
				</td>
			</tr>
		</table>
		</td>
		<td width="16" valign="top" background="../../images/mail_rightbg.gif">
		<img src="../../images/nav-right-bg.gif" width="16" height="29" /></td>
	</tr>
	<tr>
		<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
		<td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
  
        <div id="PanelForm">
            <table cellspacing="0" bordercolordark="white" cellpadding="5" align="center" bordercolorlight="gray"
                border="1">
                <tbody>
                    <tr bgcolor="#F3F3F3"  style=" font-size:14px">
                        <td colspan="2">
                            选择你的图片上传
                        </td>
                    </tr>
                    <tr bgcolor="white">
                        <td align="right" valign="top" width="80" style=" font-size:14px">
                            选择：
                        </td>
                        <td>
                            <input name="uploadedFile" type="file" id="uploadedFile" accept="text/*" size="60" />
                        </td>
                    </tr>
                    <tr bgcolor="white">
                        <td align="center" colspan="2">
                            <input type="submit" name="btnUpload" value=" 上 传 " id="btnUpload" style="height: 25px;
                                width: 100px;" />
                        </td>
                    </tr>
                </tbody>
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

