using System;
using System.Numerics;
using StudioCommunication.Util;
using Celeste.Core.Mathematics.LinearAlgebra;

namespace Celeste.Mods.Tas.Information
{
// ReSharper disable once StructCanBeMadeReadOnly
    public struct Vector2Double
    {
        public Vector2 Position;
        public Vector2 PositionRemainder;
        public bool SubpixelRounding;

        public double X
        {
            get => (double)Position.X + PositionRemainder.X;
            set
            {
                Position.X = (int)Math.Round(value);
                PositionRemainder.X = (float)(value - Position.X);
            }
        }

        public double Y
        {
            get => (double)Position.Y + PositionRemainder.Y;
            set
            {
                Position.Y = (int)Math.Round(value);
                PositionRemainder.Y = (float)(value - Position.Y);
            }
        }

        public Vector2Double(Vector2 position, Vector2 positionRemainder, bool subpixelRounding)
        {
            Position = position;
            PositionRemainder = positionRemainder;
            SubpixelRounding = subpixelRounding;
        }

        public override bool Equals(object? obj) =>
            obj is Vector2Double other && Position == other.Position && PositionRemainder == other.PositionRemainder;

        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() => "{X:" + X + " Y:" + Y + "}";

        public string ToSimpleString(int decimals)
        {
            if (SubpixelRounding)
            {
                return ToExactPositionString(decimals);
            }
            else
            {
                return $"{X.ToFormattedString(decimals)}, {Y.ToFormattedString(decimals)}";
            }
        }

        private string ToExactPositionString(int decimals)
        {
            string RoundPosition(double exactPosition, float position, float remainder)
            {
                if (decimals == 0)
                {
                    return exactPosition.ToFormattedString(decimals);
                }

                double round = Math.Round(exactPosition, decimals);

                switch (Math.Abs(remainder))
                {
                    case 0.5f:
                        // don't show subsequent zeros when subpixel is exactly equal to 0.5
                        return round.ToString("F1");
                    case < 0.5f:
                        // make 0.495 round away from 0.50
                        int diffX = (int)position - (int)Math.Round(round, MidpointRounding.AwayFromZero);
                        if (diffX != 0)
                        {
                            round += diffX * Math.Pow(10, -decimals);
                        }

                        break;
                }

                return round.ToFormattedString(decimals);
            }

            string resultX = RoundPosition(X, Position.X, PositionRemainder.X);
            string resultY = RoundPosition(Y, Position.Y, PositionRemainder.Y);
            return $"{resultX}, {resultY}";
        }

        public double Length() => Math.Sqrt(X * X + Y * Y);

        public double Angle()
        {
            double angle = 180 / Math.PI * Math.Atan2(Y, X);
            if (angle < -90.01)
            {
                return 450 + angle;
            }
            else
            {
                return 90 + angle;
            }
        }

        public static bool operator ==(Vector2Double value1, Vector2Double value2) =>
            value1.X == value2.X && value1.Y == value2.Y;

        public static bool operator !=(Vector2Double value1, Vector2Double value2) => !(value1 == value2);

        public static Vector2Double operator +(Vector2Double value1, Vector2Double value2)
        {
            return new(SourceVector2Math.Add(value1.Position, value2.Position),
                SourceVector2Math.Add(value1.PositionRemainder, value2.PositionRemainder), value1.SubpixelRounding);
        }

        public static Vector2Double operator -(Vector2Double value1, Vector2Double value2)
        {
            return new(SourceVector2Math.Subtract(value1.Position, value2.Position),
                SourceVector2Math.Subtract(value1.PositionRemainder, value2.PositionRemainder),
                value1.SubpixelRounding);
        }

        public static Vector2Double operator *(Vector2Double value, float scaleFactor)
        {
            return new(SourceVector2Math.Multiply(value.Position, scaleFactor),
                SourceVector2Math.Multiply(value.PositionRemainder, scaleFactor), value.SubpixelRounding);
        }

        public static Vector2Double operator /(Vector2Double value, float scaleFactor)
        {
            return value * (1 / scaleFactor);
        }
    }
}
