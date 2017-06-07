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
    public class VerticalGauge : Gauge
    {
        protected double minValue;
        protected double maxValue;
        protected double currentValue = 0;
        protected double topMargin = 30;
        protected double bottomMargin = 30;
        protected double gaugeWidth = 20;
        protected int gridMajorLinesAmount = 10;
        protected Canvas parentGrid;

        protected Rectangle foreGroundRectangle = new Rectangle();
        protected Rectangle backGroundRectangle = new Rectangle();

        protected List<Line> horizontalMajorGridLines = new List<Line>();
        protected List<Line> horizontalMinorGridLines = new List<Line>();

        protected List<TextBlock> gridLabels = new List<TextBlock>();

        protected TextBlock gaugeTitleTextBlock = new TextBlock();

        public double MinValue
        {
            get { return minValue; }
            set
            {
                if(value >= maxValue)
                {
                    throw new MinValueBiggerThanMaxValueException(value, maxValue);
                } else
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
                if(value <= minValue)
                {
                    throw new MinValueBiggerThanMaxValueException(minValue, value);
                } else
                {
                    this.maxValue = value;
                }
            }
        }

        public Canvas ParentGrid
        {
            get { return parentGrid; }
        }


        public VerticalGauge(string gaugeName, string unit, SolidColorBrush foreGround, SolidColorBrush backGround, SolidColorBrush gridColor, SolidColorBrush fontColor, int fontSize, FontFamily fontFamily, Canvas parentGrid, double maxValue, double minValue, int gridMajorLinesAmount) : base(gaugeName, unit, foreGround, backGround, gridColor, fontColor, fontSize, fontFamily)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.parentGrid = parentGrid;
            this.gridMajorLinesAmount = gridMajorLinesAmount;

            for(int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {
                Line line = new Line();
                line.Stroke = gridColor;
                line.StrokeThickness = 2;

                horizontalMajorGridLines.Add(line);
            }

            for(int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {
                TextBlock textB = new TextBlock();
                textB.Text = ((maxValue - minValue) / (gridMajorLinesAmount) * i).ToString();
                textB.FontSize = fontSize - 2;
                textB.FontFamily = fontFamilyGauge;
                textB.TextAlignment = System.Windows.TextAlignment.Justify;

                gridLabels.Add(textB);
            }

            Draw();
        }

        public override void Draw()
        {
            backGroundRectangle.Width = gaugeWidth;
            backGroundRectangle.Height = parentGrid.Height - topMargin - bottomMargin;
            backGroundRectangle.Fill = backGround;
            Canvas.SetLeft(backGroundRectangle, parentGrid.Width/2 - backGroundRectangle.Width/2);
            Canvas.SetTop(backGroundRectangle, 30);

            foreGroundRectangle.Width = gaugeWidth;
            foreGroundRectangle.Height = currentValue;
            foreGroundRectangle.Fill = foreGround;
            Canvas.SetLeft(foreGroundRectangle, parentGrid.Width / 2 - foreGroundRectangle.Width / 2);
            Canvas.SetTop(foreGroundRectangle, backGroundRectangle.Height + topMargin);

            gaugeTitleTextBlock.Text = this.name + " (" + this.unit + ")";
            gaugeTitleTextBlock.FontSize = this.fontSize;
            gaugeTitleTextBlock.FontFamily = this.fontFamilyGauge;
            gaugeTitleTextBlock.Foreground = this.FontColor;
            gaugeTitleTextBlock.Width = parentGrid.Width;
            gaugeTitleTextBlock.TextAlignment = System.Windows.TextAlignment.Center;
            Canvas.SetLeft(gaugeTitleTextBlock, 0);
            Canvas.SetTop(gaugeTitleTextBlock, 1);            

            parentGrid.Children.Add(backGroundRectangle);
            parentGrid.Children.Add(foreGroundRectangle);
            parentGrid.Children.Add(gaugeTitleTextBlock);

            for (int i = 0; i < (gridMajorLinesAmount+1); i++)
            {
                parentGrid.Children.Add(horizontalMajorGridLines[i]);
                horizontalMajorGridLines[i].X1 = parentGrid.Width / 2 - backGroundRectangle.Width / 2;
                horizontalMajorGridLines[i].X2 = parentGrid.Width / 2 + backGroundRectangle.Width / 2;
                horizontalMajorGridLines[i].Y1 = -i * backGroundRectangle.Height / (gridMajorLinesAmount) + backGroundRectangle.Height + topMargin;
                horizontalMajorGridLines[i].Y2 = horizontalMajorGridLines[i].Y1;
            }

            for (int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {
                parentGrid.Children.Add(gridLabels[i]);
                Canvas.SetLeft(gridLabels[i], parentGrid.Width / 2 - 40);
                Canvas.SetTop(gridLabels[i], -i * backGroundRectangle.Height / (gridMajorLinesAmount) + backGroundRectangle.Height + topMargin - 7);
            }

        }

        public override void Update()
        {
            foreGroundRectangle.Height = (currentValue / (maxValue - minValue) * backGroundRectangle.Height);
            Canvas.SetTop(foreGroundRectangle, backGroundRectangle.Height + topMargin - foreGroundRectangle.Height);
            
            if (foreGroundRectangle.Height > backGroundRectangle.Height)
                foreGroundRectangle.Height = 0;
           
        }

        public void SetCurrentValue(double value)
        {
            this.currentValue = value;
        }
    }
}
