using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Forms;
using ZXing;
using System.Data.OleDb;
namespace QR
{
    public partial class Form2 : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\KvRem\documents\visual studio 2015\Projects\QR\QR\qrcode.accdb");
        public Form2()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;
        private void button1_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDev.SelectedIndex].MonikerString);
            captureDevice.NewFrame += captureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();
        }

        private void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qrcodedata.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.qrcodedata.Table1);
            // TODO: This line of code loads data into the 'qrcodedata.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.qrcodedata.Table1);
            // TODO: This line of code loads data into the 'qrcodedata.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.qrcodedata.Table1);
            // TODO: This line of code loads data into the 'qrcodedata.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.qrcodedata.Table1);
            // TODO: This line of code loads data into the 'qrcodedata.Table1' table. You can move, or remove it, as needed.
            this.table1TableAdapter.Fill(this.qrcodedata.Table1);
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDev.Items.Add(filterInfo.Name);
            cboDev.SelectedIndex = 0;
      

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {if (captureDevice.IsRunning)
                captureDevice.Stop();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                BarcodeReader barcodereader = new BarcodeReader();
                Result result = barcodereader.Decode((Bitmap)pictureBox1.Image);

                if(result != null)
                {
                    textqr.Text = result.ToString();
                    timer1.Stop();
                    if (captureDevice.IsRunning)
                        captureDevice.Stop();
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into table1 values('"+textqr.Text+"')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Insert Successfully");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from table1";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from table1 where Information='"+textqr.Text+"'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted Successfully");
        }
    }
}
