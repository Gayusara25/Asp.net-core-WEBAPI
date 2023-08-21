using System.Data;
using WEBAPI.Models;

namespace WEBAPI.Services
{
    public interface icrud
    {
        void SendEmail();
        void InsertRecords(Diary diary);

        DataTable SyncData();

        List<Diary> GetAllRecords();
    }
}
