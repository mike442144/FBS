<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvertisementListOLd.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.AdvertisementPages.AdvertisementList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>广告列表</title>
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
					                    广告列表</div>
				</td>
			</tr>
		</table>
		</td>
		<td width="16" valign="top" background="images/mail_rightbg.gif">
		<img src="../../images/nav-right-bg.gif" width="16" height="29" /></td>
		
	</tr>
<tr>
<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
<td style="right"> <a href="AddAdvertisement.aspx"><br /> <img src="../../images/add.gif" border="0px"/> 新增</a></td></tr><br />
	<tr>
		<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
		
		<td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
          
<asp:GridView  ID="ArticlesGridView" Width="688px" AllowPaging=True  runat="server"  Font-Size="12px"  RowStyle-CssClass="row-style"
            DataKeyNames="AdvertisementID" OnRowDeleting="Row_Deleted" 
            AutoGenerateColumns="False" OnRowDataBound="gvBaidu_RowDataBound" onpageindexchanging="ArticlesGridView_PageIndexChanging" 
           >
   
   <HeaderStyle   Font-Bold= "True "   HorizontalAlign= "Center "   ForeColor="White"  BackColor= "#3198CA " Height="32px"   Font-Size="Small"> </HeaderStyle>
   
    
    <Columns>
    <asp:BoundField DataField="AdvertisementID" Visible="false"  ReadOnly="true" />
    <asp:BoundField DataField="AdvertisementURL"   HeaderText="广告链接"  >
    
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    <%--<asp:HyperLinkField DataTextField="CategoryName" DataNavigateUrlFormatString="UpdateArticlesCategory.aspx?id={0}"  DataNavigateUrlFields="CategoryID"   HeaderText="广告链接"/>--%>
    <asp:BoundField DataField="AdvertisementBeginTime"   HeaderText="开始时间"  >
    
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    
     <asp:BoundField DataField="AdvertisementEndTime"   HeaderText="结束时间"  >
    
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
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
