using System.Runtime.InteropServices.JavaScript;

public class Command
{
   public int Id { get; set; }
   
   public String Reference { get; set; }
   public String DateC { get; set; }
   public String Description { get; set; }
   
   public String NameFile { get; set; }



   public Command(int id, String Reference ,String DateC, String description, String NameFile)
   {
      
      this.Id = id;
      this.Reference = Reference; 
      this.DateC = DateC;
      this.Description = description;
      this.NameFile = NameFile; 

   }
   
   

}