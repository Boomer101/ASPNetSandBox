using CatsLibrary;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;
using TryFastEndpoints.Cats.Dto;

namespace TryFastEndpoints.Cats.Api
{
    public class GetCatEndpoint : EndpointWithoutRequest<Results<Ok<CatResponse>, NotFound, ProblemDetails>>
    {
        private readonly ICatRepository _catRepository;

        public GetCatEndpoint(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public override void Configure()
        {
            Get("/api/cat");
            AllowAnonymous();
        }

        public override async Task<Results<Ok<CatResponse>, NotFound, ProblemDetails>> ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                var catId = Query<int>("id");

                var catsResult = await _catRepository.GetCatAsync(catId);

                var response = CatsMapper.ToCatResponse(catsResult);

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
