using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
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

                default:
                    return new Size(1280, 720);
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
