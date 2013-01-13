<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.Settings.Settings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link rel="stylesheet" type="text/css" href="/Scripts/dojo-release-1.8.1/dojo/resources/dojo.css" media="all" />
<link rel="stylesheet" type="text/css" href="/Scripts/dojo-release-1.8.1/dojox/image/resources/Gallery.css" media="all" />
<link rel="stylesheet" type="text/css" href="/Scripts/dojo-release-1.8.1/dojox/image/resources/ThumbnailPicker.css" media="all" />
<link rel="stylesheet" type="text/css" href="/Scripts/dojo-release-1.8.1/dojox/image/resources/SlideShow.css" media="all" />
<link rel="stylesheet" type="text/css" href="/Scripts/dojo-release-1.8.1/dijit/themes/tundra/tundra.css" media="all" />

<script type="text/javascript" src="/Scripts/dojo-release-1.8.1/dojo/dojo.js" djConfig="isDebug:true,parseOnLoad:true"></script>
<script type="text/javascript">
    dojo.require("dojox/image/Gallery");
    dojo.require("dojo/data/ItemFileReadStore");
    dojo.require("dojo/parser");
</script>
</head>
<body class="tundra">
    <form id="form1" runat="server">
    <div>
        站点名称：<asp:TextBox runat="server" ID="txName"></asp:TextBox>
        <br />
        描述：<asp:TextBox runat="server" ID="txDesc"></asp:TextBox>
        <br />
        URL：<asp:TextBox runat="server" ID="txUrl"></asp:TextBox>
    </div>
    <div>
       版权信息：<asp:TextBox ID="txCopyRight" runat="server"></asp:TextBox>
    </div>
    <div>
    <div id="themes">
    <div jsId="imageItemStore" data-dojo-type="dojo/data/ItemFileReadStore"
  url="/Site/FetchThemes/"></div>
        <div id="gallery1" data-dojo-type="dojox/image/Gallery" data-dojo-props="imageHeight:400, imageWidth:600">
            <script type="dojo/connect">
                var itemRequest = {
                  query: {},
                  count: 20
                };
                var itemNameMap = {
                  imageThumbAttr: "SmallThumbnail",
                  imageLargeAttr: "LargeThumbnail"
                };
                this.setDataStore(imageItemStore, itemRequest, itemNameMap);
            </script>
        </div>
        
    </div>


    </div>
    <asp:Button ID="saveBtn" runat="server" Text="保存" OnClick="saveBtn_Click" />
    <asp:Label ID="msg" runat="server"></asp:Label>
    </form>
</body>
</html>
