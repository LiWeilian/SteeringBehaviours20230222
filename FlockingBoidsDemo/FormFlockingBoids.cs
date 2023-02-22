﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using SkiaSharp.Views.Desktop;
using SteeringBehavioursCore.Controller;
using SteeringBehavioursCore.Renderer;

namespace FlockingBoidsDemo
{
    public partial class FormFlockingBoids : Form
    {
        private readonly Controller _controller;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        public FormFlockingBoids()
        {
            InitializeComponent();

            _controller = new Controller();
            _controller.CreateField();
            _controller.Field.SetFieldSize((float)ResultField.Width,
                (float)ResultField.Height);

            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            _controller.Field.Advance();
            ResultField.Invalidate();
        }

        private void SKElement_PaintSurface(object sender,
            SKPaintSurfaceEventArgs e)
        {
            _controller.CreateRenderer(new RendererSkiaSharp(e.Surface.Canvas));
            _controller.Renderer.Render(_controller.Field);
        }
    }
}
