using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ICOMM
{
    public class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Positive Cases");
            Console.WriteLine("2. Positive Rate bigger than 30%");
            Console.WriteLine("Enter the desired option");
            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter the Year.");
                    int year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the Month.");
                    int month = int.Parse(Console.ReadLine());
                    Console.WriteLine(PositiveCases(month, year));
                    break;
                case 2:
                    List<dynamic> listaImprimir = PositivityRate30();
                    for (int i = 0; i < listaImprimir.Count; i++)
                    {
                        Console.WriteLine(listaImprimir[i].ToString());
                    }
                    break;
                default:
                    Console.WriteLine("Option entered incorrect");
                    break;
            }
            Console.ReadLine();
        }
        /// <summary>
        /// Summarize positives cases on a requested year and month
        /// </summary>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        /// <returns> Integer </returns>
        public static int PositiveCases(int month, int year)
        {
            int sum = 0;
            int resultado = 0;
            GetApi getApi = new GetApi();
            dynamic respuesta = getApi.Get("https://api.covidtracking.com/v1/us/daily.json");
            //Formato-> //añomesdia... aaaammddd
            for (int i = 0; i < respuesta.Count; i++)
            {
                if (month < 10)
                {
                    resultado = respuesta[i].date - int.Parse(year.ToString() + "0" + month.ToString() + "00");
                } else
                {
                    resultado = respuesta[i].date - int.Parse(year.ToString() + month.ToString() + "00");
                }
                if (resultado > 0 && resultado < 32)
                {
                    sum = sum + int.Parse(respuesta[i].positiveIncrease.ToString());
                }
            }
            return sum;
        }

        public static List<dynamic> PositivityRate30()
        {
            GetApi getApi = new GetApi();
            dynamic respuesta = getApi.Get("https://api.covidtracking.com/v1/us/daily.json");
            List<dynamic> lista = new List<dynamic>();
            for (int i = 0; i < respuesta.Count; i++)
            {
                bool resultado = int.Parse(respuesta[i].positiveIncrease.ToString()) > (int.Parse(respuesta[i].negativeIncrease.ToString()) * 0.3);
                if (resultado)
                {
                    lista.Add(respuesta[i]);
                }
            }
            return lista;
        }

    }
}
