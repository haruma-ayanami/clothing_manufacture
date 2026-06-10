using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Складской_остаток")]
public class WarehouseStock
{
    [Key]
    [Column("ID_Складской_остаток")]
    public int Id { get; set; }

    [Required]
    [Column("Количество", TypeName = "decimal(10,3)")]
    public decimal Quantity { get; set; }

    [Required]
    [Column("Дата_обновления", TypeName = "date")]
    public DateTime UpdateDate { get; set; }

    [Column("FK_Складская_ячейка")]
    public int WarehouseCellId { get; set; }

    [Column("FK_Материал")]
    public int? MaterialId { get; set; }

    [Column("FK_Вариант_модели")]
    public int? ModelVariantId { get; set; }

    [ForeignKey(nameof(WarehouseCellId))]
    public WarehouseCell WarehouseCell { get; set; } = null!;

    [ForeignKey(nameof(MaterialId))]
    public Material? Material { get; set; }

    [ForeignKey(nameof(ModelVariantId))]
    public ModelVariant? ModelVariant { get; set; }

    [NotMapped]
    public string ItemName
    {
        get
        {
            if (Material != null)
                return Material.Name;
            if (ModelVariant != null)
                return ModelVariant.DisplayName;
            return "Не указано";
        }
    }
}
