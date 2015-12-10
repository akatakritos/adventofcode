using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/*
--- Day 9: All in a Single Night ---

Every year, Santa manages to deliver all of his presents in a single night.

This year, however, he has some new locations to visit; his elves have provided him the distances between every pair of locations. He can start and end at any two (different) locations he wants, but he must visit each location exactly once. What is the shortest distance he can travel to achieve this?

For example, given the following distances:

London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141

The possible routes are therefore:

Dublin -> London -> Belfast = 982
London -> Dublin -> Belfast = 605
London -> Belfast -> Dublin = 659
Dublin -> Belfast -> London = 659
Belfast -> Dublin -> London = 605
Belfast -> London -> Dublin = 982

The shortest of these is London -> Dublin -> Belfast = 605, and so the answer is 605 in this example.

What is the distance of the shortest route?

The first half of this puzzle is complete! It provides one gold star: *
--- Part Two ---

The next year, just to show off, Santa decides to take the route with the longest distance instead.

He can still start and end at any two (different) locations he wants, and he still must visit each location exactly once.

For example, given the distances above, the longest route would be 982 via (for example) Dublin -> London -> Belfast.

What is the distance of the longest route?

Although it hasn't changed, you can still get your puzzle input.
*/
namespace AdventOfCode.Core
{
    public struct SantaRoute
    {
        public IReadOnlyCollection<string> Stops { get; }
        public int TotalDistance { get; }

        public SantaRoute(int total, params string[] stops)
        {
            TotalDistance = total;
            Stops = stops;
        }

        public SantaRoute(int total, IEnumerable<string> stops)
        {
            Stops = stops.ToArray();
            TotalDistance = total;
        }

        public override string ToString()
        {
            return string.Join(" -> ", Stops);
        }
    }

    internal class CityGraph
    {
        private readonly Dictionary<string, CityNode> _nodes = new Dictionary<string, CityNode>(20);

        public void AddRoute(string from, string to, int distance)
        {
            var fromNode = GetOrCreateCityNode(from);
            var toNode = GetOrCreateCityNode(to);

            fromNode.Routes.Add(new CityRoute(toNode, distance));
            toNode.Routes.Add(new CityRoute(fromNode, distance));
        }

        private CityNode GetOrCreateCityNode(string name)
        {
            CityNode node;
            if (!_nodes.TryGetValue(name, out node))
            {
                node = new CityNode(name);
                _nodes[name] = node;
            }

            return node;
        }

        public IEnumerable<CityNode> Cities => _nodes.Values;

        public CityNode GetCity(string name)
        {
            return _nodes[name];
        }
    }

    internal class CityNode
    {
        public string Name { get; }
        public IList<CityRoute> Routes { get; } = new List<CityRoute>();

        public CityNode(string name)
        {
            Name = name;
        }
    }

    internal class CityRoute
    {
        public CityNode Destination { get; }
        public int Distance { get; }

        public CityRoute(CityNode destination, int distance)
        {
            Destination = destination;
            Distance = distance;
        }
    }

    internal class RouteFinder
    {
        private readonly CityNode _startingCity;
        private readonly List<SantaRoute> _routes = new List<SantaRoute>();

        public RouteFinder(CityNode startingCity)
        {
            _startingCity = startingCity;
        }

        public IReadOnlyCollection<SantaRoute> FindAllRoutes()
        {
            FindRestOfRoute(new List<string>() { _startingCity.Name }, 0, _startingCity);
            return _routes;
        }

        private void FindRestOfRoute(List<string> route, int traveled, CityNode city)
        {
            bool moved = false;
            foreach (var leg in city.Routes)
            {
                if (!route.Contains(leg.Destination.Name))
                {
                    moved = true;
                    var routeSoFar = new List<string>(route);
                    routeSoFar.Add(leg.Destination.Name);
                    FindRestOfRoute(routeSoFar, traveled + leg.Distance, leg.Destination);
                }
            }

            if (!moved)
                _routes.Add(new SantaRoute(traveled, route));
        }
    }

    public class TravelingSanta
    {
        private readonly CityGraph _map = new CityGraph();

        public void AddDistance(string from, string to, int distance)
        {
            _map.AddRoute(from, to, distance);
        }

        public void AddDistance(string line)
        {
            var match = Regex.Match(line, @"^(\w+) to (\w+) = (\d+)$");
            var from = match.Groups[1].Value;
            var to = match.Groups[2].Value;
            var distance = int.Parse(match.Groups[3].Value);

            AddDistance(from, to, distance);
        }

        public void AddDistances(IEnumerable<string> lines)
        {
            foreach (var line in lines)
                AddDistance(line);
        }

        public SantaRoute FindShortestRoute()
        {
            var shortestRoute = default(SantaRoute);
            bool first = true;

            foreach (var city in _map.Cities)
            {
                var navigator = new RouteFinder(city);
                var shortest = navigator.FindAllRoutes().OrderBy(r => r.TotalDistance).First();

                if (first || shortest.TotalDistance < shortestRoute.TotalDistance)
                    shortestRoute = shortest;

                first = false;
            }

            return shortestRoute;
        }

        public SantaRoute FindLongestRoute()
        {
            var longestRoute = default(SantaRoute);
            foreach (var city in _map.Cities)
            {
                var navigator = new RouteFinder(city);
                var longest = navigator.FindAllRoutes().OrderByDescending(r => r.TotalDistance).First();

                if (longest.TotalDistance > longestRoute.TotalDistance)
                    longestRoute = longest;

            }

            return longestRoute;
        }
    }
}
