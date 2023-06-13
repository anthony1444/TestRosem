// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

namespace Marketplace.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Marketplace.Api.contracts;
    using Marketplace.Core.Bl;
    using Marketplace.Core.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Services for Users
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        #region Fields

        private readonly ILogger<OfferController> logger;

        private readonly IOfferBl offerBl;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="offerBl">The user business logic.</param>
        public OfferController(ILogger<OfferController> logger, IOfferBl offerBl)
        {
            
            
            this.offerBl = offerBl;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the list of users.
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<Offer>>> Get(int pageNumber = 1,int pageSize = 10) 
        {
            IEnumerable<Offer> result;

            try
            {
                result = await this.offerBl.GetOffersAsync(pageNumber, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(result);
        }

        
        [HttpGet("count")]
        public async Task<int> GetCountOffer() 
        {
            try
            {
                return await this.offerBl.GetCountOffer().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return -1;
            }
        }

        [HttpPost]
        public async Task<int> addNewOffer(OfferCreateRequest request) 
        {
            try
            {

        
                return await this.offerBl.AddNewOffer(
                    request.CategoryId,
                    request.Location,
                    request.Description,
                    request.Title,
                    request.PictureUrl
                ).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return -1;
            }
        }

        #endregion
    }
}