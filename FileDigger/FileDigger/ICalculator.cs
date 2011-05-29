using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace FileDigger
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    interface ICalculator
    {
        [OperationContract]
        double LogIn(double n1, double n2);
        
        [OperationContract]
        List<String> findFile(String name);
    }
    public class CalculatorService : ICalculator
    {
        // Implement the ICalculator methods.
        public double Add(double n1, double n2)
        {
            double result = n1 + n2;
            return result;
        }

        public double Subtract(double n1, double n2)
        {
            double result = n1 - n2;
            return result;
        }

        public double Multiply(double n1, double n2)
        {
            double result = n1 * n2;
            return result;
        }

        public double Divide(double n1, double n2)
        {
            double result = n1 / n2;
            return result;
        }
    }

}
