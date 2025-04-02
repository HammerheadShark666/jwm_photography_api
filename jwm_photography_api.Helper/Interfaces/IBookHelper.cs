namespace jwm_photography_api.Helpers.Interfaces;

public interface IBookHelper
{
    bool ValidISBN13(string code);

    bool ValidDiscount(decimal? price, decimal? discount, int? discountTypeId);
}