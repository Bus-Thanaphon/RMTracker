using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class SO_PAUSE : BaseEntity
    {
        public string SOID { get; set; }
        public string Lamination { get; set; }
        public string Cut { get; set; }
        public string Edgebamding { get; set; }
        public string Drill { get; set; }
        public string Painting { get; set; }
        public string Cleaning { get; set; }
        public string Packing { get; set; }
        public string QC { get; set; }
        public string Pickup { get; set; }
    }
}
