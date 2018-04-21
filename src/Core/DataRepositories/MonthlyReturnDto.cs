﻿using TinyReturns.Core.DateExtend;

namespace TinyReturns.Core.DataRepositories
{
    public class MonthlyReturnDto : IMonthAndYear
    {
        public int ReturnSeriesId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal ReturnValue { get; set; }
    }
}