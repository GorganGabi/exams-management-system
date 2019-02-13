import React      from 'react';

// import Button from "@material-ui/core/Button";

import Paper            from "../core/Paper";
import SettingsDetails  from './SettingsDetails'
import SettingsPassword from './SettingsPassword'
import SettingsFeedback from './SettingsFeedback'

import '../../styles/css/Settings.css'

class Settings extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      routeName: 'details',
    };
  }

  onTabClick = (routeName) => {
    this.setState({
      routeName: routeName
    })
  }

  render() {
    return (
      <div className='settings'>
        <p></p>
        <Paper className='settings__paper'>
          <div className='settings__header'>
            <div
              onClick   = {() => this.onTabClick('details')}
              className = {`settings__header-tab ${this.state.routeName === 'details' ? 'settings__header-tab--active' : ''}`}
            >Your Details</div>
            <div
              onClick   = {() => this.onTabClick('password')}
              className = {`settings__header-tab ${this.state.routeName === 'password' ? 'settings__header-tab--active' : ''}`}
            >Password</div>
            <div
              onClick   = {() => this.onTabClick('feedback')}
              className = {`settings__header-tab ${this.state.routeName === 'feedback' ? 'settings__header-tab--active' : ''}`}
            >Feedback</div>
          </div>

          <div className="settings__content">
            {(() => {
              switch(this.state.routeName) {
                case 'details':
                  return <SettingsDetails />;
                case 'password':
                  return <SettingsPassword />;
                case 'feedback':
                  return <SettingsFeedback />;
                default:
                  return null;
              }
            })()}
        </div>
        </Paper>
      </div>
    )
  }
}

export default Settings;
