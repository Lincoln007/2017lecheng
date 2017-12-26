

//////auther:lxwzsh
$(function () {
    var form;
    var layer;
    var laypage;
    var element;
    var laydate;
    layui.use(['form', 'layer', 'laypage', 'element', 'laydate', 'upload'], function () {
        form = layui.form()
             , layer = layui.layer
            , laypage = layui.laypage
            , element = layui.element()
            , laydate = layui.laydate;

        //查找导入的订单
        $('#SearchOrder').bind("click", function () {
            var shopID = $('#shops').val();
            //layer.msg("执行搜索", { time: 2000, icon: 6 });
            if (shopID==0||shopID=="undefined")
            {
                layer.msg("请先选择店铺平台", { time: 2000, icon: 5 });
                return;
            }
            btnhtml = "<td><input type='button' class='layui-btn layui-btn-mini' onclick='UpdateSKU(this)' value='修改'/><input type='button' class='layui-btn layui-btn-mini layui-btn-normal' onclick='DeleteSKU(this)' value='删除'/></td>";
            var param = { shopID: shopID }; //参数集合
            SetDataTable("mytable", "", param, btnhtml);
            //var tables = table.get("mytable");
            //var param = {shopID:shopID}; //参数集合
            //tables.load(param);
        })

       

        //解析入系统
        $('#InsertOrder').bind("click", function () {
            var leadin = layer.load(0, {
                shade: [0.1, '#676a6c']  //0.1透明度的白色背景
            });
            var shopID = $('#shops').val();
            var platformID = $('#platform').val();
            //获取表格中所有数据到数组
            var list = GetTableRowData("mytable");
            if (list.length==0)
            {
                layer.close(leadin);
                layer.msg("没有需要解析的数据");
                return false;
            }
            $.post('/ImportOrder/InsertOrder', { shopID: shopID, platformID: platformID, list: list }, function (data) {
                if (data) {
                    layer.close(leadin);
                    layer.msg(data.Msg, { icon: 6, time: 6000 });
                } else {
                    layer.close(leadin);
                    layer.msg(data.Msg, { icon: 5, time: 2000 });
                }
                $("#SearchOrder").click(); //查询刷新一下防止重复导入
            })
        })
        //转换SKU
        $('#transferSKU').bind("click", function () {
            var leadin = layer.load(0, {
                shade: [0.1, '#676a6c']  //0.1透明度的白色背景
            });
            //获取表格中所有数据到数组
            var list = GetTableRowData("mytable");
            if (list.length == 0) {
                layer.close(leadin);
                layer.msg("没有需要转换的数据");
                return false;
            }
            var shopID = $('#shops').val();         
            $.post('/ImportOrder/GetAllPSkuByShopID', { shopID: shopID}, function (data) {
                if (data.State) {
                    layer.close(leadin);
                    //替换SKU
                    //data.DataResult;
                    var tableObj = document.getElementById('mytable');
                    var count = 0;
                    for (var i = 0; i < tableObj.rows.length; i++)
                    {
                        for (var j = 0; j < data.DataResult.length;j++)
                        {
                            if (tableObj.rows[i].cells[3].innerText == data.DataResult[j].PSKU) {
                                tableObj.rows[i].cells[3].innerText = data.DataResult[j].SSKU;
                                count++;
                            }
                        }
                    }
                    layer.msg("一共替换"+count+"条数据", { time: 2000 });
                } else {
                    layer.close(leadin);
                    layer.msg(data.Msg, { icon: 5, time: 2000 });
                }
            })
        })

        //选择平台获取店铺
        form.on('select(plat)', function (data) {
            $('#shops').children().remove();
            var platformID = $('#platform').val();
            if (0 == platformID)
            {
                layer.msg("请选择有效平台");
                return;
            }
            //layer.msg(platformID);
            $.post('/GetDropDownList/GetShops', { platformID: platformID }, function (data) {
                $('#shops').children().remove();
                for (var i = 0; i < data.length; i++) {
                    $('#shops').append("<option value='" + data[i].shop_id + "'>" + data[i].shop_name + "</option>"); //为Select追加一个Option(下拉项)
                }
                form.render('select');
            })
        });

        //上传文件
        $("#submitId").on("click", function () {
            var shopid = $("#shops").val();
            var naqi = $("#naqi").val();
            var files = $("#files").val();
            if (shopid==0||shopid==undefined)
            {
                layer.msg("请选择店铺");                
                return false;
            }
            if (naqi == "") {
                layer.msg("请输入纳期");
                $("#naqi").focus();
                return false;
            }
            if (files == "") {
                layer.msg("请选择上传文件");
                return false;
            }
            $.ajaxFileUpload({
                url: '/ImportOrder/ImportOrderFile', //你处理上传文件的服务端
                secureuri: false,//是否启用安全机制
                fileElementId: 'files',//file的id
                dataType: 'json',//返回的类型
                data: { shopid: shopid, naqi: naqi },
                success: function (data) {//调用成功时怎么处理
                    //var str = data.match(/{(\S*)}/)[1];
                    //var result = "{" + str + "}";
                    if (data.State == 1) {
                        $("#files").val("");
                        layer.msg("导入临时库成功", { icon: 6, time: 1000 });
                    } else {
                        layer.msg("导入临时库失败,原因:" + data.Msg);
                    }
                }
            });
        });

        function EditOrder()
        {
            return "<td><input type='button' class='layui-btn layui-btn-mini' onclick='UpdateSKU(this)' value='修改'/><input type='button' class='layui-btn layui-btn-mini layui-btn-normal' onclick='DeleteSKU(this)' value='删除'/></td>";
        }
     //////----------------------弹出层JS开始--------------------------------------------------------------------------/////
        $('#layersku').click(function open(){
         var openedit = layer.open({
             type: 2,
             title: "SKU配置列表",
             anim: 2,
             //content: $('#layersupp'),
             content: ['/ImportOrder/PeizhiSKU?islayeropen=1', 'yes'],
             area: ['900px', '800px']
         });
       })
        //删除UpdateSKU
        function DeleteSKU(curr) {
            layer.msg('确定要删除吗？', {
                time: 0 //不自动关闭
           , btn: ['确认', '取消']
           , yes: function (index) {
               layer.close(index);
               $(curr).parent().parent().remove();
               var id = $(curr).parent().parent().children('td').eq(0).text();
               var shopID = $('#shops').val();
               $.post('@Url.Content("~/ImportOrder/DeleteLs")', { id: id, shopID: shopID }, function (data) {
                   if (data) {
                       layer.msg(data.Msg, { icon: 6, time: 1000 });
                   } else {
                       layer.msg(data.Msg, { icon: 5, time: 1000 });
                   }
               })
           }
            });
        }

        //更新SKU
        function UpdateSKU(e) {
            var id = $(e).parent().parent().children('td').eq(0).text();
            var clientname = $(e).parent().parent().children('td').eq(5).text();
            var sku = $(e).parent().parent().children('td').eq(3).text();
            var num = $(e).parent().parent().children('td').eq(4).text();
            var telephone = $(e).parent().parent().children('td').eq(6).text();
            var phone = $(e).parent().parent().children('td').eq(7).text();
            var address = $(e).parent().parent().children('td').eq(8).text();
            $('#clientname').val(clientname);
            $('#sku').val(sku);
            $('#num').val(num);
            $('#telephone').val(telephone);
            $('#phone').val(phone);
            $('#address').val(address);

            openedit = layer.open({
                type: 1,
                title: "订单编辑",
                content: $('#layersupp'),
                area: ['900px', '600px']
            });
            $("#save1").on("click", function () {
                layer.close(openedit);
                clientname = $('#clientname').val();
                sku = $('#sku').val();
                num = $('#num').val();
                telephone = $('#telephone').val();
                phone = $('#phone').val();
                address = $('#address').val();
                shopID = $('#shops').val();
                $.post('@Url.Content("~/ImportOrder/UpdateLSSKU")', { shopID: shopID, id: id, clientname: clientname, sku: sku, num: num, telephone: telephone, phone: phone, address: address }
                , function (data) {
                    if (data.State == 1) {
                        $(e).parent().parent().children('td').eq(5).text(clientname);
                        $(e).parent().parent().children('td').eq(3).text(sku);
                        $(e).parent().parent().children('td').eq(4).text(num);
                        $(e).parent().parent().children('td').eq(6).text(telephone);
                        $(e).parent().parent().children('td').eq(7).text(phone);
                        $(e).parent().parent().children('td').eq(8).text(address);
                        layer.msg(data.Msg, { icon: 6, time: 1000 });
                    } else {
                        layer.msg(data.Msg);
                    }
                });
            });
        }
     //////---------------------弹出层JS结尾---------------------------------------------------------------------------/////
        
    })
});