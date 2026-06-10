using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Производственное_задание")]
public class ProductionTask
{
    [Key]
    [Column("ID_Производственное_задание")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Номер_задания")]
    public string TaskNumber { get; set; } = string.Empty;

    [Required]
    [Column("Дата_создания", TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [Column("Плановая_дата_начала", TypeName = "date")]
    public DateTime? PlannedStartDate { get; set; }

    [Column("Плановая_дата_окончания", TypeName = "date")]
    public DateTime? PlannedEndDate { get; set; }

    [MaxLength(100)]
    [Column("Источник_запуска")]
    public string? LaunchSource { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Комментарий")]
    public string? Comment { get; set; }

    [Column("FK_Сотрудник_планировщик")]
    public int PlannerEmployeeId { get; set; }

    [Column("FK_Заказ_клиента")]
    public int? CustomerOrderId { get; set; }

    [Column("FK_Коллекция")]
    public int? CollectionId { get; set; }

    [Column("FK_Ассортиментный_план")]
    public int? AssortmentPlanId { get; set; }

    [ForeignKey(nameof(PlannerEmployeeId))]
    public Employee PlannerEmployee { get; set; } = null!;

    [ForeignKey(nameof(CustomerOrderId))]
    public CustomerOrder? CustomerOrder { get; set; }

    [ForeignKey(nameof(CollectionId))]
    public ClothingCollection? Collection { get; set; }

    [ForeignKey(nameof(AssortmentPlanId))]
    public AssortmentPlan? AssortmentPlan { get; set; }

    public ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();

    [NotMapped]
    public string DisplayName => TaskNumber;
}
