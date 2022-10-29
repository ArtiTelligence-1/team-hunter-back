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
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly IGenerateIDService _generateID;

        public ChatController(ChatService chatService, IGenerateIDService generateID) 
        {
            _chatService = chatService;
            _generateID = generateID;
        }

        [Authorize(Roles = "Admin")]    
        [HttpGet("getChats")]
        public async Task<List<Chat>> GetAllChats() =>
            await _chatService.GetChats();

        [Authorize]
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

        [Authorize]
        [HttpPost("CreateChat")]
        public async Task<IActionResult> CreateChat(Chat newChat)
        {
            newChat.ChatId = _generateID.GenerateID("chat_id");
            await _chatService.CreateChat(newChat);

            return CreatedAtAction(nameof(GetChatById), new { Id = newChat.ChatId }, newChat);
        }

        [Authorize]
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

        [Authorize(Roles = "Admin")]
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