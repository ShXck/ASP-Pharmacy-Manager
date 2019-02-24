using ASP_Rest_Pharmacy.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ASP_Rest_Pharmacy.Controllers
{
    [EnableCors(origins: "http://localhost:4200/", headers: "*", methods: "*")]
    public class DoctorController : ApiController
    {
        private static List<Doctor> Doctors = new List<Doctor>();

        [HttpGet]
        [Route("doctors")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Doctors);
        }

        [HttpGet]
        [Route("doctors/{id}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string id)
        {
            for (int i = 0; i < Doctors.Count; i++)
            {
                if (Doctors.ElementAt(i).DoctorID.Equals(id)) return Ok(Doctors.ElementAt(i));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("doctors/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Doctor new_cust = JsonConvert.DeserializeObject<Doctor>(value);
            Doctors.Add(new_cust);
            return Ok();
        }

        [HttpPut]
        [Route("doctors/update/{id}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string id, [FromBody]string value)
        {
            Doctor new_doc = JsonConvert.DeserializeObject<Doctor>(value);

            for (int i = 0; i < Doctors.Count; i++)
            {
                if (Doctors.ElementAt(i).DoctorID.Equals(id))
                {
                    Doctor doc = Doctors.ElementAt(i);
                    doc.Address = new_doc.Address;
                    doc.Name = new_doc.Name;
                    doc.ID = new_doc.ID;
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("doctors/delete/{id}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string id)
        {
            for (int i = 0; i < Doctors.Count; i++)
            {
                if (Doctors.ElementAt(i).DoctorID.Equals(id))
                {
                    Doctors.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
