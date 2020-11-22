using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project001.Models
{
    public class Comments
    {

        public string Id { get; set; }
        public string Content { get; set; }
        public Film Film  { get; set; }

    }
}
