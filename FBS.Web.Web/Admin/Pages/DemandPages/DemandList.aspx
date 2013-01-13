<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemandList.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.DemandPages.DemandList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>团购列表</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
       <style type="text/css">
        #content
        {
            width: 79%;
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
        #DeleteBtn
        {
        	background-image:(../images/del.jpg)
        	}
        	
        	a:link
{
	color: #000000;
 text-decoration: none;
}
a:visited
{
 text-decoration: none;
}
a:hover
{
 text-decoration: none;
}
a:active
{

 text-decoration: none;
}
.list1
{
 padding-top:10px;
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
					                    团购列表</div>
				</td>
			</tr>
		</table>
		</td>
		<td width="16" valign="top" background="images/mail_rightbg.gif">
		<img src="../../images/nav-right-bg.gif" width="16" height="29" /></td>
	</tr>
	<tr>
<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
<td style="right"> &nbsp;</td></tr><br />
	<tr>
		<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
		<td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
  
    <asp:GridView ID="DemandGridView" Width="593px" runat="server" 
            DataKeyNames="DemandID"  Font-Size="12px"
            OnRowDeleting="Row_Deleted" AutoGenerateColumns="False" 
            OnRowDataBound="gvBaidu_RowDataBound" 
            onpageindexchanged="DemandGridView_PageIndexChanged" >
    <HeaderStyle   Font-Bold= "True "   HorizontalAlign= "Center "   ForeColor="Black"  BackColor= "#3198CA "   Font-Size="Small"> </HeaderStyle>
   
    
    <Columns>
    <asp:BoundField DataField="DemandID" Visible=false HeaderText="新闻编号" ReadOnly="true" />
    <asp:HyperLinkField DataTextField="CustomerName" DataNavigateUrlFormatString="DemandDetailsView.aspx?id={0}"  DataNavigateUrlFields="DemandID"   HeaderText="团购名"/>
    <asp:BoundField DataField="CustomerPhoneNum" HeaderText="联系号码" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    <asp:BoundField DataField="BusinessmanName" HeaderText="团购商家名称" />
    <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
        <img src="../../images/del.gif" alt="" />
            <asp:Button ID="DeleteBtn" runat="server" CausesValidation="false" CommandName="Delete"  Text="删除"  Width="25px" Height="15px" BackColor="#FFFFFF" BorderStyle="None" ForeColor="#000000"  OnClientClick="return confirm('您确认删除该记录吗?');" />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    
    </Columns>
    
    </asp:GridView>
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
    
    
    
    <div>
    <table>
                   
    
    </div>
    </form>
</body>
</html>


