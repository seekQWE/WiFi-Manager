using System;

namespace Tsing.Model
{
    class Host
    {
        public string name { get; set; }
        public Uri uri { get; set; }

        public Host()
        {
        }
        public Host(string Name)
        {
            name = Name;
        }
        public Host(Uri Uri)
        {
            uri = Uri;
        }
        public Host(string Name, Uri Uri)
        {
            name = Name;
            uri = Uri;
        }
    }
}
