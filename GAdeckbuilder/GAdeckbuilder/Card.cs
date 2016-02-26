using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAdeckbuilder
{
    public class Card
    {
        public Card (string name, bool white, bool blue, bool black, bool red, bool green, int cmc, int powerLevel)
        {
            this.Name = name;
            this.White = white;
            Blue = blue;
            Black = black;
            Red = red;
            Green = green;
            ConvertedManaCost = cmc;
            PowerLevel = powerLevel;
        }
        public enum Tag { creature, removal, draw, other };
        public string Name
        {
            get; protected set;
        }

        public int ConvertedManaCost
        {
            get; protected set;
        }

        public bool White
        {
            get; protected set;
        }
        public bool Blue
        {
            get; protected set;
        }
        public bool Black
        {
            get; protected set;
        }
        public bool Red
        {
            get; protected set;
        }
        public bool Green
        {
            get; protected set;
        }

        public string Type
        {
            get; protected set;
        }
        public Tag CardTag
        {
            get; protected set; 
        }
        public int PowerLevel
        {
            get; protected set;
        }
        public string toString()
        {
            return Name + " White: " + White + " Blue: " + Blue + " black: " + Black + " Red: " + Red + " Green: " + Green + " " + PowerLevel + " " + ConvertedManaCost;
        }
    }
}
