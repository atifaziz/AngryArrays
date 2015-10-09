# Angry Arrays

Angry Arrays is a .NET library with extensions methods for arrays. Each method
is designed to copy the source array, apply the requested modifications and
return the result.

Many of the extension methods are inspired from like-named [JavaScript
functions for arrays][jsarray].

## Push

Appends a new item at the end of the array:

    var xs = new[] { 1, 2, 3, 4, 5 }.Push(6);
    Console.WriteLine(string.Join(",", xs));
    // 1, 2, 3, 4, 5, 6


Append multiple items at the end of the array too:

    var xs = new[] { 1, 2, 3, 4, 5 }.Push(6, 7, 8);
    Console.WriteLine(string.Join(",", xs));
    // 1, 2, 3, 4, 5, 6, 7, 8


  [jsarray]: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array