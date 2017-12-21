

/*
    �������
    ���ҳ����Ҫ������������ʹ�ø���䣺var Table_Layui1=Table_Layui,Ȼ��ʹ�ö���Table_Layui1ȥ����
*/
var Table_Layui = {
    //Table��ģ��ID--����ʹ��Div
    TablePanel: "",
    //�Ƿ���Ҫ���
    CountNumberBool: true,
    //�����б��������
    //{ txtName: "�б��������", 
    //ValueCode: "��ֵ��ȡֵ����", 
    //width: �еĳ���, 
    //Style: "�Զ����е���ʽ"
    //ValueDeal: �Զ���Ĵ���ֵ�÷��� }   
    Column: [
        { txtName: "", ValueCode: "", width: 0, Style: "", ValueDeal: function (value) { return value; } }
    ],
    //�༭����
    Edit: "",
    //ɾ������
    Delete: "",
    data: [],
    //��ǰҳ
    PageIndex: 1,
    //ÿҳչʾ��������
    PageSize: 10,
    //���������������ֵΪ0�����ʾ����Ҫ����ҳ��
    SumDateCounte: 0,
    //����ҳ���ѯָ�����ݵķ���
    SelectDataByPageIndex:function(){},
    //��ȡָ��������
    GetRowData: function (index) {
        return this.data[index];
    },
    //ҳ��
    Page_Layui: Page_Layui,
    //����Layui�ı����
    CreateTableFrame: function () {
        var width = 0;
        var colgroupHtml = "";
        var theadHtml = "";
        //����ֶ�
        if (this.CountNumberBool) {
            width = 50;
            colgroupHtml = "<col style='width:50;'>";
            theadHtml = "<th>���</th>";
        }
        //�б���
        var Column = this.Column;
        for (var i = 0; i < Column.length; i++) {
            width += Column[i].width;
            colgroupHtml += "<col  width='" + Column.width + "'>";
            theadHtml += "<th style='" + (Column[i].Style == undefined ? "" : Column[i].Style) + "'>" + Column[i].txtName + "</th>";
        }
        //���ư�ť ���б���
        if (this.Edit != "" || this.Delete != "") {
            width += 120;
            colgroupHtml += "<col  width='120'>";
            theadHtml += "<th></th>";
        }
        //����
        var tbody = "";
        if (this.data != undefined && this.data != null){
            if (this.data.length > 0) {
                for (var i = 0; i < this.data.length; i++) {
                    tbody += "<tr>";
                    //���
                    if (this.CountNumberBool) {
                        tbody += "<td style='text-align: center;'>" + ((parseFloat(this.PageIndex) - 1) * this.PageSize + i + 1) + "</td>";
                    }
                    for (var j = 0; j < Column.length; j++) {
                        //��Ԫ�����ʽ
                        tbody += "<td style=\'" + (Column[j].Style == undefined ? "" : Column[j].Style) + "\'>";
                        var value = "";
                        //��Ԫ�������
                        if (Column[j].ValueDeal != undefined) {
                            value = Column[j].ValueDeal(this.data[i][Column[j].ValueCode]);
                        }
                        else {
                            value = this.data[i][Column[j].ValueCode];
                        }
                        tbody += (value == null ? "" : value) + "</td>";
                    }
                    //���ư�ť
                    if (this.Edit != "" || this.Delete != "") {
                        tbody += "<td>";
                        //�༭��ť����
                        if (this.Edit != "") {
                            tbody += "<button class='layui-btn layui-btn-normal layui-btn-mini' onclick='" + this.Edit + "(" + i + ")' > <i class='layui-icon'>&#xe642;</i></button>";
                        }
                        //ɾ����ť����
                        if (this.Delete != "") {
                            tbody += "<button class='layui-btn layui-btn-normal layui-btn-mini' onclick='" + this.Delete + "(" + i + ")' > <i class='layui-icon'>&#xe640;</i></button>";
                        }
                        tbody += "</td>";
                    }
                    tbody += "</tr>";
                }
            }
        }
        //��ʼ��ҳ�����
        this.Page_Layui = Page_Layui;
        //��ʼ����ҳ������ID
        if (this.Page_Layui.PagePlaneID == "") {
            this.Page_Layui.PagePlaneID = new Date().getTime();
        }

        //��Ҫ����ҳ��
        if (this.SumDateCounte > 0) {            
            width = width < 530 ? 530 : width;//������������С������Ҫ����ҳ��
        }


        var html = "<div style='width:" + width + "px;'>"
                 + " <table class='layui-table' lay-skin='line'>"
                 + "    <colgroup>"
                 + colgroupHtml
                 + "    </colgroup>"
                 + "    <thead> "
                 + "        <tr>"
                 + theadHtml
                 + "        </tr>"
                 + "    </thead>"
                 + "    <tbody >" + tbody + "</tbody>"
                 + " </table>"
                 + " <div id=\"" + this.Page_Layui.PagePlaneID + "\"></div>"
                 + "</div>";
        $("#" + this.TablePanel).html(html);
        //����ҳ��
        if (this.SumDateCounte > 0) {
            this.Page_Layui.PageIndex = this.PageIndex;
            this.Page_Layui.PageSize = this.PageSize;
            this.Page_Layui.SumDateCounte = this.SumDateCounte;
            this.Page_Layui.SelectDataByPageIndex = this.SelectDataByPageIndex;
            this.Page_Layui.CreatePage();//����ҳ��
        }
    }
};


/*
    ����ҳ��
    ���ҳ����Ҫ�������ҳ�룬��ʹ�ø���䣺var Page_Layui1=Page_Layui,Ȼ��ʹ�ö���Page_Layui1ȥ����
*/
var Page_Layui = {
        //���ҳ�������
        PagePlaneID: "",
        //��ǰҳ
        PageIndex:1,
        //ÿҳչʾ��������
        PageSize: 10,
        //���ݵ�������
        SumDateCounte:0,
        //����ҳ���ѯ���ݵķ���
        SelectDataByPageIndex: function (PageIndex) { },
        //ˢ�µ�ǰҳ����
        Refresh: function () { this.SelectDataByPageIndex(this.PageIndex); },
        CreatePage: function () {            
            $("#"+this.PagePlaneID).css("width","100%");
            $("#"+this.PagePlaneID).css("text-align","right");
            var Page_Layui_PageSelect = this.SelectDataByPageIndex;
            var Page_Layui_PageID = this.PagePlaneID;
            var Page_Layui_PageIndex = this.PageIndex;
            var SumPage = Math.ceil(this.SumDateCounte / this.PageSize);
            layui.use(['laypage', 'layer'], function () {
                var laypage = layui.laypage
                , layer = layui.layer;
                laypage({
                    cont: Page_Layui_PageID
                    , curr: Page_Layui_PageIndex
                  , pages: SumPage //��ҳ��
                  , groups: 5 //������ʾ��ҳ��
                  , jump: function (obj, first) {
                      //�õ��˵�ǰҳ�����������������Ӧ����
                      if (!first) {
                          //$("#" + thisPageIndexLabelID).val(obj.curr);
                          Page_Layui_PageSelect(obj.curr);
                      }
                  }
                });
            });
        }


};




