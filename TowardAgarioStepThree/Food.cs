using System;
using System.Text.Json.Serialization;

namespace TowardAgarioStepThree
{
	public class Food
	{
        public long X { get; private set; }

        public long Y { get; private set; }

        public int ARGBColor { get; private set; }

        public long ID { get; private set; }

        public long Mass { get; private set; }


		public Food()
		{
            this.ID = 0;
            X = 0;
            Y = 0;
            Mass = 0;
            ARGBColor = 0;
        }

		[JsonConstructor]
        public Food(long X, long Y, int ARGBColor, long ID, long Mass)
		{
            this.X = X;
            this.Y = Y;
            this.ARGBColor = ARGBColor;
            this.ID = ID;
			this.Mass = Mass;
		}
	}
}

