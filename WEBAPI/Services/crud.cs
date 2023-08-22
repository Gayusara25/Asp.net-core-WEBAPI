
using System.Data.SqlClient;
using System.Data;

using WEBAPI.Models;
using WEBAPI.Services;

namespace HangFireDemo.crud
{
    public class crud : icrud
    {
        private readonly IConfiguration _configuration;
        public crud(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail()
        {
            Console.WriteLine($"SendEmail :Sending email is in process..{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public DataTable SyncData()
        {
            string connection = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            SqlConnection con = new SqlConnection(connection);
            var query = "SELECT * FROM Diary";
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.Fill(ds);

            Console.WriteLine($"SyncData :sync is going on..{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            return ds.Tables[0];
        }

        public void InsertRecords(Diary diary)
        {
            try
            {
                string connection = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                var query = "INSERT INTO Diary(Id,Name,Address) VALUES(@Id,@Name,@Address)";
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("Id", diary.Id);
                    cmd.Parameters.AddWithValue("Name", diary.Name);
                    cmd.Parameters.AddWithValue("Address", diary.Address);


                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
            }
            Console.WriteLine($"UpdatedDatabase :Updating the database is in process..{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public List<Diary> GetAllRecords()
        {
            DataTable records = SyncData();
            return (from DataRow dr in records.Rows
                    select new Diary()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Address = dr["Address"].ToString()
                    }).ToList();


        }

        public bool DeleteRecords(int ID)
        {
            int p;
            try
            {
                string connection = _configuration.GetValue<string>("connectionstrings:defaultconnection");

                SqlConnection con = new SqlConnection(connection);
                con.Open();



                using (SqlCommand cmd = new SqlCommand($"DELETE FROM Diary WHERE Id={ID}", con))
                {
                    p = cmd.ExecuteNonQuery();

                }
                if (p == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }
            Console.WriteLine($"UpdatedDatabase :Updating the database is in process...");
        }
    }
}




