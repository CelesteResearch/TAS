using System;
using System.Collections.Generic;
using System.Numerics;
using Celeste.Core.Mathematics;
using Celeste.Physics.Collision.Shapes;

namespace Celeste.Mods.Tas.Hitboxes
{
    internal static class SourceTasGridLineCollision
    {
        public static List<Tuple<Vector2, bool>> GetCheckedTilesInLineCollision(GridColliderGeometry grid, Vector2 from,
            Vector2 to)
        {
            from -= grid.AbsolutePosition;
            to -= grid.AbsolutePosition;
            from = Vector2ComponentDivision.Divide(from, new Vector2(grid.CellWidth, grid.CellHeight));
            to = Vector2ComponentDivision.Divide(to, new Vector2(grid.CellWidth, grid.CellHeight));

            bool needsSwapXY = Math.Abs(to.Y - from.Y) > Math.Abs(to.X - from.X);
            if (needsSwapXY)
            {
                float temp = from.X;
                from.X = from.Y;
                from.Y = temp;
                temp = to.X;
                to.X = to.Y;
                to.Y = temp;
            }

            if (from.X > to.X)
            {
                Vector2 temp = from;
                from = to;
                to = temp;
            }

            List<Tuple<Vector2, bool>> positions = new();

            float offset = 0f;
            int y = (int)from.Y;
            for (int i = (int)from.X; i <= (int)to.X; i++)
            {
                Vector2 position = needsSwapXY ? new Vector2(y, i) : new Vector2(i, y);
                Vector2 absolutePosition =
                    position * new Vector2(grid.CellWidth, grid.CellHeight) + grid.AbsolutePosition;
                bool hasTile = grid[(int)position.X, (int)position.Y];
                positions.Add(new(absolutePosition, hasTile));

                offset += Math.Abs(to.Y - from.Y) / (to.X - from.X);
                if (offset >= 0.5f)
                {
                    y += from.Y < to.Y ? 1 : -1;
                    offset -= 1f;
                }
            }

            return positions;
        }
    }
}
