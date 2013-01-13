<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAdvertisement.aspx.cs"
    Inherits="FBS.Web.News.Admin.Pages.AdvertisementPages.AddAdvertisement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加广告</title>
    <link type="text/css" href="../../../css/jquery.datepick.css" rel="Stylesheet" />

    <script type="text/javascript" src="../../../script/jquery-1.6.2.min.js">
    </script>

    <script type="text/javascript" src="../../../script/jquery.datepick.js">
    </script>

    <script type="text/javascript" src="../../../script/jquery.datepick-zh-CN.js">
    </script>

    <script language="javascript" type="text/javascript">
    
    var axml;

    
    
    
    $(document).ready(function(){
    
    
    
    $.ajax({   
    url:'GetXML.ashx',   
    type: 'GET',   
    dataType: 'xml',   
    timeout: 100000,   
    error: function(xml){   
        alert('Error loading XML document'+xml);   
    },   
    success: function(xml){  
        axml=xml; 
         $(xml).find("Section").each(function(){
    $("#one").append("<option value="+$(this).attr('Id')+">"+$(this).attr('Name')+"</option>");
    });
    }   
}); 
    
    
   
$('#one').change(function(){
    $("#two").empty();
    $("#three").empty();
    $("#four").empty();
   
    $(axml).find("Section").each(function(){
if($(this).attr('Id')==$("#one").find("option:selected").val()){
$("#two").append("<option>--请选择--</option>");
        $(this).find('Page').each(function(){
        $("#two").append("<option value="+$(this).attr('Id')+">"+$(this).attr('Name')+"</option>");

      });
   }
      });

    });

      
$('#two').change(function(){
    $("#three").empty();
     $("#four").empty();
   
    $(axml).find("Page").each(function(){
if($(this).attr('Id')==$("#two").find("option:selected").val()){
 $("#three").append("<option>--请选择--</option>");
        $(this).find('Area').each(function(){
        $("#three").append("<option value="+$(this).attr('Id')+">"+$(this).attr('Name')+"</option>");

      });
   }
      });

    });

   
      
$('#three').change(function(){
    $("#four").empty();
   
    $(axml).find("Area").each(function(){
if($(this).attr('Id')==$("#three").find("option:selected").val()){
        $(this).find('Position').each(function(){
        $("#four").append("<option value="+$(this).attr('Id')+">"+$(this).attr('Name')+"</option>");

      });
   }
      });

    });




$("#btn").click(function(){
var oneS=$("#one").find("option:selected").value();
var twoS=$("#two").find("option:selected").value();
var thrS=$("#three").find("option:selected").value();
var fouS=$("#four").find("option:selected").value();
if(oneS==""||two==""||thrS==""||fouS==""){
alert("请选择完整的路径！");
}
else{
var centen=oneS+twoS+thrS+fouS;
$("#str").append("<P>"+centen+"</P>")
}
});
});

function  ShowDesciption()
{ 
var qu=document.getElementById("one").value;
var page=document.getElementById("two").value;
var area=document.getElementById("three").value;
var position=document.getElementById("four").value;

$(axml).find("Position").each(function(){
if($(this).attr('Id')==position){
 Wposition=$(this).attr('eWidth');
  Hposition=$(this).attr('eHeight');
  $("#advertisedescription").empty();
  $("#advertisedescription").append("此广告位图片的高该为"+Hposition+"宽该为"+Wposition);
}});


}
    </script>

    <script type="text/javascript">
$(document).ready(function(){
	$('#begintime').datepick({dateFormat: 'yy-mm-dd'}); 
	$('#endtime').datepick({dateFormat: 'yy-mm-dd'}); 
});
    </script>

    <script type="text/javascript">
            function chkUrlDisplayStatus()
            {
                var rb = document.getElementById("rbAnimatedGif");
                //alert(rb.checked);
                var tr = document.getElementById("trURL");
                if(rb.checked)
                {
                    tr.style.display = "block";
                }
                else
                {
                    tr.style.display = "none";
                    var url = document.getElementById("txtUrl");
                    url.setAttribute("value","");
                }
            }
            
            function ValidateInput()
            {
                var cb = document.getElementById("rbAnimatedGif");
                var file = document.getElementById("FileUpload1");
                var rx;
                var message;
                if(cb.checked)
                {
                    rx = new RegExp( "^([a-zA-Z].*|[1-9].*)\.(((j|J)(p|P)(g|G))|((g|G)(i|I)(f|F)))$", "g" );
                    message = "图片格式只允许 *.gif, *.jpg";
                }
                else
                {
                    rx = new RegExp( "^([a-zA-Z].*|[1-9].*)\.(((s|S)(w|W)(f|F)))$", "g" );
                    message = "视频格式只允许 *.swf ";
                }
                
                if(!rx.test(file.value))
                {
                    alert(message);
                    return false;
                }
                else
                {
                    return true;
                }
                
               // WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;btnSubmit&quot;, &quot;&quot;, true, &quot;CreateAd&quot;, &quot;&quot;, false, false));
            }
    </script>

    <style type="text/css">
        .style1
        {
            width: 142px;
        }
        .style2
        {
            width: 493px;
        }
        .style3
        {
            width: 421px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="5" cellpadding="5">
        <tr>
            <td class="style1">
                广告名称：
            </td>
            <td class="style2">
                <asp:TextBox ID="AdvertiseName" runat="server"></asp:TextBox>
            </td>
            <td class="style3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="AdvertiseName"
                    ErrorMessage="不能为空" ValidationGroup="CreateAd"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
                广告上线时间：
            </td>
            <td class="style2">
                <input type="text" name="begintime" value="点击选择日期" id="begintime" />
            </td>
            <td class="style3">
            </td>
        </tr>
        <tr>
            <td class="style1">
                广告结束时间：
            </td>
            <td class="style2">
                <input type="text" name="endtime" value="点击选择日期" id="endtime" />
            </td>
            <td class="style3">
            </td>
        </tr>
        <tr>
            <td class="style1">
                广告位置：
            </td>
            <td class="style2">
                <select id="one" name="section">
                    <option>--请选择--</option>
                </select>
                <select id="two" name="page">
                    <option>--请选择--</option>
                </select>
                <select id="three" name="area">
                    <option>--请选择--</option>
                </select>
                <select id="four" onclick="ShowDesciption()" name="position">
                    <option>--请选择--</option>
                </select><%--<input type="button" onclick="Show1()" />--%>
                <%--<asp:DropDownList ID="AdvertisePosition" runat="server">
                </asp:DropDownList>--%>
            </td>
            <td id="advertisedescription" class="style3">
            
                
            
            </td>
        </tr>
        <tr>
            <td class="style1">
                广告优先级：
            </td>
            <td class="style2">
                <asp:RadioButton ID="rbAnimatedGif" Visible="false" runat="server" GroupName="Selector"
                    Text="Images (*.gif, *.jpg)" Checked="true" /><br />
                <asp:TextBox ID="Priority" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Priority"
                    ErrorMessage="应为数字" ValidationGroup="CreateAd" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Priority"
                    ErrorMessage="不能为空" ValidationGroup="CreateAd"></asp:RequiredFieldValidator>
            </td>
            <td class="style3">
           
           </td>
        </tr>
        <tr>
            <td class="style1">
                选择图片：
            </td>
            <td class="style2">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="300" />
            </td>
            <td class="style3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="FileUpload1"
                    ErrorMessage="不能为空" ValidationGroup="CreateAd"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="FileUpload1"
                    ErrorMessage="图片只支持jpg和gif" ValidationGroup="CreateAd" ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((j|J)(p|P)(g|G))|((g|G)(i|I)(f|F)))$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
                宽度(像素)：
            </td>
            <td class="style2">
                <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
            </td>
            <td class="style3">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWidth"
                    ErrorMessage="应为数字" ValidationGroup="CreateAd" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWidth"
                    ErrorMessage="不能为空" ValidationGroup="CreateAd"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
                高度 (像素)：
            </td>
            <td class="style2">
                <asp:TextBox ID="txtHeight" runat="server"></asp:TextBox>
            </td>
            <td class="style3">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtHeight"
                    ErrorMessage="应为数字" ValidationGroup="CreateAd" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtHeight"
                    ErrorMessage="不能为空" ValidationGroup="CreateAd"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trURL" style="display: block">
            <td class="style1">
                广告链接：
            </td>
            <td class="style2">
                <asp:TextBox ID="txtUrl" runat="server" Width="284px"></asp:TextBox>
            </td>
            <td class="style3">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtUrl"
                    ErrorMessage="格式有误例子:http://www.0513jl.com" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                    ValidationGroup="CreateAd"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtUrl"
                    ErrorMessage="不能为空" ValidationGroup="CreateAd"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td class="style2">
                <asp:Button ID="btnSubmit" runat="server" Text="添加" Width="80" OnClick="btnSubmit_Click"
                    ValidationGroup="CreateAd" />
            </td>
            <td class="style3">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
