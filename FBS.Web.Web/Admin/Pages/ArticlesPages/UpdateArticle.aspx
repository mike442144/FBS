<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="UpdateArticle.aspx.cs"
    Inherits="FBS.Web.News.Backstage.UpdateArticle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改新闻</title>
    <link href="../../css/right.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" charset="utf-8" src="../../kindeditor.js"></script>
    
    <script type="text/javascript">
        KE.show({
            id : 'editorcontent',
            imageUploadJson : 'UploadIMG.ashx',
            fileManagerJson : 'file_manager_json.ashx',
            allowFileManager : true,
		    afterCreate : function(id) {
		    }
        });
        KE.init({
        id : 'editorcontent',
         imageUploadJson : 'UploadIMG.ashx',
            fileManagerJson : 'file_manager_json.ashx',
             height :'350'
});
    </script>
    
    
    <style type="text/css">
        .style1
        {
            width: 844px;
        }
        #content
        {
            font-size: 12px;
            margin-right: 15px;
        }
        td
        {
            border: 1px #ccc solid;
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
            margin-left: px;
        }
        .style2
        {
            height: 29px;
            font-size: 14px;
        }
        #editorcontent
        {
            width: 749px;
        }
    </style>
    <!-- <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script> -->

    <script type="text/javascript">

        // 设置编辑器中内容
//        function SetEditorContents(ContentStr) {
//        if(ContentStr!=null&&ContentStr!="")
//        {
//        CKEDITOR.instances.editor1.insertHtml("<img src=" + "'" + ContentStr + "'" + "/>");
//        }
//        }
        
//        function check_img() {
//            var img = document.getElementById("url").value;
//            if (img != "" && img != null) {
//            
//            SetEditorContents(img);
//            
//            }
//        }
    function setEditor()
      {
        //alert(document.getElementById("ueditor").value);
          document.getElementById("ueditor").value=editor.getContent();
          
          
          return true;
         
      } 
       
    </script>

</head>
<body>
    <form id="form2" runat="server">
    <asp:TextBox ID="ueditor" runat="server" Style="display: none;"></asp:TextBox>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td width="17" valign="top" background="images/mail_leftbg.gif">
                <img src="../../images/left-top-right.gif" width="17" height="29" />
            </td>
            <td valign="top" background="images/content-bg.gif">
                <table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" class="left_topbg">
                    <tr>
                        <td height="31">
                            <div class="titlebt">
                                修改新闻</div>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="16" valign="top" background="images/mail_rightbg.gif">
                <img src="../../images/nav-right-bg.gif" width="16" height="29" />
            </td>
        </tr>
        <tr>
            <td valign="middle" background="../../images/mail_leftbg.gif">
                &nbsp;
            </td>
            <td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
                <div>
                    <table style="width: 780px; margin: 20px auto; border: solid 1px #ccc; padding: 15px;">
                        <asp:Label ID="Lable_id" runat="server" Visible="false" Font-Size="14px"></asp:Label>
                        <tr>
                            <td class="style1">
                                <div style="float: left; width: 88px; height: 21px; font-size: 14px">
                                    首页显示标题：</div>
                                <div>
                                    <asp:TextBox ID="ArticleBriefTitle" runat="server" Width="218px" class="input"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <div style="float: left; width: 88px; height: 21px; font-size: 14px">
                                    标题：</div>
                                <div>
                                    <asp:TextBox ID="ArticleTitle" runat="server" Width="218px" class="input"></asp:TextBox>
                                    <asp:Label ID="L_ImgName" runat="server" Visible="false" Text=""></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <div style="width: 15%; float: left; font-size: 14px">
                                    分类：</div>
                                <div align="left" style="width: 37%">
                                    <asp:DropDownList ID="ArticleCategory" runat="server" class="input">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <div style="width: 15%; float: left; font-size: 14px">
                                    原文链接：</div>
                                <asp:TextBox ID="ArticleLink" runat="server" Width="217px" class="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <div style="width: 15%; float: left; font-size: 14px">
                                    来源网站：
                                </div>
                                <asp:TextBox ID="ArticleWeb" runat="server" Width="212px" class="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            
                            
                             <textarea id="editorcontent" runat="server"  name="editorcontent" ></textarea>
                               </td>

                                
                        </tr>
                        <tr>
                            <td class="style1">
                                
                            </td>
                        </tr>
                        <%--<asp:Label ID="Lable_id"  Visible="false"  runat="server" Text="Label"></asp:Label>--%>

                    

                        <tr>
                            <td>
                                <asp:Button ID="ArticlesAddBtn" runat="server" Text="修改新闻" class="button" OnClick="ArticlesUpdateBtn_Click"
                                    OnClientClick="setEditor()" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td background="../images/mail_rightbg.gif">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="bottom" background="../images/mail_leftbg.gif">
                <img src="../../images/buttom_left2.gif" width="17" height="17" />
            </td>
            <td valign="bottom" background="../images/mail_rightbg.gif">
                <img src="../../images/buttom_bgs.gif" style="width: 100%; height: 16px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
