namespace InterviewExercises.PalindromeChecker;

public static class PalindromeChecker
{
    public static bool IsPalindrome(string value)
    {
        //Handles Null case
        ArgumentNullException.ThrowIfNull(value);

        //Pointers
        int left = 0, right = value.Length - 1;

        while (left < right)
        {
            //First two take care of spaces and punctuation
            if (!char.IsLetterOrDigit(value[left]))
            {
                left++;
            }
            else if (!char.IsLetterOrDigit(value[right]))
            {
                right--;
            }
            //This one returns false if string is not palindrome
            else if (char.ToLower(value[left++]) != char.ToLower(value[right--]))
            {
                return false;
            }
        }

        //return true once the whole string is checked
        return true;
    }
}
