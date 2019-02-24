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
    public class PackageController : ApiController
    {
        private static List<Package> Packages = new List<Package>();

        [HttpGet]
        [Route("packages")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Packages);
        }

        [HttpGet]
        [Route("packages/{id}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string id)
        {
            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages.ElementAt(i).ID.Equals(id)) return Ok(Packages.ElementAt(i));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("packages/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Package new_cust = JsonConvert.DeserializeObject<Package>(value);
            Packages.Add(new_cust);
            return Ok();
        }

        [HttpPut]
        [Route("packages/update/{id}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string id, [FromBody]string value)
        {
            Package new_pckg = JsonConvert.DeserializeObject<Package>(value);
            System.Diagnostics.Debug.WriteLine(value);

            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages.ElementAt(i).ID.Equals(id))
                {
                    Package pckg = Packages.ElementAt(i);
                    pckg.PickUpBranch = new_pckg.PickUpBranch;
                    pckg.PickUpTime = new_pckg.PickUpTime;
                    pckg.PhoneNumber = new_pckg.PhoneNumber;
                    pckg.Status = new_pckg.Status;
                    pckg.Client = new_pckg.Client;
                    pckg.Content = new_pckg.Content;
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("packages/delete/{id}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string id)
        {
            for (int i = 0; i < Packages.Count; i++)
            {
                if (Packages.ElementAt(i).ID.Equals(id))
                {
                    Packages.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
