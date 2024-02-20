using System;

public class CalcularPotencia
{
    public static void Main(string[] args)
    {

        int numero, exponente;
        Console.WriteLine("ingresa el valor del numero");
        numero = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("ingresa el valor del exponente");
        exponente = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine(calcular(numero,exponente));
    }
    public static int calcular(int numero, int exponente)
    {
        if (exponente== 0 )
        {
            return 1;

        }
        else
        {
            return numero * calcular(numero, exponente - 1);
        }
    }
}
