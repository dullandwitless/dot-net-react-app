import React from 'react';

import Weather from './Weather';
import classes from './WeatherList.module.css';

const WeatherList = (props) => {
  return (
    <ul className={classes['weather-list']}>
      {props.weather.map((weather) => (
        <Weather
          title={weather.title}
          date={weather.date}
          temperatureC={weather.temperatureC}
        />
      ))}
    </ul>
  );
};

export default WeatherList;
