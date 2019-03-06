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
    public class BranchController : ApiController
    {
        private static List<Branch> Branches = new List<Branch>();

        /// <summary>
        /// Obtiene la lista de sucursales.
        /// </summary>
        /// <returns>La lista de sucursales en formato json</returns>
        [HttpGet]
        [Route("branches")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Branches);
        }

        /// <summary>
        /// Obtiene una sucursal por su id.
        /// </summary>
        /// <param name="id">el id de la sucursal</param>
        /// <returns>La sucursal encontrado.</returns>
        [HttpGet]
        [Route("branches/{name}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string name)
        {
            for (int i = 0; i < Branches.Count; i++)
            {
                if (Branches.ElementAt(i).Name.Equals(name)) return Ok(Branches.ElementAt(i));
            }
            return NotFound();
        }

        /// <summary>
        /// Crea un nueva nueva sucursal.
        /// </summary>
        /// <param name="value">El json con la información de la sucursal.</param>
        /// <returns>El resultado de la opreación.</returns>
        [HttpPost]
        [Route("Branchs/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Branch new_cust = JsonConvert.DeserializeObject<Branch>(value);
            Branches.Add(new_cust);
            return Ok();
        }


        /// <summary>
        /// Actualiza la información de una sucursal.
        /// </summary>
        /// <param name="id">la id de la sucursal.</param>
        /// <param name="value">La información actualizada</param>
        /// <returns>Ek resultado de la operación.</returns>
        [HttpPut]
        [Route("Branchs/update/{name}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string name, [FromBody]string value)
        {
            Branch new_branch = JsonConvert.DeserializeObject<Branch>(value);

            for (int i = 0; i < Branches.Count; i++)
            {
                if (Branches.ElementAt(i).Name.Equals(name))
                {
                    Branch branch = Branches.ElementAt(i);
                    branch.Name = new_branch.Name;
                    branch.Description = new_branch.Description;
                    branch.Address = new_branch.Address;
                    branch.Admin = new_branch.Admin;
                    return Ok();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Elimina una sucursal.
        /// </summary>
        /// <param name="id">La id del objeto que quiere ser eliminado</param>
        /// <returns>El resultado de la operación</returns>
        [HttpDelete]
        [Route("branches/delete/{name}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string name)
        {
            for (int i = 0; i < Branches.Count; i++)
            {
                if (Branches.ElementAt(i).Name.Equals(name))
                {
                    Branches.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
