using System;
using System.Collections.Generic;

namespace Brainstable.RP5.Data
{
    public class SqlServerDataRp5 : IDataRP5
    {
        public bool ExistValue(string stantionId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public double GetValue(string stantionId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<DateTime, double> GetValues(string stantionId)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<DateTime, double> GetValues(string stantionId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public bool AddValue(string stantionId, DateTime date, double value, bool replace = false)
        {
            throw new NotImplementedException();
        }

        public double?[] GetYearArrays(string stantionId, int year)
        {
            throw new NotImplementedException();
        }

        public double?[] GeyMonthArrays(string stantionId, int year, int numberMonth)
        {
            throw new NotImplementedException();
        }

        public double?[] GetDecadeArrays(string stantionId, int year, int numberDecadeInYear)
        {
            throw new NotImplementedException();
        }

        public double?[] GetDecadeArrays(string stantionId, int year, int numberMonth, int numberDecadeInMonth)
        {
            throw new NotImplementedException();
        }
    }
}