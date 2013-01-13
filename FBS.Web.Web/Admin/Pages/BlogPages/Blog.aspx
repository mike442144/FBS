<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.BlogPages.Blog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Content/ueditor/editor_config.js"></script>
    <script type="text/javascript" src="/Content/ueditor/editor_all.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    title:<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    category:<asp:DropDownList ID="selectCategory" runat="server"></asp:DropDownList>
    content:
    <div>
    <textarea name="editorcontent" id="myEditor"></textarea>
<script type="text/javascript">
    var editor = new UE.ui.Editor();
    editor.render("myEditor");
    //1.2.4以后可以使用一下代码实例化编辑器
    //UE.getEditor('myEditor');
</script>
    </div>
    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
    </form>
</body>
</html>
