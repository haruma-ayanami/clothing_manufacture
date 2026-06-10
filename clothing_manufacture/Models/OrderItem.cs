using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Позиция_заказа")]
public class OrderItem
{
    [Key]
    [Column("ID_Позиция_заказа")]
    public int Id { get; set; }

    [Required]
    [Column("Количество")]
    public int Quantity { get; set; }

    [Column("Цена_за_единицу", TypeName = "decimal(18,2)")]
    public decimal? UnitPrice { get; set; }

    [MaxLength(1000)]
    [Column("Комментарий")]
    public string? Comment { get; set; }

    [Column("FK_Заказ_клиента")]
    public int CustomerOrderId { get; set; }

    [Column("FK_Вариант_модели")]
    public int ModelVariantId { get; set; }

    [ForeignKey(nameof(CustomerOrderId))]
    public CustomerOrder CustomerOrder { get; set; } = null!;

    [ForeignKey(nameof(ModelVariantId))]
    public ModelVariant ModelVariant { get; set; } = null!;

    [NotMapped]
    public decimal LineTotal => Quantity * (UnitPrice ?? 0);
}
