using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var shape = new Figure(layer: 2, borderColor: "blue", borderThickness: 2.5, fillColor: "red");
        Console.WriteLine(shape);

        var vertex1 = new Vertex(1.0, 2.5);
        var vertex2 = new Vertex(3.2, 4.5);
        var vertex3 = new Vertex(1.7, 5.6);

        var circleShape = new CircleShape(layer: 2, borderColor: "white", borderThickness: 2.0, radius: 6.0, center: vertex1);
        Console.WriteLine(circleShape);

        var segment1 = new Segment(layer: 2, borderColor: "red", borderThickness: 0.7, startPoint: vertex1, endPoint: vertex3);
        var segment2 = new Segment(layer: 3, borderColor: "blue", borderThickness: 0.9, startPoint: vertex2, endPoint: vertex3);

        var segments = new List<Segment> { segment1, segment2 };
        Segment.DisplayVerticalSegments(segments);

        var polygonShape = new PolygonShape(layer: 15, borderColor: "green", borderThickness: 1.2, vertices: new List<Vertex> { vertex1, vertex2, vertex3 });
        Console.WriteLine(polygonShape);
    }
}

class Figure
{
    public int Layer { get; set; }
    public string BorderColor { get; set; }
    public double BorderThickness { get; set; }
    public string FillColor { get; set; }

    public Figure(int layer = 0, string borderColor = "black", double borderThickness = 1.0, string fillColor = "white")
    {
        Layer = layer;
        BorderColor = borderColor;
        BorderThickness = borderThickness;
        FillColor = fillColor;
    }

    public override string ToString()
    {
        return $"Figure(Layer={Layer}, BorderColor={BorderColor}, BorderThickness={BorderThickness}, FillColor={FillColor})";
    }
}

class Vertex
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vertex(double x = 0.0, double y = 0.0)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"Vertex(X={X}, Y={Y})";
    }
}

sealed class CircleShape : Figure
{
    public double Radius { get; set; }
    public Vertex Center { get; set; }

    public CircleShape(int layer = 0, string borderColor = "black", double borderThickness = 1.0, string fillColor = "white", double radius = 1.0, Vertex center = null)
        : base(layer, borderColor, borderThickness, fillColor)
    {
        Radius = radius;
        Center = center ?? new Vertex();
    }

    public override string ToString()
    {
        return $"CircleShape(Layer={Layer}, BorderColor={BorderColor}, BorderThickness={BorderThickness}, FillColor={FillColor}, Radius={Radius}, Center={Center})";
    }
}

sealed class Segment : Figure
{
    public Vertex StartPoint { get; set; }
    public Vertex EndPoint { get; set; }

    private string _fillColor = "black";
    public string FillColor => _fillColor;

    public Segment(int layer = 0, string borderColor = "black", double borderThickness = 1.0, Vertex startPoint = null, Vertex endPoint = null)
        : base(layer, borderColor, borderThickness)
    {
        StartPoint = startPoint ?? new Vertex();
        EndPoint = endPoint ?? new Vertex();
    }

    public override string ToString()
    {
        return $"Segment(Layer={Layer}, BorderColor={BorderColor}, BorderThickness={BorderThickness}, FillColor={FillColor}, StartPoint={StartPoint}, EndPoint={EndPoint})";
    }

    public static void DisplayVerticalSegments(List<Segment> segments)
    {
        foreach (var segment in segments)
        {
            if (segment.StartPoint.X == segment.EndPoint.X)
            {
                Console.WriteLine($"{segment.StartPoint.X} {segment.StartPoint.Y} {segment.EndPoint.X} {segment.EndPoint.Y}");
            }
        }
    }
}

class PolygonShape : Figure
{
    public List<Vertex> Vertices { get; set; }

    public PolygonShape(int layer = 0, string borderColor = "black", double borderThickness = 1.0, string fillColor = "white", List<Vertex> vertices = null)
        : base(layer, borderColor, borderThickness, fillColor)
    {
        Vertices = vertices ?? new List<Vertex>();
    }

    public override string ToString()
    {
        return $"PolygonShape(Layer={Layer}, BorderColor={BorderColor}, BorderThickness={BorderThickness}, FillColor={FillColor}, Vertices=[{string.Join(", ", Vertices)}])";
    }
}

