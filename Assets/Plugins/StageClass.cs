using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class TargetCoord
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
}

[Serializable]
public class Stage
{
    public List<TargetCoord> TargetCoords { get; set; }
    public string PartialExpression { get; set; } = "";
    public int XMax { get; set; } = 0;
    public int XMin { get; set; } = 0;
    public int VariableMax { get; set; } = 0;
    public int VariableMin { get; set; } = 0;
}

[Serializable]
public class Root
{
    public List<Stage> Stages { get; set; }
}