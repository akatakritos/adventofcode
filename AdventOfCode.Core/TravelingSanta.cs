using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
