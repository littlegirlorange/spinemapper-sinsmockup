using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinsProto
{
  public class SampleData
  {
    private static List<SinsCategory> _scoreSheet = new List<SinsCategory>();

    static SampleData()
    {
      string[] descriptions = EnumUtils<sinsCategories>.GetDescriptions();
      foreach (string description in descriptions)
      {
        List<SinsCategoryItem> possibleScores = new List<SinsCategoryItem>();
        string[] possibleDescriptions;
        int[] possibleValues;

        if (description == "Location") {
          possibleDescriptions = EnumUtils<locationScores>.GetDescriptions();
          possibleValues = (int[]) Enum.GetValues(typeof(locationScores));
        }
        else if (description == "Pain") {
          possibleDescriptions =  EnumUtils<painScores>.GetDescriptions();
          possibleValues = (int[]) Enum.GetValues(typeof(painScores));
        }
        else if (description == "Bone lesion") {
          possibleDescriptions =  EnumUtils<boneLesionScores>.GetDescriptions();
          possibleValues = (int[]) Enum.GetValues(typeof(boneLesionScores));
        }
        else if (description == "Radiographic spinal alignment") {
          possibleDescriptions =  EnumUtils<spinalAlignmentScores>.GetDescriptions();
          possibleValues = (int[]) Enum.GetValues(typeof(spinalAlignmentScores));
        }
        else if (description == "Vertebral body collapse") {
          possibleDescriptions =  EnumUtils<collapseScores>.GetDescriptions();
          possibleValues = (int[]) Enum.GetValues(typeof(collapseScores));
        }
        else if (description == "Posterolateral involvement of spinal elements") {
          possibleDescriptions =  EnumUtils<posterolateralInvolvementScores>.GetDescriptions();
          possibleValues = (int[]) Enum.GetValues(typeof(posterolateralInvolvementScores));
        }
        else {
          possibleDescriptions = new string[0];
          possibleValues = new int[0];
        }

        for (int i = 0; i < possibleValues.Length; i++) {
          possibleScores.Add(new SinsCategoryItem { Value = possibleValues[i], Index = i, Description = possibleDescriptions[i] });
        }

        _scoreSheet.Add(new SinsCategory { Name = description, 
          Score = new SinsCategoryItem { Value = possibleValues[1], Index = 1, Description = possibleDescriptions[1] }, PossibleScores=possibleScores });
      }
    }

    public static List<SinsCategory> GetSampleData() { return _scoreSheet; }
  }
}
