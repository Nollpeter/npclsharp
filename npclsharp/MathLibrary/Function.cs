using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.MathLibrary
{
    public interface IFunction
    {

    }
    public class Function : IFunction
    {
        FunctionDefintion[] trigonoderivs = new FunctionDefintion[] { Math.Sin, Math.Cos, MinusSin, MinusCos, };
        List<double> DimensionExclude { get; set; }
        public delegate double FunctionDefintion(double Dimension);
        public double definitionvalx, definitionvalx0;
        public FunctionDefintion Defintion { get; set; }
        public SetExpression Dimension;
        double[] PolynomialMults;
        public Function(double[] PolinomialMultipliers,FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
        {
            PolynomialMults = PolinomialMultipliers;
            Defintion = DefinitionOfFunction;
            DimensionExclude = new List<double>();
            this.Dimension = Dimension;
        }
        public Function(FunctionDefintion DefinitionOfFunction, SetExpression Dimension)
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
        public Function Derivative(int precision = 100000)
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
                    double x = D - (double)(1 / (double)precision);
                    return ((double)(Defintion(x) - Defintion(D)) / (double)(x - D));
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
        public Function NthDerivative(int n,int precision = 100000)
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
                    for (int i = 0; i < n - 1; i++)
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
        public double this[double x]
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
        public double DefiniteRiemannIntegral(double from, double to, long n = 1000)
        {
            double sum = 0;
            for (double i = from; i < to; i += (to-from)/(double)n )
            {
                sum += Defintion(i);
            }
            return sum/(double)n;
                //throw new NotImplementedException();
        }
        public Function Antiderivative(int precision = 10000)
        {
            //Function 
            FunctionDefintion def = new FunctionDefintion((D) => 
            {
                return DefiniteRiemannIntegral(1, D, precision);
            });
            return new Function(def,Dimension);
        }
        private int Factiorial (int n)
        {
            if(n<=1){ return 1; }
            else { return n * Factiorial(n - 1); }
        }
        private double power(double x, int k)
        {
            if (k == 0) return 1;
            double temp = x;
            for (int i = 0; i < k-1; i++)
			{
			    temp*=x;
			}
            return temp;
        }
        public Function SplineInterpol (double from, double to, double step=0.1)
        {
            //Polynomial pol = new Polynomial();
            List<double> dimension = new List<double>();
            for (double i = from; i < to; i+= step)
            {
                if (Dimension.Contains(i))
                {
                    dimension.Add(i);
                }
            }
            
            return new Function((x) =>
            {
               List<double> ljs = new List<double>();
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
                for (double i = from; i < to; i+=step)
			    {
			        double temp = 1;
                    for (double j = from; j < to; j+=step)
                    {
                        if(i!=j)
                        temp *= (x-j)/(i-j);
                    }
                    ljs.Add(temp);
			    }
                double temp2 = 0;
                int position = 0;
                for (double i = from; i < to; i+=step)
                {
                    if(Dimension.Contains(i))
                    temp2 += ljs[position]*Defintion(i);
                    position++;
                }
                return temp2;
            },Dimension);
            //throw new NotImplementedException();
        }
        public Function TaylorSeries (int periods)
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
            double a = 0;
            
            return new Function((x) =>
            {
                double sum = 0;
                for (int i = 0; i < periods; i++)
                {

                    //double derivval = NthDerivative(i)[a];
                    int fact = Factiorial(i);
                    sum += (Math.Round((this.NthDerivative(i)[x] / (double)fact),10) * power((x), i));
                }

                return sum;

            }, Dimension);
            //throw new NotImplementedException();
        }
    }
    
}
