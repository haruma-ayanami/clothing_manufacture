using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Контроль_качества")]
public class QualityControl
{
    [Key]
    [Column("ID_Контроль_качества")]
    public int Id { get; set; }

    [Required]
    [Column("Дата_контроля", TypeName = "date")]
    public DateTime ControlDate { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Тип_контроля")]
    public string ControlType { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("Результат")]
    public string Result { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Замечания")]
    public string? Notes { get; set; }

    [MaxLength(100)]
    [Column("Решение")]
    public string? Decision { get; set; }

    [Column("FK_Производственная_партия")]
    public int? ProductionBatchId { get; set; }

    [Column("FK_Образец")]
    public int? SampleId { get; set; }

    [Column("FK_Сотрудник_контролер")]
    public int ControllerEmployeeId { get; set; }

    [ForeignKey(nameof(ProductionBatchId))]
    public ProductionBatch? ProductionBatch { get; set; }

    [ForeignKey(nameof(SampleId))]
    public Sample? Sample { get; set; }

    [ForeignKey(nameof(ControllerEmployeeId))]
    public Employee ControllerEmployee { get; set; } = null!;
}
