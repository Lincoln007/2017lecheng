using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrintToolOfLC.Helper;
using XMLClass;
using System.Xml;

namespace PrintToolOfLC.config
{
    public partial class printconfig : Form
    {
        public printconfig()
        {
            InitializeComponent();
        }

       
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printconfig_Load(object sender, EventArgs e)
        {
            #region 取得当前电脑可用的所有打印机
            listBox1.Dock = DockStyle.Fill;
            foreach (String fPrinterName in LocalPrinter.GetLocalPrinters())
            {
                listBox1.Items.Add(fPrinterName);

            } 
            #endregion

            #region 注释代码
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.ColumnHeadersVisible = false;
            ////dataGridView1.ReadOnly = true;
            //DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
            ////acCode.Name = "拣货单";
            ////acCode.DataPropertyName = "拣货单";
            ////acCode.HeaderText = "A/C Code";
            //dataGridView1.Columns.Add(acCode); 
            //DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            //col.Items.Add("模式一");
            //col.Items.Add("模式二");
            //col.Name="模式";
            //dataGridView1.Columns.Add(col);
            //dataGridView1.Columns["模式"].ReadOnly=true;
            //col.Selected = true;
            //DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            //dataGridView1.Columns.Add(check);

            //dataGridView1.Rows.Add("拣货单");
            //dataGridView1.Rows.Add("Yamato");
            //dataGridView1.Rows.Add("ECOHAI");
            //dataGridView1.Rows.Add("Umail");
            //dataGridView1.Rows.Add("包裹号");
            //dataGridView1.Rows.Add("SKU"); 
            #endregion
            int iRowIndex = 0;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "1";
            this.dataGridView1["page", iRowIndex].Value = "拣货单";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;
            iRowIndex = 1;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "2";
            this.dataGridView1["page", iRowIndex].Value = "Yamato";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;
            iRowIndex = 2;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "3";
            this.dataGridView1["page", iRowIndex].Value = "ECOHAI";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;
            iRowIndex = 3;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "4";
            this.dataGridView1["page", iRowIndex].Value = "Umail";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;
            iRowIndex = 4;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "5";
            this.dataGridView1["page", iRowIndex].Value = "包裹号";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;
            iRowIndex = 5;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "6";
            this.dataGridView1["page", iRowIndex].Value = "SKU";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;

            iRowIndex = 6;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "7";
            this.dataGridView1["page", iRowIndex].Value = "转运单";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;

            iRowIndex = 7;
            this.dataGridView1.Rows.AddCopy(iRowIndex);
            this.dataGridView1["ID", iRowIndex].Value = "8";
            this.dataGridView1["page", iRowIndex].Value = "Upacket";
            this.dataGridView1["model", iRowIndex].Value = "模式一";
            this.dataGridView1["IsUse", iRowIndex].Value = 0;
            
            //显示已配置的面单
            //1.先判断XML文件是否存在，如果不存在则创建一个
            dataGridView2.ReadOnly = true;
           
            NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "PrintList");
            dataGridView2.DataSource = xmlfile.ReadXMLfile();
           
        }
        /// <summary>
        /// 显示面单图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox1.Image = null;
            switch (Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value.ToString()))
            { 
                case 1:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//拣选单.png", true);
                    break;
                case 2:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//yamato.png", true);
                    break;
                case 3:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//ECOHAI.jpg", true);
                    break;
                case 4:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//Umail.jpg", true);
                    break;
                case 5:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//包裹号.png", true);
                    break;
                case 6:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//SKU.jpg", true);
                    break;
                case 7:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//转运单.png", true);
                    break;
                case 8:
                    pictureBox1.Image = Image.FromFile(Comm.StartupPath + "//page//Umail.jpg", true);
                    break;
            
            
            }
            
        }
        /// <summary>
        /// 开始绑定，写入到配置文件中config.xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //判断打印机和面单是否选中了
            if (listBox1.SelectedIndex < 0 || dataGridView1.SelectedRows.Count == 0) //|| (bool)dataGridView1.CurrentRow.Cells["IsUse"].EditedFormattedValue == true
            {
                MessageBox.Show("请选择打印机和面单再点击绑定按钮！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            
            }
            //确认进行绑定操作
            DialogResult result=MessageBox.Show("确认面单配置好了进行绑定？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            string selectID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            string page = dataGridView1.CurrentRow.Cells["page"].Value.ToString();
            string selectmodel = dataGridView1.CurrentRow.Cells["model"].Value.ToString();
            string printername = listBox1.SelectedItem.ToString();
            if (result == DialogResult.OK)
            {
                //1.先判断XML文件是否存在，如果不存在则创建一个
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "PrintList");
                //2.在插入节点之前先判断配置表中是否已经存在这个配置
                string xpath = "//Printer[@ID='" + selectID + "']";
                if (xmlfile.FindNode(xpath))
                {
                    MessageBox.Show("此面单已经绑定了对应的打印机！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //3.插入一个配置节点
                xmlfile.InsertNodeWithChild("PrintList", "Printer", selectID, "printmachine", printername, "page", page,"Model",selectmodel);
                //4.读取配置好的config.xml文件

                dataGridView2.DataSource = xmlfile.ReadXMLfile();

            }
            else
            {
                return;
            
            }

            

        }
        /// <summary>
        /// 删除不需要的配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView2.SelectedRows.Count;
            if(0==index)
            {
                MessageBox.Show("请选中需要删除的面单和绑定的打印机！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            
            }
            DialogResult result = MessageBox.Show("确认需要删除", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (DialogResult.OK == result)
            {
                string ID = dataGridView2.CurrentRow.Cells["ID"].Value.ToString();
                NewXmlControl xmlfile = new NewXmlControl(Comm.StartupPath + "//config//config.xml", false, "PrintList");

                bool removeOK = xmlfile.DeleteNodeByName("//Printer[@ID='" + ID + "']");
                if (removeOK)
                {


                    dataGridView2.DataSource = xmlfile.ReadXMLfile();
                    MessageBox.Show("删除成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    dataGridView2.DataSource = xmlfile.ReadXMLfile();
                    MessageBox.Show("删除失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            else
            {

                return;
            }
            
        }



    }
}
