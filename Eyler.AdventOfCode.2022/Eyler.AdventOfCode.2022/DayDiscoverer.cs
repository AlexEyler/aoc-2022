using System;
namespace Eyler.AdventOfCode._2022;

public partial class DayDiscoverer
{
    public IDay GetDay(string dayName)
    {
        return DayMapping[dayName]();
    }
}

