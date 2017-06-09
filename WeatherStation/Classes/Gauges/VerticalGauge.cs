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
    public class VerticalGauge : SingleDataGauges
    {
        public VerticalGauge(string gaugeName, string unit, SolidColorBrush foreGround, SolidColorBrush backGround, SolidColorBrush gridColor, SolidColorBrush fontColor, int fontSize, FontFamily fontFamily, Canvas parentGrid, double maxValue, double minValue, int gridMajorLinesAmount) : base(gaugeName, unit, foreGround, backGround, gridColor, fontColor, fontSize, fontFamily, parentGrid, maxValue, minValue, gridMajorLinesAmount)
        {
            Draw();
        }

        public override void Draw()
        {
            backGroundRectangle.Width = gaugeWidth;
            backGroundRectangle.Height = parentGrid.Height - topMargin - bottomMargin;
            backGroundRectangle.Fill = backGround;
            Canvas.SetLeft(backGroundRectangle, parentGrid.Width/2 - backGroundRectangle.Width/2);
            Canvas.SetTop(backGroundRectangle, topMargin);

            foreGroundRectangle.Width = gaugeWidth;
            foreGroundRectangle.Height = (currentValue / (maxValue - minValue) * backGroundRectangle.Height); ;
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

                gridLabels[i].Loaded += UpdateGridLabelsWhenLoaded;
            }

        }

        private void UpdateGridLabelsWhenLoaded(object sender, EventArgs e)
        {
            for (int i = 0; i < (gridMajorLinesAmount + 1); i++)
            {

                gridLabels[i].RenderTransformOrigin = new System.Windows.Point(gridLabels[i].ActualWidth, gridLabels[i].ActualHeight);

                Canvas.SetLeft(gridLabels[i], parentGrid.Width / 2 - gridLabels[i].ActualWidth - gaugeWidth/2 - 5);
                Canvas.SetTop(gridLabels[i], -i * backGroundRectangle.Height / (gridMajorLinesAmount) + backGroundRectangle.Height + topMargin - gridLabels[i].ActualHeight/2);
            }
            
        }

        public override void Update()
        {
            foreGroundRectangle.Height = (currentValue / (maxValue - minValue) * backGroundRectangle.Height);
            Canvas.SetTop(foreGroundRectangle, backGroundRectangle.Height + topMargin - foreGroundRectangle.Height);
            
            if (foreGroundRectangle.Height > backGroundRectangle.Height)
                foreGroundRectangle.Height = 0;
           
        }
    }
}
