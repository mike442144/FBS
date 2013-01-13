<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplySuggest.aspx.cs"  validateRequest=false  Inherits="FBS.Web.News.Admin.Pages.Suggest.ReplySuggest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>回复建议，留言</title>
     <script type="text/javascript" src="../../ckeditor/ckeditor.js"></script>
   <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .ckeditor
        {
            height: 106px;
            width: 478px;
        }
    </style>
<script type="text/javascript" charset="utf-8" src="../../../kindeditor.js"></script>
    
    
     <script type="text/javascript">
        KE.show({
            id : 'editorcontent',
            imageUploadJson : 'UploadIMG.ashx',
            fileManagerJson : 'file_manager_json.ashx',
            allowFileManager : true,
		    afterCreate : function(id) {
		    KE.toolbar.disable(id, []);
            KE.readonly(id);
            KE.g[id].newTextarea.disabled = true;
		    }
        });
        KE.init({
        id : 'editorcontent',
         imageUploadJson : 'UploadIMG.ashx',
            fileManagerJson : 'file_manager_json.ashx'
});
    </script>
</head>




<body>
    <form id="form2" method="post" action="ReplySuggest.aspx">
    
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
					                    回复建议</div>
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
    <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px; height: 268px;">
    <%
        if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        { 
            Guid id =new Guid( Request.QueryString["id"]);
            Service.SuggestService service = new Service.SuggestService();
            Service.ActionModels.SuggestModel model = service.GetOneSuggest(id);%>
        
        <tr>
        <td style=" font-size:14px">建议直通:</td><td><textarea class="" id="" name="" 
                readonly="readonly" cols="" rows="" style="width:650px; min-height:150px"><%=model.Body %></textarea></td>
        </tr>
         <tr>
        <td style=" font-size:14px">回复:</td><td><textarea class="" name="txtContent" 
                 cols="20" rows="1" id="txtContent" style="width:650px; min-height:350px"></textarea></td>
                      <script type="text/javascript">
        KE.show({
            id : 'txtContent',
            width :'960',  //编辑器的长度
            imageUploadJson : 'UploadIMG.ashx',
            fileManagerJson : 'file_manager_json.ashx',
            allowFileManager : true,
		    afterCreate : function(id) {
		    }
        });
        KE.init({
        id : 'txtContent',
         imageUploadJson : 'UploadIMG.ashx',
            fileManagerJson : 'file_manager_json.ashx'
});
    </script>
        </tr>
        <input type="hidden" name="sid" value="<%=id %>"/>
        <%} %>
        <tr><td><input type="submit" value="提交回复" class="button"/></td></tr>
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


