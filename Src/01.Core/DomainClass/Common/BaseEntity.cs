using System.ComponentModel.DataAnnotations;

namespace DomainClass.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
