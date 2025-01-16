using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Models;
using Project.Messaging.Contracts;

namespace Project.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(IPublishEndpoint endpoint) : ControllerBase
{
    [HttpPost("complete")]
    public async Task<IActionResult> CompleteTask([FromBody] int id)
    {
        if (id <= 0)
            return BadRequest($"{nameof(id)}: Invalid value for ID.");

        CompleteTaskMessage message = new() { Id = id };

        await endpoint.Publish(message);

        // Fire-and-forget for scalability, could await TaskCreatedMessage from Project.Data if client feedback is necessary
        return Ok("Task complete message published.");
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Task))
            return BadRequest($"{nameof(dto.Task)}: Field cannot be empty.");
        if (string.IsNullOrWhiteSpace(dto.AssignedToEmail))
            return BadRequest($"{nameof(dto.AssignedToEmail)}: Field cannot be empty.");

        CreateTaskMessage message = new()
        {
            Task = dto.Task,
            Details = dto.Details,
            DueDate = dto.DueDate,
            AssignedToName = dto.AssignedToName,
            AssignedToEmail = dto.AssignedToEmail
        };

        await endpoint.Publish(message);

        // Fire-and-forget for scalability, could await TaskCreatedMessage from Project.Data if client feedback is necessary
        return Ok("Task create message published.");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteTask([FromBody] int id)
    {
        if (id <= 0)
            return BadRequest($"{nameof(id)}: Invalid value for ID.");

        DeleteTaskMessage message = new() { Id = id };

        await endpoint.Publish(message);

        // Fire-and-forget for scalability, could await TaskCreatedMessage from Project.Data if client feedback is necessary
        return Ok("Task delete message published.");
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateTask([FromBody] TaskUpdateDto dto)
    {
        if (dto.Id <= 0)
            return BadRequest($"{nameof(dto.Id)}: Invalid value for ID.");

        UpdateTaskMessage message = new()
        {
            Id = dto.Id,
            Task = dto.Task,
            Details = dto.Details,
            DueDate = dto.DueDate,
            AssignedToName = dto.AssignedToName,
            AssignedToEmail = dto.AssignedToEmail
        };

        await endpoint.Publish(message);

        // Fire-and-forget for scalability, could await TaskCreatedMessage from Project.Data if client feedback is necessary
        return Ok("Task update message published.");
    }
}
