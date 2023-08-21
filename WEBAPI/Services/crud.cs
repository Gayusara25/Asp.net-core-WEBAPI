using System.Data;
using WEBAPI.Models;

namespace WEBAPI.Services
{
    public class crud:icrud
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
                var query = "INSERT INTO Diary(Name,Number,CreatedDate) VALUES(@Name,@Number,@CreatedDate)";
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("Name", diary.Name);
                    cmd.Parameters.AddWithValue("Phonenumber", diary.Phonenumber);

                    cmd.Parameters.AddWithValue("CreatedDate", DateTime.Now);
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
                        Phonenumber = (long)(dr["Number"]),

                    }).ToList();


        }
    }
}
}
