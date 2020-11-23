using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project001.Models
{
    public class Film
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public Features Features { get; set; }
        public double Duration { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }

    }
}
