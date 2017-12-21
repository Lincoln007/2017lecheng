
//添加用户
$("#addUser").on("click", function () {
    if (getCorrect($("#realname").val(), $("#username").val(), $("#password").val()) == false) {
        return;
    }
    var isAdmin = $("#adminManage")[0].checked;
    //验证通过，提交到服务端
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });

    $.post("/User/AddUserInfo", { user_name: $("#username").val(), real_name: $("#realname").val(), user_pwd: $("#password").val(), isAdmin: isAdmin }, function (data) {
        layer.close(wait);
        if (data.State >= 1) {
            layer.msg("添加成功");
        } else {
            layer.msg("操作失败,原因:" + data.Msg);
        }
    })
});
//得到正确的提交信息
function getCorrect(realname, username, password) {
    if (realname == "") {
        $("#realname").focus();
        layer.msg("用户姓名不能为空!");
        return false;
    }

    if (username == "") {
        $("#username").focus();
        layer.msg("用户名不能为空!");
        return false;
    }
    //检查字符串
    if (!/^[0-9a-zA-Z\u4e00-\u9fa5]+$/.test(username)) {
        $("#userid").focus();
        layer.msg("用户名不能包含特殊字符");
        return false;
    }
    if (password == "") {
        $("#password").focus();
        layer.msg("密码不能为空!");
        return false;
    }
    //检查字符串
    if (!/^[0-9a-zA-Z\u4e00-\u9fa5]+$/.test(password)) {
        $("#password").focus();
        layer.msg("密码不能包含特殊字符");
        return false;
    }
};
//打开更新用户界面
$('#myModalUpdate').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var userid = button.data('userid') // Extract info from data-* attributes
    var modal = $(this);
    modal.find('#updateuser').attr("data-userid", userid
        );
})
//更新用户信息
$("#updateuser").on("click", function () {
    var relname = $("#newrelname").val();
    var pwd = $("#newpwd").val();
    if (relname == "") {
        $("#newrelname").focus();
        layer.msg("用户姓名不能为空!");
        return;
    }
    if (pwd == "") {
        $("#pwd").focus();
        layer.msg("用户密码不能为空!");
        return;
    }
    var userId = $(this).attr("data-userid");
    //验证通过，提交到服务端
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });

    $.post("/User/UpdateUserInfoById", { real_name: relname, user_pwd: pwd, user_id: userId }, function (data) {
        layer.close(wait);
        if (data.State >= 1) {
            layer.msg("操作成功");
        } else {
            layer.msg("操作失败,原因:" + data.Msg);
        }
    })
})
//删除用户
//打开删除用户界面
$('#myModalDelete').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var userid = button.data('userid') // Extract info from data-* attributes
    var modal = $(this);
    modal.find('#btndel').attr("data-userid", userid);
})

$("#btndel").on("click", function () {
    var userId = $(this).attr("data-userid");
    //验证通过，提交到服务端
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });

    $.post("/User/DelUserById", { user_id: userId }, function (data) {
        layer.close(wait);
        if (data.State >= 1) {
            location.href = location.href;
        } else {
            layer.msg("操作失败,原因:" + data.Msg);
        }
    })
})
//禁用启用
$("#btnjy").on("click", function () {
    var userId = $(this).attr("data-userid");
    //验证通过，提交到服务端
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });

    $.post("/User/SetUserStateById", { user_id: userId, user_state: false }, function (data) {
        layer.close(wait);
        if (data.State >= 1) {
            location.href = location.href;
        } else {
            layer.msg("操作失败,原因:" + data.Msg);
        }
    })
})
$("#btnqy").on("click", function () {
    var userId = $(this).attr("data-userid");
    //验证通过，提交到服务端
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });

    $.post("/User/SetUserStateById", { user_id: userId, user_state: true }, function (data) {
        layer.close(wait);
        if (data.State >= 1) {
            location.href = location.href;
        } else {
            layer.msg("操作失败,原因:" + data.Msg);
        }
    })
})
//授权
$('#myModalpurview').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var userid = button.data('userid') // Extract info from data-* attributes
    var modal = $(this);
    //获取用户权限
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });

    $.post("/User/GetUserMenuById", { user_id: userid }, function (text) {
        layer.close(wait);
        var r = eval(text);
        var data = eval(r.DataResult);
        //显示已经存在的用户权限
        modal.find("input[type='checkbox']").each(function (i, item) {
            item.checked = false;
            for (var k = 0; k < data.length; k++) {
                if (item.attributes["data-menuid"].value == data[k].menu_id) {
                    item.checked = true;
                    return true;
                } else {
                    item.checked = false;
                }
            }

        })

    })

    modal.find('#btnsavemenu').attr("data-userid", userid);
})
//保存权限信息


$("#btnsavemenu").on("click", function () {
    var userId = $(this).attr("data-userid");
    var relList = [];
    //获取所有勾选的权限
    $("#myModalpurview").find("table input[type='checkbox']").each(function (i,item) {
        if (item.checked) {
            var obj=
            {
                menu_id : item.attributes["data-menuid"].value,
                user_id : userId
            };
            relList.push(obj);
        }

    })
    if (relList == null || relList.length<=0)
    {
        layer.msg("未添加任何权限");
        return;
    }
    //验证通过，提交到服务端
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });
    $.post("/User/AddUserMenuRel", { objlist: JSON.stringify(relList) }, function (data) {
        layer.close(wait);
        if (data.State >= 1) {
            layer.msg("授权成功");
        } else {
            layer.msg("操作失败,原因:" + data.Msg);
        }
    })
})