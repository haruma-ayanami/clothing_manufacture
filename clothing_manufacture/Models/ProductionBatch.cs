using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Производственная_партия")]
public class ProductionBatch
{
    [Key]
    [Column("ID_Производственная_партия")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Номер_партии")]
    public string BatchNumber { get; set; } = string.Empty;

    [Required]
    [Column("Количество")]
    public int Quantity { get; set; }

    [Column("Дата_запуска", TypeName = "date")]
    public DateTime? StartDate { get; set; }

    [Column("Дата_завершения", TypeName = "date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [Column("FK_Производственное_задание")]
    public int ProductionTaskId { get; set; }

    [Column("FK_Вариант_модели")]
    public int ModelVariantId { get; set; }

    [ForeignKey(nameof(ProductionTaskId))]
    public ProductionTask ProductionTask { get; set; } = null!;

    [ForeignKey(nameof(ModelVariantId))]
    public ModelVariant ModelVariant { get; set; } = null!;

    public ICollection<QualityControl> QualityControls { get; set; } = new List<QualityControl>();

    [NotMapped]
    public string DisplayName => BatchNumber;
}
