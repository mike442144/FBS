<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="../../Content/Scripts/jquery-1.6.2.min.js"></script>
<script type="text/javascript">
    
    function getThemeJson() {
        var loadbox = document.getElementById("loading");
        var themesbox = document.getElementById("themes");
        //alert(loadbox);
        $.ajax({ type: "POST",
            url: "/Site/FetchThemes?ts=" + new Date().getTime(),
            data: "",
            beforeSend: function() {
                loadbox.style.display = "block";
            },
            complete: function() {
                loadbox.style.display = "none";
            },
            error: function(data) { alert("json loaded error"); },
            success: function(data) {
                if (data) {
                    //alert(data.length);
                    for (var i = 0; i < data.length; i++) {
                        //一个皮肤一个div
                        var div = document.createElement("div");
                        div.setAttribute("name", data[i].Name); //设置div的name属性
                        div.setAttribute("id", data[i].Name); //设置div的id属性
                        //新建皮肤的各个属性div

                        //名称
                        var name = document.createElement("label");
                        var ntxt = document.createTextNode(data[i].Name);
                        //name.appendChild(data[i].Name);
                        name.appendChild(ntxt);
                        div.appendChild(name);
                        //缩略图
                        var thumb = document.createElement("img");
                        thumb.setAttribute("src", data[i].SmallThumbnail);
                        div.appendChild(thumb);

                        //描述
                        var desc = document.createElement("p");
                        var dtxt = document.createTextNode(data[i].Description);
                        //desc.appendChild(data[i].Description);
                        desc.appendChild(dtxt);
                        div.appendChild(desc);

                        //作者
                        var author = document.createElement("p");
                        var atxt = document.createTextNode(data[i].Author);
                        //author.appendChild(data[i].Author);
                        author.appendChild(atxt);
                        div.appendChild(author);

                        //发布日期
                        var pubDate = document.createElement("p");
                        var ptxt = document.createTextNode(data[i].PubDate);
                        //pubDate.appendChild(data[i].PubDate);
                        pubDate.appendChild(ptxt);
                        div.appendChild(pubDate);

                        themesbox.appendChild(div);    
                    }
                    
                }
                else if (data == "end") {


                }
                else {

                }
            }
        });
    }
</script>

<div id="themes">
</div>
