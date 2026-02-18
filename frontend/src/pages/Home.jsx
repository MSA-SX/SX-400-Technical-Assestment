import { useState, useEffect } from 'react';
import apiClient from '../api/apiClient';
import WeatherCard from '../components/WeatherCard';
import './Home.css';

function Home() {
    const [forecasts, setForecasts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetchForecasts();
    }, []);

    const fetchForecasts = async () => {
        try {
            setLoading(true);
            setError(null);
            const response = await apiClient.get('/weather');
            if (Array.isArray(response.data)) {
                setForecasts(response.data);
            } else {
                throw new Error('Received invalid data format (likely HTML instead of JSON). Check VITE_API_URL.');
            }
        } catch (err) {
            setError(
                err.response?.status === 0
                    ? 'Cannot reach the API. Make sure the backend is running.'
                    : `Error: ${err.response?.statusText || err.message}`
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <section className="home">
            <div className="home-hero animate-fade-in">
                <h2 className="home-title">
                    Weather <span className="gradient-text">Forecasts</span>
                </h2>
                <p className="home-description">
                    Real-time forecast data powered by ASP.NET Core API and Azure SQL.
                </p>
            </div>

            {/* Stats bar */}
            <div className="stats-bar animate-fade-in" style={{ animationDelay: '150ms' }}>
                <div className="stat-item glass-card">
                    <span className="stat-value">{forecasts.length}</span>
                    <span className="stat-label">Records</span>
                </div>
                <div className="stat-item glass-card">
                    <span className="stat-value">
                        {forecasts.length > 0
                            ? `${Math.round(forecasts.reduce((sum, f) => sum + f.temperatureC, 0) / forecasts.length)}°`
                            : '--'}
                    </span>
                    <span className="stat-label">Avg Temp</span>
                </div>
                <div className="stat-item glass-card">
                    <span className="stat-value">
                        {forecasts.length > 0
                            ? `${Math.max(...forecasts.map((f) => f.temperatureC))}°`
                            : '--'}
                    </span>
                    <span className="stat-label">Max Temp</span>
                </div>
            </div>

            {/* Content */}
            {loading && (
                <div className="home-loading">
                    <div className="loading-spinner"></div>
                    <p>Loading forecasts...</p>
                </div>
            )}

            {error && (
                <div className="home-error glass-card">
                    <span className="error-icon">⚠️</span>
                    <p>{error}</p>
                    <button className="btn-retry" onClick={fetchForecasts}>
                        Retry
                    </button>
                </div>
            )}

            {!loading && !error && (
                <div className="weather-grid">
                    {forecasts.map((forecast, index) => (
                        <WeatherCard key={forecast.id} forecast={forecast} index={index} />
                    ))}
                </div>
            )}

            {!loading && !error && forecasts.length === 0 && (
                <div className="home-empty glass-card">
                    <span className="empty-icon">📭</span>
                    <p>No forecasts available.</p>
                </div>
            )}
        </section>
    );
}

export default Home;
