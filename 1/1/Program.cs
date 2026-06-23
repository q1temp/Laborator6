using System;

namespace task1;

public class Box3D
{
    private int length;
    private int width;
    private int height;

    public Box3D()
    {
        this.length = 1;
        this.width = 1;
        this.height = 1;
    }

    public Box3D(int length, int width, int height)
    {
        this.length = length;
        this.width = width;
        this.height = height;
    }

    public Box3D(Box3D other)
    {
        if (other != null)
        {
            this.length = other.length;
            this.width = other.width;
            this.height = other.height;
        }
    }

    public int Length
    {
        get { return this.length; }
        set { this.length = value; }
    }

    public int Width
    {
        get { return this.width; }
        set { this.width = value; }
    }

    public int Height
    {
        get { return this.height; }
        set { this.height = value; }
    }

    public int CalculateVolume()
    {
        return this.length * this.width * this.height;
    }

    public override string ToString()
    {
        return $"Габариты: {this.length} x {this.width} x {this.height}";
    }
}

public class HeavyBox : Box3D
{
    private int weightGrams;
    private string material;

    public HeavyBox() : base()
    {
        this.weightGrams = 100;
        this.material = "Картон";
    }

    public HeavyBox(int length, int width, int height, int weightGrams, string material)
        : base(length, width, height)
    {
        this.weightGrams = weightGrams;
        this.material = material;
    }

    public HeavyBox(HeavyBox other) : base(other)
    {
        if (other != null)
        {
            this.weightGrams = other.weightGrams;
            this.material = other.material;
        }
    }

    public int WeightGrams
    {
        get { return this.weightGrams; }
        set { this.weightGrams = value; }
    }

    public string Material
    {
        get { return this.material; }
        set { this.material = value; }
    }

    public double CalculateDensity()
    {
        int volume = this.CalculateVolume();
        if (volume == 0)
        {
            return 0;
        }
        return (double)this.weightGrams / volume;
    }

    public bool IsTooHeavyForHuman()
    {
        const int MaxHumanWeightGrams = 20000;
        return this.weightGrams > MaxHumanWeightGrams;
    }

    public override string ToString()
    {
        return base.ToString() + $", Вес: {this.weightGrams}г, Материал: {this.material}";
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Ввод данных для Базового класса (Box3D)");
        int length = ReadPositiveInteger("Введите длину: ");
        int width = ReadPositiveInteger("Введите ширину: ");
        int height = ReadPositiveInteger("Введите высоту: ");

        Box3D baseBox = new Box3D(length, width, height);
        Console.WriteLine($"\nСоздан baseBox (Конструктор с параметрами): {baseBox}");
        Console.WriteLine($"Произведение полей (Объем): {baseBox.CalculateVolume()}");

        Box3D copiedBaseBox = new Box3D(baseBox);
        Console.WriteLine($"Создан copiedBaseBox (Конструктор копирования): {copiedBaseBox}");

        Console.WriteLine("\n Ввод данных для Дочернего класса (HeavyBox) ");
        int heavyLength = ReadPositiveInteger("Введите длину: ");
        int heavyWidth = ReadPositiveInteger("Введите ширину: ");
        int heavyHeight = ReadPositiveInteger("Введите высоту: ");
        int weight = ReadPositiveInteger("Введите вес в граммах: ");

        Console.Write("Введите материал: ");
        string material = Console.ReadLine();

        HeavyBox heavyBox = new HeavyBox(heavyLength, heavyWidth, heavyHeight, weight, material);
        Console.WriteLine($"\nСоздан heavyBox (Конструктор с параметрами): {heavyBox}");
        Console.WriteLine($"Объем через базовый метод: {heavyBox.CalculateVolume()}");
        Console.WriteLine($"Условная плотность: {heavyBox.CalculateDensity():F4}");

        if (heavyBox.IsTooHeavyForHuman())
        {
            Console.WriteLine("Внимание: Этот груз слишком тяжел для одного человека!");
        }
        else
        {
            Console.WriteLine("Груз можно поднять вручную.");
        }

        HeavyBox copiedHeavyBox = new HeavyBox(heavyBox);
        Console.WriteLine($"Создан copiedHeavyBox (Конструктор копирования): {copiedHeavyBox}");

    }

    private static int ReadPositiveInteger(string message)
    {
        int result;
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (int.TryParse(input, out result) && result > 0)
            {
                return result;
            }

            Console.WriteLine("Ошибка, нужно целое число больше нуля.");
        }
    }
}
