using System;
using System.Collections.Generic;
using System.Text;

namespace OOPJudge
{
    struct SubmissionInfo
    {
        public string ID;
        public DateTime TimeSubmit;
        public long Size;
        public string Status;
        public string Result;
        public string Note;
        public string FullPath;

        public void Report(string status, string result, string note)
        {
            this.Status = status; this.Result = result; this.Note = note;
        }
        public void Report(string status)
        {
            Report(status, "", "");
        }
        public void Report(string status, string result)
        {
            Report(status, result, "");
        }
    }
}
