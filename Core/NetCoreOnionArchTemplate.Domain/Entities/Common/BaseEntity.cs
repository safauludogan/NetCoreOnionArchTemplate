﻿namespace NetCoreOnionArchTemplate.Domain.Entities.Common
{
    public class BaseEntity : IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
