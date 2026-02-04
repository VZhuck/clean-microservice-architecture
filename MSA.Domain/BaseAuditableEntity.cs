namespace MSA.Domain;

public class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }
}
