﻿// See https://aka.ms/new-console-template for more information

int Main() => 0;
public class Calcul
{
    int Index_Symbol = 0;
    private string String_Example = "";
    int Current_Symbol;
    char Symbol_Buffer;

    private void ErrorMessage(string msg, string param)
    {
        Console.WriteLine("Error: ");
        Console.WriteLine(msg, param);
        Console.WriteLine("\n");
        Console.ReadKey(true);
    }

    private void Get_Symbol()
    {
        if (Index_Symbol + 1 > String_Example.Length)
        {
            Current_Symbol = '\0';
        }
        else
        {
            Current_Symbol = String_Example[Index_Symbol];
            Index_Symbol++;
        }
    }
    public double CalculWork(string Primer)
    {
        String_Example = Primer;
        Index_Symbol = 0;
        Get_Symbol();
        if (Current_Symbol == '\0')
        {
            Console.WriteLine("\nEmpty Example");
            return 0;
        }
        else
        {
            return MethodA();
        }
    }

    private double MethodA() // + && -
    {
        double x = MethodB();
        while (Current_Symbol == '+' || Current_Symbol == '-')
        {
            Symbol_Buffer = (char)Current_Symbol;
            Get_Symbol();
            if (Symbol_Buffer == '+')
            {
                x += MethodB();
            }
            else
            {
                x -= MethodB();
            }
        }
        return x;
    }

    private double MethodB() // * && /
    {
        double x = MethodC();
        while (Current_Symbol == '*' || Current_Symbol == '/')
        {
            Symbol_Buffer = (char)Current_Symbol;
            Get_Symbol();
            if (Symbol_Buffer == '*')
            {
                x *= MethodC();
            }
            else
            {
                x /= MethodC();
            }
        }
        return x;
    }

    private double MethodC()  // ()
    {
        double x = 0;
        if (Current_Symbol == '(')
        {
            Get_Symbol();
            x = MethodA();
            if (Current_Symbol == ')')
            {
                Get_Symbol();
            }
            else
            {
                Console.WriteLine("\nError! Missing brecket");
                return 0;
            }
        }
        else if (Current_Symbol == '-')
        {
            Get_Symbol();
            x = -MethodC();
        }
        else if (Current_Symbol >= '0' && Current_Symbol <= '9')
        {
            x = MethodD();
        }
        else
        {
            Console.WriteLine("\nError in recording the example");
            return 0;
        }
        return x;
    }

    private double MethodD()
    {
        string x = "";
        while (Current_Symbol >= '0' && Current_Symbol <= '9')
        {
            x += (char)Current_Symbol;
            Get_Symbol();
            if (Current_Symbol == ',')
            {
                x += (char)Current_Symbol;
                Get_Symbol();
            }
            if (Current_Symbol == '.')
            {
                Console.WriteLine("\nIncorrect format");
                return 0;
            }
        }
        return double.Parse(x);
    }
}

