using Ptg.Animation;
using Ptg.Animation.Helper;
using Ptg.Animation.Snoking;
using Ptg.Drawing;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Animator animator;
        public MainWindow()
        {
            InitializeComponent();
            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            Topmost = true;
            WindowState = WindowState.Maximized;
        }

        private void Ske_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (animator!=null)
            {
                e.Surface.Canvas.Clear();
                e.Surface.Canvas.DrawBitmap(animator.Scene.RenderContext.Bitmap, SKPoint.Empty);
            }
        }

        private void Ske_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                animator = new Animator(Ske.CanvasSize.Width, Ske.CanvasSize.Height);
                var Scene = animator.Scene;
                ImgHelper.LoadImg(Scene.TextureManager, System.IO.Path.Combine(Environment.CurrentDirectory, "snoke.png"), "snoke");
                var l = new SnokeLayout("snoke");
                Scene.Layouts.Add(l);
                Scene.RenderContext.LoadContent();
                animator.NeedToFrame += ani => Ske.InvalidateVisual();
                animator.Start();
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown(-1);

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown(-1);
        }
        
    }
}
