using CuteUtils.Misc;

using System.Text;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class CollectionExtensionsTests
{
    private StringBuilder consoleOutput = null!;

    [TestInitialize]
    public void Initialize()
    {
        consoleOutput = new StringBuilder();
        Console.SetOut(new StringWriter(consoleOutput));
    }

    [TestMethod]
    public void Print_ShouldPrintCollectionElements()
    {
        // Arrange
        List<int> collection = [1, 2, 3, 4, 5];
        string expectedOutput = "1, 2, 3, 4, 5";

        // Act
        collection.Print();
        string actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [TestMethod]
    public void Print_ShouldPrintCollectionElementsWithCustomDelimiter()
    {
        // Arrange
        List<string> collection = ["apple", "banana", "cherry"];
        string expectedOutput = "apple, banana, cherry";

        // Act
        collection.Print(',');
        string actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [TestMethod]
    public void Print_ShouldPrintCollectionElementsToDebugConsole()
    {
        Assert.Inconclusive("Need to find a way to test this.");
    }

    [TestMethod]
    public void PrintTable_ShouldPrint2DArrayInTableFormat()
    {
        // Arrange
        int[,] array = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        string expectedOutput = Environment.NewLine +
                                $"-------------{Environment.NewLine}" +
                                $"| 1 | 2 | 3 |{Environment.NewLine}" +
                                $"-------------{Environment.NewLine}" +
                                $"| 4 | 5 | 6 |{Environment.NewLine}" +
                                $"-------------{Environment.NewLine}" +
                                $"| 7 | 8 | 9 |{Environment.NewLine}" +
                                $"-------------{Environment.NewLine}";

        // Act
        array.PrintTable();
        string actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [TestMethod]
    public void PrintTable_ShouldPrint2DArrayInTableFormatWithAlternativeStyle()
    {
        // Arrange
        int[,] array = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        string expectedOutput = Environment.NewLine +
                                $"+---+---+---+{Environment.NewLine}" +
                                $"| 1 | 2 | 3 |{Environment.NewLine}" +
                                $"+---+---+---+{Environment.NewLine}" +
                                $"| 4 | 5 | 6 |{Environment.NewLine}" +
                                $"+---+---+---+{Environment.NewLine}" +
                                $"| 7 | 8 | 9 |{Environment.NewLine}" +
                                $"+---+---+---+{Environment.NewLine}";

        // Act
        array.PrintTable(TableStyle.Alternative);
        string actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedOutput, actualOutput);
    }

    [TestMethod]
    public void PrintTable_ShouldPrint2DArrayInTableFormatWithListStyle()
    {
        // Arrange
        int[,] array = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        string expectedOutput = Environment.NewLine +
                                $"+---+---+---+{Environment.NewLine}" +
                                $"| 1 | 2 | 3 |{Environment.NewLine}" +
                                $"+---+---+---+{Environment.NewLine}" +
                                $"| 4 | 5 | 6 |{Environment.NewLine}" +
                                $"| 7 | 8 | 9 |{Environment.NewLine}";

        // Act
        array.PrintTable(TableStyle.List);
        string actualOutput = consoleOutput.ToString();

        // Assert
        Assert.AreEqual(expectedOutput, actualOutput);
    }
}