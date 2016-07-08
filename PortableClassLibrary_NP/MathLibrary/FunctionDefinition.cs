using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PortableClassLibrary_NP.MathLibrary
{
    public class FunctionDefinition
    {
        char[] operands = new char[]{'+','-','*','/' };
        const string pattern = @"(\-)|(\+)|(\*)|(\/)|(\^)";
        const string functionpattern = @"sin|cos|tan|acos|asin|atan|sinh|cosh|tanh|abs";
        public delegate double FunctionDefintion(double Dimension);
        FunctionDefintion def = null;
        public FunctionDefinition(string expr)
        {
            string temp = expr.ToLower();
            string s = "sin(a)+b+2-2/3*5";
            //string pattern = @"(\-)|(\+)";
            Regex regex = new Regex(pattern);
            
            string[] s2 = regex.Split(s);//s.Tokenize(new char[] {'+','-'});
            string expression = temp.IgnoreChar(new char[] { '(', ')' });
            def = new FunctionDefintion((a) => { return a + 2; });
            def += new FunctionDefintion((a) => { return a *5; });
            double x = (def(4));
            
        }
        
    }
    
    public static class Extensions
    {
        
        public static string[] Tokenize(this string input, IEnumerable<char> characters)
        {
            List<string> returnList = new List<string>();
            if( characters.Count() > 0)
            {
                string[] temp = input.Split(characters.ElementAt(0));
                foreach (string item in temp)
                {
                    returnList.Add(item);
                    returnList.Add(characters.ElementAt(0).ToString());
                }
            }
            if( characters.Count() > 1)
            {
                for (int i = 1; i < characters.Count(); i++)
                {
                    List<string> tempList = new List<string>();
                    foreach (string item in returnList)
                    {
                        string[] temp = item.Split(characters.ElementAt(i));
                        foreach (string item2 in temp)
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
        public static string[] Tokenize(this IEnumerable<string> input, char character)
        {
            List<string> a, b;
            
            return new string[] { };
        }
        public static string IgnoreChar(this string input, char character)
        {
            List<char> goodvalues = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (character != input[i])
                {
                    goodvalues.Add(input[i]);
                    
                }
            }
            return string.Concat(goodvalues);
        }
        public static string IgnoreChar(this string input, IEnumerable<char> character)
        {
            List<char> goodvalues = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                if (!character.Any((a) => { return a == input[i]; }))
                {
                    goodvalues.Add(input[i]);
                }
            }
            return string.Concat(goodvalues);
        }

    }
}
