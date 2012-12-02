<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfLicense>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Index</title>
<link media="screen" rel="Stylesheet" href="../../Scripts/dojo-release-1.8.1/dijit/themes/soria/soria.css" />
<!--<link media="screen" rel="Stylesheet" href="../../Scripts/dojo-release-1.8.1/dijit/themes/soria/Dialog.css" />-->
<script src="../../Scripts/dojo-release-1.8.1/dojo/dojo.js" type="text/javascript"></script>
<!--<script src="../../Scripts/dojo-release-1.8.1/dijit/dijit-all.js" type="text/javascript"></script>-->
<script type="text/javascript">
    require(["dijit/form/Button", "dojo/domReady!"], function (Button) {
        var button = new Button({
            label: "下一步",
            onClick: function () {
                top.location = "/Install/EnvironmentCheck";
            }
        }, "nextBtn");
        button.startup();
    });
    require(["dijit/layout/BorderContainer"]);
</script>
</head>
<body class="soria">

    <div>
    <h1><%="License："+Model.Info%></h1>
    </div>
    <button id="nextBtn"></button>
</body>
</html>
