<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfCheck>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EnvironmentCheck</title>
</head>
<body>
    <div>
    <h1><%="系统版本："+Model.OSVersion %></h1>
    <h2><%=".Net运行时版本："+Model.RunTimeVersion %></h2>
    </div>
    <%=Html.ActionLink("下一步", "DbCnf")%>
</body>
</html>
