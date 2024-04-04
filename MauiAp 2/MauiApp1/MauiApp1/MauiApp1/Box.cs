

public class Box
{
    private String height;
    private String color;
    private String typedoors;
    private Boolean doors;



    public Box()
    {
        
    }
    public Box(String height, String color, String typedoors, Boolean doors)
    {
        this.height = height;
        this.color = color;
        this.typedoors = typedoors;
        this.doors = doors;
    }

    public String getHeight()
    {
        return height;
    }
    public void setHeight(String h)
    {
        this.height= h;
    }

    public String getColor()
    {
        return color;
    }
    
    public void setColor(String color)
    {
        this.color = color;
    }
    

    public String getTypeDoors()
    {
        return typedoors;
    }

    public void setTypeDoors(String typeDoors)
    {
        this.typedoors = typeDoors; 
    }

    public Boolean getDoors()
    {
        return doors;
    }

    public void setDoors(Boolean d)
    {
        this.doors = d; 
    }
    
    

    public String displayBox()
    {
        String box = " ";
        if (doors == true)
        {
            box += "Height of the box : "+height + ", Color of the Door: "+ color+", Type of the door: "+ typedoors;

        }
        else
        {
            box += "Height of the box : " + height + ", There is no door for this box";
        }

        return box; 
    }
}