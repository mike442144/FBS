<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDemand.aspx.cs" Inherits="Aviator.Web.Admin.Pages.DemandPages.AddDemand" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<!--Updated by Tian Ze on 28th Feb 2011; add on table businesscustomer_content  -->
<html>	<head>	<title>����Ź���Ϣ</title>
		<META http-equiv="Content-Type" content="text/html; charset=gbk">

<META content="������ ������ �����Ź� ���� 55tuan wowo �����Ź��� �Ź� �����Ź� �Ϻ��Ź� �����Ź� �����Ź� �����Ź� �ɶ��Ź� ֣���Ź� �����Ź� �人�Ź� ����Ź� �����Ź� �Ͼ��Ź� �����Ź� �ൺ�Ź� �Ź���վ��ȫ �Ź���ȫ �Ź����� �Ź��� �Ź�ָ�� �Ź����� ���� �Ż� �ۿ� ���� �Ż�ȯ �ֿ�ȯ ����ȯ ��ӰƱ Ʊ�� ��� ������ ���� ��ʳ ʳƷ ���� ���� ��ױƷ �٤ ĸӤ ���� ���� ���� ���� �ͷ��绰 4006969555" name="Keywords">
<META content="�������Ź���վ�������ɫ�����ѷ����Ź�ƽ̨��ÿ���Ƴ������Ź����ṩ�ڶ����ܻ�ӭ���Ź����Ѳ�Ʒ�ͷ�����Ŀ���Ź���������������ʳ��Ʊ��������������ױƷ������Ʒ��ĸӤ�����֡����Ρ���������ȷ������棬���ǲ������ưɡ�KTV��������SPA������Ժ�������ꡢ�٤�ݡ���Ȫ���Ƶꡢ���ֳ���������Ȧ�ȳ����������Ź������������š��ҵ��Ź������ţ��Ź����������š�" name="Description">
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

        <div class="g_head_logo_ab"><a href="http://www.55tuan.com/">������logo</a></div>

    </div>

    

  <%--  <div class="g_head_logo_test"></div>

    <div class="g-head-city-select-but">

    <span id="city_name">��ͨ</span>

    <a href="http://nantong.55tuan.com:80/city_list.jsp" id="g-city-select-show">�л�����</a>

    </div>--%>

	



    <div class="g_head_dy">

    	<div class="g_head_dy_input"><input name="user_email" id="user_email" type="text" class="g_head_dy_inp" value="������E-mail��ַ" /></div>

        <div class="g_head_dy_but">

        <input name="city_id" id="city_id" value="" type="hidden"/>

        <img src="http://p0.55tuan.com/themes/default/images/static/img/page_img110418/email_dy_but.jpg" id="addEmailList" />

        </div>

    </div>

</div>





<!--�����˵�-->

<div class="g_nav_box">

	<div class="g_nav">

    	<div class="g_nav_left"></div>

        <div class="g_logo_bg"></div>

        <div class="g_nav_list">

        	<ul>

                <li id="g-nav-index"><a href="Home.aspx">�����Ź�</a></li>

                 <%--<li class="g_head_nav_hr"></li>

               <li id="channel-1"><a href="http://lvyou.55tuan.com">����</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="channel-2"><a href="http://jiudian.55tuan.com">�Ƶ�</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="channel-3"><a href="http://shenghuoguan.55tuan.com">�����</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="channel-4"><a href="http://huazhuangpin.55tuan.com">��ױƷ</a></li>

                <li class="g_head_nav_hr"></li>

                <li id="group-buy"><a href="http://nantong.55tuan.com:80/group-buy">�����Ź�</a></li>--%>



        <!--��½-->



            <li class="g_head_login" date-login="befor"><a href="/register">ע��</a></li>

            <li class="g_head_login_hr" date-login="befor"></li>

            <li class="g_head_login" date-login="befor"><a href="/user/userLogin.jsp">��¼</a></li>



        <!--��½end-->

                

		<!--��½����ʾ-->



			<li class="g_head_login postion" style=" z-index:1000; display:none" date-login="after" ><a href="/loginOut">�˳�</a>

                <div class="index_box_tx" id="__index_tx" style="top:48px;z-index:100;display:none"><a href="#" class="index_a_close" id="__index_close">�ر�</a>����<span id="unpay-order-num">��</span>��δ֧���Ķ�����<a href="/myOrder-1?payStatus=0" class="index_a_xq">����鿴</a></div>

            </li>

            <li class="g_head_login_hr" date-login="after" style="display:none"></li>

            <li class="g_head_login g_head_login_text" id="g_head_login" date-login="after" style="display:none">

                  <a href="/ticket-1" class="g_user_memu" id="login-user-name">�û���</a>

                  <div class="g_head_login_memu" id="g_head_login_memu">

                    	<div class="g_head_memu_box">

                        		<a href="/ticket-1">�ҵ�����ȯ</a>

                        		<a href="/myOrder-1">�ҵĶ���</a>

                        		<a href="/useraction.do?method=showUserAccountDes">�ʺ�����</a>

                        		<a href="/friendInvite">�ҵ�����</a>

                        		<a href="/userAccountMessage">�˻����</a>

                        		<a href="/myComment">�ҵ�����</a>

                        </div>

                  </div>

             </li>



                <!--��½����ʾend-->

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

<!--ͷ��end-->

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

									<a href="http://nantong.55tuan.com:80/businessCustomer/businessCustomerComit.jsp">�ṩ�Ź���Ϣ</a>

									<span></span>



								</li>

								<%--<li>

									<a href="http://nantong.55tuan.com:80/staticpage/aboutFriends.jsp">��������</a>

									<span></span>

								</li>--%>



								<!--<li>

									<a href="http://nantong.55tuan.com:80/staticpage/joinUs.jsp">����������</a>



									<span></span>

								</li>-->

								

							</ul>

						</div>

                        <div class="yonghuzx">

						<div class="middle02">

							<form  action="AddDemand.aspx"  method="post">

								<p class="l_p6" style="font-size:16px;">

									<strong>�ر�ӭ�����̼ҡ��Ա��������ṩ�Ź���Ϣ��</strong>

								</p>

                                

								<table width="90%" cellpadding="0" cellspacing="0">

                                	<tr>

                                    	<td colspan="2" height="50">&nbsp;

                                        

                                        </td>

                                    </tr>



	  								<tr>

										<td width="150" height="30" valign="top" class="l_td">

											���ĳƺ���								

                                        </td>

	  									<td align="left" valign="top" style="">

											<input name="name" id="nameID" type="text" class="l_in" />

											<span id="tdnameid"></span>

                                            <br>

											<SPAN style="FONT-SIZE: 12px; COLOR: #666666">����д������ʵ������������ͷ�������ʵ��ݡ�</SPAN>

										</td>

						      		</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<td height="30" valign="top" class="l_td">

											���ĵ绰��										

                                        </td>

								    	<td align="left" valign="top">

										  	<input name="telnumber" id="telnumberID" type="text" class="l_in" />

											<span id="tdtelnumberid"></span>

                                            <br>

											<SPAN style="FONT-SIZE: 12px; COLOR: #666666">����д����ֱ����ϵ�绰��������ͷ�������ʵ��ݡ�</SPAN>

										</td>

									</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>

										<td height="30" valign="top"class="l_td" >

											����������ϵ��ʽ��										

                                        </td>

									  	<td align="left" style="">

											<input type="text" name="othernumber" id="othernumberID" class="l_in"/>

											<span id="tdothernumberid"></span>

											<br>

											<SPAN style="FONT-SIZE: 12px; COLOR: #666666">�����������ֻ���QQ��MSN���ߵ������䣬������ϵ��</SPAN>

										</td>

									</tr>

                                    <tr>

                                    	<td colspan="2" height="40">&nbsp;

                                        

                                        </td>

                                    </tr>

									<tr>



										<td class="style1">

											���У�										

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

											��λ���ƣ�										

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

											�Ź����ͣ�										

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

										�Ź����ݣ�

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

										  <input class=allSubmitButton type="submit" value="�ύ"

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

					<!--��������-->

                    <div class="right_box">

                    <div class="right_top">��������</div>

                    <div class="g-fw-zhongxin g-fw-zhongxin-bg">

                        <div class="fw_zhongxin_t">

                        	<span class="fuwu_text_1">400-015-5555</span><br />

                        	<span class="fuwu_text_1">400-101-5555</span><br />

                            <span class="fuwu_text_1">400-696-9555</span><br />

                            <span class="fuwu_text_2 text_2_size">24 Сʱ</span>

                            <span class="fuwu_text_3">Ϊ������</span>

                        </div>

                        <!--<div class="fw_zhongxin_t fw_zhongxin_t1_bg">

                            <span class="fuwu_text_1 fuwu_text_1_color">400-651-5190</span><br />

                            <span class="fuwu_text_2 text_2_size">9:00 - 18:00</span>

                            <span class="fuwu_text_3">Ϊ������</span>

                        </div>-->

						

                        <div class="fw_zhongxin_b">

                           <div class="ck_comment"></div>

                            <div class="ly_comment"></div>

                            <div class="clear"></div>

                        </div>

                    </div>

                    <div class="fw_right_bot"></div>

                    </div>

                    <!--��������end-->

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

			���ĳƺ���

		</td>

		<td style="">

			<input name="name" id="nameID" type="text" />

		</td>

		<td id="tdnameid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			���ĵ绰��

		</td>

		<td style="">

			<input name="telnumber" id="telnumberID" type="text" />

		</td>

		<td id="tdtelnumberid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align="right" >

			����������ϵ��ʽ��

		</td>

		<td style="">

			<input type="text" name="othernumber" id="othernumberID" />

			<br>

			<SPAN style="FONT-SIZE: 12px; COLOR: #666666">�����������ֻ���QQ��MSN���ߵ������䣬������ϵ��</SPAN>

		</td>

		<td id="tdothernumberid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			���У�

		</td>

		<td>

			<select name="city" >

				<option value="0">ȫ��</option>

				<option value="1" selected="selected" >����</option>

				<option value="2">�Ϻ�</option>

				<option value="3">����</option>

				<option value="4">��ɳ</option>

			</select>

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			�Ź��̼����ƣ�

		</td>

		<td style="">

			<input type="text" name="suppliersname" id="suppliersnameID" />

		</td>

		<td id="tdsuppliersnameid">

			

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			�Ź����ͣ�

		</td>

		<td>

			<select name="grouptype">

				<option value="1">������ʳ</option>

				<option value="2">��������</option>

				<option value="3">����</option>

				<option value="4">��������</option>

				<option value="5">�˶�����</option>

				<option value="6">��������</option>

				<option value="7">����</option>

			</select>

		</td>

	</tr>

	<tr>

		<td style="PADDING-RIGHT: 15px; FONT-SIZE: 14px; " align=right>

			�Ź�����:

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

			<input class=buybutton type="submit" value="�ύ" id="submitid" />

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

			//����

			phoneReg : /^(\d{3,4}\-)?[1-9]\d{6,7}$/,

			//�ֻ�

			mobileReg : /^1[3,5,8]{1}[0-9]{1}[0-9]{8}$/,

			//�����ŵ�����

			localReg : /0\d{2,3}-[2-9]\d{6,7}/

		};

		var errorMsg = {

			nameNull : '����д���ĳƺ���',

			nameLang : '����������ֹ�����',

			telNull : '����д�绰���롣',

			telLang : '������ĵ绰���������',

			telWrong : '���ĵ绰�����ʽ����',

			othertelLang : '��������������������',

			stringLang : '��������ַ�������',

			companyNull : '����д��˾���ơ�',

			groupcontentNull : '����д�Ź���Ϣ��',

			groupcontentLang : '�Ź���Ϣ������'

		};

		var imgStr = '<img src="http://55tuan.com:80/images/img/taken.gif" class="img_align" />'

		var imgCorrect='<img src="http://55tuan.com:80/images/img/available.gif" class="img_align" />'

		$('#nameID').blur(function(){//��֤�ƺ�

			var text = this.value;

			if(null == text || "" == text){

				//$('#tdnameid').html('<span>����д���ĳƺ���</span>'):

				$('#tdnameid').css("color","#ff6600").html(imgStr+errorMsg.nameNull);

			}else if(text.length > 60){

				$('#tdnameid').css("color","#ff6600").html(imgStr+errorMsg.nameLang);

			}else{

				$('#tdnameid').css("color","#ff6600").html(imgCorrect);

			}

		});

		

		$('#telnumberID').blur(function(){//��֤�绰

			var text = this.value;

			if(null == text || "" == text){

				$('#tdtelnumberid').css("color","#ff6600").html(imgStr+errorMsg.telNull);

			}else if(!regType.phoneReg.test(text)){

				if(!regType.localReg.test(text)){

					if(!regType.mobileReg.test(text)){

						var str = '<br/>';

						str += '<span style=\'margin-left:220px\'>&clubs;�绰��ʽΪ��</span><br/>';

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

		

		$('#othernumberID').blur(function(){//��֤��������

			var text = this.value;

			if(text.length > 100){

				$('#tdothernumberid').css("color","#ff6600").html(imgStr+errorMsg.othertelLang);

			}else{

			$('#tdothernumberid').css("color","#ff6600").html(imgCorrect);

			}

		});

		

		$('#suppliersnameID').blur(function(){//��֤�̼�����

			var text = this.value;

			if(null == text || "" == text){

				$('#tdsuppliersnameid').css("color","#ff6600").html(imgStr+errorMsg.companyNull);

			}else if(text.length > 100){

				$('#tdsuppliersnameid').css("color","#ff6600").html(imgStr+errorMsg.stringLang);

			}else{

			$('#tdsuppliersnameid').css("color","#ff6600").html(imgCorrect);

			}

		});

		

		$('#groupcontentID').blur(function(){//��֤�Ź���Ϣ

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

			//�������ύʱ����֤һ��

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

						errorText += '\t�绰��ʽΪ��\n';

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

	<div class="fkyj"><!--<a href="userComment.do?method=getUserComment" target="_blank">���ⷴ��</a>--></div>

	<div class="dibuwz">

		<div class="us" style=" background-image: url(about:blank);">

			<ul>

				<li><strong>�û�����</strong></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutHow.jsp">��ת������</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutFaq.jsp">��������</a></li>

                <li class="grey"><a href="http://www.55tuan.com/seckill/secKillRule.jsp">��ɱ����</a></li>

                <li class="grey"><a href="http://www.55tuan.com/guarantee.do">�����߱���</a></li>

				<li class="grey"><a href="http://www.55tuan.com/myOrder-1">�˿�</a></li>

				

                <!--<li class="grey"><a href="http://www.55tuan.com/staticpage/about_serviceCentre.jsp">�˻�������</a></li>-->

                <li class="grey"><a href="http://www.55tuan.com/sitemap.jsp">��վ��ͼ</a></li>

			</ul>

	    </div>

	  	<div class="us">

			<ul>

				<li><strong>��ȡ����</strong></li>

				<li class="grey"><a target="_blank" href="http://t.sina.com.cn/2010919247">����������΢��</a></li>

        <li class="grey"><a target="_blank" href="http://www.kaixin001.com/home/?uid=79670526">�����ſ�������ҳ</a></li>

        <li class="grey"><a target="_blank" href="http://www.douban.com/group/225837/">�����Ŷ���С��</a></li>

				<li class="grey"><a href="http://www.55tuan.com/feed-group-buy.xml">RSS����</a></li>

			</ul>

	    </div>

	  	<div class="us">

			<ul>

				<li><strong>�������</strong></li>

				<li class="grey"><a href="http://www.55tuan.com/businessCustomer/businessCustomerComit.jsp">�ṩ�Ź���Ϣ</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutFriends.jsp">��������</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutAPI.jsp">����API</a></li>

                <!--<li class="grey"><a href="http://www.55tuan.com/staticpage/joinUs.jsp">����������</a></li>

			-->

			</ul>

	  	</div>

	  	<div class="us">

			<ul>

				<li><strong>��˾��Ϣ</strong></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutUs.jsp">��������</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutPress.jsp">ý�屨��</a></li>

				<li class="grey"><a href="http://www.55tuan.com/job">��������</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/aboutPrivacy.jsp">��˽����</a></li>

				<li class="grey"><a href="http://www.55tuan.com/staticpage/wowoUserContract.jsp">�û�Э��</a></li>

			</ul>

  	   </div>

	  </div>

	<div class="banquan">&#169; 2010-2011 55tuan.com ��ICP֤100702�� ������������110108000829��</div>

    <div class="xinren_img">

        <img src="http://p0.55tuan.com/themes/default/images/static/img/zhifubao_teyueshangjia.jpg" width="120" height="46">

        <a href="http://www.sinocredit.net/zhtml/tags/BCP34857719.html " target="_blank"><img src="http://s0.55tuanimg.com/themes/default/images/static/img/renzheng.gif" width="120" height="46"></a>

        <a href="http://www.315online.com.cn/member/315110004.html" target="_blank"><img src="http://p0.55tuan.com/themes/default/images/static/img/jiaoyibaozhangzhongxin.gif" width="120" height="46"></a>

        <!--������վͼƬLOGO��װ��ʼ-->

		<a target="_blank" href="https://ss.cnnic.cn/verifyseal.dll?sn=2011012800100006440&ct=df&pa=822767"><img src="http://p0.55tuan.com/themes/default/images/static/img/page_img110418/kexin.jpg" width="128" height="47" border="0"/></a>

        <!--������վͼƬLOGO��װ����-->

        <a href="http://www.itrust.org.cn/yz/pjwx.asp?wm=1675710238" target="_blank"><img src="http://p0.55tuan.com/themes/default/images/static/img/page_img110418/wangzhanxinyonglianghao.jpg" width="120" height="46"></a>

    </div>

</div>



</div>



<!--���ĳɹ�-->

<div class="g-dingdan-del-msg" id="dingyu_ok">

	<div class="g-dingdan-del-msg-t">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

    <div class="g-dingdan-del-msg-c g-dingdan-del-msg-cdy">



		<p class="p_dyok">���ĳɹ�</p>

        <p class="p_dylx">

        	�����ʼ���<span id ="spEmail"></span><br>

            ���ĳ��У���ͨ

        </p>

        <a href="#" class="a_dy" id="dingyue_okclose" style="margin-top:20px;" title="�ر�">ȷ��</a>



	</div>

    <div class="g-dingdan-del-msg-b">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

</div>

<!--ending-->



<!--����ʧ��-->

<div class="g-dingdan-del-msg" id="dingyu_no">



	<div class="g-dingdan-del-msg-t">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

    <div class="g-dingdan-del-msg-c g-dingdan-del-msg-cdy">

		<p class="p_dyok p_dyno">����ʧ��</p>

        <p class="p_dynts">



        	��ʾ�������ʽ����ȷ����û�����붩���ʼ���ַ����������ȷ������ٶ��ġ�

        </p>

        <a href="#" class="a_dy" id="dingyue_noclose" title="�ر�">ȷ��</a>

	</div>

    <div class="g-dingdan-del-msg-b">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>



</div>

<!--ending-->

<!--�����ظ�-->

<div class="g-dingdan-del-msg" id="dingyu_agin">

	<div class="g-dingdan-del-msg-t">

    	<span class="l"></span>

        <span class="c"></span>

        <span class="r"></span>

    </div>

    <div class="g-dingdan-del-msg-c g-dingdan-del-msg-cdy">

		<p class="p_dyok p_dyno">�����ظ�</p>



        <p class="p_dynts">

        	��ʾ<span id ="spRepeatEmail"></span>�Ѷ��������Ź���Ϣ���������¶��ģ�

        </p>

        <a href="#" class="a_dy" id="dingyu_aginclose" title="�ر�">ȷ��</a>

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

	//��ŵ ��ױ

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

	//Ƶ��

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
