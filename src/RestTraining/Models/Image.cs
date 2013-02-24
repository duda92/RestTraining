using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestTraining.Domain
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }
    }
}