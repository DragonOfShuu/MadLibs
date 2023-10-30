namespace MadLibs.Stories {
    class ReadStory {
        public static void RunStory(Story story) 
        {
            List<StoryWord> storyWords = MakeWords(story);
            storyWords = RandomizeWords(storyWords);

            storyWords = FillWords(storyWords);
            storyWords = storyWords.OrderBy(x => x.Index).ToList();

            UserIO.Clear();

            string?[] words = storyWords.Select(s=>s.Word).ToArray();
            string storyText = string.Format(story.Content, words);

            SpeakStory(story, storyText);
        }

        private static List<StoryWord> MakeWords(Story x) 
        {
            List<StoryWord> storyWords = new();
            for (int i = 0; i<x.Words.Length; i++) 
            {
                string word = x.Words[i];
                storyWords.Add(new(word, i));
            }
            return storyWords;
        }

        private static List<StoryWord> RandomizeWords(List<StoryWord> x) 
        {
            List<StoryWord> oldWords = new(x);
            List<StoryWord> newWords = new();
            Random r = new();
            while (oldWords.Count > 0) 
            {
                int num = r.Next(oldWords.Count);
                newWords.Add(oldWords[num]);
                oldWords.RemoveAt(num);
            }
            return newWords;
        }

        private static List<StoryWord> FillWords(List<StoryWord> x) 
        {
            List<StoryWord> oldWords = new(x);
            List<StoryWord> newWords = new();
            for (int i = 0; i<oldWords.Count; i++)
            {
                StoryWord wordType = oldWords[i];
                string word = UserIO.SimpleAsk<string>($"Give a {wordType.WordType}");
                wordType.Word = word;
                newWords.Add(wordType);
            }
            return newWords;
        }

        private static void SpeakStory(Story x, string formattedContent) 
        {
            UserIO.Say(
                x.Name,
                formattedContent
            );
        }

        private struct StoryWord 
        {
            public string WordType { get; init; }
            public int Index { get; init; }
            public string? Word { get; set; }
            public StoryWord(
                string wordType, 
                int index, 
                string? word = null) 
            {
                WordType = wordType;
                Index = index;
                Word = word;
            }
        }
    }
}
