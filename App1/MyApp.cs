using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Veldrid;

namespace App1
{
    public class MyApp
    {
        private static CommandList _commandList;
        private Camera _camera;

        private AndroidApplicationWindow Window { get; }
        private GraphicsDevice GraphicsDevice { get; set; }
        private ResourceFactory ResourceFactory { get; set; }
        private Swapchain MainSwapchain { get; set; }


        public MyApp(AndroidApplicationWindow window)  
        {
            Window = window;
            Window.Resized += HandleWindowResize;
            Window.GraphicsDeviceCreated += OnGraphicsDeviceCreated;
            Window.GraphicsDeviceDestroyed += OnDeviceDestroyed;
            Window.Rendering += PreDraw;
            Window.Rendering += Draw;

            _camera = new Camera(Window.Width, Window.Height);
        }

        private void OnGraphicsDeviceCreated(GraphicsDevice gd, ResourceFactory factory, Swapchain sc)
        {
            GraphicsDevice = gd;
            ResourceFactory = factory;
            MainSwapchain = sc;
            CreateResources(factory);
            CreateSwapchainResources(factory);
        }

        private void OnDeviceDestroyed()
        {
            GraphicsDevice = null;
            ResourceFactory = null;
            MainSwapchain = null;
        }
        private void CreateSwapchainResources(ResourceFactory factory) { }

        private void CreateResources(ResourceFactory factory)
        {
            _commandList = factory.CreateCommandList();
        }

        private void PreDraw(float deltaSeconds)
        {
            _camera.Update(deltaSeconds);
        }

        private void Draw(float deltaSeconds)
        {
            // Begin() must be called before commands can be issued.
            _commandList.Begin();

            // We want to render directly to the output window.
            _commandList.SetFramebuffer(GraphicsDevice.SwapchainFramebuffer);
            _commandList.ClearColorTarget(0, RgbaFloat.Pink);

            _commandList.End();
            GraphicsDevice.SubmitCommands(_commandList);

            // Once commands have been submitted, the rendered image can be presented to the application window.
            GraphicsDevice.SwapBuffers();
        }

        private void HandleWindowResize()
        {
            _camera.WindowResized(Window.Width, Window.Height);
        }

    }
}