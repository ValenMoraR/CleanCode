using System;
using System.Collections.Generic;
using System.Linq;

Dictionary<string, Dictionary<string, int>> clientes = new();

Console.WriteLine("Bienvenido");
MenuPrincipal();

void AgregarUsuario()
{
    Console.Write("Ingrese el número de cédula del cliente: ");
    string cedula = Console.ReadLine();
    Console.Write("Ingrese el estrato del cliente: ");
    int estrato = Convert.ToInt32(Console.ReadLine());              ///******************
    Console.Write("Ingrese la meta de ahorro del cliente: ");
    int metadeahorro = Convert.ToInt32(Console.ReadLine());         ///******************
    Console.Write("Ingrese el consumo actual de energía del cliente: ");
    int consumoActualEnergia = Convert.ToInt32(Console.ReadLine()); ///******************
    Console.Write("Ingrese el consumo actual de agua del cliente:  ");
    int consumoActualAgua = Convert .ToInt32(Console.ReadLine());   ///******************

    clientes[cedula] = new Dictionary<string, int>
        {
            {"estrato", estrato},
            {"meta_ahorro", metadeahorro},
            {"consumo_actual_E", consumoActualEnergia},
            {"consumo_actual_A", consumoActualAgua},
        };
    Console.WriteLine("Cliente agregado correctamente.");
}

void CalcularPrecioAPagar()
{
    Console.Write("Ingrese el número de cédula del cliente: ");
    string cedula = Console.ReadLine();
    

    if (clientes.ContainsKey(cedula))
    {
        int PromConsumoAgua = 25;
        Dictionary<string, int> cliente = clientes[cedula];

        ///Calcula el precio a pagar por consumo de energia
        int valorParcial = cliente["consumo_actual_E"] * 850;
        int valorIncentivo = (cliente["meta_ahorro"] - cliente["consumo_actual_E"]) * 850;
        int valorPagarEnergia = valorParcial - valorIncentivo;

        ///Calcula el precio a pagar por consumo de agua
        int precioTotalAgua;
        int consumoActualAgua = cliente["consumo_actual_A"];
        int precioPorMt3 = 4600;

        if (consumoActualAgua > PromConsumoAgua)
        {
            int excesoAgua = consumoActualAgua - PromConsumoAgua;
            int precioPorExcesoAgua = excesoAgua * (2 * precioPorMt3); // Precio por exceso al doble
            precioTotalAgua = PromConsumoAgua * precioPorMt3 + precioPorExcesoAgua;
        }
        else
        {
            precioTotalAgua = consumoActualAgua * precioPorMt3;
        }

        int precioTotal = valorPagarEnergia + precioTotalAgua;
        Console.WriteLine($"El valor a pagar del cliente {cedula} por consumo de energía es: ${valorPagarEnergia}");
        Console.WriteLine("\n");
        Console.WriteLine($"El valor a pagar del cliente {cedula} por consumo de agua es: ${precioTotalAgua}");
        Console.WriteLine("\n");
        Console.WriteLine($"El valor total a pagar por el cliente {cedula} es: ${precioTotal}");
        Console.WriteLine("\n");
    }
    else
    {
        Console.WriteLine("Cliente no encontrado.");
    }
}
void CalcularPromedioDeConsumo()
{
    int totalConsumo = 0;
    foreach (var cliente in clientes.Values)
    {
        totalConsumo += cliente["consumo_actual_E"];
    }
    double promedio = (double)totalConsumo / clientes.Count;
    Console.WriteLine($"El promedio del consumo actual de energía es: {promedio} kilovatios");
}
void CalcularTotalDescuentos()
{
    int totalDescuentos = 0;
    foreach (var cliente in clientes.Values)
    {
        if (cliente["consumo_actual"] < cliente["meta_ahorro"])
        {
            totalDescuentos += (cliente["meta_ahorro"] - cliente["consumo_actual"]) * 850;
        }
    }
    Console.WriteLine($"El valor total de descuentos otorgados es: ${totalDescuentos}");
}

void MostrarCantidadTotalAguaConsumidaPorEncimaDelPromedio()
{
    int promedioConsumoAgua = 25; // Valor constante del promedio de consumo de agua
    int totalConsumoAguaPorEncimaDelPromedio = clientes.Values
        .Where(cliente => cliente["consumo_actual_A"] > promedioConsumoAgua)
        .Sum(cliente => cliente["consumo_actual_A"] - promedioConsumoAgua);

    Console.WriteLine($"La cantidad total de mt3 de agua consumida por encima del promedio es: {totalConsumoAguaPorEncimaDelPromedio}");
}
void MostrarPorcentajesConsumoExcesivoAguaPorEstrato()
{
    Dictionary<int, List<double>> porcentajesConsumoExcesivoAgua = new();
    foreach (var cliente in clientes.Values)
    {
        int estrato = cliente["estrato"];
        int consumoActual = cliente["consumo_actual_A"];
        double promedioConsumoAgua = clientes.Values.Average(c => c["consumo_actual_A"]);

        if (consumoActual > promedioConsumoAgua)
        {
            double porcentajeConsumoExcesivo = ((double)consumoActual / promedioConsumoAgua - 1) * 100;
            if (porcentajesConsumoExcesivoAgua.ContainsKey(estrato))
            {
                porcentajesConsumoExcesivoAgua[estrato].Add(porcentajeConsumoExcesivo);
            }
            else
            {
                porcentajesConsumoExcesivoAgua[estrato] = new List<double> { porcentajeConsumoExcesivo };
            }
        }
    }

    foreach (var kvp in porcentajesConsumoExcesivoAgua)
    {
        double promedioPorcentajeConsumoExcesivo = kvp.Value.Average();
        Console.WriteLine($"El porcentaje de consumo excesivo de agua para el estrato {kvp.Key} es: {promedioPorcentajeConsumoExcesivo}%");
    }
}

void ContabilizarClientesConsumoAguaMayorPromedio()
{
    int promedioConsumoAgua = 25; // Valor constante del promedio de consumo de agua
    int clientesConConsumoMayorPromedio = clientes.Values.Count(cliente => cliente["consumo_actual_A"] > promedioConsumoAgua);
    Console.WriteLine($"El número de clientes con consumo de agua mayor al promedio es: {clientesConConsumoMayorPromedio}");
}

void MenuPrincipal()
{
    Console.WriteLine("¿Qué acción deseas realizar?");
    Console.WriteLine("1: Agregar un usuario Nuevo");
    Console.WriteLine("2: Calcular el precio a pagar por servicios de energía y agua");
    Console.WriteLine("3: Calcular el promedio del consumo actual de energía");
    Console.WriteLine("4: Calcular el valor total de descuentos otorgados por incentivo de energía");
    Console.WriteLine("5: Mostrar la cantidad total de mt3 de agua consumidos por encima del promedio");
    Console.WriteLine("6: Mostrar los porcentajes de consumo excesivo de agua por estrato");
    Console.WriteLine("7: Contabilizar los clientes que tuvieron un consumo de agua mayor al promedio");
    Console.WriteLine("0: Salir");
    Console.Write("Seleccione una opción: ");
    int opcion = Convert.ToInt32(Console.ReadLine());

    switch (opcion)
    {
        case 0:
            Console.WriteLine("Gracias por visitarnos. Hasta pronto");
            break;
        case 1:
            AgregarUsuario();
            break;
        case 2:
            CalcularPrecioAPagar();
            break;
        case 3:
            CalcularPromedioDeConsumo();
            break;
        case 4:
            CalcularTotalDescuentos();
            break;
        case 5:
            MostrarCantidadTotalAguaConsumidaPorEncimaDelPromedio();
            break;
        case 6:
            MostrarPorcentajesConsumoExcesivoAguaPorEstrato();
            break;
        case 7:
            ContabilizarClientesConsumoAguaMayorPromedio();
            break;
        default:
            Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
            break;
    }

    if (opcion != 0)
    {
        MenuPrincipal();
    }
}
