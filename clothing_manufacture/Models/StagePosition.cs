using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clothing_manufacture.Models;

[Table("Этап_должность")]
public class StagePosition
{
    [Key]
    [Column("ID_Этап_должность")]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("Роль_на_этапе")]
    public string StageRole { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("Описание_взаимодействия")]
    public string? InteractionDescription { get; set; }

    [Column("FK_Этап_процесса")]
    public int ProcessStageId { get; set; }

    [Column("FK_Должность")]
    public int PositionId { get; set; }

    [ForeignKey(nameof(ProcessStageId))]
    public ProcessStage ProcessStage { get; set; } = null!;

    [ForeignKey(nameof(PositionId))]
    public Position Position { get; set; } = null!;
}
