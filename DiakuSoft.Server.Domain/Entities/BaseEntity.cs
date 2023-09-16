
namespace DiakuSoft.Server.Domain.Entities;

public class BaseEntity<T>
{
    public T? Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}
