using Emirhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emirhan
{
    public class Viewmodel
    {
        public List<elektronik> Elektroniklist{ get; set; }     
        public List<giyim> Giyimlist { get; set; }      
        public List<sporoutdoor> Sporoutdoorlist { get; set; }
        public List<saataksesuar> Saataksesuarslist { get; set; }
    }
}