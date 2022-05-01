namespace AspireOverflow.Models.Interfaces
{

    public interface IAuditField
    {

         int CreatedBy { get; set; }

         DateTime CreatedOn { get; set; }
         int? UpdatedBy { get; set; }
         DateTime? UpdatedOn { get; set; }
    }
}