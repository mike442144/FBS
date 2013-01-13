<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShareVideo.aspx.cs" Inherits="FBS.Web.News.Admin.Pages.Video.AddShareVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加视频</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
   <link href="../../../css/sharewindow.css" rel="Stylesheet" type="text/css" /> 

   <script type="text/javascript">
       function analyVideo() {
           if (document.getElementById("input_box").value == "") {
               alert("请输入视频链接！")
           }else if(document.getElementById("videoname").value==""){
           alert("请输入视频名称！")
           }else{
           form1.submit();
           }
           
       }
   </script>
</head>





<body>
    <form id="form1" runat="server" action="AddShareVideo.aspx">
    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td width="17" valign="top" background="images/mail_leftbg.gif">
            <img src="../../images/left-top-right.gif" width="17" height="29"/>
		</td>
		<td valign="top" background="images/content-bg.gif">
		<table width="100%" height="31" border="0" cellpadding="0" cellspacing="0" class="left_topbg">
			<tr>
				<td height="31">
				<div class="titlebt">
					                    视频添加</div>
				</td>
			</tr>
		</table>
		</td>
		<td width="16" valign="top" background="images/mail_rightbg.gif">
		<img src="../../images/nav-right-bg.gif" width="16" height="29" /></td>
	</tr>
	<tr>
		<td valign="middle" background="../../images/mail_leftbg.gif">&nbsp;</td>
		<td valign="top" bgcolor="#F7F8F9" align="center" class="list1">
  
    <div>
    <div class="share_container">
        <div class="comment_title">
        输入视频网站播放页链接地址
            
        </div>
        <div class="remaind">目前已经支持优酷网视频</div>
        
            <input type="text" name="input_box" id="input_box" />
       <div class="remaind">视频名称</div>
       
       <input type="text" name="videoname" id="videoname" />
            <div class="comment">
                <div class="comment_title">说说你的看法：</div>
                <textarea id="subject" name="subject" cols="20" rows="1"></textarea>
            </div>
            
            <div class="inputs">
                <input type="button" value="发布" class="share_button" onclick="analyVideo()" />
                <input type="button" value="取消" class="cancle_button" onclick="self.close()" />
            </div>
    
    </div>
    </div>
                </td>
		<td background="../images/mail_rightbg.gif">&nbsp;</td>
	</tr>
	<tr>
		<td valign="bottom" background="../images/mail_leftbg.gif">
            <img src="../../images/buttom_left2.gif" width="17" height="17" />
	</td>
		
		
		<td valign="bottom" background="../images/mail_rightbg.gif">
		<img src="../../images/buttom_bgs.gif" style="width: 100%; height: 16px" /></td>
	</tr>
	</table>
    
    
    
    </form>
    
    

</body>
</html>

