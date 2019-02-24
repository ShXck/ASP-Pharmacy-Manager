using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_Rest_Pharmacy.Classes
{
    public class Package
    {
        public string ID;
        public string PickUpBranch;
        public string Client;
        public int PhoneNumber;
        public List<Medicine> Content;
        public string PickUpTime;
        public string Status;
    }
}