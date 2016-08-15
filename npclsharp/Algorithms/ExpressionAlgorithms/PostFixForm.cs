using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.Algorithms.ExpressionAlgorithms
{
    public struct Token
    {
        public String Value;
        public enum Type { Constant, Variable, Bracket, Operator };
        public Double priority;
    }
    
    public class PostFixForm
    {
        List<String> operators = new List<String>(){ "+","-","*","/","sqrt"};
        public static List<Token> Tokenize(String input)
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
