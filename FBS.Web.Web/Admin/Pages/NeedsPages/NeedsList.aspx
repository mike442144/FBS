<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NeedsList.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.NeedsPages.NeedsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>团购需求列表</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
     <style type="text/css">
        #content
        {
            width: 79%;
        }
        .style1
        {
        	font-size:14px;
            width: 105px;
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
	color:#707070;
 text-decoration: none;
}
a:visited
{
	color:#707070;
 text-decoration: none;
}
a:hover
{
	color:#E9967A;
 text-decoration: underline;
}
a:active
{

 text-decoration: none;
}
.row-style
{
	text-indent:10px;
	color:#707070;
	line-height:20px;
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
					                    需求列表</div>
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
  
    <asp:GridView ID="NeedsGridView" Width="407px" runat="server"  RowStyle-CssClass="row-style"
            DataKeyNames="NeedsID"  Font-Size="12px"
            OnRowDeleting="Row_Deleted" AutoGenerateColumns="False" 
            OnRowDataBound="gvBaidu_RowDataBound" 
            onpageindexchanged="DemandGridView_PageIndexChanged" AllowPaging="True" 
            onpageindexchanging="NeedsGridView_PageIndexChanging" PageSize="1" >
   
       <HeaderStyle   Font-Bold= "True "   HorizontalAlign= "Center "   ForeColor="Black"  BackColor= "#3198CA "   Font-Size="Small"> </HeaderStyle>
   
    
    <Columns>
    <asp:BoundField DataField="NeedsID" Visible=false HeaderText="新闻编号" ReadOnly="true" />
    <asp:HyperLinkField DataTextField="NeedsContent" DataNavigateUrlFormatString="NeedsDetailsView.aspx?id={0}"  DataNavigateUrlFields="NeedsID"   HeaderText="需求内容"/>
    <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Admin/images/del.gif" runat="server" CausesValidation="false" CommandName="Delete" Text="删 除"  Width="15px" Height="15px" BackColor="#FFFFFF" BorderStyle="None" ForeColor="#000000"  OnClientClick="return confirm('您确认删除该记录吗?');" />
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
</body>
</html>
