using System;
using System.Collections.Generic;
using System.Text;

namespace OOPJudge
{
    struct LabInfo : IComparable
    {
        public int Mode;
        public string Name;
        public string FullName;
        public DateTime Start;
        public DateTime End;
        public int Elapse;
        public string Path;
        public string[] Files;



        int IComparable.CompareTo(object obj)
        {
            if (obj is LabInfo)
            {
                LabInfo lInfo = (LabInfo)obj;
                int r = this.Start.CompareTo(lInfo.Start);
                if (r != 0)
                    return r;
                r = this.End.CompareTo(lInfo.End);
                if (r != 0)
                    return r;
                return this.Name.CompareTo(lInfo.Name);
            }
            return 0;
        }
    }
}
