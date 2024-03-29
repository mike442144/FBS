﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Install.Master" Inherits="System.Web.Mvc.ViewPage<FBS.Service.ActionModels.StepOfSiteCnf>" %>

<asp:Content ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        require(["dijit/form/Button", "dijit/form/TextBox", "dijit/form/SimpleTextarea", "dijit/form/ValidationTextBox","dojo/parser", "dojo/domReady!"], function (Button, TextBox, SimpleTextarea, ValidationTextBox) {
            var button = new Button({
                label: "完成",
                onClick: function () {
                    siteform.submit();
                }
            }, "submitBtn");
            button.startup();

            var siteName = new TextBox({
                placeHolder: "名称",
                name:"SiteName"
            }, "SiteName");
            siteName.startup();

            var siteDesc = new SimpleTextarea({
                rows: 5,
                cols: 30,
                value: "简单介绍一下您的网站...",
                name:"SiteDesc"
            }, "SiteDesc");
            siteDesc.startup();

            var siteUrl = new TextBox({
                placeHolder: "域名",
                name:"SiteUrl"
            }, "SiteUrl");
            siteUrl.startup();

            var email = new ValidationTextBox({
                required: true,
                regExp:'/^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$/',
                invalidMessage:"Invalid email format",
                name:"FounderEmail"
            }, "FounderEmail");
            email.startup();

        });
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <%using (Html.BeginForm("SiteCnf", "Install", FormMethod.Post, new { @id = "siteform" }))
      {
          %>
          <%=Html.ValidationSummary(true) %>
          
          <div>
            <%=Html.LabelFor(model=>model.SiteName) %>
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.SiteName)%>
            <%=Html.ValidationMessageFor(model => model.SiteName)%>
          </div>
          <div>
            <%=Html.LabelFor(model=>model.SiteDesc) %>
            
          </div>
          <div>
            <%=Html.TextAreaFor(model => model.SiteDesc)%>
            <%=Html.ValidationMessageFor(model => model.SiteDesc)%>
            
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.SiteUrl) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.SiteUrl)%>
            <%=Html.ValidationMessageFor(model => model.SiteUrl)%>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.FounderName) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.FounderName)%>
            <%=Html.ValidationMessageFor(model => model.FounderName)%>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.FounderPsd) %>
            
          </div>
          <div>
            <%=Html.PasswordFor(model => model.FounderPsd)%>
            <%=Html.ValidationMessageFor(model => model.FounderPsd)%>
          </div>
          
          <div>
            <%=Html.LabelFor(model=>model.FounderEmail) %>
            
          </div>
          <div>
            <%=Html.TextBoxFor(model => model.FounderEmail)%>
            <%=Html.ValidationMessageFor(model => model.FounderEmail)%>
          </div>
          
          <div>
            <button id="submitBtn"></button>
          </div>
          <% 
          
      }
        
         %>
    </div>
</asp:Content>
