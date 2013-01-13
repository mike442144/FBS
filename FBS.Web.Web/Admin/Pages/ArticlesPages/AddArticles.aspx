<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false"  CodeBehind="AddArticles.aspx.cs" Inherits="FBS.Web.News.Backstage.AddArticles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加新闻</title>
    <script type="text/javascript" src="/Content/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="/Content/ueditor/editor_all.js"></script>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
        	font-size:14px;
            width: 844px;
        }
        
        #content
        {
           font-size:12px;
           margin-right:15px;
        }
        td
        {
        	border:1px  #ccc solid;
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
            margin-left: 0px;
        }
        .style2
        {
            width: 844px;
            height: 28px;
        }
        #editor1
        {
            height: 118px;
            width: 566px;
        }
        #editorcontent
        {
            width: 688px;
        }
    </style>
    
</head>
<body>
    <form id="form2" runat="server">
    <asp:TextBox ID="ueditor" runat="server" style="display:none;"></asp:TextBox>
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
					                    添加新闻</div>
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
  
    <div>
  <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
  <tr>
  <td class="style1">
      <div style="width:20%;float:left">首页显示标题：</div>
      <asp:TextBox ID="ArticleBriefTitle" runat="server" Width="184px" class="input"></asp:TextBox>
      
      </td>
  </tr>
  <tr>
  <td class="style1">
      <div style="width:20%;float:left">标题：</div>
      <asp:TextBox ID="ArticleTitle" runat="server" Width="184px" class="input"></asp:TextBox>
                        <asp:Label ID="L_ImgName" runat="server" Visible="false" Text=""></asp:Label>
      </td>
  </tr>
   <tr>
  <td class="style2">
     <div style="width:20%;float:left"> 分类：</div><asp:DropDownList ID="ArticleCategory" runat="server" class="input">
      </asp:DropDownList> 
      </td>
  </tr>
   <tr>
  <td class="style1">
   <div style="width:20%;float:left">  原文链接：</div>
                        <asp:TextBox ID="ArticleLink" runat="server" Width="180px" 
          class="input"></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td class="style1">
    <div style="width:20%;float:left"> 来源网站：</div><asp:TextBox ID="ArticleWeb" runat="server" 
                            Width="179px" class="input"></asp:TextBox>
      </td>
  </tr>
  <tr>
  <td class="style1">
  <span>新闻中图片的宽度请设置成560px以下</span>
       <textarea name="editorcontent" id="myEditor"></textarea>
<script type="text/javascript">
    var editor = new UE.ui.Editor();
    editor.render("myEditor");
    //1.2.4以后可以使用一下代码实例化编辑器
    //UE.getEditor('myEditor');
</script>
    </td> 
  </tr>
   <tr>
  <td class="style1"> 
     
          
          </td>
  </tr>
  <tr>
  <td>
  
      <asp:Button ID="ArticlesAddBtn" runat="server" Text="发布新闻"  class="button"
          onclick="ArticlesAddBtn_Click" />
          &nbsp;</td>
  </tr>
  
  </table>
    </div>
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

