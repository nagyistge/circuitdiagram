﻿// PrintRenderer.cs
//
// Circuit Diagram http://www.circuit-diagram.org/
//
// Copyright (C) 2011  Sam Fisher
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace CircuitDiagram
{
    class PrintRenderer : IRenderer
    {
        private Canvas m_canvas;
        private double m_renderWidth;
        private double m_renderHeight;

        public PrintRenderer(double width, double height, double renderWidth, double renderHeight)
        {
            m_canvas = new Canvas();
            m_canvas.Width = width;
            m_canvas.Height = height;
            m_renderWidth = renderWidth;
            m_renderHeight = renderHeight;
        }

        public RenderTargetBitmap GetImage()
        {
            m_canvas.Arrange(new Rect(0, 0, m_canvas.Width, m_canvas.Height));
            double dpiX = (m_renderWidth / m_canvas.Width) * 96d;
            double dpiY = (m_renderHeight / m_canvas.Height) * 96d;
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)m_renderWidth, (int)m_renderHeight, dpiX, dpiY, PixelFormats.Default);
            bmp.Render(m_canvas);
            return bmp;
        }

        public FixedDocument GetDocument(Size pageSize)
        {
            FixedDocument returnDocument = new FixedDocument();
            PageContent pageContent = new PageContent();
            pageContent.Width = pageSize.Width;
            pageContent.Height = pageSize.Height;
            FixedPage fixedPage = new FixedPage();
            fixedPage.Width = pageSize.Width;
            fixedPage.Height = pageSize.Height;
            double scale = 1.0d;
            if (m_canvas.Width > fixedPage.Width)
                scale = fixedPage.Width / m_canvas.Width;
            m_canvas.LayoutTransform = new ScaleTransform(scale, scale);
            fixedPage.Children.Add(m_canvas);
            ((IAddChild)pageContent).AddChild(fixedPage);
            returnDocument.Pages.Add(pageContent);
            return returnDocument;
        }

        public void DrawLine(Color color, double thickness, Point point0, Point point1)
        {
            m_canvas.Children.Add(new Line() { X1 = point0.X, Y1 = point0.Y, X2 = point1.X, Y2 = point1.Y, StrokeThickness = thickness, Stroke = new SolidColorBrush(color)});
        }

        public void DrawEllipse(Color filColor, Color strokColor, double strokeThickness, Point centre, double radiusX, double radiusY)
        {
            Ellipse ellipse = new Ellipse() { Fill = new SolidColorBrush(filColor), Stroke = new SolidColorBrush(strokColor), StrokeThickness = strokeThickness, Width = radiusX * 2, Height = radiusY * 2 };
            ellipse.SetValue(Canvas.LeftProperty, centre.X - radiusX);
            ellipse.SetValue(Canvas.TopProperty, centre.Y - radiusY);
            m_canvas.Children.Add(ellipse);
        }

        public void DrawRectangle(Color filColor, Color strokColor, double strokeThickness, Rect rectangle)
        {
            Rectangle rect = new Rectangle() { Fill = new SolidColorBrush(filColor), Stroke = new SolidColorBrush(strokColor), StrokeThickness = strokeThickness, Width = rectangle.Width, Height = rectangle.Height };
            rect.SetValue(Canvas.LeftProperty, rectangle.X);
            rect.SetValue(Canvas.TopProperty, rectangle.Y);
            m_canvas.Children.Add(rect);
        }

        public void DrawText(string text, string fontName, double emSize, Color forColor, Point origin)
        {
            TextBlock textBlock = new TextBlock() { Text = text, FontFamily = new FontFamily(fontName), FontSize = emSize, Foreground = new SolidColorBrush(forColor) };
            textBlock.SetValue(Canvas.LeftProperty, origin.X);
            textBlock.SetValue(Canvas.TopProperty, origin.Y);
            m_canvas.Children.Add(textBlock);
        }

        public void DrawFormattedText(FormattedText text, Point origin)
        {
        }

        public void DrawPath(Color? filColor, Color strokeColor, double thickness, string path, double translateOffsetX = 0.0, double translateOffsetY = 0.0)
        {
            Path pathElement = new Path();
            pathElement.Data = Geometry.Parse(path);
            pathElement.Stroke = new SolidColorBrush(strokeColor);
            if (translateOffsetX != 0.0 || translateOffsetY != 0.0)
                pathElement.RenderTransform = new TranslateTransform(translateOffsetX, translateOffsetY);
            if (filColor.HasValue)
                pathElement.Fill = new SolidColorBrush(filColor.Value);
            pathElement.StrokeThickness = thickness;
            m_canvas.Children.Add(pathElement);
        }
    }
}