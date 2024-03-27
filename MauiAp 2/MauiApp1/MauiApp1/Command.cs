using System.Runtime.InteropServices.JavaScript;
using System.ComponentModel;

public class Command: INotifyPropertyChanged
{
   public int Id { get; set; }
   
   public String Reference { get; set; }
   public String DateC { get; set; }
   public String Description { get; set; }
   
   public String NameFile { get; set; }

   private bool _showDescr;
   public bool showDescr{
        get { return _showDescr; }
        set
        {
            if (_showDescr != value)
            {
                _showDescr = value;
                OnPropertyChanged("showDescr");
            }
        }
    }



   public Command(int id, String Reference ,String DateC, String description, String NameFile)
   {
      
      this.Id = id;
      this.Reference = Reference; 
      this.DateC = DateC;
      this.Description = description;
      this.NameFile = NameFile; 
      this.showDescr = false;

   }

   public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
   
   

}