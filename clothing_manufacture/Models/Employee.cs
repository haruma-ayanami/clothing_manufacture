using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Сотрудник")]
public class Employee
{
    [Key]
    [Column("ID_Сотрудник")]
    public int Id { get; set; }

    [MaxLength(50)]
    [Column("Табельный_номер")]
    public string? PersonnelNumber { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [Column("FK_Человек")]
    public int PersonId { get; set; }

    [Column("FK_Должность")]
    public int PositionId { get; set; }

    [ForeignKey(nameof(PersonId))]
    public Person Person { get; set; } = null!;

    [ForeignKey(nameof(PositionId))]
    public Position Position { get; set; } = null!;

    [NotMapped]
    public string DisplayName
    {
        get
        {
            string personName = Person?.FullName ?? $"Сотрудник #{Id}";
            string positionName = Position?.Name ?? "должность не указана";
            return $"{personName} — {positionName}";
        }
    }

    public ICollection<AssortmentPlan> ResponsibleAssortmentPlans { get; set; } = new List<AssortmentPlan>();
    public ICollection<AssortmentPlan> ApprovedAssortmentPlans { get; set; } = new List<AssortmentPlan>();
    public ICollection<ClothingModel> DesignerModels { get; set; } = new List<ClothingModel>();
    public ICollection<ClothingModel> ConstructorModels { get; set; } = new List<ClothingModel>();
    public ICollection<Pattern> ConstructedPatterns { get; set; } = new List<Pattern>();
    public ICollection<Sample> TailorSamples { get; set; } = new List<Sample>();
    public ICollection<Sample> TechnologistSamples { get; set; } = new List<Sample>();
    public ICollection<CustomerOrder> ManagedOrders { get; set; } = new List<CustomerOrder>();
    public ICollection<ProductionTask> PlannedProductionTasks { get; set; } = new List<ProductionTask>();
    public ICollection<QualityControl> QualityControls { get; set; } = new List<QualityControl>();
    public ICollection<ProductOperation> ResponsibleProductOperations { get; set; } = new List<ProductOperation>();
}
