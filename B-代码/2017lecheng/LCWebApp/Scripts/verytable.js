/** layui-v1.0.9_rls MIT License By http://www.layui.com */
; !
function (e) {
    var layer;
    var laypage;
    var form;
    var element;
    layui.use(['layer', 'laypage', 'element', 'form'], function () {
        layer = layui.layer;
        laypage = layui.laypage;
        element = layui.element();
        form = layui.form();
        //全选
        form.on('checkbox(allChoose)', function (data) {
            var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]');
            child.each(function (index, item) {
                if (typeof (item.attributes["disabled"]) == "undefined" || item.attributes["disabled"].value == "false") {
                    item.checked = data.elem.checked;
                }

            });
            form.render('checkbox');
        });
    })
    //时间格式化
    Date.prototype.Format = function (fmt) { //author: meizz 
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "H+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        if (/(Y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }

    var t = function () {
        this.v = "1.0.0_rls"
    };
    t.fn = t.prototype;
    var d = document;

    var index = 1;//用于判断是否第一次加载分页功能
    var sizelistindex = 1;//用户判断是否第一次查询
    $.fn.load = function (param, loadover) {
        var n = this;
        var url = "";
        if (typeof (n.attr("url")) == "undefined") {
            return;
        }
        url = n.attr("url");
        if (url.length <= 0) {
            layer.msg("请求的地址不能为空");
            return;
        }
        //判断是否需要分页
        if (typeof (n.attr("showpager")) != "undefined") {
            if (index == 1) {
                if (typeof (param) == "undefined") {
                    param = { pageIndex: 1 };
                }
            }
            if (typeof (param.pageIndex) == "undefined") {
                if (typeof (n.attr("pageindex")) != "undefined") {
                    param.pageIndex = n.attr("pageindex");
                } else {
                    param.pageIndex = 1;
                }
            }
            //用于控制下拉框选择页码
            if (typeof (param.pageSize) == "undefined") {
                if (typeof (n.attr("sizelist")) != "undefined") {
                    if (sizelistindex == 1)
                    {
                        var sizelist = n.attr("sizelist");
                        var objlist = eval(sizelist);
                        param.pageSize = objlist[0];
                    } else
                    {
                        var selid = n[0].id + "sizelist"
                        param.pageSize = $("#" + selid + " select").val();
                    }
                    sizelistindex++;
            
                } else {
                    if (typeof (n.attr("pagesize")) != "undefined") {
                        param.pageSize = n.attr("pagesize");
                    } else {
                        param.pageSize = 5;
                    }
                }
            }

        }
        //发送请求得到数据
        var ajaxType = "post";//请求方式
        if (typeof (n.attr("ajaxType")) != "undefined") {
            ajaxType = n.attr("ajaxType");
        }
        var daoru = layer.load(0, {
            shade: [0.1, '#676a6c'] //0.1透明度的白色背景
        });
        $.ajax({
            url: url,
            data: param,
            type: ajaxType,
            success: function (e) {
                layer.close(daoru);
                var data = eval(e);
                t.fn.SetTableRow(n, data.list);
                //网格加载之后执行的事件
                if (typeof (loadover) != "undefined") {
                    loadover(data);
                }
                //是否进行分页
                if (typeof (n.attr("showpager")) != "undefined") {
                    if (index != 1) {
                        //分页功能
                        laypage({
                            cont: '' + n[0].id + "_pager" + ''
                            , pages: data.pageCount
                            , curr: data.pageIndex
                            , skip: true
                            , jump: function (obj, first) {
                                if (!first) {
                                    param.pageIndex = obj.curr;
                                    n.load(param, loadover);
                                }
                            }
                        });
                        $("#" + n[0].id + "_pagecount")[0].textContent = "每页" + data.pageSize + "条,共" + data.count + "条";
                        return;
                    }
                    index++;

                    if (n.attr("showpager") == "true") {
                        //生成分页DIV
                        var $pager = $('<div></div>');
                        $pager.prop('id', n[0].id + "_pager");
                        n.after($pager);
                        //分页功能
                        laypage({
                            cont: '' + n[0].id + "_pager" + ''
                            , pages: data.pageCount
                            , curr: data.pageIndex
                            , skip: true
                            , jump: function (obj, first) {
                                if (!first) {
                                    param.pageIndex = obj.curr;
                                    n.load(param, loadover);
                                }
                            }
                        });
                    }
                    //是否显示每页记录数
                    if (typeof (n.attr("sizelist")) != "undefined") {
                        var sizelist = n.attr("sizelist");
                        var objlist = eval(sizelist);
                        var $pagesize = $("<div>每页记录数:</div>");
                        var $pagesel = $("<select lay-ignore></select>");
                        var $pagesizeId = n[0].id + "sizelist";
                        $pagesize.prop("id", $pagesizeId);
                        for (var i = 0; i < objlist.length; i++) {
                            var $pageoption = $("<option value='" + objlist[i] + "'>" + objlist[i] + "</option>")
                            $pagesel.append($pageoption);
                        }
                        $pagesize.css("margin", "10px")
                        $pagesize.css("line-height", "30px")
                        $pagesel.css("display", "inline-block");
                        $pagesize.css("float", "left");
                        $pagesize.append($pagesel);
                        n.after($pagesize);
                        if (typeof (param.pageSize) == "undefined") {
                            param.pageSize = objlist[0];
                        }
                        SetSelct($pagesize, param, n)
                    }

                    if (typeof (n.attr("showcount")) != "undefined") {
                        if (n.attr("showcount") == "true") {
                            //是否显示总记录数
                            var $pagecount = $("<div>每页" + data.pageSize + "条,共" + data.count + "条</div>")
                            $pagecount.prop("id", n[0].id + "_pagecount");
                            $pager.css("float", "left");
                            $pagecount.css("float", "right");
                            $pagecount.css("line-height", "55px");
                            n.after($pagecount);
                        }
                    }

                }
            },
            error: function () {
                layer.close(daoru);
                layer.msg("系统异常请稍后再试");
            }
        });
    }

    //生成表格行
    t.fn.SetTableRow = function ($table, data) {
        //清除表格行数据
        $table.find("tbody").remove();
        //获取表格头
        var thlist = $("#" + $table[0].id + " thead tr th");
        $tbody = $("<tbody></tbody>")
        //遍历得到的结果
        for (var i = 0; i < data.length; i++) {
            var $tr = $("<tr style='text-align:left;'></tr>");
            var $trId = $table[0].id + "row" + i;
            $tr.prop("id", $trId);
            thlist.each(function (index, item) {
                var $td = $('<td></td>')
                var $tdId = $table[0].id + i + "cell" + index;
                $td.prop("id", $tdId);
                if (typeof (item.attributes["very-type"]) != "undefined") {
                    //获取lay-filter
                    var filter = "Choose";
                    if (typeof (item.attributes["lay-filter"]) != "undefined") {
                        filter = item.attributes["lay-filter"].value;
                    }
                    var $input = $('<input title=" " name="sel" type="' + item.attributes["very-type"].value + '"  lay-skin="primary" lay-filter="' + filter + '"></input>');
                    //是否能选中（默认true）
                    if (typeof (item.attributes["allowCellSelect"]) != "undefined" && item.attributes["allowCellSelect"].value == "false") {
                        $input.prop("disabled", "disabled");
                    }
                    //控制容器中的文本是居中,靠左还是靠右。（默认靠左）
                    if (typeof (item.attributes["very-align"]) != "undefined") {
                        var align_value = item.attributes["very-align"].value;
                        $td.css("text-align", align_value);
                    }
                    $td.append($input);
                } else if (typeof (item.attributes["field"]) != "undefined") {
                    //绑定数据返回的列
                    var fieldname = item.attributes["field"].value;
                    var value = data[i][fieldname];
                    if (value == null) {
                        value = "";
                    }
                    //根据头格元素隐藏
                    if (item.style.display == "none") {
                        $td[0].style.display = "none";
                    }
                    //判断返回的数据是否为时间
                    if (typeof (item.attributes["very-data-format"]) != "undefined") {
                        var formatvalue = item.attributes["very-data-format"].value;
                        if (value != "" && value.indexOf('Date')>-1)
                        {
                            value = eval('new ' + (value.replace(/\//g, ''))).Format(formatvalue);
                        }
                    }
                    //判断是否为图片显示
                    if (typeof (item.attributes["very-image"]) != "undefined") {
                        var $img = $("<img src='" + value + "'></img>")
                        $td.append($img);
                    } else {
                        $td.html(value);
                    }
                    //双击表格是否进行编辑
                    if (typeof (item.attributes["very-dblclick"]) != "undefined") {
                        $td[0].className = "tddbclick"
                    }
                } else {
                    //单元格自定义显示内容
                    if (typeof (item.attributes["very-create"]) != "undefined") {
                        var tdcontent = eval(item.attributes["very-create"].value);
                        $td.html(tdcontent($tr[0].id));
                    }
                }
                $tr.append($td);
            });
            $tbody.append($tr);
        }
        //添加表格行
        $table.append($tbody);
        form.render();
    }

    //双击表格进行单个修改（只需要在需要修改的表格上加上tdclick）
    $("body").on("dblclick", "tbody .tddbclick", function () {
        if ($(this).hasClass("addinput")) {
            $(this).removeClass("addinput");
            $(this).html = $(this).html($(this).find(".importtext").val());

        } else {
            if ($(this).find("input").length <= 0) {
                $(this).addClass("addinput");
                $(this).html = $(this).html("<input type='text' class='importtext' value='" + $(this).html() + "' />")
            } else {
                $(this).html = $(this).html($(this).find(".importtext").val());
            }
        }

    });
    //修改输入框数据之后鼠标离开时自动关闭
    $("body").on("blur", ".importtext", function () {
        $(this).removeClass("addinput");
        $(this).html = $(this)[0].parentNode.innerHTML = $(this).val();
    })
    //添加每页记录数下拉框选中事件
    function SetSelct($pagesize, param, $table) {
        $pagesize[0].onchange = function (e) {
            param.pageIndex = 1;
            param.pageSize = e.target.value;
            $table.load(param);
        };

    }


    //获取表格所有行的数据
    $.fn.GetTableRowData = function () {
        var $table = $(this);
        var data = [];
        //获取表格所有的行
        if (typeof ($table[0].children[1]) == "undefined") {
            return data;
        }
        for (var i = 0; i < $table[0].children[1].children.length; i++) {
            var rowdata = t.fn.get($table[0].children[1].children[i].id).GetRowData();
            data.push(rowdata);
        }

        return data;
    }
    //获取表格所有选中的行的数据
    $.fn.GetSelTableRowData = function () {
        var $table = $(this);
        var data = [];
        //获取表格所有的行
        if (typeof ($table[0].children[1]) == "undefined")
        {
            return data;
        }
        for (var i = 0; i < $table[0].children[1].children.length; i++) {
           var objrow=$("#" + $table[0].children[1].children[i].id + " input:checkbox:checked");
           if (objrow.length > 0)
           {
               var rowdata = t.fn.get($table[0].children[1].children[i].id).GetRowData();
               data.push(rowdata);
           }
           
        }

        return data;
    }
    //获取行数据(row:行对象)
    $.fn.GetRowData = function () {
        var row = $(this)[0];
        var rowdata = {};
        var tdchildrens = row.children;
        //获取表格header用于给数据赋值
        var theadlist = $("#" + row.parentNode.parentNode.id + " thead tr th");
        for (var i = 0; i < tdchildrens.length; i++) {
            //排除没有定义的列名
            if (typeof (theadlist[i].attributes["field"]) == "undefined") {
                continue;
            }
            var attrname = theadlist[i].attributes["field"].value;
            rowdata[attrname] = tdchildrens[i].innerText;
        }
        return rowdata;
    }
    t.fn.get = function (id) {
        var obj = $("#" + id + "");
        return obj;
    };

    e.table = new t;
}(window);