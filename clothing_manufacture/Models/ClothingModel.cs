using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Модель")]
public class ClothingModel
{
    [Key]
    [Column("ID_Модель")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Артикул")]
    public string Article { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("Наименование")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Описание")]
    public string? Description { get; set; }

    [MaxLength(1000)]
    [Column("Технологическая_карта")]
    public string? TechnologicalCard { get; set; }

    [MaxLength(150)]
    [Column("Бренд")]
    public string? Brand { get; set; }

    [MaxLength(250)]
    [Column("Логотип")]
    public string? Logo { get; set; }

    [MaxLength(100)]
    [Column("Фирменный_цвет")]
    public string? BrandColor { get; set; }

    [MaxLength(1000)]
    [Column("Требования_брендбука")]
    public string? BrandbookRequirements { get; set; }

    [MaxLength(100)]
    [Column("Способ_брендирования")]
    public string? BrandingMethod { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Статус")]
    public string Status { get; set; } = string.Empty;

    [Column("FK_Классификатор_категория")]
    public int CategoryClassifierId { get; set; }

    [Column("FK_Коллекция")]
    public int? CollectionId { get; set; }

    [Column("FK_Сотрудник_дизайнер")]
    public int? DesignerEmployeeId { get; set; }

    [Column("FK_Сотрудник_конструктор")]
    public int? ConstructorEmployeeId { get; set; }

    [ForeignKey(nameof(CategoryClassifierId))]
    public Classifier CategoryClassifier { get; set; } = null!;

    [ForeignKey(nameof(CollectionId))]
    public ClothingCollection? Collection { get; set; }

    [ForeignKey(nameof(DesignerEmployeeId))]
    public Employee? DesignerEmployee { get; set; }

    [ForeignKey(nameof(ConstructorEmployeeId))]
    public Employee? ConstructorEmployee { get; set; }

    public ICollection<ModelVariant> Variants { get; set; } = new List<ModelVariant>();
    public ICollection<ModelMaterial> ModelMaterials { get; set; } = new List<ModelMaterial>();
    public ICollection<Pattern> Patterns { get; set; } = new List<Pattern>();
    public ICollection<Sample> Samples { get; set; } = new List<Sample>();

    [NotMapped]
    public string DisplayName => $"{Article} — {Name}";
}
