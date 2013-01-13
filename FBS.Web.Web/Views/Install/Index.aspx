<%@ Page Title="FBS安装" Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html >
<html>
<head>
<title>FBS安装</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="Stylesheet" href="/Scripts/dojo-release-1.8.1/dojo/resources/dojo.css" />
    <link type="text/css" rel="Stylesheet" href="/Scripts/dojo-release-1.8.1/dijit/themes/claro/claro.css" />
    <link type="text/css" rel="Stylesheet" href="/Scripts/dojo-release-1.8.1/dijit/themes/claro/layout/ContentPane.css" />
    <link type="text/css" rel="Stylesheet" href="/Scripts/dojo-release-1.8.1/dojox/widget/Wizard/Wizard.css" />
    <script src="/Scripts/dojo-release-1.8.1/dojo/dojo.js" data-dojo-config="parseOnLoad:true" type="text/javascript"></script>
    <script type="text/javascript">
        <!--require(["dojo/ready", "dojox/widget/Wizard", "dojox/widget/WizardPane", "dijit/layout/ContentPane"]);-->
    </script>
    <style type="text/css">
    .pageOverlay {
        top: 50px;
        left: 275px;
        position: absolute;
        height: auto;
        width: auto;
        z-index: 1001;
        display: none;
    }
 
    #loadingOverlay {
        background:  #fff url('/Scripts/dojo-release-1.8.1/dijit/themes/claro/images/loadingAnimation.gif') no-repeat 10px 23px;
    }
 
    .loadingMessage {
        padding: 25px 40px;
        color: #999;
    }
    </style>
    <script type="text/javascript">
        var theloadbox;
        function checkAgreement(name) {
            var acc = dojo.byId("ck");
            if (!acc.checked)
                return "在继续安装前，必须同意服务条款";
        }
        function saveDbCnf() {
            var cnfForm = document.forms["dbSettingForm"];
            theloadbox.startLoading();
            require(["dojo/request/xhr"], function (xhr) {
                xhr(cnfForm.action, {
                    query: {
                        DbAddr: cnfForm.DbAddr.value,
                        DbName: cnfForm.DbName.value,
                        DbUser: cnfForm.DbUser.value,
                        DbPsd: cnfForm.DbPsd.value
                    },
                    method: cnfForm.method
                }).then(function (data) {
                    theloadbox.endLoading();
                    if (data && data !== '') dijit.byId("completingPane").set("content", "安装失败！错误信息："+data);
                    else dijit.byId("completingPane").set("content", "安装成功点击完成") ;
                });
            });
        }
        function done() {
            top.location = "/Home/Index/";
        }
        
        
    </script>
</head>
<body class="claro">
    <div style="margin:0 auto;width:850px;padding-top:30px;text-align: center;position:relative;">
        <div id="loadingOverlay" class="loadingOverlay pageOverlay"><div class="loadingMessage">正在安装数据库......先去玩会蚂蚁吧:)</div></div>
        <div id="WizardContainer" data-dojo-type="dojox/widget/Wizard" data-dojo-props="nextButtonLabel: '下一步',previousButtonLabel: '上一步',doneButtonLabel:'完成'">
            <div id="Agreement" data-dojo-type="dojox/widget/WizardPane" passfunction="checkAgreement"
                data-dojo-props="">
                <h2>
                    FBS许可协议</h2>
                <hr />
                <div id="content" data-dojo-type="dijit/layout/ContentPane" data-dojo-props="href:'../../LICENSE.txt'"
                    style="width: 800px; height: 400px; margin: 0 auto;">
                </div>
                <hr />
                <input type="checkbox" name="ck" id='ck'>我已经阅读<<许可协议>>并且同意条款.</input>
            </div>
            <!--<div id="Env" data-dojo-type="dojox/widget/WizardPane" cangoback="false" data-dojo-props="href:'/Install/EnvironmentCheck',onLoad:onLoaded,onDownloadEnd:onEnd">
            </div>-->
            <div id="DbSettings" data-dojo-type="dojox/widget/WizardPane" passfunction="saveDbCnf" cangoback="false" data-dojo-props="">
                <div data-dojo-type="dijit/layout/ContentPane" data-dojo-props="" style="margin-top:50px;">
                    <form action="/Install/DbCnf" name="dbSettingForm" method="post">
                      <div>
                        <label for="DbAddr">数据库地址</label>
                        <input id="DbAddr" data-dojo-type="dijit/form/TextBox" name="DbAddr" type="text" value="127.0.0.1">
                      </div>
                      <div>
                        <label for="DbName">数据库名称</label>
                        <input id="DbName" data-dojo-type="dijit/form/TextBox" name="DbName" type="text" value="fbs">
                      </div>
                      <div>
                        <label for="DbUser">数据库用户</label>
                        <input id="DbUser" data-dojo-type="dijit/form/TextBox" name="DbUser" type="text" value="sa">
                      </div>
          
                      <div>
                        <label for="DbPsd">数据库密码</label>
                        <input id="DbPsd" data-dojo-type="dijit/form/TextBox" name="DbPsd" type="password">
                      </div>
                      <!--<div>
                        <input type="button" data-dojo-type="dijit/form/Button" data-dojo-props="label:'保存'" onclick="saveDbCnf"></input>
                      </div>-->
                  </form>
              </div>
            </div>
            <div data-dojo-type="dojox/widget/WizardPane" id='completingPane' data-dojo-props="doneFunction:done">
                
            </div>
        </div>
    </div>
    <script type="text/javascript">
        
        require(["dojo/_base/declare", "dojo/dom", "dojo/dom-style"],
        function (declare, dom, domStyle) {
            var loadbox = declare(null, {
                overlayNode: null,
                constructor: function () {
                    // save a reference to the overlay
                    this.overlayNode = dom.byId("loadingOverlay");
                },
                // called to hide the loading overlay
                endLoading: function () {
                    domStyle.set(this.overlayNode, 'display', 'none');
                },
                startLoading: function () {
                    domStyle.set(this.overlayNode, 'display', 'block');
                }
            });
            theloadbox = new loadbox();
        });
        
    </script>
</body>
</html>
