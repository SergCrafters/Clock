
public class ClockConstants
{
    public const int SECONDS_PER_MINUTE = 60;
    public const int MINUTES_PER_HOUR = 60;
    public const int HOURS_PER_DAY = 24;
    public const float MILLISECONDS_PER_SECOND = 1000f;

    public const int HOURS_ON_ANALOG_CLOCK = 12;

    public const float FULL_CIRCLE_DEGREES = 360f;
    public const float DEGREES_PER_SECOND_AND_MINUTE = FULL_CIRCLE_DEGREES / SECONDS_PER_MINUTE;
    public const float DEGREES_PER_HOUR = FULL_CIRCLE_DEGREES / HOURS_ON_ANALOG_CLOCK;

    public const double SECONDS_PER_HOUR = SECONDS_PER_MINUTE * MINUTES_PER_HOUR;
    public const double SECONDS_PER_DAY = HOURS_PER_DAY * SECONDS_PER_HOUR;

    public const string TWO_DIGIT_FORMAT = "D2";

    public const string TIME_FORMAT = "{0:00}:{1:00}:{2:00}";

    public const string TIME_API_URL = "https://yandex.com/time/sync.json";
    public const int REQUEST_TIMEOUT_SECONDS = 5;

    public const int MIN_HOUR = 0;
    public const int MAX_HOUR = 23;
    public const int MIN_MINUTE = 0;
    public const int MAX_MINUTE = 59;
    public const int MIN_SECOND = 0;
    public const int MAX_SECOND = 59;
}
