using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WeatherStation
{
    public abstract class SingleDataGauges : Gauge
    {

        protected double minValue;//Minimale waarde van de gauge
        protected double maxValue;//Maximale waarde van de gauge
        protected double currentValue = 0;//Huidige waarde van de gauge
        protected double topMargin = 30;//Margin van de gauge aan de bovenkant/linkerkant
        protected double bottomMargin = 30;//Margin van de gauge aan de onderkant/rechterkant
        protected double gaugeWidth = 25;//De breedte van de gauge
        protected int gridMajorLinesAmount = 10;//Aantal gridlines
        protected Canvas parentGrid;//Parent waarop getekend wordt

        protected Rectangle foreGroundRectangle = new Rectangle();//Voorgrond van de gauge
        protected Rectangle backGroundRectangle = new Rectangle();//Achtergrond van de gauge

        protected List<Line> horizontalMajorGridLines = new List<Line>();//Lijst met de grote lijnen van de grid
        protected List<Line> horizontalMinorGridLines = new List<Line>();//Lijst met de kleine lijnen van de grid

        protected List<TextBlock> gridLabels = new List<TextBlock>();//Lijst met alle labels van de grid

        protected TextBlock gaugeTitleTextBlock = new TextBlock();//Titel van de gauge

        //Getters en Setters
        public double MinValue
        {
            get { return minValue; }
            set
            {
                if (value >= maxValue)
                {
                    throw new MinValueBiggerThanMaxValueException(value, maxValue);
                }
                else
                {
                    this.minValue = value;
                }
            }
        }

        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value <= minValue)
                {
                    throw new MinValueBiggerThanMaxValueException(minValue, value);
                }
                else
                {
                    this.maxValue = value;
                }
            }
        }

        public Canvas ParentGrid
        {
            get { return parentGrid; }
        }

        public double CurrentValue
        {
            get { return currentValue; }
            set { this.currentValue = value; }
        }


        //Constructor
        public SingleDataGauges(string gaugeName, string unit, SolidColorBrush foreGround, SolidColorBrush backGround, SolidColorBrush gridColor, SolidColorBrush fontColor, int fontSize, FontFamily fontFamily, Canvas parentGrid, double maxValue, double minValue, int gridMajorLinesAmount) : base(gaugeName, unit, foreGround, backGround, gridColor, fontColor, fontSize, fontFamily)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.parentGrid = parentGrid;
            this.gridMajorLinesAmount = gridMajorLinesAmount;

            for (int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {
                Line line = new Line();
                line.Stroke = gridColor;
                line.StrokeThickness = 2;

                horizontalMajorGridLines.Add(line);
            }

            for (int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {
                TextBlock textB = new TextBlock();
                textB.Text = ((maxValue - minValue) / (gridMajorLinesAmount) * i).ToString();
                textB.FontSize = fontSize - 2;
                textB.FontFamily = fontFamilyGauge;
                textB.TextAlignment = System.Windows.TextAlignment.Justify;

                gridLabels.Add(textB);
            }
        }
    }
}
