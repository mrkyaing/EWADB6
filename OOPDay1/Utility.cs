namespace OOPDay1
{
    public static class Utility
    {
        //states
        public static string WeekDays="Monday";
        public static string AppVersion="1.0.0";
        //behaviors
        public static void  Today()=>Console.WriteLine(DateTime.Now);

        public static void PrintTime()=>Console.WriteLine(DateTime.Now);

        public static void ApplicationUser()=>Console.WriteLine("UserName: MGMG,Role: Admin");

    }
}