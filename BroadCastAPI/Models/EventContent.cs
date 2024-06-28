using System;
using System.Collections.Generic;

namespace BroadCastAPI.Models;

public partial class EventContent
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;
}
