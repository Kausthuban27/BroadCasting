using System;
using System.Collections.Generic;

namespace BroadCastAPI.Models;

public partial class Participant
{
    public int Id { get; set; }

    public string ParticipantName { get; set; } = null!;

    public string ParticipantEmail { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public int DeptId { get; set; }

    public DateTime DateOfRegistration { get; set; } = DateTime.Now;

    public virtual Department Dept { get; set; } = null!;
}
