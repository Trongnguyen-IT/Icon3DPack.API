using Icon3DPack.API.Application.Models.WeatherForecast;

namespace Icon3DPack.API.Application.Services;

public interface IWeatherForecastService
{
    public Task<IEnumerable<WeatherForecastResponseModel>> GetAsync();
}
