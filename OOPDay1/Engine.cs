
namespace OOPDay1
{
    public class Engine:IEngineOperation // Specialization association
    {
     private int oilLevel;
     public int OilLevel
     {
        get { return oilLevel; }
        set { oilLevel = value; }
     }
     
    private string gearno;
    public string GearNo
    {
        get { return gearno; }
        set { gearno = value; }
    }
    override public string ToString()
    {
        return $"Oil Level: {oilLevel}, Gear No: {gearno}";
    }
        //abstraction
        public void ChangePinion()
        {
            Console.WriteLine("Change Pinion");
        }

        public void StartEngine()
        {
           Console.WriteLine("Engine Started");
        }
        public void StopEngine()=>Console.WriteLine("Engine Stopped");
    }
}