using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(int userId)
        {
            return await _chatRepository.GetMessagesAsync(userId);
        }

        public async Task<ChatMessage> AddMessageAsync(ChatMessage message)
        {
            return await _chatRepository.AddMessageAsync(message);
        }
    }
}
