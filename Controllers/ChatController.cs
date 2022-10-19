using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TeamHunterBackend.Schemas;
using TeamHunterBackend.Services;

namespace TeamHunterBackend.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("Policy")]
    //[DisableCors]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService) =>
            _chatService = chatService;
            
        [HttpGet("getChats")]
        public async Task<List<Chat>> GetAllChats() =>
            await _chatService.GetChats();

        [HttpGet("getChat/{Id}")]
        public async Task<ActionResult<Chat>> GetChatById(int Id)
        {
            var _chat = await _chatService.GetChatById(Id);

            if (_chat is null)
            {
                return NotFound();
            }

            return _chat;
        }

        [HttpPost("CreateChat")]
        public async Task<IActionResult> CreateChat(Chat newChat)
        {
            await _chatService.CreateChat(newChat);

            return CreatedAtAction(nameof(GetChatById), new { Id = newChat.ChatId }, newChat);
        }

        [HttpPut("UpdateChat/{Id}")]
        public async Task<IActionResult> UpdateEvent(int Id, Chat updatedChat) 
        {
            var _chat = await _chatService.GetChatById(Id);

            if (_chat is null)
            {
                return NotFound();
            }

            updatedChat.ChatId = _chat.ChatId;

            await _chatService.UpdateChat(Id, updatedChat);

            return NoContent();
        }

        [HttpDelete("DeleteChat/{Id}")]
        public async Task<IActionResult> DeleteChat(int Id)
        {
            var _chat = await _chatService.GetChatById(Id);

            if (_chat is null)
            {
                return NotFound();
            }

            await _chatService.DeleteChatById(Id);

            return NoContent();
        }
    }
}