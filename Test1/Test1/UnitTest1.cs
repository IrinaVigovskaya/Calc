/*
    S→I:=E;
    E→E"|"T|T
    T→T&M|M
    M→~M|(E)|I|C
    I→AK|A
    K→AK|DK|A|D 
    C→D
 */

namespace Test1
{
    public class CalculTests
    {
        private readonly Calcul _primer = new Calcul();

        /*[Test]
        public void Calcul_ShouldReturnZero_IfStringIsNull()
        {
            string primer = "";
            var result = _primer.CalculWork(primer);
            Assert.AreEqual(result, 0);
        }

 
        [Test]
        public void Calcul_ShouldReturnSummOfTwoNumbers_IfThePlusSign()
        {
            string primer = "8+11";
            var result = _primer.CalculWork(primer);
            Console.WriteLine(result);
            Assert.AreEqual(result, 19);
        }

        [Test]
        public void Calcul_ShouldReturnNegativeNumber_IfTheNumberIsLessThanZero()
        {
            string primer = "-4";
            var result = _primer.CalculWork(primer);
            Assert.AreEqual(result, -4);
        }

        [Test]
        public void Calcul_ShouldReturnDifference_IfTheMinusSign()
        {
            string primer = "1-4";
            var result = _primer.CalculWork(primer);
            Assert.AreEqual(result, -3);
        }

        [Test]
        public void Calcul_ShouldReturnMultiply_IfTheMultiplicationSign()
        {
            string primer = "10*2,456";
            var result = _primer.CalculWork(primer);
            Assert.AreEqual(result, 24.56);
        }

        [Test]
        public void Calcul_ShouldReturnDivide_IfTheDivisionSign()
        {
            string primer = "10/5";
            var result = _primer.CalculWork(primer);
            Assert.AreEqual(result, 2);
        }

        [Test]
        public void Calcul_SholdReturnTheCorrectAnswer_IfConsideringThePriorityOfOperations()
        {
            string primer = "-(5*4)/2";
            var result = _primer.CalculWork(primer);
            Assert.AreEqual(result, -10);
        }*/

        [TestCase("", 0)]
        [TestCase("8+11", 19)]
        [TestCase("-4", -4)]
        [TestCase("1-4", -3)]
        [TestCase("10*2,456", 24.56)]
        [TestCase("10/5", 2)]
        [TestCase("(5*4)", 20)]
        [TestCase("-(5*4)/2", -10)]
        public void Calcul(string primer, double expected)
        {
            var result = _primer.CalculWork(primer);
            Assert.AreEqual(result, expected);
        }
    }

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
            if (Index_Symbol+1 > String_Example.Length)
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
            while(Current_Symbol == '+' || Current_Symbol == '-')
            {
                Symbol_Buffer = (char)Current_Symbol;
                Get_Symbol();
                if(Symbol_Buffer == '+')
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
           if(Current_Symbol == '(')
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
           else if(Current_Symbol == '-')
            {
                Get_Symbol();
                x = -MethodC();
            }
            else if(Current_Symbol >= '0' && Current_Symbol<= '9')
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
            while(Current_Symbol >= '0' && Current_Symbol <= '9')
            {
                x += (char)Current_Symbol;
                Get_Symbol();
                if(Current_Symbol == ',')
                {
                    x += (char)Current_Symbol;
                    Get_Symbol() ;
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
}