namespace BarModel
{
    public class ElementStorage
    {
        public int ID { get; set; }
        public int StorageID { get; set; }
        public int ElementID { get; set; }
        public int Count { get; set; }
        public virtual Storage Storage { set; get; }
        public virtual Element Element { set; get; }
    }
}
