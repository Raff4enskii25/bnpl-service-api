using System;
using System.Collections.Generic;
using System.Text;

namespace Bnpl.Domain.Logic
{
    public record InstallmentScheduleItem(decimal Amount, int Number, DateOnly Date);
}
