using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WeatherStation
{
    public abstract class MultiDataGauge : Gauge
    {
        protected double xMinValue;
        protected double xMaxValue;
        protected double yMinValue;
        protected double yMaxValue;
        protected double topMargin = 30;
        protected double leftMargin = 30;

        protected Rectangle backgroundRectangle = new Rectangle();

        protected List<Measurement> measurements = new List<Measurement>();
        protected List<Ellipse> measurementsEllipses = new List<Ellipse>();
        protected List<Line> measurementsLines = new List<Line>();
        protected List<Point> measurementsPoints = new List<Point>();

        protected List<Line> horizontalMajorGridLines = new List<Line>();
        protected List<Line> horizontalMinorGridLines = new List<Line>();
        protected List<Line> verticalMajorGridLines = new List<Line>();
        protected List<Line> verticalMinorGridLines = new List<Line>();

        protected Line xAxesLine = new Line();
        protected Line yAxesLine = new Line();

        protected List<TextBlock> horizontalGridLabels = new List<TextBlock>();
        protected List<TextBlock> verticalGridLabels = new List<TextBlock>();
        protected TextBlock xAxesLabel = new TextBlock();
        protected TextBlock yAxesLabel = new TextBlock();

        protected TextBlock chartTitleTextBlock = new TextBlock();

        protected Canvas parentGrid;
        
        public List<Measurement> Measurements
        {
            get { return measurements; }
        }

        public MultiDataGauge(string name, string unit, SolidColorBrush foreGround, SolidColorBrush backGround, SolidColorBrush gridColor, SolidColorBrush fontColor, int fontSize, FontFamily fontFamily, double xMinValue, double xMaxValue, double yMinValue, double yMaxValue, string xAxesLabel, string yAxesLabel, Canvas parentGrid) : base(name, unit, foreGround, backGround, gridColor, fontColor, fontSize, fontFamily)
        {
            this.parentGrid = parentGrid;
            this.xAxesLabel.Text = xAxesLabel;
            this.yAxesLabel.Text = yAxesLabel;
            this.xMaxValue = xMaxValue;
            this.xMinValue = xMinValue;
            this.yMaxValue = yMaxValue;
            this.yMinValue = yMinValue;
        }
    }
}
