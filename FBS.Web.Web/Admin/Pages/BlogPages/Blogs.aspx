<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blogs.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.BlogPages.Blogs" %>
<%@ Register Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div id="add">
    <a href="Blog.aspx"><br /> <img src="../../images/add.gif" border="0px"/> 新增</a>
</div>

        <div id="blogs">
            <asp:GridView  HeaderStyle-BackColor="#D3EAEF" ID="StoreGridView" Width="644px"  RowStyle-CssClass="row-style"
            runat="server" DataKeyNames="StoryID" OnRowDeleting="Row_Deleted" 
            AutoGenerateColumns="False" OnRowDataBound="gvBaidu_RowDataBound" 
             >
            <HeaderStyle   Font-Bold= "True "   HorizontalAlign= "Center "   ForeColor="White"  BackColor= "#3198CA "   Font-Size="Small"> </HeaderStyle>
   
    <%--<HeaderStyle BackColor="#D3EAEF" />
   
    <RowStyle Font-Size="13px" />--%>
    
    <RowStyle CssClass="row-style"></RowStyle>
    
    <Columns>
    <asp:BoundField DataField="StoryID" Visible="false" HeaderText="博客编号" ReadOnly="true" />
    <asp:HyperLinkField DataTextField="Title" 
            DataNavigateUrlFormatString="Blog.aspx?id={0}"  
            DataNavigateUrlFields="StoryID"   HeaderText="标题" HeaderStyle-Width="257px">
<HeaderStyle Width="257px"></HeaderStyle>
        </asp:HyperLinkField>
    <asp:BoundField DataField="PublishTime" HeaderText="操作时间"  HeaderStyle-Width="125px">
    
    
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
        </div>
        <webdiyer:AspNetPager runat="server" ID="anp"  PageSize="10" Font-Size="12px"
                    FirstPageText="首页" 
                    LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" 
                    ShowCustomInfoSection="Never" TextAfterPageIndexBox="页" 
                    onpagechanged="getPager"     ShowPageIndexBox="Never" Width="500px">
                    </webdiyer:AspNetPager>
    </form>
</body>
</html>
