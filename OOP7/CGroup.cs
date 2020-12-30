﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace OOP7
{
    class CGroup: Object
    {
        protected int maxcount;
        protected int count;
        public Object[] _object;

        public CGroup()
        {
            id = ID;
            ++ID;
            code = 'G';
            createShape();
        }

        public CGroup(int maxcount)
        {
            id = ID;
            ++ID;
            code = 'G';
            this.maxcount = maxcount;
            count = 0;
            _object = new Object[maxcount];
            for (int i = 0; i < maxcount; i++)
                _object[i] = null;
              
        }
        ~CGroup()
        {
            for (int i = 0; i < maxcount; i++)
                _object[i] = null;
            _object = null;
            --ID;
        }

        public bool addShape(Object obj)
        {
            if (count >= maxcount)
                return false;
            obj.setFlag(true);
            count++;
            _object[count - 1] = obj;
            return true;
        }
        public override void createShape() { }

        public override void DrawShape(Graphics G)
        {
            
                for (int i = 0; i < count; i++)
                {
                    if (_object[i] != null)
                        _object[i].DrawShape(G);
                }
            
        }

        public override void Move(int dx, int dy)
        {
            
            for (int i = 0; i < count; i++)
            {
                if(_object[i] != null)
                    _object[i].Move(dx, dy);
            }
            outOfBounds();
        }

        public override void ObjSize(int dx)
        {
            for (int i = 0; i < count; i++)
            {
                if(_object[i] != null)
                    _object[i].ObjSize(dx);
            }
            outOfBounds();
        }

        public override bool Popal(int x, int y)
        {
            for(int i = 0; i < count; i++)
            {
                if (_object[i]!=null && _object[i].Popal(x, y))
                {
                    return true;
                }   
            }
            return false;
        }

        public override void setSelect(bool a)
        {
            selected = a;
            for (int i = 0; i < count; i++)
            {
                if(_object[i] != null)
                    _object[i].setSelect(a);
            }
        }

        public override void ChangeColor(Color color)
        {
            for (int i = 0; i < count; i++)
                _object[i].ChangeColor(color);
        }
        
        public override void outOfBounds()
        {
            if (flag == false)
            {
                while (!CheckCircuit())
                {
                    RectangleF Rec = myPath.GetBounds();
                    PointF LeftTop = Rec.Location;
                    PointF RightTop = new PointF(Rec.Right, Rec.Top);
                    PointF LeftBottom = new PointF(Rec.Left, Rec.Bottom);
                    PointF RightBottom = new PointF(Rec.Right, Rec.Bottom);
                    if (!circuit.Contains(LeftTop) && !circuit.Contains(LeftBottom))
                        Move(1, 0);
                    if (!circuit.Contains(LeftTop) && !circuit.Contains(RightTop))
                        Move(0, 1);
                    if (!circuit.Contains(RightTop) && !circuit.Contains(RightBottom))
                        Move(-1, 0);
                    if (!circuit.Contains(LeftBottom) && !circuit.Contains(RightBottom))
                        Move(0, -1);
                }
            }
        }

        public override bool CheckCircuit()
        {
            for(int i = 0; i < count; i++)
            {
                if (_object[i]!=null && !_object[i].CheckCircuit())
                {
                    myPath = _object[i].getPath();
                    return false;
                }
            }
            return true;
        }
        public override void setRectangleF(RectangleF value)
        {
            circuit = value;
            for(int i =0; i < count; i++)
            {
                if(_object[i] != null)
                    _object[i].setRectangleF(value);
            }
        }

        public Object[] Ungroup()
        {
            for(int i = 0; i < count; i++)
            {
                if (_object[i] != null)
                {
                    _object[i].setFlag(false);
                }
            }
            
            return _object;
          
        }

        public override void save(StreamWriter writer)
        {
            writer.WriteLine(code);
            writer.WriteLine("maxcount" + maxcount.ToString());
            writer.WriteLine("count" + count.ToString());
            writer.WriteLine("Flag" + flag.ToString());
            for (int i = 0; i < count; ++i)
                _object[i].save(writer);
            writer.WriteLine();
        }

        public void loadGroup(StreamReader reader, ObjectFactory factory)
        {
            maxcount = int.Parse(extractInfo(reader.ReadLine()));
            count = int.Parse(extractInfo(reader.ReadLine()));
            flag = bool.Parse(extractInfo(reader.ReadLine()));
            for(int i = 0; i < count; i++)
                _object[i].loadShapes(reader, factory);
            for(int i =0; i < count; ++i)
                this.circuit = _object[i].getCircuit();
        }
    }
}
