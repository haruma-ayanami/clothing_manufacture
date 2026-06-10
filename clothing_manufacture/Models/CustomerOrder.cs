using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Заказ_клиента")]
public class CustomerOrder
{
    [Key]
    [Column("ID_Заказ_клиента")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Номер_заказа")]
    public string OrderNumber { get; set; } = string.Empty;

    [Required]
    [Column("Дата_заказа", TypeName = "date")]
    public DateTime OrderDate { get; set; }

    [Column("Срок_исполнения", TypeName = "date")]
    public DateTime? DueDate { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Требования_к_брендированию")]
    public string? BrandingRequirements { get; set; }

    [MaxLength(1000)]
    [Column("Комментарий")]
    public string? Comment { get; set; }

    [Column("FK_Клиент")]
    public int ClientId { get; set; }

    [Column("FK_Сотрудник_менеджер")]
    public int? ManagerEmployeeId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; } = null!;

    [ForeignKey(nameof(ManagerEmployeeId))]
    public Employee? ManagerEmployee { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<ProductionTask> ProductionTasks { get; set; } = new List<ProductionTask>();

    [NotMapped]
    public string DisplayName => $"{OrderNumber} — {Client?.Name ?? "клиент"}";
}
