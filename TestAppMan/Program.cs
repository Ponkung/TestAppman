using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestAppMan
{
    class Program
    {
        static void Main(string[] args)
        {
            //BingoGame();
            FormulaCalculation();
        }

        private static void BingoGame()
        {
            int[] bingo1 = new int[] { 1, 2, 3, 4, 5 };
            int[] bingo2 = new int[] { 6, 7, 8, 9, 10 };
            int[] bingo3 = new int[] { 11, 12, 13, 14, 15 };
            int[] bingo4 = new int[] { 16, 17, 18, 19, 20 };
            int[] bingo5 = new int[] { 21, 22, 23, 24, 25 };
            int[] bingo6 = new int[] { 1, 6, 11, 16, 21 };
            int[] bingo7 = new int[] { 2, 7, 12, 17, 22 };
            int[] bingo8 = new int[] { 3, 8, 13, 18, 23 };
            int[] bingo9 = new int[] { 4, 9, 14, 19, 24 };
            int[] bingo10 = new int[] { 5, 10, 15, 20, 25 };
            int[] bingo11 = new int[] { 1, 7, 13, 19, 25 };
            int[] bingo12 = new int[] { 5, 9, 13, 17, 21 };


            List<int[]> bingoValues = new List<int[]>() { bingo1, bingo2, bingo3, bingo4, bingo5, bingo6, bingo7, bingo8, bingo9, bingo10, bingo11, bingo12 };

            Console.WriteLine("Hello! Please enter minimal 5 numbers between 1-25, example 1,2,3,4,5 ");
            try
            {
                string[] numberInput = Console.ReadLine().Split(',');
                if (numberInput.Length < 5)
                {
                    Console.WriteLine("You may enter more 5 numbers or must have comma between number");
                    return;
                }
                List<int> numberInputData = new List<int>();
                int index = 0;
                foreach (string number in numberInput)
                {
                    numberInputData.Add(Convert.ToInt32(number));
                    index++;
                }

                if (IsBingo(bingoValues, numberInputData))
                {
                    Console.WriteLine("Bingo");
                }
                else
                {
                    Console.WriteLine("Not Bingo");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool IsBingo(List<int[]> bingoValues, List<int> numberInputData)
        {
            foreach (int[] val in bingoValues)
            {
                int numberContains = 0;
                foreach (int inputData in numberInputData)
                {
                    if (val.Contains(inputData))
                    {
                        numberContains++;
                    }
                }
                if (numberContains == 5)
                {
                    return true;
                }

            }
            return false;
        }

        private static void FormulaCalculation() {
            Console.WriteLine("Hello! Please input formula, example (22*2) + 50 ");
            string input = Console.ReadLine();
            string dataInput = input.Replace(" ", ""); ;
            var ans = Evaluate(dataInput);
            Console.WriteLine(input + " Result: " + ans);
        }

        public static double Evaluate(String expr)
        {

            Stack<String> stack = new Stack<String>();

            string value = "";
            for (int i = 0; i < expr.Length; i++)
            {
                String s = expr.Substring(i, 1);
                char chr = s.ToCharArray()[0];

                if (!char.IsDigit(chr) && chr != '.' && value != "")
                {
                    stack.Push(value);
                    value = "";
                }

                if (s.Equals("("))
                {

                    string innerExp = "";
                    i++; //Fetch Next Character
                    int bracketCount = 0;
                    for (; i < expr.Length; i++)
                    {
                        s = expr.Substring(i, 1);

                        if (s.Equals("("))
                            bracketCount++;

                        if (s.Equals(")"))
                            if (bracketCount == 0)
                                break;
                            else
                                bracketCount--;


                        innerExp += s;
                    }

                    stack.Push(Evaluate(innerExp).ToString());

                }
                else if (s.Equals("+")) stack.Push(s);
                else if (s.Equals("-")) stack.Push(s);
                else if (s.Equals("*")) stack.Push(s);
                else if (s.Equals("/")) stack.Push(s);
                else if (s.Equals("sqrt")) stack.Push(s);
                else if (s.Equals(")"))
                {
                }
                else if (char.IsDigit(chr) || chr == '.')
                {
                    value += s;

                    if (value.Split('.').Length > 2)
                        throw new Exception("Invalid decimal.");

                    if (i == (expr.Length - 1))
                        stack.Push(value);

                }
                else
                    throw new Exception("Invalid character.");

            }

            double result = 0;
            while (stack.Count >= 3)
            {

                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());

                if (op == "+") result = left + right;
                else if (op == "+") result = left + right;
                else if (op == "-") result = left - right;
                else if (op == "*") result = left * right;
                else if (op == "/") result = left / right;

                stack.Push(result.ToString());
            }


            return Convert.ToDouble(stack.Pop());
        }
    }
}
