using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection; // for enum utils FieldInfo
using System.ComponentModel; // for enum Description


namespace SinsProto
{
  #region SinScore categories, values and explanations
    public enum sinsCategories {
      [Description("Location")]
      location,
      [Description("Pain")]
      pain,
      [Description("Bone lesion")]
      boneLesion,
      [Description("Radiographic spinal alignment")]
      spinalAlignment,
      [Description("Vertebral body collapse")]
      collapse,
      [Description("Posterolateral involvement of spinal elements")]
      posterolateralInvolvement
    }

    // Location
    public enum locationScores
    {
      [Description("3 - Junctional (occiput-C2, C7-T2, T11-L1, L5-S1)")]
      junctional = 3,
      [Description("2 - Mobile spine (C3-C6, L2-L4)")]
      mobile = 2,
      [Description("1 - Semirigid (T3-T10)")]
      semirigid = 1,
      [Description("0 - Rigid (S2-S5)")]
      rigid = 0
    };

    // Pain
    public enum painScores
    {
      [Description("3 - Yes")]
      yes = 3,
      [Description("1 - Occasional pain but not mechanical")]
      occasional = 1,
      [Description("0 - Pain-free lesion")]
      painFree = 0
    };

    // Bone lesion
    public enum boneLesionScores
    {
      [Description("2 - Lytic")]
      lytic = 2,
      [Description("1 - Mixed (lytic/blastic)")]
      mixed = 1,
      [Description("0 - Blastic")]
      blastic = 0
    };

    // Radiographic spinal alignment
    public enum spinalAlignmentScores
    {
      [Description("4 - Subluxation/translation present")]
      subluxationTranslation = 4,
      [Description("2 - De novo deformity (kyphosis/scoliosis)")]
      denovo = 2,
      [Description("0 - Normal alignment")]
      normal = 0
    };

    // Vertebral body collapse
    public enum collapseScores
    {
      [Description("3 - >50% collapse")]
      greaterThanFiftyPercent = 3,
      [Description("2 - <50% collapse")]
      lessThanFiftyPercent = 2,
      [Description("1 - No collapse with >50% body involvement")]
      noCollapseWithBodyInvolvment = 1,
      [Description("0 - None of the above")]
      noneOfTheAbove = 0
    };

    // Posterolateral involvement of spinal elements
    public enum posterolateralInvolvementScores
    {
      [Description("3 - Bilateral")]
      bilateral = 3,
      [Description("1 - Unilateral")]
      unilateral = 1,
      [Description("0 - None of the above")]
      noneOfTheAbove = 0
    };
  #endregion SinScore categories, values and explanations

  public class Sins
  {
    private List<SinsCategory> _sinScorecard;

    public Sins(List<SinsCategory> scorecard){
      _sinScorecard = scorecard;
    }

    public List<SinsCategory> SinScorecard {
      get { return _sinScorecard; }
      set { _sinScorecard = value; }
    }

    public int CalculateTotal()
    {
        return _sinScorecard.Sum(item => item.Score.Value);
    }

    public string CalculateDiagnosis()
    {
      int total = CalculateTotal();
      if (1 <= total && total <= 6) {
        return "1-6 - Stable";
      }
      else if (6 < total && total <= 12) {
        return "7-12 - Potentially unstable: Surgical consultation recommended";
      }
      else if (12 < total && total <= 18) {
        return "13-18 - Unstable: Surgical consultation recommended";
      }
      else {
        return String.Format("{0} - Invald score", total);
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged != null) {

        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

  }
    /// <summary>
    /// Enum Utilities
    /// </summary>
    public class EnumUtils<T>
    {
      public static string GetDescription(T enumValue, string defDesc)
      {
        FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

        if (null != fi) {
          object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
          if (attrs != null && attrs.Length > 0)
            return ((DescriptionAttribute)attrs[0]).Description;
        }

        return defDesc;
      }

      /// <summary>
      /// Gets the string description of an enum value
      /// </summary>
      public static string GetDescription(T enumValue)
      {
        return GetDescription(enumValue, string.Empty);
      }

      /// <summary>
      /// Gets all string descriptions for the enum
      /// </summary>
      public static string[] GetDescriptions()
      {
        string[] descs = new string[EnumUtils<T>.Length];
        for (int i = 0; i < EnumUtils<T>.Length; i++) {
          descs[i] = GetDescription(EnumUtils<T>.NumToEnum(i));
        }
        return descs;
      }

      /// <summary>
      /// Gets the enum value of a string description
      /// </summary>
      public static T GetEnumValue(string description)
      {
        Type t = typeof(T);
        foreach (FieldInfo fi in t.GetFields()) {
          object[] attrs = fi.GetCustomAttributes
        (typeof(DescriptionAttribute), true);
          if (attrs != null && attrs.Length > 0) {
            foreach (DescriptionAttribute attr in attrs) {
              if (attr.Description.Equals(description))
                return (T)fi.GetValue(null);
            }
          }
        }
        return default(T);
      }

      /// <summary>
      /// Returns the length of the enum
      /// </summary>
      public static int Length
      {
        get { return Enum.GetNames(typeof(T)).Length; }
      }

      /// <summary>
      /// Returns the value of an enum from its index
      /// </summary>
      public static T NumToEnum(int num)
      {
        Array values = Enum.GetValues(typeof(T));
        T value = (T)values.GetValue(num);
        return value;
      }
    }

}
