using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_DatabaseConnection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ado.net

            string tableNumber;
            Console.WriteLine("----------");
            Console.WriteLine("1-Kategoriler");
            Console.WriteLine("2-Ürünler");
            Console.WriteLine("3-Siparişler");
            Console.WriteLine("4-Çıkış Yap");
            Console.Write("Lütfen getirmek istediğiniz tablo numarasını giriniz:");
            tableNumber = Console.ReadLine();
            Console.WriteLine("----------");

            //Sql bağlantısını yapalım
            #region Sql Bağlantı Ayarları

            //DBye bağlanalım
            SqlConnection connection = new SqlConnection(
                "Data source=**********\\***********;" + // Veritabanının Referans Kodu
                "initial Catalog=***********; " + // Veritabanı Adı
                "integrated security=true");
            connection.Open();

            /*
             * Komutu yazalım
             * command ile komutu yazdıktan sonra hangi veri tabanına sorgu atacağımızı ,connection ile belirtelim
             */
            SqlCommand command = new SqlCommand("Select * from TblCategory", connection);

            //adapter c# komutlarım ile sqlServer arasında köprü görevi görür
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            // DataTable verileri ram'e alarak kullanabilmemizi sağlar'
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item.ToString());
                }
                Console.WriteLine();
            }

            #endregion
            Console.Read();
        }
    }
}
