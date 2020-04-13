using CleanTesting.Application.Common.Interfaces;
using System;

namespace CleanTesting.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
