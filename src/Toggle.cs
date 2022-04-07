using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


public class Toggle : CheckBox
{
    public Toggle()
    {
        _checkedOvalColor = Color.White;
        _unchekedOvalColor = Color.White;
        _checkedRecColor = Color.DodgerBlue;
        _uncheckedRecColor = Color.Gainsboro;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        Padding = new Padding(4);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        this.OnPaintBackground(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using (var path = new GraphicsPath())
        {
            var d = Padding.All;
            var r = this.Height - 2 * d;
            path.AddArc(d, d, r, r, 90, 180);
            path.AddArc(this.Width - r - d, d, r, r, -90, 180);
            path.CloseFigure();
            e.Graphics.FillPath(Checked ? new SolidBrush(CheckedRecColor) : new SolidBrush(UncheckedRecColor), path);
            r = Height - 1;
            var rect = Checked ? new Rectangle(Width - r - 1, 0, r, r) : new Rectangle(0, 0, r, r);
            e.Graphics.FillEllipse(Checked ? new SolidBrush(CheckedOvalColor) : new SolidBrush(UnchekedOvalColor), rect);
        }
    }

    //custom properties
    public Color CheckedOvalColor
    {
        get 
        {
            return _checkedOvalColor;
        }
        set 
        { 
            if(value != _checkedOvalColor)
            {
                _checkedOvalColor = value;
                Invalidate();
            }
        } 
    }
    public Color UnchekedOvalColor
    {
        get
        {
            return _unchekedOvalColor;
        }
        set
        {
            if (value != _unchekedOvalColor)
            {
                _unchekedOvalColor = value;
                Invalidate();
            }
        }
    }
    public Color CheckedRecColor
    {
        get
        {
            return _checkedRecColor;
        }
        set
        {
            if (value != _checkedRecColor)
            {
                _checkedRecColor = value;
                Invalidate();
            }
        }
    }
    public Color UncheckedRecColor
    {
        get
        {
            return _uncheckedRecColor;
        }
        set
        {
            if (value != _uncheckedRecColor)
            {
                _uncheckedRecColor = value;
                Invalidate();
            }
        }
    }


    private Color _checkedOvalColor;
    private Color _unchekedOvalColor;
    private Color _checkedRecColor;
    private Color _uncheckedRecColor;
}