namespace Assets.Model
{
  public class ItemSlot
  {
    public Weapon Weapon;
    public Anh Anh;

    public ItemSlot(Weapon weapon)
    {
      Weapon = weapon;
    }

    public ItemSlot(Anh anh)
    {
      Anh = anh;
    }
  }
}