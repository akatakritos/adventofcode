using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Core
{
    public enum ReindeerState
    {
        Flying,
        Resting
    }

    public struct ReindeerStats
    {
        public int Stamina { get; }
        public int Recovery { get; }
        public int Speed { get; }
        public string Name { get; }

        public ReindeerStats(string name, int speed, int stamina, int recovery)
        {
            Name = name;
            Speed = speed;
            Stamina = stamina;
            Recovery = recovery;
        }

        public static ReindeerStats Parse(string description)
        {
            var result = Regex.Match(description, @"^(?<name>\w+) can fly (?<speed>\d+) km/s for (?<stamina>\d+) seconds, but then must rest for (?<recovery>\d+) seconds.$");

            if (!result.Success)
                throw new FormatException();

            var name = result.Groups["name"].Value;
            var speed = int.Parse(result.Groups["speed"].Value);
            var stamina = int.Parse(result.Groups["stamina"].Value);
            var recovery = int.Parse(result.Groups["recovery"].Value);

            return new ReindeerStats(name, speed, stamina, recovery);
        }
    }

    public class RacingReindeer
    {
        private readonly ReindeerStats _stats;

        private int _distance;
        private ReindeerState _state = ReindeerState.Flying;
        private int _timeInState;
        private int _points;

        public RacingReindeer(ReindeerStats stats)
        {
            _stats = stats;
        }

        public string Name => _stats.Name;
        public int Distance => _distance;
        public int Points => _points;

        public void Step()
        {
            if (_state == ReindeerState.Flying)
            {
                _distance += _stats.Speed;
            }

            _timeInState++;

            if (_state == ReindeerState.Flying && _timeInState == _stats.Stamina)
            {
                _timeInState = 0;
                _state = ReindeerState.Resting;
            }
            else if (_state == ReindeerState.Resting && _timeInState == _stats.Recovery)
            {
                _timeInState = 0;
                _state = ReindeerState.Flying;
            }
        }

        public void AwardStepPoints(int points)
        {
            _points += points;
        }
    }

    public class ReindeerRace
    {
        private List<RacingReindeer> _racers = new List<RacingReindeer>();

        public void AddReindeer(string description)
        {
            _racers.Add(new RacingReindeer(ReindeerStats.Parse(description)));
        }

        public void AddMultipleReindeer(IEnumerable<string> descriptions)
        {
            foreach (var description in descriptions)
                AddReindeer(description);
        }

        public RacingReindeer GetWinner(int steps)
        {
            for (var i = 1; i <= steps; i++)
            {
                foreach (var racer in _racers)
                {
                    racer.Step();
                }
            }

            return GetWinners().First();
        }

        public RacingReindeer GetWinnerByRoundRules(int steps)
        {
            for (var i = 1; i <= steps; i++)
            {
                foreach (var racer in _racers)
                {
                    racer.Step();
                }

                foreach (var winner in GetWinners())
                {
                    winner.AwardStepPoints(1);
                }
            }

            return _racers.OrderByDescending(r => r.Points)
                .First();
        }


        private IEnumerable<RacingReindeer> GetWinners()
        {
            int? distance = null;
            return _racers.OrderByDescending(r => r.Distance)
                .TakeWhile(r =>
                {
                    if (distance == null)
                        distance = r.Distance;

                    return r.Distance == distance.Value;
                });
        }
    }
}
