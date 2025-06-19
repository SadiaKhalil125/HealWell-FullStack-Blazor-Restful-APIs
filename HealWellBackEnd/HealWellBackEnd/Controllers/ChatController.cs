using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealWellBackEnd.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatservice;
        private readonly OpenRouterChatService _aiChatService;

        public ChatController(ChatService chatservice, OpenRouterChatService aiChatService)
        {
            _chatservice = chatservice;
            _aiChatService = aiChatService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetMessages(int userId)
        {
            var messages = await _chatservice.GetMessagesAsync(userId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage userMessage)
        {
            userMessage.Timestamp = DateTime.Now;
            userMessage.IsDoctor = false;

            // Store user message
            await _chatservice.AddMessageAsync(userMessage);

            // Get AI response
            var aiReply = await _aiChatService.GetAIResponseAsync(userMessage.Text);

            var aiMessage = new ChatMessage
            {
                PatientId = userMessage.PatientId,
                Text = aiReply.ToString(),
                IsDoctor = true,
                Timestamp = DateTime.Now
            };

            // Store AI message
            await _chatservice.AddMessageAsync(aiMessage);

          
            // Return both messages
            Console.WriteLine(aiMessage.Text);
           return Ok(new[] { userMessage, aiMessage });
        }
    }



}
