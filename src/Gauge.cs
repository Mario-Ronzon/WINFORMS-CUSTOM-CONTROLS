using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
public class Gauge : PictureBox
{
    public Gauge()
    {
        _minimum = 0;
        _maximum = 100;
        _value = 50;
        _fillThickness = 20;
        _unfilledThickness = 20;
        _fillColor = Color.DodgerBlue;
        _unfilledColor = Color.Gainsboro;
        Width = 100;
        Height = 100;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        float angleStep = 270f / _maximum;
        byte maxThickness = _unfilledThickness > _fillThickness ? _unfilledThickness : FillThickness;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        //UNFILLED---------------------
        using (var path = new GraphicsPath())
        {
            path.AddArc(
                maxThickness / 2,
                maxThickness / 2,
                Width - 2 - maxThickness,
                Height - 2 - maxThickness,
                135, 270);//external
            Pen pen = new Pen(_unfilledColor, _unfilledThickness / 2)
            {
                EndCap = LineCap.Round,
                StartCap = LineCap.Round
            };
            e.Graphics.DrawPath(pen, path);
        }
        //FILLED---------------------
        using (var path = new GraphicsPath())
        {
            path.AddArc(
                maxThickness / 2,
                maxThickness / 2,
                Width - 2 - maxThickness,
                Height - 2 - maxThickness,
                angleStep * _value + 135, (angleStep * _value) * -1);

            Pen pen = new Pen(_fillColor, _fillThickness / 2)
            {
                EndCap = LineCap.Round,
                StartCap = LineCap.Round
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
            if (value != _value)
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

    public byte FillThickness
    {
        get
        {
            return _fillThickness;
        }
        set
        {
            if (value != _fillThickness && value > 9)
            {
                int max = Width > Height ? Height : Width;
                if (value > (max * 0.5f))
                {
                    _fillThickness = (byte)(max *0.5f);
                }
                else
                {
                    _fillThickness = value;
                }
                Invalidate();
            }
        }
    }

    public byte UnfilledThickness
    {
        get
        {
            return _unfilledThickness;
        }
        set
        {
            if (value != _unfilledThickness && value > 9)
            {
                int max = Width > Height ? Height : Width;
                if (value > (max *0.5f))
                {
                    _unfilledThickness = (byte)(max * 0.5f);
                }
                else
                {
                    _unfilledThickness = value;
                }
                Invalidate();
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
            if (value != _fillColor)
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
    private byte _fillThickness;
    private byte _unfilledThickness;
    private Color _fillColor;
    private Color _unfilledColor;
}
