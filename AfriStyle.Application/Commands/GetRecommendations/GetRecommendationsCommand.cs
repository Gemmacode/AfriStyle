using AfriStyle.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfriStyle.Application.Commands.GetRecommendations
{
    public record GetRecommendationsCommand(
     RecommendationRequestDto Request
 ) : IRequest<RecommendationResponseDto>;
}
