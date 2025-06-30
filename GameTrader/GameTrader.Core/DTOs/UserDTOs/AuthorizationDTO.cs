using System.Net;

namespace GameTrader.Core.DTOs.UserDTOs
{
    public record AuthorizationDTO(string UserId, HttpStatusCode StatusCode);
}
