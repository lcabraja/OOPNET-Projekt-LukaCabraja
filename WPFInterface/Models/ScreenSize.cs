using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;

namespace WPFInterface
{
    public class ScreenSize
    {
        public int ChosenSize { get; set; }
        public Size GetSize()
        {

            switch(ChosenSize)
            {
                case 10:
                    return new Size(1280, 720);
                    break;
                case 20:
                    return new Size(854, 480);
                    break;
                case 30:
                    return new Size(640, 360);
                default:
                    return new Size(1920, 1080);
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
