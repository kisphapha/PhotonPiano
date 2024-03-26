using System;
using System.Collections.Generic;

namespace PhotonPiano.Models;

public partial class StudentClassScore
{
    public long Id { get; set; }

    public long StudentClassId { get; set; }

    public decimal? Score { get; set; }

    public long CriteriaId { get; set; }

    public virtual Criterion Criteria { get; set; } = null!;

    public virtual StudentClass StudentClass { get; set; } = null!;
}
