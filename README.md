# Angry Arrays

Angry Arrays is a .NET library with extensions methods for arrays. Each method
is designed to copy the source array, apply the requested modifications and
return the result.

Angry Arrays is available from NuGet in two formats:

  * [Portable Class Library][pclpkg]  
    [![NuGet version](https://badge.fury.io/nu/AngryArrays.svg)](http://badge.fury.io/nu/AngryArrays)
  * [Single C# source][srcpkg] for direct inclusion in your project:  
    [![NuGet version](https://badge.fury.io/nu/AngryArrays.Source.svg)](http://badge.fury.io/nu/AngryArrays.Source)

Many of the extension methods are inspired from like-named [JavaScript
functions for arrays][jsarray].

Each extension method along with its overloads lie in a separate namespace
based on the name of the method so you only import what you use:

    using AngryArrays.Copy;
    using AngryArrays.Pop;
    using AngryArrays.Push;
    using AngryArrays.Shift;
    using AngryArrays.Splice;
    using AngryArrays.Unshift;


## Push

Appends a new item at the end of an array:

    var xs = new[] { 1, 2, 3, 4, 5 }.Push(6);
    Console.WriteLine(string.Join(",", xs));
    // 1,2,3,4,5,6

Append multiple items at the end of an array too:

    var xs = new[] { 1, 2, 3, 4, 5 }.Push(6, 7, 8);
    Console.WriteLine(string.Join(",", xs));
    // 1,2,3,4,5,6,7,8

## Unshift

Prepends a new item to the beginning of an array:

    var xs = new[] { 1, 2, 3, 4, 5 }.Unshift(0);
    Console.WriteLine(string.Join(",", xs));
    // 0,1,2,3,4,5

Prepends multiple items to the beginning of an array too:

    var xs = new[] { 4, 5, 6, 7, 8 }.Unshift(1, 2, 3);
    Console.WriteLine(string.Join(",", xs));
    // 1,2,3,4,5,6,7,8

## Shift

Removes the first item of an array:

    var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Shift((h, t) => new
    {
        Shifted = h,
        Rest    = string.Join(",", t)
    });
    Console.WriteLine(res);
    // { Shifted = 1, Rest = 2,3,4,5,6,7,8 }

Remove several items from the head of an array:

    var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Shift(4, (h, t) => new
    {
        Shifted = string.Join(",", h),
        Rest    = string.Join(",", t)
    });
    Console.WriteLine(res);
    // { Shifted = 1,2,3,4, Rest = 5,6,7,8 }

**Beware** that following will throw `InvalidOperationException` because the
array is empty and `h` will be undefined:

    var res = new int[0].Shift((h, t) => new
    {
        Shifted = h,
        Rest    = string.Join(",", t)
    });
    Console.WriteLine(res); // This is never reached!

However, calling the overload with a count of one won't throw and both `h` and
`t` will be empty:

    var res = new int[0].Shift(1, (h, t) => new
    {
        Shifted = string.Join(",", h),
        Rest    = string.Join(",", t)
    });
    Console.WriteLine(res);
    // { Shifted = , Rest = }

## Pop

Removes the last item of an array and returns the popped item as well as the
a copy of the original array without the popped item:

    var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Pop((t, h) => new
    {
        Popped = t, Rest = string.Join(",", h)
    });
    Console.WriteLine(res);
    // { Popped = 8, Rest = 1,2,3,4,5,6,7 }

You can also use an overload to say how many items to pop:

    var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Pop(4, (t, h) => new
    {
        Popped = string.Join(",", t),
        Rest   = string.Join(",", h)
    });
    Console.WriteLine(res);
    // { Popped = 5,6,7,8, Rest = 1,2,3,4 }

**Beware** that following will throw `InvalidOperationException` because the
array is empty and `t` will be undefined:

    var res = new int[0].Pop((t, h) => new
    {
        Popped = t,
        Rest   = string.Join(",", h)
    });
    Console.WriteLine(res); // This is never reached!

However, calling the overload with a count of one won't throw and both `h` and
`t` will be empty:

    var res = new int[0].Pop(1, (t, h) => new
    {
        Popped = string.Join(",", t),
        Rest   = string.Join(",", h)
    });
    Console.WriteLine(res);
    // { Popped = , Rest = }

## Splice

Removes a contiguous segment of items from an array identified by a starting
index and count:

    var xs = new[] { 1, 2, 3, 4, 5, 6, 8 }.Splice(3, 2);
    Console.WriteLine(string.Join(",", xs));
    // 1,2,3,6,7,8

You can also request the spliced array as well as the removed segment:

    var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Splice(3, 2, (s, d) => new
    {
        Spliced = string.Join(",", s),
        Deleted = string.Join(",", d),
    });
    Console.WriteLine(res);
    // { Spliced = 1,2,3,6,7,8, Deleted = 4,5 }

Like in JavaScript, `Splice` allows negative values for the start index,
meaning that many items from the end of the array:

    var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Splice(-3, 2, (s, d) => new
    {
        Spliced = string.Join(",", s),
        Deleted = string.Join(",", d),
    });
    Console.WriteLine(res);
    // { Spliced = 1,2,3,4,5,8, Deleted = 6,7 }

Omit the count and everything until the end of the array will be removed:

    var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Splice(-4, (s, d) => new
    {
        Spliced = string.Join(",", s),
        Deleted = string.Join(",", d),
    });
    Console.WriteLine(res);
    // { Spliced = 1,2,3,4, Deleted = 5,6,7,8 }

## Copy

It is just [`Array.Clone`][array-clone] with the return type same as the
input array so a re-cast is not necessary:

    var xs = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    var ys = xs.Copy(); // ys is also int[], not object as with Array.Clone
    Console.WriteLine(object.ReferenceEquals(xs, ys));
    // False



  [pclpkg]: https://www.nuget.org/packages/AngryArrays/
  [srcpkg]: https://www.nuget.org/packages/AngryArrays.Source/
  [jsarray]: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array
  [array-clone]: https://msdn.microsoft.com/en-us/library/system.array.clone%28v=vs.110%29.aspx
