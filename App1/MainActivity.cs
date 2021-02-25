using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidBase;
using Veldrid;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private VeldridSurfaceView _view;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            bool debug = false;
#if DEBUG
            debug = true;
#endif

            GraphicsDeviceOptions options = new GraphicsDeviceOptions(
                debug,
                PixelFormat.R16_UNorm,
                false,
                ResourceBindingModel.Improved,
                true,
                true);
            GraphicsBackend backend = GraphicsDevice.IsBackendSupported(GraphicsBackend.Vulkan)
                ? GraphicsBackend.Vulkan
                : GraphicsBackend.OpenGLES;
            _view = new VeldridSurfaceView(this, backend, options);
            AndroidApplicationWindow window = new AndroidApplicationWindow(this, _view);
            window.GraphicsDeviceCreated += (g, r, s) => window.Run();
            MyApp app = new MyApp(window);
            SetContentView(_view);
        }
        protected override void OnPause()
        {
            base.OnPause();
            _view.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _view.OnResume();
        }


    }
}
