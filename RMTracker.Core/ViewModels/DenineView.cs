﻿using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.ViewModels
{
    public class DenineView
    {
        public Sub_C2B Subc2bs { get; set; }
        public s_Lamination SLaminations { get; set; }
        public s_Cut SCuts { get; set; }
        public s_Edgebanding SEdgebandings { get; set; }
        public s_Drill SDrills { get; set; }
        public s_Painting SPaintings { get; set; }
        public s_Cleaning SCleanings { get; set; }
        public s_Packing SPackings { get; set; }
        public s_QC SQCs { get; set; }
        public s_Pickup SPickups { get; set; }
        public WorksDenine WorksDenine { get; set; }
        public WorksPause WorksPause { get; set; }
    }
}
