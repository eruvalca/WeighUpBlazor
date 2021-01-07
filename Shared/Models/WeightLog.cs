﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeighUpBlazor.Shared.Models
{
    public class WeightLog
    {
        public int WeightLogId { get; set; }
        public decimal WeightMeasurement { get; set; }
        public DateTime MeasurementDate { get; set; } = DateTime.Now;

        public int ContestantId { get; set; }
    }
}
