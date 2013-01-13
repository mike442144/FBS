<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="FBS.Web.News.Admin.AddRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加角色</title>
    <link  href="../../css/right.css" rel="Stylesheet" type="text/css"/>
    <style type="text/css">
        .style1
        {
            width: 315px;
            font-size:14px;
        }
    </style>
</head>





<body>
    <form id="form2" runat="server">
    
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
					                    添加角色</div>
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
    <div>
    <table>
    
    <tr>
    <td class="style1">角色名称：</td><td class="style1"><input type=text id="TB_name" name="TB_name" class="input"/>
                   
                    </td>
    </tr>
    <tr>
    <td class="style1">用户拥有的操作：</td>
    <td class="style1">
         <%
              System.Xml.XmlNodeList root =BindControl();
              int k=0;
              for (; k < root.Count; k++)
              { 
              %>
              <input type="checkbox" name="<%="CHB"+ k.ToString() %>" id="<%="CHB"+ k.ToString() %>"  value="<%=root[k].Attributes[0].Value %>"  /><%= root[k].Attributes[1].Value %>
              <%
                  
              }
              
                   %>
                  
                    </td>
    </tr>
    <tr> 
    <td>
        <input type="submit" value="创建角色" class="button"/>
    </td>
    <td>
        &nbsp;</td>
    </tr>
    </table>
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

