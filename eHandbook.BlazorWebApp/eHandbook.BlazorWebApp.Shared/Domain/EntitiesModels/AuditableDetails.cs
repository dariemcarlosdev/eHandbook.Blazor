namespace eHandbook.BlazorWebApp.Shared.Domain.Entities
{
    public record AuditableDetails(string? CreatedBy,
        DateTime? CreatedOn, string? UpdatedBy, DateTime? UpdatedOn,
        bool IsUpdated, DateTime? DeletedOn, string? DeletedBy, bool IsDeleted);
}
