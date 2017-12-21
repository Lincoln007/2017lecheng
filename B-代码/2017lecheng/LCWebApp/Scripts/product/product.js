

    $(document).ready(function () {
        layui.use(['form', 'layer', 'laypage', 'element'], function () {
            var form = layui.form()
                  , layer = layui.layer
                 , laypage = layui.laypage
                 , element = layui.element();

            //判断款号是否已经存在
           // $("Code").blur(function ()
            //$("Code").change(function ()
            //{
            //    var code = $("Code").val();
            //    $.post('@Url.Content("~/NewProduct/PcodeIsEixt")', { code: code }, function (data) {
            //        if (data.State == 1) {
            //            layer.msg(data.Msg, { icon: 6, time: 1000 });
            //        } else {
            //            layer.msg(data.Msg);
            //        }
            //    })
            //});

        })

    });
