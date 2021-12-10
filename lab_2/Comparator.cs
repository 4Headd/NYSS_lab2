using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class Comparator
    {
        public static bool Compare(Data lData, Data rData)
        {
            return (lData?.ThreatName != rData?.ThreatName ||
                    lData?.ThreatDescription != rData?.ThreatDescription ||
                    lData?.ThreatSource != rData?.ThreatSource ||
                    lData?.ThreatTarget != rData?.ThreatTarget ||
                    lData?.ConfidentialityViolation != rData?.ConfidentialityViolation ||
                    lData?.IntegrityViolation != rData?.IntegrityViolation ||
                    lData?.AvailabilityViolation != rData?.AvailabilityViolation
                    );
        }

    }
}
