namespace Tsing.Helper
{
    class StringHelper
    {
        public static string getStringBetween(string Source, string Ahead, string Behind)
        {
            int startAt = Source.IndexOf(Ahead) + Ahead.Length;
            int endBefore = Source.IndexOf(Behind, startAt);
            if (Source.Contains(Ahead) && Source.Contains(Behind))
                return Source.Substring(startAt, endBefore - startAt);
            return null;
        }

        public static string removeString(string Source, string toRemove)
        {
            while (Source.Contains(toRemove))
                Source = Source.Remove(Source.IndexOf(toRemove), toRemove.Length);
            return Source;
        }
        public static string removeStringBetween(string Source, string Start, string End)
        {
            while (Source.Contains(Start))
            {
                int startAt = Source.IndexOf(Start);
                int endBefore = Source.IndexOf(End, startAt) + End.Length;
                if (Source.Contains(Start) && Source.Contains(End))
                    Source = Source.Remove(startAt, endBefore - startAt);
            }
            return Source;
        }
    }
}
