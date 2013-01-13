<%@ Page Language="C#" CodeBehind="VideoList.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.Video.VideoList" %>
<%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
    
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
    <form id="form1" runat="server">
    <div>
    <asp:GridView  HeaderStyle-BackColor="#D3EAEF" ID="VideosGridView" Width="731px"  RowStyle-CssClass="row-style"
            runat="server" DataKeyNames="VideoID" 
            AutoGenerateColumns="False" OnRowDataBound="gvBaidu_RowDataBound" 
            onrowdeleted="VideosGridView_RowDeleted" 
            onrowdeleting="VideosGridView_RowDeleting" Height="169px" >
            <HeaderStyle   Font-Bold= "True "   HorizontalAlign= "Center "   ForeColor="Black"  BackColor= "#3198CA "   Font-Size="Small"> </HeaderStyle>
   
    <%--<HeaderStyle BackColor="#D3EAEF" />
   
    <RowStyle Font-Size="13px" />--%>
    
    <RowStyle CssClass="row-style"></RowStyle>
    
    <Columns>
    <asp:BoundField DataField="VideoID" Visible="false" HeaderText="新闻编号" ReadOnly="true" />
    <asp:BoundField DataField="Subject" HeaderText="视频标题"  HeaderStyle-Width="183px">
    <HeaderStyle Width="200px"></HeaderStyle>
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    <asp:BoundField DataField="ShareTime" HeaderText="分享时间"  HeaderStyle-Width="125px">
    
    
<HeaderStyle Width="125px"></HeaderStyle>
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    
    
    
    <asp:BoundField DataField="PlayUrl" HeaderText="视频链接"  HeaderStyle-Width="125px">
    
    
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
    
     </tr>
    <tr>
     <webdiyer:AspNetPager runat="server" ID="anp"  PageSize="3" Font-Size="12px"
                    FirstPageText="首页" 
                    LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" 
                    ShowCustomInfoSection="Never" TextAfterPageIndexBox="页" 
                    onpagechanged="getPager"     ShowPageIndexBox="Never" Width="500px">
                    </webdiyer:AspNetPager>
     </tr>
    </div>
    </form>
</body>
</html>
