public class Element
{

    public String Casier { get; set; }
    public String depth { get; set; }
    public String length { get; set; }
    public String nbBoxe { get; set; }
    public String color { get; set; }
    public String heigth { get; set; }
    public String doors { get; set; }
    public String typeDoors { get; set; }
    public String colorDoors { get; set; }
    
    public String boxe { get; set; }
    
    public bool isEditingL{get;set;}
    
    public bool isEditingE{get;set;}
    
    
    
    public Element(String Casier, String depth, String length, String nbBoxe, String color, String heigth, String doors, String typeDoors, String colorDoors,String boxe, bool editL, bool editE)
    {
        this.Casier = Casier;
        this.depth = depth;
        this.length = length;
        this.nbBoxe = nbBoxe;
        this.color = color; 
        this.heigth = heigth;
        this.doors = doors;
        this.typeDoors = typeDoors;
        this.colorDoors = colorDoors;
        this.boxe = boxe; 
        this.isEditingE = editE;
        this.isEditingL = editL;
    }
}