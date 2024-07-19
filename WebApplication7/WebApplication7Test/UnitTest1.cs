using YourNamespace.Controllers;

namespace WebApplication7.Tests
{
    public class StringProcessingControllerTests
    {
        [Test]
        public void WrongChars_ShouldReturnNonLetterCharacters()
        {
            var result = StringProcessingController.WrongChars("hello123!");
            Assert.AreEqual("123!", result);
        }

        [Test]
        public void IsOnlyLetters_Method_ShouldReturnTrue_ForValidInput()
        {
            var result = StringProcessingController.IsOnlyLetters_Method("hello");
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOnlyLetters_Method_ShouldReturnFalse_ForInvalidInput()
        {
            var result = StringProcessingController.IsOnlyLetters_Method("hello123");
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("a", "aa")]
        [TestCase("abcdef", "cbafed")]
        [TestCase("abcde", "edcbaabcde")]
        public void ReversString_ShouldReverseString(string input, string expected)
        {
            var result = StringProcessingController.StringManipulator.ReversString(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NumberOfOccurrences_ShouldReturnCorrectDictionary()
        {
            var result1 = StringProcessingController.NumberOfOccurrences("a");
            var expected1 = new Dictionary<char, int> { { 'a', 1 } };
            CollectionAssert.AreEquivalent(expected1, result1);

            var result2 = StringProcessingController.NumberOfOccurrences("abcdef");
            var expected2 = new Dictionary<char, int> { { 'a', 1 }, { 'b', 1 }, { 'c', 1 }, { 'd', 1 }, { 'e', 1 }, { 'f', 1 } };
            CollectionAssert.AreEquivalent(expected2, result2);

            var result3 = StringProcessingController.NumberOfOccurrences("abcde");
            var expected3 = new Dictionary<char, int> { { 'a', 1 }, { 'b', 1 }, { 'c', 1 }, { 'd', 1 }, { 'e', 1 } };
            CollectionAssert.AreEquivalent(expected3, result3);
        }

        [Test]
        [TestCase("aa", "aa")]
        [TestCase("abcdef", "abcde")]
        [TestCase("abcde", "abcde")]
        public void FindLongestVowelSubstring_ShouldReturnLongestVowelSubstring(string input, string expected)
        {
            var result = StringProcessingController.FindLongestVowelSubstring(input);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("abcdef", 1, "acdef")]
        [TestCase("abcde", 2, "abde")]
        public void RemoveCharacterAt_ShouldRemoveCharacterAtIndex(string input, int index, string expected)
        {
            var result = StringProcessingController.RemoveCharacterAt(input, index);
            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase("a", "a")]
        [TestCase("abcdef", "abcdef")]
        [TestCase("dcba", "abcd")]
        public void QuickSortStrings_Sort_ShouldSortString(string input, string expected)
        {
            var result = StringProcessingController.QuickSortStrings.Sort(input);
            Assert.AreEqual(expected, result);
        }
    }
}
