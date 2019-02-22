using ASP_Rest_Pharmacy.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASP_Rest_Pharmacy.Controllers
{
    public class CustomerController : ApiController
    {
        private static List<Customer> customers = new List<Customer>();

        [HttpGet]
        [Route("customers")]
        public IHttpActionResult Get()
        {
            return Ok(JsonConvert.SerializeObject(customers));
        }

        // GET: api/Customer/5
        [HttpGet]
        [Route("customers/{id}")]
        public IHttpActionResult Get([FromUri]string id)
        {
            for(int i = 0; i < customers.Count; i++)
            {
                if (customers.ElementAt(i).ID.Equals(id)) return Ok(JsonConvert.SerializeObject(customers.ElementAt(i)));
            }
            return NotFound();
        }

        // POST: api/Customer
        [HttpPost]
        [Route("customers/new")]
        public IHttpActionResult Post([FromBody]string value)
        { 
            Customer new_cust = JsonConvert.DeserializeObject<Customer>(value);
            customers.Add(new_cust);
            return Ok();
        }

        // PUT: api/Customer/5
        [HttpPut]
        [Route("customers/update/{id}")]
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
