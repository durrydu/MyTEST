$.SaveForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        loading: "正在处理数据...",
        success: null,
        close: true
    };
    var options = $.extend(defaults, options);
    Loading(true, options.loading);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    window.setTimeout(function () {
        $.ajax({
            url: options.url,
            data: options.param,
            type: options.type,
            dataType: options.dataType,
            success: function (data) {
                if (data.type == "3") {
                    dialogAlert(data.message, -1);
                } else {
                    Loading(false);
                    dialogMsg(data.message, 1);
                    options.success(data);
                    if (options.close == true) {
                        dialogClose();
                    }
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                Loading(false);
                if (XMLHttpRequest.statusText == "TimeOut") {
                    dialogTop("用户操作超时，请您手动注销后，重写登录系统！", "error");
                } else {
                    dialogMsg("系统正在优化中，请联系管理员！", -1);

                }
            },
            beforeSend: function () {
                Loading(true, options.loading);
            },
            complete: function () {
                Loading(false);
            }
        });
    }, 500);
}
$.SetForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        type: "get",
        dataType: "json",
        success: null,
        async: false
    };
    var options = $.extend(defaults, options);
    $.ajax({
        url: options.url,
        data: options.param,
        type: options.type,
        dataType: options.dataType,
        async: options.async,
        success: function (data) {
            if (data != null && data.type == "3") {
                dialogAlert(data.message, -1);
            } else {
                options.success(data);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            Loading(false);
            if (XMLHttpRequest.statusText == "TimeOut") {
                dialogTop("用户操作超时，请您手动注销后，重写登录系统！", "error");
            } else {
                dialogMsg("系统正在优化中，请联系管理员！", -1);

            }
        },
        beforeSend: function () {
            Loading(true);
        },
        complete: function () {
            Loading(false);
        }
    });
}
$.RemoveForm = function (options) {
    var defaults = {
        msg: "注：您确定要删除吗？该操作将无法恢复",
        loading: "正在删除数据...",
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null
    };
    var options = $.extend(defaults, options);
    dialogConfirm(options.msg, function (r) {
        if (r) {
            Loading(true, options.loading);
            window.setTimeout(function () {
                var postdata = options.param;
                if ($('[name=__RequestVerificationToken]').length > 0) {
                    postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                }
                $.ajax({
                    url: options.url,
                    data: postdata,
                    type: options.type,
                    dataType: options.dataType,
                    success: function (data) {
                        if (data.type == "3") {
                            dialogAlert(data.message, -1);
                        } else {
                            dialogMsg(data.message, 1);
                            options.success(data);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        Loading(false);
                        if (XMLHttpRequest.statusText == "TimeOut") {
                            dialogTop("用户操作超时，请您手动注销后，重写登录系统！", "error");
                        } else {
                            dialogMsg("系统正在优化中，请联系管理员！", -1);

                        }
                    },
                    beforeSend: function () {
                        Loading(true, options.loading);
                    },
                    complete: function () {
                        Loading(false);
                    }
                });
            }, 500);
        }
    });
}
$.ConfirmAjax = function (options) {
    var defaults = {
        msg: "提示信息",
        loading: "正在处理数据...",
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null
    };
    var options = $.extend(defaults, options);
    dialogConfirm(options.msg, function (r) {
        if (r) {
            Loading(true, options.loading);
            window.setTimeout(function () {
                var postdata = options.param;
                if ($('[name=__RequestVerificationToken]').length > 0) {
                    postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                }
                $.ajax({
                    url: options.url,
                    data: postdata,
                    type: options.type,
                    dataType: options.dataType,
                    success: function (data) {
                        Loading(false);
                        if (data.type == "3") {
                            dialogAlert(data.message, -1);
                        } else {
                            dialogMsg(data.message, 1);
                            options.success(data);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        Loading(false);
                        if (XMLHttpRequest.statusText == "TimeOut") {
                            dialogTop("用户操作超时，请您手动注销后，重写登录系统！", "error");
                        } else {
                            dialogMsg("系统正在优化中，请联系管理员！", -1);

                        }
                    },
                    beforeSend: function () {
                        Loading(true, options.loading);
                    },
                    complete: function () {
                        Loading(false);
                    }
                });
            }, 200);
        }
    });
}
$.ExistField = function (controlId, url, param) {
    var $control = $("#" + controlId);
    if (!$control.val()) {
        return false;
    }
    var data = {
        keyValue: request('keyValue')
    };
    data[controlId] = $control.val();
    var options = $.extend(data, param);
    $.ajax({
        url: url,
        data: options,
        type: "get",
        dataType: "text",
        async: false,
        success: function (data) {
            if (data.toLocaleLowerCase() == 'false') {
                ValidationMessage($control, '已存在,请重新输入');
                $control.attr('fieldexist', 'yes');
            } else {
                $control.attr('fieldexist', 'no');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            Loading(false);
            if (XMLHttpRequest.statusText == "TimeOut") {
                dialogTop("用户操作超时，请您手动注销后，重写登录系统！", "error");
            } else {
                dialogMsg("系统正在优化中，请联系管理员！", -1);

            }
        },
    });
}


$.GetAjax = function (options) {
    var defaults = {
        msg: "提示信息",
        loading: "正在处理数据...",
        url: "",
        param: [],
        type: "get",
        dataType: "json",
        success: null,
        async: true,
        cache: false,
        load: true,
    };

    var options = $.extend(defaults, options);

    if (options.load) {
        Loading(true, options.loading);
    }
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    //window.setTimeout(function () {
    var postdata = options.param;

    $.ajax({
        url: options.url,
        data: postdata,
        type: options.type,
        dataType: options.dataType,
        cache: options.cache,
        async: options.async,
        success: function (data) {
            Loading(false);
            //if (data.type == "3") {
            //    if (!!data.message) {
            //        dialogAlert(data.message, -1);
            //    }
            //} else {
            //    console.log(data);
            //    if (!!data.message) {
            //        dialogMsg(data.message, 1);
            //    }
            options.success(data);
            //  }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            Loading(false);
            //dialogMsg(errorThrown, -1);
            if (XMLHttpRequest.statusText == "TimeOut") {
                dialogTop("用户操作超时，请您手动注销后，重写登录系统！", "error");
            } else {
                dialogMsg("系统正在优化中，请联系管理员！", -1);

            }
        },
        beforeSend: function () {
            Loading(true, options.loading);
        },
        complete: function () {
            Loading(false);
        }
    });
    //}, 200);

}

$.PostAjax = function (options) {
    var defaults = {
        msg: "提示信息",
        loading: "正在处理数据...",
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null,
        async: true,
        load: true,
        beforeSend: null,
        complete: null,
    };

    var options = $.extend(defaults, options);
    if (options.load) {
        Loading(true, options.loading);
    }

    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    window.setTimeout(function () {
        var postdata = options.param;
        $.ajax({
            url: options.url,
            data: postdata,
            type: options.type,
            async: options.async,
            dataType: options.dataType,
            success: function (data) {
                Loading(false);
                //if (data.type == "3") {
                //    if (!!data.message) {
                //        dialogalert(data.message, -1);
                //    }
                //} else {
                //    if (!!data.message) {
                //        dialogMsg(data.message, 1);
                //    }
                if (options.success != null) {
                    options.success(data);
                }
                // }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                Loading(false);
                // dialogMsg(errorThrown, -1);
                if (XMLHttpRequest.statusText == "TimeOut") {
                    dialogTop("用户操作超时，请您手动注销后，重写登录系统！", "error");
                } else {
                    dialogMsg("系统正在优化中，请联系管理员！", -1);

                }
            },
            beforeSend: function () {
                Loading(true, options.loading);
                if (options.beforeSend != null) {
                    options.beforeSend();
                }
            },
            complete: function () {
                Loading(false);
                if (options.complete != null) {
                    options.complete();
                }
            }
        });
    }, 200);

}




