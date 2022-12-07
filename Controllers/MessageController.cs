// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Mvc;
// using TeamHunter.Schemas;
// using TeamHunter.Services;

// namespace TeamHunter.Controllers 
// {
//     [ApiController]
//     [Route("/api/v1/[controller]")]
//     [EnableCors("Policy")]
//     //[DisableCors]
//     public class MessageController : ControllerBase
//     {
//         private readonly EventMessageService _messageService;

//     public MessageController(EventMessageService messageService) =>
//         _messageService = messageService;

//     [HttpGet("getMessages")]
//     public async Task<List<Message>> GetAllMessages() =>
//         await _messageService.GetMessages();

//     [HttpGet("getMessage/{Event}")]
//     public async Task<ActionResult<Message>> GetMessageByEvent(int Event)
//     {
//         var _message = await _messageService.GetMessageById(Event);

//         if (_message is null)
//         {
//             return NotFound();
//         }

//         return _message;
//     }

//     [HttpPost("CreateMessage")]
//     public async Task<IActionResult> CreateMessage(Message newMessage)
//     {
//         await _messageService.CreateMessage(newMessage);

//         return CreatedAtAction(nameof(GetMessageByEvent), new { Event = newMessage.Id }, newMessage);
//     }

//     [HttpPut("UpdateMessage/{Id}")]
//     public async Task<IActionResult> UpdateMessage(int Id, Message updatedMessage) 
//     {
//         var _message = await _messageService.GetMessageById(Id);

//         if (_message is null)
//         {
//             return NotFound();
//         }

//         updatedMessage.Id = _message.Id;

//         await _messageService.UpdateMessage(Id, updatedMessage);

//         return NoContent();
//     }

//     [HttpDelete("DeleteMessage/{Id}")]
//     public async Task<IActionResult> DeleteMessage(int Id)
//     {
//         var _message = await _messageService.GetMessageById(Id);

//         if (_message is null)
//         {
//             return NotFound();
//         }

//         await _messageService.DeleteMessageById(Id);

//         return NoContent();
//     }

//     }

// }