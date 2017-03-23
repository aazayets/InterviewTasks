using System;
using System.Linq;

namespace Surface
{
    public class Tester
    {
        private readonly ISolution solution;

        public Tester(ISolution solution)
        {
            this.solution = solution;
        }

        public void RunTests()
        {
            NoLakes();
            SingleLake();
            SeveralLakes();
            LargeLake();
        }

        private void SingleLake()
        {
            var surface = new[]
            {
                "####",
                "#O##",
                "#OO#",
                "####"
            };

            Test("Single lake", surface, new []
            {
                new TestCase(1, 1, answer: 3),
                new TestCase(2, 1, answer: 3),
                new TestCase(2, 2, answer: 3),
                new TestCase(0, 1, answer: 0),
                new TestCase(0, 3, answer: 0),
                new TestCase(1, 3, answer: 0),
                new TestCase(1, 0, answer: 0),
                new TestCase(3, 2, answer: 0),
            });
        }

        private void NoLakes()
        {
            var surface = new[]
            {
                "######",
                "######",
                "######",
                "######",
                "######"
            };

            Test("No Lakes", surface, new[]
            {
                new TestCase(1, 1, answer: 0),
                new TestCase(2, 1, answer: 0),
                new TestCase(4, 2, answer: 0),
                new TestCase(0, 1, answer: 0),
                new TestCase(0, 3, answer: 0),
                new TestCase(1, 4, answer: 0),
                new TestCase(1, 0, answer: 0),
                new TestCase(3, 2, answer: 0),
            });
        }

        private void SeveralLakes()
        {
            var surface = new[]
            {
                "OO########",
                "#OO##OO###",
                "##OO###OO#",
                "#######OO#",
                "OOO#######",
                "OOO#######",
                "O#########"
            };

        Test("Several lakes", surface, new[]
            {
                new TestCase(1, 1, answer: 6),
                new TestCase(2, 2, answer: 6),
                new TestCase(1, 0, answer: 0),
                new TestCase(1, 6, answer: 2),
                new TestCase(3, 7, answer: 4),
                new TestCase(4, 2, answer: 7),
                new TestCase(4, 6, answer: 0)
            });
        }

        private void LargeLake()
        {
            var surface = new[]
            {
                "###################",
                "#OOOOOOOOOOOOOOOOO#",
                "#OOOOOOOOOOOOOOOOO#",
                "#OOOOOOOOOOOOOOOOO#",
                "#OOOOOOOOOOOOOOOOO#",
                "#OOOOOOOOOOOOOOOOO#",
                "#OOOOOOOOOOOOOOOOO#",
                "#OOOOOOOOOOOOOOOOO#",
                "##############O####",
                "##############O####",
                "#############OOO###",
                "#############O#####",
                "#############O#####",
                "#OOOOOOOOOOOOOOOOO#",
                "#OOOOOOOOOOOOOOOOO#",
                "###################"
            };

            Test(
                "Large lake",
                surface,
                new[]
                {
                    new TestCase(1, 1, answer: 160),
                    new TestCase(8, 14, answer: 160),
                    new TestCase(9, 14, answer: 160),
                    new TestCase(10, 13, answer: 160),
                    new TestCase(10, 15, answer: 160),
                    new TestCase(14, 10, answer: 160),
                    new TestCase(0, 0, answer: 0),
                    new TestCase(9, 9, answer: 0),
                });
        }

        private SurfaceType[][] ParseSurface(params string[] lines)
        {
            return lines
                .Select(line => line.Select(ch => ch == '#' ? SurfaceType.Land : SurfaceType.Water).ToArray())
                .ToArray();
        }

        private void Test(string name, string[] surface, TestCase[] testCases)
        {
            Console.WriteLine($"Test '{name}', surface:");
            foreach (var str in surface)
            {
                Console.WriteLine(str);
            }

            solution.Initialize(ParseSurface(surface));

            foreach (var testCase in testCases)
            {
                var answer = solution.GetArea(testCase.X, testCase.Y);
                if (answer != testCase.Answer)
                {
                    Console.WriteLine($"Wrong answer for coordinates ({testCase.X}, {testCase.Y})");
                    Console.WriteLine($"\tExpected: {testCase.Answer}, got {answer}");
                }
            }

            Console.WriteLine();
        }

        private class TestCase
        {
            public readonly int X;
            public readonly int Y;
            public readonly int Answer;

            public TestCase(int x, int y, int answer)
            {
                X = x;
                Y = y;
                Answer = answer;
            }
        }
    }
}