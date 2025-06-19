using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   

    public interface IChatRepository
    {
        Task<List<ChatMessage>> GetMessagesAsync(int userId);
        Task<ChatMessage> AddMessageAsync(ChatMessage message);
    }

}
