using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.MathLibrary
{
    public class SetExpression
    {
        bool neginfstart;
        bool infend;
        bool openStart;
        bool openEnd;
        double from;
        double to;
        double[] exclude;
        public SetExpression(string from, string to, double[] exlcude)
        {
            if (from == "neginf")
                neginfstart = true;
            else
            {
                this.from = Convert.ToDouble(from); neginfstart = false;
            }
                
            if (to == "inf")
                infend = true;
            else { this.to = Convert.ToDouble(to); infend = false; }

            this.exclude = exlcude;
           
        }
        public SetExpression(string interval, double[] exclude)
        {
            char first = interval[0];
            char last = interval[interval.Length - 1];
            openStart = first == '(';
            openEnd = last == ')';
            interval = interval.Remove(0, 1);
            interval = interval.Remove(interval.Length - 1, 1);
            string[] exp = interval.Split(',');

            if (exp[0] == "neginf")
                neginfstart = true;
            else
            {
                this.from = Convert.ToDouble(exp[0]); neginfstart = false;
            }

            if (exp[1] == "inf")
                infend = true;
            else { this.to = Convert.ToDouble(exp[1]); infend = false; }

            this.exclude = exclude;
            
        }
        public bool Contains(double value)
        {
            if(exclude.Contains(value))
            {
                return false;
            }
            if(neginfstart && infend)
            {
                return true;
            }
            if( neginfstart && !infend)
            {
                if(openEnd)
                {
                    return value < to;
                }
                else
                {
                    return value <= to;
                }
            }
            if( !neginfstart && infend)
            {
                if(openStart)
                {
                    return value > from;
                }
                else
                {
                    return value >= from;
                }
            }
            else
            {
                if(openStart && openEnd)
                {
                    return value > from && value < to;
                }
                if(!openStart && openEnd)
                {
                    return value >= from && value < to;
                }
                if (openStart &&!openEnd)
                {
                    return value > from && value <= to;
                }
                else
                {
                    return value >= from && value <= to;
                }
            }
        }

    }
}
