<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvertisementList.aspx.cs"
    Inherits="FBS.Web.News.Admin.Pages.AdvertisementPages.AdvertisementList1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>广告列表</title>

    <script type="text/javascript" src="../../../script/jquery-1.6.2.min.js">
    </script>

   <%-- <script language="javascript" type="text/javascript">
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
if($(this).attr('Name')==$("#one").find("option:selected").text()){
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
if($(this).attr('Name')==$("#two").find("option:selected").text()){
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
if($(this).attr('Name')==$("#three").find("option:selected").text()){
        $(this).find('Position').each(function(){
        $("#four").append("<option value="+$(this).attr('Id')+">"+$(this).attr('Name')+"</option>");

      });
    }
      });
   });



//$("#btn").click(function(){
//var oneS=$("#one").find("option:selected").value();
//var twoS=$("#two").find("option:selected").value();
//var thrS=$("#three").find("option:selected").value();
//var fouS=$("#four").find("option:selected").value();
//if(oneS==""||two==""||thrS==""||fouS==""){
//alert("请选择完整的路径！");
//}
//else{
//var centen=oneS+twoS+thrS+fouS;
//$("#str").append("<P>"+centen+"</P>")
//}
//});
});

function  Show1()
{
var qu=document.getElementById("one").value;
var page=document.getElementById("two").value;
var area=document.getElementById("three").value;
var position=document.getElementById("four").value;

alert(qu+"."+page+"."+area+"."+position);
}
    </script>--%>

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
  $("#advertisedescription").append("此广告位图片的高该为"+Hposition+"宽该为"+Wposition);
}});


}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                <select id="four" name="position">
                    <option>--请选择--</option>
                </select><%--<input type="button" onclick="Show1()" />--%>
                <asp:Button ID="ViewAdvertise" Text="查看广告" runat="server" OnClick="ViewAdvertise_Click" />
                <%--<asp:DropDownList ID="AdvertisePosition" runat="server">
                </asp:DropDownList>--%>
            </td>
            <td class="style3">
            </td>
        </tr>
    </div>
    <div>
        <asp:Label ID="L_location" runat="server" Visible="false"></asp:Label>
        <asp:GridView ID="AdvertiseGridView" Width="309px" runat="server" RowStyle-CssClass="row-style"
            DataKeyNames="Id" Font-Size="12px" OnRowDeleting="Row_Deleted" AutoGenerateColumns="False"
            OnRowDataBound="gvBaidu_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView_PageIndexChanging"
            PageSize="3">
            <HeaderStyle Font-Bold="True " HorizontalAlign="Center " ForeColor="Black" BackColor="#3198CA "
                Font-Size="Small"></HeaderStyle>
            <Columns>
                <asp:BoundField DataField="ID" Visible="false" HeaderText="新闻编号" ReadOnly="true" />
                <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFormatString="UpdateAdvertisement.aspx?id={0}"
                    DataNavigateUrlFields="ID" HeaderText="广告名称" />
                <asp:BoundField DataField="Begin" Visible="true" HeaderText="上线时间" ReadOnly="true" />
                <asp:BoundField DataField="End" Visible="true" HeaderText="结束时间" ReadOnly="true" />
                <asp:TemplateField HeaderText="删除">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/Admin/images/del.gif" runat="server"
                            CausesValidation="false" CommandName="Delete" Text="删 除" Width="15px" Height="15px"
                            BackColor="#FFFFFF" BorderStyle="None" ForeColor="#000000" OnClientClick="return confirm('您确认删除该记录吗?');" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
