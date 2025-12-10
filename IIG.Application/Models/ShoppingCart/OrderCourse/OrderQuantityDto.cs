namespace IIG.Application.Models.OrderCourse;

public class OrderQuantityDto
{
    public Guid? OrderId { get; set; }
  
    public int Quantity { get; set; }
}