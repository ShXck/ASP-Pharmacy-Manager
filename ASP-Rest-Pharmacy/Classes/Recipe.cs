using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Rest_Pharmacy.Classes
{
    public class Recipe
    {
        public int RecpID;
        public string Doctor;
        public List<Medicine> Medicines;
    }
}