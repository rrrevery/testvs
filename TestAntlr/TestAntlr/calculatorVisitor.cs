using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace TestAntlr
{
    public class calculatorVisitor : calculatorBaseVisitor<object>
    {
        public override object VisitConstant([NotNull] calculatorParser.ConstantContext context)
        {
            if (context.PI() != null)
                return Math.PI;
            if (context.EULER() != null)
                return Math.E;
            return base.VisitConstant(context);
        }
        public override object VisitExpression([NotNull] calculatorParser.ExpressionContext context)
        {
            double left = Convert.ToDouble(Visit(context.multiplyingExpression(0)));
            int plus = 0;
            double right = 0;
            for (int i = 1; i < context.children.Count; i++)
            {
                right = 0;
                if (i % 2 == 1)
                {
                    //运算符
                    if (context.children[i].GetText() == "+")
                        plus = 1;
                    else
                        plus = 0;
                }
                if (i % 2 == 0)
                {
                    //表达式
                    right = Convert.ToDouble(Visit(context.children[i]));
                }
                if (plus == 1)
                    left += right;
                else
                    left -= right;
            }
            return left;
        }
        public override object VisitMultiplyingExpression([NotNull] calculatorParser.MultiplyingExpressionContext context)
        {
            //double res = 0;
            //double left = Convert.ToDouble(Visit(context.powExpression(0)));
            //double right = 1;
            //if (context.powExpression(1) != null)
            //    right = Convert.ToDouble(Visit(context.powExpression(1)));
            //if (context.TIMES() != null)
            //    res = left * right;
            //else
            //    res = left / right;
            //return res;

            double left = Convert.ToDouble(Visit(context.children[0]));
            int plus = 0;
            double right = 1;
            for (int i = 1; i < context.children.Count; i++)
            {
                right = 1;
                if (i % 2 == 1)
                {
                    //运算符
                    if (context.children[i].GetText() == "*")
                        plus = 1;
                    else
                        plus = 0;
                }
                if (i % 2 == 0)
                {
                    //表达式
                    right = Convert.ToDouble(Visit(context.children[i]));
                }
                if (plus == 1)
                    left *= right;
                else
                    left /= right;
            }
            return left;

        }
        public override object VisitPowExpression([NotNull] calculatorParser.PowExpressionContext context)
        {
            double res = 0;
            double left = Convert.ToDouble(Visit(context.signedAtom(0)));
            double right = 1;
            if (context.signedAtom(1) != null)
                right = Convert.ToDouble(Visit(context.signedAtom(1)));
            return Math.Pow(left, right);
        }
        public override object VisitSignedAtom([NotNull] calculatorParser.SignedAtomContext context)
        {
            double left = 0;
            if (context.func() != null)
                left = Convert.ToDouble(Visit(context.func()));
            else if (context.atom() != null)
                left = Convert.ToDouble(Visit(context.atom()));
            else if (context.signedAtom() != null)
                left = Convert.ToDouble(Visit(context.signedAtom()));
            if (context.MINUS() != null)
                return -left;
            else
                return left;
        }
        public override object VisitFunc([NotNull] calculatorParser.FuncContext context)
        {
            double res = 0;
            double left = Convert.ToDouble(Visit(context.expression(0)));
            double left_h = Math.PI * left / 180;
            double right = 0;
            if (context.expression().Length > 1)
                right = Convert.ToDouble(Visit(context.expression(1)));
            string func = context.funcname().GetText();
            switch (func)
            {
                case "cos": res = Math.Cos(left_h); break;
                case "tan": res = Math.Tan(left_h); break;
                case "sin": res = Math.Sin(left_h); break;
                case "acos": res = Math.Acos(left_h); break;
                case "atan": res = Math.Atan(left_h); break;
                case "asin": res = Math.Asin(left_h); break;
                case "log": res = Math.Log10(left); break;
                case "ln": res = Math.Log(left); break;
                case "sqrt": res = Math.Sqrt(left); break;
                case "abs": res = Math.Abs(left); break;
                case "round": res = Math.Round(left, Convert.ToInt32(right)); break;
            }
            return res;
        }
        public override object VisitAtom([NotNull] calculatorParser.AtomContext context)
        {
            if (context.LPAREN() != null)
            {
                return Visit(context.expression());
            }
            return base.VisitAtom(context);
        }
        public override object VisitScientific([NotNull] calculatorParser.ScientificContext context)
        {
            return context.SCIENTIFIC_NUMBER().GetText();
        }
    }
}
