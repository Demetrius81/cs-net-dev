using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lesson1.Seminar.Server.Models.DTO;
public class MessageDto
{
    public required string Id { get; set; }
    public string? SenderName { get; set; }
    public string? ReciverName { get; set; }
    public required string Text { get; set; }
    public Command Status { get; set; }

    public string Serialize()=>JsonSerializer.Serialize(this);

    public static MessageDto? Deerialize(string json)=> JsonSerializer.Deserialize<MessageDto>(json);
}

public enum Command
{
    Registred,
    Confirmed,
    Message
}
