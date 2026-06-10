using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Склад")]
public class Warehouse
{
    [Key]
    [Column("ID_Склад")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("Тип_склада")]
    public string WarehouseType { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("Адрес")]
    public string? Address { get; set; }

    public ICollection<WarehouseCell> WarehouseCells { get; set; } = new List<WarehouseCell>();
    public ICollection<ProductOperation> SenderOperations { get; set; } = new List<ProductOperation>();
    public ICollection<ProductOperation> ReceiverOperations { get; set; } = new List<ProductOperation>();
}
