using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class StudentClassTuition
{
    public long Id { get; set; }

    public long StudentClassId { get; set; }

    public int Month { get; set; }

    public long? Amount { get; set; }

    public bool Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime DueDate { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? TransactionDescription { get; set; }

    public virtual StudentClass StudentClass { get; set; } = null!;
}
