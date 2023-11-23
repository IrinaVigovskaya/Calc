using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.CodeDom;
using System.Reflection;

namespace Calc
{
    public struct Result_operarion
    {
        public string Error;
        public string Result;
    }
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
            double x = 0;
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

    public partial class MainWindow : Window
    {
        private readonly Calcul _primer = new Calcul();
        public MainWindow()
        {
            InitializeComponent();

            foreach(UIElement elementCol in MainBody.Children)
            {
                if(elementCol is Button)
                {
                    ((Button)elementCol).Click += Button_Event_Click;
                }
            }

        }

        private void Button_Event_Click(object sender, RoutedEventArgs element_)
        {
            Result_operarion ans = new Result_operarion();
            Error_Message.Text = null;
            string part = (string)((Button)element_.OriginalSource).Content;
            char buf = '1';

            if (Example.Text != "") 
            { 
            buf = Example.Text[Example.Text.Length-1];
            }

            if (part == "Clear all")
            {
                Example.Text = "";
            }

            else if(buf == '('&&(part == "+"||part == "*" || part == "/" || part == ")"))
            {

            }

            else if((part == "+" || part == "-" || part == "*" || part == "/") && (buf == '+' || buf == '-' || buf == '*' || buf == '/'))
            {
                Example.Text = Example.Text.Remove(Example.Text.Length - 1);
                Example.Text += part;

            }
            else if(part == "=")
            {
                    ans.Result = _primer.CalculWork(Example.Text, ref ans).ToString();
                    if (ans.Error == null)
                {
                    /*if (counter == 18)
                    {
                        History.Text = null;
                        for (int i =0; i<17; i++)
                        {
                            HistoryArray[i] = HistoryArray[i+1];
                            History.Text += HistoryArray[i];
                        } 
                    }*/
                    History.Text += Example.Text + "=" + ans.Result+"\n";
                    //HistoryArray[counter] = Example.Text + "=" + ans.Result + "\n"; 
                    Example.Text = ans.Result;
                }
                else
                {
                    Error_Message.Text = ans.Error;
                }
            }

            else if(part == "Clear entry")
            {
                Example.Text = Example.Text.Remove(Example.Text.Length - 1);
            }
            else if(part == "Clear History")
            {
                History.Text = null;
                //counter = 0;
            }
            else
            Example.Text += part;
        }
    }
}
