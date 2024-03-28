using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class EntranceTestResult
{
    public long Id { get; set; }

    public long EntranceTestId { get; set; }

    public decimal? Score { get; set; }

    public long CriteriaId { get; set; }

    public int? Year { get; set; }

    public virtual Criterion Criteria { get; set; } = null!;

    public virtual EntranceTest EntranceTest { get; set; } = null!;
}
