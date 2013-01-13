<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeBehind="AddGoods.aspx.cs" Inherits="Aviator.Web.Admin.Pages.GoodsPages.AddGoods" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加商品信息</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 708px;
        }
        .style2
        {
        }
        .style3
        {
            width: 158px;
            font-size:14px;
        }
        .style5
        {
            width: 708px;
            height: 23px;
        }
    </style>
     <script type="text/javascript" src="../../../ckeditor/ckeditor.js"></script>
</head>


<body>
    <form id="form1" runat="server">
    
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
					                    商品信息添加</div>
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
         <asp:FileUpload ID="FU_Pic" runat="server" class="input" Width="217px"/>
                 </td>
     </tr>
     <tr>
     <td class="style3">
        商品详细介绍:</td>
     <td class="style5"></td>
     </tr>
      <tr>
     <td class="style2" colspan="2">
      <textarea class="ckeditor" id="editor1" name="ThreadContent" rows="10" runat="server" cols="70"></textarea>
        </td>
     </tr>
     <tr>
     <td class="style3">
         <asp:Button ID="BT_AddGoods" runat="server" Text="添加商品" 
             onclick="BT_AddGoods_Click" class="button" />
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

