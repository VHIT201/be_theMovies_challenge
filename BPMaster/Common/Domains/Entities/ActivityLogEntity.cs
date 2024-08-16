using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domains.Entities
{
    public class ActivityLogEntity<T> : SystemLogEntity<T>
    {  
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set;} = string.Empty;
    }
}
