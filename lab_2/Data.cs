using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab_2
{

    public class Data
    {
        public int ThreatId { get; set; }

        public string ThreatName { get; set; }

        public string ThreatDescription { get; set; }

        public string ThreatSource { get; set; }

        
        public string ThreatTarget { get; set; }


        public bool ConfidentialityViolation { get; set; }


        public bool IntegrityViolation { get; set; }


        public bool AvailabilityViolation { get; set; }

        public Data(int threatId,
            string threatName, 
            string threatDescription,
            string threatSource,
            string threatTarget, 
            bool confidentialityViolation,
            bool integrityViolation,
            bool availabilityViolation
            )
        {
            ThreatId = threatId;
            ThreatName = threatName?.Replace("_x000D_", "");
            ThreatDescription = threatDescription?.Replace("_x000D_", "");
            ThreatSource = threatSource?.Replace("_x000D_", "");
            ThreatTarget = threatTarget?.Replace("_x000D_", "");
            ConfidentialityViolation = confidentialityViolation;
            IntegrityViolation = integrityViolation;
            AvailabilityViolation = availabilityViolation;
        }
    }
}
