using System;
using System.Runtime.InteropServices;
//using System.Windows.Forms;


namespace Jade.PrintTool
{	
	/// <summary>
	/// Bpladll ��ժҪ˵����
	/// </summary>
	public class PrinterDll
	{
		public const int BPLA_OK             = 1000;     //һ������
		public const int BPLA_COMERROR       = 1001;     //ͨѶ�����δ���Ӵ�ӡ��
		public const int BPLA_PARAERROR      = 1002;     //��������
		public const int BPLA_FILEOPENERROR  = 1003;     //�ļ��򿪴���
		public const int BPLA_FILEREADERROR  = 1004;     //�ļ�������
		public const int BPLA_FILEWRITEERROR = 1005;     //�ļ�д����
		public const int BPLA_FILEERROR      = 1006;     //�ļ�����Ҫ��
		public const int BPLA_NUMBEROVER     = 1007;     //ָ���Ľ�����Ϣ��������
		public const int BPLA_IMAGETYPEERROR = 1008;     //ͼ���ļ���ʽ����ȷ 
        public const int BPLA_DRIVERERROR    = 1009;     //��������
        public const int BPLA_TIMEOUTERROR   = 1010;     //��ʱ����
        public const int BPLA_LOADDLLERROR   = 1011;     //���ض�̬��ʧ��
        public const int BPLA_LOADFUNCERROR  = 1012;     //���ض�̬�⺯��ʧ��
        public const int BPLA_NOOPENERROR    = 1013;     //�˿�δ��
		//static string m_dllpath = Application.StartupPath+@"\BPLADLL.dll";

        //�˿����ͺ궨��
        public const int BPLA_COM_PORT        = 0;
        public const int BPLA_LPT_PORT        = 1;
        public const int BPLA_API_USB_PORT    = 2;
        public const int BPLA_CLASS_USB_PORT  = 3;
        public const int BPLA_DRIVER_PORT     = 4;
        public const int BPLA_NET_PORT        = 5;

        //��ӡֽ�ź궨��
        public const int BPLA_CONTINUE_PAPER_PRINT = 0;
        public const int BPLA_LABEL_PAPER_PRINT    = 1;
        public const int BPLA_BLACK_PAPER_PRINT    = 2;
        public const int BPLA_CONTINUE_PRINT       = 3;

        //��ӡӦ��
        public const int BPLA_ADDVALUE_PRINT      = 0;
        public const int BPLA_ROLL_PRINT          = 1;
        public const int BPLA_DEEPNESS_PRINT      = 2;
        public const int BPLA_GRAY_PRINT          = 3;

        //��ӡ���ݺ궨��
        public const int BPLA_TEXT_PRINT     = 0;
        public const int BPLA_BARCODE_PRINT  = 1;
        public const int BPLA_IMAGE_PRINT    = 2;
        public const int BPLA_FIGURE_PRINT   = 3;


        //TrueType������ṹ�嶨��
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct TrueTypeFontStyle
        {        
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public bool Bold;//�Ӵ�
      
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public bool Italic;//��б

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public bool Underline;//�»���
        }



        //��ָ��浽�ļ�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetSaveFile")]
        public static extern int BPLA_SetSaveFile([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool bsave, string filename, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool bport);


        //��ȡ�汾
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_GetVersion")]
        public static extern int BPLA_GetVersion();


        //�����ô���
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenCom")]
        public static extern int BPLA_OpenCom(string comname, int intbaudrate, int handshake);


        //�����ô��ڣ����������Ӽ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenComEx")]
        public static extern int BPLA_OpenComEx(string PortName, int Baudrate, int Handshake, int WriteTimeOut);


        //�رմ���
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseCom")]
        public static extern int BPLA_CloseCom();


        //ͨ��API�򿪲���
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenLptByAPI")]
        public static extern int BPLA_OpenLptByAPI(string cLptName);


        //�ر�API����
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseLptByAPI")]
        public static extern int BPLA_CloseLptByAPI();

        //������˿�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenNetPort")]
        public static extern int BPLA_OpenNetPort(string cIpAddress, int iPort);


        //�ر�����˿�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseNetPort")]
        public static extern int BPLA_CloseNetPort();


        //����������
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenLpt")]
        public static extern int BPLA_OpenLpt(int address, int busySleep);


        //�ر���������
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseLpt")]
        public static extern int BPLA_CloseLpt();


        //��USB��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenUsb")]
        public static extern int BPLA_OpenUsb();


        //ͨ���ڲ�ID�Ŵ�USB��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenUsbByID")]
        public static extern int BPLA_OpenUsbByID(int ID);


        //�ر�USB��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseUsb")]
        public static extern int BPLA_CloseUsb();


        //ö��USB��ģʽ�豸����
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_EnumUsbPrn")]
        public static extern int BPLA_EnumUsbPrn(ref int iUsbPrnNum);


        //��USB��ģʽ�˿�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenUsbPrn")]
        public static extern int BPLA_OpenUsbPrn(int iDeviceNum);


        //�ر�USB��ģʽ�˿�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CloseUsbPrn")]
        public static extern int BPLA_CloseUsbPrn();


        //���ö˿�д��ʱʱ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetTimeOut")]
        public static extern int BPLA_SetTimeOut(int WriteTimeOut);


        //���ö˿ڶ�д��ʱʱ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetTimeOutEx")]
        public static extern int BPLA_SetTimeOutEx(int WriteTimeOut, int ReadTimeOut);


        //����ָ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SendCommand")]
        public static extern int BPLA_SendCommand(string command, int commandlength);


        //����ָ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ReceiveInfo")]
        public static extern int BPLA_ReceiveInfo(int relength, string buffer, ref int length, int time);


        //�����ļ� 
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SendFile")]
        public static extern int BPLA_SendFile(string filename);


        //�������ݵ��ļ�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ReceiveFile")]
        public static extern int BPLA_ReceiveFile(int relength, string filename, ref int length, int time);


        //����������
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_OpenPrinterDriver")]
        public static extern int BPLA_OpenPrinterDriver(string DriverName);


        //�ر���������
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ClosePrinterDriver")]
        public static extern int BPLA_ClosePrinterDriver();


        //��ʼ��ӡ��ҵ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_StartDoc")]
        public static extern int BPLA_StartDoc();


        //������ӡ��ҵ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_EndDoc")]
        public static extern int BPLA_EndDoc();


        //����λͼ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_DownloadImage")]
        public static extern int BPLA_DownloadImage(string imagename, int imagetype, int modeltype, string filename);


        //ɾ���洢��ģ����ָ�����ļ�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_DownErase")]
        public static extern int BPLA_DownErase(int modeltype, int filetype, string filename);


        //ɾ��ָ���洢��ģ���е�ȫ���ļ�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_DownEraseAll")]
        public static extern int BPLA_DownEraseAll(int modeltype);


        //��λ��ӡ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_Reset")]
        public static extern int BPLA_Reset();


        //ִ�н�/�˱�ǩ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_ForBack")]
        public static extern int BPLA_ForBack(int distance, int delaytime);


        //���ó�ֽ��ʽ��ֽ�����͡�����ģʽ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_Set")]
        public static extern int BPLA_Set(int outmode, int papermode, int printmode);


        //���ô�����ģʽ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetSensor")]
        public static extern int BPLA_SetSensor(int labelmode);


        //�����������ʴ�ӡ����
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetPaperLength")]
        public static extern int BPLA_SetPaperLength(int continuelength, int labellength);


        //���ô�ӡֹͣλ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetEnd")]
        public static extern int BPLA_SetEnd(int position);

        //���ûҶ�ģʽ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetGrayMode")]
        public static extern int BPLA_SetGrayMode(int mode);


        //�����ǩģʽ�����ô�ӡ���򼰴�ӡ����
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_StartArea")]
        public static extern int BPLA_StartArea(int unitmode, int printwidth, int column, int row, int darkness, int speedprint, int speedfor, int speedbac);


        //����Ʊ����ĳЩ��ɾ���Ч������������Ч
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetMirror")]
        public static extern int BPLA_SetMirror();


        //���徵�� 
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetAllMirror")]
        public static extern int BPLA_SetAllMirror();


        //������ӡ��ǩ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_Print")]
        public static extern int BPLA_Print(int pieces, int samepieces, int outunit);


        //�����ǩ����ӡ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SaveLabel")]
        public static extern int BPLA_SaveLabel();


        //��ӡ�Ѿ�����ı�ǩ����֧������������
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintSaveLabel")]
        public static extern int BPLA_PrintSaveLabel(int pieces);


        //������ 
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetCopy")]
        public static extern int BPLA_SetCopy(int pieces, int gap);


        //���巭ת
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_SetAllRotate")]
        public static extern int BPLA_SetAllRotate(int rotatemode);


        //�����߶εİ���λ�ã����ø���ģʽ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintLine")]
        public static extern int BPLA_PrintLine(int startx, int starty, int endx, int endy, int linewidth);


        //���þ��εİ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintBox")]
        public static extern int BPLA_PrintBox(int startx, int starty, int width, int height, int horizontal, int vertical, int bitmode);


        //����Բ�εİ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintCircle")]
        public static extern int BPLA_PrintCircle(int centerx, int centery, int radius, int linewidth, int bitmode);


        //����Ԥ����ͼ��İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_LoadImage")]
        public static extern int BPLA_LoadImage(string imagename, int startx, int starty, int pointwidth, int pointheight, int bitmode);


        //����ֱ������ͼ��İ���λ�ã�֧��BMP��ɫλͼ��Ҫ��λͼ�Ŀ�ȵ���Ϊ32�ı���
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintImage")]
        public static extern int BPLA_PrintImage(string imagename, int startx, int starty, int bitmode);

        //����ֱ�����ػҶ�ͼ��İ���λ�ã�֧��8λBMP�Ҷ�λͼ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintGrayImage")]
        public static extern int BPLA_PrintGrayImage(string imagename);


        //��ӡ�ڲ���������
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintText")]
        public static extern int BPLA_PrintText(string text, int startx, int starty, int rotate, int fonttype, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //��ӡ�ڲ���������
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTextEx")]
        public static extern int BPLA_PrintTextEx(string text, int startx, int starty, int rotate, int fonttype, int pointwidth, int pointheight, int space, int bitmode, string addvalue, int iValueStartPlace, int iValueLen);


        //��ӡ�ⲿ���ص�RAM��FLASH�е���չ���塣
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintOut")]
        public static extern int BPLA_PrintOut(string text, int startx, int starty, int rotate, string fonttype, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //��ӡ�ⲿ���ص�RAM��FLASH�е���չ���塣
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintOutEx")]
        public static extern int BPLA_PrintOutEx(string text, int startx, int starty, int rotate, string fonttype, int pointwidth, int pointheight, int space, int bitmode);


        //��Ӣ�Ļ��Ŵ�ӡ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMixText")]
        public static extern int BPLA_PrintMixText(string text, int startx, int en_starty, int cn_starty, int rotate, int en_fonttype, string cn_fonttype, int en_width, int cn_width, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //��Ӣ�Ļ��Ŵ�ӡ
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


        //��Ӣ�Ļ��Ŵ�ӡ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMixTextEx")]
        public static extern int BPLA_PrintMixTextEx(string text, int startx, int cn_starty, int xy_adjust, int rotate, string en_fonttype, string cn_fonttype, int pointwidth, int pointheight, string addvalue, int space, int bitmode);


        //��Ӣ�Ļ��Ŵ�ӡ
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMixTextCmd")]
        public static extern int BPLA_PrintMixTextCmd(string text, int startx, int cn_starty, int xy_adjust, int rotate, string en_fonttype, string cn_fonttype, int pointwidth, int pointheight, int space, int bitmode, string addvalue, int iValueStartPlace, int iValueLen);


        //��ӡTRUETYPE����
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTruetype")]
        public static extern int BPLA_PrintTruetype(string text, int startx, int starty, string fontname, int fontheight, int fontwidth);


        //��ӡTRUETYPE����
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTruetypeEx")]
        public static extern int BPLA_PrintTruetypeEx(string text, int startx, int starty, string fontname, int fontheight, int fontwidth, int rowrotate);


        //��ӡTRUETYPE����
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintTruetypeStyle")]
        public static extern int BPLA_PrintTruetypeStyle(string text, int startx, int starty, string fontname, int fontheight, int fontwidth, ref TrueTypeFontStyle sStyle, int rowrotate);


        //����һά����İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintBarcode")]
        public static extern int BPLA_PrintBarcode(string codedata, int startx, int starty, int rotate, int bartype, int height, int number, int numberbase, string addvalue);


        //����һά����İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintBarcodeEx")]
        public static extern int BPLA_PrintBarcodeEx(string codedata, int startx, int starty, int rotate, int bartype, int height, int number, int numberbase, string addvalue, int iValueStartPlace, int iValueLen);


        //����PDF417��İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintPDF")]
        public static extern int BPLA_PrintPDF(string codedata, int startx, int starty, int rotate, int basewidth, int baseheight, int scalewidth, int scaleheight, int row, int column, int cutmode, int level, int length, string addvalue);


        //����PDF417��İ���λ��
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


        //����Maxicode��İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMaxi")]
        public static extern int BPLA_PrintMaxi(string codedata, int startx, int starty, string addvalue);


        //����Maxicode��İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintMaxiEx")]
        public static extern int BPLA_PrintMaxiEx(string codedata, int startx, int starty, string addvalue, int iValueStartPlace, int iValueLen);


        //����QR��İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintQR")]
        public static extern int BPLA_PrintQR(string codedata, int startx, int starty, int weigth, int symboltype, int languagemode, int number);


        //����DataMatrix��İ���λ��
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_PrintDatamatrix")]
        public static extern int BPLA_PrintDatamatrix(string codedata, int startx, int starty, int weigth, int reversecolor, int shape, int number);


        //���Դ���
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CheckCom")]
        public static extern int BPLA_CheckCom();


        //����״̬
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CheckStatus")]
        public static extern int BPLA_CheckStatus(byte[] papershort, byte[] ribbionshort, byte[] busy, byte[] pause, byte[] com, byte[] headheat, byte[] headover, byte[] cut);


        //�����е�
        [System.Runtime.InteropServices.DllImportAttribute("BPLADLL.dll", EntryPoint = "BPLA_CheckCut")]
        public static extern int BPLA_CheckCut();

	}
}
