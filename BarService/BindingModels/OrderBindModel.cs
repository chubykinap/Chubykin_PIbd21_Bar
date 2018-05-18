namespace BarService.BindingModels
{
    public class OrderBindModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int CocktailID { get; set; }
        public int? ExecutorID { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
