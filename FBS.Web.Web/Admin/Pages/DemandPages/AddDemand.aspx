<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDemand.aspx.cs" Inherits="Aviator.Web.Admin.Pages.DemandPages.AddDemand" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<!--Updated by Tian Ze on 28th Feb 2011; add on table businesscustomer_content  -->
<html>	<head>	<title>添加团购信息</title>
		<META http-equiv="Content-Type" content="text/html; charset=gbk">

<META content="窝窝团 窝窝网 窝窝团购 窝窝 55tuan wowo 窝窝团购网 团购 北京团购 上海团购 广州团购 深圳团购 杭州团购 成都团购 郑州团购 重庆团购 武汉团购 天津团购 西安团购 南京团购 济南团购 青岛团购 团购网站大全 团购大全 团购导航 团购网 团购指南 团购搜索 消费 优惠 折扣 打折 优惠券 抵扣券 消费券 电影票 票务 火锅 自助餐 餐饮 美食 食品 美容 美发 化妆品 瑜伽 母婴 亲子 旅游 娱乐 购物 客服电话 4006969555" name="Keywords">
<META content="窝窝团团购网站是最具特色的消费服务团购平台，每天推出最新团购，提供众多最受欢迎的团购消费产品和服务项目，团购类别包括餐饮、美食、票务、美容美发、化妆品、快消品、母婴、娱乐、旅游、健身、户外等方方面面，涵盖餐厅、酒吧、KTV、健身房、SPA、美容院、美发店、瑜伽馆、温泉、酒店、游乐场、热门商圈等场所，开心团购，尽在窝窝团。我的团购窝窝团，团购就上窝窝团。" name="Description">
<LINK href="http://s0.55tuanimg.com/themes/default/images/static/img/page_img110418/favicon.ico" rel="shortcut icon">
<LINK href="http://s0.55tuanimg.com/themes/default/images/static/img/page_img110418/animated_favicon.gif" type="image/gif" rel="icon">
<script type="text/javascript" src="../../../script/jquery-1.6.2.min.js" ></script>
<!--[if IE 6]> 
<script type="text/javascript" src="http://s2.55tuanimg.com/themes/default/images/static/js/DD_belatedPNG_0.0.8a-min.js"></script>
<script type="text/javascript">DD_belatedPNG.fix('*'); </script> 
<![endif]-->
<script type="text/javascript" src="http://s2.55tuanimg.com/themes/default/images/static/js/script-20110826-1.js" charset="UTF-8"></script>
<link href="../../../css/css-20110822-1.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
$(document).ready(function(){
	$("#xq_tx").show();
	$("#xq_close").click(function(){
		$("#xq_tx").hide();
	});
	$("#index_tx").show();
    $("#index_close").click(function(){
		$("#index_tx").hide();
	})
})
</script>
	    <style type="text/css">
            .style1
            {
                font-size: 14px;
                color: #333;
                text-align: right;
                height: 11px;
            }
            .style2
            {
                height: 11px;
            }
        </style>

	</head>



	<body>

<div class="g_head_box">

    <div class="g_head_logo">

        <div class="g_head_logo_ab"><a href="http://www.55tuan.com/">窝窝团logo</a></div>

    </div>

    

  <%--  <div class="g_head_logo_test"></div>

    <div class="g-head-city-select-but">

    <span id="city_name">南通</span>

    <a href="http://nantong.55tuan.com:80/city_list.jsp" id="g-city-select-show">切换城市</a>

    </div>--%>

	



    <div class="g_head_dy">

    	<div class="g_head_dy_input"><input name="user_email" id="user_email" type="text" class="g_head_dy_inp" value="请输入E-mail地址" /></div>

        <div class="g_head_dy_but">

        <input name="city_id" id="city_id" value="" type="hidden"/>

        <img src="http://p0.55tuan.com/themes/default/images/static/img/page_img110418/email_dy_but.jpg" id="addEmailList" />

        </div>

    </div>

</div>





<!--导航菜单-->

<div class="g_nav_box">

	<div class="g_nav">

    	<div class="g_nav_left"></div>

        <div class="g_logo_bg"></div>

        <div class="g_nav_list">

        	<ul>

                <li id="g-nav-index"><a href="Home.aspx">今日团购</a></li>

                 <%--<li class="g_head_nav_hr"></li>

               <li id="channel-1"><a href="http://lvyou.55tuan.com">旅游</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="channel-2"><a href="http://jiudian.55tuan.com">酒店</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="channel-3"><a href="http://shenghuoguan.55tuan.com">生活馆</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="channel-4"><a href="http://huazhuangpin.55tuan.com">化妆品</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="group-buy"><a href="http://nantong.55tuan.com:80/group-buy">往期团购</a></li>--%>



        <!--登陆-->



            <li class="g_head_login" date-login="befor"><a href="/register">注册</a></li>

            <li class="g_head_login_hr" date-login="befor"></li>

            <li class="g_head_login" date-login="befor"><a href="/user/userLogin.jsp">登录</a></li>



        <!--登陆end-->

                

		<!--登陆后显示-->



			<li class="g_head_login postion" style=" z-index:1000; display:none" date-login="after" ><a href="/loginOut">退出</a>

                <div class="index_box_tx" id="__index_tx" style="top:48px;z-index:100;display:none"><a href="#" class="index_a_close" id="__index_close">关闭</a>您有<span id="unpay-order-num">几</span>个未支付的订单，<a href="/myOrder-1?payStatus=0" class="index_a_xq">点击查看</a></div>

            </li>

            <li class="g_head_login_hr" date-login="after" style="display:none"></li>

            <li class="g_head_login g_head_login_text" id="g_head_login" date-login="after" style="display:none">

                  <a href="/ticket-1" class="g_user_memu" id="login-user-name">用户名</a>

                  <div class="g_head_login_memu" id="g_head_login_memu">

                    	<div class="g_head_memu_box">

                        		<a href="/ticket-1">我的窝窝券</a>

                        		<a href="/myOrder-1">我的订单</a>

                        		<a href="/useraction.do?method=showUserAccountDes">帐号设置</a>

                        		<a href="/friendInvite">我的邀请</a>

                        		<a href="/userAccountMessage">账户余额</a>

                        		<a href="/myComment">我的留言</a>

                        </div>

                  </div>

             </li>



                <!--登陆后显示end-->

            </ul>

        </div>

        <div class="g_nav_right"></div>

    </div>

</div> 

<script type="text/javascript">

(function($){

	$(function(){

		$.ajax({

		    type:"GET",

		    url:"/u/getUserInfoByAjax.do",

		    dataType:"json",

		    success:handleUserInfo

		});

	

		function handleUserInfo(msg)

		{

	            if(0 == msg.flag){

	            	if(msg.name){

	            		$("#login-user-name").html(msg.name);

	            		if(msg.unpay){

	            			$("#unpay-order-num").html(msg.unpay);

	            			$("#__index_tx").show();

	            		}

	            		$("li[date-login='befor']").hide();

	            		$("li[date-login='after']").show();

	            	}

	            }

		}

		

		$("#__index_close").click(function(){

			$("#__index_tx").hide();

		});

	});

})(jQuery);

</script>

<!--头部end-->

		<div class="content">

			<div class="contenter">



				<!--<div class="biaoqian">

					<img

						src="http://p0.55tuan.com/themes/default/images/static/img/banner.gif"

						border="0" usemap="#Map" />



					<map name="Map" id="Map">

						<area shape="rect" coords="889,7,934,26" href="#" />

					</map>

				</div>-->



				<div class="left">

					<div class="xiangxi01" style="background: none;">

						<div class="dashboard">

							<ul>



								<li class="current">

									<a href="http://nantong.55tuan.com:80/businessCustomer/businessCustomerComit.jsp">提供团购信息</a>

									<span></span>



								</li>

								<%--<li>

									<a href="http://nantong.55tuan.com:80/staticpage/aboutFriends.jsp">友情链接</a>

									<span></span>

								</li>--%>



								<!--<li>

									<a href="http://nantong.55tuan.com:80/staticpage/joinUs.jsp">加盟窝窝团</a>



									<span></span>

								</li>-->

								

							</ul>

						</div>

                        <div class="yonghuzx">

						<div class="middle02">

							<form  action="AddDemand.aspx"  method="post">

								<p class="l_p6" style="font-size:16px;">

									<strong>特别欢迎优质商家、淘宝大卖家提供团购信息。</strong>

								</p>

                                

								<table width="90%" cellpadding="0" cellspacing="0">

                                	<tr>

                                    	<td colspan="2" height="50">&nbsp;

                                        

                                        </td>

                                    </tr>



	  								<tr>

										<td width="150" height="30" valign="top" class="l_td">

											您的称呼：								

                                        </td>

	  									<td align="left" valign="top" style="">

											<input name="name" id="nameID" type="text" class="l_in" />

											<span id="tdnameid"></span>

                                            <br>

											<SPAN style="FONT-SIZE: 12px; COLOR: #666666">请填写您的真实姓名，方便今后客服与您核实身份。</SPAN>

										</td>

						      		</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<td height="30" valign="top" class="l_td">

											您的电话：										

                                        </td>

								    	<td align="left" valign="top">

										  	<input name="telnumber" id="telnumberID" type="text" class="l_in" />

											<span id="tdtelnumberid"></span>

                                            <br>

											<SPAN style="FONT-SIZE: 12px; COLOR: #666666">请填写您的直接联系电话，方便今后客服与您核实身份。</SPAN>

										</td>

									</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<td height="30" valign="top"class="l_td" >

											您的其他联系方式：										

                                        </td>

									  	<td align="left" style="">

											<input type="text" name="othernumber" id="othernumberID" class="l_in"/>

											<span id="tdothernumberid"></span>

											<br>

											<SPAN style="FONT-SIZE: 12px; COLOR: #666666">请留下您的手机，QQ，MSN或者电子邮箱，方便联系。</SPAN>

										</td>

									</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>



										<td class="style1">

											城市：										

                                        </td>

						  				<td align="left" class="style2">

		  									<input type="text" name="city" id="city"  />

										</td>



								  </tr>

                                  <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<td height="30" class="l_td">

											贵单位名称：										

                                        </td>

								    	<td align="left">

										  	<input type="text" name="suppliersname" id="suppliersnameID" class="l_in" />

								  		  	<span id="tdsuppliersnameid"></span>



										</td>

									</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<td height="30"class="l_td">

											团购类型：										

                                        </td>

										<td align="left">

											<input type="text" name="grouptype" id="grouptype" />

										</td>



								  	</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<td class="l_td">

										团购内容：

										</td>

										<td align="left">

											<textarea rows="5" cols="60" name="groupcontent"id="groupcontentID" class="l_in2"></textarea>

										<br/>



								 		 	<span id="tdgroupcontentid"></span>

										</td>

									</tr>

                                    <tr>

                                    	<td colspan="2" height="20">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<TD>

										</td>

										<TD align="left" style="PADDING-BOTTOM: 50px; PADDING-TOP: 30px">

										  <input class=allSubmitButton type="submit" value="提交"

												id="submitid" />



										</td>

								  </tr>

								</table>





						  </form>

                          </div>

						</div>

						<div class="bottom"></div>

						<div class="clear"></div>

					</div>

					<div class="clear"></div>

				</div>

                

				<div class="right_xx" style="margin-top:37px;">

					<!--服务中心-->

                    <div class="right_box">

                    <div class="right_top">服务中心</div>

                    <div class="g-fw-zhongxin g-fw-zhongxin-bg">

                        <div class="fw_zhongxin_t">

                        	<span class="fuwu_text_1">400-015-5555</span><br />

                        	<span class="fuwu_text_1">400-101-5555</span><br />

                            <span class="fuwu_text_1">400-696-9555</span><br />

                            <span class="fuwu_text_2 text_2_size">24 小时</span>

                            <span class="fuwu_text_3">为您服务</span>

                        </div>

                        <!--<div class="fw_zhongxin_t fw_zhongxin_t1_bg">

                            <span class="fuwu_text_1 fuwu_text_1_color">400-651-5190</span><br />

                            <span class="fuwu_text_2 text_2_size">9:00 - 18:00</span>

                            <span class="fuwu_text_3">为您服务</span>

                        </div>-->

						

                        <div class="fw_zhongxin_b">

                           <div class="ck_comment"></div>

                            <div class="ly_comment"></div>

                            <div class="clear"></div>

                        </div>

                    </div>

                    <div class="fw_right_bot"></div>

                    </div>

                    <!--服务中心end-->

				</div>

				<div class="clear"></div>



			</div>

			<div class="contenter_bottom"></div>

		</div>



		<!-- <div class="content">

	<div class="contenter">

        	                <div class="left">

            <div class="xiangxi01">

                <div class="middle01">

<form action="/businessCustomerAction.do?method=addBusinessContent" method="post">



<table>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			您的称呼：

		</td>

		<td style="">

			<input name="name" id="nameID" type="text" />

		</td>

		<td id="tdnameid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			您的电话：

		</td>

		<td style="">

			<input name="telnumber" id="telnumberID" type="text" />

		</td>

		<td id="tdtelnumberid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align="right" >

			您的其他联系方式：

		</td>

		<td style="">

			<input type="text" name="othernumber" id="othernumberID" />

			<br>

			<SPAN style="FONT-SIZE: 12px; COLOR: #666666">请留下您的手机，QQ，MSN或者电子邮箱，方便联系。</SPAN>

		</td>

		<td id="tdothernumberid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			城市：

		</td>

		<td>

			<select name="city" >

				<option value="0">全国</option>

				<option value="1" selected="selected" >北京</option>

				<option value="2">上海</option>

				<option value="3">广州</option>

				<option value="4">长沙</option>

			</select>

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			团购商家名称：

		</td>

		<td style="">

			<input type="text" name="suppliersname" id="suppliersnameID" />

		</td>

		<td id="tdsuppliersnameid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			团购类型：

		</td>

		<td>

			<select name="grouptype">

				<option value="1">餐饮美食</option>

				<option value="2">娱乐休闲</option>

				<option value="3">丽人</option>

				<option value="4">健康保健</option>

				<option value="5">运动健身</option>

				<option value="6">电商网店</option>

				<option value="7">其他</option>

			</select>

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			团购内容:

		</td>

		<td style="">

			<textarea rows="5" cols="20" name="groupcontent" id="groupcontentID" ></textarea>

		</td>

		<td id="tdgroupcontentid">

			

		</td>

	</tr>

	<tr>

		<TD>

		</td>

		<TD style="PADDING-BOTTOM: 50px; PADDING-TOP: 30px">

			<input class=buybutton type="submit" value="提交" id="submitid" />

		</td>

	</tr>

</table>



                  

</form>

              </div>

                <div class="di01"></div>

            </div>

            <div class="clear"></div> 

        </div>     

        <div class="clear"></div>

        </div>

</div>-->

		<script type="text/javascript">

	$(document).ready(function(){

		var regType = {

			//座机

			phoneReg : /^(\d{3,4}\-)?[1-9]\d{6,7}$/,

			//手机

			mobileReg : /^1[3,5,8]{1}[0-9]{1}[0-9]{8}$/,

			//带区号的座机

			localReg : /0\d{2,3}-[2-9]\d{6,7}/

		};

		var errorMsg = {

			nameNull : '请填写您的称呼。',

			nameLang : '您输入的名字过长。',

			telNull : '请填写电话号码。',

			telLang : '您输入的电话号码过长。',

			telWrong : '您的电话号码格式不对',

			othertelLang : '您输入的其他号码过长。',

			stringLang : '您输入的字符过长。',

			companyNull : '请填写贵公司名称。',

			groupcontentNull : '请填写团购信息。',

			groupcontentLang : '团购信息过长。'

		};

		var imgStr = '<img src="http://55tuan.com:80/images/img/taken.gif" class="img_align" />'

		var imgCorrect='<img src="http://55tuan.com:80/images/img/available.gif" class="img_align" />'

		$('#nameID').blur(function(){//验证称呼

			var text = this.value;

			if(null == text || "" == text){

				//$('#tdnameid').html('<span>请填写您的称呼。</span>'):

				$('#tdnameid').css("color","#ff6600").html(imgStr+errorMsg.nameNull);

			}else if(text.length > 60){

				$('#tdnameid').css("color","#ff6600").html(imgStr+errorMsg.nameLang);

			}else{

				$('#tdnameid').css("color","#ff6600").html(imgCorrect);

			}

		});

		

		$('#telnumberID').blur(function(){//验证电话

			var text = this.value;

			if(null == text || "" == text){

				$('#tdtelnumberid').css("color","#ff6600").html(imgStr+errorMsg.telNull);

			}else if(!regType.phoneReg.test(text)){

				if(!regType.localReg.test(text)){

					if(!regType.mobileReg.test(text)){

						var str = '<br/>';

						str += '<span style=\'margin-left:220px\'>&clubs;电话格式为：</span><br/>';

						str += '<span style=\'margin-left:230px\'>010-88888888</span><br/>';

						str += '<span style=\'margin-left:230px\'>13888888888</span>';

						$('#tdtelnumberid').css("color","#ff6600").html(imgStr+errorMsg.telWrong+str);

					}else{

						$('#tdtelnumberid').css("color","#ff6600").html(imgCorrect);

					}

				}

			}else if(text.length > 100){

				$('#tdtelnumberid').css("color","#ff6600").html(imgStr+errorMsg.telLang);

			}

		});

		

		$('#othernumberID').blur(function(){//验证其他号码

			var text = this.value;

			if(text.length > 100){

				$('#tdothernumberid').css("color","#ff6600").html(imgStr+errorMsg.othertelLang);

			}else{

			$('#tdothernumberid').css("color","#ff6600").html(imgCorrect);

			}

		});

		

		$('#suppliersnameID').blur(function(){//验证商家名称

			var text = this.value;

			if(null == text || "" == text){

				$('#tdsuppliersnameid').css("color","#ff6600").html(imgStr+errorMsg.companyNull);

			}else if(text.length > 100){

				$('#tdsuppliersnameid').css("color","#ff6600").html(imgStr+errorMsg.stringLang);

			}else{

			$('#tdsuppliersnameid').css("color","#ff6600").html(imgCorrect);

			}

		});

		

		$('#groupcontentID').blur(function(){//验证团购信息

			var text = this.value;

			if(null == text || "" == text){

				$('#tdgroupcontentid').css("color","#ff6600").html(imgStr+errorMsg.groupcontentNull);

			}else if(text.length > 1000){

				$('#tdgroupcontentid').css("color","#ff6600").html(imgStr+errorMsg.groupcontentLang);

			}else{

			$('#tdgroupcontentid').css("color","#ff6600").html(imgCorrect);

			}

		});

		

		$('#submitid').click(function(){

			var name = $('#nameID').val();

			var telnumber = $('#telnumberID').val();

			var othernumber = $('#othernumberID').val();

			var suppliersname= $('#suppliersnameID').val();

			var groupcontent = $('#groupcontentID').val();

			var errorText = "";

			var flag = 0;

			//以下是提交时再验证一次

			if(null == name || "" == name){

				errorText += errorMsg.nameNull + '\n';

				flag += 1;

			}

			if(name.length > 60){

				errorText += errorMsg.nameLang + '\n';

				flag += 1;

			}

			if(null == telnumber || "" == telnumber){

				errorText += errorMsg.telNull + '\n';

				flag += 1;

			}else if(telnumber.length > 100){

				errorText += errorMsg.telLang + '\n';

				flag += 1;

			}else if(!regType.phoneReg.test(telnumber)){

				if(!regType.localReg.test(telnumber)){

					if(!regType.mobileReg.test(telnumber)){

						errorText += errorMsg.telWrong + '\n';

						errorText += '\t电话格式为：\n';

						errorText += '\t\t010-88888888\n';

						errorText += '\t\t13888888888\n';

						flag += 1;

					}

				}

			}

			if(othernumber.length > 100){

				errorText += errorMsg.othertelLang + '\n';

				flag += 1;

			}

			if(null == suppliersname || "" == suppliersname){

				errorText += errorMsg.companyNull + '\n';

				flag += 1;

			}

			if(suppliersname.length > 100){

				errorText += errorMsg.stringLang + '\n';

				flag += 1;

			}

			if(null == groupcontent || "" == groupcontent){

				errorText += errorMsg.groupcontentNull + '\n';

				flag += 1;

			}

			if(groupcontent.length>1000){

				errorText += errorMsg.groupcontentLang + '\n';

				flag += 1;

			}

			if(flag > 0){

				alert(errorText);

				return false;

			}

		});

	});

</script>



		

<div class="bottom01">

<div class="bottom">

	<div class="fkyj"><!--<a href="userComment.do?method=getUserComment" target="_blank">问题反馈</a>--></div>

	<div class="dibuwz">

		<div class="us" style=" background-image: url(about:blank);">

			<ul>

				<li><strong>用户帮助</strong></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutHow.jsp">玩转窝窝团</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutFaq.jsp">常见问题</a></li>

                <li class="grey"><a href="http://www.55tuan.com/seckill/secKillRule.jsp">秒杀规则</a></li>

                <li class="grey"><a href="http://www.55tuan.com/guarantee.do">消费者保障</a></li>

				<li class="grey"><a href="http://www.55tuan.com/myOrder-1">退款</a></li>

				

                <!--<li class="grey"><a href="http://www.55tuan.com/staticpage/about_serviceCentre.jsp">退换货服务</a></li>-->

                <li class="grey"><a href="http://www.55tuan.com/sitemap.jsp">网站地图</a></li>

			</ul>

	    </div>

	  	<div class="us">

			<ul>

				<li><strong>获取更新</strong></li>

				<li class="grey"><a target="_blank" href="http://t.sina.com.cn/2010919247">窝窝团新浪微博</a></li>

        <li class="grey"><a target="_blank" href="http://www.kaixin001.com/home/?uid=79670526">窝窝团开心网主页</a></li>

        <li class="grey"><a target="_blank" href="http://www.douban.com/group/225837/">窝窝团豆瓣小组</a></li>

				<li class="grey"><a href="http://www.55tuan.com/feed-group-buy.xml">RSS订阅</a></li>

			</ul>

	    </div>

	  	<div class="us">

			<ul>

				<li><strong>商务合作</strong></li>

				<li class="grey"><a href="http://www.55tuan.com/businessCustomer/businessCustomerComit.jsp">提供团购信息</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutFriends.jsp">友情链接</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutAPI.jsp">开放API</a></li>

                <!--<li class="grey"><a href="http://www.55tuan.com/staticpage/joinUs.jsp">加盟窝窝团</a></li>

			-->

			</ul>

	  	</div>

	  	<div class="us">

			<ul>

				<li><strong>公司信息</strong></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutUs.jsp">关于我们</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutPress.jsp">媒体报道</a></li>

				<li class="grey"><a href="http://www.55tuan.com/job">加入我们</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutPrivacy.jsp">隐私声明</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/wowoUserContract.jsp">用户协议</a></li>

			</ul>

  	   </div>

	  </div>

	<div class="banquan">&#169; 2010-2011 55tuan.com 京ICP证100702号 京公海网安备110108000829号</div>

    <div class="xinren_img">

        <img src="http://p0.55tuan.com/themes/default/images/static/img/zhifubao_teyueshangjia.jpg" width="120" height="46">

        <a href="http://www.sinocredit.net/zhtml/tags/BCP34857719.html " target="_blank"><img src="http://s0.55tuanimg.com/themes/default/images/static/img/renzheng.gif" width="120" height="46"></a>

        <a href="http://www.315online.com.cn/member/315110004.html" target="_blank"><img src="http://p0.55tuan.com/themes/default/images/static/img/jiaoyibaozhangzhongxin.gif" width="120" height="46"></a>

        <!--可信网站图片LOGO安装开始-->

		<a target="_blank" href="https://ss.cnnic.cn/verifyseal.dll?sn=2011012800100006440&ct=df&pa=822767"><img src="http://p0.55tuan.com/themes/default/images/static/img/page_img110418/kexin.jpg" width="128" height="47" border="0"/></a>

        <!--可信网站图片LOGO安装结束-->

        <a href="http://www.itrust.org.cn/yz/pjwx.asp?wm=1675710238" target="_blank"><img src="http://p0.55tuan.com/themes/default/images/static/img/page_img110418/wangzhanxinyonglianghao.jpg" width="120" height="46"></a>

    </div>

</div>



</div>



<!--订阅成功-->

<div class="g-dingdan-del-msg" id="dingyu_ok">

	<div class="g-dingdan-del-msg-t">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

    <div class="g-dingdan-del-msg-c g-dingdan-del-msg-cdy">



		<p class="p_dyok">订阅成功</p>

        <p class="p_dylx">

        	订阅邮件：<span id ="spEmail"></span><br>

            订阅城市：南通

        </p>

        <a href="#" class="a_dy" id="dingyue_okclose" style="margin-top:20px;" title="关闭">确认</a>



	</div>

    <div class="g-dingdan-del-msg-b">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

</div>

<!--ending-->



<!--订阅失败-->

<div class="g-dingdan-del-msg" id="dingyu_no">



	<div class="g-dingdan-del-msg-t">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

    <div class="g-dingdan-del-msg-c g-dingdan-del-msg-cdy">

		<p class="p_dyok p_dyno">订阅失败</p>

        <p class="p_dynts">



        	提示：邮箱格式不正确或者没有输入订阅邮件地址，请输入正确邮箱后再订阅。

        </p>

        <a href="#" class="a_dy" id="dingyue_noclose" title="关闭">确认</a>

	</div>

    <div class="g-dingdan-del-msg-b">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>



</div>

<!--ending-->

<!--订阅重复-->

<div class="g-dingdan-del-msg" id="dingyu_agin">

	<div class="g-dingdan-del-msg-t">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

    <div class="g-dingdan-del-msg-c g-dingdan-del-msg-cdy">

		<p class="p_dyok p_dyno">订阅重复</p>



        <p class="p_dynts">

        	提示<span id ="spRepeatEmail"></span>已订阅窝窝团购信息，无需重新订阅！

        </p>

        <a href="#" class="a_dy" id="dingyu_aginclose" title="关闭">确认</a>

	</div>

    <div class="g-dingdan-del-msg-b">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>



    </div>

</div>

<!--ending-->

<script type="text/javascript">

$(function(){

	var pindaoID = window.location.search;

	//承诺 化妆

	if (pindaoID != ""){

		var _pindaoID = pindaoID.split("=");

		if (_pindaoID.length>0){

			if (parseInt(_pindaoID[1])==4 && _pindaoID[0] == "?channel"){

				$("#chengnuo").show();

			}else{

				$("#huazhuang").show();

			};

		};

	}else{

		$("#huazhuang").show();

	};

	//频道

	var $url=window.location.href;

	if ($url.indexOf("group-buy")>=0 || $url.indexOf("selPastGoods")>=0){

		var $html = $("<div class=\"g-nav-ico\"></div>").css("left","44%");

		$("#group-buy").addClass("g-nav-ico-bg").append($html);

		return;

	};

	if ($url.indexOf("channel=1")>=0 || $url.indexOf("lvyou")>=0){

		var $html = $("<div class=\"g-nav-ico\"></div>").css("left","40%");

		$("#channel-1").addClass("g-nav-ico-bg").append($html);

		return;

	};

	if ($url.indexOf("channel=2")>=0 || $url.indexOf("jiudian")>=0){

		var $html = $("<div class=\"g-nav-ico\"></div>").css("left","40%");

		$("#channel-2").addClass("g-nav-ico-bg").append($html);

		return;

	};

	if ($url.indexOf("channel=3")>=0 || $url.indexOf("shenghuoguan")>=0){

		var $html = $("<div class=\"g-nav-ico\"></div>").css("left","43%");

		$("#channel-3").addClass("g-nav-ico-bg").append($html);

		return;

	};

	if ($url.indexOf("channel=4")>=0 || $url.indexOf("huazhuangpin")>=0){

		var $html = $("<div class=\"g-nav-ico\"></div>").css("left","43%");

		$("#channel-4").addClass("g-nav-ico-bg").append($html);

		return;

	};

	

	var $html = $("<div class=\"g-nav-ico\"></div>").css("left","44%");

	$("#g-nav-index").addClass("g-nav-ico-bg").append($html);

	return;

	

	



	

});

</script>

<script type="text/javascript" src="http://js.tongji.linezing.com/2323244/tongji.js"></script>

	</body>

</html>
