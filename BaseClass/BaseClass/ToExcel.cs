//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Text;

using System.Data.OleDb;
using System.Data;

using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace MyContrals
{
    public class ToExcel
    {

        /// <summary>
        /// 将ExDataGridView中的数据导出到Excel文件中
        /// </summary>
        /// <param name="gridview">要导出的ExDataGridView</param>
        /// <returns></returns>
        public static bool ExDataGridViewToExcel(ExDataGridView gridview)
        {

            //导出为xls格式用HSSF，xlsx用XSSF。

            System.Windows.Forms.SaveFileDialog SFD = new System.Windows.Forms.SaveFileDialog();

            string FileName = "";

            if (gridview.Columns.Count==0)
            {
                throw new Exception("没有可以导出的数据！");
            }

            SFD.Filter = "Excel文件(*.xls)|*.xls";
            SFD.Title = "导出Excel表";

            if (SFD.ShowDialog()==System.Windows.Forms.DialogResult.Cancel)
            {
                return false;
            }


            FileName = SFD.FileName.ToString().Trim();

            



            try
            {
                HSSFWorkbook wb = new HSSFWorkbook();
                HSSFSheet sheet = (HSSFSheet)wb.CreateSheet("数据页" +DateTime.Now.ToString("yyyyMMddHHmmssms")  );
                HSSFRow headRow = (HSSFRow)sheet.CreateRow(0);
                for (int i = 0; i < gridview.Columns.Count; i++)
                {
                    HSSFCell headCell = (HSSFCell)headRow.CreateCell(i, CellType.String);
                    headCell.SetCellValue(gridview.Columns[i].HeaderText);
                }
                for (int i = 0; i < gridview.Rows.Count; i++)
                {
                    HSSFRow row = (HSSFRow)sheet.CreateRow(i + 1);
                    for (int j = 0; j < gridview.Columns.Count; j++)
                    {
                        HSSFCell cell = (HSSFCell)row.CreateCell(j);
                        if (gridview.Rows[i].Cells[j].Value == null)
                        {
                            cell.SetCellType(CellType.Blank);
                        }
                        else
                        {
                            if (gridview.Rows[i].Cells[j].ValueType.FullName.Contains("System.Int32"))
                            {
                                cell.SetCellValue(Convert.ToInt32(gridview.Rows[i].Cells[j].Value));
                            }
                            else if (gridview.Rows[i].Cells[j].ValueType.FullName.Contains("System.String"))
                            {
                                cell.SetCellValue(gridview.Rows[i].Cells[j].Value.ToString());
                            }
                            else if (gridview.Rows[i].Cells[j].ValueType.FullName.Contains("System.Single"))
                            {
                                cell.SetCellValue(Convert.ToSingle(gridview.Rows[i].Cells[j].Value));
                            }
                            else if (gridview.Rows[i].Cells[j].ValueType.FullName.Contains("System.Double"))
                            {
                                cell.SetCellValue(Convert.ToDouble(gridview.Rows[i].Cells[j].Value));
                            }
                            else if (gridview.Rows[i].Cells[j].ValueType.FullName.Contains("System.Decimal"))
                            {
                                cell.SetCellValue(Convert.ToDouble(gridview.Rows[i].Cells[j].Value));
                            }
                            else if (gridview.Rows[i].Cells[j].ValueType.FullName.Contains("System.DateTime"))
                            {
                                cell.SetCellValue(Convert.ToDateTime(gridview.Rows[i].Cells[j].Value).ToString("yyyy-MM-dd"));
                            }
                        }

                    }

                }
                for (int i = 0; i < gridview.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    wb.Write(fs);
                }
                MessageBox.Show("导出成功！", "导出提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                
            }


        
            return true;
        }



      
    }
}
