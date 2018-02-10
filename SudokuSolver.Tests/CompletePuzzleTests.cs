using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace SudokuSolver.Tests
{
    public class CompletePuzzleTests
    {
        [Fact]
        public void AdvancedPuzzle()
        {
            PuzzleShouldHaveExpectedSolution("Tests/AdvancedPuzzle.txt");
        }

        [Fact]
        public void Puzzle1()
        {
            PuzzleShouldHaveExpectedSolution("Tests/puzzleWithSolution.txt");
        }

        private static PlayingField LoadPuzzle(string path)
        {
            var lines = File.ReadAllLines(path);
            PlayingField field = new PlayingField();

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    var value = int.Parse(lines[y][x].ToString());
                    if (value != 0)
                    {
                        field.FieldValue(x + 1, y + 1).SetKnownValue(value);
                    }
                }
            }

            return field;
        }

        private static PlayingField LoadSolution(string path)
        {
            var lines = File.ReadAllLines(path);
            PlayingField field = new PlayingField();

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    var value = int.Parse(lines[y + 10][x].ToString());
                    if (value != 0)
                    {
                        field.FieldValue(x + 1, y + 1).SetKnownValue(value);
                    }
                }
            }

            return field;
        }

        private static void PuzzleShouldHaveExpectedSolution(string path)
        {
            var puzzle = LoadPuzzle(path);
            var solution = LoadSolution(path);
            var solved = PlayingField.Solve(puzzle);

            solved.ToString().Should().BeEquivalentTo(solution.ToString());
        }
    }
}