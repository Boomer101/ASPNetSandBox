using CatsLibrary;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;
using TryFastEndpoints.Cats.Dto;

namespace TryFastEndpoints.Cats.Api
{
    public class GetAllEndpoint : EndpointWithoutRequest<Results<Ok<CatsListResponse>, NotFound, ProblemDetails>>
    {
        private readonly ICatRepository _catRepository;

        public GetAllEndpoint(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public override void Configure()
        {
            Get("/api/cats");
            AllowAnonymous();
        }

        public override async Task<Results<Ok<CatsListResponse>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                var catsResult = await _catRepository.GetCatsAsync();

                var response = new CatsListResponse
                {
                    Cats = CatsMapper.ToCatsListResponse(catsResult)
                };

                return TypedResults.Ok(response);
            }
            catch (AggregateException ae)
            {
                var aggregatedExceptionsMessage = new StringBuilder();
                foreach (var ex in ae.InnerExceptions)
                {
                    aggregatedExceptionsMessage.AppendJoin(",", ex.Message);
                }

                AddError(aggregatedExceptionsMessage.ToString());

                return new ProblemDetails(ValidationFailures, 500);
            }
            catch (Exception ex)
            {
                AddError(ex.Message);

                return new ProblemDetails(ValidationFailures, 500);
            }
        }
    }
}
