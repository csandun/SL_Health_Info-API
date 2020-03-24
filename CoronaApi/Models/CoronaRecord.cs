using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApi.Models
{
    [Serializable]
    public class CoronaRecord
    {
        public long Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime RecordDate { get; set; }
        public long CasesCount { get; set; }
        public long DeathCount { get; set; }
        public long RecoverCount{ get; set; }
        public long SuspectCount { get; set; }
    }
}
