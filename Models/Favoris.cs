using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TrackerDeFavorisApi.Models
{
    public class Favoris
    {
        public int Id { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int FilmId { get; set; } = 0;
    }
}