﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BarServiceDAL.BindingModels
{
    [DataContract]
    public class HabitueBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string HabitueFIO { get; set; }
    }
}
