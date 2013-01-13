<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormInfoList.aspx.cs" Inherits="Aviator.Web.Pages.Back.FormInfoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>问卷信息</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>

    <script type="text/javascript">
    function Sub()
    {
        var category = document.getElementById("displayid");
        var title = category.options[category.selectedIndex].text;
        var formID = category.value;
        document.getElementById("FormId").value = formID;
        document.getElementById("Title").value = title;
        document.getElementById("display").submit();
    }
    
    function AddForm()
    {
         var form = document.getElementById("addnewform");
         form.style.display="block";

    }
    
    function ChooseDisForm()
    {
         var disform =document.getElementById("showDisForm");
         disform.style.display="block";
    }
    </script>

    <style type="text/css">
        #addform
        {
            height: 71px;
        }
           body{ 
        text-align:center; 
        font-family:Verdana,Helvetica,sans-serif; 
        } 

        .datatable{ 
        border:1px solid #d6dde6; 
        border-collapse:collapse; 
        } 
        .datatable td{ 
        border:1px solid #d6dde6; 
        text-align:right; 
        padding:4px; 
        } 

        .datatable th{ 
        border:1px solid #828282; 
                        background:#bcbcbc; 
        font-weight:bold; 
        text-align:left; 
        padding:4px; 
        } 
        .datatable caption{ 
        font:bold 1.9em "Times New Roman",Times,serif; 
        background:#b0c4de; 
        color:#333517; 
        border:1px solid #78a367; 
        } 

        .datatable tr:hover, .datatable .highlight{ 
        background:#fcdead; 

        } 
        .altrow{ 
        background:#eefeef; 
        color:#000000; 
        } 


    </style> 

<script type="text/javascript"> 
onload=function(){
    var rows=document.getElementsByTagName("tr");
    for(var i=1;i <rows.length;i++){ 
        rows[i].index=i
        rows[i].className=(i%2==0)?"":"altrow"
        rows[i].onmouseover=function(){ 
            this.className="highlight"; 
        } 
        rows[i].onmouseout=function(){ 
            this.className=this.index%2==0?"":"altrow"; 
        } 
    } 
}
</script> 
   
</head>
<body>
<table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;" class="datatable">
   
  
  <thead>
  <tr><td colspan="4"><div align="left"><input type="button" value="添加新问卷" onclick="AddForm()"  class="button"/></div></td></tr>
    <tr><td class="style5"><b>问卷主题</b></td>
    <td class="style6"><b>问卷描述</b></td><td><b>删除问卷</b></td><td><b>修改问卷</b></td></tr>
    </thead>
    <% Aviator.Service.BlogService service = new Aviator.Service.BlogService(); 
          var  form=   service.FetchFormInfo();
          if (form == null || form.Count == 0)
          {
              %>
               <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
            <tr><td>无问卷信息，请先添加！</td></tr>
            
            </table>
              
              <% 
          }
            
    foreach (Aviator.Service.ActionModels.FormInfoModel model in form)
       {%>
    <tr>
        <td><a href="../SheetQuestion/Question.aspx?FormID=<%=model.FormID %>"><%=model.Title %></a></td>
    
       <td ><%=model.Description %></td> 
    
       <td ><a href="FormInfoList.aspx?FormID=<%=model.FormID %>"><img border="0" alt="删除问卷" src="../img/cancel_x.gif"/></a></td> 
    
       <td ><a href="UpdateFormInfo.aspx?FormID=<%=model.FormID%>"><img border="0"  alt="修改问卷" src="../img/edit_x.gif"/></a></td> 
    <%} %>
     </tr>
    <tr><td colspan="4"><div align="left"><input type="button" value="选择您想显示的问卷信息" onclick="ChooseDisForm()" class="button"/></div></td></tr>
</table>
    <div id="showDisForm" style="display:none;">
     <%
        if (service.DisplayForm() == false)
        {
         %>
        <table  style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
         <tr><td><font color="red">您还没选择显示的问卷，请选择！</font></td></tr>
         </table>
         <%} %>
    <table  style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
    <form id="display" action="FormInfoList.aspx"  method="post">
   
   <tr><td colspan="4">请选择想显示的问卷名:<select id="displayid">
        <% 
            

            foreach (Aviator.Service.ActionModels.FormInfoModel model in service.FetchFormInfo())
            {%>
        <option value="<%=model.FormID %>"><%=model.Title %></option>
        <%} %>
    </select>
    </td>
    </tr>
    <input type="hidden" name="FormId" id="FormId" />
    <input type="hidden" name="Title" id="Title" />
   <tr><td> <input id="SubmitForm" type="button" onclick="Sub()" value="提交选择" class="button"/> </td>
    </tr>
    </form>
    </table>
    </div>
    
    
    <div style="display:none;" id="addnewform">
    <table style="width: 780px; margin:20px auto; border:solid 1px #ccc;padding:15px;">
       <form id="addform"  action ="FormInfoList.aspx" method="post">
    
    
    <tr><td>问卷主题：<input type="text" name="title" />(不能为空！)</td></tr>
    
    
    <tr><td>问卷描述：<input type ="text" name="description" />(不能为空！)</td></tr>
    
   
   
 <tr><td> <input type ="submit" value ="添加新问卷" class="button"/></td>
   <td><input type="button" class="button" onclick="back()" value="返回问卷信息"/></td></tr>
  <script type="text/javascript">
  function back(){
      window.location.href="FormInfoList.aspx";
  
  }
  </script>
    </form>
    </table>
    </div>
</body>
</html>
