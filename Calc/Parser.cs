using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calc
{
    public class Calcul
    {
        int Index_Symbol = 0;
        private string String_Example = "";
        int Current_Symbol;
        char Symbol_Buffer;

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
        public double CalculWork(string Primer, ref Result_operarion ans)
        {
            String_Example = Primer;
            Index_Symbol = 0;
            Get_Symbol();
            if (Current_Symbol == '\0')
            {
                ans.Error = "Empty Example";
                return 0;
            }
            else
            {
                return MethodA(ref ans);
            }
        }

        private double MethodA(ref Result_operarion ans) // + && -
        {
            double x = MethodB(ref ans);
            while (Current_Symbol == '+' || Current_Symbol == '-')
            {
                Symbol_Buffer = (char)Current_Symbol;
                Get_Symbol();
                if (Symbol_Buffer == '+')
                {
                    x += MethodB(ref ans);
                }
                else
                {
                    x -= MethodB(ref ans);
                }
            }
            return x;
        }

        private double MethodB(ref Result_operarion ans) // * && /
        {
            double x = MethodC(ref ans);
            while (Current_Symbol == '*' || Current_Symbol == '/')
            {
                Symbol_Buffer = (char)Current_Symbol;
                Get_Symbol();
                if (Symbol_Buffer == '*')
                {
                    x *= MethodC(ref ans);
                }
                else
                {
                    x /= MethodC(ref ans);
                }
            }
            return x;
        }

        private double MethodC(ref Result_operarion ans)  // ()
        {
            double x;
            if (Current_Symbol == '(')
            {
                Get_Symbol();
                x = MethodA(ref ans);
                if (Current_Symbol == ')')
                {
                    Get_Symbol();
                }
                else
                {
                    ans.Error = "Error! Missing brecket";
                    return 0;
                }
            }
            else if (Current_Symbol == '-')
            {
                Get_Symbol();
                x = -MethodC(ref ans);
            }
            else if (Current_Symbol >= '0' && Current_Symbol <= '9')
            {
                x = MethodD(ref ans);
            }
            else
            {
                ans.Error = "Error in recording the example";
                return 0;
            }
            return x;
        }

        private double MethodD(ref Result_operarion ans)
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
                    ans.Error = "Incorrect format";
                    return 0;
                }
            }
            return double.Parse(x);
        }
    }
}
