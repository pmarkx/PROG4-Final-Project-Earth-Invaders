using Logic;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UI.Renderer
{
    public class Display : FrameworkElement
    {

        IGameModel model;
        Size size;

        public void Resize(Size size)
        {
            this.size = size;
        }

        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {

            if (model != null && size.Width > 50 && size.Height > 50)
            {
                double rectWidth = size.Width / model.Map.GetLength(0);
                double rectHeight = size.Height / model.Map.GetLength(1);

                //drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0),
                //    new Rect(0, 0, size.Width, size.Height));
                string folder = "./Renderer/images/";

                //https://stackoverflow.com/questions/31701159/alternative-to-bitmapimage-to-load-a-png
                for (int i = 0; i < model.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < model.Map.GetLength(1); j++)
                    {
                        ImageBrush brush = new ImageBrush();

                        switch (model.Map[i, j])
                        {
                            case Player:
                                brush = new ImageBrush
                                    (BitmapFrame.Create(File.OpenRead(folder + "player.png")));
                                break;
                            case Wall:
                                brush = new ImageBrush
                                    (BitmapFrame.Create(File.OpenRead(folder + "wall.png")));
                                break;
                            case Floor:
                                break;
                            case Mine:
                                brush = new ImageBrush
                                    (BitmapFrame.Create(File.OpenRead(folder + "mine.png")));
                                break;
                            case Enemy:
                                brush = new ImageBrush
                                    (BitmapFrame.Create(File.OpenRead(folder + "enemy.png")));
                                break;
                            case LifeReward:
                                brush = new ImageBrush
                                    (BitmapFrame.Create(File.OpenRead(folder + "lifereward.png")));
                                break;
                            case AmmoBox:
                                brush = new ImageBrush
                                    (BitmapFrame.Create(File.OpenRead(folder + "mine.bmp")));
                                break;
                            default:
                                break;
                        }

                        drawingContext.DrawRectangle(brush
                                    , new Pen(Brushes.Black, 0),
                                    new Rect(j * rectWidth, i * rectHeight, rectWidth, rectHeight)
                                    );
                    }
                }
            }
        }
    }
}
