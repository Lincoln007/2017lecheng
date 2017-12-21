using System;
using System.Runtime.InteropServices;
//using System.Windows.Forms;


namespace Jade.PrintTool
{	
	/// <summary>
	/// Bpladll 的摘要说明。
	/// </summary>
	public class PrinterDll
	{
		public const int BPLA_OK             = 1000;     //一切正常
		public const int BPLA_COMERROR       = 1001;     //通讯错或者未联接打印机
		public const int BPLA_PARAERROR      = 1002;     //参数错误
		public const int BPLA_FILEOPENERROR  = 1003;     //文件打开错误
		public const int BPLA_FILEREADERROR  = 1004;     //文件读错误
		public const int BPLA_FILEWRITEERROR = 1005;     //文件写错误
		public const int BPLA_FILEERROR      = 1006;     //文件不合要求
		public const int BPLA_NUMBEROVER     = 1007;     //指定的接收信息数量过大
		public const int BPLA_IMAGETYPEERROR = 1008;     //图象文件格式不正确 
        public const int BPLA_DRIVERERROR    = 1009;     //驱动错误
        public const int BPLA_TIMEOUTERROR   = 1010;     //超时错误
        public const int BPLA_LOADDLLERROR   = 1011;     //加载动态库失败
        public const int BPLA_LOADFUNCERROR  = 1012;     //加载动态库函数失败
        public const int BPLA_NOOPENERROR    = 1013;     //端口未打开
		//static string m_dllpath = Application.StartupPath+@"\BPLADLL.dll";

        //端口类型宏定义
        public const int BPLA_COM_PORT        = 0;
        public const int BPLA_LPT_PORT        = 1;
        public const int BPLA_API_USB_PORT    = 2;
        public const int BPLA_CLASS_USB_PORT  = 3;
        public const int BPLA_DRIVER_PORT     = 4;
        public const int BPLA_NET_PORT        = 5;

        //打印纸张宏定义
        public const int BPLA_CONTINUE_PAPER_PRINT = 0;
        public const int BPLA_LABEL_PAPER_PRINT    = 1;
        public const int BPLA_BLACK_PAPER_PRINT    = 2;
        public const int BPLA_CONTINUE_PRINT       = 3;

        //打印应用
        public const int BPLA_ADDVALUE_PRINT      = 0;
        public const int BPLA_ROLL_PRINT          = 1;
        public const int BPLA_DEEPNESS_PRINT      = 2;
        public const int BPLA_GRAY_PRINT          = 3;

        //打印内容宏定义
        public const int BPLA_TEXT_PRINT     = 0;
        public const int BPLA_BARCODE_PRINT  = 1;
        public const int BPLA_IMAGE_PRINT    = 2;
        public const int BPLA_FIGURE_PRINT   = 3;


        //TrueType字体风格结构体定义
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct TrueTypeFontStyle
        {        
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public bool Bold;//加粗
      
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public bool Italic;//倾斜

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public bool Underline;//下划线
        }



        //将指令保存到文件
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetSaveFile")]
        public static extern int BPLA_SetSaveFile([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool bsave, string filename, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool bport);


        //获取版本
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_GetVersion")]
        public static extern int BPLA_GetVersion();


        //打开配置串口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenCom")]
        public static extern int BPLA_OpenCom(string comname, int intbaudrate, int handshake);


        //打开配置串口，不进行连接检查
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenComEx")]
        public static extern int BPLA_OpenComEx(string PortName, int Baudrate, int Handshake, int WriteTimeOut);


        //关闭串口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseCom")]
        public static extern int BPLA_CloseCom();


        //通过API打开并口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenLptByAPI")]
        public static extern int BPLA_OpenLptByAPI(string cLptName);


        //关闭API并口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseLptByAPI")]
        public static extern int BPLA_CloseLptByAPI();

        //打开网络端口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenNetPort")]
        public static extern int BPLA_OpenNetPort(string cIpAddress, int iPort);


        //关闭网络端口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseNetPort")]
        public static extern int BPLA_CloseNetPort();


        //打开驱动并口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenLpt")]
        public static extern int BPLA_OpenLpt(int address, int busySleep);


        //关闭驱动并口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseLpt")]
        public static extern int BPLA_CloseLpt();


        //打开USB口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenUsb")]
        public static extern int BPLA_OpenUsb();


        //通过内部ID号打开USB口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenUsbByID")]
        public static extern int BPLA_OpenUsbByID(int ID);


        //关闭USB口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseUsb")]
        public static extern int BPLA_CloseUsb();


        //枚举USB类模式设备数量
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_EnumUsbPrn")]
        public static extern int BPLA_EnumUsbPrn(ref int iUsbPrnNum);


        //打开USB类模式端口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenUsbPrn")]
        public static extern int BPLA_OpenUsbPrn(int iDeviceNum);


        //关闭USB类模式端口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseUsbPrn")]
        public static extern int BPLA_CloseUsbPrn();


        //设置端口写超时时间
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetTimeOut")]
        public static extern int BPLA_SetTimeOut(int WriteTimeOut);


        //设置端口读写超时时间
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetTimeOutEx")]
        public static extern int BPLA_SetTimeOutEx(int WriteTimeOut, int ReadTimeOut);


        //发送指令
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SendCommand")]
        public static extern int BPLA_SendCommand(string command, int commandlength);


        //接收指令
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ReceiveInfo")]
        public static extern int BPLA_ReceiveInfo(int relength, string buffer, ref int length, int time);


        //发送文件 
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SendFile")]
        public static extern int BPLA_SendFile(string filename);


        //接收数据到文件
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ReceiveFile")]
        public static extern int BPLA_ReceiveFile(int relength, string filename, ref int length, int time);


        //打开驱动程序
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenPrinterDriver")]
        public static extern int BPLA_OpenPrinterDriver(string DriverName);


        //关闭驱动程序
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ClosePrinterDriver")]
        public static extern int BPLA_ClosePrinterDriver();


        //开始打印作业
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_StartDoc")]
        public static extern int BPLA_StartDoc();


        //结束打印作业
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_EndDoc")]
        public static extern int BPLA_EndDoc();


        //下载位图
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_DownloadImage")]
        public static extern int BPLA_DownloadImage(string imagename, int imagetype, int modeltype, string filename);


        //删除存储器模块中指定的文件
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_DownErase")]
        public static extern int BPLA_DownErase(int modeltype, int filetype, string filename);


        //删除指定存储器模块中的全部文件
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_DownEraseAll")]
        public static extern int BPLA_DownEraseAll(int modeltype);


        //复位打印机
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_Reset")]
        public static extern int BPLA_Reset();


        //执行进/退标签
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ForBack")]
        public static extern int BPLA_ForBack(int distance, int delaytime);


        //设置出纸方式、纸张类型、工作模式
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_Set")]
        public static extern int BPLA_Set(int outmode, int papermode, int printmode);


        //设置传感器模式
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetSensor")]
        public static extern int BPLA_SetSensor(int labelmode);


        //设置连续介质打印长度
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetPaperLength")]
        public static extern int BPLA_SetPaperLength(int continuelength, int labellength);


        //设置打印停止位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetEnd")]
        public static extern int BPLA_SetEnd(int position);

        //设置灰度模式
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetGrayMode")]
        public static extern int BPLA_SetGrayMode(int mode);


        //进入标签模式，设置打印区域及打印参数
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_StartArea")]
        public static extern int BPLA_StartArea(int unitmode, int printwidth, int column, int row, int darkness, int speedprint, int speedfor, int speedbac);


        //设置票面内某些域成镜象效果，对条码无效
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetMirror")]
        public static extern int BPLA_SetMirror();


        //整体镜象 
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetAllMirror")]
        public static extern int BPLA_SetAllMirror();


        //启动打印标签
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_Print")]
        public static extern int BPLA_Print(int pieces, int samepieces, int outunit);


        //保存标签不打印
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SaveLabel")]
        public static extern int BPLA_SaveLabel();


        //打印已经保存的标签，不支持连续域设置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintSaveLabel")]
        public static extern int BPLA_PrintSaveLabel(int pieces);


        //横向复制 
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetCopy")]
        public static extern int BPLA_SetCopy(int pieces, int gap);


        //整体翻转
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetAllRotate")]
        public static extern int BPLA_SetAllRotate(int rotatemode);


        //设置线段的版面位置，采用覆盖模式
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintLine")]
        public static extern int BPLA_PrintLine(int startx, int starty, int endx, int endy, int linewidth);


        //设置矩形的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintBox")]
        public static extern int BPLA_PrintBox(int startx, int starty, int width, int height, int horizontal, int vertical, int bitmode);


        //设置圆形的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintCircle")]
        public static extern int BPLA_PrintCircle(int centerx, int centery, int radius, int linewidth, int bitmode);


        //设置预下载图象的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_LoadImage")]
        public static extern int BPLA_LoadImage(string imagename, int startx, int starty, int pointwidth, int pointheight, int bitmode);


        //设置直接下载图象的版面位置，支持BMP单色位图，要求位图的宽度点数为32的倍数
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintImage")]
        public static extern int BPLA_PrintImage(string imagename, int startx, int starty, int bitmode);

        //设置直接下载灰度图象的版面位置，支持8位BMP灰度位图
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintGrayImage")]
        public static extern int BPLA_PrintGrayImage(string imagename);


        //打印内部点阵字体
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintText")]
        public static extern int BPLA_PrintText(string text, int startx, int starty, int rotate, int fonttype, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //打印内部点阵字体
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTextEx")]
        public static extern int BPLA_PrintTextEx(string text, int startx, int starty, int rotate, int fonttype, int pointwidth, int pointheight, int space, int bitmode, string addvalue, int iValueStartPlace, int iValueLen);


        //打印外部下载到RAM或FLASH中的扩展字体。
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintOut")]
        public static extern int BPLA_PrintOut(string text, int startx, int starty, int rotate, string fonttype, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //打印外部下载到RAM或FLASH中的扩展字体。
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintOutEx")]
        public static extern int BPLA_PrintOutEx(string text, int startx, int starty, int rotate, string fonttype, int pointwidth, int pointheight, int space, int bitmode);


        //中英文混排打印
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMixText")]
        public static extern int BPLA_PrintMixText(string text, int startx, int en_starty, int cn_starty, int rotate, int en_fonttype, string cn_fonttype, int en_width, int cn_width, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //中英文混排打印
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMixTextBuild")]
        public static extern int BPLA_PrintMixTextBuild(
                        string text,
                        int startx,
                        int en_starty,
                        int cn_starty,
                        int rotate,
                        int en_fonttype,
                        string cn_fonttype,
                        int en_width,
                        int cn_width,
                        int pointwidth,
                        int pointheight,
                        int space,
                        int bitmode,
                        string addvalue,
                        int iValueStartPlace,
                        int iValueLen);


        //中英文混排打印
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMixTextEx")]
        public static extern int BPLA_PrintMixTextEx(string text, int startx, int cn_starty, int xy_adjust, int rotate, string en_fonttype, string cn_fonttype, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //中英文混排打印
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMixTextCmd")]
        public static extern int BPLA_PrintMixTextCmd(string text, int startx, int cn_starty, int xy_adjust, int rotate, string en_fonttype, string cn_fonttype, int pointwidth, int pointheight, int space, int bitmode, string addvalue, int iValueStartPlace, int iValueLen);


        //打印TRUETYPE字体
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTruetype")]
        public static extern int BPLA_PrintTruetype(string text, int startx, int starty, string fontname, int fontheight, int fontwidth);


        //打印TRUETYPE字体
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTruetypeEx")]
        public static extern int BPLA_PrintTruetypeEx(string text, int startx, int starty, string fontname, int fontheight, int fontwidth, int rowrotate);


        //打印TRUETYPE字体
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTruetypeStyle")]
        public static extern int BPLA_PrintTruetypeStyle(string text, int startx, int starty, string fontname, int fontheight, int fontwidth, ref TrueTypeFontStyle sStyle, int rowrotate);


        //设置一维条码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintBarcode")]
        public static extern int BPLA_PrintBarcode(string codedata, int startx, int starty, int rotate, int bartype, int height, int number, int numberbase, string addvalue);


        //设置一维条码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintBarcodeEx")]
        public static extern int BPLA_PrintBarcodeEx(string codedata, int startx, int starty, int rotate, int bartype, int height, int number, int numberbase, string addvalue, int iValueStartPlace, int iValueLen);


        //设置PDF417码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintPDF")]
        public static extern int BPLA_PrintPDF(string codedata, int startx, int starty, int rotate, int basewidth, int baseheight, int scalewidth, int scaleheight, int row, int column, int cutmode, int level, int length, string addvalue);


        //设置PDF417码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintPDFEx")]
        public static extern int BPLA_PrintPDFEx(
                        string codedata,
                        int startx,
                        int starty,
                        int rotate,
                        int basewidth,
                        int baseheight,
                        int scalewidth,
                        int scaleheight,
                        int row,
                        int column,
                        int cutmode,
                        int level,
                        int length,
                        string addvalue,
                        int iValueStartPlace,
                        int iValueLen);


        //设置Maxicode码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMaxi")]
        public static extern int BPLA_PrintMaxi(string codedata, int startx, int starty, string addvalue);


        //设置Maxicode码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMaxiEx")]
        public static extern int BPLA_PrintMaxiEx(string codedata, int startx, int starty, string addvalue, int iValueStartPlace, int iValueLen);


        //设置QR码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintQR")]
        public static extern int BPLA_PrintQR(string codedata, int startx, int starty, int weigth, int symboltype, int languagemode, int number);


        //设置DataMatrix码的版面位置
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintDatamatrix")]
        public static extern int BPLA_PrintDatamatrix(string codedata, int startx, int starty, int weigth, int reversecolor, int shape, int number);


        //测试串口
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CheckCom")]
        public static extern int BPLA_CheckCom();


        //测试状态
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CheckStatus")]
        public static extern int BPLA_CheckStatus(byte[] papershort, byte[] ribbionshort, byte[] busy, byte[] pause, byte[] com, byte[] headheat, byte[] headover, byte[] cut);


        //测试切刀
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CheckCut")]
        public static extern int BPLA_CheckCut();

	}
}
