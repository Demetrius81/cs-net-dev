using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Seminar.Server.Models;
public class User
{
    public required string Id { get; set; }
    public string? Username { get; set; }
    public virtual ICollection<Message> SendedMessage { get; set; } = new List<Message>();
    public virtual ICollection<Message> RecivedMessage { get; set; } = new List<Message>();
}
