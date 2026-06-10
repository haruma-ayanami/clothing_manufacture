using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Вариант_модели")]
public class ModelVariant
{
    [Key]
    [Column("ID_Вариант_модели")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("Штрихкод")]
    public string? Barcode { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [Column("FK_Модель")]
    public int ModelId { get; set; }

    [Column("FK_Классификатор_цвет")]
    public int ColorClassifierId { get; set; }

    [Column("FK_Классификатор_размер")]
    public int SizeClassifierId { get; set; }

    [ForeignKey(nameof(ModelId))]
    public ClothingModel Model { get; set; } = null!;

    [ForeignKey(nameof(ColorClassifierId))]
    public Classifier ColorClassifier { get; set; } = null!;

    [ForeignKey(nameof(SizeClassifierId))]
    public Classifier SizeClassifier { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();
    public ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();
    public ICollection<ProductOperation> ProductOperations { get; set; } = new List<ProductOperation>();

    [NotMapped]
    public string DisplayName => $"{Model?.Article ?? "Модель"} — {Model?.Name ?? "без названия"} / {ColorClassifier?.Value ?? "цвет"} / {SizeClassifier?.Value ?? "размер"}";
}
