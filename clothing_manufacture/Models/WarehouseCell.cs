using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Складская_ячейка")]
public class WarehouseCell
{
    [Key]
    [Column("ID_Складская_ячейка")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Код_ячейки")]
    public string CellCode { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("Зона_хранения")]
    public string? StorageZone { get; set; }

    [MaxLength(500)]
    [Column("Описание")]
    public string? Description { get; set; }

    [Column("FK_Склад")]
    public int WarehouseId { get; set; }

    [ForeignKey(nameof(WarehouseId))]
    public Warehouse Warehouse { get; set; } = null!;

    public ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();

    [NotMapped]
    public string DisplayName => $"{Warehouse?.Name ?? "Склад"} / {CellCode}";
}
