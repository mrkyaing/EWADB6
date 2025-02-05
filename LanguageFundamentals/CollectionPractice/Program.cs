using System.Collections;

namespace CollectionPractice;

class Program
{
    static void Main(string[] args)
    {
        List<string> Friends = new List<string>(){
            "MG MG",
            "AUNG AUNG",
            "TUN TUN",
            "SU SU",
            "MG MG",
        };
        Friends.Add("MAY MAY");

        foreach (var friend in Friends)
        {
            Console.WriteLine(friend);
        }

        Stack<string> stack = new Stack<string>();//Generic Collection Type
        stack.Push("Zin Min");
        stack.Push("KO KO");
        stack.Push("Zaw Zaw");
        Console.WriteLine(stack.Pop());//KO KO
        Console.WriteLine(stack.Pop());//Zin Min
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        Console.WriteLine(queue.Dequeue());//1
        Console.WriteLine(queue.Dequeue());//2

        ArrayList myArrayList = new ArrayList();//Non-Generic Collection Type
        myArrayList.Add(1);
        myArrayList.Add("Hello");
        myArrayList.Add(3.5);
        myArrayList.Add(true);
        foreach (var item in myArrayList)
        {
            Console.WriteLine(item);
        }
        Stack order = new Stack();
        order.Push("A");
        order.Push(1);
        Console.WriteLine("Output from Dictionary");
        try
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();//declare dictionary
            //initialize the values dictionary
            dic.Add("notepad", "Notepad app");
            dic.Add("word", "Word app");
            dic.Add("excel", "Excel app");

            if (!dic.ContainsKey("word"))
            {
                dic.Add("word", "word 2");
            }

            foreach (var item in dic)
            {
                Console.WriteLine(item.Key + "=>" + item.Value);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Key is already exist");
        }

        Console.WriteLine("Output from Hashtable");
        Hashtable MyNotes = new Hashtable();
        MyNotes.Add("SUNDAY", "Rest Day");
        MyNotes.Add("MONDAY", "Working as a new job");
        foreach (DictionaryEntry item in MyNotes)
        {
            Console.WriteLine(item.Key + "=>" + item.Value);
        }
    }
}
