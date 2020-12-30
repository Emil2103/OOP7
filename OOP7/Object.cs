using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OOP7
{
    public  abstract class Object
    {
        public int x;
        public int y;
        protected bool flag = false;
        protected int OValue = 50;
        protected RectangleF circuit;
        protected GraphicsPath myPath = new GraphicsPath();
        protected Color color;
        Pen redpen = new Pen(Color.Red);
        protected bool selected = false;

        public virtual void DrawShape(Graphics G)
        {
            Pen pen = new Pen(color, 2);
            redpen.Width = 2;
            G.DrawPath(selected ? redpen : pen, myPath);
        }

        public virtual bool Popal(int x, int y)
        {
            return myPath.IsVisible(x, y);
        }

        public virtual void ObjSize(int dx)
        {
            OValue = OValue + dx;
            createShape();
            if(!flag)
                outOfBounds();
        }

        public virtual void Move(int dx, int dy)
        {
            x = x + dx;
            y = y + dy;
            createShape();
            if(!flag)
                outOfBounds();
        }

        public abstract void createShape();

        public virtual void outOfBounds()
        {
            while (!CheckCircuit())
            {
                RectangleF ShapeCircuit = myPath.GetBounds();
                PointF LeftTop = ShapeCircuit.Location;
                PointF RightTop = new PointF(ShapeCircuit.Right, ShapeCircuit.Top);
                PointF LeftBottom = new PointF(ShapeCircuit.Left, ShapeCircuit.Bottom);
                PointF RightBottom = new PointF(ShapeCircuit.Right, ShapeCircuit.Bottom);
                if (!circuit.Contains(LeftTop) && !circuit.Contains(LeftBottom))
                    ++x;
                if (!circuit.Contains(LeftTop) && !circuit.Contains(RightTop))
                    ++y;
                if (!circuit.Contains(RightTop) && !circuit.Contains(RightBottom))
                    --x;
                if (!circuit.Contains(LeftBottom) && !circuit.Contains(RightBottom))
                    --y;
                createShape();
            }
        }

        public virtual void setRectangleF(RectangleF value)
        {
            circuit = value;
        }

        public virtual void ChangeColor(Color color)
        {
            this.color = color;
        }

        public virtual bool getSelect()
        {
            return selected;
        }

        public virtual void setSelect(bool value)
        {
            selected = value;
        }

        public virtual void setFlag(bool value)
        {
            flag = value;
        }

        

        
        public virtual bool CheckCircuit()
        {
            return circuit.Contains(myPath.GetBounds());
        }

        public RectangleF getCircuit()
        {
            return circuit;
        }
        public GraphicsPath getPath()
        {
            return myPath;
        }

        public virtual void createGroupRectF() { }
       
    }
}
