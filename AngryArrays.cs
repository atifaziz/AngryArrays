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

namespace AngryArrays
{
    using System;

    // ReSharper disable PartialTypeWithSinglePart

    namespace Push
    {
        static partial class AngryArray
        {
            public static T[] Push<T>(this T[] array, T item)
            {
                if (array == null) throw new ArgumentNullException(nameof(array));
                var combined = new T[array.Length + 1];
                array.CopyTo(combined, 0);
                combined[combined.Length - 1] = item;
                return combined;
            }

            public static T[] Push<T>(this T[] array, params T[] items)
            {
                if (array == null) throw new ArgumentNullException(nameof(array));
                var length = array.Length + (items?.Length ?? 0);
                if (length == 0)
                    return array;
                var combined = new T[length];
                array.CopyTo(combined, 0);
                items?.CopyTo(combined, array.Length);
                return combined;
            }
        }
    }
}
