<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<FBS.Service.ActionModels.BlogCategoryModel>>" %>
<ul>
    <%foreach (var m in Model)
      { %>
      
    <li><a href="/Blog/Index/<%=m.BlogCategoryID %>"><%=m.CategoryName %></a> (3)<span><%=m.Description %></span></li>  
      <%
          } %>
    
    <!--<li><a href="#">Vivamus Fermentum</a> (13)<span>Lorem Ipsum Dolor Sit Amit</span> </li>-->
</ul>