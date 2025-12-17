using System;
using System.Collections.Generic;

namespace K2U2Library;

public partial class Member
{
    public int MemberId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
