using System;

namespace CalcLogorifm
{
    public interface IOperationFactory
    {
        IOperation Create();
    }
    public class NaturalLogarithmOperationFactory : IOperationFactory
    {
        public IOperation Create()
        {
            return new NaturalLogarithmOperation();
        }
    }
    public class LogarithmBase10OperationFactory : IOperationFactory
    {
        public IOperation Create()
        {
            return new LogarithmBase10Operation();
        }
    }
    

    
    public interface IOperationVisitor
    {
        void Visit(NaturalLogarithmOperation operation);
        void Visit(LogarithmBase10Operation operation);
    }
    public class CalculatorVisitor : IOperationVisitor
    {
        private double result;

        public double Result
        {
            get { return result; }
            set { ; }
        }

        public void Visit(NaturalLogarithmOperation operation)
        {
            result = operation.Execute(result);
        }

        public void Visit(LogarithmBase10Operation operation)
        {
            result = operation.Execute(result);
        }
    }
    public interface IOperation
    {
        void Accept(IOperationVisitor visitor);
    }
    public class NaturalLogarithmOperation : IOperation
    {
        public void Accept(IOperationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public double Execute(double number)
        {
            return Math.Log(number);
        }
    }
    public class LogarithmBase10Operation : IOperation
    {
        public void Accept(IOperationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public double Execute(double number)
        {
            return Math.Log10(number);
        }
    }
    public class Calculator
    {
        public static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            Console.WriteLine("Enter a number:");
            double number = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Select an operation:");
            Console.WriteLine("1. Natural Logarithm");
            Console.WriteLine("2. Logarithm Base 10");

            int choice = Convert.ToInt32(Console.ReadLine());

            IOperation operation = null;
            switch (choice)
            {
                case 1:
                    operation = new NaturalLogarithmOperation();
                    break;
                case 2:
                    operation = new LogarithmBase10Operation();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            calculator.SetOperation(operation);

            try
            {
                double result = calculator.Calculate(number);
                Console.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadKey();
        }
        private IOperation operation;

        public void SetOperation(IOperation operation)
        {
            this.operation = operation;
        }

        public double Calculate(double number)
        {
            if (operation == null)
            {
                throw new InvalidOperationException("Operation is not set.");
            }

            CalculatorVisitor visitor = new CalculatorVisitor();
            visitor.Result = number;

            operation.Accept(visitor);

            return visitor.Result;
        }
    }
}