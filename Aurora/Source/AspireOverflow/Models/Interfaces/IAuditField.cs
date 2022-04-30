namespace AspireOverflow.Models.Interfaces
{

    public interface IAuditField
    {

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}