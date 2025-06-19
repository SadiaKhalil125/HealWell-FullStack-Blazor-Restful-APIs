using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    

    public class ChatRepository : IChatRepository
    {
        private readonly HealWellDbContext _context;

        public ChatRepository(HealWellDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(int userId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.PatientId == userId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
            return messages; 
        }

        public async Task<ChatMessage> AddMessageAsync(ChatMessage message)
        {
            message.Timestamp = DateTime.Now;
            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }

}
