﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

internal class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService
    ) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
        {
            throw new ForbiddenException();
        }

        mapper.Map(request, restaurant);
    

        await restaurantsRepository.SaveChanges();

    }
}
