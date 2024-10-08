﻿using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Ticket.GetTicket
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var ticket = await repositoryProvider.TicketRepository.GetByTicketIdAndUserIdAsync(request.TicketId, request.UserId)
                ?? throw new HandledException("Обращение не найдено");

            result.Id = ticket.Id;
            result.Title = ticket.Title;
            result.TicketMessages = ticket.TicketMessages.Select(x => new Result.TicketMessage()
            {
                Id = x.Id,
                Message = x.Message,
                Date = x.SendDate,
                UserId = x.UserId,
                Files = x.MessageFiles.Select(x => new Result.File()
                {
                    Id = x.Id,
                    Name = x.FileName,
                    Path = x.Path.Replace("/home/build/wwwroot", "")
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
