﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP7
{
    class CRectangle: Object
    {
        public CRectangle(int x, int y, RectangleF circuit, Color color)
        {
            this.x = x;
            this.y = y;
            this.circuit = circuit;
            this.color = color;
            createShape();
            outOfBounds();

        }
        public override void createShape()
        {
            myPath.Reset();
            Rectangle PathRec = new Rectangle(x - OValue / 2, y - OValue / 2, OValue, OValue);
            myPath.AddRectangle(PathRec);
        }
    }
}