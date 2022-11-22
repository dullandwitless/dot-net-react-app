import React from 'react';

import classes from './Weather.module.css';

const Weather = (props) => {
  return (
    <li className={classes.weather}>
      <h2>{props.title}</h2>
      {/* <h3>{props.temperatureC} Degree Temperature</h3>
      <p>{props.summary}</p> */}
    </li>
  );
};
export default Weather;
