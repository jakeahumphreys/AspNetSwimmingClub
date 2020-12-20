using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public interface IRoundRepository : IDisposable
    {
        IList<Round> GetRounds();
        Round GetRoundById(int id);
        void InsertRound(Round round);
        void DeleteRound(Round round);
        void UpdateRound(Round round);
        void Save();
    }
}