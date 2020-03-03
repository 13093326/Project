﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class QuestionRating
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public DateTime Time { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
