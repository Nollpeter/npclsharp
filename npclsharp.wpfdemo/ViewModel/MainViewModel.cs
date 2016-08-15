using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PortableClassLibrary_NP.MathLibrary;
using testWPF.Annotations;
using testWPF.ViewModel;

namespace testWPF
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private String _points;
        private Point _startPoint;
        private Int32 _samplingFrequency;
        public Function Function { get; set; }

        public String Points
        {
            get { return _points; }
            set
            {
                if (value == _points) return;
                _points = value;
                OnPropertyChanged();
            }
        }

        public Point StartPoint
        {
            get { return _startPoint; }
            set
            {
                if (value.Equals(_startPoint)) return;
                _startPoint = value;
                OnPropertyChanged();
            }
        }

        public Int32 SamplingFrequency
        {
            get { return _samplingFrequency; }
            set
            {
                if (value.Equals(_samplingFrequency)) return;
                _samplingFrequency = value;
                OnPropertyChanged();
            }
        }

        public ICommand MouseWheelUpCommand { get; set; }

        public void Recalculate()
        {
            Points = "";
            var XPlus = 400;
            var l = 100;
            var d = 25;
            StartPoint = new Point(-1 * l + XPlus, Function[-1 * l] / d);
            for (Int32 i = -1 * l; i < l; i += SamplingFrequency )
            {

                //vals.Add(Function[i]);

                Points += $"{i + XPlus},{Function[i] / d} ";
            }
            Points += $"{l + 1 + XPlus},{Function[l + 1] / d} ";
        }
        public MainViewModel()
        {
            MouseWheelUpCommand = new DelegateCommand(p =>
            {
                
            });
            Function=new Function(new Function.FunctionDefintion(dimension => dimension*dimension),new SetExpression("(neginf,inf)",new Double[] {}) );
            this.PropertyChanged += MainViewModel_PropertyChanged;

        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SamplingFrequency))
            {
                Recalculate();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
