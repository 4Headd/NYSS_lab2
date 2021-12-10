using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class DataComparatorNode
    {
        public static ObservableCollection<ChangedData> CompareData(ObservableCollection<Data> oldData, ObservableCollection<Data> newData)
        {

            ObservableCollection<ChangedData> changedData = new ObservableCollection<ChangedData>();

            int biggestCollection = oldData.Count > newData.Count ? oldData.Count : newData.Count;

            for (int i = 0; i < biggestCollection; i++)
            {
                if (i < oldData.Count && i < newData.Count)
                {
                    if (Comparator.Compare(oldData[i], newData[i]))
                    {
                        Data newRow = new Data(
                            oldData[i].ThreatId,
                            newData[i].ThreatName,
                            newData[i].ThreatDescription,
                            newData[i].ThreatSource,
                            newData[i].ThreatTarget,
                            newData[i].ConfidentialityViolation,
                            newData[i].IntegrityViolation,
                            newData[i].AvailabilityViolation
                            );

                        changedData.Add(new ChangedData(oldData[i], newRow));
                    }
                }
                else
                {
                    if (i >= oldData.Count)
                    {
                        Data newRow = new Data(
                                newData[i].ThreatId,
                                newData[i].ThreatName,
                                newData[i].ThreatDescription,
                                newData[i].ThreatSource,
                                newData[i].ThreatTarget,
                                newData[i].ConfidentialityViolation,
                                newData[i].IntegrityViolation,
                                newData[i].AvailabilityViolation
                                );

                        Data oldRow = new Data(
                            newData[i].ThreatId,
                            "No such threat in local version",
                            "No such threat in local version",
                            "No such threat in local version",
                            "No such threat in local version",
                            false,
                            false,
                            false
                            );

                        changedData.Add(new ChangedData(oldRow, newRow));
                    }
                    else
                    {
                        if (i >= newData.Count)
                        {
                            Data newRow = new Data(
                                oldData[i].ThreatId,
                                "No such threat in actual version",
                                "No such threat in actual version",
                                "No such threat in actual version",
                                "No such threat in actual version",
                                false,
                                false,
                                false
                                );

                            changedData.Add(new ChangedData(oldData[i], newRow));
                        }
                    }
                }

            }


            return changedData;
        }

    }
}
