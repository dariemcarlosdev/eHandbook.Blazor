namespace eHandbook.BlazorWebApp.Shared.Domain.Entities
{

    //Records are immutable by default, which means that once you create a record, you can't change its properties.
    public record Manual(Guid Id, string? Description, string? Path, AuditableDetails? AuditableDetails);

}
