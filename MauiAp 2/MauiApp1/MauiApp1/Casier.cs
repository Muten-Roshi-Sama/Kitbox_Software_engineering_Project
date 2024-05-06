public class Casier
{
    public String depth{get;set;}
    public String length{get;set;}
    public String color{get;set;}

    public String nbBox{get;set;}
    public List<Box> _boxes{get;set;}

    public String angleIronColor{get;set;}



    public Casier(String depth, String length, String color,String nbBox, List<Box> _boxes, String angleIronColor)
    {
        this.depth = depth;
        this.length = length;
        this.color = color;
        this.nbBox = nbBox; 
        this._boxes = _boxes;
        this.angleIronColor = angleIronColor; 
    }


    public String DisplayCasier()
    {
        String casier = "The width of the box : "+depth + ", The length of the box : "+length+", Color of the Box: "+ color+", number of boxes: "+ nbBox;

        return casier; 
    }


    public void addBoxes(Box box)
    {
        _boxes.Add(box);
    }



}
