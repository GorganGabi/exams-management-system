import React     from 'react';

import ReactDropdown from 'react-dropdown'
import '../../styles/css/core/Dropdown.css'

class Dropdown extends React.Component {

  render() {
    return
      <div className='dropdown'>
        <ReactDropdown {...@props} ref={ (r) => @props.refDropdown?(r) } />
      </div>
}
Dropdown.defaultProps =
  arrowClassName: 'fa fa-caret-down'

export default Dropdown
