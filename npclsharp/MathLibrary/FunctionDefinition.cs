using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PortableClassLibrary_NP.MathLibrary
{
    public class FunctionDefinition
    {
        Char[] operands = new Char[]{'+','-','*','/' };
        const String pattern = @"(\-)|(\+)|(\*)|(\/)|(\^)";
        const String functionpattern = @"sin|cos|tan|acos|asin|atan|sinh|cosh|tanh|abs";
        public delegate Double FunctionDefintion(Double Dimension);
        FunctionDefintion def = null;
        public FunctionDefinition(String expr)
        {
            String temp = expr.ToLower();
            String s = "sin(a)+b+2-2/3*5";
            //string pattern = @"(\-)|(\+)";
            Regex regex = new Regex(pattern);
            
            String[] s2 = regex.Split(s);//s.Tokenize(new char[] {'+','-'});
            String expression = temp.IgnoreChar(new Char[] { '(', ')' });
            def = new FunctionDefintion((a) => { return a + 2; });
            def += new FunctionDefintion((a) => { return a *5; });
            Double x = (def(4));
            
        }
        
    }
    
    public static class Extensions
    {
        
        public static String[] Tokenize(this String input, IEnumerable<Char> characters)
        {
            List<String> returnList = new List<String>();
            if( characters.Count() > 0)
            {
                String[] temp = input.Split(characters.ElementAt(0));
                foreach (String item in temp)
                {
                    returnList.Add(item);
                    returnList.Add(characters.ElementAt(0).ToString());
                }
            }
            if( characters.Count() > 1)
            {
                for (Int32 i = 1; i < characters.Count(); i++)
                {
                    List<String> tempList = new List<String>();
                    foreach (String item in returnList)
                    {
                        String[] temp = item.Split(characters.ElementAt(i));
                        foreach (String item2 in temp)
                        {
                            tempList.Add(item2);
                            //tempList.Add(characters.ElementAt(i).ToString());
                        }
                    }
                    returnList = tempList;
                }
            }
            

            return returnList.ToArray();
        }
        public static String[] Tokenize(this IEnumerable<String> input, Char character)
        {
            List<String> a, b;
            
            return new String[] { };
        }
        public static String IgnoreChar(this String input, Char character)
        {
            List<Char> goodvalues = new List<Char>();
            for (Int32 i = 0; i < input.Length; i++)
            {
                if (character != input[i])
                {
                    goodvalues.Add(input[i]);
                    
                }
            }
            return String.Concat(goodvalues);
        }
        public static String IgnoreChar(this String input, IEnumerable<Char> character)
        {
            List<Char> goodvalues = new List<Char>();
            for (Int32 i = 0; i < input.Length; i++)
            {
                if (!character.Any((a) => { return a == input[i]; }))
                {
                    goodvalues.Add(input[i]);
                }
            }
            return String.Concat(goodvalues);
        }

    }
}
