namespace MadLibs.Stories
{
    class Story {
        const string STORY_DIR = "stories";

        private readonly string name;
        public string Name {
            get { return name; }
        }

        private readonly string content;
        public string Content {
            get { return content; }
        }

        private readonly string[] words;
        public string[] Words {
            get { return words; }
        }

        public Story(string name, string content, string[] words) {
            this.name = name;
            this.content = content;
            this.words = words;
        }

        public static Story[] GetAllStories() {
            string[] storyPaths = Directory.GetFiles(STORY_DIR);

            List<Story> stories = new();
            foreach (string story in storyPaths) {
                stories.Add(ParseStory(story));
            }
            return stories.ToArray();
        }

        private static Story ParseStory(string path) {
            List<string> allLines = new();
            using (StreamReader sr = File.OpenText(path)) {
                string? s;
                while ((s = sr.ReadLine()) != null) {
                    allLines.Add(s);
                }
            }
            if (allLines.Count < 4) throw new FormatException();

            string name = allLines[0];
            string content = allLines[1];
            string[] words = allLines.Skip(3).ToArray();

            return new(name, content, words);
        }
    }
}