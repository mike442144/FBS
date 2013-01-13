<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticlesList.aspx.cs" Inherits="FBS.Web.News.Backstage.ArticlesList" %>
<%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新闻列表</title>
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
            color: black;
            padding: 0px 0px;
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
					                    新闻列表</div>
				</td>
			</tr>
		</table>
		</td>
		<td width="16" valign="top" background="images/mail_rightbg.gif">
		<img src="../../images/nav-right-bg.gif" width="16" height="29" /></td>
	</tr>
	<tr>
<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
<td style="right"> <a href="AddArticles.aspx" font-size:14px; "><br /> <img src="../../images/add.gif" border="0px"/> 新增</a></td></tr><br />
	<tr>
		<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
		<td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
  
    <table >
    <tr>
    <td class="style1">选择分类：</td>
    <td> 
        <asp:DropDownList ID="ArticlesCategoryDP" runat="server" 
            onselectedindexchanged="ArticlesCategory_SelectedIndexChanged" 
            AutoPostBack="True" style="height: 22px" class="input">
        </asp:DropDownList>
    <tr>
    <td colspan=2 style="font-size: 12px; text-decoration: none;">
<asp:GridView  HeaderStyle-BackColor="#D3EAEF" ID="ArticlesGridView" Width="644px"  RowStyle-CssClass="row-style"
            runat="server" DataKeyNames="ArticleID" OnRowDeleting="Row_Deleted" 
            AutoGenerateColumns="False" OnRowDataBound="gvBaidu_RowDataBound" 
             >
            <HeaderStyle   Font-Bold= "True "   HorizontalAlign= "Center "   ForeColor="White"  BackColor= "#3198CA "   Font-Size="Small"> </HeaderStyle>
   
    <%--<HeaderStyle BackColor="#D3EAEF" />
   
    <RowStyle Font-Size="13px" />--%>
    
    <RowStyle CssClass="row-style"></RowStyle>
    
    <Columns>
    <asp:BoundField DataField="ArticleID" Visible="false" HeaderText="新闻编号" ReadOnly="true" />
    <asp:BoundField DataField="BriefTitle" HeaderText="首页新闻标题"  HeaderStyle-Width="183px">
    <HeaderStyle Width="183px"></HeaderStyle>
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    <asp:HyperLinkField DataTextField="Title" 
            DataNavigateUrlFormatString="UpdateArticle.aspx?id={0}"  
            DataNavigateUrlFields="ArticleID"   HeaderText="新闻标题" HeaderStyle-Width="257px">
<HeaderStyle Width="257px"></HeaderStyle>
        </asp:HyperLinkField>
    <asp:BoundField DataField="CreatedOn" HeaderText="操作时间"  HeaderStyle-Width="125px">
    
    
<HeaderStyle Width="125px"></HeaderStyle>
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    
    
    <asp:TemplateField HeaderText="删除" HeaderStyle-Width="78px">
        <ItemTemplate>
          
         
         <asp:ImageButton ID="ImageButton1" ImageUrl="~/Admin/images/del.gif" runat="server" CausesValidation="false" CommandName="Delete" Text="删 除"  Width="15px" Height="15px" BackColor="#FFFFFF" BorderStyle="None" ForeColor="#000000"  OnClientClick="return confirm('您确认删除该记录吗?');" />
             
        </ItemTemplate>

<HeaderStyle Width="78px"></HeaderStyle>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    
    </Columns>
    
    </asp:GridView>
    </td>
    </tr>
    
     <webdiyer:AspNetPager runat="server" ID="anp"  PageSize="10" Font-Size="12px"
                    FirstPageText="首页" 
                    LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" 
                    ShowCustomInfoSection="Never" TextAfterPageIndexBox="页" 
                    onpagechanged="getPager"     ShowPageIndexBox="Never" Width="500px">
                    </webdiyer:AspNetPager>
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
