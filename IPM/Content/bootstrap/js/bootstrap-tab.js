var addTabs = function (options) {
    //var rand = Math.random().toString();
    //var id = rand.substring(rand.indexOf('.') + 1);
    var url = window.location.protocol + '//' + window.location.host;
    options.url = url + options.url;
    id = "tab_" + options.id;
    $(".active").removeClass("active");
    //如果TAB不存在，创建一个新的TAB
    if (!$("#" + id)[0]) {
        //固定TAB中IFRAME高度
        mainHeight = $(window).height() - 120;
        //创建新TAB的title
        title = '<li role="presentation" id="tab_' + id + '" tabAllClose="' + id + '"><a href="#' + id + '" aria-controls="' + id + '" role="tab" data-toggle="tab">' + options.title;
        //是否允许关闭
        if (options.close) {
            title += ' <i class="glyphicon glyphicon-remove" tabclose="' + id + '"></i>';
        }
        title += '</a></li>';
        //是否指定TAB内容
        if (options.content) {
            content = '<div role="tabpanel" class="tab-pane" id="' + id + '">' + options.content + '</div>';
        } else {//没有内容，使用IFRAME打开链接
            content = '<div role="tabpanel" class="tab-pane" id="' + id + '"><div class="tab-loading alert alert-info" role="alert"><h4><span class="glyphicon glyphicon-off" aria-hidden="true"></span> Loading......<h4></div><iframe src="' + options.url + '" width="100%" height="' + mainHeight +
                    '" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="yes" allowtransparency="yes"></iframe></div>';
        }
        //加入TABS
        $(".nav-tabs").append(title);
        $(".tab-content").append(content);
        $("#" + id).find('iframe').bind('load',function () {
            $(".tab-loading").hide();
        });
    }
    //激活TAB
    $("#" + id).addClass("active");
    $("#tab_" + id).addClass("active");
};
var closeTab = function (id) {
    //如果关闭的是当前激活的TAB，激活他的前一个TAB
    if ($("li.active").attr('id') == "tab_" + id) {
        $("#tab_" + id).prev().addClass('active');
        $("#" + id).prev().addClass('active');
    }
    //关闭TAB
    $("#tab_" + id).remove();
    $("#" + id).remove();
};
var closeActiveTab = function () {
    if ($(".nav-tabs li.active").prev().length === 0) {
        return;
    }
    var id = $(".nav-tabs li.active").attr('id');
    var content_id = id.substring(4, id.length);

    $("#" + id).prev().addClass('active');
    $("#" + content_id).prev().addClass('active');

    $("#" + id).remove();
    $("#" + content_id).remove();  
}
var closeAllTab = function () {
    $.each($(".nav-tabs li"), function (i, t) {
        if (i > 0) {
            var id = $(t).attr('id');
            var content_id = id.substring(4, id.length);
            $(t).remove();
            $("#" + content_id).remove();
        } else {
            var id = $(t).attr('id');

            $(t).addClass('active');
            //$("#Index").addClass('active');
        }
    });
}
var closeOtherTab = function () {
    var activeId = $(".nav-tabs li.active").attr('id');
    $.each($(".nav-tabs li"), function (i, t) {
        if (i > 0) {            
            var id = $(t).attr('id');
            if (id != activeId) {
                var content_id = id.substring(4, id.length);
                $(t).remove();
                $("#" + content_id).remove();
            }
        }
    });
}
$(function () {
    mainHeight = $(document.body).height() - 45;
    $('.main-left,.main-right').height(mainHeight);
    //$("[addtabs]").click(function () {
    //    addTabs({ id: $(this).attr("id"), title: $(this).attr('title'), close: true });
    //});

    $(".nav-tabs").on("click", "[tabclose]", function (e) {
        id = $(this).attr("tabclose");
        closeTab(id);
    });
    $(".nav-tabs").on("contextmenu", function (event) {
        if ($("#tabs_contextMenu").length == 0) {
            var menuText = '<ul class="dropdown-menu" id="tabs_contextMenu"><li><a href="javascript:closeActiveTab();">关闭当前</a>	<a href="javascript:closeOtherTab();">关闭其它</a> </li><li> <a href="javascript:closeAllTab();">关闭所有</a></li></ul>';
            $(menuText).appendTo($("body"));
        }
        var pageX = event.pageX;//鼠标单击的x坐标
        var pageY = event.pageY;//鼠标单击的y坐标
        //获取菜单
        var contextMenu = $("#tabs_contextMenu");
        var cssObj = {
            top: pageY + "px",//设置菜单离页面上边距离，top等效于y坐标
            left: pageX + "px"//设置菜单离页面左边距离，left等效于x坐标
        };
        //判断横向位置（pageX）+自定义菜单宽度之和，如果超过页面宽度及为溢出，需要特殊处理；
        var menuWidth = contextMenu.width();
        var pageWidth = $(document).width();
        if (pageX + contextMenu.width() > pageWidth) {
            cssObj.left = pageWidth - menuWidth - 5 + "px"; //-5是预留右边一点空隙，距离右边太紧不太美观；
        }
        //设置菜单的位置
        $("#tabs_contextMenu").css(cssObj).stop().fadeIn(200);//显示使用淡入效果,比如不需要动画可以使用show()替换;

        //阻止浏览器与事件相关的默认行为；此处就是弹出右键菜单
        event.preventDefault();

        $(document).on("mousedown", function (event) {
            //button等于0代表左键，button等于1代表中键
            if (event.button == 0 || event.button == 1) {
                $("#tabs_contextMenu").stop().fadeOut(200);//获取菜单停止动画，进行隐藏使用淡出效果
                /// $(document).unbind("mousedown,contextmenu");
            }
        });
    });
});