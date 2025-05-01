namespace AL_Nibras_Ecom_API.Model.Order
{
    public class Order
    {

        public class cartMaster
        {
            public int? ProductId { get; set; }
            public int? VariantId { get; set; }
            public int Quantity { get; set; }
        }
        public class ReviewRating
        {
            public int ProductId { get; set; }
            public string ReviewText { get; set; }
            public decimal Rating { get; set; }
        }

        public class AddressDetails
        {
            public string UserName { get; set; }
            public string Address { get; set; }
            public string District { get; set; }
            public string City { get; set; }
            public string PinCode { get; set; }
            public string PhoneNumber { get; set; }
            public string AddressLabel { get; set; }
            public string LandMark { get; set; }

        }

        public class Order_Header
        {
            public string UserName { get; set; }
            public string OrderDate { get; set; }
            public decimal OrderAmount { get; set; }
            public string PaymentMode { get; set; }
            public string PaymentReference { get; set; }
            public decimal TaxAmount { get; set; }
            public int AddressId { get; set; }
            public List<Order_Details> OrderDetails { get; set; }
        }
        public class Order_Details
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal TotalAmount { get; set; }
        }


    }
}
