using MadLibs.Stories;

namespace MadLibs
{
    class MadLibs 
    {
        public static int Main(string[] args) 
        {
            UserIO.Clear();
            UserIO.Say("Welcome to madlibs! I am your host, Dragon of Shuu!!!");

            Story[] stories = Story.GetAllStories();
            while (true) 
            {
                string[] storyNames = stories.Select(s => s.Name).ToArray();
                
                int storyDec = UserIO.Choose(
                    "Choose story",
                    "Let the computer decide!",
                    "Leave :("
                );

                Story? story = storyDec switch
                {
                    0=> UserChooseStory(stories),
                    1=> RandomChooseStory(stories),
                    _=> null
                };

                if (story is null) return 0;

                UserIO.Clear();
                ReadStory.RunStory(story);
            }
        }

        public static Story UserChooseStory(Story[] stories) {
            int x = UserIO.Choose(stories.Select(s=>s.Name).ToArray());
            return stories[x];
        }

        public static Story RandomChooseStory(Story[] stories) {
            Random r = new();
            return stories[r.Next(stories.Length)];
        }
    }
}