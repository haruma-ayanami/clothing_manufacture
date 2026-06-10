using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Материал")]
public class Material
{
    [Key]
    [Column("ID_Материал")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("Тип_материала")]
    public string MaterialType { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    [Column("Единица_измерения")]
    public string UnitOfMeasure { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("Описание")]
    public string? Description { get; set; }

    public ICollection<ModelMaterial> ModelMaterials { get; set; } = new List<ModelMaterial>();
    public ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();
    public ICollection<ProductOperation> ProductOperations { get; set; } = new List<ProductOperation>();
}
