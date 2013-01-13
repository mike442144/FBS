<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemandDetailsView.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.DemandPages.DemandDetailsView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>团购细节</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 484px;
        }
        .style2
        {
            width: 318px;
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
					                    客户管理</div>
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
  
<html xmlns="http://www.w3.org/1999/xhtml" >


<body>
    
    

</body>
</html>




<body>
    <div>
        <br />
    <table>
    <tr>
    <td class="style2">客户称呼：</td>
    <td class="style1">
        <asp:Label ID="LB_Name" runat="server" Text="Label" class=""></asp:Label>
                    </td>
    </tr>
      <tr>
    <td class="style2">客户电话：</td>
    <td class="style1">
        <asp:Label ID="LB_Telnum" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
        <tr>
    <td class="style2">客户其他联系方式：</td>
    <td class="style1">
        <asp:Label ID="LB_Othernum" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
        <tr>
    <td class="style2">客户所在城市：</td>
    <td class="style1">
        <asp:Label ID="LB_City" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
        <tr>
    <td class="style2">客户要求的团购商家名称：</td>
    <td class="style1">
        <asp:Label ID="LB_BussissName" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
    <tr>
    <td class="style2">客户要求的团购类型：</td>
    <td class="style1">
        <asp:Label ID="LB_Type" runat="server" Text="Label"></asp:Label>
                    </td>
    </tr>
    <tr>
    <td class="style2" colspan=2>客户要求的团购内容：</td>
   
    </tr>
     <tr>
    <td class="style2" colspan=2>
        <asp:Label ID="LB_Content" runat="server" Text="Label"></asp:Label>
         </td>
    
    </tr>
    </table>
    </div>
</body>
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
    
 