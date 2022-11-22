
import MainHeader from './components/MainHeader';
import React, { useState } from 'react';
import WeatherList from './components/WeatherList';
import './App.css';

function App() {
  const [weather, setWeather] = useState([]);

  function fetchWeatherDataHandler() {
    fetch('https://localhost:44316/WeatherForecast')
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        const transformedWeatherData = data.map((weatherData) => {
          return {
            id: weatherData.cityId,
            title: weatherData.summary,
            temperatureC: weatherData.temperatureC,
            date: weatherData.date,
            temperatureF: weatherData.temperatureF,
          };
        });
        setWeather(transformedWeatherData);
      });
  }

  return (
    <React.Fragment>
      <MainHeader/>
      <section>
        <button onClick={fetchWeatherDataHandler}>Fetch WeatherForecast</button>
      </section>
      <section>
        <WeatherList weather={weather} />
      </section>
    </React.Fragment>
  );
}

export default App;
