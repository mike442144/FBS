<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/ITsdsMasterPage.Master" Inherits="System.Web.Mvc.ViewPage" %>



<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
<link href="../../css/connectionUs.css" rel="stylesheet" type="text/css"/>
<title>联系我们</title>
<!--引用百度地图API-->
<style type="text/css">
   /*html,body{margin:0;padding:0;}*/
    .iw_poi_title {color:#CC5522;font-size:14px;font-weight:bold;overflow:hidden;padding-right:13px;white-space:nowrap}
    .iw_poi_content {font:12px arial,sans-serif;overflow:visible;padding-top:4px;white-space:-moz-pre-wrap;word-wrap:break-word}
</style>
<script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NavigationContent" runat="server">
<div class="navitem"><a href="/Home/Index">首页</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Home/About">企业简介</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Product/Index">作品展示</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Service/Index">服务项目</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/News/NewsList">企业动态</a></div>
<div class="navfenge"></div>
<div class="navitem"><a href="/Jobs/Index">招贤纳士</a></div>
<div class="navfenge"></div>
<div class="navitemCurrent"><a href="/Home/Contact">联系我们</a></div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--口号展示区 begin -->
<div class="blank0"></div>
<div class="bigshowbox">
<div class="one"><span>惊雷网络</span></div>
<div class="blank0"></div>
<div class="two"><span>过去，我们如惊雷前的蓄势待发</span></div>
<div class="blank0"></div>
<div class="three"><span>现在，我们似随惊雷的闪电绽放</span></div>
<div class="blank0"></div>
<div class="four"><span>未来我们一定让你如惊雷贯耳</span></div>
<div class="blank0"></div>
<div class="five"></div>
<div class="blank0"></div>
<div class="ltr">
<div class="leftline"></div>
<div class="connectionustext">联系我们</div>
<div class="rightline"></div>
</div>
<div class="six"></div>
<div class="seven"></div>
<div class="connectionblank50"></div>
<div class="blank0"></div>
</div>
<!--口号展示区 end -->
<div class="blankblack8"></div>

<!--地图展示区 begin -->
<div class="mapbox">
   <div class="connectionwaybox">
     <p>传真：0513-81181061</p>
     <p>联系电话：0513-81181061</p>
     <p>Email：mike442144@163.com</p>
     <p>地址：南通市崇川区崇川路58号南通科技园3号楼215室</p>
   </div>
   <div class="blank0"></div>
   <div class="map">
     <div style="height:100%;width: 100%;border:#ccc solid 1px;" id="dituContent"></div>
   </div>
</div>
<!--地图展示区 end -->
<script type="text/javascript">
    //创建和初始化地图函数：
    function initMap(){
        createMap();//创建地图
        setMapEvent();//设置地图事件
        addMapControl();//向地图添加控件
        addMarker();//向地图中添加marker
    }
    
    //创建地图函数：
    function createMap(){
        var map = new BMap.Map("dituContent");//在百度地图容器中创建一个地图
        var point = new BMap.Point(120.892645,31.974472);//定义一个中心点坐标
        map.centerAndZoom(point,14);//设定地图的中心点和坐标并将地图显示在地图容器中
        window.map = map;//将map变量存储在全局
    }
    
    //地图事件设置函数：
    function setMapEvent(){
        map.enableDragging();//启用地图拖拽事件，默认启用(可不写)
        map.enableScrollWheelZoom();//启用地图滚轮放大缩小
        map.enableDoubleClickZoom();//启用鼠标双击放大，默认启用(可不写)
        map.enableKeyboard();//启用键盘上下左右键移动地图
    }
    
    //地图控件添加函数：
    function addMapControl(){
        //向地图中添加缩放控件
	var ctrl_nav = new BMap.NavigationControl({anchor:BMAP_ANCHOR_TOP_LEFT,type:BMAP_NAVIGATION_CONTROL_LARGE});
	map.addControl(ctrl_nav);
        //向地图中添加缩略图控件
	var ctrl_ove = new BMap.OverviewMapControl({anchor:BMAP_ANCHOR_BOTTOM_RIGHT,isOpen:1});
	map.addControl(ctrl_ove);
        //向地图中添加比例尺控件
	var ctrl_sca = new BMap.ScaleControl({anchor:BMAP_ANCHOR_BOTTOM_LEFT});
	map.addControl(ctrl_sca);
    }
    
    //标注点数组
    var markerArr = [{title:"南通惊雷网络科技有限公司",content:"",point:"120.902329|31.980292",isOpen:0,icon:{w:21,h:21,l:0,t:0,x:6,lb:5}}
		 ];
    //创建marker
    function addMarker(){
        for(var i=0;i<markerArr.length;i++){
            var json = markerArr[i];
            var p0 = json.point.split("|")[0];
            var p1 = json.point.split("|")[1];
            var point = new BMap.Point(p0,p1);
			var iconImg = createIcon(json.icon);
            var marker = new BMap.Marker(point,{icon:iconImg});
			var iw = createInfoWindow(i);
			var label = new BMap.Label(json.title,{"offset":new BMap.Size(json.icon.lb-json.icon.x+10,-20)});
			marker.setLabel(label);
            map.addOverlay(marker);
            label.setStyle({
                        borderColor:"#808080",
                        color:"#333",
                        cursor:"pointer"
            });
			
			(function(){
				var index = i;
				var _iw = createInfoWindow(i);
				var _marker = marker;
				_marker.addEventListener("click",function(){
				    this.openInfoWindow(_iw);
			    });
			    _iw.addEventListener("open",function(){
				    _marker.getLabel().hide();
			    })
			    _iw.addEventListener("close",function(){
				    _marker.getLabel().show();
			    })
				label.addEventListener("click",function(){
				    _marker.openInfoWindow(_iw);
			    })
				if(!!json.isOpen){
					label.hide();
					_marker.openInfoWindow(_iw);
				}
			})()
        }
    }
    //创建InfoWindow
    function createInfoWindow(i){
        var json = markerArr[i];
        var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>"+json.content+"</div>");
        return iw;
    }
    //创建一个Icon
    function createIcon(json){
        var icon = new BMap.Icon("http://openapi.baidu.com/map/images/us_mk_icon.png", new BMap.Size(json.w,json.h),{imageOffset: new BMap.Size(-json.l,-json.t),infoWindowOffset:new BMap.Size(json.lb+5,1),offset:new BMap.Size(json.x,json.h)})
        return icon;
    }
    
    initMap();//创建和初始化地图
</script>
</asp:Content>