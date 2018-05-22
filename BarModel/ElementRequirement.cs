namespace BarModel
{
    public class ElementRequirement
    {
        public int ID { get; set; }
        public int CocktailID { get; set; }
        public int ElementID { get; set; }
        public int Count { get; set; }
        public virtual Cocktail Cocktail { set; get; }
        public virtual Element Element { set; get; }
    }
}
