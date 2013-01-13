<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAnswer.aspx.cs" Inherits="FBS.Web.News.Pages.Back.AddAnswer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加问题选项</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 270px;
            font-size:14px;
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
					                    添加问题选项</div>
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
               
            <tr>
            <td class="style1">问题</td>
            <td class="style1">答案</td>
            
            </tr>
        <%
            Guid questionid = new Guid(Request.QueryString["questionid"]);
            FBS.Service.BlogService service = new FBS.Service.BlogService();
            FBS.Service.ActionModels.AnswerSheetQuestionModel mo = service.GetOneQuestionByID(questionid);
        %>
        
        <tr>
        <td class="style1"><%=mo.QuestionName%></td>
        <td><input type="text" name="answer" /></td>
        </tr>
            
        
            
        <input type="hidden" name="questionid" value="<%=mo.QuestionID %>" />
        <input type="hidden" name="formid" value="<%=mo.FormID %>" />
        
   
        <tr><td><input type="submit" value="添加答案" /></td></tr>
        <tr><td><a href="../SheetQuestion/Question.aspx?FormID=<%=mo.FormID %>">返回问题列表</a></td></tr> 
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

