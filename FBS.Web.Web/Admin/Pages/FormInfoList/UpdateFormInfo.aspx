<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateFormInfo.aspx.cs" Inherits="FBS.Web.News.Pages.Back.UpdateFormInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改问卷信息</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 263px;
        }
    </style>
</head>
<body>

 <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
   <form id="updateform" runat="server" action="UpdateFormInfo.aspx" method="post">
   
        <%
       string formID = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["FormID"]))
            {
                formID = Request.QueryString["FormID"];
              Service.ActionModels.FormInfoModel model = new Service.ActionModels.FormInfoModel();
              Service.BlogService service = new Service.BlogService();
             try
             {
                 model = service.ViewFormByID(new Guid(formID));
             }
             catch (Exception error)
             {
                 Response.Write(error.Message); 
             }
             
 %>

 <tr><td class="style1"><b>主题：</b><input type="text" name="title" value="<%=model.Title%>" class="input" /></td></tr>
 <tr><td><b>描述：</b><input type="text" name="description" value="<%=model.Description %>" class="input"/></td>
 </tr>
        
    
    
        
    
    <input type ="hidden" name = "formid" value ="<%=model.FormID %>" />
    <%
            }
        
        %>
   <tr><td><input type ="submit" value ="修改问卷" class="button"/></td></tr> 
    
    </form>
    </table>
</body>
</html>
