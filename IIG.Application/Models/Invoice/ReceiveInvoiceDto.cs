namespace IIG.Web.Data.Models.Invoice;

public class ReceiveInvoiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string TaxCode { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
    public string OrderCode { get; set; }
    public long ReceiptCode { get; set; }
}