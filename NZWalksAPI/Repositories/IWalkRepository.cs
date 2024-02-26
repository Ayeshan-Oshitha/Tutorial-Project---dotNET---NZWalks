﻿using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository
    {

        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync(); //Won't require any input parmeter, as a return type, It will output a "List of walks"


        Task<Walk?> GetByIdAsync(Guid id);


        Task<Walk?> UpdateAsync(Guid id, Walk walk);
    }
}
