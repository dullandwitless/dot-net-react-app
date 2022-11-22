import { NavLink } from 'react-router-dom';

import classes from './MainHeader.module.css';

const MainHeader = () => {
  return (
    <header className={classes.header}>
      <nav>
        <ul>
          <li>
            Hello World App
          </li>
          {/* <li>
            <NavLink to='/empower'>
            Empower
            </NavLink>
          </li>
          <li>
            <NavLink to='/weatherForecast'>
            WeatherForecast
            </NavLink>
          </li> */}
        </ul>
      </nav>
    </header>
  );
};

export default MainHeader;
