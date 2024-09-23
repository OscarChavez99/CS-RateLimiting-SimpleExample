namespace CS_RateLimiting_SimpleExample.Application
{
    public class ApiTestHandler
    {
        public IResult TwoTimesPerIp()
        {
            return Results.Ok("You can test me just 2 times per IP!");
        }

        public IResult ThreeTimesPerMinute()
        {
            return Results.Ok("You can test me just 3 times per minute!");
        }
    }
}
