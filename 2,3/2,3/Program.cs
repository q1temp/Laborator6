using System;

namespace Task23;

public class Time
{
    private byte hours;
    private byte minutes;

    public Time()
    {
        this.hours = 0;
        this.minutes = 0;
    }

    public Time(byte hours, byte minutes)
    {
        this.hours = (byte)(hours % 24);
        this.minutes = (byte)(minutes % 60);
    }

    public Time(Time other)
    {
        if (other != null)
        {
            this.hours = other.hours;
            this.minutes = other.minutes;
        }
    }

    public byte Hours
    {
        get { return this.hours; }
        set { this.hours = (byte)(value % 24); }
    }

    public byte Minutes
    {
        get { return this.minutes; }
        set { this.minutes = (byte)(value % 60); }
    }

    public Time Subtract(Time other)
    {
        if (other == null)
        {
            return new Time(this.hours, this.minutes);
        }

        int thisTotalMinutes = this.hours * 60 + this.minutes;
        int otherTotalMinutes = other.hours * 60 + other.minutes;

        int diffMinutes = thisTotalMinutes - otherTotalMinutes;

        if (diffMinutes < 0)
        {
            diffMinutes += 24 * 60;
        }

        byte resultHours = (byte)(diffMinutes / 60);
        byte resultMinutes = (byte)(diffMinutes % 60);

        return new Time(resultHours, resultMinutes);
    }

    public override string ToString()
    {
        return $"{this.hours:D2}:{this.minutes:D2}";
    }

    public static Time operator ++(Time t)
    {
        int totalMinutes = t.hours * 60 + t.minutes + 1;
        byte nextHours = (byte)((totalMinutes / 60) % 24);
        byte nextMinutes = (byte)(totalMinutes % 60);
        return new Time(nextHours, nextMinutes);
    }

    public static Time operator --(Time t)
    {
        int totalMinutes = t.hours * 60 + t.minutes - 1;
        if (totalMinutes < 0)
        {
            totalMinutes += 24 * 60;
        }
        byte nextHours = (byte)(totalMinutes / 60);
        byte nextMinutes = (byte)(totalMinutes % 60);
        return new Time(nextHours, nextMinutes);
    }

    public static implicit operator int(Time t)
    {
        return t.hours * 60 + t.minutes;
    }

    public static explicit operator bool(Time t)
    {
        return t.hours != 0 || t.minutes != 0;
    }

    public static bool operator <(Time t1, Time t2)
    {
        int m1 = t1.hours * 60 + t1.minutes;
        int m2 = t2.hours * 60 + t2.minutes;
        return m1 < m2;
    }

    public static bool operator >(Time t1, Time t2)
    {
        int m1 = t1.hours * 60 + t1.minutes;
        int m2 = t2.hours * 60 + t2.minutes;
        return m1 > m2;
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("ЗАДАНИЕ 2");

        Console.WriteLine("Ввод данных для первого времени (T1):");
        byte h1 = ReadByteInput("Введите часы (0-23): ", 0, 23);
        byte m1 = ReadByteInput("Введите минуты (0-59): ", 0, 59);
        Time t1 = new Time(h1, m1);

        Console.WriteLine("\nВвод данных для второго времени (T2):");
        byte h2 = ReadByteInput("Введите часы (0-23): ", 0, 23);
        byte m2 = ReadByteInput("Введите минуты (0-59): ", 0, 59);
        Time t2 = new Time(h2, m2);

        Console.WriteLine($"\nОбъект T1 (ToString): {t1}");
        Console.WriteLine($"Объект T2 (ToString): {t2}");

        Time tCopied = new Time(t1);
        Console.WriteLine($"Проверка конструктора копирования (Копия T1): {tCopied}");

        Time tSubtracted = t1.Subtract(t2);
        Console.WriteLine($"Результат вычитания T1.Subtract(T2): {tSubtracted}");

        Console.WriteLine("\nЗАДАНИЕ 3");

        Console.WriteLine($"Исходное время T1: {t1}");
        t1++;
        Console.WriteLine($"Операция T1++: {t1}");
        t1--;
        Console.WriteLine($"Операция T1--: {t1}");

        int totalMinutes = t1;
        Console.WriteLine($"Неявное приведение T1 в int (минуты): {totalMinutes}");

        bool isNotZero = (bool)t1;
        Console.WriteLine($"Явное приведение T1 в bool (не нули?): {isNotZero}");

        Console.WriteLine($"Сравнение: T1 > T2? Ответ: {t1 > t2}");
        Console.WriteLine($"Сравнение: T1 < T2? Ответ: {t1 < t2}");
    }

    private static byte ReadByteInput(string message, byte min, byte max)
    {
        byte result;
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (byte.TryParse(input, out result) && result >= min && result <= max)
            {
                return result;
            }

            Console.WriteLine($"Ошибка! Пожалуйста, введите корректное число в диапазоне от {min} до {max}.");
        }
    }
}
