var tablist = {

};
(function ($) {
    "use strict";
    $.movittab = {
        addTab: function () {

            var dataId = $(this).attr('data-id');
            if (dataId != "") {
                top.$.cookie('currentmoduleId', dataId, { path: "/" });
            }
            var dataUrl = $(this).attr('href');
            var target = $(this).attr('target');
            var menuName = $.trim($(this).html());
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                return false;
            }
            if (target == "iframe") {
                //window.setTimeout(function () {
                var iframeContent = $(".movit_iframe");
                iframeContent.attr("src", dataUrl);
                iframeContent.attr("id", "iframe" + dataId);
                $(".menuTab").html(menuName);
                iframeContent.load(function () {
                    window.setTimeout(function () {
                        Loading(false);
                    }, 200);

                });
                if ($(this).parents(".treeview-menu").hasClass("menu-classB")) {
                    $(".menu-classB .menuItem").removeClass("selected");
                    $(this).addClass("selected");
                    return false;
                } else {
                    $(this).parent().trigger("click");
                    return false;
                }
                //}, 200);

            }
            else {

                sysf_openFullScreen(dataUrl);
            }

        },
        calSumWidth: function (element) {
            var width = 0;
            $(element).each(function () {
                width += $(this).outerWidth(true);
            });
            return width;
        }
    };
    $.movitindex = {
        load: function () {
            $("#mainContent").height($(window).height() - 100);
            $(".lea-left .content").css("height", $(window).height() - 100);
            $(window).resize(function (e) {
                $("#mainContent").height($(window).height() - 100);
                $(".lea-left .content").css("height", $(window).height() - 100);
                //$.movitindex.loadMenu(true);
            });
            //个人中心
            $("#UserSetting").click(function () {
                tablist.newTab({ id: "UserSetting", title: "个人中心", closed: true, icon: "fa fa fa-user", url: contentPath + "/PersonCenter/Index" });
            });

            $(window).load(function () {
                window.setTimeout(function () {
                    $('#ajax-loader').fadeOut();
                    Loading(false);
                }, 300);
            });
        },
        jsonWhere: function (data, action) {
            if (action == null) return;
            var reval = new Array();
            $(data).each(function (i, v) {
                if (action(v)) {
                    reval.push(v);
                }
            })
            return reval;
        },
        loadMenu: function (isInit) {

            var data = authorizeMenuData;
            var _html = "";
            var menuWidth = 0;
            var _html1 = "", _html2 = "";
            var _c = "";
            $.each(data, function (i) {
                var row = data[i];
                var _itemHtml = "";
                var _treeHtml = "";
                if (row.ParentId == "0") {

                    var childNodes = $.movitindex.jsonWhere(data, function (v) { return v.ParentId == row.ModuleId });
                    var isNeedTrueLeft = childNodes.length > 0 ? "" : "notTreeMenu";
                    _itemHtml += '<li class="treeview ' + isNeedTrueLeft + '" id="sys-' + row.ModuleId + '">';
                    _itemHtml += '<a>';
                    _itemHtml += '<span>' + row.FullName + '</span>';
                    _itemHtml += '</a>';

                    if (childNodes.length > 0) {
                        _treeHtml += '<div class="menu sys-' + row.ModuleId + '"><ul class="menu-classA treeview-menu">';
                        $.each(childNodes, function (i) {
                            var subrow = childNodes[i];
                            var subchildNodes = $.movitindex.jsonWhere(data, function (v) { return v.ParentId == subrow.ModuleId });
                            _treeHtml += '<li>';

                            if (subchildNodes.length > 0) {
                                _treeHtml += '<a class="menuTreeItem menuItem" href="#"><i class="' + subrow.Icon + ' firstIcon"></i>' + subrow.FullName + '';
                                _treeHtml += '<i class="fa fa-angle-right pull-right"></i></a>';

                                _treeHtml += '<div class="popover-menu-sub"><ul class="treeview-menu menu-classB">';
                                $.each(subchildNodes, function (i) {
                                    var subchildNodesrow = subchildNodes[i];
                                    _treeHtml += '<li><a class="menuItem menuiframe" data-parent="m-' + subchildNodesrow.ParentId + '"  target="' + subchildNodesrow.Target + '" href="' + subchildNodesrow.UrlAddress + '"  data-id="' + subchildNodesrow.ModuleId + '" ><i class="' + subchildNodesrow.Icon + ' firstIcon"></i>' + subchildNodesrow.FullName + '</a></li>';
                                });
                                _treeHtml += '</ul></div>';
                            } else {
                                _treeHtml += '<a class="menuItem menuiframe" data-parent="m-' + subrow.ParentId + '"  target="' + subrow.Target + '"  data-id="' + subrow.ModuleId + '" href="' + subrow.UrlAddress + '" "><i class="' + subrow.Icon + ' firstIcon"></i>' + subrow.FullName + '</a>';
                            }
                            _treeHtml += '</li>';
                        });
                        _treeHtml += '</ul></div>';
                        $(".lea-left .content").append(_treeHtml);
                    }
                    _itemHtml += '</li>';
                    _html += _itemHtml;
                }
            });

            _html += _html1;

            if (isInit) {
                $("#top-menu").html(_html);
                var indexDom = '<li class="treeview notTreeMenu checked" id="sys-Home" data-id="/Home/Index" data-href="/Home/Index"><a><span>首页</span></a></li>'
                $("#top-menu").prepend(indexDom);
                $("#top-menu>.treeview").unbind();
                $('.menu>ul>li').unbind();
                $("#top-menu>.treeview").click(function () {
                    var thischildNodesId = $(this).attr("id").replace(/sys-/, "m-");
                    var childNodes = $("[data-parent='" + thischildNodesId + "']");
                    if (childNodes.length > 0) {
                        childNodes.eq(0).trigger("click");
                    }
                    var treeID = $(this).attr("id");
                    $("#top-menu>.treeview").removeClass("checked");
                    $(this).addClass("checked");
                    if ($(this).hasClass("notTreeMenu")) {
                        if ($(".lea-left").hasClass("notShow")) {
                            return false;
                        } else {
                            $(".lea-left").addClass("notShow");
                            var iframeContent = $(".movit_iframe");
                            var dataId = $(this).attr('data-id');
                            var dataUrl = $(this).attr('data-href');
                            iframeContent.attr("src", dataUrl);
                            iframeContent.attr("id", "iframe" + dataId);

                        }
                    } else {
                        $(".lea-left").removeClass("notShow");
                        $(".lea-left .menu").hide();
                        $(".lea-left").find("." + treeID).fadeIn(500);
                    };

                });
                $('.menu>ul>li').click(
                    function () {
                        var $li = $(this);
                        var $popover = $li.find('.popover-menu-sub');
                        $li.siblings().removeClass('active');
                        $li.addClass('active');
                        $popover.stop(true, false).slideToggle(150);
                    });
                $('.menu>ul>li .menuiframe').unbind();
                $('.menu>ul>li .menuiframe').on('click', $.movittab.addTab);

            }
        },
        indexOut: function () {
            dialogConfirm("注：您确定要安全退出本次登录吗？", function (r) {
                if (r) {
                    Loading(true, "正在安全退出...");
                    window.setTimeout(function () {
                        $.ajax({
                            url: contentPath + "/Login/OutLogin",
                            type: "post",
                            dataType: "json",
                            success: function (data) {

                                window.location.href = data.resultdata;
                            }
                        });
                    }, 500);
                }
            });
        }
    };
    $(function () {
        $.movitindex.loadMenu(true);
        $.movitindex.load();
        //$("#top-menu .treeview").click(function () {
        //    var thisId = $(this).attr("id").replace(/sys-/, "m-");
        //     debugger
        //    //alert(thisId);
        //    //var _t = $(this)[0].className == "treeview active" ? true : false;
        //    //if (_t == true) {
        //    //    $("#top-menu .treeview").removeClass("checked");
        //    //    $(this).addClass("checked");
        //    //} else {

        //    //}
        //})

    });
})(jQuery);

//安全退出
function IndexOut() {
    dialogConfirm("注：您确定要安全退出本次登录吗？", function (r) {
        if (r) {
            Loading(true, "正在安全退出...");
            window.setTimeout(function () {
                $.ajax({
                    url: contentPath + "/Login/OutLogin",
                    type: "get",
                    dataType: "json",
                    success: function (data) {
                      
                        window.location.href = data.resultdata;
                    }
                });
            }, 500);
        }
    });
}
