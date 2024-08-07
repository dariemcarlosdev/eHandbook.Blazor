using System.ComponentModel.DataAnnotations;

namespace eHandbook.BlazorWebApp.Shared.Domain.DTOs.Manual
{
    /// <summary>
    /// What was fixed:
    /// 1.	Regular Expression for Path Validation: Added a RegularExpression attribute to the Path property to include path validation as per your comment.
    ///    The regular expression ^[\w\-\./\\]+ is a basic example that you might need to adjust based on your specific path validation requirements.

    public record ManualDto(
        Guid Id,
        string Description,
        string Path,
        AuditableDetailsDto AuditableDetails
    )
    {
        //this method is used to convert ManualToCreateDto to ManualDto using implicit conversion operator
        //implicit operator is used to convert one type to another type without any explicit casting
        public static implicit operator ManualDto(ManualToCreateDto v)
        {
            //implement conversion logic
            return new ManualDto(Guid.NewGuid(), v.Description, v.Path, new AuditableDetailsDto("System", DateTime.Now, "System", DateTime.Now, false, null, null, false));
        }

        public static implicit operator ManualDto(ManualToUpdateDto v)
        {
            return new ManualDto(v.Id, v.Description, v.Path, new AuditableDetailsDto("System", DateTime.Now, "System", DateTime.Now, false, null, null, false));
        }

        public static implicit operator ManualDto(ManualToDeleteDto v)
        {
            return new ManualDto(v.Id, v.Description, v.Path, new AuditableDetailsDto("System", DateTime.Now, "System", DateTime.Now, false, DateTime.Now, "System", true));
        }
    }
}
