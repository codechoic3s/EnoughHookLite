using EnoughHookLite;
using EnoughHookLite.Scripting;
using EnoughHookLiteUI.Features;
using EnoughHookLiteUI.Rendering;
using EnoughHookLiteUI.Utils;
using EnoughHookLiteUI.Utils.Windowing;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnoughHookLiteUI.Windows
{
    /// <summary>
    /// Interaction logic for EHLWindow.xaml
    /// </summary>
    public partial class EHLWindow : System.Windows.Window
    {
        private App App;
        
        
        private WindowSystem WindowSystem;
        private ManualRenderer ManualRenderer;

        public EHLWindow(App app, string[] args)
        {
            InitializeComponent();
            App = app;

            ManualRenderer = new ManualRenderer(this);
            App.BeforeSetupScript = SetupDrawAPI;
            App.OnUpdate = OnUpdate;

            SetupWindowSystem();
        }

        private void SetupWindowSystem()
        {
            WindowSystem = new WindowSystem();
        }

        private void OnUpdate(System.Drawing.Point pos, Vector2 size)
        {
            Dispatcher.Invoke(() =>
            {
                if (size.X != Width || size.Y != Height)
                {
                    //LogIt($"Changed resolution to {size.X}:{size.y}");
                    ManualRenderer.Renderer.Drawer.Setup((ulong)size.X, (ulong)size.Y);
                    CustomDraw.Source = ManualRenderer.Renderer.Drawer.WriteBitmap;
                }

                ehlwindow.Left = pos.X;
                ehlwindow.Top = pos.Y;
                ehlwindow.Width = size.X;
                ehlwindow.Height = size.Y;
            });
        }

        private void SetupDrawAPI(App app)
        {
            app.ScriptHost.ScriptLoader.ScriptApi.AddSharedAPI("draw", ManualRenderer.Renderer.Drawer.DrawAPI);
        }
    }
}
