namespace InterviewExercises.PermissionsEvaluator;

public class PermissionEvaluatorTests
{
    [Fact]
    public void IsAllowed_ReturnsTrueForAnExactPermissionMatch()
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("alice", "reports", "read")
        ]);

        Assert.True(evaluator.IsAllowed("alice", "reports", "read"));
    }

    [Theory]
    [InlineData("bob", "reports", "read")]
    [InlineData("alice", "reports", "write")]
    [InlineData("alice", "users", "read")]
    public void IsAllowed_ReturnsFalseWhenNoExactPermissionMatches(
        string user, string resource, string action)
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("alice", "reports", "read")
        ]);

        Assert.False(evaluator.IsAllowed(user, resource, action));
    }



    [Fact]
    //Makes sure it returns False when no permissions exist.
    public void IsAllowed_ReturnsFalseWhenThereAreNoPermissions()
    {
        var evaluator = new PermissionEvaluator([]);

        Assert.False(evaluator.IsAllowed("alice", "reports", "read"));
    }

    [Theory]
    //Makes sure slightly different permissions are categorized properly
    [InlineData("alice", "reports", "read", true)]
    [InlineData("alice", "reports", "write", true)]
    [InlineData("alice", "users", "read", true)]
    [InlineData("alice", "users", "write", false)] 
    [InlineData("bob", "reports", "read", false)] 
    public void IsAllowed_OnlyMatchesTheSpecificPermissionGranted(
        string user, string resource, string action, bool expected)
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("alice", "reports", "read"),
            new Permission("alice", "reports", "write"),
            new Permission("alice", "users", "read")
        ]);

        Assert.Equal(expected, evaluator.IsAllowed(user, resource, action));
    }

    [Theory]
    //Checks for case sensitivity
    [InlineData("Alice", "reports", "read")]
    [InlineData("alice", "Reports", "read")]
    [InlineData("alice", "reports", "READ")]
    public void IsAllowed_IsCaseSensitive(string user, string resource, string action)
    {
        var evaluator = new PermissionEvaluator(
        [
            new Permission("alice", "reports", "read")
        ]);

        Assert.False(evaluator.IsAllowed(user, resource, action));
    }
}
