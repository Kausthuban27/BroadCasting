using System;
using System.Collections.Generic;

namespace BroadCastingAPI.Models;

public partial class Department
{
    public int DeptId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
