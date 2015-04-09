using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // for INotifyProperty

namespace SinsProto
{
  public class SinsCategoryItem
  {
    public int Value { get; set; }
    public int Index { get; set; }
    public string Description { get; set; }
    public override string ToString()
    {
      return Description;
    }
  }

  public class SinsCategory : INotifyPropertyChanged
  {
    public string Name { get; set; }

    private SinsCategoryItem _score;
    public SinsCategoryItem Score
    {
      get { return _score; }
      set
      {
        _score = value;
        OnPropertyChanged("Score");
      }
    }
 
    public List<SinsCategoryItem> PossibleScores { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

  }
}
