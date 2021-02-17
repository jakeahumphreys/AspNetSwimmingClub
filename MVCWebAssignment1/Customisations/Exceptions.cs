using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.Customisations
{
    public class VenueNotFoundException : Exception
    {
        public VenueNotFoundException(string message) : base(message) { }
    }
}