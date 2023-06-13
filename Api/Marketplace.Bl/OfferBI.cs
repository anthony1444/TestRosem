// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Bl;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;

namespace Marketplace.Bl;

/// <summary>
///     Offers' logic
/// </summary>
/// <seealso cref="Marketplace.Core.Bl.IOfferBl" />
public class OfferBl : IOfferBl
{
    #region Fields

    private readonly IOfferRepository offerRepository;

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="OfferBl" /> class.
    /// </summary>
    /// <param name="OfferRepository">The Offer repository.</param>
    public OfferBl(IOfferRepository offerRepository)
    {
        this.offerRepository = offerRepository;
    }



    #endregion

    #region Methods

    /// <inheritdoc />
    public async Task<IEnumerable<Offer>> GetOffersAsync(int pageNumber,int pageSize)
    {
        return await offerRepository.GetAllOffersAsync(pageNumber,pageSize).ConfigureAwait(false);
    }
    /// <inheritdoc />
   
    public async Task<int> GetCountOffer()
    {
        return await offerRepository.GetCountOffer().ConfigureAwait(false);

    }
    public async Task<int> AddNewOffer(int CategoryId,string Location, string Description, string Title, string PictureUrl)
    {
        Offer offer = new Offer();
        offer.CategoryId = Convert.ToByte(CategoryId);
        offer.Location = Location;
        offer.Description = Description;
        offer.Title = Title;
        offer.PictureUrl = PictureUrl;
        return await offerRepository.AddNewOffer(offer).ConfigureAwait(false);

    }
    #endregion
}