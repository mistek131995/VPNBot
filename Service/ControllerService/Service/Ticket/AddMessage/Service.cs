﻿using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Ticket;
using Infrastructure.MailService;

namespace Service.ControllerService.Service.Ticket.AddMessage
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var ticket = await repositoryProvider.TicketRepository.GetByTicketIdAndUserIdAsync(request.TicketId, request.UserId);

            ticket.TicketMessages.Add(new TicketMessage()
            {
                Condition = TicketMessageCondition.New,
                Message = request.Message,
                SendDate = DateTime.Now,
                UserId = request.UserId,
            });

            await repositoryProvider.TicketRepository.UpdateAsync(ticket);


            //Тут оповещение админа о новом сообщении от пользователя
            var adminUsers = await repositoryProvider.UserRepository.GetAllAdminsAsync();
            var adminEmails = adminUsers
                .Select(x => x.Email)
                .ToList();

            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(adminEmails, $"Новое сообщение в тикете {ticket.Id} ({ticket.Title})", @$"
                В тикет {ticket.Id} пришло новое сообщение.</br>
                ----</br>
                {request.Message}</br>
                ----</br>
                </br>
                <a href='https://lockvpn.me/admin/ticket/{ticket.Id}'>Перейти к тикету</a>
            ");

            return true;
        }
    }
}
