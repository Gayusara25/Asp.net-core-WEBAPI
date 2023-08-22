using Microsoft.AspNetCore.Mvc;
using WEBAPI.Models;
using WEBAPI.Services;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WebapiController : ControllerBase
    {


        public static List<Diary> records = new List<Diary>();
        private readonly icrud _icrud;

        public WebapiController(icrud Icrud)
        {
            _icrud = Icrud;
        }
        [HttpPost]
        public IActionResult AddRecord(Diary diary)
        {
            if (ModelState.IsValid)
            {
                records.Add(diary);
                _icrud.InsertRecords(diary);
            
                return CreatedAtAction("GetDetails", new { diary.Id }, diary);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetRecord(int id)
        {
            var records = _icrud.GetAllRecords();

            var diary = records.FirstOrDefault(x => x.Id == id);
            if (diary == null)
                return NotFound();
          
            return Ok(diary);
        }

        [HttpDelete]
        public IActionResult Remove(int BookID)
        {
            bool k = _icrud.DeleteRecords(BookID);
            if (k)
            {
                return Ok(k);
            }
            return NoContent();
        }
    }
}
