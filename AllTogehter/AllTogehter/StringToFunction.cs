using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTogether
{
    public class StringToFunction
    {
        private static string[] _operators = { "-", "+", "/", "*", "^" };   // array of all possible operator
        private static Func<double, double, double>[] _operations = {       // tupel array with the functions of operators
        (a1, a2) => a1 - a2,
        (a1, a2) => a1 + a2,
        (a1, a2) => a1 / a2,
        (a1, a2) => a1 * a2,
        (a1, a2) => Math.Pow(a1, a2)
        };

        public static double Eval(string expression, string firstElementMinus)
        {
            List<string> tokens = getTokens(expression);                    // expression to a string list
            Stack<double> operandStack = new Stack<double>();               // stack of operands
            Stack<string> operatorStack = new Stack<string>();              // stack of operators
            int tokenIndex = 0;

            while (tokenIndex < tokens.Count)
            {
                string token = tokens[tokenIndex];                          // start tokens check at the first entry
                if(SubExpressionTest(ref operandStack, tokens, token, firstElementMinus,ref tokenIndex))
                {
                    continue;
                }
                if (Array.IndexOf(_operators, token) >= 0)                  //If this is an operator
                {
                    if (Array.IndexOf(_operators, token) == 0)              // if negativ number
                    {
                        if (tokenIndex == 0 && _operators.Contains(tokens[tokenIndex + 1]))
                        {
                            firstElementMinus = tokens[tokenIndex + 1];
                        }
                        else if (tokenIndex == 0 && !_operators.Contains(tokens[tokenIndex + 1]) || tokenIndex != 0 && _operators.Contains(tokens[tokenIndex - 1]))
                        {
                            if (tokens[tokenIndex + 1] == "(" || tokens[tokenIndex + 1] == ")")
                            {
                               operatorStack.Push(token);
                            }
                            else
                            {
                                tokens[tokenIndex + 1] = (Convert.ToSingle(tokens[tokenIndex + 1]) * -1).ToString();
                            }
                        }
                        else
                        {
                            CheckEvalHierarchy(operatorStack, operandStack, token);
                        }
                    }
                    else
                    {
                        CheckEvalHierarchy(operatorStack, operandStack, token);
                    }
                }
                else                                                        // if its a number/operand
                {
                    operandStack.Push(double.Parse(token));                 // add it to operandStack
                }
                tokenIndex += 1;
            }
            return CalculateFunction(operatorStack, operandStack, firstElementMinus);
        }

        private static bool SubExpressionTest(ref Stack<double> operandStack, List<string> tokens, string token, string firstElementMinus,ref int tokenIndex)
        {
            if (token == "(")                                           // if ( new subexpression
            {
                string subExpr = getSubExpression(tokens, ref tokenIndex);
                operandStack.Push(Eval(subExpr, firstElementMinus));
                tokenIndex += 1;
                return true;
            }
            else if (token == ")")                                           // not possible if not a ( was before
            {
                return true;
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            return false;
        }

        private static string getSubExpression(List<string> tokens, ref int index)
        {
            StringBuilder subExpr = new StringBuilder();
            int parenlevels = 1;                                        // initialise with 1 because there musst been a ( before or the method does not triggered
            index += 1;
            while (index < tokens.Count && parenlevels > 0)
            {
                string token = tokens[index];
                if (tokens[index] == "(")                               // if ( parenlevels +1 if ) parenlevels -1
                {
                    parenlevels += 1;
                }

                if (tokens[index] == ")")
                {
                    parenlevels -= 1;
                }

                if (parenlevels > 0)                                    // if more ( than ) 
                {
                    subExpr.Append(token);                              // add the testet token to subexpr
                }

                index += 1;
            }

            if ((parenlevels > 0))                                      // at the end its not possible if count is >0 because then there are more ( than )
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            return subExpr.ToString();                                  
        }

        private static List<string> getTokens(string expression)
        {
            string operators = "()^*/+-";
            List<string> tokens = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (char c in expression.Replace(" ", string.Empty)) // delete spacebarentries
            {
                if (operators.IndexOf(c) >= 0)                        // check if c an operator is
                {
                    if ((sb.Length > 0))                              // if something was added to sb before this query  
                    {                                                 // add sb to tokens (each sb is one number e.g "10")
                        tokens.Add(sb.ToString());
                        sb.Length = 0;                                // reset sb
                    }
                    tokens.Add(c.ToString());                         // add operator to tokens
                }
                else
                {                                                      // if c is not an operator add c to sb
                    sb.Append(c);
                }
            }

            if ((sb.Length > 0))                                        // check the last sb (number)
            {
                tokens.Add(sb.ToString());                              // add the last number 
            }
            return tokens;
        }

        private static void CheckEvalHierarchy(Stack<string> operatorStack, Stack<double> operandStack, string token)
        {
            while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()) && operatorStack.Peek() != _operators[1])
            {
                operandStack.Push(CalculateSmallFunction1(operatorStack, operandStack));
            }
            operatorStack.Push(token);
        }

        public static double CalculateSmallFunction1(Stack<string> operatorStack, Stack<double> operandStack)
        {
            string op = operatorStack.Pop();
            double arg2 = operandStack.Pop();
            double arg1 = operandStack.Pop();

            operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));

            return operandStack.Pop();
        }
        
        public static double CalculateSmallFuntion2(Stack<string> revOperatorStack, Stack<double> revOperandStack)
        {
            string op = revOperatorStack.Pop();
            double arg1 = revOperandStack.Pop();
            double arg2 = revOperandStack.Pop();

            revOperandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));

            return revOperandStack.Pop();
        }

        public static double CalculateFunction(Stack<string> operatorStack, Stack<double> operandStack, string firstElementMinus)
        {
            double result = 0;
            if (operatorStack.Count == 0)
            {
                result = NoOperatorOnStack(operandStack, result, firstElementMinus);
            }
            else if (operatorStack.Count == 1)
            {
                result = OneOperatorOnStack(operatorStack, operandStack, result, firstElementMinus);
            }
            else
            {
                result = MoreThanOneOperatorOnStack(operatorStack, operandStack, result, firstElementMinus);
            }
            return result;
        }

        private static double NoOperatorOnStack(Stack<double> operandStack, double result, string firstElementMinus)
        {
            if (firstElementMinus != string.Empty)
            {
                firstElementMinus = string.Empty;
                result = (operandStack.Pop() * -1);
            }
            else
            {
                result = operandStack.Pop();
            }
            return result;
        }

        private static double OneOperatorOnStack(Stack<string> operatorStack, Stack<double> operandStack, double result, string firstElementMinus)
        {
            if (operandStack.Count == 1)
            {
                operatorStack.Pop();
                result = (operandStack.Pop() * -1);
            }
            if (firstElementMinus != string.Empty)
            {
                result = CalculateSmallFunction1(operatorStack, operandStack) * -1;
            }
            else
            {
                result = CalculateSmallFunction1(operatorStack, operandStack);
            }
            return result;
        }

        private static double MoreThanOneOperatorOnStack(Stack<string> operatorStack, Stack<double> operandStack, double result, string firstElementMinus)
        {
            Stack<string> revOperatorStack = new Stack<string>();
            Stack<double> revOperandStack = new Stack<double>();
            if (operatorStack.Peek() == "^")
            {
                operandStack.Push(CalculateSmallFunction1(operatorStack, operandStack));
            }
            if ((operatorStack.Peek() == "/" || operatorStack.Peek() == "*"))
            {
                while (operatorStack.Peek() == "/" || operatorStack.Peek() == "*")
                {
                    revOperatorStack.Push(operatorStack.Pop());
                    revOperandStack.Push(operandStack.Pop());
                    if (operatorStack.Count == 0)
                    {
                        break;
                    }
                }
                revOperandStack.Push(operandStack.Pop());
            }
            while (revOperatorStack.Count > 0)
            {
                operandStack.Push(CalculateSmallFuntion2(revOperatorStack, revOperandStack));
            }
            while (operatorStack.Count > 0)
            {
                revOperatorStack.Push(operatorStack.Pop());
                revOperandStack.Push(operandStack.Pop());
            }
            try
            {
                revOperandStack.Push(operandStack.Pop());
            }
            catch
            {
            }
            if (firstElementMinus != string.Empty)
            {
                revOperandStack.Push(revOperandStack.Pop() * -1);
                firstElementMinus = string.Empty;
            }
            while (revOperatorStack.Count > 0)
            {
                if (revOperandStack.Count == 1)
                {
                    revOperatorStack.Pop();
                    revOperandStack.Push(revOperandStack.Pop() * -1);
                }
                else
                {
                    revOperandStack.Push(CalculateSmallFuntion2(revOperatorStack, revOperandStack));
                }
            }
            if (firstElementMinus != string.Empty)
            {
                revOperandStack.Push(revOperandStack.Pop() * -1);
            }
            return result = revOperandStack.Pop();
        }

    }
}
