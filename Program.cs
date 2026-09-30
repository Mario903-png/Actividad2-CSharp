using Spectre.Console;

// Pedimos los datos del préstamo
decimal monto = AnsiConsole.Ask<decimal>("Indica el monto del préstamo: ");
decimal tasaAnual = AnsiConsole.Ask<decimal>("Indica la tasa de interés anual (%): ");
int meses = AnsiConsole.Ask<int>("Indica el plazo en meses: ");

// Convertimos la tasa anual a mensual
decimal tasaMensual = (tasaAnual / 100) / 12;

// Calculamos la cuota fija mensual
decimal cuota = monto *
    (tasaMensual * (decimal)Math.Pow((double)(1 + tasaMensual), meses)) /
    ((decimal)Math.Pow((double)(1 + tasaMensual), meses) - 1);

cuota = Math.Round(cuota, 2);

// Creamos la tabla para mostrar los resultados
var table = new Table();

table.AddColumn("No. de cuota");
table.AddColumn("Pago de cuota");
table.AddColumn("Interés");
table.AddColumn("Abono a capital");
table.AddColumn("Saldo");

// Guardamos el monto inicial como saldo
decimal saldo = monto;

// Repetimos el cálculo para cada mes
for (int i = 1; i <= meses; i++)
{
    // Calculamos el interés del mes
    decimal interes = Math.Round(saldo * tasaMensual, 2);

    // Calculamos el abono al capital
    decimal abonoCapital = Math.Round(cuota - interes, 2);

    // Actualizamos el saldo pendiente
    saldo = Math.Round(saldo - abonoCapital, 2);

    // Agregamos los resultados a la tabla
    table.AddRow(
        i.ToString(),
        cuota.ToString("N2"),
        interes.ToString("N2"),
        abonoCapital.ToString("N2"),
        saldo.ToString("N2")
    );
}

// Mostramos la tabla en la consola
AnsiConsole.Write(table);
