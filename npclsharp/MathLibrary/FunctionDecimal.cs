using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.MathLibrary
{
    public class FunctionDecimal : IFunction
    {
        //FunctionDefintion[] trigonoderivs = new FunctionDefintion[] { Math.Sin, Math.Cos, MinusSin, MinusCos, };
        List<Double> DimensionExclude { get; set; }
        public delegate Decimal FunctionDefintion(Decimal Dimension);
        public Double definitionvalx, definitionvalx0;
        public FunctionDefintion Defintion { get; set; }
        public SetExpression Dimension;
        Decimal[] PolynomialMults;
        public FunctionDecimal(Decimal[] PolinomialMultipliers, FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
        {
            PolynomialMults = PolinomialMultipliers;
            Defintion = DefinitionOfFunction;
            DimensionExclude = new List<Double>();
            this.Dimension = Dimension;
        }
        public FunctionDecimal(FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
        {
            Defintion = DefinitionOfFunction;
            DimensionExclude = new List<Double>();
            this.Dimension = Dimension;
        }
        private static Double MinusSin(Double x)
        {
            return -1 * Math.Sin(x);
        }
        private static Double MinusCos(Double x)
        {
            return -1 * Math.Cos(x);
        }
        public FunctionDecimal Derivative(Int32 precision = 100000)
        {
            FunctionDefintion derivativeDef;
           
           
            {
                derivativeDef = new FunctionDefintion((D) =>
                {
                    Decimal x = D - (Decimal)(1 / (Decimal)precision);
                    return (Decimal)((Decimal)(Defintion(x) - Defintion(D)) / (Decimal)(x - D));
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
        public FunctionDecimal NthDerivative(Int32 n, Int32 precision = 100000)
        {

            if (n >= 1)
            {
                FunctionDecimal derivativeFunc;
                    derivativeFunc = this.Derivative(precision);
                    for (Int32 i = 0; i < n - 1; i++)
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
        public Decimal this[Decimal x]
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
        public Decimal DefiniteRiemannIntegral(Decimal from, Decimal to, Int64 n = 1000)
        {
            Decimal sum = 0;
            for (Decimal i = from; i < to; i += (to - from) / (Decimal)n)
            {
                sum += Defintion(i);
            }
            return sum / (Decimal)n;
            //throw new NotImplementedException();
        }
        public FunctionDecimal Antiderivative(Int32 precision = 10000)
        {
            //Function 
            FunctionDefintion def = new FunctionDefintion((D) =>
            {
                return DefiniteRiemannIntegral(1, D, precision);
            });
            return new FunctionDecimal(def, Dimension);
        }
        private Int32 Factiorial(Int32 n)
        {
            if (n <= 1) { return 1; }
            else { return n * Factiorial(n - 1); }
        }
        private Decimal power(Decimal x, Int32 k)
        {
            if (k == 0) return 1;
            Decimal temp = x;
            for (Int32 i = 0; i < k - 1; i++)
            {
                temp *= x;
            }
            return temp;
        }
        public FunctionDecimal SplineInterpol(Decimal from, Decimal to, Double step = 0.1)
        {
            //Polynomial pol = new Polynomial();
            List<Decimal> dimension = new List<Decimal>();
            for (Decimal i = from; i < to; i += (Decimal)step)
            {
                //if (Dimension.Contains(i))
                {
                    dimension.Add(i);
                }
            }

            return new FunctionDecimal((x) =>
            {
                List<Decimal> ljs = new List<Decimal>();
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
                for (Decimal i = from; i < to; i += (Decimal)step)
                {
                    Decimal temp = 1;
                    for (Decimal j = from; j < to; j += (Decimal)step)
                    {
                        if (i != j)
                            temp *= (x - j) / (i - j);
                    }
                    ljs.Add(temp);
                }
                Decimal temp2 = 0;
                Int32 position = 0;
                for (Decimal i = from; i < to; i += (Decimal)step)
                {
                    //if (Dimension.Contains(i))
                        temp2 += ljs[position] * Defintion(i);
                    position++;
                }
                return temp2;
            }, Dimension);
            //throw new NotImplementedException();
        }
        public FunctionDecimal TaylorSeries(Int32 periods)
        {
            //new FunctionDefinition("Sin(x)");
            Decimal a = 0;
            Decimal[] derivvals = new Decimal[periods];
            for (Int32 i = 0; i < periods; i++)
            {
                derivvals[i] = NthDerivative(i)[a];
            }
            return new FunctionDecimal(derivvals,(x) => 
            {
                Decimal sum = 0;
                for (Int32 i = 0; i < periods; i++)
                {
                    
                    //double derivval = NthDerivative(i)[a];
                    Int32 fact = Factiorial(i);
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
