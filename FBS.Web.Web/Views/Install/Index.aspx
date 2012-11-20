<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfLicense>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Index</title>
</head>
<body>

    <div>
    <h1><%="License："+Model.Info%></h1>
    </div>
    <%=Html.ActionLink("下一步", "EnvironmentCheck")%>

</body>
</html>
