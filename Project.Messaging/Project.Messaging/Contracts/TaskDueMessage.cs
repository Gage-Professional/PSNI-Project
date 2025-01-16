namespace Project.Messaging.Contracts;

public class TaskDueMessage
{
    public int Id { get; set; }

    public string Task { get; set; }

    public string? Details { get; set; }

    public DateTime DueDate { get; set; }

    public string? AssignedToName { get; set; }

    public string AssignedToEmail { get; set; }
}