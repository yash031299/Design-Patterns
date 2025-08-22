using System;

namespace PaymentProcessing
{
    /// <summary>
    /// Interface representing a general payment method.
    /// All concrete payment methods will implement this interface.
    /// </summary>
    public interface IPaymentMethod
    {
        /// <summary>
        /// Processes the payment of a specified amount.
        /// </summary>
        /// <param name="amount">The amount to be paid.</param>
        void Pay(double amount);
    }

    /// <summary>
    /// Concrete implementation of the IPaymentMethod interface for CreditCard payments.
    /// </summary>
    public class CreditCard : IPaymentMethod
    {
        /// <summary>
        /// Processes the payment through a credit card.
        /// </summary>
        /// <param name="amount">The amount to be paid using a credit card.</param>
        public void Pay(double amount)
        {
            // Simulate calling the API for credit card payment processing
            Console.WriteLine($"Payment Processing from Credit Card for amount = {amount}");
        }
    }

    /// <summary>
    /// Concrete implementation of the IPaymentMethod interface for DebitCard payments.
    /// </summary>
    public class DebitCard : IPaymentMethod
    {
        /// <summary>
        /// Processes the payment through a debit card.
        /// </summary>
        /// <param name="amount">The amount to be paid using a debit card.</param>
        public void Pay(double amount)
        {
            // Simulate calling the API for debit card payment processing
            Console.WriteLine($"Payment Processing for Debit Card for amount = {amount}");
        }
    }

    /// <summary>
    /// Concrete implementation of the IPaymentMethod interface for UPI (Unified Payments Interface) payments.
    /// </summary>
    public class UPI : IPaymentMethod
    {
        /// <summary>
        /// Processes the payment through UPI.
        /// </summary>
        /// <param name="amount">The amount to be paid using UPI.</param>
        public void Pay(double amount)
        {
            // Simulate calling the API for UPI payment processing
            Console.WriteLine($"Payment Processing for UPI for amount = {amount}");
        }
    }

    /// <summary>
    /// Concrete implementation of the IPaymentMethod interface for NetBanking payments.
    /// </summary>
    public class NetBanking : IPaymentMethod
    {
        /// <summary>
        /// Processes the payment through NetBanking.
        /// </summary>
        /// <param name="amount">The amount to be paid using NetBanking.</param>
        public void Pay(double amount)
        {
            // Simulate calling the API for NetBanking payment processing
            Console.WriteLine($"Payment Processing for NetBanking for amount = {amount}");
        }
    }

    /// <summary>
    /// Factory class responsible for creating instances of different IPaymentMethod implementations.
    /// This class follows the Factory design pattern.
    /// </summary>
    public static class PaymentFactory
    {
        /// <summary>
        /// Creates and returns an instance of an IPaymentMethod based on the provided mode.
        /// </summary>
        /// <param name="mode">The payment mode (e.g., "creditcard", "debitcard", "upi", "netbanking").</param>
        /// <returns>An implementation of IPaymentMethod corresponding to the mode. Returns null if mode is unrecognized.</returns>
        public static IPaymentMethod CreatePayment(string mode)
        {
            // Return appropriate IPaymentMethod object based on the payment mode.
            if (string.Equals(mode, "creditcard", StringComparison.OrdinalIgnoreCase))
            {
                return new CreditCard();  // Return CreditCard object.
            }
            else if (string.Equals(mode, "debitcard", StringComparison.OrdinalIgnoreCase))
            {
                return new DebitCard();   // Return DebitCard object.
            }
            else if (string.Equals(mode, "upi", StringComparison.OrdinalIgnoreCase))
            {
                return new UPI();         // Return UPI object.
            }
            else if (string.Equals(mode, "netbanking", StringComparison.OrdinalIgnoreCase))
            {
                return new NetBanking();  // Return NetBanking object.
            }
            else
            {
                return null;  // Return null if the mode is invalid or unrecognized.
            }
        }
    }

    /// <summary>
    /// Main class to demonstrate the usage of the Factory Pattern for creating payment method objects.
    /// The class calls the factory method to create payment methods and then processes payments.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create a PaymentMethod instance for CreditCard and process a payment.
            IPaymentMethod paymentMethod = PaymentFactory.CreatePayment("creditcard");
            if (paymentMethod != null)
            {
                paymentMethod.Pay(102.22);  // Process payment of amount 102.22 using CreditCard.
            }

            // Create a PaymentMethod instance for UPI and process a payment.
            paymentMethod = PaymentFactory.CreatePayment("upi");
            if (paymentMethod != null)
            {
                paymentMethod.Pay(343.24);  // Process payment of amount 343.24 using UPI.
            }
        }
    }
}
