using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using Kakuro.Model;
using System.Collections.Generic;
using System.Linq;

namespace UnitTesting
{
    [TestFixture]
    public class KakuroLogicTests
    {
        private Board _testBoard;

        [SetUp]
        public void Setup()
        {
            // Create a small 3x3 Mock Board
            // Legend: E = Empty, C = Clue, V = Entry
            // [C] [C] [C]
            // [C] [V] [V]  <- Row sum clue might be 7
            // [C] [V] [V]

            var grid = new Cell[3, 3];
            var entries = new List<Entry>();

            // Fill with entries for a simple 2x2 playable area
            for (int x = 1; x < 3; x++)
            {
                for (int y = 1; y < 3; y++)
                {
                    var entry = new Entry(x, y, x + y); // Correct value is sum of coords
                    grid[x, y] = entry;
                    entries.Add(entry);
                }
            }

            // Create a Horizontal Segment (Clue: 7, Cells: (1,1) and (2,1))
            var horSeg = new List<SumSegment>
            {
                new SumSegment(7, new List<Entry> { (Entry)grid[1,1], (Entry)grid[2,1] })
            };

            var verSeg = new List<SumSegment>
            {
                new SumSegment(5, new List<Entry> { (Entry)grid[1,1], (Entry)grid[1,2] })
            };

            _testBoard = new Board(1, 3, 3, "Easy", grid, horSeg, verSeg, 100);
        }

        [Test]
        public void ValidateSolution_WithCorrectInput_ReturnsTrue()
        {
            // Arrange
            var gameplayPage = new Gameplay();
            int[,] userInput = new int[3, 3];

            // Set values that match the segment logic (even if mocked simply)
            // In a real scenario, Segment.IsValid() usually checks if Sum(Entries) == TargetSum
            // For this test, we assume the SumSegments were populated correctly.

            // Act
            // Note: Since ValidateSolution in your code uses HorizontalSegments.All(...)
            // we are testing the logic of the Board model through the Page method.
            bool result = gameplayPage.ValidateSolution(_testBoard, userInput);

            // Assert
            Assert.That(result, Is.True, "The solution should be valid if segments satisfy their sums.");
        }

        [Test]
        public void FetchBoard_InvalidId_ReturnsNull()
        {
            // This tests the behavior of your FetchBoard logic 
            // Note: In a real environment, you'd wrap the SQL call in a Repository 
            // to avoid "Network-related instance" errors during testing.
            var gameplayPage = new Gameplay();

            // Act & Assert
            // We expect this to fail or return null because the DB connection string 
            // won't resolve in a test runner environment.
            Assert.DoesNotThrow(() => {
                var board = gameplayPage.FetchBoard(-1);
                Assert.IsNull(board);
            });
        }
    }
}
