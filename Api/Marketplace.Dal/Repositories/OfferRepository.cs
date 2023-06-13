// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System.Threading.Tasks;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;

namespace Marketplace.Dal.Repositories;

public class OfferRepository : IOfferRepository
{
    #region Fields

    private readonly MarketplaceDb _context;

    #endregion

    #region Constructors

    public OfferRepository()
    {
        _context = new MarketplaceDb();
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<Offer[]> GetAllOffersAsync(int pageNumber,int pageSize)
    {
        return await _context.GetOffersAsync(pageNumber,pageSize);
    }
    public async Task<int> GetCountOffer()
    {
        return await _context.GetCountOffer();
    }
    public async Task<int> AddNewOffer(Offer offer)
    {
        return await _context.AddNewOffer(offer);
    }

    #endregion
}