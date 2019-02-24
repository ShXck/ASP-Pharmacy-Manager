﻿using ASP_Rest_Pharmacy.Classes;
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
        private static List<Recipe> Recipes = new List<Recipe>();

        [HttpGet]
        [Route("recipes")]
        [DisableCors]
        public IHttpActionResult Get()
        {
            return Ok(Recipes);
        }

        [HttpGet]
        [Route("recipes/{id}")]
        [DisableCors]
        public IHttpActionResult Get([FromUri]string id)
        {
            for (int i = 0; i < Recipes.Count; i++)
            {
                if (Recipes.ElementAt(i).RecpID.Equals(id)) return Ok(Recipes.ElementAt(i));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("recipes/new")]
        [DisableCors]
        public IHttpActionResult Post([FromBody]string value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Recipe new_cust = JsonConvert.DeserializeObject<Recipe>(value);
            Recipes.Add(new_cust);
            return Ok();
        }

        [HttpPut]
        [Route("recipes/update/{id}")]
        [DisableCors]
        public IHttpActionResult Put([FromUri]string id, [FromBody]string value)
        {
            Recipe new_rcp = JsonConvert.DeserializeObject<Recipe>(value);

            for (int i = 0; i < Recipes.Count; i++)
            {
                if (Recipes.ElementAt(i).RecpID.Equals(id))
                {
                    Recipe recp = Recipes.ElementAt(i);
                    recp.Doctor = new_rcp.Doctor;
                    recp.Medicines = new_rcp.Medicines;
                    return Ok();
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("recipes/delete/{id}")]
        [DisableCors]
        public IHttpActionResult Delete([FromUri] string id)
        {
            for (int i = 0; i < Recipes.Count; i++)
            {
                if (Recipes.ElementAt(i).RecpID.Equals(id))
                {
                    Recipes.RemoveAt(i);
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}