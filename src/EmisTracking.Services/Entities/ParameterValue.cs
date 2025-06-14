﻿namespace EmisTracking.Services.Entities
{
    public class ParameterValue : BaseEntity
    {
        public string MethodologyParameterId { get; set; }
        public virtual MethodologyParameter MethodologyParameter { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double Value { get; set; }

        public string SourceSubstanceId { get; set; }
        public virtual SourceSubstance SourceSubstance { get; set; }
    }
}
