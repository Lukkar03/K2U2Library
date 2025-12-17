using System;
using System.Collections.Generic;

namespace K2U2Library;

public partial class Loan
{
    public int LoanId { get; set; }

    public DateOnly LoanDate { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? ReturnDate { get; set; }

    public int FkmemberId { get; set; }

    public int FkbookId { get; set; }

    public virtual Book Fkbook { get; set; } = null!;

    public virtual Member Fkmember { get; set; } = null!;
}
