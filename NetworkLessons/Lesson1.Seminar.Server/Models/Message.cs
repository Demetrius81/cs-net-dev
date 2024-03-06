using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Seminar.Server.Models;
public class Message
{
    public required string Id { get; set; }
    public string? AuthorId { get; set; }
    public string? ConsumerId { get; set; }
    public required string content { get; set; }
    public bool IsRecived { get; set; }
    public virtual User? Author { get; set; }
    public virtual User? Consumer { get; set; }


}
