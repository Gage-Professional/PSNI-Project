using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Models;

public class TaskEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Task { get; set; }

    [MaxLength(500)]
    public string? Details { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CompletedOn { get; set; }

    [MaxLength(100)]
    public string? AssignedToName { get; set; }

    [Required, EmailAddress, MaxLength(100)]
    public string AssignedToEmail { get; set; }
}