layui.use('laydate', function () {
    var laydate = layui.laydate;

    var start = {
        min: laydate.now()
      , max: '2099-06-16 23:59:59'
      , istoday: false
      , choose: function (datas) {
          end.min = datas; //开始日选好后，重置结束日的最小日期
          end.start = datas //将结束日的初始值设定为开始日
      }
    };

    var end = {
        min: laydate.now()
      , max: '2099-06-16 23:59:59'
      , istoday: false
      , choose: function (datas) {
          start.max = datas; //结束日选好后，重置开始日的最大日期
      }
    };

    document.getElementById('LAY_demorange_s').onclick = function () {
        start.elem = this;
        laydate(start);
    }
    document.getElementById('LAY_demorange_e').onclick = function () {
        end.elem = this
        laydate(end);
    }

});

layui.use(['layer'], function () {
    var layer = layui.layer;

});
$(document).ready(function () {

    //添加采购
    $("#btnadd").on("click", function ()
    {
        var sku = $("#sku").val();
        var num = $("#num").val();
        var url = $("#url").val();
        var supId = $('#supid option:selected').val();
        if (sku=="")
        {
            layer.msg("sku不能为空")
            return;
        }
        if (parseInt(num)<=0)
        {
            layer.msg("采购数量必须大于0")
            return;
        }
        //验证通过，提交到服务端
        var wait = layer.load(0, {
            shade: [0.1, '#676a6c'] //0.1透明度的白色背景
        });
        $.post("/Purchase/AddPurchaseService", { sku:sku, num: num, url: url, supId: supId }, function (data) {
            layer.close(wait);
            if (data.State >= 1) {
                layer.msg("添加成功");
            } else {
                layer.msg("操作失败,原因:" + data.Msg);
            }
        })
    })
});