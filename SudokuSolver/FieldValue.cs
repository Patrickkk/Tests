using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    public class FieldValue
    {
        public int KnownValue { get; set; } = 0;
        public List<int> PossibleValues { get; set; } = null;
        public int X { get; set; }

        public int Y { get; set; }

        public void SetKnownValue(int value)
        {
            this.KnownValue = value;
            this.PossibleValues = new List<int> { value };
        }

        //private void Match(Action<int> knownValue, Action<PossibleValues> possibleValues)
        //{
        //    if (this.KnownValue > 0)
        //    {
        //        knownValue(this.KnownValue);
        //    }
        //    else if (this.PossibleValues != null)
        //    {
        //        possibleValues(this.PossibleValues);
        //    }
        //    else
        //    {
        //        throw new Exception("invalid value");
        //    }
        //}
    }

    public class PlayingField
    {
        public HashSet<FieldValue> values = new HashSet<FieldValue>();

        public PlayingField()
        {
            for (int x = 1; x < 10; x++)
            {
                for (int y = 1; y < 10; y++)
                {
                    values.Add(new FieldValue { X = x, Y = y, PossibleValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 } });
                }
            }
        }

        public static IEnumerable<FieldValue> FieldValuesInBlock(PlayingField playingField, int x, int y, int blockXOffset, int blockYOffset)
        {
            var xdiv = (x + blockXOffset - 1) / 3;
            var ydiv = (y + blockYOffset - 1) / 3;
            return playingField.values.Where(v =>
                (v.X - 1) / 3 == xdiv &&
                (v.Y - 1) / 3 == ydiv
            );
        }

        public static IEnumerable<int> PossibleValuesAtPosition(PlayingField field, int x, int y)
        {
            var currentField = field.FieldValue(x, y);
            if (currentField.KnownValue > 0)
            {
                return new int[] { currentField.KnownValue };
            }
            var possibleValues = currentField.PossibleValues;
            return possibleValues
                .Except(ValuesInRow(field, y))
                .Except(ValuesInCollum(field, x))
                .Except(ValuesInBlock(field, x, y))
                .Except(ValuesThatMustBeInRow(field, x, y))
                .Except(ValuesThatMustBeInCollum(field, x, y));
        }

        public static PlayingField Solve(PlayingField puzzle)
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int x = 1; x < 10; x++)
                {
                    for (int y = 1; y < 10; y++)
                    {
                        if (puzzle.FieldValue(x, y).KnownValue == 0)
                        {
                            var possibleValues = PlayingField.PossibleValuesAtPosition(puzzle, x, y);
                            if (possibleValues.Count() == 0)
                            {
                                throw new Exception("Zero options left");
                            }
                            if (possibleValues.Count() == 1)
                            {
                                puzzle.FieldValue(x, y).SetKnownValue(possibleValues.ElementAt(0));
                            }
                        }
                    }
                }
                if (NoRemainingUnknownValues(puzzle))
                {
                    return puzzle;
                }
            }

            return puzzle;
        }

        public static IEnumerable<int> ValuesInBlock(PlayingField playingField, int x, int y)
        {
            return ValuesInBlock(playingField, x, y, 0, 0);
        }

        public static IEnumerable<int> ValuesInBlock(PlayingField playingField, int x, int y, int blockXOffset, int blockYOffset)
        {
            return FieldValuesInBlock(playingField, x, y, blockXOffset, blockYOffset).Select(f => f.KnownValue);
        }

        public static IEnumerable<int> ValuesInCollum(PlayingField playingField, int x)
        {
            return playingField.values.Where(v => v.X == x && v.KnownValue > 0).Select(f => f.KnownValue);
        }

        public static IEnumerable<int> ValuesInRow(PlayingField playingField, int y)
        {
            return playingField.values.Where(v => v.Y == y && v.KnownValue > 0).Select(f => f.KnownValue);
        }

        public static IEnumerable<int> ValuesThatMustBeInCollum(PlayingField playingField, int x, int y)
        {
            if (y <= 3)
            {
                return ValuesThatMustBeInCollumBlock(playingField, x, 2)
                    .Concat(ValuesThatMustBeInCollumBlock(playingField, x, 3));
            }
            if (y > 3 && x <= 6)
            {
                return ValuesThatMustBeInCollumBlock(playingField, x, 1)
                     .Concat(ValuesThatMustBeInCollumBlock(playingField, x, 3));
            }
            if (y > 6)
            {
                return ValuesThatMustBeInCollumBlock(playingField, x, 1)
                    .Concat(ValuesThatMustBeInCollumBlock(playingField, x, 2));
            }
            throw new Exception(":(");
        }

        public static IEnumerable<int> ValuesThatMustBeInRow(PlayingField playingField, int x, int y)
        {
            if (x <= 3)
            {
                return ValuesThatMustBeInRowBlock(playingField, y, 2)
                    .Concat(ValuesThatMustBeInRowBlock(playingField, y, 3));
            }
            if (x > 3 && x <= 6)
            {
                return ValuesThatMustBeInRowBlock(playingField, y, 1)
                     .Concat(ValuesThatMustBeInRowBlock(playingField, y, 3));
            }
            if (x > 6)
            {
                return ValuesThatMustBeInRowBlock(playingField, y, 1)
                    .Concat(ValuesThatMustBeInRowBlock(playingField, y, 2));
            }
            throw new Exception(":(");
        }

        public FieldValue FieldValue(int x, int y)
        {
            return values.Single(f => f.X == x && f.Y == y);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int y = 1; y < 10; y++)
            {
                for (int x = 1; x < 10; x++)
                {
                    builder.Append(this.FieldValue(x, y).KnownValue);
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        private static bool NoRemainingUnknownValues(PlayingField puzzle)
        {
            return !puzzle.values.Any(x => x.KnownValue == 0);
        }

        private static IEnumerable<int> ValuesThatMustBeInCollumBlock(PlayingField playingField, int x, int block)
        {
            var valuesInFirstBlock = FieldValuesInBlock(playingField, block * 3, x, 0, 0);
            var valuesInSameX = valuesInFirstBlock.Where(field => field.X == x).SelectMany(f => f.PossibleValues);
            var valuesNotInSameX = valuesInFirstBlock.Where(field => field.X != x).SelectMany(f => f.PossibleValues);
            return valuesInSameX.Except(valuesNotInSameX);
        }

        private static IEnumerable<int> ValuesThatMustBeInRowBlock(PlayingField playingField, int y, int block)
        {
            var valuesInFirstBlock = FieldValuesInBlock(playingField, block * 3, y, 0, 0);
            var valuesInSameY = valuesInFirstBlock.Where(field => field.Y == y).SelectMany(x => x.PossibleValues);
            var valuesNotInSameY = valuesInFirstBlock.Where(field => field.Y != y).SelectMany(x => x.PossibleValues);
            return valuesInSameY.Except(valuesNotInSameY);
        }
    }
}