from abc import ABC, abstractmethod

class PaymentMethod(ABC):
    """
    Interface representing a general payment method.
    All concrete payment methods will implement this interface.
    """

    @abstractmethod
    def pay(self, amount: float):
        """
        Processes the payment of a specified amount.
        
        :param amount: The amount to be paid.
        """
        pass


class CreditCard(PaymentMethod):
    """
    Concrete implementation of the PaymentMethod interface for CreditCard payments.
    """

    def pay(self, amount: float):
        """
        Processes the payment through a credit card.

        :param amount: The amount to be paid using a credit card.
        """
        # Simulate calling the API for credit card payment processing
        print(f"Payment Processing from Credit Card for amount = {amount}")


class DebitCard(PaymentMethod):
    """
    Concrete implementation of the PaymentMethod interface for DebitCard payments.
    """

    def pay(self, amount: float):
        """
        Processes the payment through a debit card.

        :param amount: The amount to be paid using a debit card.
        """
        # Simulate calling the API for debit card payment processing
        print(f"Payment Processing for Debit Card for amount = {amount}")


class UPI(PaymentMethod):
    """
    Concrete implementation of the PaymentMethod interface for UPI (Unified Payments Interface) payments.
    """

    def pay(self, amount: float):
        """
        Processes the payment through UPI.

        :param amount: The amount to be paid using UPI.
        """
        # Simulate calling the API for UPI payment processing
        print(f"Payment Processing for UPI for amount = {amount}")


class NetBanking(PaymentMethod):
    """
    Concrete implementation of the PaymentMethod interface for NetBanking payments.
    """

    def pay(self, amount: float):
        """
        Processes the payment through NetBanking.

        :param amount: The amount to be paid using NetBanking.
        """
        # Simulate calling the API for NetBanking payment processing
        print(f"Payment Processing for NetBanking for amount = {amount}")


class PaymentFactory:
    """
    Factory class responsible for creating instances of different PaymentMethod implementations.
    This class follows the Factory design pattern.
    """

    @staticmethod
    def create_payment(mode: str) -> PaymentMethod:
        """
        Creates and returns an instance of a PaymentMethod based on the provided mode.
        
        :param mode: The payment mode (e.g., "creditcard", "debitcard", "upi", "netbanking").
        :return: A concrete implementation of PaymentMethod corresponding to the mode.
                 Returns None if the mode is unrecognized.
        """
        if mode.lower() == "creditcard":
            return CreditCard()  # Return CreditCard object.
        elif mode.lower() == "debitcard":
            return DebitCard()   # Return DebitCard object.
        elif mode.lower() == "upi":
            return UPI()         # Return UPI object.
        elif mode.lower() == "netbanking":
            return NetBanking()  # Return NetBanking object.
        else:
            return None  # Return None if the mode is invalid or unrecognized.


def main():
    """
    Main function to demonstrate the usage of the Factory Pattern for creating payment method objects.
    The function calls the factory method to create payment methods and then processes payments.
    """
    
    # Create a PaymentMethod instance for CreditCard and process a payment.
    payment_method = PaymentFactory.create_payment("creditcard")
    if payment_method is not None:
        payment_method.pay(102.22)  # Process payment of amount 102.22 using CreditCard.
    
    # Create a PaymentMethod instance for UPI and process a payment.
    payment_method = PaymentFactory.create_payment("upi")
    if payment_method is not None:
        payment_method.pay(343.24)  # Process payment of amount 343.24 using UPI.


if __name__ == "__main__":
    main()
