﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Models
{
    public class University
    {
        [Key]
        public int UniversityId {get; set}

        public string UniversityName {get; set}

        public int RectorId {get; set}

        public Rector Rector {get; set}
        public ICollection<Achievement> Achievements {get; set}

    }
}
