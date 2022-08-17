using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Oreder
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Duser\\Desktop\\C#pojects\\Oreder\\items.mdb");
        OleDbConnection con2 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Duser\\Desktop\\C#pojects\\Oreder\\items.mdb");

        public String sellPrice, count;
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "Select * from items ";

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            OleDbDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                ListViewItem list = new ListViewItem(r[0].ToString());
                list.SubItems.Add(r[1].ToString());
                list.SubItems.Add(r[2].ToString());
                list.SubItems.Add(r[3].ToString());
                list.SubItems.Add(r[4].ToString());
                list.SubItems.Add(r[5].ToString());
                ItemslistView.Items.Add(list);
            }
            con.Close();


            ////////////////////////////



            OleDbCommand cmd2 = con2.CreateCommand();
            con2.Open();
            cmd2.CommandText = "Select * from suppliers ";

            cmd2.Connection = con2;
            cmd2.ExecuteNonQuery();
            OleDbDataReader r2 = cmd2.ExecuteReader();

            while (r2.Read())
            {
                ListViewItem list2 = new ListViewItem(r2[0].ToString());
                list2.SubItems.Add(r2[1].ToString());
                //list2.SubItems.Add(r2[2].ToString());
                SupplierlistView.Items.Add(list2);
            }

            con2.Close();



            /////////////////////////////////

















        }





        private void add_Click_1(object sender, EventArgs e)
        {


            try
            {

                ListViewItem list2 = new ListViewItem(idTextBox.Text);
                list2.SubItems.Add(itemTextBox.Text);
                list2.SubItems.Add(countTextBox.Text);
                list2.SubItems.Add(priceTextBox.Text);
                string sellPrice = Convert.ToString(Convert.ToDouble(priceTextBox.Text) + Convert.ToDouble(profitTextBox.Text));
                list2.SubItems.Add(sellPrice);
                list2.SubItems.Add(SnumtextBox.Text);
                ItemslistView.Items.Add(list2);

                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "Insert into items(Id,Itemm, Countt, Pricee,Sellpricee,SupplierNum) Values('" + idTextBox.Text + "','" + itemTextBox.Text + "','" + countTextBox.Text + "','" + priceTextBox.Text + "','" + sellPrice + "','" + SnumtextBox.Text + "')";


                cmd.ExecuteNonQuery();
                MessageBox.Show("تمت الإضافة بنجاح");

                cmd.Connection = con;

                idTextBox.Text = "";
                itemTextBox.Text = "";
                countTextBox.Text = "";
                priceTextBox.Text = "";
                profitTextBox.Text = "";
                SnumtextBox.Text = "";
                con.Close();


            }
            catch (Exception ex)
            {
                if (idTextBox.Text == "")
                {
                    // MessageBox.Show("Invalid ID ");
                    MessageBox.Show("ادخل الرقم");
                }
                else if (itemTextBox.Text == "")
                {
                    MessageBox.Show("ادخل المنتج");

                }
                else if (countTextBox.Text == "")
                {
                    MessageBox.Show("ادخل العدد");

                }
                else if (priceTextBox.Text == "")
                {
                    MessageBox.Show("ادخل السعر");

                }
                else if (profitTextBox.Text == "")
                {
                    MessageBox.Show("ادخل الربح");

                }
                else if (SnumtextBox.Text == "")
                {
                    MessageBox.Show("ادخل رقم الموزع");
                    //   MessageBox.Show(ex.Message);

                }
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {

                count = Convert.ToString(Convert.ToInt32(ItemslistView.SelectedItems[0].SubItems[2].Text) - Convert.ToInt32(itemCountTextBox.Text));
                string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\Duser\\Desktop\\C#pojects\\Oreder\\items.mdb";
                if (Convert.ToInt32(count) >= 0)
                {
                    OleDbConnection con = new OleDbConnection(connString);
                    con.Open();


                    OleDbCommand commandObj = new OleDbCommand();


                    commandObj.CommandText = "UPDATE items Set Id='" + ItemslistView.SelectedItems[0].SubItems[0].Text + "',Itemm='" + ItemslistView.SelectedItems[0].SubItems[1].Text + "',Countt='" + count + "',Pricee='" + ItemslistView.SelectedItems[0].SubItems[3].Text + "',Sellpricee='" + ItemslistView.SelectedItems[0].SubItems[4].Text + "' Where Id=" + ItemslistView.SelectedItems[0].SubItems[0].Text;


                    commandObj.Connection = con;
                    commandObj.ExecuteNonQuery();
                    MessageBox.Show("تم");
                    con.Close();

                    ////
                    ItemslistView.Items.Clear();
                    ////

                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "Select * from items ";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    OleDbDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        ListViewItem list = new ListViewItem(r[0].ToString());
                        list.SubItems.Add(r[1].ToString());
                        list.SubItems.Add(r[2].ToString());
                        list.SubItems.Add(r[3].ToString());
                        list.SubItems.Add(r[4].ToString());
                        ItemslistView.Items.Add(list);
                    }
                    con.Close();
                }


                else
                    MessageBox.Show("لا يوجد منتجات كافية");

                itemCountTextBox.Text = null;


            }

            catch (Exception ex)
            {
                MessageBox.Show("الرجاء ادخال عدد");
            }
        }

        private void addSupplierBTN_Click(object sender, EventArgs e)
        {

            try
            {
                ListViewItem list3 = new ListViewItem(SupplierNUMTextBox.Text);
                list3.SubItems.Add(SupplierNameTextBox.Text);
                SupplierlistView.Items.Add(list3);


                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "Insert into suppliers(SupplierNum,SupplierName) Values('" + SupplierNUMTextBox.Text + "','" + SupplierNameTextBox.Text + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("تمت الإضافة بنجاح");

                cmd.Connection = con;

                SupplierNameTextBox.Text = "";
                SupplierNUMTextBox.Text = "";

                //con.Close();

                SupplierlistView.Items.Clear();





                OleDbCommand cmd2 = con2.CreateCommand();
                // con.Open();
                cmd2.CommandText = "Select * from suppliers ";

                cmd2.Connection = con;
                cmd2.ExecuteNonQuery();
                OleDbDataReader r2 = cmd2.ExecuteReader();

                while (r2.Read())
                {
                    ListViewItem list2 = new ListViewItem(r2[0].ToString());
                    list2.SubItems.Add(r2[1].ToString());

                    SupplierlistView.Items.Add(list2);
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("الرجاء ادخال المعلومات");
            }






        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd2 = con2.CreateCommand();
                con2.Open();
                cmd2.CommandText = "DELETE From suppliers where SupplierNum =" + SupplierlistView.SelectedItems[0].SubItems[0].Text;
                cmd2.Connection = con2;
                cmd2.ExecuteNonQuery();
                MessageBox.Show("تم الحذف بنجاح");

                SupplierlistView.Items.Remove(SupplierlistView.SelectedItems[0]);

                con2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("الرجاء اختيار موزع");
            }

        }

        private void CountBTN_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < ItemslistView.Items.Count; i++)
                {
                    if (ItemslistView.Items[i].SubItems[5].Text == SupplierlistView.SelectedItems[0].SubItems[0].Text)
                        count++;
                }


                Countlabel.Text = count.ToString() + " - " + SupplierlistView.SelectedItems[0].SubItems[1].Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("الرجاء اختيار موزع");
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x = 0;
            for (int i = 0; i < ItemslistView.Items.Count; i++)
            {
                x += Convert.ToDouble(ItemslistView.Items[i].SubItems[4].Text) - Convert.ToDouble(ItemslistView.Items[i].SubItems[3].Text);
            }
            label12.Text = Convert.ToString(x + " JD");
        }

        private void delete_Click_1(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand commandObj = new OleDbCommand();
                con.Open();
                commandObj.CommandText = "DELETE From items where Id=" + ItemslistView.SelectedItems[0].SubItems[0].Text;
                commandObj.Connection = con;
                commandObj.ExecuteNonQuery();
                MessageBox.Show("تم الحذف بنجاح");

                ItemslistView.Items.Remove(ItemslistView.SelectedItems[0]);

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("الرجاء اختيار منتج");
            }
        }




    }
}

