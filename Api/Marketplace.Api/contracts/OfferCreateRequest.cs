namespace Marketplace.Api.contracts;

public record OfferCreateRequest
(
    int CategoryId,
    string Location,
    string Description,
    string PictureUrl,
    string Title
);
