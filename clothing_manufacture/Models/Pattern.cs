using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Лекало")]
public class Pattern
{
    [Key]
    [Column("ID_Лекало")]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Версия")]
    public string Version { get; set; } = string.Empty;

    [Required]
    [Column("Дата_создания", TypeName = "date")]
    public DateTime CreationDate { get; set; }

    [MaxLength(300)]
    [Column("Файл_лекала")]
    public string? PatternFile { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Комментарий")]
    public string? Comment { get; set; }

    [Column("FK_Модель")]
    public int ModelId { get; set; }

    [Column("FK_Сотрудник_конструктор")]
    public int ConstructorEmployeeId { get; set; }

    [ForeignKey(nameof(ModelId))]
    public ClothingModel Model { get; set; } = null!;

    [ForeignKey(nameof(ConstructorEmployeeId))]
    public Employee ConstructorEmployee { get; set; } = null!;

    public ICollection<Sample> Samples { get; set; } = new List<Sample>();

    [NotMapped]
    public string DisplayName => $"{Model?.Article ?? "Модель"} — версия {Version}";
}
