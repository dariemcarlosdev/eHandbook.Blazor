namespace eHandbook.BlazorWebApp.Shared.Domain.EntitiesModels
{
    public record AuditableDetails(string? CreatedBy,
        DateTime? CreatedOn, string? UpdatedBy, DateTime? UpdatedOn,
        bool IsUpdated, DateTime? DeletedOn, string? DeletedBy, bool IsDeleted);
}
