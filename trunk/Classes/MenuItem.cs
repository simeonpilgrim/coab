using System;
using System.Diagnostics;

namespace Classes
{
	public class MenuItem
	{
		public string Text { get; set;}  // 0x00;
		public bool Heading { get; protected set;} // 0x29;
        public Item Item { get; protected set;}

        public MenuItem()
		{
            Heading = false;
			Text = string.Empty;
            Item = null;
		}

        public MenuItem(string text)
        {
            Heading = false;
            Text = text;
            Item = null;
        }

        public MenuItem(string text, bool heading)
        {
            Heading = heading;
            Text = text;
            Item = null;
        }

        public MenuItem(string text, Item item)
        {
            Heading = false;
            Text = text;
            Item = item;
        }
	}
}
