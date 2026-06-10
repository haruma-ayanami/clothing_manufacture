using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Модель_материал")]
public class ModelMaterial
{
    [Key]
    [Column("ID_Модель_материал")]
    public int Id { get; set; }

    [Required]
    [Column("Количество_на_изделие", TypeName = "decimal(10,3)")]
    public decimal QuantityPerItem { get; set; }

    [Required]
    [Column("Основной_материал")]
    public bool IsMainMaterial { get; set; }

    [Column("FK_Модель")]
    public int ModelId { get; set; }

    [Column("FK_Материал")]
    public int MaterialId { get; set; }

    [ForeignKey(nameof(ModelId))]
    public ClothingModel Model { get; set; } = null!;

    [ForeignKey(nameof(MaterialId))]
    public Material Material { get; set; } = null!;
}
