using System;

namespace Calculator
{
    public class Calc<T> where T : struct, IComparable<T>
    {
        public delegate T OperationDelegate(T value1, T value2);

        public delegate void CalculationEventHandler(T result);
        public delegate void ErrorEventHandler(string msg);

        public event CalculationEventHandler OnCalculationComplete;
        public event ErrorEventHandler OnError;

        public T Add(T a, T b) => ExecOps(a, b, (x, y) => (dynamic)x + y);
        public T Subtract(T a, T b) => ExecOps(a, b, (x, y) => (dynamic)x - y);
        public T Multiply(T a, T b) => ExecOps(a, b, (x, y) => (dynamic)x * y);
        public T Divide(T a, T b) => ExecOps(a, b, (x, y) => (dynamic)x / y);

        public T ExecOps(T val1, T val2, OperationDelegate ops)
        {
            try
            {
                T result = ops(val1, val2);
                OnCalculationComplete?.Invoke(result);
                return result;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Error: {ex.Message}");
                return default(T);
            }
        }
        public T CustomOps(T val1, T val2, OperationDelegate ops)
        {
            return ExecOps(val1, val2, ops);
        }

    }
}
