using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WeatherStation
{
    public class HorizontalGauge : SingleDataGauges
    {


        public HorizontalGauge(string gaugeName, string unit, SolidColorBrush foreGround, SolidColorBrush backGround, SolidColorBrush gridColor, SolidColorBrush fontColor, int fontSize, FontFamily fontFamily, Canvas parentGrid, double maxValue, double minValue, int gridMajorLinesAmount) : base(gaugeName, unit, foreGround, backGround, gridColor, fontColor, fontSize, fontFamily, parentGrid, maxValue, minValue, gridMajorLinesAmount)
        {
            Draw();
        }

        public override void Draw()
        {
            backGroundRectangle.Height = gaugeWidth;
            backGroundRectangle.Width = parentGrid.Width - topMargin - bottomMargin;
            backGroundRectangle.Fill = backGround;
            Canvas.SetLeft(backGroundRectangle, topMargin);
            Canvas.SetTop(backGroundRectangle, parentGrid.Height / 2 - gaugeWidth / 2);

            foreGroundRectangle.Height = gaugeWidth;
            foreGroundRectangle.Width = (currentValue / (maxValue - minValue) * backGroundRectangle.Width);
            foreGroundRectangle.Fill = foreGround;
            Canvas.SetLeft(foreGroundRectangle, topMargin);
            Canvas.SetTop(foreGroundRectangle, parentGrid.Height / 2 - gaugeWidth / 2);

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
            ParentGrid.Children.Add(gaugeTitleTextBlock);

            for (int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {
                parentGrid.Children.Add(horizontalMajorGridLines[i]);

                horizontalMajorGridLines[i].X1 = -i * backGroundRectangle.Width / (gridMajorLinesAmount) + backGroundRectangle.Width + topMargin;
                horizontalMajorGridLines[i].X2 = horizontalMajorGridLines[i].X1;

                horizontalMajorGridLines[i].Y1 = parentGrid.Height / 2 - backGroundRectangle.Height / 2;
                horizontalMajorGridLines[i].Y2 = horizontalMajorGridLines[i].Y1 + gaugeWidth;
            }

            for (int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {
                parentGrid.Children.Add(gridLabels[i]);

                Canvas.SetLeft(gridLabels[i], i * backGroundRectangle.Width / (gridMajorLinesAmount) + topMargin - 7);
                Canvas.SetTop(gridLabels[i], parentGrid.Height / 2 + gaugeWidth / 2 + 3);
            }
        }

        public override void Update()
        {
            foreGroundRectangle.Width = (currentValue / (MaxValue - MinValue) * backGroundRectangle.Width);
        }
    }
}
