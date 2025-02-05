public class Car{
    public string Model{get;set;}
    public string Color{get;set;}
    public int GearNo{get;set;}
     public Car(string model,string color,int gearno){
        
        if(string.IsNullOrEmpty(model)){
            throw new Exception("Model is required");
        }
        if(string.IsNullOrEmpty(color)){
            throw new Exception("Color is required");
        }
        if(gearno<=0){
            throw new Exception("Gear No is required");
        }

        Model=model;
        Color=color;
        GearNo=gearno;
     }
    public void StartEngine(){
        Console.WriteLine("Engine is started with Gear No"+GearNo);
    }
    override public string ToString(){
        return $"Car Information:[{Model}:{Color}:{GearNo}]";
    }
}