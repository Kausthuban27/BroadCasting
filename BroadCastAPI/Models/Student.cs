using System;
using System.Collections.Generic;

namespace BroadCastAPI.Models;

public partial class Student
{
    public int StuId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StuEmail { get; set; } = null!;

    public int DeptId { get; set; }

    public int Year { get; set; }

    public virtual Department Dept { get; set; } = null!;
}
