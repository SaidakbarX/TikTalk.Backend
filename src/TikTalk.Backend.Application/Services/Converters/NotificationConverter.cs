using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTalk.Backend.Application.Dtos;
using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Services.Converters;

public static class NotificationConverter
{
    public static NotificationDto ToDto(Notification notification) => new()
    {
        Id = notification.Id,
        Title = notification.Title,
        Message = notification.Message,
        IsRead = notification.IsRead,
        CreatedAt = notification.CreatedAt
    };
}