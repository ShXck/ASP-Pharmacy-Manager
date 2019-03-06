
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

        /// <summary>
        /// Obtiene la lista de doctores.
        /// </summary>
        /// <returns>La lista de doctores en formato json</returns>
        [HttpGet]
        [Route("doctors")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Doctors);
        }

        /// <summary>
        /// Obtiene un doctor por su id.
        /// </summary>
        /// <param name="id">el id del doctor</param>
        /// <returns>El doctor encontrado.</returns>
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

        /// <summary>
        /// Crea un nuevo doctor.
        /// </summary>
        /// <param name="value">El json con la información del doctor.</param>
        /// <returns>El resultado de la opreación.</returns>
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

        /// <summary>
        /// Actualiza la información de un cliente.
        /// </summary>
        /// <param name="id">la id del cliente.</param>
        /// <param name="value">La información actualizada</param>
        /// <returns>El resultado de la operación.</returns>
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
                    doc.Lastname = new_doc.Lastname;
                    doc.ID = new_doc.ID;
                    return Ok();
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Elimina un doctor.
        /// </summary>
        /// <param name="id">La id del objeto que quiere ser eliminado</param>
        /// <returns>El resultado de la operación</returns>
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
