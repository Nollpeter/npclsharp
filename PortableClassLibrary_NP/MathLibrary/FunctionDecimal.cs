using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.MathLibrary
{
    public class FunctionDecimal : IFunction
    {
        //FunctionDefintion[] trigonoderivs = new FunctionDefintion[] { Math.Sin, Math.Cos, MinusSin, MinusCos, };
        List<double> DimensionExclude { get; set; }
        public delegate decimal FunctionDefintion(decimal Dimension);
        public double definitionvalx, definitionvalx0;
        public FunctionDefintion Defintion { get; set; }
        public SetExpression Dimension;
        decimal[] PolynomialMults;
        public FunctionDecimal(decimal[] PolinomialMultipliers, FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
        {
            PolynomialMults = PolinomialMultipliers;
            Defintion = DefinitionOfFunction;
            DimensionExclude = new List<double>();
            this.Dimension = Dimension;
        }
        public FunctionDecimal(FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
        {
            Defintion = DefinitionOfFunction;
            DimensionExclude = new List<double>();
            this.Dimension = Dimension;
        }
        private static double MinusSin(double x)
        {
            return -1 * Math.Sin(x);
        }
        private static double MinusCos(double x)
        {
            return -1 * Math.Cos(x);
        }
        public FunctionDecimal Derivative(int precision = 100000)
        {
            FunctionDefintion derivativeDef;
           
           
            {
                derivativeDef = new FunctionDefintion((D) =>
                {
                    decimal x = D - (decimal)(1 / (decimal)precision);
                    return (decimal)((decimal)(Defintion(x) - Defintion(D)) / (decimal)(x - D));
                    //double h = 1 / (double)precision;
                    //return (Defintion(D+h)-Defintion(D-h))/(double)(2*h);
                    ///(double)precision;
                    /*if (Math.Abs(Defintion(D) - Defintion(x)) < 10e-4)
                        return 0;
                    return ((double)(Defintion(x) - Defintion(D)) / (double)(x - D));*/
                    /*double h = 1/(double)precision;
                    return ((double)Defintion(D + h) - Defintion(D)) / h;*/

                });
            }

            return new FunctionDecimal(derivativeDef, Dimension);
        }
        /*public Function Derivative(string definition)
        {

            if (definition == "sin(x)")
                return new Function(new FunctionDefintion((a) => { return Math.Cos(a); }), Dimension);
        }*/
        public FunctionDecimal NthDerivative(int n, int precision = 100000)
        {

            if (n >= 1)
            {
                FunctionDecimal derivativeFunc;
                    derivativeFunc = this.Derivative(precision);
                    for (int i = 0; i < n - 1; i++)
                    {
                        derivativeFunc = derivativeFunc.Derivative(precision);

                    }
                return derivativeFunc;
            }
            else
            {
                return this;
            }
        }
        public decimal this[decimal x]
        {
            get
            {
                //if (Dimension.Contains(x))
                // This indexer is very simple, and just returns or sets 
                // the corresponding element from the internal array. 
                return (Defintion(x));
                //throw new Exception("Out of Dimension");
            }

        }
        public decimal DefiniteRiemannIntegral(decimal from, decimal to, long n = 1000)
        {
            decimal sum = 0;
            for (decimal i = from; i < to; i += (to - from) / (decimal)n)
            {
                sum += Defintion(i);
            }
            return sum / (decimal)n;
            //throw new NotImplementedException();
        }
        public FunctionDecimal Antiderivative(int precision = 10000)
        {
            //Function 
            FunctionDefintion def = new FunctionDefintion((D) =>
            {
                return DefiniteRiemannIntegral(1, D, precision);
            });
            return new FunctionDecimal(def, Dimension);
        }
        private int Factiorial(int n)
        {
            if (n <= 1) { return 1; }
            else { return n * Factiorial(n - 1); }
        }
        private decimal power(decimal x, int k)
        {
            if (k == 0) return 1;
            decimal temp = x;
            for (int i = 0; i < k - 1; i++)
            {
                temp *= x;
            }
            return temp;
        }
        public FunctionDecimal SplineInterpol(decimal from, decimal to, double step = 0.1)
        {
            //Polynomial pol = new Polynomial();
            List<decimal> dimension = new List<decimal>();
            for (decimal i = from; i < to; i += (decimal)step)
            {
                //if (Dimension.Contains(i))
                {
                    dimension.Add(i);
                }
            }

            return new FunctionDecimal((x) =>
            {
                List<decimal> ljs = new List<decimal>();
                /*for (int i = 0; i < dimension.Count; i++)
                {
                    decimal temp = 1;
                    for (int j = 0; j < dimension.Count; j += 1)
                    {
                        if (dimension[i] != dimension[j])
                            temp *= (x - dimension[j]) / (dimension[i] - dimension[j]);
                    }
                    ljs.Add(temp);
                }*/
                for (decimal i = from; i < to; i += (decimal)step)
                {
                    decimal temp = 1;
                    for (decimal j = from; j < to; j += (decimal)step)
                    {
                        if (i != j)
                            temp *= (x - j) / (i - j);
                    }
                    ljs.Add(temp);
                }
                decimal temp2 = 0;
                int position = 0;
                for (decimal i = from; i < to; i += (decimal)step)
                {
                    //if (Dimension.Contains(i))
                        temp2 += ljs[position] * Defintion(i);
                    position++;
                }
                return temp2;
            }, Dimension);
            //throw new NotImplementedException();
        }
        public FunctionDecimal TaylorSeries(int periods)
        {
            //new FunctionDefinition("Sin(x)");
            decimal a = 0;
            decimal[] derivvals = new decimal[periods];
            for (int i = 0; i < periods; i++)
            {
                derivvals[i] = NthDerivative(i)[a];
            }
            return new FunctionDecimal(derivvals,(x) => 
            {
                decimal sum = 0;
                for (int i = 0; i < periods; i++)
                {
                    
                    //double derivval = NthDerivative(i)[a];
                    int fact = Factiorial(i);
                    sum += ((derivvals[i] / fact) * power((x-a),i));
                }
                
                return sum;
            
            }, Dimension);
            /*decimal a = 0;

            return new FunctionDecimal((x) =>
            {
                decimal sum = 0;
                for (int i = 0; i < periods; i++)
                {
                    decimal pow = power(x, i);
                    decimal derivval = NthDerivative(i)[a];
                    int fact = Factiorial(i);
                    sum += ((this.NthDerivative(i)[x] / (decimal)fact) * power((x), i));
                }

                return sum;

            }, Dimension);*/
            //throw new NotImplementedException();
        }
    }
}
