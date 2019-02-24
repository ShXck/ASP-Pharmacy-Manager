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
    public class MedicineController : ApiController
    {
        private static List<Medicine> Medicines = new List<Medicine>();

        [HttpGet]
        [Route("medicines")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Medicines);
        }

        [HttpGet]
        [Route("medicines/{name}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string name)
        {
            for (int i = 0; i < Medicines.Count; i++)
            {
                if (Medicines.ElementAt(i).Name.Equals(name)) return Ok(Medicines.ElementAt(i));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("medicines/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Medicine new_cust = JsonConvert.DeserializeObject<Medicine>(value);
            Medicines.Add(new_cust);
            return Ok();
        }

        [HttpPut]
        [Route("medicines/update/{name}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string name, [FromBody]string value)
        {
            Medicine new_med = JsonConvert.DeserializeObject<Medicine>(value);

            for (int i = 0; i < Medicines.Count; i++)
            {
                if (Medicines.ElementAt(i).Name.Equals(name))
                {
                    Medicine med = Medicines.ElementAt(i);
                    med.Manufacturer = new_med.Manufacturer;
                    med.Name = new_med.Name;
                    med.AvailableQnt = new_med.AvailableQnt;
                    med.Prescription = new_med.Prescription;
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("medicines/delete/{name}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string name)
        {
            for (int i = 0; i < Medicines.Count; i++)
            {
                if (Medicines.ElementAt(i).Name.Equals(name))
                {
                    Medicines.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
