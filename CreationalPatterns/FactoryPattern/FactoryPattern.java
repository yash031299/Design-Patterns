/**
 * Interface representing a general payment method.
 * All concrete payment methods will implement this interface.
 */
interface PaymentMethod {
    
    /**
     * Processes the payment of a specified amount.
     * 
     * @param amount The amount to be paid.
     */
    public void pay(double amount);
}

/**
 * Concrete implementation of the PaymentMethod interface for CreditCard payments.
 */
class CreditCard implements PaymentMethod {

    /**
     * Processes the payment through a credit card.
     * 
     * @param amount The amount to be paid using a credit card.
     */
    @Override
    public void pay(double amount) {
        // Simulate calling the API for credit card payment processing
        System.out.println("Payment Processing from Credit Card for amount = " + amount);
    }
}

/**
 * Concrete implementation of the PaymentMethod interface for DebitCard payments.
 */
class DebitCard implements PaymentMethod {

    /**
     * Processes the payment through a debit card.
     * 
     * @param amount The amount to be paid using a debit card.
     */
    @Override
    public void pay(double amount) {
        // Simulate calling the API for debit card payment processing
        System.out.println("Payment Processing for the Debit Card for amount = " + amount);
    }
}

/**
 * Concrete implementation of the PaymentMethod interface for UPI (Unified Payments Interface) payments.
 */
class UPI implements PaymentMethod {

    /**
     * Processes the payment through UPI.
     * 
     * @param amount The amount to be paid using UPI.
     */
    @Override
    public void pay(double amount) {
        // Simulate calling the API for UPI payment processing
        System.out.println("Payment Processing for UPI for amount = " + amount);
    }
}

/**
 * Concrete implementation of the PaymentMethod interface for NetBanking payments.
 */
class NetBanking implements PaymentMethod {

    /**
     * Processes the payment through NetBanking.
     * 
     * @param amount The amount to be paid using NetBanking.
     */
    @Override
    public void pay(double amount) {
        // Simulate calling the API for NetBanking payment processing
        System.out.println("Payment Processing for NetBanking for amount = " + amount);
    }
}

/**
 * Factory class responsible for creating instances of different PaymentMethod implementations.
 * This class follows the Factory design pattern.
 */
class PaymentFactory {

    // Private constructor to prevent instantiation of the factory class.
    private PaymentFactory() {}

    /**
     * Creates and returns an instance of a PaymentMethod based on the provided mode.
     * 
     * @param mode The payment mode (e.g., "creditcard", "debitcard", "upi", "netbanking").
     * @return A concrete implementation of PaymentMethod corresponding to the mode.
     *         Returns null if the mode is unrecognized.
     */
    public static PaymentMethod createPayment(String mode) {

        // Return appropriate PaymentMethod object based on the payment mode.
        if (mode.equalsIgnoreCase("creditcard")) {
            return new CreditCard();  // Return CreditCard object.
        } else if (mode.equalsIgnoreCase("debitcard")) {
            return new DebitCard();   // Return DebitCard object.
        } else if (mode.equalsIgnoreCase("upi")) {
            return new UPI();         // Return UPI object.
        } else if (mode.equalsIgnoreCase("netbanking")) {
            return new NetBanking();  // Return NetBanking object.
        } else {
            return null;  // Return null if the mode is invalid or unrecognized.
        }
    }
}

/**
 * Main class to demonstrate the usage of the Factory Pattern for creating payment method objects.
 * The class calls the factory method to create payment methods and then processes payments.
 */
public class FactoryPattern {

    public static void main(String[] args) {

        // Create a PaymentMethod instance for CreditCard and process a payment.
        PaymentMethod paymentMethod = PaymentFactory.createPayment("CreditCard");
        if (paymentMethod != null) {
            paymentMethod.pay(102.22); // Process payment of amount 102.22 using CreditCard.
        }

        // Create a PaymentMethod instance for UPI and process a payment.
        paymentMethod = PaymentFactory.createPayment("UPI");
        if (paymentMethod != null) {
            paymentMethod.pay(343.24); // Process payment of amount 343.24 using UPI.
        }
    }
}
