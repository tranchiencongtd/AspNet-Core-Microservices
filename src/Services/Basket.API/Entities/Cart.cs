namespace Basket.API.Entities
{
  public class Cart
  {

    public Cart()
    {
    }

    public Cart(string userName)
    {
      UserName = userName;
    }

    public string UserName { get; set; }
    public List<CartItem> Items { get; set; }
    public decimal TotalPrice => Items.Sum(x => x.ItemPrice * x.Quantity);

  }
}
