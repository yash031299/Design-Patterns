# Module defining the payment method behavior
# All concrete payment methods will include this module
module PaymentMethod
  # Processes the payment of a specified amount
  # @param amount [Float] The amount to be paid
  def pay(amount)
    raise NotImplementedError, "Subclasses must implement the pay method"
  end
end

# Class implementing PaymentMethod for CreditCard payments
class CreditCard
  include PaymentMethod

  # Processes the payment through a credit card
  # @param amount [Float] The amount to be paid using a credit card
  def pay(amount)
    # Simulate calling the API for credit card payment processing
    puts "Payment Processing from Credit Card for amount = #{amount}"
  end
end

# Class implementing PaymentMethod for DebitCard payments
class DebitCard
  include PaymentMethod

  # Processes the payment through a debit card
  # @param amount [Float] The amount to be paid using a debit card
  def pay(amount)
    # Simulate calling the API for debit card payment processing
    puts "Payment Processing for the Debit Card for amount = #{amount}"
  end
end

# Class implementing PaymentMethod for UPI (Unified Payments Interface) payments
class UPI
  include PaymentMethod

  # Processes the payment through UPI
  # @param amount [Float] The amount to be paid using UPI
  def pay(amount)
    # Simulate calling the API for UPI payment processing
    puts "Payment Processing for UPI for amount = #{amount}"
  end
end

# Class implementing PaymentMethod for NetBanking payments
class NetBanking
  include PaymentMethod

  # Processes the payment through NetBanking
  # @param amount [Float] The amount to be paid using NetBanking
  def pay(amount)
    # Simulate calling the API for NetBanking payment processing
    puts "Payment Processing for NetBanking for amount = #{amount}"
  end
end

# Factory class responsible for creating instances of different PaymentMethod implementations
# Follows the Factory design pattern
class PaymentFactory
  # Private constructor to prevent instantiation (Ruby doesn't need this explicitly,
  # but we define as a class method for clarity)
  private_class_method :new

  # Creates and returns an instance of a PaymentMethod based on the provided mode
  # @param mode [String] The payment mode (e.g., 'creditcard', 'debitcard', 'upi', 'netbanking')
  # @return [PaymentMethod, nil] A concrete implementation of PaymentMethod or nil if mode is unrecognized
  def self.create_payment(mode)
    # Return appropriate PaymentMethod object based on the payment mode
    case mode.downcase
    when 'creditcard'
      CreditCard.new
    when 'debitcard'
      DebitCard.new
    when 'upi'
      UPI.new
    when 'netbanking'
      NetBanking.new
    else
      nil # Return nil if the mode is invalid or unrecognized
    end
  end
end

# Demonstration of the Factory Pattern for creating payment method objects
# Calls the factory method to create payment methods and process payments
def main
  # Create a PaymentMethod instance for CreditCard and process a payment
  payment_method = PaymentFactory.create_payment('CreditCard')
  payment_method&.pay(102.22) # Process payment of amount 102.22 using CreditCard

  # Create a PaymentMethod instance for UPI and process a payment
  payment_method = PaymentFactory.create_payment('UPI')
  payment_method&.pay(343.24) # Process payment of amount 343.24 using UPI
end

# Run the demonstration if this file is executed directly
main if __FILE__ == $PROGRAM_NAME