#region Copyright (c) 2015 Atif Aziz. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace AngryArrays.Tests
{
    using System;
    using NUnit.Framework;
    using Push;

    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void PushFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                AngryArray.Push(null, 42));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void PushParamsFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                AngryArray.Push(null, 123, 456, 789));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void Push()
        {
            const int x = 123, y = 456, z = 789;
            Assert.AreEqual(new[] { x, y, z }, new[] { x, y }.Push(z));
        }

        [Test]
        public void PushParams()
        {
            const int x = 123, y = 456, z = 789;

            var empty = new int[0];
            Assert.AreEqual(new[] { x }, empty.Push(new[] { x }));
            Assert.AreEqual(new[] { x, y }, empty.Push(x, y));
            Assert.AreEqual(new[] { x, y, z }, empty.Push(x, y, z));

            Assert.AreEqual(new[] { x, y }, new[] { x }.Push(new[] { y }));
            Assert.AreEqual(new[] { x, y, z }, new[] { x }.Push(y, z));

            Assert.AreEqual(new[] { x, y, z }, new[] { x, y }.Push(new[] { z }));
        }

        [Test]
        public void PushCopiesEvenWithNoParams()
        {
            var xs = new[] { 1234, 456, 789 };
            Assert.AreNotSame(xs, xs.Push());
        }

        [Test]
        public void PushWithNoParamsOnEmptyReturnsSame()
        {
            var empty = new int[0];
            Assert.AreSame(empty, empty.Push());
        }

        [Test]
        public void PushCopiesEvenWithNullParams()
        {
            var xs = new[] { 1234, 456, 789 };
            Assert.AreNotSame(xs, xs.Push(null));
        }

        [Test]
        public void PushWithNullParamsOnEmptyReturnsSame()
        {
            var empty = new int[0];
            Assert.AreSame(empty, empty.Push(null));
        }
    }
}

namespace AngryArrays.Tests
{
    using System;
    using NUnit.Framework;
    using Splice;

    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void SpliceFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                // ReSharper disable once InvokeAsExtensionMethod
                AngryArray.Splice((object[]) null, 0, 0, delegate { return 0; }));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void SpliceFailsWithNegativeCount()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new int[42].Splice(0, -1, delegate { return 0; }));
            Assert.AreEqual("count", e.ParamName);
        }

        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 0, 0)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 1, 0)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 2, 0)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3, 0)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4, 0)]

        [TestCase("bar,baz"    , "foo"        , "foo,bar,baz", 0, 1)]
        [TestCase("foo,baz"    , "bar"        , "foo,bar,baz", 1, 1)]
        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", 2, 1)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3, 1)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4, 1)]

        [TestCase("baz"        , "foo,bar"    , "foo,bar,baz", 0, 2)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", 1, 2)]
        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", 2, 2)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3, 2)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4, 2)]

        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", 0, 3)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", 1, 3)]
        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", 2, 3)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3, 3)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4, 3)]

        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", 0, 4)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", 1, 4)]
        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", 2, 4)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3, 4)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4, 4)]

        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", -1, 0)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", -2, 0)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", -3, 0)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", -4, 0)]

        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", -1, 1)]
        [TestCase("foo,baz"    , "bar"        , "foo,bar,baz", -2, 1)]
        [TestCase("bar,baz"    , "foo"        , "foo,bar,baz", -3, 1)]
        [TestCase("bar,baz"    , "foo"        , "foo,bar,baz", -4, 1)]

        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", -1, 2)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", -2, 2)]
        [TestCase("baz"        , "foo,bar"    , "foo,bar,baz", -3, 2)]
        [TestCase("baz"        , "foo,bar"    , "foo,bar,baz", -3, 2)]

        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", -1, 3)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", -2, 3)]
        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", -3, 3)]
        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", -3, 3)]

        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", -1, 4)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", -2, 4)]
        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", -3, 4)]
        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", -3, 4)]

        public void Splice(string expected, string deletions, string input, int index, int count)
        {
            var r = Split(input).Splice(index, count, (s, d) => new { Spliced = s, Deleted = d });
            Assert.AreEqual(Split(expected), r.Spliced);
            Assert.AreEqual(Split(deletions), r.Deleted);

            Assert.AreEqual(Split(expected), Split(input).Splice(index, count));
        }

        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", 0)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", 1)]
        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", 2)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4)]

        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", -1)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", -2)]
        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", -3)]

        public void SpliceOverloadDefaultingCount(string expected, string deletions, string input, int index)
        {
            var r = Split(input).Splice(index, (s, d) => new { Spliced = s, Deleted = d });
            Assert.AreEqual(Split(expected), r.Spliced);
            Assert.AreEqual(Split(deletions), r.Deleted);

            Assert.AreEqual(Split(expected), Split(input).Splice(index));
        }

        [Test]
        public void SpliceOnEmptyReturnsSame()
        {
            var empty = new int[0];
            Assert.AreSame(empty, empty.Splice(0, 10));
        }

        static string[] Split(string s) =>
            !string.IsNullOrEmpty(s) ? s.Split(',') : new string[0];
    }
}

namespace AngryArrays.Tests
{
    using System;
    using NUnit.Framework;
    using Copy;

    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void CopyFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                AngryArray.Copy((object[])null));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void Copy()
        {
            var xs = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var ys = xs.Copy();
            Assert.IsFalse(ReferenceEquals(xs, ys));
            Assert.AreEqual(xs, ys);
        }
    }
}

namespace AngryArrays.Tests
{
    using System;
    using NUnit.Framework;
    using Unshift;

    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void UnshiftFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                AngryArray.Unshift(null, 42));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void UnshiftParamsFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                AngryArray.Unshift(null, 123, 456, 789));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void Unshift()
        {
            const int x = 123, y = 456, z = 789;
            Assert.AreEqual(new[] { z, x, y }, new[] { x, y }.Unshift(z));
        }

        [Test]
        public void UnshiftParams()
        {
            const int x = 123, y = 456, z = 789;

            var empty = new int[0];
            Assert.AreEqual(new[] { x }, empty.Unshift(new[] { x }));
            Assert.AreEqual(new[] { x, y }, empty.Unshift(x, y));
            Assert.AreEqual(new[] { x, y, z }, empty.Unshift(x, y, z));

            Assert.AreEqual(new[] { y, x }, new[] { x }.Unshift(new[] { y }));
            Assert.AreEqual(new[] { y, z, x }, new[] { x }.Unshift(y, z));

            Assert.AreEqual(new[] { z, x, y }, new[] { x, y }.Unshift(new[] { z }));
        }

        [Test]
        public void UnshiftCopiesEvenWithNoParams()
        {
            var xs = new[] { 1234, 456, 789 };
            Assert.AreNotSame(xs, xs.Unshift());
        }

        [Test]
        public void UnshiftWithNoParamsOnEmptyReturnsSame()
        {
            var empty = new int[0];
            Assert.AreSame(empty, empty.Unshift());
        }

        [Test]
        public void UnshiftCopiesEvenWithNullParams()
        {
            var xs = new[] { 1234, 456, 789 };
            Assert.AreNotSame(xs, xs.Unshift(null));
        }

        [Test]
        public void UnshiftWithNullParamsOnEmptyReturnsSame()
        {
            var empty = new int[0];
            Assert.AreSame(empty, empty.Unshift(null));
        }
    }
}

namespace AngryArrays.Tests
{
    using System;
    using NUnit.Framework;
    using Pop;

    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void PopFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                // ReSharper disable once InvokeAsExtensionMethod
                AngryArray.Pop((object[]) null, delegate { return 0; }));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void PopFailsWithNullSelector()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                // ReSharper disable once InvokeAsExtensionMethod
                new int[0].Pop<int, object>(null));
            Assert.AreEqual("selector", e.ParamName);
        }

        [Test]
        public void PopWithCountFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                // ReSharper disable once InvokeAsExtensionMethod
                AngryArray.Pop((object[]) null, 0, delegate { return 0; }));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void PopFailsWithNegativeCount()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new int[42].Pop(-1, delegate { return 0; }));
            Assert.AreEqual("count", e.ParamName);
        }

        [Test]
        public void PopFailsWithEmptyArray()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new int[0].Pop(delegate { return 0; }));
        }

        [Test]
        public void PopOne()
        {
            const int x = 123, y = 456, z = 789;
            var array = new[] { x, y, z };

            var pop1 = array.Pop((t, h) => new { Popped = t, Rest = h });
            Assert.AreEqual(z, pop1.Popped);
            Assert.AreEqual(new[] { x, y }, pop1.Rest);

            var pop2 = pop1.Rest.Pop((t, h) => new { Popped = t, Rest = h });
            Assert.AreEqual(y, pop2.Popped);
            Assert.AreEqual(new[] { x }, pop2.Rest);

            var pop3 = pop2.Rest.Pop((t, h) => new { Popped = t, Rest = h });
            Assert.AreEqual(x, pop3.Popped);
            Assert.AreEqual(0, pop3.Rest.Length);
        }

        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", 0)]
        [TestCase("baz"        , "foo,bar"    , "foo,bar,baz", 1)]
        [TestCase("bar,baz"    , "foo"        , "foo,bar,baz", 2)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4)]

        public void Pop(string popped, string rest, string input, int count)
        {
            var r = Split(input).Pop(count, (t, h) => new { Popped = t, Rest = h });
            Assert.AreEqual(Split(popped), r.Popped);
            Assert.AreEqual(Split(rest), r.Rest);
        }

        [Test]
        public void PopOnEmptyReturnsSame()
        {
            var empty = new int[0];
            var result = empty.Pop(10, (tail, head) => new { Popped = tail, Rest = head });
            Assert.AreEqual(0, result.Popped.Length);
            Assert.AreEqual(0, result.Rest.Length);
        }
    }
}

namespace AngryArrays.Tests
{
    using System;
    using NUnit.Framework;
    using Shift;

    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void ShiftFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                // ReSharper disable once InvokeAsExtensionMethod
                AngryArray.Shift((object[])null, delegate { return 0; }));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void ShiftFailsWithNullSelector()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                // ReSharper disable once InvokeAsExtensionMethod
                new int[0].Shift<int, object>(null));
            Assert.AreEqual("selector", e.ParamName);
        }

        [Test]
        public void ShiftWithCountFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                // ReSharper disable once InvokeAsExtensionMethod
                AngryArray.Shift((object[])null, 0, delegate { return 0; }));
            Assert.AreEqual("array", e.ParamName);
        }

        [Test]
        public void ShiftFailsWithNegativeCount()
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new int[42].Shift(-1, delegate { return 0; }));
            Assert.AreEqual("count", e.ParamName);
        }

        [Test]
        public void ShiftFailsWithEmptyArray()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new int[0].Shift(delegate { return 0; }));
        }

        [Test]
        public void ShiftOne()
        {
            const int x = 123, y = 456, z = 789;
            var array = new[] { x, y, z };

            var shift1 = array.Shift((t, h) => new { Shifted = t, Rest = h });
            Assert.AreEqual(x, shift1.Shifted);
            Assert.AreEqual(new[] { y, z }, shift1.Rest);

            var shift2 = shift1.Rest.Shift((t, h) => new { Shifted = t, Rest = h });
            Assert.AreEqual(y, shift2.Shifted);
            Assert.AreEqual(new[] { z }, shift2.Rest);

            var shift3 = shift2.Rest.Shift((t, h) => new { Shifted = t, Rest = h });
            Assert.AreEqual(z, shift3.Shifted);
            Assert.AreEqual(0, shift3.Rest.Length);
        }

        [TestCase(""           , "foo,bar,baz", "foo,bar,baz", 0)]
        [TestCase("foo"        , "bar,baz"    , "foo,bar,baz", 1)]
        [TestCase("foo,bar"    , "baz"        , "foo,bar,baz", 2)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 3)]
        [TestCase("foo,bar,baz", ""           , "foo,bar,baz", 4)]

        public void Shift(string shifted, string rest, string input, int count)
        {
            var r = Split(input).Shift(count, (t, h) => new { Shifted = t, Rest = h });
            Assert.AreEqual(Split(shifted), r.Shifted);
            Assert.AreEqual(Split(rest), r.Rest);
        }

        [Test]
        public void ShiftOnEmptyReturnsSame()
        {
            var empty = new int[0];
            var result = empty.Shift(10, (tail, head) => new { Shifted = tail, Rest = head });
            Assert.AreEqual(0, result.Shifted.Length);
            Assert.AreEqual(0, result.Rest.Length);
        }
    }
}
