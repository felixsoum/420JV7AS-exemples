namespace AdapterProject
{
    class Program
    {
        static PaymentSystem paymentSystem = new PaymentSystem();

        static void Main(string[] args)
        {
            RunServerUS();
            RunServerKR();
        }

        static void RunServerUS()
        {
            paymentSystem.Pay2Win(1);
            paymentSystem.Pay2Win(1);
            paymentSystem.Pay2Win(1);
            paymentSystem.Pay2Win(1);
            paymentSystem.Pay2Win(1);
            paymentSystem.Pay2Win(50);
            paymentSystem.Pay2Win(50);
            paymentSystem.Pay2Win(50);
            paymentSystem.Pay2Win(50);
            paymentSystem.Pay2Win(1000);
        }

        static void RunServerKR()
        {
            paymentSystem.Pay2Win(1000);
            paymentSystem.Pay2Win(1000);
            paymentSystem.Pay2Win(1000);
            paymentSystem.Pay2Win(1000);
            paymentSystem.Pay2Win(1000);
            paymentSystem.Pay2Win(50000);
            paymentSystem.Pay2Win(50000);
            paymentSystem.Pay2Win(50000);
            paymentSystem.Pay2Win(50000);
            paymentSystem.Pay2Win(10000000);
        }
    }
}
