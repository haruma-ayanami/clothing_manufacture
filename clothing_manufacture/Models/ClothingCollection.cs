using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Коллекция")]
public class ClothingCollection
{
    [Key]
    [Column("ID_Коллекция")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("Сезон")]
    public string? Season { get; set; }

    [Column("Год")]
    public int? Year { get; set; }

    [MaxLength(100)]
    [Column("Ценовой_сегмент")]
    public string? PriceSegment { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [Column("FK_Ассортиментный_план")]
    public int? AssortmentPlanId { get; set; }

    [ForeignKey(nameof(AssortmentPlanId))]
    public AssortmentPlan? AssortmentPlan { get; set; }

    public ICollection<ClothingModel> Models { get; set; } = new List<ClothingModel>();
    public ICollection<ProductionTask> ProductionTasks { get; set; } = new List<ProductionTask>();

    [NotMapped]
    public string DisplayName => Year == null ? Name : $"{Name} ({Year})";
}
