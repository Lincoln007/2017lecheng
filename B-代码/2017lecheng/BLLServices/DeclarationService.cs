using DBModel.Common;
using DBModel.DBmodel;
using IBLLService;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SqlSugarRepository;
using System;
using System.Data;
using System.Drawing;


namespace BLLServices
{
    public class DeclarationService : IDeclaration
    {
        /// <summary>
        /// 打印到excel
        /// </summary>
        /// <param name="table"></param>
        /// <param name="SavePath"></param>
        /// <param name="yundan"></param>
        public void Printer(System.Data.DataTable table, string SavePath, string yundan)
        {
            //---------------------------------****20170911NPOI重写excel文件生成*****---------------------------------------------
            //HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            //if (hssfworkbook == null)
            //{
            //    throw new Exception("无法创建NPOI");
            //}
            //// 新建一个Excel页签
            //ISheet sheet = hssfworkbook.CreateSheet("报关单");
            //IRow row = sheet.CreateRow(0); //创建sheet页的第0行（索引从0开始） 
            //row.CreateCell(0, CellType.String).SetCellValue("发票声明书");//创建第0行第0列 
            ///-----------------------------------------------------------------------------------------------------------
            //创建一个Excel应用程序对象，如果未创建成功则推出。
            Microsoft.Office.Interop.Excel.Application excel1 = new Microsoft.Office.Interop.Excel.Application();
            if (excel1 == null)
            {
                throw new Exception("无法创建Excel对象，可能你的电脑未装Excel");       
            }          
            Microsoft.Office.Interop.Excel.Workbooks workBooks1 = excel1.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workBook1 = workBooks1.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook1.Worksheets[1]; //取得sheet1          
            //worksheet.PageSetup.Zoom = 63;           //打印时页面设置,缩放比例
            //把DataTable的表头导入到Excel的第一行
            Microsoft.Office.Interop.Excel.Range xlsRow100 = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[3000, 20]];
            Microsoft.Office.Interop.Excel.Range xlsRow99 = worksheet.Range[worksheet.Cells[4, 8], worksheet.Cells[3000, 8]];
            xlsRow99.ColumnWidth = 12;  //设置列宽度
            xlsRow100.Font.Name = "Arial";//设置字体 
            xlsRow100.Font.Bold = true;//加粗显示
            xlsRow100.Font.Size = 10;//字体大小  
            Microsoft.Office.Interop.Excel.Range xlsRow = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 11]];
            xlsRow.Merge(true);    //合并单元格
            xlsRow.Value = "发票声明书";
            xlsRow.Font.Name = "Arial";//设置字体  
            xlsRow.Font.Size = 18;//字体大小  
            xlsRow.Font.Bold = true;//加粗显示
            xlsRow.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//水平居中  
            xlsRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//垂直居中 
            //xlsRow.Interior.Color = "255,255,0";   //设置背景颜色
            //xlsRow.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;//设置边框  
            //xlsRow.Borders.Weight = Excel.XlBorderWeight.xlMedium;//边框常规粗细 
            Microsoft.Office.Interop.Excel.Range xlsRow1 = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, 11]];
            xlsRow1.Merge(true);  //合并单元格
            xlsRow1.Value = "INVOICE STATEMENT ";
            xlsRow1.Font.Name = "Arial";//设置字体  
            xlsRow1.Font.Size = 18;//字体大小  
            xlsRow1.Font.Bold = true;//加粗显示
            xlsRow1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//水平居中  
            xlsRow1.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//垂直居中 
            Microsoft.Office.Interop.Excel.Range xlsRow2 = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[11, 2]];
            xlsRow2.Interior.Color = Color.FromArgb(255, 255, 153);
            worksheet.Cells[4, 2] = "收件人"; //Excel单元格赋值
            worksheet.Cells[5, 2] = "Consignee:"; //Excel单元格赋值
            worksheet.Cells[6, 2] = "地址:"; //Excel单元格赋值
            worksheet.Cells[10, 2] = "邮编:"; //Excel单元格赋值
            worksheet.Cells[7, 2] = "Address:"; //Excel单元格赋值
            worksheet.Cells[11, 2] = "Zip Code"; //Excel单元格赋值
            worksheet.Cells[4, 7] = "公司名称"; //Excel单元格赋值 YANG CHEN WEI
            worksheet.Cells[5, 7] = "Company Name:";
            worksheet.Cells[5, 8] = "NICCHU SHOMU K.K";  
            worksheet.Cells[7, 8] = "O48";
            worksheet.Cells[9, 8] = "080-4730-8688";
            worksheet.Cells[11, 8] = "JAPAN";
            worksheet.Cells[6, 7] = "城市/地区号:";
            worksheet.Cells[7, 7] = "Town/Area Code";
            worksheet.Cells[8, 7] = "电话/传真:";
            worksheet.Cells[9, 7] = "Phone/Fax:";
            worksheet.Cells[10, 7] = "州名/国家:";
            worksheet.Cells[11, 7] = "State/Country:";
            worksheet.Cells[5, 3] = "YANG CHEN WEI";
            worksheet.Cells[7, 3] = "1-3-4-2F ";
            worksheet.Cells[8, 3] = "TODA SHI.SAITAMA ";
            worksheet.Cells[9, 3] = "KEN.JAPAN";
            worksheet.Cells[11, 3] = "335-0016";

            xlsRow2.ColumnWidth = 30;
            xlsRow2.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;//设置边框
            xlsRow2.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;              //边框常规粗细
            xlsRow2.Font.Name = "Arial";//设置字体
            xlsRow2.Font.Bold = true;//加粗显示
            Microsoft.Office.Interop.Excel.Range xlsRow3 = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[11, 1]];
            xlsRow3.ColumnWidth = 2;  //设置列宽度
            Microsoft.Office.Interop.Excel.Range xlsRow4 = worksheet.Range[worksheet.Cells[4, 2], worksheet.Cells[4, 11]];
            xlsRow4.RowHeight = 25;   //设置行高度
            Microsoft.Office.Interop.Excel.Range xlsRow5 = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[11, 11]];
            xlsRow5.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;         //设置边框
            xlsRow5.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;              //边框常规粗细
            Microsoft.Office.Interop.Excel.Range xlsRow6 = worksheet.Range[worksheet.Cells[4, 7], worksheet.Cells[11, 7]];
            xlsRow6.Interior.Color = Color.FromArgb(255, 255, 153);
            xlsRow6.ColumnWidth = 20;
            xlsRow6.Font.Name = "Arial";//设置字体 
            xlsRow6.Font.Bold = true;//加粗显示
            Microsoft.Office.Interop.Excel.Range xlsRow7 = worksheet.Range[worksheet.Cells[13, 2], worksheet.Cells[14, 2]];
            xlsRow7.Font.Name = "Arial";//设置字体
            xlsRow7.Font.Bold = true;//加粗显示
            xlsRow7.Interior.Color = Color.FromArgb(255, 255, 153);
            worksheet.Cells[13, 2] = "运单号：";
            worksheet.Cells[14, 2] = "Airway Bill No：";
            worksheet.Cells[14, 3] = yundan;
            xlsRow7.ColumnWidth = 30;
            xlsRow7.RowHeight = 25;
            Microsoft.Office.Interop.Excel.Range xlsRow8 = worksheet.Range[worksheet.Cells[4, 3], worksheet.Cells[14, 3]];
            xlsRow8.ColumnWidth = 18;
            Microsoft.Office.Interop.Excel.Range xlsRow9 = worksheet.Range[worksheet.Cells[4, 4], worksheet.Cells[11, 4]];
            xlsRow9.ColumnWidth = 2;
            Microsoft.Office.Interop.Excel.Range xlsRow10 = worksheet.Range[worksheet.Cells[4, 6], worksheet.Cells[11, 6]];
            xlsRow10.ColumnWidth = 2;
            worksheet.Cells[16, 2] = "详细的商品名称";
            worksheet.Cells[16, 3] = "海关商品编码";
            worksheet.Cells[16, 4] = "生产厂商";
            worksheet.Cells[16, 6] = "重量";
            worksheet.Cells[16, 8] = "体积";
            worksheet.Cells[16, 9] = "数量";
            worksheet.Cells[16, 10] = "单价";
            worksheet.Cells[16, 11] = "报关总价";
            Microsoft.Office.Interop.Excel.Range xlsRow11 = worksheet.Range[worksheet.Cells[16, 2], worksheet.Cells[16, 11]];
            xlsRow11.Interior.Color = Color.FromArgb(255, 255, 153);
            xlsRow11.Font.Name = "Arial";//设置字体
            Microsoft.Office.Interop.Excel.Range xlsRow12 = worksheet.Range[worksheet.Cells[16, 4], worksheet.Cells[16, 5]];
            xlsRow12.Merge(true);    //合并单元格
            Microsoft.Office.Interop.Excel.Range xlsRow13 = worksheet.Range[worksheet.Cells[16, 6], worksheet.Cells[16, 7]];
            xlsRow13.Merge(true);    //合并单元格
            xlsRow11.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;         //设置边框
            xlsRow11.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;              //边框常规粗细Full Description of  Weight
            xlsRow11.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;        //水平居中  
            xlsRow11.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;          //垂直居中
            worksheet.Cells[17, 2] = "Full Description of";
            worksheet.Cells[17, 3] = "Harmonised";
            worksheet.Cells[17, 7] = "Weight";
            worksheet.Cells[17, 8] = "Dimensions";
            worksheet.Cells[17, 9] = "No of ";
            worksheet.Cells[17, 10] = "Unit value ";
            worksheet.Cells[17, 11] = "Total Value ";
            worksheet.Cells[18, 2] = "Goods";
            worksheet.Cells[18, 3] = "Code (if have)";  //Items Manufacturer
            worksheet.Cells[18, 9] = "Items";
            worksheet.Cells[18, 10] = "(USD$)";
            worksheet.Cells[18, 11] = "(USD$)";
            worksheet.Cells[17, 4] = "Manufacturer";
            Microsoft.Office.Interop.Excel.Range xlsRow14 = worksheet.Range[worksheet.Cells[17, 4], worksheet.Cells[18, 5]];
            xlsRow14.Merge(true);    //合并单元格
            Microsoft.Office.Interop.Excel.Range xlsRow15 = worksheet.Range[worksheet.Cells[17, 4], worksheet.Cells[18, 4]];
            xlsRow15.Merge(true);    //合并单元格
            Microsoft.Office.Interop.Excel.Range xlsRow16 = worksheet.Range[worksheet.Cells[16, 1], worksheet.Cells[18, 11]];
            xlsRow16.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;         //设置边框
            xlsRow16.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;              //边框常规粗细
            //---------------------开始插入数据Datatable---------------------------------------------------------------------------
            double allprice = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                worksheet.Cells[(19 + i), 2] = table.Rows[i]["bgname"].ToString(); //table.Rows[i]["BName"].ToString();
                worksheet.Cells[(19 + i), 3] = table.Rows[i]["bgcode"].ToString();
                worksheet.Cells[(19 + i), 9] = table.Rows[i]["count"].ToString();
                worksheet.Cells[(19 + i), 10] = table.Rows[i]["money"].ToString();
                worksheet.Cells[(19 + i), 11] = "0"; //table.Rows[i]["Totil"].ToString();
                allprice = allprice + Convert.ToDouble("100");  //table.Rows[i]["Totil"].ToString()
            }

            Microsoft.Office.Interop.Excel.Range xlsRow17 = worksheet.Range[worksheet.Cells[19, 1], worksheet.Cells[18 + table.Rows.Count, 11]];
            xlsRow17.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;         //设置边框
            xlsRow17.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;              //边框常规粗细
            xlsRow17.RowHeight = 25;   //设置行高度
            xlsRow17.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//水平居中  
            xlsRow17.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//垂直居中
            //------------------------------------------------------------------------------------------------------------------------
            Microsoft.Office.Interop.Excel.Range xlsRow18 = worksheet.Range[worksheet.Cells[19 + table.Rows.Count, 1], worksheet.Cells[19 + table.Rows.Count, 11]];
            xlsRow18.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;         //设置边框
            xlsRow18.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;              //边框常规粗细
            xlsRow18.Interior.Color = Color.FromArgb(255, 255, 153);

            Microsoft.Office.Interop.Excel.Range xlsRow19 = worksheet.Range[worksheet.Cells[19 + table.Rows.Count, 1], worksheet.Cells[19 + table.Rows.Count, 10]];
            xlsRow19.Merge(true);
            xlsRow19.Value = "Declared Value Terms of Trade";
            xlsRow19.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//水平居中  
            xlsRow19.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//垂直居中
            worksheet.Cells[19 + table.Rows.Count, 11] = allprice;
            //-------------------------------------------------------------------------------------------------------------------------
            worksheet.Cells[19 + table.Rows.Count + 2, 2] = "本人认为以上提供的资料属实和正确，货物原产地是：";
            worksheet.Cells[19 + table.Rows.Count + 2, 6] = "China";
            worksheet.Cells[19 + table.Rows.Count + 3, 2] = "I declare that the above information is true and correct to the best of my knowledge and";
            worksheet.Cells[19 + table.Rows.Count + 4, 2] = "that the goods are of ";
            worksheet.Cells[19 + table.Rows.Count + 4, 6] = "origin ";
            worksheet.Cells[19 + table.Rows.Count + 4, 5] = "China ";
            //----------------------------------最下面----------------------------------------------------------------------------------------
            Microsoft.Office.Interop.Excel.Range xlsRow20 = worksheet.Range[worksheet.Cells[19 + table.Rows.Count + 7, 1], worksheet.Cells[19 + table.Rows.Count + 12, 11]];
            xlsRow20.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;         //设置边框
            xlsRow20.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;              //边框常规粗细 Town/Area Code:
            worksheet.Cells[19 + table.Rows.Count + 7, 2] = "发件人";
            worksheet.Cells[19 + table.Rows.Count + 8, 2] = "Consignor:";
            worksheet.Cells[19 + table.Rows.Count + 9, 2] = "地址：";
            worksheet.Cells[19 + table.Rows.Count + 10, 2] = "Address:";
            worksheet.Cells[19 + table.Rows.Count + 11, 2] = "州名/国家";
            worksheet.Cells[19 + table.Rows.Count + 12, 2] = "State/County";
            worksheet.Cells[19 + table.Rows.Count + 7, 7] = "公司名称：";
            worksheet.Cells[19 + table.Rows.Count + 8, 7] = "Company Name:";
            worksheet.Cells[19 + table.Rows.Count + 9, 7] = "城市/地区号：";
            worksheet.Cells[19 + table.Rows.Count + 10, 7] = "Town/Area Code:";
            worksheet.Cells[19 + table.Rows.Count + 11, 7] = "电话/传真/电子邮件：";
            worksheet.Cells[19 + table.Rows.Count + 12, 7] = "Phone/Fax/E－mail:";
            Microsoft.Office.Interop.Excel.Range xlsRow21 = worksheet.Range[worksheet.Cells[19 + table.Rows.Count + 7, 2], worksheet.Cells[19 + table.Rows.Count + 12, 2]];
            xlsRow21.Interior.Color = Color.FromArgb(255, 255, 153);
            Microsoft.Office.Interop.Excel.Range xlsRow22 = worksheet.Range[worksheet.Cells[19 + table.Rows.Count + 7, 7], worksheet.Cells[19 + table.Rows.Count + 12, 7]];
            xlsRow22.Interior.Color = Color.FromArgb(255, 255, 153);
            Microsoft.Office.Interop.Excel.Range xlsRow23 = worksheet.Range[worksheet.Cells[19 + table.Rows.Count + 7, 8], worksheet.Cells[19 + table.Rows.Count + 8, 11]];
            xlsRow23.Merge(true);
            worksheet.Cells[19 + table.Rows.Count + 8, 3] = "XUXU";
            worksheet.Cells[19 + table.Rows.Count + 9, 3] = "1049  Jinfan  Street  Wucheng District，";
            worksheet.Cells[19 + table.Rows.Count + 10, 3] = "Jinhua City , Zhejiang Province, China";

            worksheet.Cells[19 + table.Rows.Count + 12, 3] = "China";
            worksheet.Cells[19 + table.Rows.Count + 8, 8] = "LeCheng Network Technology CO., Ltd";

            worksheet.Cells[19 + table.Rows.Count + 10, 8] = "O579";
            worksheet.Cells[19 + table.Rows.Count + 12, 8] = "18606881258";
            try
            {
                //保存Excel
                workBook1.Saved = true;
                workBook1.SaveCopyAs(SavePath);

            }
            catch (Exception ex)
            {
                throw new Exception("导出文件时出错，文件可能正被打开!\n" + ex.Message);
                //MessageBox.Show("导出文件时出错，文件可能正被打开!\n" + ex.ToString());
            }
            workBook1.Close();
            excel1.Visible = true;
            if (excel1 != null)
            {
                excel1.Workbooks.Close();
                excel1.Quit();

                int generation = System.GC.GetGeneration(excel1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel1);

                excel1 = null;
                System.GC.Collect(generation);
            }

        }
        public string GetDeclarationExcelUrl(string dhlcode, string zhuanyuncode,string fullpath)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    DataTable list=db.Queryable<base_product>().JoinTable<base_prod_code>((s1, s2) => s1.prod_id == s2.prod_id)
                        .JoinTable<base_prod_code, busi_sendorder_detail>((s1, s2, s3) => s2.code_id == s3.code_id)
                        .JoinTable<busi_sendorder_detail, busi_sendorder>((s1, s3, s4) => s3.order_id == s4.order_id)
                        .JoinTable<busi_sendorder, busi_transfer>((s1, s4, s5) => s4.tran_id == s5.tran_id)
                        .Where<busi_transfer>((s1, s5) => s5.tran_code == zhuanyuncode.Trim())
                        .GroupBy<base_product>(s1 => s1.bgcode)
                        .GroupBy<base_product>(s1 => s1.bgname)
                        .GroupBy<base_product>(s1 => s1.prod_style)
                        .Select("s1.bgcode,s1.bgname,s1.prod_style,count(s3.prod_num) as count,count(s1.price_cn) as money").ToDataTable();
          
                    string cbgcode = string.Empty;
                    string cbgname = string.Empty;
                    string proname = string.Empty;
                    for (int i=0; i<list.Rows.Count;i++)//增加判断，如果某一个产品没有填写报关信息，如报关名称或者报关编码，提醒
                    {
                        cbgcode=list.Rows[i]["bgcode"].ToString();
                        cbgname= list.Rows[i]["bgname"].ToString();
                        if (string.IsNullOrEmpty(cbgcode) || string.IsNullOrEmpty(cbgname))
                        {
                            proname= list.Rows[i]["prod_style"].ToString();
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(proname))
                    {
                        throw new Exception("款号:"+proname+"不存在报关编码或者报关名称！请先添加");
                    }
                    else
                    {
                        Printer(list, fullpath, dhlcode);
                        return fullpath;
                    }
                                        
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public bool IsExitZY(string zhuanyuncode)
        {
            using (var db = SugarDao.GetInstance(LoginUser.GetConstr()))
            {
                try
                {
                    busi_transfer a=db.Queryable<busi_transfer>().Where(s => s.tran_code == zhuanyuncode).FirstOrDefault();
                    if (null == a)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
