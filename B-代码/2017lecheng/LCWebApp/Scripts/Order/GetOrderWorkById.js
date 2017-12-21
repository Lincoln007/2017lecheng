layui.use(['layer'], function () {
    var layer = layui.layer;

});
$(document).ready(function () {

    //更新收货地址
    $("#btnadd").on("click", function () {
        var order_id = $("#sendorderid").val();
        if (order_id == "") {
            layer.msg("发货订单信息不正确")
            return;
        }
        var receive_name = $("#receive_name").val();
        var receive_address = $("#receive_address").val();
        var receive_phone = $("#receive_phone").val();
        var receive_zip = $("#receive_zip").val();
        var receive_mobile = $("#receive_mobile").val();
        //验证通过，提交到服务端
        var wait = layer.load(0, {
            shade: [0.1, '#676a6c'] //0.1透明度的白色背景
        });
        $.post("/ImportOrder/UpdateSendOrderUserInfoById", {
            order_id: order_id, receive_name:
                receive_name, receive_address: receive_address, receive_phone: receive_phone, receive_zip: receive_zip, receive_mobile: receive_mobile
        }, function (data) {
            layer.close(wait);
            if (data.State >= 1) {
                layer.msg("添加成功");
            } else {
                layer.msg("操作失败,原因:" + data.Msg);
            }
        })
    })

    //更改采购方式
    $("#btnset").on("click", function () {
        var detail_source = $("#detail_source  option:selected").val();
        var new_wh_id = $("#whid  option:selected").val();
        var detail_id = $(this).attr("data-detail_id");
        var old_wh_id = $(this).attr("data-wh_id");
        var num = $(this).attr("data-num");
        var proId = $(this).attr("data-proid");
        //验证通过，提交到服务端
        var wait = layer.load(0, {
            shade: [0.1, '#676a6c'] //0.1透明度的白色背景
        });
        $.post("/ImportOrder/UpdateWorkInfo", {
            detail_id: detail_id, old_wh_id:
                old_wh_id, detail_source: detail_source, new_wh_id: new_wh_id, num:num,proId: proId
        }, function (data) {
            layer.close(wait);
            if (data.State >= 1) {
                layer.msg("更改成功");
            } else {
                layer.msg("操作失败,原因:" + data.Msg);
            }
        })
    })

});