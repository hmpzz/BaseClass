using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection();
            SqlCommand sqlcmd = new SqlCommand();
            SqlDataAdapter sqlda = new SqlDataAdapter();

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();


            string cmd;
            string ss;

            sqlcon = GetSqlConn();
            sqlcmd.Connection = sqlcon;

            cmd = "select top 10 * from t_item ";

            sqlcmd.CommandText = cmd;

            sqlda.SelectCommand = sqlcmd;
            

            sqlda.Fill(ds);
            ds.Tables[0].TableName = "t_item";
            //dt.TableName = "t_item";

            //cmd = "select * from Equipment";

            //sqlcmd.CommandText = cmd;

            //sqlda.SelectCommand = sqlcmd;
            //sqlda.Fill(ds, "Equipment");


            this.exDataGridView1.DataSource = ds;
            this.exDataGridView1.DataMember = "t_item";

            this.exDataGridView1.Columns.Clear();
            this.exDataGridView1.AddColumn("item_num", "item_num");
            this.exDataGridView1.AddColumn("item_des", "item_des");


        }


        /// <summary>
        /// 返回MES数据库的连接
        /// </summary>
        /// <returns>MES数据库的连接</returns>
        private SqlConnection GetSqlConn()
        {
            string SQLconn_str;
            SqlConnection sqlcon1 = new SqlConnection();

            SQLconn_str = "server=172.16.1.15;Password=Ying2Zhou@;User ID=sa;database=wsprint";

            sqlcon1.ConnectionString = SQLconn_str;

            try
            {
                sqlcon1.Open();
            }
            catch (Exception ex)
            {

                throw new Exception("数据库连接错误！\r\n" + ex.Message);
            }
            return sqlcon1;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyContrals.ToExcel.ExDataGridViewToExcel(this.exDataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyContrals.WaitFormService.Show(this);

            MyContrals.WaitFormService.SetLeftText("lefttext");
            MyContrals.WaitFormService.SetProgressBarMax(10000, "啥意思?");
            MyContrals.WaitFormService.SetRightText("righttext");
            MyContrals.WaitFormService.SetTopText("toptext");

            MyContrals.WaitFormService.SetLeftText("lefttext");

            for (int i = 0; i < 10000; i++)
            {

                MyContrals.WaitFormService.ProgressBarGrow();
                // Application.DoEvents();
            }


            MyContrals.WaitFormService.Close();

            MessageBox.Show("aac");
        }
    }
}
