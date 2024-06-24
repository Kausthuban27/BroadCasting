using System;
using System.Collections.Generic;

namespace BroadCastAPI.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string StaffName { get; set; } = null!;

    public string StaffEmail { get; set; } = null!;

    public int DeptId { get; set; }

    public int SubId { get; set; }

    public virtual Department Dept { get; set; } = null!;

    public virtual Subject Sub { get; set; } = null!;
}
