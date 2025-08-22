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
    pay(amount: number): void;
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
    pay(amount: number): void {
        // Simulate calling the API for credit card payment processing
        console.log(`Payment Processing from Credit Card for amount = ${amount}`);
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
    pay(amount: number): void {
        // Simulate calling the API for debit card payment processing
        console.log(`Payment Processing for Debit Card for amount = ${amount}`);
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
    pay(amount: number): void {
        // Simulate calling the API for UPI payment processing
        console.log(`Payment Processing for UPI for amount = ${amount}`);
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
    pay(amount: number): void {
        // Simulate calling the API for NetBanking payment processing
        console.log(`Payment Processing for NetBanking for amount = ${amount}`);
    }
}

/**
 * Factory class responsible for creating instances of different PaymentMethod implementations.
 * This class follows the Factory design pattern.
 */
class PaymentFactory {
    /**
     * Creates and returns an instance of a PaymentMethod based on the provided mode.
     * 
     * @param mode The payment mode (e.g., "creditcard", "debitcard", "upi", "netbanking").
     * @returns A concrete implementation of PaymentMethod corresponding to the mode.
     *          Returns null if the mode is unrecognized.
     */
    static createPayment(mode: string): PaymentMethod | null {
        // Return appropriate PaymentMethod object based on the payment mode.
        if (mode.toLowerCase() === "creditcard") {
            return new CreditCard();  // Return CreditCard object.
        } else if (mode.toLowerCase() === "debitcard") {
            return new DebitCard();   // Return DebitCard object.
        } else if (mode.toLowerCase() === "upi") {
            return new UPI();         // Return UPI object.
        } else if (mode.toLowerCase() === "netbanking") {
            return new NetBanking();  // Return NetBanking object.
        } else {
            return null;  // Return null if the mode is invalid or unrecognized.
        }
    }
}

/**
 * Main function to demonstrate the usage of the Factory Pattern for creating payment method objects.
 * The function calls the factory method to create payment methods and then processes payments.
 */
function main(): void {
    // Create a PaymentMethod instance for CreditCard and process a payment.
    let paymentMethod = PaymentFactory.createPayment("creditcard");
    if (paymentMethod) {
        paymentMethod.pay(102.22);  // Process payment of amount 102.22 using CreditCard.
    }

    // Create a PaymentMethod instance for UPI and process a payment.
    paymentMethod = PaymentFactory.createPayment("upi");
    if (paymentMethod) {
        paymentMethod.pay(343.24);  // Process payment of amount 343.24 using UPI.
    }
}

// Run the main function
main();
