using System.Runtime.Serialization;

namespace BarService.BindingModels
{
    [DataContract]
    public class OrderBindModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public int CocktailID { get; set; }
        [DataMember]
        public int? ExecutorID { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
