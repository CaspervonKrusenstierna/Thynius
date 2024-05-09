import React, { useEffect, useState } from 'react'
import "./DashboardUserComponent.css"
import DUCProfileButton from './components/ducprofilebutton/DUCProfileButton'
import DUCDropdown from './components/ducdropdown/DUCDropdown'
import useDUCSizeState from './hooks/useDUCSizeState'

const DashboardUserComponent = () => {
  const sizeState = useDUCSizeState();
  const [showDropDown, setShowDropdown] = useState(false);

  function onProfileClick(){
    setShowDropdown(!showDropDown);
  }
  function onDropDownClick(e){
    if(!document.getElementById("UserComponent").contains(e.target) && !document.getElementById("dialog")?.contains(e.target)){
      setShowDropdown(false);
    }
  }

  return (
    <div id="UserComponent" className='DashboardUserComponent'>
      <DUCProfileButton sizeState={sizeState} onClick={onProfileClick}></DUCProfileButton>
      {showDropDown ? <DUCDropdown sizeState={sizeState} onClick={(e) => {onDropDownClick(e)}}></DUCDropdown> : <></>}
    </div>
  )
}

export default DashboardUserComponent