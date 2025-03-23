using EmisTracking.Services.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.Services.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [Required]
        [MaxLength(36)] // GUID length
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
