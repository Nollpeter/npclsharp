using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.MathLibrary
{
    public class PreciseNumber
    {
        internal List<int> intPart = new List<int>();
        internal List<int> floatPart = new List<int>();


        private static List<int> intToList(int number)
        {
            List<int> temp = new List<int>();
            //int[] result = number.ToString().Select(o => Convert.ToInt32(o)).ToArray();
            char[] tempstr = number.ToString().ToCharArray();
            
            foreach (char item in tempstr)
            {
                temp.Add(Convert.ToInt16((int)item)-48);
            }
            return temp;
        }
        
        private static int max(int first, int second)
        {
            if (first > second) { return first; } return second;
        }
        private static int min(int first, int second)
        {
            if (first < second) { return first; } return second;
        }



        protected PreciseNumber()
        {

        }
        public PreciseNumber(double number)
        {
            intPart = PreciseNumber.intToList((int)number);
            floatPart = PreciseNumber.intToList(Convert.ToInt16(number.ToString().Split(',')[1]));
        }
        public PreciseNumber(int number)
        {
            intPart = PreciseNumber.intToList(number);
        }
        public PreciseNumber(string numberstr)
        {
            string[] parts = numberstr.Split(',');
            foreach (char item in parts[0])
            {
                intPart.Add(Convert.ToInt32(item) - 48);
            }
            if(parts.Length>1)
            {
                foreach (char item in parts[1])
                {
                    floatPart.Add(Convert.ToInt32(item) - 48);
                }
            }
        }
        public static bool operator < (PreciseNumber first, PreciseNumber second)
        {
            throw new NotImplementedException();
        }
        public static bool operator >(PreciseNumber first, PreciseNumber second)
        {
            throw new NotImplementedException();
        }
        public static PreciseNumber operator + (PreciseNumber first, PreciseNumber second)
        {
            PreciseNumber result = new PreciseNumber();
            first.intPart.Reverse();
            first.floatPart.Reverse();
            second.intPart.Reverse();
            second.floatPart.Reverse();
            if(first.floatPart.Count < second.floatPart.Count)
            {
                
                //tizedes rész összeadása
                for (int i = 0; i < second.floatPart.Count-first.floatPart.Count; i++)
                {
                    result.floatPart.Add(second.floatPart[i]);
                }
                int leftover = 0;
                int ind2 = second.floatPart.Count - first.floatPart.Count;
                int ind = first.floatPart.Count;
                for (int i = 0; i < ind; i++)
                {
                    result.floatPart.Add((second.floatPart[i] + first.floatPart[ind2+i] + leftover) % 10);
                    leftover = (second.floatPart[i] + first.floatPart[i]) / 10;
                }
                //egészrész összeadása
                for (int i = 0; i < min(first.intPart.Count, second.intPart.Count); i++)
                {
                    result.intPart.Add((second.intPart[i] + first.intPart[i] + leftover) % 10);
                    leftover = (second.intPart[i] + first.intPart[i]) / 10;
                }

                for (int i = min(first.intPart.Count, second.intPart.Count); i < max(first.intPart.Count, second.intPart.Count) ; i++)
                {
                    if (first.intPart.Count < second.intPart.Count)
                        result.intPart.Add(second.intPart[i]);
                    else
                        result.intPart.Add(first.intPart[i]);
                }
                /*if(leftover != 0)
                {
                    result.intPart.Add(leftover);
                }*/
            }

            else
            {
                //tizedes rész összeadása
                for (int i = 0; i < first.floatPart.Count - second.floatPart.Count; i++)
                {
                    result.floatPart.Add(first.floatPart[i]);
                }
                int leftover = 0;
                int ind2 = first.floatPart.Count - second.floatPart.Count;
                int ind = second.floatPart.Count;
                for (int i = 0; i < ind; i++)
                {
                    result.floatPart.Add((second.floatPart[i] + first.floatPart[ind2+i] + leftover) % 10);
                    leftover = (second.floatPart[i] + first.floatPart[i]) / 10;
                }
                //egész összeadása

                for (int i = 0; i < min(first.intPart.Count, second.intPart.Count); i++)
                {
                    result.intPart.Add((second.intPart[i] + first.intPart[i] + leftover) % 10);
                    leftover = (second.intPart[i] + first.intPart[i]) / 10;
                }

                for (int i = min(first.intPart.Count, second.intPart.Count); i < max(first.intPart.Count, second.intPart.Count); i++)
                {
                    if (first.intPart.Count < second.intPart.Count)
                        result.intPart.Add(second.intPart[i]);
                    else
                        result.intPart.Add(first.intPart[i]);
                }
               
            }
            result.intPart.Reverse();
            result.floatPart.Reverse();
            return result;
        }
        public static PreciseNumber operator -(PreciseNumber first, PreciseNumber second)
        {
            throw new NotImplementedException();
        }
        public static PreciseNumber operator *(PreciseNumber first, PreciseNumber second)
        {
            throw new NotImplementedException();
        }
        public static PreciseNumber operator /(PreciseNumber first, PreciseNumber second)
        {
            throw new NotImplementedException();
        }
        public int ToInt()
        {
            List<int> intPartTemp = intPart;
            intPartTemp.Reverse();
            int temp = 0;
            int multiplier = 1;
            int i = 0;
            while (multiplier <=10e9 && i < intPart.Count)
            {
                temp += intPart[i] * multiplier;
                ++i; multiplier *= 10;
            }
            return temp;
            
        }
        public double ToDouble()
        {

            double temp = 0;
            double multiplier = 1;
            int j = 0;
            while (j < 318 && j < intPart.Count)
            {
                temp += intPart[j] * multiplier;
                ++j; multiplier *= 10;
            }
            //j = 0;
            multiplier = 0.1;
            for (int i = 0; i < 15; i++)
            {
                if(i<floatPart.Count)
                {
                    temp += floatPart[i] * multiplier;
                    multiplier *= 0.1;
                }
                
            }
            return temp;

        }
        override public string ToString()
        {
            string ip = string.Concat(intPart);
            string fp = string.Concat(floatPart);
            return ip + "," + fp;

        }
        
    }
}
