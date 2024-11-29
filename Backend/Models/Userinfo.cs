using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerDeFavorisApi.Models
{
    public class Userinfo
    {
        public string Prenom { get; set; } = "";
        public string MotDePasse { get; set; } = "";
        public int Id { get; set; } = 0;
    }
}