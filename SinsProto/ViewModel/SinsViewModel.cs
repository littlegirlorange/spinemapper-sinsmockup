using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;  // for ICommand
using System.ComponentModel; // for INotifyProperty
using System.Collections.ObjectModel; // for ObservableCollection

namespace SinsProto
{
  public class SinsViewModel : INotifyPropertyChanged
  {

    private Sins _sins;
    private ObservableCollection<SinsCategory> _scorecard;
    private OcPropertyChangedListener<SinsCategory> _listener;

    public SinsViewModel()
    {
      // Populate SINS with data.
      _sins = new Sins(SampleData.GetSampleData());

      // Get the scorecard from the SINS object for display.
      _scorecard = new ObservableCollection<SinsCategory>(_sins.SinScorecard);

      // Listen for changes in the scores.
      _listener = OcPropertyChangedListener.Create(_scorecard);
      _listener.PropertyChanged += (sender, args) => { OnPropertyChanged("Total"); OnPropertyChanged("Diagnosis"); };
    }

    public ObservableCollection<SinsCategory> Scorecard
    {
      get { return _scorecard; }
      set { _scorecard = value; }
   }

    public Int32 Total
    {
      get { return _sins.CalculateTotal(); }
    }

    public string Diagnosis
    {
      get { return _sins.CalculateDiagnosis(); }
    }

    private readonly ICommand _calculateTotalCommand;
    public ICommand CalculateTotalCommand
    {
      get { return _calculateTotalCommand; }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

  }

  public class RelayCommand : ICommand
  {
    private Action _whatToExecute;
    private Func<bool> _whenToExecute;
    public RelayCommand(Action what, Func<bool> when)
    {
      _whatToExecute = what;
      _whenToExecute = when;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return _whenToExecute();
    }

    public void Execute(object parameter)
    {
      _whatToExecute();
    }
  }
}
