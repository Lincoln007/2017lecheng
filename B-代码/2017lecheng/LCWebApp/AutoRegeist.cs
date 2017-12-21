using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using IBLLService;
using BLLServices;
using BLLServices.Supplier;
using BLLServices.Product;

using BLLServices.Base.Platform;
using IBLLService.Base.Platform;
using BLLServices.Order;
using BLLServices.Base.Shop;
using IBLLService.Base.Shop;
using IBLLService.Order;
using IBLLService.Purchase;
using BLLServices.Purchase;
using BLLServices.Base.WareHouse;
using IBLLService.Base.WareHouse;
using BLLServices.Base.StorageBin;
using IBLLService.Base.StorageBin;
using BLLServices.Base.Stock;
using IBLLService.Base.Stock;
using BLLServices.Procurement;
using IBLLService.Procurement;
using BLLServices.WaitPurchase;
using IBLLService.WaitPurchase;
using BLLServices.MaterialReceipt;
using IBLLService.MaterialReceipt;
using BLLServices.OrderQuery;
using IBLLService.OrderQuery;
using IBLLService.OrderQuery_Shop;
using BLLServices.OrderQuery_Shop;
using IBLLService.RFT;
using BLLServices.RFT;
using IBLLService.StoreSales;
using BLLServices.StoreSales;
using BLLServices.ProcurementCosts;
using IBLLService.ProcurementCosts;
using BLLServices.NotPicking;
using IBLLService.NotPicking;
using BLLServices.InternationalQuiry;
using IBLLService.InternationalQuiry;
using BLLServices.OverseasDelivery;
using IBLLService.OverseasDelivery;
using BLLServices.LogisticsQuery;
using IBLLService.LogisticsQuery;
using IBLLService.GoodsReceived;


namespace LCWebApp
{
    /// <summary>
    /// AutoFac注册类
    /// </summary>
    public class AutoRegeist
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //在此添加接口和业务类的映射 
            builder.RegisterType<LoginService>().As<ILoginService>();
            builder.RegisterType<SupplierService>().As<ISupplier>();
            builder.RegisterType<ProductService>().As<IProduct>();
            builder.RegisterType<NewProductService>().As<INewProduct>();
            //订单
            builder.RegisterType<OrderService>().As<IOrderService>();
            //采购
            builder.RegisterType<PurchaseService>().As<IPurchaseService>();
            //用户信息
            builder.RegisterType<UserService>().As<IUserService>();
            //快递公司
            builder.RegisterType<ExpressService>().As<IExpress>();
            //订单导入

            //基础数据
            builder.RegisterType<PlatformService>().As<IPlatformService>(); //平台管理
            builder.RegisterType<ShopService>().As<IShopService>(); //商铺管理
            builder.RegisterType<WareHouseService>().As<IWareHouseService>(); //仓库管理
            builder.RegisterType<StorageBinService>().As<IStorageBinService>(); //仓库库位
            builder.RegisterType<StockService>().As<IStockService>(); //仓库库存

            //RFT手持

            builder.RegisterType<RFTService>().As<IRFT>(); //采购历史
            builder.RegisterType<DeclarationService>().As<IDeclaration>(); //报关
            //打印插件
            builder.RegisterType<PrintService>().As<IPrint>(); //采购历史
            builder.RegisterType<ProcurementService>().As<IProcurementService>(); //采购历史

            builder.RegisterType<WaitPurchaseService>().As<IWaitPurchaseService>(); //等待采购

            builder.RegisterType<GoodsReceivedService>().As<IGoodsReceivedService>(); //待收货

            builder.RegisterType<MaterialReceiptService>().As<IMaterialReceiptService>(); //等待收货

            builder.RegisterType<OrderQueryService>().As<IOrderQueryService>(); //订单综合查询

            builder.RegisterType<OrderQuery_ShopService>().As<IOrderQuery_ShopService>(); //店铺订单查询

            builder.RegisterType<StoreSalesService>().As<IStoreSalesService>(); //店铺销售额查询

            builder.RegisterType<ProcurementCostsService>().As<IProcurementCostsService>(); //采购成本查询

            builder.RegisterType<NotPickingService>().As<INotPickingService>(); //未配货查询

            builder.RegisterType<InternationalQuiryService>().As<IInternationalQuiryService>(); //国际物流查询

            builder.RegisterType<OverseasDeliveryService>().As<IOverseasDeliveryService>(); //海外待收货

            builder.RegisterType<LogisticsQueryService>().As<ILogisticsQueryService>(); //物流综合查询

            builder.RegisterType<ExportCSVService>().As<IExportCSV>(); //导出csv

            //autofac 注册依赖
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}