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

        /// <summary>
        /// Obtiene la lista de medicinas.
        /// </summary>
        /// <returns>La lista de medicinas en formato json</returns>
        [HttpGet]
        [Route("medicines")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Medicines);
        }

        /// <summary>
        /// Obtiene una medicina por su id.
        /// </summary>
        /// <param name="id">el id de la medicina</param>
        /// <returns>La medicina encontrado.</returns>
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

        /// <summary>
        /// Crea una nueva medicina.
        /// </summary>
        /// <param name="value">El json con la información de la medicina.</param>
        /// <returns>El resultado de la opreación.</returns>
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

        /// <summary>
        /// Actualiza la información de una medicina.
        /// </summary>
        /// <param name="id">la id de la medicina.</param>
        /// <param name="value">La información actualizada</param>
        /// <returns>Ek resultado de la operación.</returns>
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

        /// <summary>
        /// Elimina una medicina.
        /// </summary>
        /// <param name="id">La id del objeto que quiere ser eliminado</param>
        /// <returns>El resultado de la operación</returns>
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
