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
    public class Tests
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
            Assert.AreEqual(new[] { x }, empty.Push(new [] { x }));
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
