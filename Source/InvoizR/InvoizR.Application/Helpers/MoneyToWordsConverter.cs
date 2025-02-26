namespace InvoizR.Application.Helpers;

public static class MoneyToWordsConverter
{
    private static string NumberToLetters(decimal value)
    {
        var expression = string.Empty;

        value = Math.Truncate(value);

        if (value == 0)
            expression = "CERO";
        else if (value == 1)
            expression = "UNO";
        else if (value == 2)
            expression = "DOS";
        else if (value == 3)
            expression = "TRES";
        else if (value == 4)
            expression = "CUATRO";
        else if (value == 5)
            expression = "CINCO";
        else if (value == 6)
            expression = "SEIS";
        else if (value == 7)
            expression = "SIETE";
        else if (value == 8)
            expression = "OCHO";
        else if (value == 9)
            expression = "NUEVE";
        else if (value == 10)
            expression = "DIEZ";
        else if (value == 11)
            expression = "ONCE";
        else if (value == 12)
            expression = "DOCE";
        else if (value == 13)
            expression = "TRECE";
        else if (value == 14)
            expression = "CATORCE";
        else if (value == 15)
            expression = "QUINCE";
        else if (value < 20)
            expression = "DIECI" + NumberToLetters(value - 10);
        else if (value == 20)
            expression = "VEINTE";
        else if (value < 30)
            expression = "VEINTI" + NumberToLetters(value - 20);
        else if (value == 30)
            expression = "TREINTA";
        else if (value == 40)
            expression = "CUARENTA";
        else if (value == 50)
            expression = "CINCUENTA";
        else if (value == 60)
            expression = "SESENTA";
        else if (value == 70)
            expression = "SETENTA";
        else if (value == 80)
            expression = "OCHENTA";
        else if (value == 90)
            expression = "NOVENTA";
        else if (value < 100)
            expression = NumberToLetters(Math.Truncate(value / 10) * 10) + " Y " + NumberToLetters(value % 10);
        else if (value == 100)
            expression = "CIEN";
        else if (value < 200)
            expression = "CIENTO " + NumberToLetters(value - 100);
        else if (value == 200 || value == 300 || value == 400 || value == 600 || value == 800)
            expression = NumberToLetters(Math.Truncate(value / 100)) + "CIENTOS";
        else if (value == 500)
            expression = "QUINIENTOS";
        else if (value == 700)
            expression = "SETECIENTOS";
        else if (value == 900)
            expression = "NOVECIENTOS";
        else if (value < 1000)
            expression = NumberToLetters(Math.Truncate(value / 100) * 100) + " " + NumberToLetters(value % 100);
        else if (value == 1000)
            expression = "MIL";
        else if (value < 2000)
            expression = "MIL " + NumberToLetters(value % 1000);
        else if (value < 1000000)
        {
            expression = NumberToLetters(Math.Truncate(value / 1000)) + " MIL";
            if (value % 1000 > 0)
                expression = expression + " " + NumberToLetters(value % 1000);
        }
        else if (value == 1000000)
        {
            expression = "UN MILLÓN";
        }
        else if (value < 2000000)
        {
            expression = "UN MILLÓN " + NumberToLetters(value % 1000000);
        }
        else if (value < 1000000000000)
        {
            expression = NumberToLetters(Math.Truncate(value / 1000000)) + " MILLONES ";
            if (value - Math.Truncate(value / 1000000) * 1000000 > 0)
                expression = expression + " " + NumberToLetters(value - Math.Truncate(value / 1000000) * 1000000);
        }
        else if (value == 1000000000000)
            expression = "UN BILLON";
        else if (value < 2000000000000)
            expression = "UN BILLÓN " + NumberToLetters(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        else
        {
            expression = NumberToLetters(Math.Truncate(value / 1000000000000)) + " BILLONES";
            if (value - Math.Truncate(value / 1000000000000) * 1000000000000 > 0)
                expression = expression + " " + NumberToLetters(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        }

        return expression;
    }

    public static string SpellingNumber(this decimal? amount)
    {
        var integer = Convert.ToInt64(Math.Truncate(amount ?? 0));
        var decimals = Convert.ToInt32(Math.Round(((amount ?? 0) - integer) * 100, 2));
        var dec = decimals > 0 ? $" DÓLARES {decimals:0,0}/100" : $" DÓLARES {decimals:0,0}/100";
        return NumberToLetters(Convert.ToDecimal(integer)) + dec;
    }

    public static string SpellingNumber(this double? amount)
        => SpellingNumber((decimal)amount);
}
