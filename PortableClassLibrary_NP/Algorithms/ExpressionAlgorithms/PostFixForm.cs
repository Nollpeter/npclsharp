using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.Algorithms.ExpressionAlgorithms
{
    public struct Token
    {
        public string Value;
        public enum Type { Constant, Variable, Bracket, Operator };
        public double priority;
    }
    
    public class PostFixForm
    {
        List<string> operators = new List<string>(){ "+","-","*","/","sqrt"};
        public static List<Token> Tokenize(string input)
        {
            List<Token> list = new List<Token>();

            return list;
        }
        public static List<Token> PostFix(IList<Token> input)
        {
            Stack<Token> temp = new Stack<Token>();
            throw new NotImplementedException();
        }
    }
}
