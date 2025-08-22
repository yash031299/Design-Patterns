#include <iostream>
#include <string>
using namespace std;

/**
 * Interface representing a general payment method.
 * All concrete payment methods will implement this interface.
 */
class PaymentMethod {
public:
    /**
     * Processes the payment of a specified amount.
     * 
     * @param amount The amount to be paid.
     */
    virtual void pay(double amount) = 0;

    // Virtual destructor to ensure proper cleanup of derived classes.
    virtual ~PaymentMethod() {}
};

/**
 * Concrete implementation of the PaymentMethod interface for CreditCard payments.
 */
class CreditCard : public PaymentMethod {
public:
    /**
     * Processes the payment through a credit card.
     * 
     * @param amount The amount to be paid using a credit card.
     */
    void pay(double amount) override {
        // Simulate calling the API for credit card payment processing
        cout << "Payment Processing from Credit Card for amount = " << amount << endl;
    }
};

/**
 * Concrete implementation of the PaymentMethod interface for DebitCard payments.
 */
class DebitCard : public PaymentMethod {
public:
    /**
     * Processes the payment through a debit card.
     * 
     * @param amount The amount to be paid using a debit card.
     */
    void pay(double amount) override {
        // Simulate calling the API for debit card payment processing
        cout << "Payment Processing for Debit Card for amount = " << amount << endl;
    }
};

/**
 * Concrete implementation of the PaymentMethod interface for UPI (Unified Payments Interface) payments.
 */
class UPI : public PaymentMethod {
public:
    /**
     * Processes the payment through UPI.
     * 
     * @param amount The amount to be paid using UPI.
     */
    void pay(double amount) override {
        // Simulate calling the API for UPI payment processing
        cout << "Payment Processing for UPI for amount = " << amount << endl;
    }
};

/**
 * Concrete implementation of the PaymentMethod interface for NetBanking payments.
 */
class NetBanking : public PaymentMethod {
public:
    /**
     * Processes the payment through NetBanking.
     * 
     * @param amount The amount to be paid using NetBanking.
     */
    void pay(double amount) override {
        // Simulate calling the API for NetBanking payment processing
        cout << "Payment Processing for NetBanking for amount = " << amount << endl;
    }
};

/**
 * Factory class responsible for creating instances of different PaymentMethod implementations.
 * This class follows the Factory design pattern.
 */
class PaymentFactory {
public:
    /**
     * Creates and returns an instance of a PaymentMethod based on the provided mode.
     * 
     * @param mode The payment mode (e.g., "creditcard", "debitcard", "upi", "netbanking").
     * @return A concrete implementation of PaymentMethod corresponding to the mode.
     *         Returns nullptr if the mode is unrecognized.
     */
    static PaymentMethod* createPayment(const string& mode) {
        // Return appropriate PaymentMethod object based on the payment mode.
        if (mode == "creditcard") {
            return new CreditCard();  // Return CreditCard object.
        } else if (mode == "debitcard") {
            return new DebitCard();   // Return DebitCard object.
        } else if (mode == "upi") {
            return new UPI();         // Return UPI object.
        } else if (mode == "netbanking") {
            return new NetBanking();  // Return NetBanking object.
        } else {
            return nullptr;  // Return nullptr if the mode is invalid or unrecognized.
        }
    }
};

/**
 * Main function to demonstrate the usage of the Factory Pattern for creating payment method objects.
 * The function calls the factory method to create payment methods and then processes payments.
 */
int main() {

    // Create a PaymentMethod instance for CreditCard and process a payment.
    PaymentMethod* paymentMethod = PaymentFactory::createPayment("creditcard");
    if (paymentMethod != nullptr) {
        paymentMethod->pay(102.22); // Process payment of amount 102.22 using CreditCard.
        delete paymentMethod; // Clean up the created object.
    }

    // Create a PaymentMethod instance for UPI and process a payment.
    paymentMethod = PaymentFactory::createPayment("upi");
    if (paymentMethod != nullptr) {
        paymentMethod->pay(343.24); // Process payment of amount 343.24 using UPI.
        delete paymentMethod; // Clean up the created object.
    }

    return 0;
}
