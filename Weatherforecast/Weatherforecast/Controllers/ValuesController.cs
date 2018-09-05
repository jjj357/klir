using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

namespace Weatherforecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        SQLiteConnection conn;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var rng = new Random();
            var number = Math.Round((decimal)rng.Next(0, 100), 0);
            //Randomly return an Error
            if (number % 2 == 0) {
                IEnumerable<string> result = null;
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=C:\\Users\\109summer\\Documents\\Visual Studio 2017\\Projects\\Weatherforecast\\Weatherforecast\\sqlite\\weatherforecast.db;Version=3;"))
                {
                    string sql = "select * from weatherforecast order by forecastdate";
                    using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                    {
                        conn.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            List<string> temp = new List<string>();
                            while (reader.Read())
                            {
                                String wf = "{\"date\":\"" + reader["forecastdate"] + "\",\"tempc\":\"" + reader["tempc"] + "\",\"tempf\":\"" + reader["tempf"] + "\",\"summary\":\"" + reader["summary"] + "\"}";
                                temp.Add(wf);
                            }
                            result = (IEnumerable<string>)temp;
                        }
                    }
                }
                return Ok(result);
            } else {
                //return error 500
                return StatusCode(500);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
