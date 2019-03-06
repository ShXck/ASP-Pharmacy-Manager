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
    public class RecipeController : ApiController
    {
        private static List<Recipe> recipes = new List<Recipe>();

        /// <summary>
        /// Obtiene la lista de recetas.
        /// </summary>
        /// <returns>La lista de recetas en formato json</returns>
        [HttpGet]
        [Route("recipes")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(recipes);
        }

        /// <summary>
        /// Obtiene una receta por su id.
        /// </summary>
        /// <param name="id">el id de la receta</param>
        /// <returns>La receta encontrado.</returns>
        [HttpGet]
        [Route("recipes/{id}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string id)
        {            
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes.ElementAt(i).RecpID.Equals(id)) return Ok(recipes.ElementAt(i));
            }
            return NotFound();
        }

        /// <summary>
        /// Crea un paquete doctor.
        /// </summary>
        /// <param name="value">El json con la información del paquete.</param>
        /// <returns>El resultado de la opreación.</returns>
        [HttpPost]
        [Route("recipes/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Recipe new_cust = JsonConvert.DeserializeObject<Recipe>(value);
            recipes.Add(new_cust);
            return Ok();
        }

        /// <summary>
        /// Actualiza la información de un paquete.
        /// </summary>
        /// <param name="id">la id del paquete.</param>
        /// <param name="value">La información actualizada</param>
        /// <returns>El resultado de la operación.</returns>
        [HttpPut]
        [Route("recipes/update/{id}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string id, [FromBody]string value)
        {
            Recipe new_rcp = JsonConvert.DeserializeObject<Recipe>(value);

            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes.ElementAt(i).RecpID.Equals(id))
                {
                    Recipe recp = recipes.ElementAt(i);
                    recp.Doctor = new_rcp.Doctor;
                    recp.Medicines = new_rcp.Medicines;
                    return Ok();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Elimina un paquete.
        /// </summary>
        /// <param name="id">La id del objeto que quiere ser eliminado</param>
        /// <returns>El resultado de la operación</returns>
        [HttpDelete]
        [Route("recipes/delete/{id}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string id)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes.ElementAt(i).RecpID.Equals(id))
                {
                    recipes.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
