using BarcodeLib;
using Printer.CommHelper;
using Printer.DBmodel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Printer.Service
{
    public class Printselect
    {
        //public Myselect packge;
        public static List<Myselect> mylist = new List<Myselect>();


        public Printselect(Myselect pack)
        {
            //packge = pack;
            mylist.Add(pack);

        }
        /// <summary>
        /// 直接打印,是否显示打印对话框
        /// </summary>
        /// <param name="p_ShowPrintDialog"></param>
        public static void Print(bool p_ShowPrintDialog)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                //1.获取打印机插件是否设置了打印拣选单的面单
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "Passport");
                bool isexit = xmlfile.IsExitinnertextOfNode("拣货单", "page");
                if (isexit)
                {
                    //设置打印机的名称
                    pd.PrinterSettings.PrinterName = xmlfile.GetinnertextOfPrintName("拣货单", "page");
                }
                else
                {
                    throw new Exception("没有配置拣货单面单！请先配置！");
                }
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                if (p_ShowPrintDialog)
                {
                    PrintDialog pdlg = new PrintDialog();
                    pdlg.Document = pd;
                    DialogResult res = pdlg.ShowDialog();
                    if (res == DialogResult.OK)
                        pd.Print();
                }
                else
                {
                    pd.Print();
                }
            }
            finally
            {


            }
        }

        /// <summary>
        /// 打印页面的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Point ptS;
            Point ptE;
            List<Myselect> mylist2 = new List<Myselect>(mylist.ToArray()); //复制到另一个集合
            List<Myselect> dic = new List<Myselect>();
            List<string> pac = new List<string>();
            //打印条码  201510270066
            //Barcode bar = new Barcode();
            e.Graphics.DrawString("配货拣选单", new System.Drawing.Font("黑体", 15f), System.Drawing.Brushes.Black, new System.Drawing.PointF(350f, 50f));
            ptS = new Point(70, 100);
            ptE = new Point(730, 100);
            //先画标题
            e.Graphics.DrawLine(Pens.Black, ptS, ptE);
            e.Graphics.DrawLine(Pens.Black, new Point(ptS.X, ptS.Y + 30), new Point(ptE.X, ptE.Y + 30));
            e.Graphics.DrawString("包裹号", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(115, 110));
            e.Graphics.DrawString("SKU", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(300, 110f));
            e.Graphics.DrawString("数量", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(420, 110f));
            e.Graphics.DrawString("货位号", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(500, 110f));
            e.Graphics.DrawString("备注", new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(650f, 110f));
            //e.Graphics.DrawString(time, new System.Drawing.Font("黑体", 7f), System.Drawing.Brushes.Black, new System.Drawing.PointF(280f, 135f));

            int y = 135;
            int objY = 130;   //整体对象高度
            int LineY = 130;
            int codeY = 175;
            Image Image = null;
            string code = string.Empty;
            for (int j = 0; j < mylist.Count; j++)   //(Myselect select in mylist)   
            {
                if (mylist2.Contains(mylist[j]) && !pac.Contains(mylist[j].Packgecode))  //
                {
                    //2.查询包裹号相同的行

                    var result = mylist2.Where(p => p.Packgecode == mylist[j].Packgecode);
                    foreach (Myselect dr in result)
                    {
                        dic.Add(dr);
                        pac.Add(dr.Packgecode);
                        //mylist2.Remove(dr);
                    }
                    if (3 < dic.Count)//超过条码高度的处理
                    {
                        for (int h = 0; h < dic.Count; h++)
                        {
                            code = dic[h].Packgecode;
                            Barcode bar = new Barcode();
                            Image = bar.Encode(TYPE.CODE128, dic[h].Packgecode, 130, 40);
                            //2.先画一行SKU信息
                            e.Graphics.DrawString(dic[h].Skus, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(280f, y));
                            e.Graphics.DrawString(dic[h].Number, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(420f, y));
                            e.Graphics.DrawString(dic[h].Position, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(500f, y));
                            e.Graphics.DrawString(dic[h].Remarks1, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(640f, y));
                            e.Graphics.DrawLine(Pens.Black, new Point(260, LineY + 20), new Point(730, LineY + 20));
                            y = y + 20;
                            LineY = LineY + 20;
                        }
                        e.Graphics.DrawLine(Pens.Black, new Point(70, objY + 20 * dic.Count), new Point(270, objY + 20 * dic.Count));
                        System.Drawing.Point pt = new System.Drawing.Point(80, codeY - 40);
                        e.Graphics.DrawImage(Image, pt);
                        e.Graphics.DrawString(code, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(105f, codeY));
                        objY = objY + 20 * dic.Count;
                        codeY = codeY + 20 * dic.Count;
                        LineY = objY;
                        y = LineY + 5;
                    }
                    else if (3 >= dic.Count)//小于等于三个
                    {
                        for (int h = 0; h < dic.Count; h++)
                        {
                            code = dic[h].Packgecode;
                            Barcode bar = new Barcode();
                            Image = bar.Encode(TYPE.CODE128, dic[h].Packgecode, 130, 40);
                            //2.先画一行SKU信息
                            e.Graphics.DrawString(dic[h].Skus, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(280f, y));
                            e.Graphics.DrawString(dic[h].Number, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(420f, y));
                            e.Graphics.DrawString(dic[h].Position, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(500f, y));
                            e.Graphics.DrawString(dic[h].Remarks1, new System.Drawing.Font("黑体", 9f), System.Drawing.Brushes.Black, new System.Drawing.PointF(640f, y));
                            e.Graphics.DrawLine(Pens.Black, new Point(260, LineY + 20), new Point(730, LineY + 20));
                            y = y + 20;
                            LineY = LineY + 20;
                        }
                        e.Graphics.DrawLine(Pens.Black, new Point(70, objY + 60), new Point(730, objY + 60));
                        System.Drawing.Point pt = new System.Drawing.Point(80, codeY - 40);
                        e.Graphics.DrawImage(Image, pt);
                        //打印条码代号
                        e.Graphics.DrawString(code, new System.Drawing.Font("黑体", 10f), System.Drawing.Brushes.Black, new System.Drawing.PointF(105f, codeY));
                        objY = objY + 60;
                        codeY = codeY + 60;
                        LineY = objY;
                        y = LineY + 5;
                    }
                    dic.Clear();
                }
                else
                {

                }
            }
            e.HasMorePages = false;
        }

    }
}
