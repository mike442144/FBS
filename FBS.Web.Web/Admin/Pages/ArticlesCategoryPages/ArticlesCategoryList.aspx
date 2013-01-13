<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticlesCategoryList.aspx.cs" Inherits="FBS.Web.News.Backstage.ArticlesCategoryList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新闻分类列表</title>
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
					                    新闻分类</div>
				</td>
			</tr>
		</table>
		</td>
		<td width="16" valign="top" background="images/mail_rightbg.gif">
		<img src="../../images/nav-right-bg.gif" width="16" height="29" /></td>
		
	</tr>
<tr>
<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
<td style="right"> <a href="AddArticlesCategory.aspx" font-size:14px; "><br /> <img src="../../images/add.gif" border="0px"/> 新增</a></td></tr><br />
	<tr>
		<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
		
		<td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
          
<asp:GridView  ID="ArticlesGridView" Width="688px" AllowPaging=True  runat="server"  Font-Size="12px"  RowStyle-CssClass="row-style"
            DataKeyNames="CategoryID" OnRowDeleting="Row_Deleted" 
            AutoGenerateColumns="False" OnRowDataBound="gvBaidu_RowDataBound" onpageindexchanging="ArticlesGridView_PageIndexChanging" 
           >
   
   <HeaderStyle   Font-Bold= "True "   HorizontalAlign= "Center "   ForeColor="White"  BackColor= "#3198CA " Height="32px"   Font-Size="Small"> </HeaderStyle>
   
    
    <Columns>
    <asp:BoundField DataField="CategoryID" Visible="false"  ReadOnly="true" />
    <asp:HyperLinkField DataTextField="CategoryName" DataNavigateUrlFormatString="UpdateArticlesCategory.aspx?id={0}"  DataNavigateUrlFields="CategoryID"   HeaderText="新闻分类名称"/>
    <asp:BoundField DataField="Deepth"   HeaderText="分类深度"  >
    
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    
    <asp:TemplateField HeaderText="删除">
        <ItemTemplate>
        
            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Admin/images/del.gif" runat="server" CausesValidation="false" CommandName="Delete" Text="删 除"  Width="15px" Height="15px" BackColor="#FFFFFF" BorderStyle="None" ForeColor="#000000"  OnClientClick="return confirm('删除此分类，分类下其他分类及其下所有文章都将删除，您确认删除此分类吗?');" />
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
