using EnoughHookLite;
using EnoughHookLite.Scripting;
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
        private Drawer Drawer;
        private List<(string, Script)> DrawList;
        private WindowSystem WindowSystem;

        public EHLWindow(App app, string[] args)
        {
            InitializeComponent();
            App = app;

            DrawList = new List<(string, Script)>();
            Drawer = new Drawer(this);

            Drawer.Setup((ulong)Width, (ulong)Height);
            CustomDraw.Source = Drawer.WriteBitmap;

            Drawer.DrawCall = DrawAll;
            Drawer.Start();

            App.Start(args);
            App.BeforeSetupScript = SetupDrawAPI;
            App.OnUpdate = OnUpdate;
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
                    Drawer.Setup((ulong)Width, (ulong)Height);
                    CustomDraw.Source = Drawer.WriteBitmap;
                }

                ehlwindow.Left = pos.X;
                ehlwindow.Top = pos.Y;
                ehlwindow.Width = size.X;
                ehlwindow.Height = size.Y;
            });
        }

        private void SetupDrawAPI(App app)
        {
            app.JSLoader.JSApi.AddValue("drawapi", Drawer.DrawAPI);

            app.JSLoader.JSApi.AddType("Brush", typeof(System.Drawing.Brush));
            app.JSLoader.JSApi.AddType("SolidBrush", typeof(SolidBrush));
            app.JSLoader.JSApi.AddType("Color", typeof(System.Drawing.Color));
            app.JSLoader.JSApi.AddType("Pen", typeof(System.Drawing.Pen));
            app.JSLoader.JSApi.AddType("Font", typeof(Font));
            app.JSLoader.JSApi.AddType("PointF", typeof(PointF));

            app.JSLoader.JSApi.AddEvent("OnDraw", DrawList);
        }

        private void DrawAll()
        {
            int dlco = DrawList.Count;
            for (int i = 0; i < dlco; i++)
            {
                var del = DrawList[i];
                var name = del.Item1;

                del.Item2.JSEngine.Invoke(name);
            }
        }
    }
}
