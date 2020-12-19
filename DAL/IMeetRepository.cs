using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.DAL
{
    public interface IMeetRepository : IDisposable
    {
        IList<Meet> GetMeets();
        Meet GetMeetById(int id);
        void InsertMeet(Meet meet);
        void DeleteMeet(Meet meet);
        void UpdateMeet(Meet meet);
        void Save();
    }
}