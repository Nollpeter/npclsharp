using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.MathLibrary
{
    public class PreciseNumber
    {
        internal List<Int32> intPart = new List<Int32>();
        internal List<Int32> floatPart = new List<Int32>();


        private static List<Int32> intToList(Int32 number)
        {
            List<Int32> temp = new List<Int32>();
            //int[] result = number.ToString().Select(o => Convert.ToInt32(o)).ToArray();
            Char[] tempstr = number.ToString().ToCharArray();
            
            foreach (Char item in tempstr)
            {
                temp.Add(Convert.ToInt16((Int32)item)-48);
            }
            return temp;
        }
        
        private static Int32 max(Int32 first, Int32 second)
        {
            if (first > second) { return first; } return second;
        }
        private static Int32 min(Int32 first, Int32 second)
        {
            if (first < second) { return first; } return second;
        }



        protected PreciseNumber()
        {

        }
        public PreciseNumber(Double number)
        {
            intPart = PreciseNumber.intToList((Int32)number);
            floatPart = PreciseNumber.intToList(Convert.ToInt16(number.ToString().Split(',')[1]));
        }
        public PreciseNumber(Int32 number)
        {
            intPart = PreciseNumber.intToList(number);
        }
        public PreciseNumber(String numberstr)
        {
            String[] parts = numberstr.Split(',');
            foreach (Char item in parts[0])
            {
                intPart.Add(Convert.ToInt32(item) - 48);
            }
            if(parts.Length>1)
            {
                foreach (Char item in parts[1])
                {
                    floatPart.Add(Convert.ToInt32(item) - 48);
                }
            }
        }
        public static Boolean operator < (PreciseNumber first, PreciseNumber second)
        {
            throw new NotImplementedException();
        }
        public static Boolean operator >(PreciseNumber first, PreciseNumber second)
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
                for (Int32 i = 0; i < second.floatPart.Count-first.floatPart.Count; i++)
                {
                    result.floatPart.Add(second.floatPart[i]);
                }
                Int32 leftover = 0;
                Int32 ind2 = second.floatPart.Count - first.floatPart.Count;
                Int32 ind = first.floatPart.Count;
                for (Int32 i = 0; i < ind; i++)
                {
                    result.floatPart.Add((second.floatPart[i] + first.floatPart[ind2+i] + leftover) % 10);
                    leftover = (second.floatPart[i] + first.floatPart[i]) / 10;
                }
                //egészrész összeadása
                for (Int32 i = 0; i < min(first.intPart.Count, second.intPart.Count); i++)
                {
                    result.intPart.Add((second.intPart[i] + first.intPart[i] + leftover) % 10);
                    leftover = (second.intPart[i] + first.intPart[i]) / 10;
                }

                for (Int32 i = min(first.intPart.Count, second.intPart.Count); i < max(first.intPart.Count, second.intPart.Count) ; i++)
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
                for (Int32 i = 0; i < first.floatPart.Count - second.floatPart.Count; i++)
                {
                    result.floatPart.Add(first.floatPart[i]);
                }
                Int32 leftover = 0;
                Int32 ind2 = first.floatPart.Count - second.floatPart.Count;
                Int32 ind = second.floatPart.Count;
                for (Int32 i = 0; i < ind; i++)
                {
                    result.floatPart.Add((second.floatPart[i] + first.floatPart[ind2+i] + leftover) % 10);
                    leftover = (second.floatPart[i] + first.floatPart[i]) / 10;
                }
                //egész összeadása

                for (Int32 i = 0; i < min(first.intPart.Count, second.intPart.Count); i++)
                {
                    result.intPart.Add((second.intPart[i] + first.intPart[i] + leftover) % 10);
                    leftover = (second.intPart[i] + first.intPart[i]) / 10;
                }

                for (Int32 i = min(first.intPart.Count, second.intPart.Count); i < max(first.intPart.Count, second.intPart.Count); i++)
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
        public Int32 ToInt()
        {
            List<Int32> intPartTemp = intPart;
            intPartTemp.Reverse();
            Int32 temp = 0;
            Int32 multiplier = 1;
            Int32 i = 0;
            while (multiplier <=10e9 && i < intPart.Count)
            {
                temp += intPart[i] * multiplier;
                ++i; multiplier *= 10;
            }
            return temp;
            
        }
        public Double ToDouble()
        {

            Double temp = 0;
            Double multiplier = 1;
            Int32 j = 0;
            while (j < 318 && j < intPart.Count)
            {
                temp += intPart[j] * multiplier;
                ++j; multiplier *= 10;
            }
            //j = 0;
            multiplier = 0.1;
            for (Int32 i = 0; i < 15; i++)
            {
                if(i<floatPart.Count)
                {
                    temp += floatPart[i] * multiplier;
                    multiplier *= 0.1;
                }
                
            }
            return temp;

        }
        override public String ToString()
        {
            String ip = String.Concat(intPart);
            String fp = String.Concat(floatPart);
            return ip + "," + fp;

        }
        
    }
}
