using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Movit.Application.Busines;
using Movit.Application.Busines.AuthorizeManage;
using Movit.Application.Busines.BaseManage;
using Movit.Application.Busines.CapitalFlow;
using Movit.Application.Cache;
using Movit.Application.Code;
using Movit.Util;
using Movit.Util.Offices;
using System.Web;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;


namespace 测试导出
{
    public partial class Form1 : Form
    {
        //
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = null;

            OpenFileDialog sflg = new OpenFileDialog();
            sflg.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            if (sflg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            FileStream fs = new FileStream(sflg.FileName, FileMode.Open, FileAccess.Read);
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);
            int sheetCount = book.NumberOfSheets;
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                NPOI.SS.UserModel.ISheet sheet = book.GetSheetAt(sheetIndex);
                if (sheet == null) continue;

                NPOI.SS.UserModel.IRow row = sheet.GetRow(0);
                if (row == null) continue;

                int firstCellNum = row.FirstCellNum;
                int lastCellNum = row.LastCellNum;
                if (firstCellNum == lastCellNum) continue;

                dt = new DataTable(sheet.SheetName);
                for (int i = firstCellNum; i < lastCellNum; i++)
                {
                    dt.Columns.Add(row.GetCell(i).StringCellValue, typeof(string));
                }

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow newRow = dt.Rows.Add();
                    for (int j = firstCellNum; j < lastCellNum; j++)
                    {
                        newRow[j] = sheet.GetRow(i).GetCell(j).StringCellValue;
                    }
                }

                ds.Tables.Add(dt);
            }           
        }
        private void bind(string fileName)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                 "Data Source=" + fileName + ";" +
                 "Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'";
            if ((System.IO.Path.GetExtension(fileName.Trim())).ToLower() == ".xls")
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "data source=" + fileName + ";Extended Properties=Excel 5.0;Persist Security Info=False";
            }

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT *  FROM [student$]", strConn);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
                this.dataGridView1.DataSource = dt;
            }
            catch (Exception err)
            {
                MessageBox.Show("操作失败！" + err.ToString());
            }
        }  
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
