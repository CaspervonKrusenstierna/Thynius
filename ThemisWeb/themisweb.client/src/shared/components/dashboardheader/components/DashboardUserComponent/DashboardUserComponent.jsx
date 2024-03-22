import React, { useState } from 'react'
import "./DashboardUserComponent.css"
import DashboardUserComponentDropDown from './components/dashboardusercomponentdropdown/DashboardUserComponentDropDown'
import DashboardUserComponentProfileButton from './components/dashboardusercomponentprofilebutton/DashboardUserComponentProfileButton'

const DashboardUserComponent = () => {
  const [showDropDown, setShowDropdown] = useState(false);


function onButtonClick(){
  setShowDropdown(!showDropDown);
}

  return (
    <div className='DashboardUserComponent'>
      <DashboardUserComponentProfileButton onClick={onButtonClick} clickState={showDropDown}></DashboardUserComponentProfileButton>
      {showDropDown ? <DashboardUserComponentDropDown></DashboardUserComponentDropDown> : <></>}
    </div>
  )
}

export default DashboardUserComponent