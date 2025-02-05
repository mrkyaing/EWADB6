namespace OOPDay1
{
    public class Car
    {
        //states
        public string Model;
        public string Color;
        public string LicenceNo;
        //Has-A relationship >> Composition assication type 
        public Engine engine;
        
        public Car(Engine engine){
            this.engine=engine;
        }
        
        public void PushLever(){
            engine.ChangePinion();
            Console.WriteLine("Move forward.");
        }   
        public Car(){

        }
        //Method Overloading
        public void PlayHorn(int count){
            for(int i=0; i<count; i++){
                Console.WriteLine("T");
            }
        }
        //Method Overloading >>one of polymprphism
        public void PlayHorn()=>Console.WriteLine("T");

        override public string ToString(){
            return $"Model: {Model}, Color: {Color}, Gearno: {this.engine.GearNo}, LicenceNo: {LicenceNo} ";
        }
    public void Dashboard(){
        Console.WriteLine("Dashboard Status!");
        Console.WriteLine($"Oil Level:{this.engine.OilLevel}");
    }
        public static void UseBreak()=>Console.WriteLine("Break Used");
    }
}