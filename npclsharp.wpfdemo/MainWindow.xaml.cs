using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PortableClassLibrary_NP;
using PortableClassLibrary_NP.MathLibrary;
using System.Windows.Threading;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        const double maxdouble = 10e12;
        const double mindouble = 10e-12;
        const double from = -100;
        const double to = 100;
        const double step = 1;
        PortableClassLibrary_NP.MathLibrary.Function func, func2, taylorseries;
        private FunctionDecimal funcDec, taylorseriesDecimal;
        public MainWindow()
        {
            base.DataContext = context;
            InitializeComponent();
            func = new PortableClassLibrary_NP.MathLibrary.Function(Math.Sin,new PortableClassLibrary_NP.MathLibrary.SetExpression("(neginf,inf)",emptyarray()));
            funcDec = new FunctionDecimal(fdecimal, new SetExpression("(neginf,inf)", emptyarray()));
            func2 = func.SplineInterpol(from,to,step);
            taylorseries = func.TaylorSeries(11);
            taylorseriesDecimal = funcDec.TaylorSeries(15);
            
            //Slider_Horizontal.DataContext = context;
            //Slider_Vertical.DataContext = context;
            context.A = 10;
            context.B = 10;
            PreciseNumber a = new PreciseNumber("584753435875487,3548978458789719325136278327153216731932751");
            PreciseNumber b = new PreciseNumber(512300341.911);
            var c= a+b;
            var str = a.ToDouble();
        }
        private double[] emptyarray()
        {
            return new double[] { };
        }
        private double f (double x)
        {
            return Math.Cos(x);
            //return (x * x*x*x);
            //return -1 * Math.Sin(x);
           
        }
        private decimal fdecimal (decimal x)
        {
            return (decimal)Math.Cos((double)x);
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DrawPoints(func);
            DrawPoints(taylorseriesDecimal);
            /*for (int i = 0; i < 3; i++)
            {
                DrawPoints(func.NthDerivative(i));
            }*/
        }
        List<Line> CoordPoints = new List<Line>();
        public struct DataContexts
        {
            public decimal A { get; set; }
            public decimal B { get; set; }
        }
        private int width;
        public int Width
        {
            get { return width; }
            private set
            {
                width = value;
            }
        }
        DataContexts context;
        private void DrawPoints(FunctionDecimal f)
        {
           // CoordPoints.Clear();
            //MessageBox.Show(f[152.0].ToString());
            double baseY = Canvas.Height / 2;
            double baseX = Canvas.Width / 2;
            for (double i = from; i < to; i += step)
            {
                decimal val, val2;
                //if (f.Dimension.Contains((decimal)i / context.B))
                {
                    
                    val = (decimal)baseY - context.A * f[(decimal)i / context.B];
                    //if (f.Dimension.Contains((i + step) / context.B))
                    {
                        val2 = (decimal)baseY - context.A * f[(decimal)(i + step) / context.B];
                        //if (val > 0)
                        {
                            //if (!double.IsNaN(val) && !double.IsNaN(val2))
                            {
                                Line r = new Line();
                                //CoordPoints.Add(r);
                                //r.Width = r.Height = 1;
                                r.StrokeThickness = 1;
                                r.X1 = baseX + i;
                                r.X2 = baseX + i + step;
                                r.Y1 = (double)val;
                                r.Y2 = (double)val2;
                                r.Fill = Brushes.Black;
                                r.Stroke = Brushes.Red;
                                r.Visibility = System.Windows.Visibility.Visible;
                                CoordPoints.Add(r);
                                Canvas.Children.Add(r);
                            }
                            
                          
                        }
                    }
                }
            }
          
                
                
        }
        
        private static Action EmptyDelegate = delegate() { };

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //MessageBox.Show(e.Delta.ToString());
        }

        private void Slider_Horizontal_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            context.A = (decimal)Slider_Horizontal.Value;
        }

        private void Slider_Vertical_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            context.B = (decimal)Slider_Vertical.Value;
        }

        private void Slider_Periods_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(func!=null)
            {
                Button_Click_1(sender, e);
                taylorseries = func.TaylorSeries((int)Slider_Periods.Value);
                DrawPoints(taylorseriesDecimal);
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < CoordPoints.Count; i++)
            {
                CoordPoints[i].Visibility = System.Windows.Visibility.Hidden;
                Canvas.Children.Remove(CoordPoints[i]);
                
            }
            CoordPoints.Clear();
            //Canvas.Children.Clear();
        }
    }
}
