<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAdvertisementOld.aspx.cs"
    Inherits="Aviator.Web.Admin.AddAdvertisement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加广告</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        body
        {
            text-align: center;
        }
        .datatable
        {
            border: 1px solid #d6dde6;
            border-collapse: collapse;
            text-align: center;
        }
        .datatable td
        {
            border: 1px solid #d6dde6;
            text-align: right;
            padding: 4px;
        }
        .input
        {
            width: 180px;
            height: 23px;
            line-height: 23px;
            font-size: 12px;
            font-weight: bold;
            color: #9D9E9D;
            padding: 0px 10px;
            border: 1px solid #90ACC3;
        }
        .datatable th
        {
            border: 1px solid #828282;
            background: #bcbcbc;
            font-weight: bold;
            text-align: left;
            padding: 4px;
        }
        .datatable caption
        {
            font: bold 1.9em "Times New Roman" ,Times,serif;
            background: #b0c4de;
            color: #333517;
            border: 1px solid #78a367;
        }
        .datatable tr:hover, .datatable .highlight
        {
            background: #fcdead;
        }
        .altrow
        {
            background: #eefeef;
            color: #000000;
        }
        .style2
        {
        	font-size:14px
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
    <form id="form1" runat="server">
    <div>
        <table class="datatable">
            <tr>
                <td class="style2">
                    广告类型：
                </td>
                <td class="style5">
                    <asp:DropDownList ID="DD_AdvertiseType" AutoPostBack="true" runat="server" class="input">
                        <asp:ListItem Text="图片" Selected="True" Value="true"></asp:ListItem>
                        <%--<asp:ListItem Text="Flash" Value="false"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    广告投放开始时间：
                </td>
                <td class="style3">
                    <asp:Calendar ID="C_AdvertiseBegin" runat="server"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    广告结束时间：
                </td>
                <td class="style3">
                    <asp:Calendar ID="C_AdvertiseEnd" runat="server"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td>
                    广告内容：
                </td>
                <td class="style3">
                    <asp:FileUpload ID="FU_AdvertiseContent" runat="server" Width="192px" class="input"/>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    广告优先级：
                </td>
                <td class="style3">
                    <asp:TextBox ID="TB_AdvertisePriority" runat="server" class="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    广告显示的页面：
                </td>
                <td class="style3">
                    <asp:DropDownList ID="DD_AdvertisePage" runat="server" class="input">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    广告占位符名称：
                </td>
                <td class="style3">
                <asp:DropDownList ID="TB_PositionName" runat="server">
                <asp:ListItem Text="宽广告"  Value="long"></asp:ListItem>
                <asp:ListItem Text="窄广告" Value="short"  ></asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                
                <td class="style2">
                    广告URL：
                </td>
                <td class="style3">
                    <asp:TextBox ID="TB_AdvertisementURL" runat="server" class="input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    <asp:Button ID="BT_AddAdvertise" runat="server" Text="添加广告" OnClick="BT_AddAdvertise_Click" class="button" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
