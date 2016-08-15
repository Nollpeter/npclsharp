using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.MathLibrary
{
    public interface IFunction
    {

    }

    public class Function<T>
    {
        public Func<T,T> function { get; set; }

        public Function()
        {
        }
    }
    public class Function : IFunction
    {
        
        FunctionDefintion[] trigonoderivs = new FunctionDefintion[] { Math.Sin, Math.Cos, MinusSin, MinusCos, };
        List<Double> DimensionExclude { get; set; }
        public delegate Double FunctionDefintion(Double Dimension);
        public Double definitionvalx, definitionvalx0;
        public FunctionDefintion Defintion { get; set; }
        public SetExpression Dimension;
        Double[] PolynomialMults;
        public Function(Double[] PolinomialMultipliers,FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
        {
            PolynomialMults = PolinomialMultipliers;
            Defintion = DefinitionOfFunction;
            DimensionExclude = new List<Double>();
            this.Dimension = Dimension;
        }
        public Function(FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
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
        public Function Derivative(Int32 precision = 100000)
        {
            FunctionDefintion derivativeDef;
            if (Defintion == Math.Sin)
                derivativeDef = Math.Cos;//((x) => { return Math.Cos(x); });
            else if (Defintion == Math.Cos)
                derivativeDef = MinusSin;//((x) => { return MinusSin(x); });
            else if (Defintion == MinusSin)
                derivativeDef = MinusCos;//((x) => { return MinusCos(x); });
            else if (Defintion == MinusCos)
                derivativeDef = Math.Sin;//((x) => { return Math.Sin(x); });

            else
            {
                derivativeDef = new FunctionDefintion((D) =>
                {

                    Double x = D - (Double)(1 / (Double)precision);
                    return ((Double)(Defintion(x) - Defintion(D)) / (Double)(x - D));
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
            
            
            return new Function(derivativeDef, Dimension);
        }
        /*public Function Derivative(string definition)
        {

            if (definition == "sin(x)")
                return new Function(new FunctionDefintion((a) => { return Math.Cos(a); }), Dimension);
        }*/
        public Function NthDerivative(Int32 n,Int32 precision = 100000)
        {
            
            if(n >= 1)
            {
                Function derivativeFunc;

                if (Defintion == Math.Sin)
                    derivativeFunc = new Function(trigonoderivs[n%4], Dimension);//((x) => { return Math.Cos(x); });
                else if (Defintion == Math.Cos)
                    derivativeFunc = new Function(trigonoderivs[n % 4 +1], Dimension);//((x) => { return MinusSin(x); });
                else if (Defintion == MinusSin)
                    derivativeFunc = new Function(trigonoderivs[n % 4+2], Dimension);//((x) => { return MinusCos(x); });
                else if (Defintion == MinusCos)
                    derivativeFunc = new Function(trigonoderivs[n % 4+3], Dimension);//((x) => { return Math.Sin(x); });
                

                else
                {
                    derivativeFunc = this.Derivative(precision);
                    for (Int32 i = 0; i < n - 1; i++)
                    {
                       derivativeFunc = derivativeFunc.Derivative(precision);
                    
                    }
                }
                
                return derivativeFunc;
            }
            else
            {
                return this;
            }
        }
        public Double this[Double x]
        {
            get
            {
                //if (Dimension.Contains(x))
                    // This indexer is very simple, and just returns or sets 
                    // the corresponding element from the internal array. 
                    return Math.Round(Defintion(x),4);
                //throw new Exception("Out of Dimension");
            }
           
        }
        public Double DefiniteRiemannIntegral(Double from, Double to, Int64 n = 1000)
        {
            Double sum = 0;
            for (Double i = from; i < to; i += (to-from)/(Double)n )
            {
                sum += Defintion(i);
            }
            return sum/(Double)n;
                //throw new NotImplementedException();
        }
        public Function Antiderivative(Int32 precision = 10000)
        {
            //Function 
            FunctionDefintion def = new FunctionDefintion((D) => 
            {
                return DefiniteRiemannIntegral(1, D, precision);
            });
            return new Function(def,Dimension);
        }
        private Int32 Factiorial (Int32 n)
        {
            if(n<=1){ return 1; }
            else { return n * Factiorial(n - 1); }
        }
        private Double power(Double x, Int32 k)
        {
            if (k == 0) return 1;
            Double temp = x;
            for (Int32 i = 0; i < k-1; i++)
			{
			    temp*=x;
			}
            return temp;
        }
        public Function SplineInterpol (Double from, Double to, Double step=0.1)
        {
            //Polynomial pol = new Polynomial();
            List<Double> dimension = new List<Double>();
            for (Double i = from; i < to; i+= step)
            {
                if (Dimension.Contains(i))
                {
                    dimension.Add(i);
                }
            }
            
            return new Function((x) =>
            {
               List<Double> ljs = new List<Double>();
               /*for (int i = 0; i < dimension.Count; i++)
               {
                   double temp = 1;
                   for (int j = 0; j < dimension.Count; j += 1)
                   {
                       if (dimension[i] != dimension[j])
                           temp *= (x - dimension[j]) / (dimension[i] - dimension[j]);
                   }
                   ljs.Add(temp);
               }*/
                for (Double i = from; i < to; i+=step)
			    {
			        Double temp = 1;
                    for (Double j = from; j < to; j+=step)
                    {
                        if(i!=j)
                        temp *= (x-j)/(i-j);
                    }
                    ljs.Add(temp);
			    }
                Double temp2 = 0;
                Int32 position = 0;
                for (Double i = from; i < to; i+=step)
                {
                    if(Dimension.Contains(i))
                    temp2 += ljs[position]*Defintion(i);
                    position++;
                }
                return temp2;
            },Dimension);
            //throw new NotImplementedException();
        }
        public Function TaylorSeries (Int32 periods)
        {
            //new FunctionDefinition("Sin(x)");
            /*double a =0;
            double[] derivvals = new double[periods];
            for (int i = 0; i < periods; i++)
            {
                derivvals[i] = NthDerivative(i)[a];
            }
            return new Function(derivvals,(x) => 
            {
                double sum = 0;
                for (int i = 0; i < periods; i++)
                {
                    
                    //double derivval = NthDerivative(i)[a];
                    int fact = Factiorial(i);
                    sum += ((derivvals[i] / fact) * power((x-a),i));
                }
                
                return sum;
            
            }, Dimension);*/
            Double a = 0;
            
            return new Function((x) =>
            {
                Double sum = 0;
                for (Int32 i = 0; i < periods; i++)
                {

                    //double derivval = NthDerivative(i)[a];
                    Int32 fact = Factiorial(i);
                    sum += (Math.Round((this.NthDerivative(i)[x] / (Double)fact),10) * power((x), i));
                }

                return sum;

            }, Dimension);
            //throw new NotImplementedException();
        }
    }
    
}
