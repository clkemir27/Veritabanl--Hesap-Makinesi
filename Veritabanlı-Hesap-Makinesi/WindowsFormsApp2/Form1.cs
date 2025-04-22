using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        static string constring = "Data Source=DESKTOP-MV54P42\\SQLEXPRESS;Initial Catalog = HesapMakinesi; Integrated Security = True";
        SqlConnection connect = new SqlConnection(constring);
        double mevcutToplam;
        double mevcutSayı;
        string mevcutOperator;
        bool yeniSayı;
        int tıklamaSayısı = 0;
        private Dictionary<Keys, Button> keyMappings;


        public Form1()
        {

            InitializeComponent();
            label2.Text = tıklamaSayısı + " kez operatör kullanıldı";
            this.KeyPreview = true;
            keyMappings = new Dictionary<Keys, Button>
                 {
        { Keys.D0, button22 },
        { Keys.NumPad0, button22 },
        { Keys.D1, button17 },
        { Keys.NumPad1, button17 },
        { Keys.D2, button18 },
        { Keys.NumPad2, button18 },
        { Keys.D3, button19 },
        { Keys.NumPad3, button19 },
        { Keys.D4, button13 },
        { Keys.NumPad4, button13 },
        { Keys.D5, button14 },
        { Keys.NumPad5, button14 },
        { Keys.D6, button15 },
        { Keys.NumPad6, button15 },
        { Keys.D7, button9 },
        { Keys.NumPad7, button9 },
        { Keys.D8, button10 },
        { Keys.NumPad8, button10 },
        { Keys.D9, button11 },
        { Keys.NumPad9, button11 },
        { Keys.Add, button20 },
        { Keys.Subtract, button16 },
        { Keys.Multiply, button12 },
        { Keys.Divide, button8 },
        { Keys.Enter, button24 },
        { Keys.Decimal, button23 },
        { Keys.Back, button4 }
    };
        
        mevcutToplam = 0;
            mevcutSayı = 0;
            mevcutOperator = string.Empty;
            yeniSayı = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyMappings.ContainsKey(e.KeyCode))
            {
                keyMappings[e.KeyCode].PerformClick();
            }
        }
        private void sayilar(Button button)
        {

            if (yeniSayı)
            {
                textBox1.Text = button.Text;
                yeniSayı = false;

            }
            else
            {
                textBox1.Text += button.Text;
            }
        }

        private void PerformCalculation()
        {
            int labelX = textBox1.Left + (textBox1.Width - label1.Width) - 23;
            int labelY = textBox1.Top + (textBox1.Height - label1.Height) - 85;

            label1.Location = new Point(labelX, labelY);

            label1.BackColor = SystemColors.ControlDark;
            ;
            try
            {
                mevcutSayı = Convert.ToDouble(textBox1.Text);
                switch (mevcutOperator)
                {
                    case "+":
                        mevcutToplam += mevcutSayı;
                        break;
                    case "-":
                        mevcutToplam -= mevcutSayı;
                        break;
                    case "X":
                        mevcutToplam *= mevcutSayı;
                        break;
                    case "/":
                        if (mevcutSayı != 0)
                        {
                            mevcutToplam /= mevcutSayı;
                        }
                        else
                        {
                            MessageBox.Show("Sıfıra bölme hatası. Lütfen sıfırdan farklı bir sayı girin.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    case "mod":
                        mevcutToplam %= mevcutSayı;
                        break;
                    default:
                        mevcutToplam = mevcutSayı;
                        break;

                }

                textBox1.Text = mevcutToplam.ToString();
                yeniSayı = true;


            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonOperator_Click(object sender, EventArgs e)
        {

            tıklamaSayısı++;
            label2.Text = tıklamaSayısı + " kez operatör kullanıldı";

            if (!yeniSayı)
            {
                PerformCalculation();

            }

            Button button = sender as Button;
            mevcutOperator = button.Text;
            label1.Text += mevcutSayı + " " + mevcutOperator + " ";

            yeniSayı = true;

        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            PerformCalculation();
            tıklamaSayısı++;
            label2.Text = tıklamaSayısı + " kez operatör kullanıldı";


            //label1.Text = mevcutToplam.ToString(); EŞİTTİRE TIKLAYINCA SONUCU LABEL'DA GÖSTERİYOR
            mevcutOperator = string.Empty;
            label1.Text += mevcutOperator + mevcutSayı;  // EŞİTTİRE TIKLAYINCA YAPILAN İŞLEMLERİ LABEL'DA GÖSTERİYOR



        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            mevcutToplam = 0;
            mevcutSayı = 0;
            mevcutOperator = string.Empty;
            yeniSayı = true;
            textBox1.Text = "0";
            label1.Text = "";

        }

        private void buttonNegate_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.StartsWith("-"))
            {
                textBox1.Text = textBox1.Text.Substring(1);
            }
            else if (!textBox1.Text.Equals("0"))
            {
                textBox1.Text = "-" + textBox1.Text;
            }
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains(","))
            {
                textBox1.Text += ",";
            }
        }

        private void buttonNumber_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            sayilar(button);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            sayilar(button17);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            sayilar(button18);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            sayilar(button19);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            sayilar(button13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
            sayilar(button14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            sayilar(button15);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sayilar(button9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sayilar(button10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sayilar(button11);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            sayilar(button22);
        }

        private void button4_Click(object sender, EventArgs e) // SİL
        {
            if (Convert.ToDouble(textBox1.Text) > 0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                if (textBox1.Text.Length == 0)
                {
                    textBox1.Text = "0";
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            buttonOperator_Click(sender, e);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            buttonEquals_Click(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            buttonOperator_Click(sender, e);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            buttonOperator_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            buttonOperator_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                double kare = Convert.ToDouble(textBox1.Text);
                kare = Math.Pow(kare, 2);
                textBox1.Text = kare.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                double karekok = Convert.ToDouble(textBox1.Text);
                karekok = Math.Sqrt(karekok);
                textBox1.Text = karekok.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e) // 1/X
        {
            try
            {
                double bolumx = Convert.ToDouble(textBox1.Text);
                if (bolumx == 0)
                {
                    MessageBox.Show("Sıfıra bölme hatası. Lütfen sıfırdan farklı bir sayı girin.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                }
                else
                {
                    bolumx = 1 / bolumx;
                    textBox1.Text = bolumx.ToString();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) // %
        {
            buttonOperator_Click(sender, e);
        }

        private void button23_Click(object sender, EventArgs e) // ,
        {
            buttonDecimal_Click(sender, e);
        }

        private void button21_Click(object sender, EventArgs e) // +/-
        {
            buttonNegate_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e) // C
        {
            buttonClear_Click(sender, e);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                {
                    connect.Open();


                }

                string kayit = "insert into   dbo.HesapMakinesi  (Islem, Sonuc, HesaplamaTarihi, OperatorSayısı) values ('" + label1.Text + "', '" + textBox1.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "' , '" + label2.Text + "')";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.ExecuteNonQuery();

                string kayit1 = "select * from HesapMakinesi ";
                // SqlCommand komut1 = new SqlCommand(kayit1, connect);
                // komut1.ExecuteNonQuery();


                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(kayit1, connect);
                sqlDataAdapter.Fill(dataSet);


                dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch { }


            if (connect.State == ConnectionState.Open)
            {
                connect.Close();


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
           dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
           dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();


            }
            string kayit1 = "select * from HesapMakinesi ";

            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(kayit1, connect);
            sqlDataAdapter.Fill(dataSet);


            dataGridView1.DataSource = dataSet.Tables[0];

            foreach (Control control in this.Controls)
            {
                control.Tag = control.Visible;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //StringBuilder sb = new StringBuilder();     //StringBuilder birleştirmek için
            //foreach (DataGridViewCell cell in dataGridView1.CurrentRow.Cells)
            //{
            //    sb.AppendLine(cell.Value.ToString());   ///append metin eklemek için 
            //    sb.AppendLine("   "); 
            //}
            //textBox2.Text = sb.ToString();

            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();




        }

        private void button26_Click(object sender, EventArgs e)     ///////////GEÇMİŞİ GÖSTER
        {


            foreach (Control control in this.Controls)
            {

                if (control is Button || control is ListBox || control is TextBox || control is Label)    // gizle
                {
                    control.Visible = false;
                    button27.Visible = false;
                }
            }

            dataGridView1.Visible = true;     //  göster
            button27.Visible = true;
            dataGridView1.Dock = DockStyle.Fill;        // tam ekran yap
        }


        private void button27_Click(object sender, EventArgs e)   ///////////// GERİ
        {
            foreach (Control control in this.Controls)
            {
                if (control.Tag != null && control.Tag is bool)    //tag veri saklamak için 
                {
                    control.Visible = (bool)control.Tag;
                }
            }
            dataGridView1.Dock = DockStyle.None;    // küçült
                                                    // dataGridView1.Rows.Clear();
        }

        private void button28_Click(object sender, EventArgs e)             ////////////SIFIRLA
        {
            if(connect.State == ConnectionState.Closed)
{
                connect.Open();
            }

            string sil = "DELETE FROM dbo.HesapMakinesi";
            SqlCommand silKomut = new SqlCommand(sil, connect);
            silKomut.ExecuteNonQuery();
        }
    }

    }










       
   

    

