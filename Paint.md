Creating a C# application using GDI+ to draw shapes with rubber banding (interactive drawing) and selected colors involves using Windows Forms. Below is a complete example demonstrating how to draw a line, rectangle, and circle with rubber banding and color selection.

### Step-by-Step Guide

1. **Create a Windows Forms Application:**
   - Open Visual Studio.
   - Create a new "Windows Forms App" project.

2. **Add UI Elements:**
   - A `ComboBox` for selecting colors.
   - `RadioButtons` for selecting shapes (line, rectangle, circle).
   - A `Panel` or `PictureBox` for drawing.

3. **Write the Code:**

Here's the complete code to achieve this:

**Form1.cs:**

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingApp
{
    public partial class Form1 : Form
    {
        private enum Shape
        {
            None,
            Line,
            Rectangle,
            Circle
        }

        private Shape selectedShape = Shape.None;
        private Color selectedColor = Color.Black;
        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize color selection
            comboBoxColors.Items.AddRange(Enum.GetNames(typeof(KnownColor)));
            comboBoxColors.SelectedIndex = 0;

            // Initialize shape selection
            radioButtonLine.Checked = true;
        }

        private void comboBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedColor = Color.FromName(comboBoxColors.SelectedItem.ToString());
        }

        private void radioButtonLine_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLine.Checked)
            {
                selectedShape = Shape.Line;
            }
        }

        private void radioButtonRectangle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRectangle.Checked)
            {
                selectedShape = Shape.Rectangle;
            }
        }

        private void radioButtonCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCircle.Checked)
            {
                selectedShape = Shape.Circle;
            }
        }

        private void panelDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isDrawing = true;
            }
        }

        private void panelDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.Location;
                panelDrawing.Invalidate(); // Trigger the Paint event
            }
        }

        private void panelDrawing_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                endPoint = e.Location;
                isDrawing = false;
                panelDrawing.Invalidate(); // Trigger the Paint event
            }
        }

        private void panelDrawing_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing || startPoint != endPoint)
            {
                using (Pen pen = new Pen(selectedColor))
                {
                    Rectangle rect = GetRectangle(startPoint, endPoint);
                    switch (selectedShape)
                    {
                        case Shape.Line:
                            e.Graphics.DrawLine(pen, startPoint, endPoint);
                            break;
                        case Shape.Rectangle:
                            e.Graphics.DrawRectangle(pen, rect);
                            break;
                        case Shape.Circle:
                            e.Graphics.DrawEllipse(pen, rect);
                            break;
                    }
                }
            }
        }

        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y)
            );
        }
    }
}
```

**Form1.Designer.cs:**

Add the necessary controls to the form:

```csharp
namespace DrawingApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBoxColors;
        private RadioButton radioButtonLine;
        private RadioButton radioButtonRectangle;
        private RadioButton radioButtonCircle;
        private Panel panelDrawing;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxColors = new ComboBox();
            this.radioButtonLine = new RadioButton();
            this.radioButtonRectangle = new RadioButton();
            this.radioButtonCircle = new RadioButton();
            this.panelDrawing = new Panel();

            // 
            // comboBoxColors
            // 
            this.comboBoxColors.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxColors.FormattingEnabled = true;
            this.comboBoxColors.Location = new Point(12, 12);
            this.comboBoxColors.Name = "comboBoxColors";
            this.comboBoxColors.Size = new Size(121, 21);
            this.comboBoxColors.TabIndex = 0;
            this.comboBoxColors.SelectedIndexChanged += new EventHandler(this.comboBoxColors_SelectedIndexChanged);

            // 
            // radioButtonLine
            // 
            this.radioButtonLine.AutoSize = true;
            this.radioButtonLine.Location = new Point(12, 39);
            this.radioButtonLine.Name = "radioButtonLine";
            this.radioButtonLine.Size = new Size(45, 17);
            this.radioButtonLine.TabIndex = 1;
            this.radioButtonLine.TabStop = true;
            this.radioButtonLine.Text = "Line";
            this.radioButtonLine.UseVisualStyleBackColor = true;
            this.radioButtonLine.CheckedChanged += new EventHandler(this.radioButtonLine_CheckedChanged);

            // 
            // radioButtonRectangle
            // 
            this.radioButtonRectangle.AutoSize = true;
            this.radioButtonRectangle.Location = new Point(12, 62);
            this.radioButtonRectangle.Name = "radioButtonRectangle";
            this.radioButtonRectangle.Size = new Size(79, 17);
            this.radioButtonRectangle.TabIndex = 2;
            this.radioButtonRectangle.TabStop = true;
            this.radioButtonRectangle.Text = "Rectangle";
            this.radioButtonRectangle.UseVisualStyleBackColor = true;
            this.radioButtonRectangle.CheckedChanged += new EventHandler(this.radioButtonRectangle_CheckedChanged);

            // 
            // radioButtonCircle
            // 
            this.radioButtonCircle.AutoSize = true;
            this.radioButtonCircle.Location = new Point(12, 85);
            this.radioButtonCircle.Name = "radioButtonCircle";
            this.radioButtonCircle.Size = new Size(51, 17);
            this.radioButtonCircle.TabIndex = 3;
            this.radioButtonCircle.TabStop = true;
            this.radioButtonCircle.Text = "Circle";
            this.radioButtonCircle.UseVisualStyleBackColor = true;
            this.radioButtonCircle.CheckedChanged += new EventHandler(this.radioButtonCircle_CheckedChanged);

            // 
            // panelDrawing
            // 
            this.panelDrawing.BorderStyle = BorderStyle.FixedSingle;
            this.panelDrawing.Location = new Point(139, 12);
            this.panelDrawing.Name = "panelDrawing";
            this.panelDrawing.Size = new Size(600, 400);
            this.panelDrawing.TabIndex = 4;
            this.panelDrawing.Paint += new PaintEventHandler(this.panelDrawing_Paint);
            this.panelDrawing.MouseDown += new MouseEventHandler(this.panelDrawing_MouseDown);
            this.panelDrawing.MouseMove += new MouseEventHandler(this.panelDrawing_MouseMove);
            this.panelDrawing.MouseUp += new MouseEventHandler(this.panelDrawing_MouseUp);

            // 
            // Form1
            // 
            this.ClientSize = new Size(751, 424);
            this.Controls.Add(this.panelDrawing);
            this.Controls.Add(this.radioButtonCircle);
            this.Controls.Add(this.radioButtonRectangle);
            this.Controls.Add(this.radioButtonLine);
            this.Controls.Add(this.comboBoxColors);
            this.Name = "Form1";
            this.Text = "Drawing Application";
            this.Load += new EventHandler(this.Form1_Load);
        }
    }
}
```

### Explanation:

1. **Form Elements:**
   - `ComboBox` for color selection.
   - `RadioButton` controls for selecting the shape.
   - `Panel` for drawing shapes.

2. **Event Handlers:**
   - `comboBoxColors_SelectedIndexChanged`: Updates the selected color.
   - `radioButtonLine_CheckedChanged`, `radioButtonRectangle_CheckedChanged`, `radioButtonCircle_CheckedChanged`: Updates the selected shape.
   - `panelDrawing_MouseDown`, `panelDrawing_MouseMove`, `panelDrawing_MouseUp`: Handles mouse events for drawing.
   - `panelDrawing_Paint`: Paints the shape based on the current selection and mouse position.

3. **Drawing Logic:**
   - The shapes are drawn based on the current mouse position and selected shape. Delimiters `<SOF>` and `<EOF>` are not used in this specific context but are implied in the drawing operation by the start and end of the mouse drag.

### Running the Application:

1. **Build and Run:**
   - Press `F5` or click "Start" in Visual Studio to build and run the application.

2. **Interact with the UI:**
   - Select a color from the dropdown.
   - Choose a shape to draw using the radio buttons.
   - Click and drag on the `Panel` to draw the selected shape.

This application provides a simple yet effective way to draw shapes with interactive feedback using GDI+ in a Windows Forms application.
