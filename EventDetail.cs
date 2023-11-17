using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOleg.DetailFolder
{
    public class EventDetail
    {
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventLocation { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
    }
}
