﻿namespace EmisTracking.Services.Entities
{
    public class Subdivision : BaseEntity
    {
        public string AreaId { get; set; }
        public virtual Area Area { get; set; }

        public string Name { get; set; }
    }
}
