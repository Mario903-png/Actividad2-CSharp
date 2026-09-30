using Spectre.Console;

decimal monto = AnsiConsole.Ask<decimal>("Indica el monto del préstamo: ");
decimal tasaAnual = AnsiConsole.Ask<decimal>("Indica la tasa de interés anual (%): ");
int meses = AnsiConsole.Ask<int>("Indica el plazo en meses: ");

decimal tasaMensual = (tasaAnual / 100) / 12;

decimal cuota = monto *
    (tasaMensual * (decimal)Math.Pow((double)(1 + tasaMensual), meses)) /
    ((decimal)Math.Pow((double)(1 + tasaMensual), meses) - 1);

cuota = Math.Round(cuota, 2);

AnsiConsole.MarkupLine("\n[green]Tabla de amortización[/]\n");

var table = new Table();

table.AddColumn("No. de cuota");
table.AddColumn("Pago de cuota");
table.AddColumn("Interés");
table.AddColumn("Abono a capital");
table.AddColumn("Saldo");

decimal saldo = monto;

for (int i = 1; i <= meses; i++)
{
    decimal interes = Math.Round(saldo * tasaMensual, 2);
    decimal abonoCapital = Math.Round(cuota - interes, 2);

    saldo = Math.Round(saldo - abonoCapital, 2);

    if (i == meses)
    {
        saldo = 0;
    }

    table.AddRow(
        i.ToString(),
        cuota.ToString("N2"),
        interes.ToString("N2"),
        abonoCapital.ToString("N2"),
        saldo.ToString("N2")
    );
}

AnsiConsole.Write(table);