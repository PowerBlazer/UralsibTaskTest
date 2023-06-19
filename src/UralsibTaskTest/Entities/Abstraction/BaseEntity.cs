using System.ComponentModel.DataAnnotations;

namespace UralsibTaskTest.Entities.Abstraction;

public class BaseEntity<T>
{
    [Key]
    public T? Id { get; set; }
}