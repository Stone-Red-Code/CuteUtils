using CuteUtils.Misc;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class StringExtTests_Coverage
{
    [TestMethod]
    public void ToFileName_EmptyAndNull_ReturnsEmptyOrThrows()
    {
        Assert.AreEqual("", "".ToFileName());
        Assert.AreEqual("", "".ToFileName(allowSpaces: false));

        string? nullStr = null;
        _ = Assert.ThrowsExactly<NullReferenceException>(() => nullStr!.ToFileName());
    }

    [TestMethod]
    public void ToFileName_NormalizesUnicodeAccents()
    {
        string input = "Ångström Îñţérñåţîöñåļ";
        string output = input.ToFileName();

        Assert.IsFalse(output.Any(c => c > 127));  // no extended unicode
        Assert.Contains("An", output);       // Å -> A + n (typical decompose)
    }

    [TestMethod]
    public void ToFileName_LongString_TrimsProperly()
    {
        string longInput = new('x', 500);
        string result = longInput.ToFileName();

        Assert.IsLessThanOrEqualTo(255, result.Length, "File name should be trimmed to FS-safe length");
    }

    [TestMethod]
    public void ToPath_EmptyAndNull_ReturnsEmptyOrThrows()
    {
        Assert.AreEqual("", "".ToPath());

        string? nullStr = null;
        _ = Assert.ThrowsExactly<NullReferenceException>(() => nullStr!.ToPath());
    }

    [TestMethod]
    public void ToPath_RemovesBothFileAndPathInvalidChars()
    {
        string input = "t<>e\"s|t/dir\\name:*?";
        string output = input.ToPath();

        foreach (char c in Path.GetInvalidPathChars())
        {
            Assert.DoesNotContain(c, output);
        }
    }

    [TestMethod]
    public void Truncate_HandlesNull_Throws()
    {
        string? nullStr = null;
        _ = Assert.ThrowsExactly<NullReferenceException>(() => nullStr!.Truncate(5));
    }

    [TestMethod]
    public void Truncate_LongString_Performance()
    {
        string longStr = new('x', 10_000);
        string result = longStr.Truncate(100);

        Assert.AreEqual(100, result.Length);
    }

    [TestMethod]
    public void CorrectNewLine_EmptyString_ReturnsEmpty()
    {
        Assert.AreEqual("", "".CorrectNewLine());
    }

    [TestMethod]
    public void CorrectNewLine_Mixed_Newlines_Normalizes()
    {
        string mixed = "a\r\nb\nc\rd";

        string result = mixed.CorrectNewLine();

        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            Assert.AreEqual("a\nb\nc\nd", result);
        }
        else
        {
            Assert.AreEqual("a\r\nb\r\nc\r\nd", result);
        }
    }

    [TestMethod]
    public void RemoveWhitespaces_UnicodeSpaces()
    {
        string input = "a\u2003b\u3000c"; // em-space, ideographic space
        Assert.AreEqual("abc", input.RemoveWhitespaces());
    }

    [TestMethod]
    public void Reverse_EmptyAndSingleCharacter()
    {
        Assert.AreEqual("", "".Reverse());
        Assert.AreEqual("a", "a".Reverse());
    }

    [TestMethod]
    public void Reverse_UnicodeCombiningCharacters()
    {
        string input = "e\u0301"; // é composed as e + diacritic
        string result = input.Reverse();

        Assert.AreEqual("\u0301e", result);
    }

    [TestMethod]
    public void ReplaceCaseInsensitive_UnicodeCase()
    {
        string input = "Straße"; // Eszett
        string replaced = input.ReplaceCaseInsensitive("ß", "ss");

        Assert.AreEqual("Strasse", replaced);
    }

    [TestMethod]
    public void ReplaceCaseInsensitive_ReplacesOverlappingCorrectly()
    {
        string input = "aaaa";
        string replaced = input.ReplaceCaseInsensitive("aa", "b");

        Assert.AreEqual("bb", replaced); // correct non-overlapping behavior
    }

    [TestMethod]
    public void RemoveDiacritics_RemovesAccents_PreservesBaseCharacters()
    {
        // composed character
        string caf = "café";
        Assert.AreEqual("cafe", caf.RemoveDiacritics());

        // another composed example (Å -> A or Ang depending on normalization)
        string ang = "Ångström";
        Assert.AreEqual("Angstrom", ang.RemoveDiacritics());

        // decomposed form: e + combining acute accent
        string decomposed = "e\u0301"; // e + ◌́
        Assert.AreEqual("e", decomposed.RemoveDiacritics());

        // empty string stays empty
        Assert.AreEqual(string.Empty, string.Empty.RemoveDiacritics());

        // null behavior: current implementation will throw NullReferenceException
        string? nullStr = null;
        _ = Assert.ThrowsExactly<NullReferenceException>(() => nullStr!.RemoveDiacritics());
    }

}
