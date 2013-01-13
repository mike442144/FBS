<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateAnswer.aspx.cs" Inherits="FBS.Web.News.Pages.Back.UpdateAnswer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改问卷问题选项</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
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
					                    修改问卷问题选项</div>
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
  
<table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
  <%
      if (!string.IsNullOrEmpty(Request.QueryString["answerid"])&&!string.IsNullOrEmpty(Request.QueryString["questionid"]))
      {
          Guid questionid = new Guid(Request.QueryString["questionid"]);
          Guid answerid = new Guid(Request.QueryString["answerid"]);
          Service.BlogService service = new Service.BlogService();
          Service.ActionModels.AnswerSheetQuestionModel model = service.GetOneQuestionByID(questionid);
       %>
      
      <tr><td style=" font-size:14px">问题名称：<%=model.QuestionName%></td></tr> 
        <input type="hidden" name ="questionid" value="<%=model.QuestionID%>" />
       <%Service.ActionModels.SheetAnswerModel m = service.FetchOneAnserByAnswerID(answerid);%>
         <tr><td><input type="text" name = "answer" value="<%=m.AnswerName%>" class="input"/></td>
         <td><input type="hidden" name ="answerid" value ="<%=m.AnswerID %>" /></td>
         </tr>
         
        
       <%} %>
       
       <tr><td><input type="submit" value="修改选项" class="button"/></td></tr>
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

