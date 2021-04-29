using OxyPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollisionAtSea
{
    public class StepWidth
    {
        public float _slopeA { get; set; }
        public float _slopeB { get; set; }
        public float _stepWidthXA { get; set; }
        public float _stepWidthXB { get; set; }
        public float _stepWidthYA { get; set; }
        public float _stepWidthYB { get; set; }

        public StepWidth()
        {
            _slopeA = 0;
            _slopeB = 0;
            _stepWidthXA = 0;
            _stepWidthXB = 0;
            _stepWidthYA = 0;
            _stepWidthYB = 0;
        }

        private float CalculateSlope(DataPoint[] line)
        {
            double slope = Math.Round((line[0].Y - line[1].Y) / (line[0].X - line[1].X), 4);
            return (float)slope;
        }
        private float ConvertSlopeToAngle(float slope)
        {
            float angle = (float)Math.Round(Math.Atan(slope), 4);
            return angle;
        }

        private float CalculateStepWidthX(float speed, float angle)
        {
            float x = (float)Math.Round(speed * Math.Cos(angle),4);
            return x;
        }
        private float CalculateStepWidthY(float speed, float angle)
        {
            float y = (float)Math.Round(speed * Math.Sin(angle),4);
            return y;
        }

        public void CalculateStepWidth(DataPoint[] line1, DataPoint[] line2, int point, int nextPoint, DataGridView dataGridView)
        {
            _slopeA = CalculateSlope(line1);
            _slopeB = CalculateSlope(line2);

            float angleA = ConvertSlopeToAngle(_slopeA);
            float angleB = ConvertSlopeToAngle(_slopeB);

            _stepWidthXA = CalculateStepWidthX(Convert.ToSingle(dataGridView[0, point].Value), angleA);
            _stepWidthXB = CalculateStepWidthX(Convert.ToSingle(dataGridView[0, nextPoint].Value), angleB);

            _stepWidthYA = CalculateStepWidthY(Convert.ToSingle(dataGridView[0, point].Value), angleA);
            _stepWidthYB = CalculateStepWidthY(Convert.ToSingle(dataGridView[0, nextPoint].Value), angleB);

            if (line1[0].X > line1[1].X)
            {
                _stepWidthXA *= -1;
                _stepWidthYA *= -1;
            }
            if (line2[0].X > line2[1].X)
            {
                _stepWidthXB *= -1;
                _stepWidthYB *= -1;
            }
            // was ist wenn z.b. line1[0].X == line1[1].X
            if (line1[0].X == line1[1].X) 
            {
                //if (line1[0].Y < line1[1].Y)
                    _stepWidthYA *= -1;
            }
            if (line2[0].X == line2[1].X)
            {
                //if (line2[0].Y < line2[1].Y)
                    _stepWidthYB *= -1;
            }
        }
    }
}
