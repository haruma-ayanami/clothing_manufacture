using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Клиент")]
public class Client
{
    [Key]
    [Column("ID_Клиент")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("Тип_клиента")]
    public string ClientType { get; set; } = string.Empty;

    [MaxLength(30)]
    [Column("Телефон")]
    public string? Phone { get; set; }

    [MaxLength(150)]
    [Column("Email")]
    public string? Email { get; set; }

    [MaxLength(500)]
    [Column("Адрес")]
    public string? Address { get; set; }

    public ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
    public ICollection<ProductOperation> ProductOperations { get; set; } = new List<ProductOperation>();
}
