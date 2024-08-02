namespace eHandbook.BlazorWebApp.Shared.Domain.DTOs.Manual
{
   public record AuditableDetailsDto(
        string CreatedBy,
        DateTime CreatedOn,
        string? UpdatedBy,
        DateTime? UpdatedOn,
        bool IsUpdated,
        DateTime? DeletedOn,
        string? DeletedBy,
        bool IsDeleted
    );
}
