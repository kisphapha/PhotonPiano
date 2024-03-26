using System;
using System.Collections.Generic;

namespace PhotonPiano.Models;

public partial class EntranceTest
{
    public long Id { get; set; }

    public long StudentId { get; set; }

    public decimal? Score { get; set; }

    public long CriteriaId { get; set; }

    public int? Year { get; set; }

    public virtual Criterion Criteria { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
