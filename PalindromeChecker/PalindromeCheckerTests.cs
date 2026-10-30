namespace InterviewExercises.PalindromeChecker;

public class PalindromeCheckerTests
{
    [Theory]
    [InlineData("racecar")]
    [InlineData("Racecar")]
    [InlineData("A man, a plan, a canal: Panama!")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("a")]

    //Added cases
    [InlineData("12321")]
    [InlineData("1221")]
    [InlineData("abba")]
    [InlineData("?!., ")]
    [InlineData("a1b2 2b1a")]
    public void IsPalindrome_ReturnsTrueForPalindromes(string value)
    {
        Assert.True(PalindromeChecker.IsPalindrome(value));
    }

    [Theory]
    [InlineData("hello")]
    [InlineData("racecars")]
    [InlineData("A man, a plan, a canal: California!")]
    [InlineData("ab")]

    [InlineData("12345")]
    [InlineData("1234")]
    [InlineData("a,b")]
    [InlineData("0Z")]
    public void IsPalindrome_ReturnsFalseForNonPalindromes(string value)
    {
        Assert.False(PalindromeChecker.IsPalindrome(value));
    }

    [Fact]
    public void IsPalindrome_ThrowsForNull()
    {
        Assert.Throws<ArgumentNullException>(() => PalindromeChecker.IsPalindrome(null!));
    }
}
