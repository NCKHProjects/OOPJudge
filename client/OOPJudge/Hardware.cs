using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPJudge
{
    class Hardware
    {
        public static string GetCPUID()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = Identifier("Win32_Processor", "UniqueId");
            if (retVal != "") return retVal;
            retVal = Identifier("Win32_Processor", "ProcessorId");
            if (retVal != "") return retVal;
            retVal = Identifier("Win32_Processor", "Name");
            if (retVal == "") //If no Name, use Manufacturer
            {
                retVal = Identifier("Win32_Processor", "Manufacturer");
            }
            //Add clock speed for extra security
            retVal += Identifier("Win32_Processor", "MaxClockSpeed");
            return retVal;
        }
        public static string GetBaseID()
        {
            return Identifier("Win32_BaseBoard", "Model")
            + Identifier("Win32_BaseBoard", "SerialNumber");
        }
        private static string Identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementBaseObject mo in moc)
            {
                //Only get the first one
                if (result != "") continue;
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch
                {
                }
            }
            return result;
        }
    }
}
