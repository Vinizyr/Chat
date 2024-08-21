using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR_Chat.HubConfig;
using SignalR_Chat.Models;
using System;

namespace SignalR_Chat.Repository
{
    public class UserRepository
    {
        private readonly Context _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public UserRepository(Context context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // Adicionar um novo usuário
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Recupera o total de usuários e envia a atualização para os clientes conectados
            int totalUsers = await _context.Users.CountAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveStatisticsUpdate", totalUsers);
        }

        // Recuperar o total de usuários
        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }
    }
}
