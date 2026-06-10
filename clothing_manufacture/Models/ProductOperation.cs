using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Операция_товара")]
public class ProductOperation
{
    [Key]
    [Column("ID_Операция_товара")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Тип_операции")]
    public string OperationType { get; set; } = string.Empty;

    [Required]
    [Column("Дата_операции", TypeName = "date")]
    public DateTime OperationDate { get; set; }

    [Required]
    [Column("Количество", TypeName = "decimal(10,3)")]
    public decimal Quantity { get; set; }

    [Column("Цена_за_единицу", TypeName = "decimal(18,2)")]
    public decimal? UnitPrice { get; set; }

    [MaxLength(100)]
    [Column("Номер_документа")]
    public string? DocumentNumber { get; set; }

    [MaxLength(200)]
    [Column("Получатель")]
    public string? Recipient { get; set; }

    [MaxLength(500)]
    [Column("Адрес_доставки")]
    public string? DeliveryAddress { get; set; }

    [MaxLength(500)]
    [Column("Маршрут")]
    public string? Route { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("Причина_возврата")]
    public string? ReturnReason { get; set; }

    [MaxLength(500)]
    [Column("Решение_по_возврату")]
    public string? ReturnDecision { get; set; }

    [Column("FK_Склад_отправитель")]
    public int? SenderWarehouseId { get; set; }

    [Column("FK_Склад_получатель")]
    public int? ReceiverWarehouseId { get; set; }

    [Column("FK_Материал")]
    public int? MaterialId { get; set; }

    [Column("FK_Вариант_модели")]
    public int? ModelVariantId { get; set; }

    [Column("FK_Клиент")]
    public int? ClientId { get; set; }

    [Column("FK_Сотрудник_ответственный")]
    public int? ResponsibleEmployeeId { get; set; }

    [ForeignKey(nameof(SenderWarehouseId))]
    public Warehouse? SenderWarehouse { get; set; }

    [ForeignKey(nameof(ReceiverWarehouseId))]
    public Warehouse? ReceiverWarehouse { get; set; }

    [ForeignKey(nameof(MaterialId))]
    public Material? Material { get; set; }

    [ForeignKey(nameof(ModelVariantId))]
    public ModelVariant? ModelVariant { get; set; }

    [ForeignKey(nameof(ClientId))]
    public Client? Client { get; set; }

    [ForeignKey(nameof(ResponsibleEmployeeId))]
    public Employee? ResponsibleEmployee { get; set; }

    [NotMapped]
    public decimal OperationTotal => Quantity * (UnitPrice ?? 0);

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
