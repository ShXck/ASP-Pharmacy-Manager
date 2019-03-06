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
    public class RoleController : ApiController
    {
        private static List<Role> Roles = new List<Role>();

        /// <summary>
        /// Obtiene la lista de roles.
        /// </summary>
        /// <returns>La lista de roles en formato json</returns>
        [HttpGet]
        [Route("roles")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Roles);
        }

        /// <summary>
        /// Obtiene un rol por su id.
        /// </summary>
        /// <param name="id">el id del rol</param>
        /// <returns>El rol encontrado.</returns>
        [HttpGet]
        [Route("roles/{name}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string name)
        {
            for (int i = 0; i < Roles.Count; i++)
            {
                if (Roles.ElementAt(i).Name.Equals(name)) return Ok(Roles.ElementAt(i));

            }
            return NotFound();
        }

        /// <summary>
        /// Crea un nuevo rol.
        /// </summary>
        /// <param name="value">El json con la información del rol.</param>
        /// <returns>El resultado de la operación.</returns>
        [HttpPost]
        [Route("roles/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Role new_cust = JsonConvert.DeserializeObject<Role>(value);
            Roles.Add(new_cust);
            return Ok();
        }

        /// <summary>
        /// Actualiza la información de un rol.
        /// </summary>
        /// <param name="id">la id del rol.</param>
        /// <param name="value">La información actualizada</param>
        /// <returns>El resultado de la operación.</returns>
        [HttpPut]
        [Route("roles/update/{name}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string name, [FromBody]string value)
        {
            Role new_role = JsonConvert.DeserializeObject<Role>(value);

            for (int i = 0; i < Roles.Count; i++)
            {
                if (Roles.ElementAt(i).Name.Equals(name))
                {
                    Role role = Roles.ElementAt(i);
                    role.Name = new_role.Name;
                    role.Description = new_role.Description;
                    return Ok();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Elimina un role.
        /// </summary>
        /// <param name="id">La id del objeto que quiere ser eliminado</param>
        /// <returns>El resultado de la operación</returns>
        [HttpDelete]
        [Route("roles/delete/{name}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string name)
        {
            for (int i = 0; i < Roles.Count; i++)
            {
                if (Roles.ElementAt(i).Name.Equals(name))
                {
                    Roles.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
