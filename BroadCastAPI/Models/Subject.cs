using System;
using System.Collections.Generic;

namespace BroadCastAPI.Models;

public partial class Subject
{
    public int SubId { get; set; }

    public string SubName { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
