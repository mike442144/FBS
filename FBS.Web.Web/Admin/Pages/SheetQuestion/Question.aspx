<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="FBS.Web.News.Pages.Back.Question" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>问卷问题页面</title>
     <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <script type="text/javascript">
    function AddQuestion()
    {
        var question = document.getElementById("addquestion");
        question.style.display="block";
        document.getElementById("questioncontent").focus();
    }
    
    function AddAnswer()
    {
        var answer = document.getElementById("addanswer");
        answer.style.display="block";
    }
    </script>

    <style type="text/css">
        .style15
        {
            width: 274px;
        }
        .style16
        {
            width: 66px;
        }
        .style17
        {
            width: 61px;
        }
        .style18
        {
            width: 91px;
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
					                    问卷问题添加</div>
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
           <tr> <td><input type="button" onclick="AddQuestion()" value="添加新问题" class="button"/></td></tr>
            
            </table>
    
    
    <%
        Guid FormID = Guid.Empty;

        if (!string.IsNullOrEmpty(Request.QueryString["FormID"]))
        {
            FormID = new Guid(Request.QueryString["FormID"]);

            Service.BlogService service = new Service.BlogService();
            var x=service.FetchQuestionByFormID(FormID);
            if(x==null||x.Count==0)
            {
            %>
            <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px; font-size:14px">
            <tr><td>该问卷下没有问题，请添加新问题！</td></tr>
            
            </table>
            <%
            }
            foreach (Service.ActionModels.SheetQuestionModel model in x)
            {
            
                Guid questionid = model.QuestionID;
                Guid formid = model.FormID;
                
                
                %>
               
    <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
    <thead>
   
    <tr>
    <td><b><%=model.QuestionName%></b> </td>
    <td class="style16">
        <a href="Question.aspx?questionID=<%=model.QuestionID%>&formID=<%=model.FormID%>"><img border="0" alt="删除问题" src="../img/cancel_x.gif"/></a></td>
    <td class="style17">
            <a href="UpdateQuestion.aspx?questionID=<%=model.QuestionID %>"><img border="0" alt="修改问题" src="../img/edit_x.gif"/></a></td>
    
    <td class="style18"><a href="../SheetAnswer/AddAnswer.aspx?questionid=<%=model.QuestionID %>" class="button">添加选项</a></td>
    
    </tr>
    </thead>
          
       
    <%foreach (Service.ActionModels.SheetAnswerModel m in service.FetchAnswerByQuestionID(questionid))
      { %>
        <tr>
            <td class="style15">
                <%=m.AnswerName %>
            </td>
            <td class="style16">
                <a href="Question.aspx?answerid=<%=m.AnswerID %>&formID=<%=formid%>"><img alt="删除选项" border="0" src="../img/Delete.gif"/></a>
            </td>
            <td class="style17">
               <a href="../SheetAnswer/UpdateAnswer.aspx?answerid=<%=m.AnswerID %>&questionid=<%=m.QuestionID%>"><img border="0" alt="修改选项" src="../img/edit_x.gif"/></a>
            </td>
            <td class="style18">
                </td>
        </tr>
        
      
        <%}
            }
        } %>
    </table>   
      <div id="addquestion" style="display:;">
      <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;" >
        <tr><td>请添加该问卷下的问题内容:</td><td><input type="text" name="question" id="questioncontent" style="width:260px" /></td></tr>
            
        <input type="hidden" name="formid" value="<%=FormID%>" />
        
        
        <tr><td><input type="submit" value="添加问题" class="button"/></td></tr>
            
        <tr><td><a href="../FormInfoList/FormInfoList.aspx" class="button">返回问卷信息</a></td></tr> 
    
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

