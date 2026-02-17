import './WeatherCard.css';

function WeatherCard({ forecast, index }) {
    const getTemperatureColor = (temp) => {
        if (temp <= 0) return 'temp-freezing';
        if (temp <= 15) return 'temp-cool';
        if (temp <= 25) return 'temp-warm';
        return 'temp-hot';
    };

    const getTemperatureIcon = (temp) => {
        if (temp <= 0) return '❄️';
        if (temp <= 15) return '🌤️';
        if (temp <= 25) return '☀️';
        return '🔥';
    };

    return (
        <div
            className="weather-card glass-card animate-fade-in"
            style={{ animationDelay: `${index * 100}ms` }}
        >
            <div className="weather-card-header">
                <span className="weather-icon">{getTemperatureIcon(forecast.temperatureC)}</span>
                <span className="weather-date">
                    {new Date(forecast.date).toLocaleDateString('en-US', {
                        month: 'short',
                        day: 'numeric',
                    })}
                </span>
            </div>

            <div className="weather-card-body">
                <div className={`weather-temp ${getTemperatureColor(forecast.temperatureC)}`}>
                    {forecast.temperatureC}°C
                </div>
                <div className="weather-temp-f">{forecast.temperatureF}°F</div>
            </div>

            <div className="weather-card-footer">
                <span className="weather-summary">{forecast.summary}</span>
            </div>
        </div>
    );
}

export default WeatherCard;
