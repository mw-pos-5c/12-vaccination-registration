#region usings

using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using VaccRegDb;

#endregion

namespace VaccReg.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        #region Constants and Fields

        private readonly VaccRegContext db;

        #endregion

        public ValuesController(VaccRegContext db)
        {
            this.db = db;
        }

        [HttpGet("GetVaccinations")]
        public object GetVaccinations()
        {
            try
            {
                int nr = db.Vaccinations.Count();
                return new
                {
                    IsOk = true,
                    Nr = nr
                };
            }
            catch (Exception exc)
            {
                return new
                {
                    IsOk = false,
                    Nr = -1,
                    Error = exc.Message
                };
            }
        }
    }
}
