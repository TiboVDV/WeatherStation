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
    public class GraphGauge : MultiDataGauge
    {
        protected DateTime currentTime = DateTime.Now;


        public GraphGauge(string name, string unit, SolidColorBrush foreGround, SolidColorBrush backGround, SolidColorBrush gridColor, SolidColorBrush fontColor, int fontSize, FontFamily fontFamily, double xMinValue, double xMaxValue, double yMinValue, double yMaxValue, string xAxesLabel, string yAxesLabel, Canvas parentGrid) : base(name, unit, foreGround, backGround, gridColor, fontColor, fontSize, fontFamily, xMinValue, xMaxValue, yMinValue, yMaxValue, xAxesLabel, yAxesLabel, parentGrid)
        {

            Draw();
        }

        public override void Draw()
        {
            backgroundRectangle.Width = parentGrid.Width - leftMargin * 2;
            backgroundRectangle.Height = parentGrid.Height - topMargin * 2;
            backgroundRectangle.Fill = backGround;
            Canvas.SetTop(backgroundRectangle, topMargin);
            Canvas.SetLeft(backgroundRectangle, leftMargin);

            parentGrid.Children.Add(backgroundRectangle);

            chartTitleTextBlock.Text = this.name;
            chartTitleTextBlock.FontFamily = fontFamilyGauge;
            chartTitleTextBlock.FontSize = fontSize;
            chartTitleTextBlock.Foreground = fontColor;
            chartTitleTextBlock.Width = parentGrid.Width;
            chartTitleTextBlock.TextAlignment = System.Windows.TextAlignment.Center;

            parentGrid.Children.Add(chartTitleTextBlock);

            xAxesLine.X1 = leftMargin;
            xAxesLine.Y1 = parentGrid.Height - topMargin;
            xAxesLine.X2 = parentGrid.Width - leftMargin;
            xAxesLine.Y2 = xAxesLine.Y1;
            xAxesLine.Stroke = gridColor;
            xAxesLine.StrokeThickness = 3;

            parentGrid.Children.Add(xAxesLine);

            yAxesLine.X1 = leftMargin;
            yAxesLine.Y1 = parentGrid.Height - topMargin;
            yAxesLine.X2 = yAxesLine.X1;
            yAxesLine.Y2 = topMargin;
            yAxesLine.Stroke = gridColor;
            yAxesLine.StrokeThickness = 3;

            parentGrid.Children.Add(yAxesLine);
            
            xAxesLabel.FontFamily = fontFamilyGauge;
            xAxesLabel.FontSize = fontSize;
            xAxesLabel.Foreground = fontColor;
            xAxesLabel.Width = parentGrid.Width;
            xAxesLabel.TextAlignment = System.Windows.TextAlignment.Center;
            Canvas.SetTop(xAxesLabel, parentGrid.Height - 10);

            yAxesLabel.FontFamily = fontFamilyGauge;
            yAxesLabel.FontSize = fontSize;
            yAxesLabel.Foreground = fontColor;
            yAxesLabel.Width = parentGrid.Height;
            yAxesLabel.TextAlignment = System.Windows.TextAlignment.Center;

            RotateTransform rtrans = new RotateTransform(-90);
            yAxesLabel.LayoutTransform = rtrans;

            Canvas.SetLeft(yAxesLabel, -10);
            Canvas.SetTop(yAxesLabel, 0);

            for(int i = 0; i < (xMaxValue - xMinValue)/10; i++)
            {
                Line l = new Line();
                l.Y1 = topMargin;
                l.Y2 = parentGrid.Height - topMargin;

                l.X1 = backgroundRectangle.Width + topMargin - i * (backgroundRectangle.Width / ((xMaxValue - xMinValue) / 10));
                l.X2 = l.X1;
                l.Stroke = gridColor;
                l.StrokeThickness = 2;

                verticalMajorGridLines.Add(l);
                parentGrid.Children.Add(l);
            }

            for (int i = 0; i < (xMaxValue - xMinValue) / 2; i++)
            {
                Line l = new Line();
                l.Y1 = topMargin;
                l.Y2 = parentGrid.Height - topMargin;

                l.X1 = backgroundRectangle.Width + topMargin - i * (backgroundRectangle.Width / ((xMaxValue - xMinValue) / 2));
                l.X2 = l.X1;
                l.Stroke = gridColor;
                l.StrokeThickness = 0.5;

                verticalMinorGridLines.Add(l);
                parentGrid.Children.Add(l);
            }

            for (int i = 0; i < (yMaxValue - yMinValue) / 10; i++)
            {
                Line l = new Line();
                l.Y1 = backgroundRectangle.Height + topMargin - i * (backgroundRectangle.Height / ((yMaxValue - yMinValue) / 10));
                l.Y2 = l.Y1;

                l.X1 = leftMargin;
                l.X2 = parentGrid.Width - leftMargin;
                l.Stroke = gridColor;
                l.StrokeThickness = 2;

                horizontalMajorGridLines.Add(l);
                parentGrid.Children.Add(l);
            }

            for (int i = 0; i < (yMaxValue - yMinValue) / 2; i++)
            {
                Line l = new Line();
                l.Y1 = backgroundRectangle.Height + topMargin - i * (backgroundRectangle.Height / ((yMaxValue - yMinValue) / 2));
                l.Y2 = l.Y1;

                l.X1 = leftMargin;
                l.X2 = parentGrid.Width - leftMargin;
                l.Stroke = gridColor;
                l.StrokeThickness = 0.5;

                horizontalMinorGridLines.Add(l);
                parentGrid.Children.Add(l);
            }

            parentGrid.Children.Add(xAxesLabel);

            parentGrid.Children.Add(yAxesLabel);

        }

        public override void Update()
        {
            measurementsPoints.Clear();
            measurementsLines.Clear();
            if(measurements.Count > 1)
            {
                for (int i = measurements.Count-1; i > 0; i--)
                {

                    TimeSpan timeDiff = (currentTime - measurements[i].DateTimeOfMeasurement);
                    if (timeDiff.TotalSeconds > (xMaxValue - xMinValue))
                    {
                        measurements.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        Point p = new Point();
                        p.X = -(timeDiff.TotalSeconds / (xMaxValue - xMinValue)) * backgroundRectangle.Width  + leftMargin;
                        p.Y = parentGrid.Height - topMargin - (((int)measurements[i].Value / (yMaxValue - yMinValue)) * backgroundRectangle.Height);
                        measurementsPoints.Add(p);
                    }
                }

                if (measurementsPoints.Count > 1)
                {
                    for (int i = 1; i < measurementsPoints.Count; i++)
                    {
                        Line l = new Line();
                        l.X1 = measurementsPoints[i - 1].X;
                        l.Y1 = measurementsPoints[i - 1].Y;
                        l.X2 = measurementsPoints[i].X;
                        l.Y2 = measurementsPoints[i].Y;
                        l.Stroke = foreGround;
                        l.StrokeThickness = 4;

                        parentGrid.Children.Add(l);
                    }
                }
            }
            


        }

        public void AddMeasurement(Measurement m)
        {

            if (measurements.Count > (xMaxValue - xMinValue))
            {
                measurements.RemoveAt(0);
                measurements.Add(m);
            } else
            {
                measurements.Add(m);
            }
        }

        private void CalculatePoints()
        {
        }
    }
}
