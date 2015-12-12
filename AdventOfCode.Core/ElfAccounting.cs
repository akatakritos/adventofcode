using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

/*
--- Day 12: JSAbacusFramework.io ---

Santa's Accounting-Elves need help balancing the books after a recent order. Unfortunately, their accounting software uses a peculiar storage format. That's where you come in.

They have a JSON document which contains a variety of things: arrays ([1,2,3]), objects ({"a":1, "b":2}), numbers, and strings. Your first job is to simply find all of the numbers throughout the document and add them together.

For example:

[1,2,3] and {"a":2,"b":4} both have a sum of 6.
[[[3]]] and {"a":{"b":4},"c":-1} both have a sum of 3.
{"a":[-1,1]} and [-1,{"a":1}] both have a sum of 0.
[] and {} both have a sum of 0.
You will not encounter any strings containing numbers.

What is the sum of all numbers in the document?

--- Part Two ---

Uh oh - the Accounting-Elves have realized that they double-counted everything red.

Ignore any object (and all of its children) which has any property with the value "red". Do this only for objects ({...}), not arrays ([...]).

[1,2,3] still has a sum of 6.
[1,{"c":"red","b":2},3] now has a sum of 4, because the middle object is ignored.
{"d":"red","e":[1,2,3,4],"f":5} now has a sum of 0, because the entire structure is ignored.
[1,"red",5] has a sum of 6, because "red" in an array has no effect.
*/
namespace AdventOfCode.Core
{
    public static class ElfAccounting
    {
        public static int SumAllNumbers(string json)
        {
            var token = JToken.Parse(json);
            var sum = 0;

            Recurse(token, i => sum += i);

            return sum;
        }

        public static int SumAllNonRedObjects(string json)
        {
            var token = JToken.Parse(json);
            var sum = 0;

            RecurseSkippingRedObjects(token, i => sum += i);

            return sum;
        }

        private static void Recurse(JToken token, Action<int> process)
        {
            Predicate<JObject> predicate = o => true;
            RecurseWithObjectFilter(token, process, predicate);
        }

        private static void RecurseSkippingRedObjects(JToken token, Action<int> process)
        {
            Predicate<JObject> predicate = o => o.Properties().All(p => p.Value.ToString() != "red");
            RecurseWithObjectFilter(token, process, predicate);
        }

        private static void RecurseWithObjectFilter(JToken token, Action<int> process, Predicate<JObject> includeObject)
        {

            if (token.Type == JTokenType.Integer)
                process(token.Value<int>());

            if (token.Type == JTokenType.Array)
            {
                foreach (var element in token.Children())
                {
                    RecurseWithObjectFilter(element, process, includeObject);
                }
            }

            if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                if (!includeObject(obj))
                    return;

                foreach (var prop in token.Children<JProperty>())
                    RecurseWithObjectFilter(prop.Value, process, includeObject);
            }
        }

    }
}
