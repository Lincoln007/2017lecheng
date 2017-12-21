//打开订单详情页面
$('#myModalDetail').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var url = button[0].href; // Extract info from data-* attributes
    var modal = $(this);
    $("#myModalDetail").load(url);
})
//处理订单
function dealOrder()
{
    var ids = [];
    //获取勾选的下拉框
    if ($("tbody input[type=checkbox]:checked").length <= 0) {
        layer.msg("您未选中任何数据");
        return;
    }
    //遍历选中的值
    $("tbody input[type=checkbox]:checked").each(function (e, i)
    {
        var id = { id: e.attr("data-orderid") }
        ids.push(id);
    })
    //验证通过，提交到服务端
    var wait = layer.load(0, {
        shade: [0.1, '#676a6c'] //0.1透明度的白色背景
    });
    $.post("/ImportOrder/UpdateCustOrder", { ids: ids, state: 10 }, function (data) {
        layer.close(wait);
        if (data.State >= 1) {
            layer.msg("添加成功");
        } else {
            layer.msg("操作失败,原因:" + data.Msg);
        }
    })
}