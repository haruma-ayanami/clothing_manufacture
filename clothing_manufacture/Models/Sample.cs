using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Образец")]
public class Sample
{
    [Key]
    [Column("ID_Образец")]
    public int Id { get; set; }

    [Required]
    [Column("Дата_изготовления", TypeName = "date")]
    public DateTime ManufactureDate { get; set; }

    [MaxLength(1000)]
    [Column("Результат_примерки")]
    public string? FittingResult { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Комментарий_по_доработке")]
    public string? RevisionComment { get; set; }

    [Column("FK_Модель")]
    public int ModelId { get; set; }

    [Column("FK_Лекало")]
    public int PatternId { get; set; }

    [Column("FK_Сотрудник_портной")]
    public int? TailorEmployeeId { get; set; }

    [Column("FK_Сотрудник_технолог")]
    public int? TechnologistEmployeeId { get; set; }

    [ForeignKey(nameof(ModelId))]
    public ClothingModel Model { get; set; } = null!;

    [ForeignKey(nameof(PatternId))]
    public Pattern Pattern { get; set; } = null!;

    [ForeignKey(nameof(TailorEmployeeId))]
    public Employee? TailorEmployee { get; set; }

    [ForeignKey(nameof(TechnologistEmployeeId))]
    public Employee? TechnologistEmployee { get; set; }

    public ICollection<QualityControl> QualityControls { get; set; } = new List<QualityControl>();

    [NotMapped]
    public string DisplayName => $"Образец #{Id} — {Model?.Article ?? "модель"}";
}
