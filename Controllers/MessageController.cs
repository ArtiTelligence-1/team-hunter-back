using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunterBackend.DB;
using TeamHunterBackend.Schemas;
using TeamHunterBackend.Services;

namespace TeamHunterBackend.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("Policy")]
    //[DisableCors]
    public class MessageController : ControllerBase
    {
        private readonly EventMessageService _messageService;
        private readonly IGenerateIDService _generateID;

    public MessageController(EventMessageService messageService, IGenerateIDService generateID)
    {
        _messageService = messageService;
        _generateID = generateID;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("getMessages")]
    public async Task<List<Message>> GetAllMessages() =>
        await _messageService.GetMessages();

    [Authorize(Roles = "Admin")]
    [HttpGet("getMessage/{Id}")]
    public async Task<ActionResult<Message>> GetMessageById(int Id)
    {
        var _message = await _messageService.GetMessageById(Id);

        if (_message is null)
        {
            return NotFound();
        }

        return _message;
    }

    [Authorize(Roles = "User")]
    [HttpPost("CreateMessage")]
    public async Task<IActionResult> CreateMessage(Message newMessage)
    {
        newMessage.MessageId = _generateID.GenerateID("message_id");
        await _messageService.CreateMessage(newMessage);

        return CreatedAtAction(nameof(GetMessageById), new { Id = newMessage.MessageId }, newMessage);
    }

    [Authorize(Roles = "User")]
    [HttpPut("UpdateMessage/{Id}")]
    public async Task<IActionResult> UpdateMessage(int Id, Message updatedMessage) 
    {
        var _message = await _messageService.GetMessageById(Id);

        if (_message is null)
        {
            return NotFound();
        }

        updatedMessage.MessageId = _message.MessageId;

        await _messageService.UpdateMessage(Id, updatedMessage);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteMessage/{Id}")]
    public async Task<IActionResult> DeleteMessage(int Id)
    {
        var _message = await _messageService.GetMessageById(Id);

        if (_message is null)
        {
            return NotFound();
        }

        await _messageService.DeleteMessageById(Id);

        return NoContent();
    }

    }

}