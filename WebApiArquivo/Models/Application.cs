using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class Application
    {
		[Key]
		public int application { get; set; }
		public string url { get; set; }
		public string pathLocal { get; set; }
		[DefaultValue(false)]
		public bool debuggingMode { get; set; }

	}

}

