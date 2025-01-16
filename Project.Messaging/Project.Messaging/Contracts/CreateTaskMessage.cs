namespace Project.Messaging.Contracts;

public class CreateTaskMessage
{
    public string Task { get; set; }

    public string? Details { get; set; }

    public DateTime? DueDate { get; set; }

    public string? AssignedToName { get; set; }

    public string AssignedToEmail { get; set; }
}