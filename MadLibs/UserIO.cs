namespace MadLibs {
    class UserIO {
        public static T SimpleAsk<T>(string question) {
            return Ask<T>(null, $"{question}: ");
        }

        public static T Ask<T>(string? question = null, string beckon = ">>> ") 
        {
            while (true) 
            {
                if (question is not null) 
                {
                    Console.WriteLine(question);
                }
                
                Console.Write(beckon);
                string? x = Console.ReadLine();
                if (x == null) continue;

                try
                {
                    T returnable = (T) Convert.ChangeType(x, typeof(T));
                    return returnable;
                }
                catch (Exception)
                {
                    Console.WriteLine($"The wrong type was given. {typeof(T).Name} was expected.");            
                }
            }
        }

        public static int Choose(params string[] options) 
        {
            for (int i = 0; i<options.Length; i++) 
            {
                Say($"[{i+1}] {options[i]}");
            }
            while (true) 
            {
                int chosen = Ask<int>() -1;

                if (-1 < chosen && chosen< options.Length) return chosen;
                Say("The chosen number is not within range of the options.");
            }
        }

        public static void Say(string say) 
        {
            foreach (char l in say)
            {
                Console.Write(l);
                Thread.Sleep(10);
            }
            Thread.Sleep(30);
            Console.Write("\n");
        }

        public static void Say(params string[] sayable) 
        {
            foreach (string say in sayable)
            {
                Say(say);
            }
        }

        public static void Say(ITalkable x, string message)
        {
            TalkData data = x.Say(message);

            Print($"{data.Name}: ");
            Say(message);
        }

        public static void Say(ITalkable x, params string[] message)
        {
            for (int i = 0; i<message.Length; i++) {
                string mes = message[i];

                TalkData data = x.Say(mes);
                
                if (i==0) Say($"-> {data.Name}");

                Say(mes);
            }
        }

        public static void Print(params string[] printable)
        {
            foreach (string print in printable)
            {
                Console.WriteLine(print);
            }
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public readonly struct TalkData 
        {
            public TalkData(string message, string name) 
            {
                Message = message;
                Name = name;
            }

            public string Message { get; init; }
            public string Name { get; init; }
        }

        public interface ITalkable 
        {
            TalkData Say(string message);
        }
    }

}