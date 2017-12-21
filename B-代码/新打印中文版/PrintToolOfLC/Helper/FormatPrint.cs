using System;
using System.Xml;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;


namespace UDF.GUI.PrintControl
{
    public class FormatPrint
    {

        private SortedList m_PrintPoint_List = new SortedList();
        /// <summary>
        /// 从打印模版文件中读取点阵数据，并保存到Hash表中
        /// </summary>
        /// <param name="p_tempateFile"></param>
        public  void loadPrintTemplate(string p_tempateFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(p_tempateFile);
            XmlElement xmlNodePoints = xmlDoc["Points"];
            XmlNodeList nodelist = xmlNodePoints.ChildNodes;
            m_PrintPoint_List.Clear();
            foreach (XmlElement el in nodelist)//读元素值 
            {

                OnePrintPoint op = new OnePrintPoint();
                op.Key = el.Attributes["Key"].InnerText;
                op.FontName = el.Attributes["FontName"].InnerText;
                op.FontSize = float.Parse(el.Attributes["FontSize"].InnerText);
                op.X = float.Parse(el.Attributes["X"].InnerText);
                op.Y = float.Parse(el.Attributes["Y"].InnerText);
                m_PrintPoint_List.Add(op.Key.ToUpper(), op);
            }
        }
        //private FormatPrint()
        //{
        //}
        //public FormatPrint(string p_tempateFile)
        //{
        //    loadPrintTemplate(p_tempateFile);
        //}
        /// <summary>
        /// 设置打印点的值
        /// </summary>
        /// <param name="p_Key"></param>
        /// <param name="p_Value"></param>
        public void SetValue(string p_Key, string p_Value)
        {
            p_Key = p_Key.ToUpper();
            if (m_PrintPoint_List.Contains(p_Key))
            {
                OnePrintPoint tmpOnePrintPoint = (OnePrintPoint)m_PrintPoint_List[p_Key];
                tmpOnePrintPoint.Value = p_Value;
            }
            else
            {
               throw new Exception ("打印模版中不存在Key为" + p_Key+"的节点");
            }
        }
        /// <summary>
        /// 直接打印
        /// </summary>
 
        public void Print( )
        {
            Print(false);
        }
        /// <summary>
        /// 直接打印
        /// </summary>
        /// <param name="p_ShowPrintDialog"></param>
        public void Print(bool p_ShowPrintDialog)
        {
            try
            {
                PrintDocument pd = new PrintDocument();

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
        /// 预览打印
        /// </summary>
        public void Preview()
        {
            Preview(false );
        }
       /// <summary>
       /// 预览打印
       /// </summary>
       /// <param name="p_ShowPageSetup"></param>
        public void Preview(bool p_ShowPageSetup)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
               
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                if (p_ShowPageSetup)
                {
                    //   display   a   pagesetup   dialog   
                  
                    PageSetupDialog pageSetup = new PageSetupDialog();
                    pageSetup.Document = pd;
                    DialogResult Rc = pageSetup.ShowDialog();
                    if (Rc == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                previewDialog.Document = pd;
                previewDialog.ShowInTaskbar = true;   
                previewDialog.ShowDialog();
                //pd.Print();
            }
            finally
            {


            }
        }
        /// <summary>
        /// 选择打印机并打印
        /// </summary>
        public void DialogPrint()
        {
            try
            {

                PrintDocument pd = new PrintDocument();
                PrintDialog pdlg = new PrintDialog();
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                pdlg.Document = pd;
                DialogResult res = pdlg.ShowDialog();
                if (res == DialogResult.OK)
                    pd.Print();
            }
            finally
            {


            }
        }
        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {

            for (int i = 0; i < m_PrintPoint_List.Count; i++)
            {
                OnePrintPoint tmpOnePrintPoint = (OnePrintPoint)m_PrintPoint_List.GetByIndex(i);

                // Create string to draw.
                string drawString = tmpOnePrintPoint.Value;
                // Create font and brush.
                Font drawFont = new Font(tmpOnePrintPoint.FontName, tmpOnePrintPoint.FontSize);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                // Create point for upper-left corner of drawing.

                float x = tmpOnePrintPoint.X;
                float y = tmpOnePrintPoint.Y;
                // Set format of string.
                StringFormat drawFormat = new StringFormat();
                //drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                // Draw string to screen.
                ev.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            }

        }

    }
    /// <summary>
    /// 存储打印信息类
    /// </summary>
    class OnePrintPoint
    {
        public string Key;
        public float X;
        public float Y;
        public string FontName;
        public float FontSize;
        public string Value;
    }
}
