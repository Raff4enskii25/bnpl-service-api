namespace Bnpl.Api.Infrastructure
{
    public class MoscowTimeProvider : TimeProvider
    {
        private readonly TimeZoneInfo _moscowZone;

        public MoscowTimeProvider()
        {
            _moscowZone = TimeZoneInfo.FindSystemTimeZoneById(OperatingSystem.IsWindows() ? "Russian Standard Time" : "Europe/Moscow");
        }

        public override TimeZoneInfo LocalTimeZone => _moscowZone;
    }
}
