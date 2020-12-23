using System;
using System.Collections.Generic;

namespace Brainstable.RP5.Data
{
    public interface IDataRP5
    {
        bool ExistValue(string stantionId, DateTime date);
        double GetValue(string stantionId, DateTime date);
        IReadOnlyDictionary<DateTime, double> GetValues(string stantionId);
        IReadOnlyDictionary<DateTime, double> GetValues(string stantionId, DateTime start, DateTime end);
        bool AddValue(string stantionId, DateTime date, double value, bool replace = false);
        double?[] GetYearArrays(string stantionId, int year); // 365 или 364 значения с 1 января, если значения нет, то null
        double?[] GeyMonthArrays(string stantionId, int year, int numberMonth);
        double?[] GetDecadeArrays(string stantionId, int year, int numberDecadeInYear);
        double?[] GetDecadeArrays(string stantionId, int year, int numberMonth, int numberDecadeInMonth);
    }
}
