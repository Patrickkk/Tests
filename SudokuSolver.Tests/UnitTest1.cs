using FluentAssertions;
using System;
using Xunit;

namespace SudokuSolver.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void DefaultPlayingFieldShouldHAveAllPossibleValues()
        {
            var field = new PlayingField();
            var value = field.FieldValue(1, 1);
            value.PossibleValues.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [Fact]
        public void ValuesInBlockSHouldBeCorrect()
        {
            var field = new PlayingField();
            field.FieldValue(1, 1).SetKnownValue(1);
            field.FieldValue(1, 2).SetKnownValue(2);
            field.FieldValue(1, 3).SetKnownValue(3);
            field.FieldValue(2, 1).SetKnownValue(4);
            field.FieldValue(2, 2).SetKnownValue(5);
            field.FieldValue(2, 3).SetKnownValue(6);
            field.FieldValue(3, 1).SetKnownValue(7);
            field.FieldValue(3, 2).SetKnownValue(8);
            field.FieldValue(3, 3).SetKnownValue(9);

            var vlauesInBlock = PlayingField.ValuesInBlock(field, 2, 2);
            vlauesInBlock.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [Fact]
        public void ValuesThatMustBeInCollumn()
        {
            var field = new PlayingField();
            field.FieldValue(2, 7).SetKnownValue(1);
            field.FieldValue(2, 8).SetKnownValue(2);
            field.FieldValue(2, 9).SetKnownValue(3);
            field.FieldValue(3, 7).SetKnownValue(4);
            field.FieldValue(3, 8).SetKnownValue(5);
            field.FieldValue(3, 9).SetKnownValue(6);

            var requiredValuesInRow = PlayingField.ValuesThatMustBeInCollum(field, 6, 1);
            requiredValuesInRow.Should().BeEquivalentTo(7, 8, 9);
        }

        [Fact]
        public void ValuesThatMustBeInRow()
        {
            var field = new PlayingField();
            field.FieldValue(7, 2).SetKnownValue(1);
            field.FieldValue(8, 2).SetKnownValue(2);
            field.FieldValue(9, 2).SetKnownValue(3);
            field.FieldValue(7, 3).SetKnownValue(4);
            field.FieldValue(8, 3).SetKnownValue(5);
            field.FieldValue(9, 3).SetKnownValue(6);

            var requiredValuesInRow = PlayingField.ValuesThatMustBeInRow(field, 6, 1);
            requiredValuesInRow.Should().BeEquivalentTo(7, 8, 9);
        }

        [Fact]
        public void WhenCalculatingPossibleValuesInARowAllOtherValuesInCollumnShouldBeExcluded()
        {
            var field = new PlayingField();
            field.FieldValue(1, 2).SetKnownValue(1);
            field.FieldValue(1, 3).SetKnownValue(2);
            field.FieldValue(1, 4).SetKnownValue(3);
            field.FieldValue(1, 9).SetKnownValue(9);
            var possibleValues = PlayingField.PossibleValuesAtPosition(field, 1, 1);
            possibleValues.Should().BeEquivalentTo(4, 5, 6, 7, 8);
        }

        [Fact]
        public void WhenCalculatingPossibleValuesInARowAllOtherValuesInRowShouldBeExcluded()
        {
            var field = new PlayingField();
            field.FieldValue(2, 1).SetKnownValue(1);
            field.FieldValue(3, 1).SetKnownValue(2);
            field.FieldValue(4, 1).SetKnownValue(3);
            field.FieldValue(9, 1).SetKnownValue(9);
            var possibleValues = PlayingField.PossibleValuesAtPosition(field, 1, 1);
            possibleValues.Should().BeEquivalentTo(4, 5, 6, 7, 8);
        }

        [Fact]
        public void WhenCalculatingPossibleValuesValuesInBlockShouldBeExcluded()
        {
            var field = new PlayingField();
            field.FieldValue(1, 1).SetKnownValue(1);
            field.FieldValue(3, 1).SetKnownValue(2);
            field.FieldValue(1, 3).SetKnownValue(3);
            field.FieldValue(3, 3).SetKnownValue(4);
            // other irrelevant fields
            field.FieldValue(9, 9).SetKnownValue(5);
            field.FieldValue(5, 5).SetKnownValue(9);
            var possibleValues = PlayingField.PossibleValuesAtPosition(field, 2, 2);
            possibleValues.Should().BeEquivalentTo(5, 6, 7, 8, 9);
        }
    }
}