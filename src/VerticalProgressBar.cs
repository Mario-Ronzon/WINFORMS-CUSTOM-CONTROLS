using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
public class VerticalProgressBar : PictureBox
{
    public VerticalProgressBar()
    {
        _minimum = 0;
        _maximum = 100;
        _value = 20;
        _rounded = true;
        _innerDistance = 0;
        _fillColor = Color.DodgerBlue;
        _unfilledColor = Color.Gainsboro;
        Width = 20;
        Height = 100;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        float stepSize = (float)(Height - Width) / Maximum;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using (var path = new GraphicsPath())
        {
            sbyte distance = 0;
            if (_innerDistance < 0) distance = _innerDistance;
            path.AddLine(Width / 2, Width / 2, Width / 2, Height - (Width / 2));
            Pen pen = new Pen(_unfilledColor, Width / 2 + distance)
            {
                EndCap = _rounded == true ? LineCap.Round : LineCap.Flat,
                StartCap = _rounded == true ? LineCap.Round : LineCap.Flat
            };
            e.Graphics.DrawPath(pen, path);
        }
        using (var path = new GraphicsPath())
        {
            sbyte distance = 0;
            if (_innerDistance > 0) distance = _innerDistance;
            path.AddLine(
                Width / 2,
                Height - (stepSize * _value) - (Width / 2), 
                Width / 2,
                Height - (Width / 2));
            Pen pen = new Pen(_fillColor, Width / 2 - distance)
            {
                EndCap = _rounded == true ? LineCap.Round : LineCap.Flat,
                StartCap = _rounded == true ? LineCap.Round : LineCap.Flat
            };
            e.Graphics.DrawPath(pen, path);
        }
    }
    public int Maximum
    { 
        get 
        {
            return _maximum;
        } 
        set 
        {
            if (value != _maximum)
            {
                if (value > _minimum)
                {
                    _maximum = value;
                    if (_value > Maximum)
                        _value = _maximum;
                    Invalidate();
                }
            }
        } 
    }
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            if(value != _value)
            {
                _value = value;

                if (_value < _minimum)
                    _value = _minimum;
                else if (_value > _maximum)
                    _value = _maximum;

                Invalidate();
            }
        }
    }

    public bool Rounded
    {
        get
        {
            return _rounded;
        }
        set
        {
            if (value != _rounded)
            {
                _rounded = value;
                Invalidate();
            }
        }
    }

    public sbyte InnerDistance
    {
        get
        {
            return _innerDistance;
        }
        set
        {
            if(value != _innerDistance)
            {
                if (Math.Abs(value) < Width / 4 || Math.Abs(value) < Height / 4)
                {
                    _innerDistance = value;
                    Invalidate();
                }
            }
        }
    }

    public Color FillColor
    {
        get 
        {
            return _fillColor;
        }
        set 
        {
            if(value != _fillColor)
            {
                _fillColor = value;
                Invalidate();
            }
        }
    }
    public Color UnfilledColor
    {
        get
        {
            return _unfilledColor;
        }
        set
        {
            if (value != _unfilledColor)
            {
                _unfilledColor = value;
                Invalidate();
            }
        }
    }

    private readonly int _minimum;
    private int _maximum;
    private int _value;
    private bool _rounded;
    private sbyte _innerDistance;
    private Color _fillColor;
    private Color _unfilledColor;
}
