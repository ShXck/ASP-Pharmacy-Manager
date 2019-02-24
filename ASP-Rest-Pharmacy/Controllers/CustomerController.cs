using ASP_Rest_Pharmacy.App_Start;
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
    public class CustomerController : ApiController
    {
        private static List<Customer> customers = new List<Customer>();

        [HttpGet]
        [Route("customers")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(customers);
            // return Ok(JsonConvert.SerializeObject(customers));
        }

        // GET: api/Customer/5
        [HttpGet]
        [Route("customers/{id}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string id)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers.ElementAt(i).ID.Equals(id)) return Ok(customers.ElementAt(i));
            }
            return NotFound();
        }

        // POST: api/Customer
        [HttpPost]
        [Route("customers/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Customer new_cust = JsonConvert.DeserializeObject<Customer>(value);
            customers.Add(new_cust);
            return Ok();
        }

        // PUT: api/Customer/5
        [HttpPut]
        [Route("customers/update/{id}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string id, [FromBody]string value)
        {
            Customer new_cust = JsonConvert.DeserializeObject<Customer>(value);

            for (int i = 0; i < customers.Count; i++)
            {
                if (customers.ElementAt(i).ID.Equals(id))
                {
                    Customer cust = customers.ElementAt(i);
                    cust.Address = new_cust.Address;
                    cust.Birthday = new_cust.Address;
                    cust.Name = new_cust.Name;
                    cust.MedicalHistory = new_cust.MedicalHistory;
                    cust.PhoneNumber = new_cust.PhoneNumber;
                    return Ok();
                }
            }
            return NotFound();
        }

        // DELETE: api/Customer/5
        [HttpDelete]
        [Route("customers/delete/{id}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string id)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers.ElementAt(i).ID.Equals(id))
                {
                    customers.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
