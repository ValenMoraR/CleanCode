using System;
using System.Collections.Generic;
using System.Linq;
using PA2;

class Principal
{
    static List<Cliente> clientes = new List<Cliente>();
    static void Main()
    {
        Console.WriteLine("Bienvenido");
        MenuPrincipal();
    }
    static void AgregarUsuario()
    {
        Console.Write("Ingrese el número de cédula del cliente: ");
        string cedula = Console.ReadLine();
        Console.Write("Ingrese el estrato del cliente: ");
        int estrato = Convert.ToInt32(Console.ReadLine());
        Console.Write("Ingrese la meta de ahorro de energía del cliente: ");
        int metaAhorro = Convert.ToInt32(Console.ReadLine());
        Console.Write("Ingrese el consumo actual de energía del cliente: ");
        int consumoActualEnergia = Convert.ToInt32(Console.ReadLine());
        Console.Write("Ingrese el consumo actual de agua del cliente: ");
        int consumoActualAgua = Convert.ToInt32(Console.ReadLine());

        Cliente cliente = new Cliente
        {
            Cedula = cedula,
            Estrato = estrato,
            MetaAhorro = metaAhorro,
            ConsumoActualEnergia = consumoActualEnergia,
            ConsumoActualAgua = consumoActualAgua
        };

        clientes.Add(cliente);
        Console.WriteLine("Cliente agregado correctamente.");
    }
    static void ActualizarCliente() ///NUEVO
    {
        Console.Write("Ingrese el número de cédula del cliente que desea actualizar: ");
        string cedula = Console.ReadLine();
        Cliente cliente = clientes.FirstOrDefault(c => c.Cedula == cedula);  //EXPLICACIÓN 1(c.Cedula) Y 2 (FirstOrDefault
        if (cliente != null)
        {
            Console.WriteLine("Ingrese los nuevos datos del cliente:");
            Console.Write("Estrato: ");
            cliente.Estrato = Convert.ToInt32(Console.ReadLine());
            Console.Write("Meta de ahorro: ");
            cliente.MetaAhorro = Convert.ToInt32(Console.ReadLine());
            Console.Write("Consumo actual de energía: ");
            cliente.ConsumoActualEnergia = Convert.ToInt32(Console.ReadLine());
            Console.Write("Consumo actual de agua: ");
            cliente.ConsumoActualAgua = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Cliente actualizado correctamente.");
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
        }
    }
    static void EliminarCliente() ///NUEVO
    {
        Console.Write("Ingrese el número de cédula del cliente que desea eliminar: ");
        string cedula = Console.ReadLine();
        Cliente cliente = clientes.FirstOrDefault(c => c.Cedula == cedula);
        if (cliente != null)
        {
            clientes.Remove(cliente);
            Console.WriteLine("Cliente eliminado correctamente.");
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
        }
    }
    static void CalcularPrecioAPagar()
    {
        Console.Write("Ingrese el número de cédula del cliente: ");
        string cedula = Console.ReadLine();

        Cliente cliente = clientes.FirstOrDefault(c => c.Cedula == cedula);
        if (cliente != null)
        {
            int promConsumoAgua = 25;
            int valorKilovatio = 850;
            int valorParcial = cliente.ConsumoActualEnergia * valorKilovatio;
            int valorIncentivo = (cliente.MetaAhorro - cliente.ConsumoActualEnergia) * valorKilovatio;
            int valorPagarEnergia = valorParcial - valorIncentivo;

            int precioPorMt3 = 4600;
            int precioTotalAgua;
            int consumoActualAgua = cliente.ConsumoActualAgua;

            if (consumoActualAgua > promConsumoAgua)
            {
                int excesoAgua = consumoActualAgua - promConsumoAgua;
                int precioPorExcesoAgua = excesoAgua * (2 * precioPorMt3);
                precioTotalAgua = promConsumoAgua * precioPorMt3 + precioPorExcesoAgua;
            }
            else
            {
                precioTotalAgua = consumoActualAgua * precioPorMt3;
            }
            int precioTotal = valorPagarEnergia + precioTotalAgua;
            Console.WriteLine($"El valor a pagar del cliente {cedula} por consumo de energía es: ${valorPagarEnergia}");
            Console.WriteLine();
            Console.WriteLine($"El valor a pagar del cliente {cedula} por consumo de agua es: ${precioTotalAgua}");
            Console.WriteLine();
            Console.WriteLine($"El valor total a pagar por el cliente {cedula} es: ${precioTotal}");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
        }
    }
    static void CalcularPromedioDeConsumo()
    {
        int totalConsumo = clientes.Sum(c => c.ConsumoActualEnergia);
        double promedio = (double)totalConsumo / clientes.Count;
        Console.WriteLine($"El promedio del consumo actual de energía es: {promedio} kilovatios");
    }
    static void CalcularTotalDescuentos()
    {
        int totalDescuentos = 0;
        foreach (var cliente in clientes)
        {
            if (cliente.ConsumoActualEnergia < cliente.MetaAhorro)
            {
                totalDescuentos += (cliente.MetaAhorro - cliente.ConsumoActualEnergia) * 850;
            }
        }
        Console.WriteLine($"El valor total de descuentos otorgados es: ${totalDescuentos}");
    }

    static void MostrarCantidadTotalAguaConsumidaPorEncimaDelPromedio()
    {
        int promedioConsumoAgua = 25;
        int totalConsumoAguaPorEncimaDelPromedio = clientes
            .Where(cliente => cliente.ConsumoActualAgua > promedioConsumoAgua)
            .Sum(cliente => cliente.ConsumoActualAgua - promedioConsumoAgua);

        Console.WriteLine($"La cantidad total de mt3 de agua consumida por encima del promedio es: {totalConsumoAguaPorEncimaDelPromedio}");
    }
    static void MostrarPorcentajesConsumoExcesivoAguaPorEstrato()
    {
        Dictionary<int, List<double>> porcentajesConsumoExcesivoAgua = new Dictionary<int, List<double>>();
        foreach (var cliente in clientes)
        {
            int estrato = cliente.Estrato;
            int consumoActual = cliente.ConsumoActualAgua;
            double promedioConsumoAgua = clientes.Average(c => c.ConsumoActualAgua);

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
    static void ContabilizarClientesConsumoAguaMayorPromedio()
    {
        int promedioConsumoAgua = 25;
        int clientesConConsumoMayorPromedio = clientes.Count(cliente => cliente.ConsumoActualAgua > promedioConsumoAgua);
        Console.WriteLine($"El número de clientes con consumo de agua mayor al promedio es: {clientesConConsumoMayorPromedio}");
    }

    static void ClientesDesfase()
    {
        Cliente clienteMayorDesfase = null;
        int mayorDesfase = 0;

        foreach (var cliente in clientes)
        {
            int desfase = cliente.ConsumoActualEnergia - cliente.MetaAhorro;
            if (desfase > mayorDesfase)
            {
                mayorDesfase = desfase;
                clienteMayorDesfase = cliente;
            }
        }

        if (clienteMayorDesfase != null)
        {
            Console.WriteLine("Cliente con mayor desfase:");
            Console.WriteLine($"Cédula: {clienteMayorDesfase.Cedula}");
            Console.WriteLine($"Estrato: {clienteMayorDesfase.Estrato}");
            Console.WriteLine($"Meta de ahorro: {clienteMayorDesfase.MetaAhorro}");
            Console.WriteLine($"Consumo actual de energía: {clienteMayorDesfase.ConsumoActualEnergia}");
            Console.WriteLine($"Consumo actual de agua: {clienteMayorDesfase.ConsumoActualAgua}");
        }
        else
        {
            Console.WriteLine("No hay clientes registrados.");
        }
    }

    static void EstratoClientesMayorAhorroAgua()
    {
        List<int> ahorroPorEstrato = new List<int>(); // Inicializar la lista con un elemento para cada estrato

        for (int i = 0; i <= 7; i++) // Suponiendo que los estratos están en el rango de 1 a 7
        {
            ahorroPorEstrato.Add(0);
        }

        // Calcular el ahorro de agua para cada cliente y sumarlo al ahorro correspondiente al estrato
        foreach (var cliente in clientes)
        {
            int ahorroAgua = cliente.ConsumoActualAgua - 25; // Calcular el ahorro como diferencia con el promedio
            ahorroPorEstrato[cliente.Estrato - 1] += ahorroAgua; // Restar 1 para ajustar el índice de la lista al rango 0-5
        }

        // Encontrar el estrato con el mayor ahorro de agua
        int maxAhorro = ahorroPorEstrato.Max();
        int estratoMayorAhorro = ahorroPorEstrato.IndexOf(maxAhorro) + 1; // Sumar 1 para ajustar el estrato al rango 1-6

        if (maxAhorro > 0)
        {
            Console.WriteLine($"El estrato con el mayor ahorro de agua es: {estratoMayorAhorro}");
        }
        else
        {
            Console.WriteLine("No hay clientes registrados.");
        }

    }

    static void EstratoMayorYMenorConsumoAgua()
    {
        // Crear listas para almacenar el consumo de energía por estrato
        List<int> consumoEnergiaPorEstrato = new List<int>();

        // Inicializar las listas con ceros para cada estrato
        for (int i = 0; i < 6; i++) // Suponiendo que los estratos están en el rango de 1 a 6
        {
            consumoEnergiaPorEstrato.Add(0);
        }

        // Calcular el consumo de energía total para cada estrato
        foreach (var cliente in clientes)
        {
            consumoEnergiaPorEstrato[cliente.Estrato - 1] += cliente.ConsumoActualEnergia; // Restar 1 para ajustar el índice al rango 0-5
        }

        // Encontrar el máximo y mínimo consumo de energía y sus respectivos estratos
        int maxConsumo = consumoEnergiaPorEstrato.Max();
        int minConsumo = consumoEnergiaPorEstrato.Min();
        int estratoMayorConsumo = consumoEnergiaPorEstrato.IndexOf(maxConsumo) + 1; // Sumar 1 para ajustar el estrato al rango 1-6
        int estratoMenorConsumo = consumoEnergiaPorEstrato.IndexOf(minConsumo) + 1; // Sumar 1 para ajustar el estrato al rango 1-6

        Console.WriteLine($"El estrato con el mayor consumo de energía es: {estratoMayorConsumo}");
        Console.WriteLine($"El estrato con el menor consumo de energía es: {estratoMenorConsumo}");
    }

    static void ValorTotalPagarALaEmpresa()
    {
        int totalPagarEnergia = 0;
        int totalPagarAgua = 0;
        int totalPagar = 0;

        foreach (var cliente in clientes)
        {
            int valorKilovatio = 850;
            int valorParcialEnergia = cliente.ConsumoActualEnergia * valorKilovatio;
            int valorIncentivo = (cliente.MetaAhorro - cliente.ConsumoActualEnergia) * valorKilovatio;
            int valorPagarEnergia = valorParcialEnergia - valorIncentivo;
            totalPagarEnergia += valorPagarEnergia;

            int promConsumoAgua = 25;
            int precioPorMt3 = 4600;
            int consumoActualAgua = cliente.ConsumoActualAgua;
            int precioTotalAgua;

            if (consumoActualAgua > promConsumoAgua)
            {
                int excesoAgua = consumoActualAgua - promConsumoAgua;
                int precioPorExcesoAgua = excesoAgua * (2 * precioPorMt3);
                precioTotalAgua = promConsumoAgua * precioPorMt3 + precioPorExcesoAgua;
            }
            else
            {
                precioTotalAgua = consumoActualAgua * precioPorMt3;
            }
            totalPagarAgua += precioTotalAgua;
        }

        totalPagar = totalPagarEnergia + totalPagarAgua;

        Console.WriteLine($"Valor total a pagar por energía: ${totalPagarEnergia}");
        Console.WriteLine($"Valor total a pagar por agua: ${totalPagarAgua}");
        Console.WriteLine($"Valor total a pagar por energía y agua: ${totalPagar}");

    }

    static void MenuPrincipal()
    {
        Console.WriteLine("¿Qué acción deseas realizar?");
        Console.WriteLine("1: Agregar Nuevo usuario");
        Console.WriteLine("2: Actualizar información del usuario"); ///NUEVO**
        Console.WriteLine("3: Eliminar usuario");/// NUEVO **
        Console.WriteLine("4: Calcular el precio a pagar por servicios de energía y agua");
        Console.WriteLine("5: Calcular el promedio del consumo actual de energía");
        Console.WriteLine("6: Calcular el valor total de descuentos otorgados por incentivo de energía");
        Console.WriteLine("7: Mostrar la cantidad total de m3 de agua consumidos por encima del promedio");
        Console.WriteLine("8: Mostrar los porcentajes de consumo excesivo de agua por estrato");
        Console.WriteLine("9: Contabilizar los clientes que tuvieron un consumo de agua mayor al promedio");
        Console.WriteLine("10: Mostrar cliente con mayor desfase por meta de ahorro"); ///NUEVO
        Console.WriteLine("11: Mostrar el estrato en el cual los clientes ahorraron la mayor cantidad de agua");///NUNEVO
        Console.WriteLine("12: Mostrar el estrato con el mayor y menor consumo de energía");///NUEVO
        Console.WriteLine("13: Mostrar el valor total que los clientes le pagan a la empresa por concepto de energía y agua.");///NUEVO


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
                ActualizarCliente();
                break;
            case 3:
                EliminarCliente();
                break;
            case 4:
                CalcularPrecioAPagar();
                break;
            case 5:
                CalcularPromedioDeConsumo();
                break;
            case 6:
                CalcularTotalDescuentos();
                break;
            case 7:
                MostrarCantidadTotalAguaConsumidaPorEncimaDelPromedio();
                break;
            case 8:
                MostrarPorcentajesConsumoExcesivoAguaPorEstrato();
                break;
            case 9:
                ContabilizarClientesConsumoAguaMayorPromedio();
                break;
            case 10:
                ClientesDesfase();
                break;
            case 11:
                EstratoClientesMayorAhorroAgua();
                break;
            case 12:
                EstratoMayorYMenorConsumoAgua();
                break;
            case 13:
                ValorTotalPagarALaEmpresa();
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
}