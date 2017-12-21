/**
 * Created by david on 2017/1/13.
 */
layui.config({
    base: './static/modules/'
}).use(['jquery', 'tab','navbar', 'layer'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        navbar = layui.navbar(),
        tab = layui.tab({
            elem: '.admin-nav-card' //����ѡ�����
        });



    // �����˵� ����л���ߵ���
    $('.myTabs>li').click(function () {
        var ids = $(this).attr('aria-controls');

        $('.nav_wraper ul').css('display', 'none');
        $('#' + ids).css('display', 'block');

        $('.myTabs li').removeClass('active');
        $('.' + ids).addClass('active');
    });

    // ����û�
    $('#infoDesc').hover(function () {
        $(this).find('ul').show();
    }, function () {
        $(this).find('ul').hide();
    });

    //�˵�
    // $('.menu>li').on('click', function () {
    //     $(this).find('.submenu').toggle(300);
    // });





    //iframe����Ӧ
    $(window).on('resize', function () {
        var $content = $('.admin-nav-card .layui-tab-content');
        $content.height($(this).height() - 147);
        $content.find('iframe').each(function () {
            $(this).height($content.height());
        });
    }).resize();

    //����navbar
    navbar.set({
        spreadOne: true,
        elem: '#admin-navbar-side',
        cached:false,
        url: './datas/nav.json'
    });
    //��Ⱦnavbar
    navbar.render();
    //��������¼�
    navbar.on('click(side)', function(data) {
        tab.tabAdd(data.field);
    });

	

    // $('.openapp').on('click', function () {
    //     var url = $(this).attr('data-href');
    //     var icon = $(this).attr('data-icon');
    //     var title = $(this).attr('data-title');
    //     openTab(url, title, icon);
    // });

    // function openTab(url, title, icon) {
    //     if (!url || !title) {
    //         layer.msg('��������Ϊ��');
    //         return false;
    //     }
    //     var str = {
    //         'href': url,
    //         'icon': icon,
    //         'title': title
    //     };
    //     tab.tabAdd(str);
    // };


});