using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20_DatabaseCRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //slqe bağlan 
            SqlConnection connection = new SqlConnection(
                "Data source=**********\\**********;" +
                "initial catalog = ************;" +
                "integrated security = true");
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            connection.Open();

            #region Variables
            string categoryName;
            string productName;
            decimal productPrice;
            int productId;
            #endregion

            #region  Kategori Ekleme

            Console.WriteLine("********** Kategori Ekleme Sistemi **********\n");
            Console.WriteLine("------------------------------------------");
            command = new SqlCommand("select * from TblCategory",connection);
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row  in dataTable.Rows)
            {
                foreach(var item in row.ItemArray)
                {
                    Console.Write(item.ToString());
                }
                Console.WriteLine();
            }
            Console.Write("Eklemek istediğiniz kategoriyi giriniz:");
            categoryName = Console.ReadLine();

            command = new SqlCommand("insert into TblCategory (CategoryName) values (@categoryName)", connection);
            command.Parameters.AddWithValue("@categoryName", categoryName);
            command.ExecuteNonQuery();
            //connection.Close();
            Console.WriteLine("Tebrikler!!! Kategori Veritabanına Eklendi.");
            Console.ReadLine();
            #endregion

            #region Ürün Ekleme
            Console.Clear();
            Console.WriteLine("********** Ürün Ekleme Sistemi **********\n");
            Console.WriteLine("------------------------------------------");
            Console.Write("Eklemek İstediğiniz Ürünü Yazın:");
            productName = Console.ReadLine();
            Console.Write($"Eklemek İstediğiniz Ürün olan {productName} Fiyatını Yazın:");
            productPrice = decimal.Parse(Console.ReadLine());

            command = new SqlCommand("insert into TblProduct (ProductName,ProductPrice,ProductStatus) values (@productName,@productPrice,@productStatus)", connection);
            command.Parameters.AddWithValue("@productName", productName);
            command.Parameters.AddWithValue("@productPrice", productPrice);
            command.Parameters.AddWithValue("@productStatus", true);
            command.ExecuteNonQuery();
            Console.ReadLine();
            #endregion

            #region Ürün Listeleme
            Console.Clear();
            Console.WriteLine("********** Menude Yer Alan Ürünler **********\n");
            Console.WriteLine("------------------------------------------");
            command = new SqlCommand("Select * From TblProduct", connection);
            //ekrana vereceğim için verileri rama çekmem lazım
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item.ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
            #endregion

            #region Ürün Silme
            Console.Clear();
            Console.WriteLine("********** Ürün Silme Sistemi **********\n");
            Console.WriteLine("------------------------------------------");
            Console.Write("Silmek istediğiniz ürünün ID değerini girin:");
            productId = int.Parse(Console.ReadLine());

            command = new SqlCommand("delete from TblProduct where ProductId = @productId", connection); // isimlendirmeye dikkat db ile aynı olacak
            command.Parameters.AddWithValue("@productId", productId);
            command.ExecuteNonQuery();
            Console.WriteLine("Ürün Silindi");
            Console.ReadLine();
            #endregion

            #region Ürün Güncelleme
            Console.Clear();
            Console.WriteLine("********** Ürün Güncelleme Sistemi **********\n");
            Console.WriteLine("------------------------------------------");
            Console.Write("Güncelleme Yapılacak Olan Ürünün Id:");
            productId = int.Parse(Console.ReadLine());
            Console.Write("Güncelleme Yapılacak Olan Ürünün Adı:");
            productName = Console.ReadLine();
            Console.Write("Güncelleme Yapılacak Olan Ürünün Fiyatı:");
            productPrice = decimal.Parse(Console.ReadLine());

            command = new SqlCommand("update TblProduct Set ProductName = @productName, ProductPrice = @productPrice where ProductId=@productId", connection);
            command.Parameters.AddWithValue("@productName", productName);
            command.Parameters.AddWithValue("@productPrice", productPrice);
            command.Parameters.AddWithValue("@productId", productId);
            command.ExecuteNonQuery();

            dataTable = new DataTable();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item.ToString());
                }
                Console.WriteLine();
            }
            connection.Close();
            Console.Read();

            #endregion

        }
    }

}
