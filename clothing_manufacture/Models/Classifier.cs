using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Классификатор")]
public class Classifier
{
    [Key]
    [Column("ID_Классификатор")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Тип_классификатора")]
    public string ClassifierType { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [Column("Значение")]
    public string Value { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("Код")]
    public string? Code { get; set; }

    [Column("Порядковый_номер")]
    public int? SortOrder { get; set; }

    [MaxLength(500)]
    [Column("Описание")]
    public string? Description { get; set; }

    [Column("FK_Классификатор_родитель")]
    public int? ParentClassifierId { get; set; }

    [ForeignKey(nameof(ParentClassifierId))]
    public Classifier? ParentClassifier { get; set; }

    public ICollection<Classifier> ChildClassifiers { get; set; } = new List<Classifier>();
    public ICollection<ClothingModel> CategoryModels { get; set; } = new List<ClothingModel>();
    public ICollection<ModelVariant> ColorModelVariants { get; set; } = new List<ModelVariant>();
    public ICollection<ModelVariant> SizeModelVariants { get; set; } = new List<ModelVariant>();

    [NotMapped]
    public string DisplayName => string.IsNullOrWhiteSpace(Code)
        ? $"{ClassifierType}: {Value}"
        : $"{ClassifierType}: {Value} ({Code})";
}
