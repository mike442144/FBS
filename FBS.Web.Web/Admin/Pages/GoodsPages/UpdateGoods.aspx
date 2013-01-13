<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false"  CodeBehind="UpdateGoods.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.GoodsPages.UpdateGoods" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>更新商品</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 212px;
        }
        .style2
        {
            width: 204px;
        }
        .style3
        {
        	font-size:14px;
            width: 115px;
        }
    </style>
</head>
<script type="text/javascript" src="../../../ckeditor/ckeditor.js"></script>

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
					                    更新商品</div>
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
  
     <table style="width: 806px">
     <tr>
     <td class="style3">商品名称：</td>
     <td class="style1">
         <asp:TextBox ID="TB_GoodsName" runat="server" class="input"></asp:TextBox>
                 </td>
     </tr>
     <tr>
     <td class="style3">
        商品现价：</td>
     <td class="style1">
         <asp:TextBox ID="TB_NowPrice" runat="server" class="input"></asp:TextBox>
                 </td>
     </tr>
     <tr>
     <td class="style3">
        商品原价：</td>
     <td class="style1">
         <asp:TextBox ID="TB_OldPrice" runat="server" class="input"></asp:TextBox>
                 </td>
     </tr>
     
    
         <asp:Label ID="Label1" runat="server"  Visible="false" Text=""></asp:Label>
     <tr>
     <td class="style3">
         商品开始时间：</td>
     <td class="style1">
         <asp:Calendar ID="C_BeginTime" runat="server"></asp:Calendar>
                 </td>
     </tr>
     <tr>
     <td class="style3">
        商品结束时间：</td>
     <td class="style1">
         <asp:Calendar ID="C_EndTime" runat="server"></asp:Calendar>
                 </td>
     </tr>
     
      <tr>
     <td class="style3">
         商品图片：</td>
     <td class="style1">
         <asp:FileUpload ID="FU_Pic" runat="server" class="input" Width="219px"/>
                 </td>
     </tr>
     <tr>
     <td class="style3">
        商品详细介绍:</td>
     <td class="style1">&nbsp;</td>
     </tr>
      <tr>
     <td class="style2" colspan="2">
      <textarea class="ckeditor" id="editor1" name="ThreadContent" rows="10" runat="server" cols="70"></textarea>
        </td>
     </tr>
     <tr>
     <td class="style3">
         <asp:Button ID="BT_AddGoods" runat="server" Text="修改商品"  class="button"
             onclick="BT_AddGoods_Click" />
         </td>
     <td class="style1"></td>
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

