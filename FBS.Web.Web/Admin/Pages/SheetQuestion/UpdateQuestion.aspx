<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateQuestion.aspx.cs" Inherits="FBS.Web.News.Pages.Back.UpdateQuestion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改问题</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 125px;
            font-size:14px
        }
        
        	a:link
{
	color: #000000;
 text-decoration: none;
}
a:visited
{
 text-decoration: none;
}
a:hover
{
 text-decoration: none;
}
a:active
{

 text-decoration: none;
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
					                    修改问卷问题</div>
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
            Guid questionID = Guid.Empty;
            
        if (!string.IsNullOrEmpty(Request.QueryString["questionID"]))
            {
                questionID = new Guid(Request.QueryString["questionID"]);
                Service.BlogService service = new Service.BlogService();
                Service.ActionModels.AnswerSheetQuestionModel model = new Service.ActionModels.AnswerSheetQuestionModel();
             try
             {
                 model = service.GetOneQuestionByID(questionID);
                 
             }
             catch (Exception error)
             {
                 Response.Write(error.Message); 
             }
             
 %>
 <tr><td class="style1"> 问题：</td><td><input type="text" name="question" value="<%=model.QuestionName%>" style="width:260px"/></td></tr>
       
    
    
    <input type ="hidden" name = "questionid" value ="<%=model.QuestionID %>" />
    <input type ="hidden" name = "formid" value ="<%=model.FormID %>" /> 
   <tr><td class="style1" style=" font-size:12px"><a href="Question.aspx?FormID=<%=model.FormID%>">返回问题列表</a></td></tr>
    <%
            }
        
        %>
    <tr><td class="style1"><input type ="submit" value ="修改问题"/ class="button"></td></tr>
    <tr><td class="style1" style=" font-size:12px"><a href="../FormInfoList/FormInfoList.aspx">返回问卷表</a></td></tr>
    
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

