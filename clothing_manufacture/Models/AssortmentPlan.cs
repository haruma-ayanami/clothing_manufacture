using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Ассортиментный_план")]
public class AssortmentPlan
{
    [Key]
    [Column("ID_Ассортиментный_план")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Номер_плана")]
    public string PlanNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("Период")]
    public string Period { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Комментарий")]
    public string? Comment { get; set; }

    [Column("FK_Сотрудник_ответственный")]
    public int? ResponsibleEmployeeId { get; set; }

    [Column("FK_Сотрудник_утвердивший")]
    public int? ApprovedEmployeeId { get; set; }

    [ForeignKey(nameof(ResponsibleEmployeeId))]
    public Employee? ResponsibleEmployee { get; set; }

    [ForeignKey(nameof(ApprovedEmployeeId))]
    public Employee? ApprovedEmployee { get; set; }

    public ICollection<ClothingCollection> Collections { get; set; } = new List<ClothingCollection>();
    public ICollection<ProductionTask> ProductionTasks { get; set; } = new List<ProductionTask>();

    [NotMapped]
    public string DisplayName => $"{PlanNumber} — {Period}";
}
