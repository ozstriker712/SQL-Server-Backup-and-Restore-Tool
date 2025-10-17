using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraBars.Alerter;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataReader reader;
        string sql = "";
        string connectionString = "";

        public Form1()
        {
            InitializeComponent(); 
            simpleButton2.Enabled = false;
            simpleButton5.Enabled = false;


            textEdit3.Visible = false; simpleButton4.Visible = false; simpleButton5.Visible = false;
            
        }

       
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                connectionString = "Data Source=localhost;Initial Catalog=master;Integrated Security=True";

                //connectionString = "Data Source=.\\;Initial Catalog=master;Integrated Security=True";

                    //connectionString = "Trusted_Connection=True";
              
               

                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "EXEC sp_databases";
                sql = "    SELECT * FROM  sys.databases d where d.database_id>4";
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();
                comboBox1.Items.Clear();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());
                }
                reader.Dispose();
                conn.Close();
                conn.Dispose();

                AlertInfo info = new AlertInfo("", "لقد تمت العملية بنجاح ");
                alertControl1.Show(this, info);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        string dd = DateTime.Now.ToString("dd-MM-yyyy ");

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBox1.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("!! يرجى تحديد قاعدة البيانات أولا");
                    return;
                }
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "BACKUP DATABASE \"" + comboBox1.Text + "\" TO DISK ='" + textEdit2.Text + "\\" + comboBox1.Text + "-" + DateTime.Now.Ticks.ToString() + ".bak'";
                //sql = "BACKUP DATABASE  " + comboBox1.Text + "  TO DISK ='" + textEdit2.Text + "\\" + comboBox1.Text + "-" + dd + ".bak'";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();
                MessageBox.Show("(" + textEdit2.Text + ") لقد تم حفـــظ قاعدة البيانات بنجاح في ");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog()== DialogResult.OK)
            {
                textEdit2.Text = dlg.SelectedPath;
                simpleButton2.Enabled = true;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Backup Files (*.bak)|*.bak|All Files(*.*)|*.*";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog()== DialogResult.OK)
            {
                textEdit3.Text = dlg.FileName;
                simpleButton5.Enabled = true;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("يرجى تحديد قاعدة البيانات أولا");
                    return;
                }
                conn = new SqlConnection(connectionString);
                conn.Open();
                sql = "Alter Database \"" + comboBox1.Text + "\" Set SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                sql += "Restore Database \"" + comboBox1.Text + "\" FROM Disk = '" + textEdit3.Text + "' WITH REPLACE;";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                MessageBox.Show("لقد تم استرجـــــــــاع قاعدة البيانات بنجاح");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int heure = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int seconde = DateTime.Now.Second;

            labelControl1.Text = DateTime.Now.ToLongDateString() + "      " + heure.ToString() + ":" + minute.ToString() + ":" + seconde.ToString();
           
                
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            if (textEdit1.Text=="password")
            {
                textEdit3.Visible = true; simpleButton4.Visible = true; simpleButton5.Visible = true;
                textEdit1.Visible = false; simpleButton6.Visible = false;
            }
            else
            {
                MessageBox.Show("كلمـــــة الســــــر خاطئة  !! ");

                textEdit1.Text = "";
            }

        }

       
        
          
    }
}
