﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Z.Expressions;

namespace TestAntlr
{
    public static class calculator
    {
        public static string Calc(string expression)
        {
            string input = expression;

            var stream = new AntlrInputStream(input);
            var lexer = new calculatorLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new calculatorParser(tokens);
            var tree = parser.expression();

            var visitor = new calculatorVisitor();
            var result = visitor.Visit(tree);

            //textBox2.Text += tree.ToStringTree(parser)+ "\r\n";
            return result.ToString();
        }
        public static double Calc2(string expression)
        {
            string input = expression;

            var stream = new AntlrInputStream(input);
            var lexer = new calculatorLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new calculatorParser(tokens);
            var tree = parser.expression();

            var visitor = new calculatorVisitor();
            var result = visitor.Visit(tree);

            //textBox2.Text += tree.ToStringTree(parser)+ "\r\n";
            return double.Parse(result.ToString());
        }

        public static double Calc3(string expression)
        {
            return expression.Execute<double>();
        }
    }
}
